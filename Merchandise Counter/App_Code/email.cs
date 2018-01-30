using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Net.Mail;
using System.Data.OleDb;

/// <summary>
/// Summary description for email
/// </summary>
public class email
{

	public email()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static void sendConfirmationEmail(string abstractID)
    {
        Functionality fn = new Functionality();
        DataSet ds = fn.GetDatasetByCommand("select * from UploadPoster where abstractID= '" + abstractID + "'", "UploadPoster");
        DataTable dt = ds.Tables[0];
            string posterAuthor = dt.Rows[0]["posterAuthor"].ToString();
            string posterTitle = dt.Rows[0]["posterTitle"].ToString();
            string Email = dt.Rows[0]["email"].ToString();
            string abstractId = abstractID;

            Alpinely.TownCrier.TemplateParser tm = new Alpinely.TownCrier.TemplateParser();
            Alpinely.TownCrier.MergedEmailFactory factory = new Alpinely.TownCrier.MergedEmailFactory(tm);
            string template = HttpContext.Current.Server.MapPath("~/template/confirmationemail.html");
            var tokenValues = new Dictionary<string, string>
            {
                    {"author", posterAuthor},
                    {"poster", abstractID},
                    {"title", posterTitle}
            };
            MailMessage message = factory
          .WithTokenValues(tokenValues)
          .WithHtmlBodyFromFile(template)
          .Create();
            message.From = new MailAddress("randylai94@gmail.com", "IDA2013");
            message.To.Add(new MailAddress(Email));
            message.CC.Add(new MailAddress("onglinsheng@corpit.com.sg"));
            message.Bcc.Add(new MailAddress("alvinlim@corpit.com.sg"));
            SmtpClient emailClient = new SmtpClient("smtp.gmail.com");
            System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential("corpitmail@event-admin.biz", "c0rpm41l");
            emailClient.UseDefaultCredentials = false;
            emailClient.EnableSsl = true;
            emailClient.Credentials = SMTPUserInfo;
            emailClient.Send(message);
    }
    public static void sendAdministratorAlert(string abstractID)
    {
        Functionality fn = new Functionality();
        DataSet ds = fn.GetDatasetByCommand("select * from UploadPoster where abstractID='" + abstractID + "'", "UploadPoster");
        DataTable dt = ds.Tables[0];
            string abstractId = abstractID;
            string posterAuthor = dt.Rows[0]["posterAuthor"].ToString();
            string posterTitle = dt.Rows[0]["posterTitle"].ToString();
            string Email = dt.Rows[0]["email"].ToString();
            string posterCategory = dt.Rows[0]["posterCategory"].ToString();

            Alpinely.TownCrier.TemplateParser tm = new Alpinely.TownCrier.TemplateParser();
            Alpinely.TownCrier.MergedEmailFactory factory = new Alpinely.TownCrier.MergedEmailFactory(tm);
            string template = HttpContext.Current.Server.MapPath("~/template/adminAlert.html");
            var tokenValues = new Dictionary<string, string>
            {
                    {"author", posterAuthor},
                    {"poster", abstractID},
                    {"title", posterTitle},
                    {"category", posterCategory},
                    {"email", Email}
            };
            MailMessage message = factory
          .WithTokenValues(tokenValues)
          .WithHtmlBodyFromFile(template)
          .Create();
            message.From = new MailAddress("randylai94@gmail.com", "IDA2013");
            message.To.Add(new MailAddress("onglinsheng@corpit.com.sg"));
            message.CC.Add(new MailAddress("alvinlim@corpit.com.sg"));
            message.Bcc.Add(new MailAddress("randylai@corpit.com.sg"));
            SmtpClient emailClient = new SmtpClient("smtp.gmail.com");
            System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential("corpitmail@event-admin.biz", "c0rpm41l");
            emailClient.UseDefaultCredentials = false;
            emailClient.EnableSsl = true;
            emailClient.Credentials = SMTPUserInfo;
            emailClient.Send(message);
        }
}