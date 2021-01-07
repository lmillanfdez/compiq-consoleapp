using System;
using System.Configuration;
using System.Timers;

namespace ConsoleApp.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var timeoutTimeSpan = ConfigurationManager.AppSettings["executionTimeout"];
            var webAPIUrl = ConfigurationManager.AppSettings["apiURL"];

            var webApiClient = new WebAPIClient();
            webApiClient.WebAPIUrl = webAPIUrl;

            var timer = new Timer(Convert.ToInt32(timeoutTimeSpan));

            timer.Elapsed += webApiClient.PerformCalls;
            timer.Enabled = true;

            Console.Write("Press any key to stop the app from running...");
            Console.ReadLine();

            timer.Stop();
            timer.Dispose();
            webApiClient.Dispose();
        }
    }
}
