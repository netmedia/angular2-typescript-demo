using System;

namespace Netmedia.Common.Extensions
{
    public static class PathExtensions
    {
        public static string RelativeToWebPath(this string relativePath, bool forceStartWithSlash = false, bool appendTilda = false, string startPathWith = "")
        {
            relativePath = relativePath.Replace(@"\", "/");
            
            if (forceStartWithSlash && relativePath.StartsWith("/") == false)
            {
                relativePath = "/" + relativePath;
            }
            if (startPathWith.IsNotNullOrEmpty())
            {
                relativePath = startPathWith + relativePath;
            }
            if (appendTilda && relativePath.StartsWith("~") == false)
            {
                if (relativePath.StartsWith("/") == false) relativePath = "/" + relativePath;
                
                relativePath = "~" + relativePath;
            }

            return relativePath;
        }
        public static string FileInWebPath(this string relativePath, string file, bool forceStartWithSlash = false, bool appendTilda = false, string startPathWith = "")
        {
            var path = relativePath.RelativeToWebPath(forceStartWithSlash, appendTilda, startPathWith);
            if (path.EndsWith("/") == false) path = path + "/";
            
            return  path + file;
        }
    }
}