using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;
using Experimental.System.Messaging;

namespace CommonLayer.UserModel
{
    public class MSMQModel
    {
        MessageQueue messageQ = new MessageQueue();

        public void sendData2Queue(string token)
        {
            messageQ.Path = @".\private$\Tokens";
            if (!MessageQueue.Exists(messageQ.Path))
            {
                MessageQueue.Create(messageQ.Path);

            }
            messageQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQ.ReceiveCompleted += MessageQ_ReceiveCompleted; ;
            messageQ.Send(token);
            messageQ.BeginReceive();
            messageQ.Close();
        }

        private void MessageQ_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = messageQ.EndReceive(e.AsyncResult);
            string token = msg.Body.ToString();
            string Subject = "Fundoo notes Token LINK";
            string Body = token;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("yogeshprojectdemo@gmail.com");
            mail.To.Add("yogeshprojectdemo@gmail.com");
            mail.Subject = "subject";
            mail.IsBodyHtml = true;

            string htmlbody;

            htmlbody = $"Fundoo Notes Reset Password: <a href=http://localhost:4200/User/ResetPassword/{token}> Click Here</a>";

            //htmlbody = "<body><p>Dear User,<br>" +
            //            "Copy below token to reset your password.<br></body>" +
            //            token;

            mail.Body = htmlbody;


            var SMTP = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("yogeshprojectdemo@gmail.com", "rhvzdxnfxijuutjd"),
                EnableSsl = true,
            };

            //SMTP.Send("yogeshprojectdemo@gmail.com", "yogeshprojectdemo@gmail.com",Subject,Body);
            SMTP.Send(mail);
            // from, recieveing, subject, body
            messageQ.BeginReceive();
        }

    }
}
