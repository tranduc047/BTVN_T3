using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using lib;


namespace Web
{
    public partial class api : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = this.Request["action"];

            switch (action)
            {
                case "get_status":
                    get_status();
                    break;
                case "get_history":
                    get_history();
                    break;
            }
        }
        Class1 get_db()
        {
            Class1 db = new Class1();
            db.cnstr = "Data Source=LD\\SQLEXPRESS;Initial Catalog=QLPhong;Integrated Security=True;";
            return db;
        }
        void get_status()
        {
            Class1 db = get_db();
            string json = db.get_status();
            this.Response.Write(json);
        }

        void get_history()
        {
            Class1 db = get_db();
            int a = int.Parse(this.Request["ma_phonghoc"]);
            string json = db.get_history(a);
            this.Response.Write(json);
        }

    }
}