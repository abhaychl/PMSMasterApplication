
using System.Net.Mail;

namespace PMSMaster.Utility
{
    public class EmailService
    {
        public bool SendEmail(string Fromemail, string Toemail, string[] CCemail, string bcc, string Subject, string body, List<Attachment> file)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(Fromemail);

                mail.To.Add(Toemail);
                if (CCemail != null && CCemail.Length > 0)
                {
                    foreach (var item in CCemail)
                    {
                        if (!string.IsNullOrWhiteSpace(item))
                            mail.CC.Add(item);
                    }
                }
                if (!string.IsNullOrEmpty(bcc))
                    mail.Bcc.Add(bcc);
                mail.Subject = Subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp-legacy.office365.com");
                smtp.Port = 587;
                smtp.EnableSsl = true;
                //string gmailAppPassword = user.Password;

                //if (!string.IsNullOrWhiteSpace(user.AppPass))
                //    gmailAppPassword = user.AppPass;

                //smtp.Credentials = new System.Net.NetworkCredential(user.Email, gmailAppPassword);
                smtp.Credentials = new System.Net.NetworkCredential("chlenquiry@crystalhues.com", "Bor80022");
                if (file != null && file.Count > 0)
                {
                    foreach (var upLoadFIle in file)
                    {
                        mail.Attachments.Add(upLoadFIle);
                    }
                }
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                //ILog Logger = LogManager.GetLogger(System.Environment.MachineName);
                //Logger.Error("Email Sending Failed : " + ex.Message, ex);
                return false;
            }
            return true;
        }
    }
}
