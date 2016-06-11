using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Netmedia.DumpDay.Data;
using Netmedia.DumpDay.Models;

namespace Netmedia.DumpDay
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            _BindRepeater();
        }

        protected void SaveSessionButton_OnClick(object sender, EventArgs e)
        {
            var session = new Session()
            {
                Title = TitleTextBox.Text,
                Link = LinkTextBox.Text,
                Votes = 0
            };

            //db.Sessions.Add(session);
            //db.SaveChanges();

            _BindRepeater();
        }

        protected void SearchSessionsButton_OnTextChanged(object sender, EventArgs e)
        {
            _BindRepeater();
        }

        protected void SessionsRepeater_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            _BindRepeater();
        }

        private void _BindRepeater()
        {
            using (var db = new ModelContext())
            {
                IQueryable<Session> sessions = db.Sessions;

                if (SearchSessionsTextBox.Text != "")
                {
                    var search = SearchSessionsTextBox.Text;

                    sessions = sessions.Where(s => s.Title.Contains(search));
                }

                SessionsRepeater.DataSource = sessions.ToList();
                SessionsRepeater.DataBind();
            }               
        }
    }
}