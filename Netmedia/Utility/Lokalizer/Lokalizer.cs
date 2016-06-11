using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Castle.Components.DictionaryAdapter.Xml;
using Castle.Core.Internal;
using Netmedia.Common.Extensions;

namespace Netmedia.Utility.Lokalizer
{
    public class Lokalizer
    {
        private readonly string[] namespaces;

        private readonly List<string> sourceAssemblyPaths = new List<string>();
        private readonly string destinationPath;
        private readonly CultureInfo culture;

        private Assembly assembly;
        private readonly List<string> xmlChunks = new List<string>();
        private List<string> warnings = new List<string>();

        public bool UseTimestampInFilename { get; set; }

        public string[] Warnings
        {
            get { return warnings.ToArray(); }
        }

        /// <summary>
        /// Initializes lokalizer app, checks and assignes source and destination paths.
        /// </summary>
        /// <param name="sourceAssemblyPaths">Paths to the assembly from which class types will be used for localization generation.</param>
        /// <param name="namespaces">Namespaces to include in generation. If null, all classes will be included.</param>
        /// <param name="destinationXmlPath">Path (folder) where localization files will be created.</param>
        /// <param name="culture">Name of the class to process.</param>
        public Lokalizer(IEnumerable<string> sourceAssemblyPaths, IEnumerable<string> namespaces, string destinationXmlPath, CultureInfo culture)
        {
            foreach (var asseblyFile in sourceAssemblyPaths)
            {
                if (File.Exists(asseblyFile) == false)
                    warnings.Add(string.Format("File {0} doesn't exist or specified path is not correct!", asseblyFile));
                else
                    this.sourceAssemblyPaths.Add(asseblyFile);
            }

            if (Directory.Exists(destinationXmlPath) == false)
                throw new Exception("Directory doesn't exist!");
            destinationPath = destinationXmlPath;

            this.namespaces = namespaces.ToArray();
            this.culture = culture;
            UseTimestampInFilename = true;
        }

        /// <summary>
        /// Process all assemblies and their classes that reside inside specified namespaces.
        /// </summary>
        public void Process()
        {
            foreach (var path in sourceAssemblyPaths)
            {
                assembly = Assembly.LoadFrom(path.Trim());
                var tryGetTypesResult = assembly.DefinedTypes;

                //AppDomain currentAd = AppDomain.CurrentDomain;
                //AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve;
                //currentAd.SetData(currentAssemblyKey, Path.GetDirectoryName(path));

                ProcessTypes();
            }
        }

        /// <summary>
        /// Process assemblies and generate localization file.
        /// NOTE: Generated file will always have prefixed timestamp in its filename. 
        /// If you want to disable that option, set UseTimestampInFilename to false!
        /// </summary>
        /// <param name="_types"></param>
        public void ProcessTypes(Type[] _types = null){
            var types = _types ?? assembly.DefinedTypes.Where(t => namespaces.Contains(t.Namespace)).ToArray();

            foreach (var type in types)
            {
                if (type == null || Attribute.GetCustomAttributes(type).Any(a => a is LokalizerIgnoreAttribute))
                    continue;

                foreach (var propertyInfo in type.GetProperties())
                {
                    var attributes = propertyInfo.GetCustomAttributesData();
                    if (attributes.Count == 0) continue;

                    var attribute = attributes.FirstOrDefault(a => a.AttributeType == typeof(LokalizerAttribute));
                    if (attribute == null) continue;

                    if (attribute.NamedArguments == null ||
                        (attribute.NamedArguments != null && attribute.NamedArguments.Count == 0))
                        throw new Exception("No named arguments on Lokalizer attribute.");

                    var localizedValue =
                        attribute.NamedArguments.First(na => na.MemberName == "Name").TypedValue.Value.ToString();
                    _GenerateChunk(type.Name, propertyInfo.Name, localizedValue);
                }

                string line;
                var sb = new StringBuilder();
                var file = new StreamReader(@"D:\DEVELOPMENT\nava\crm\trunk\Netmedia\Utility\Lokalizer\LokalizerXmlTemplate.xml");
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Trim() == "<!--\"*=*= INSERT HERE =*=*\"-->")
                        xmlChunks.ForEach(c => sb.AppendLine(c));
                    else
                        sb.AppendLine(line);
                }
                file.Close();

                _SaveToFile(sb.ToString());
            }
        }

        private void _GenerateChunk(string className, string property, string propertyLocalized)
        {
            var xmlChunk = string.Format(
                "<data name=\"{0}_{1}\" xml:space=\"preserve\">"+
                    "<value>{2}</value>"+
                "</data>", className, property, propertyLocalized);

            xmlChunks.Add(xmlChunk);
        }

        private void _SaveToFile(string xml)
        {
            var timestamp = DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Year.ToString();

            if (UseTimestampInFilename)
                timestamp = timestamp + "_";
            else
                timestamp = string.Empty;

            var designerPath = string.Format("{0}\\{1}ModelLocalization.{2}.resx", destinationPath, timestamp, culture.Name);
            using (var file = new StreamWriter(designerPath)){
                file.Write(xml);
            }

            var resxPath = string.Format("{0}\\{1}ModelLocalization.{2}.Designer.cs", destinationPath, timestamp, culture.Name);
            using (var file = new StreamWriter(resxPath)){
                file.Write("");
            }
        }

        //static Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs e)
        //{
        //    var name = new AssemblyName(e.Name);
        //    string assemblyPath = Path.Combine((string)AppDomain.CurrentDomain.GetData(currentAssemblyKey), name.Name + ".dll");

        //    if (File.Exists(assemblyPath))
        //    {
        //        // The dependency was found in the same directory as the base
        //        return Assembly.ReflectionOnlyLoadFrom(assemblyPath);
        //    }
        //    else
        //    {
        //        // Wasn't found on disk, hopefully we can find it in the GAC...
        //        var newPath = Path.Combine(@"C:\Windows\Microsoft.NET\Framework\v4.0.30319", name.Name + ".dll");
        //        if (File.Exists(newPath))
        //            return Assembly.ReflectionOnlyLoadFrom(newPath);
        //        else
        //            return Assembly.ReflectionOnlyLoad(name.Name);
        //    }
            
        //    //            return Assembly.ReflectionOnlyLoadFrom(args.Name);
        //}
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class LokalizerAttribute : Attribute {
        public string Name { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class LokalizerIgnoreAttribute : Attribute {}
}
