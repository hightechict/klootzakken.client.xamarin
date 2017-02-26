using System;
using HtmlAgilityPack;
using System.Net;
using AngleSharp.Parser.Html;
using System.Linq;
using Android.OS;

namespace KlootzakkenClient
{
    public class RequestVerificationTokenGetter
    {
        private readonly string klootzakkenUrl = "http://www.glueware.nl/Klootzakken/kz/Account/Login?returnurl=%2FKlootzakken%2Fkz%2FToken";

        private WebClient webClient = new WebClient();
        private HtmlParser htmlParser = new HtmlParser();

        public string Get()
        {
            var tokenValue = "";
            var document = htmlParser.Parse(webClient.DownloadString(klootzakkenUrl));
            var inputs = document.QuerySelectorAll("input");

            foreach (var i in inputs)
            {
                try
                {
                    var value = i.Attributes["name"].Value;
                    if (value.Equals("__RequestVerificationToken"))
                    {
                        tokenValue = i.Attributes["value"].Value;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return tokenValue;
        }
    }
}
