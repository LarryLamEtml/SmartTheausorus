using System;
using System.Collections.Generic;
namespace SmartThesaurus
{
    class WebPage
    {
        const String URL = @"http://etml.ch";
        static WebPage webSearch;

        private WebPage() { }

        public static WebPage getInstance()
        {
            if (webSearch == null)
                webSearch = new WebPage();
            return webSearch;
        }

        public List<string> getAllUrls()
        {
            SpiderLogic.SpiderLogic spiderUrl = new SpiderLogic.SpiderLogic();
            List<string> allUrl = new List<string>(spiderUrl.GetUrls(URL, true));
            List<string> correctUrl = new List<string>();

            foreach (string str in allUrl)
            {
                string newstr = str.Replace("http:/", "http://");
                correctUrl.Add(newstr);
            }

            return correctUrl;
        }

        public string searchOnWeb(string word, string url)
        {
            string webpageData;

            using (System.Net.WebClient webClient = new System.Net.WebClient())
                webpageData = webClient.DownloadString(url);

            string[] tableWords = webpageData.Split(' ');
            bool containsWord = webpageData.Contains(word);
            if (containsWord)
            {
                return url;
            }else
            {
                return "";
            }

        }

    }
}