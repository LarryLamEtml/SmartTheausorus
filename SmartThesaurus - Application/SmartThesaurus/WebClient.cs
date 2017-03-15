using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;

namespace SmartThesaurus
{
    class Web : Component
    {
       

        public Web()
        {
            string formUrl = "https://www.educanet2.ch"; // NOTE: This is the URL the form POSTs to, not the URL of the form (you can find this in the "action" attribute of the HTML's form tag
            string formParams = string.Format("login_login_right={0}&login_password_right={1}", "your email", "your password");
            string cookieHeader;
            WebRequest req = WebRequest.Create(formUrl);
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            byte[] bytes = Encoding.ASCII.GetBytes(formParams);
            req.ContentLength = bytes.Length;
            using (Stream os = req.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }
            WebResponse resp = req.GetResponse();
            cookieHeader = resp.Headers["Set-cookie"];
        }
    }
}
