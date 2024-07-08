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
            string userName = ExtractUserName(emailTo);

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom, "iChurchConnect Team");
                mail.To.Add(emailTo);
                mail.Subject = "Your OTP Code for iChurchConnect Verification";
                mail.Body = GenerateBodyMessage(otp, userName);
                mail.IsBodyHtml = true; // Enable HTML formatting

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
            return otp;
        }

        private string GenerateBodyMessage(string otp, string userName)
        {
            return $@"
<p style='font-size: 14pt; font-family: Arial, sans-serif;'>Dear {userName},</p>

<p style='font-size: 14pt; font-family: Arial, sans-serif;'>Greetings from the iChurch.Connect team!</p>

<p style='font-size: 14pt; font-family: Arial, sans-serif;'>To ensure the security of your account, please use the One-Time Password (OTP) provided below to complete your verification process. This code is valid for the next 10 minutes.</p>

<p style='font-size: 16pt; font-family: Arial, sans-serif; font-weight: bold;'>Your OTP Code: {otp}</p>

<p style='font-size: 14pt; font-family: Arial, sans-serif;'>Please do not share this code with anyone. If you did not request this OTP, please contact our support team immediately.</p>

<p style='font-size: 14pt; font-family: Arial, sans-serif;'>Thank you for choosing iChurch.Connect. We are committed to keeping your account safe and secure.</p>

<p style='font-size: 14pt; font-family: Arial, sans-serif;'>Best regards,<br>The iChurchConnect Team</p>

<hr>

<p style='font-size: 12pt; font-family: Arial, sans-serif;'>For support, please contact us at support@ichurchconnect.com</p>

<p style='font-size: 12pt; font-family: Arial, sans-serif;'>This is an automated message, please do not reply.</p>
";
        }

        private string ExtractUserName(string email)
        {
            string userName = email.Split('@')[0];
            return userName.Replace(".", " "); // Replace dots with spaces for better formatting
        }

        private string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
