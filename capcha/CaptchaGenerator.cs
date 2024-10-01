using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace capcha
{
    public class CaptchaGenerator
    {
        private static Random random = new Random();

        public string GenerateRandomCode(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] stringChars = new char[length];
            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            return new string(stringChars);
        }

        public Bitmap GenerateCaptchaImage(string captchaCode, int width = 200, int height = 80)
        {
            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.FillRectangle(Brushes.White, 0, 0, width, height);

                Font[] fonts = {
                    new Font("Arial", 24, FontStyle.Bold),
                    new Font("Georgia", 24, FontStyle.Bold),
                    new Font("Tahoma", 24, FontStyle.Bold),
                };

                for (int i = 0; i < captchaCode.Length; i++)
                {
                    Font font = fonts[random.Next(fonts.Length)];
                    PointF point = new PointF(i * 30 + random.Next(10), random.Next(5, 20));
                    g.DrawString(captchaCode[i].ToString(), font, Brushes.Black, point);

                    float angle = random.Next(-30, 30);
                    g.RotateTransform(angle);
                    g.ResetTransform();
                }

                DrawRandomLines(g, width, height);
            }
            return bitmap;
        }

        private void DrawRandomLines(Graphics g, int width, int height)
        {
            Pen pen = new Pen(Color.Gray);
            for (int i = 0; i < 5; i++)
            {
                int x1 = random.Next(width);
                int y1 = random.Next(height);
                int x2 = random.Next(width);
                int y2 = random.Next(height);
                g.DrawLine(pen, x1, y1, x2, y2);
            }
        }

        public byte[] GetCaptchaImageBytes(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
    