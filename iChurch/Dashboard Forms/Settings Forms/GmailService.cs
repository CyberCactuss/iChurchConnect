using System;
using System.Net;
using System.Net.Mail;

namespace iChurch.Dashboard_Forms.Settings_Forms
{
    internal class GmailService
    {
        private const string smtpAddress = "smtp.gmail.com";
        private const int portNumber = 587;
        private const bool enableSSL = true;
        private const string emailFrom = "ichurch.connect.team@gmail.com"; // Your email
        private const string password = "bvxh zubt cbkr vtel"; // Your app password

        public string SendOTP(string emailTo)
        {
            string otp = GenerateOTP();
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = "Your OTP Code for iChurchConnect Verification";
                mail.Body = GenerateBodyMessage(otp);
                mail.IsBodyHtml = false;

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
            return otp;
        }
        private string GenerateBodyMessage(string otp)
        {
            return $@"
Dear [User's Name],

Greetings from the iChurch.Connect team!

To ensure the security of your account, please use the One-Time Password (OTP) provided below to complete your verification process. This code is valid for the next 10 minutes.

Your OTP Code: {otp}

Please do not share this code with anyone. If you did not request this OTP, please contact our support team immediately.

Thank you for choosing iChurch.Connect. We are committed to keeping your account safe and secure.

Best regards,
The iChurchConnect Team

---

For support, please contact us at support@ichurchconnect.com

This is an automated message, please do not reply.
";
        }


        private string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
