using System;
using System.Drawing;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using capcha;
using lib;
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
                    GenerateCaptcha(); // Gọi hàm tạo CAPTCHA
                    break;
                    // Xóa phương thức VerifyCaptcha khỏi phần Page_Load
            }
        }

        // Hàm khởi tạo kết nối cơ sở dữ liệu
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
            // Lấy mã CAPTCHA từ session
            string correctCaptcha = HttpContext.Current.Session["CaptchaCode"] as string;

            // So sánh mã CAPTCHA
            bool isCorrect = captcha.Equals(correctCaptcha, StringComparison.OrdinalIgnoreCase);

            // Tạo phản hồi JSON
            var result = new { success = isCorrect };
            return JsonConvert.SerializeObject(result);
        }
    }
}
