using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        private const string _mainUrl = "http://localhost:5000/";
        private const string _urlToCreatePin = _mainUrl + "pin/create/";

        //This is test main app for testing codes from the klootzakken xamarin app
        static void Main(string[] args)
        {
            Console.ReadKey();
        }
    }
}
