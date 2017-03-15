using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartThesaurus
{
    public partial class EducanetLogin : Form
    {
        private string email;
        private string password;
        private Regex regex = new Regex(@"^\w+@\w+(.\w+)+$");
        Match match;

        public void log(string email, string password)
        {
            /*var loginAddress = "https://www.educanet2.ch/wws/9.php#/wws/100001.php?sid=45028881783075828648896009600980";
            var loginData = new NameValueCollection
            {
              { "login_login", email },
              { "login_password", password }
            };

            var client = new webLogin();
            client.Login(loginAddress, loginData);*/

            CookieCollection cookies = new CookieCollection();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.educanet2.ch/wws/9.php#/wws/100001.php?sid=45028881783075828648896009600980");
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(cookies);
            //Get the response from the server and save the cookies from the first request..
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            cookies = response.Cookies;
            string getUrl = "https://www.educanet2.ch/wws/9.php#/wws/100001.php?sid=47030315360573748948896049604590";
            string postData = String.Format("login_login={0}&login_password={1}", email, password);
            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(getUrl);
            getRequest.CookieContainer = new CookieContainer();
            getRequest.CookieContainer.Add(cookies); //recover cookies First request
            getRequest.Method = WebRequestMethods.Http.Post;
            getRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
            getRequest.AllowWriteStreamBuffering = true;
            getRequest.ProtocolVersion = HttpVersion.Version11;
            getRequest.AllowAutoRedirect = true;
            getRequest.ContentType = "application/x-www-form-urlencoded";

            byte[] byteArray = Encoding.ASCII.GetBytes(postData);
            getRequest.ContentLength = byteArray.Length;
            Stream newStream = getRequest.GetRequestStream(); //open connection
            newStream.Write(byteArray, 0, byteArray.Length); // Send the data.
            newStream.Close();

            HttpWebResponse getResponse = (HttpWebResponse)getRequest.GetResponse();
            using (StreamReader sr = new StreamReader(getResponse.GetResponseStream()))
            {
                string sourceCode = sr.ReadToEnd();
            }
            System.Diagnostics.Process.Start("https://www.educanet2.ch/wws/9.php#/wws/100001.php?sid=47030315360573748948896049604590");

        }

        public EducanetLogin(formSearch form)
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            email = txbEmail.Text;
            password = txbPassword.Text;
            match = regex.Match(email);

            if (match.Success)
            {
                if (password != "")
                {
                    log(email, password);
                }
                else
                {
                    MessageBox.Show("Veuillez remplir le mot de passe");
                }
            }
            else
            {
                MessageBox.Show("Adresse mail invalide");
            }
        }
    }
}
