using System.Net.Mail;
using System.Net;
using System.Text;
using MailKit.Security;
using MimeKit;
using Org.BouncyCastle.Utilities.Net;
using System;

namespace BackEndWebShop.Helper
{
    public class SendEmail
    {
        private readonly Random _random = new Random();

        public string Link(string email)
        {
            string urlFriendlyString = WebUtility.UrlEncode(email);
            string code = "https://localhost:7147/Controller/Confirm?Email=" + urlFriendlyString;

            return code;
        }
        
        public void ConfirmationMail(string toAddress)
        {
            // email và password người gửi  
            string fromMail = "chaungocminh202@gmail.com";
            string fromPassword = "clzabmghjapybojg";

            // Nội dung thư
            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Validate registration";
            message.To.Add(new MailAddress(toAddress));

            message.Body = "<h1>Click on the link to activate your account: </h1>" +  "<a href = \" " + Link(toAddress) +  "  \"> Chuyển đến trang chủ </a>  ";
 
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }
    }
}

