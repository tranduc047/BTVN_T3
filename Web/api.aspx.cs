using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using lib;
using capcha;
using Newtonsoft.Json;

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
                case "capcha":
                    GenerateCaptcha();
                    break;
                case "verifyCaptcha":
                    string captchaInput = Request["captcha"];
                    string result = VerifyCaptcha(captchaInput);
                    Response.ContentType = "application/json";
                    Response.Write(result);
                    Response.End();
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

        private void GenerateCaptcha()
        {
            CaptchaGenerator generator = new CaptchaGenerator();
            string captchaCode = generator.GenerateRandomCode(6);
            Session["CaptchaCode"] = captchaCode;

            using (Bitmap captchaImage = generator.GenerateCaptchaImage(captchaCode))
            {
                Response.ContentType = "image/png";
                captchaImage.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        [WebMethod]
        public static string VerifyCaptcha(string captcha)
        {
            string correctCaptcha = HttpContext.Current.Session["CaptchaCode"] as string;
            bool isCorrect = captcha.Equals(correctCaptcha, StringComparison.OrdinalIgnoreCase);

            var result = new { success = isCorrect };
            return JsonConvert.SerializeObject(result);
        }
    }
}
