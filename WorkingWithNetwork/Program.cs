using System;
using System.Net;
using System.Net.NetworkInformation;

namespace WorkingWithNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a valid web address: ");
            string url = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(url))
            {
                url =
                    "https://www.google.com/search?sxsrf=ACYBGNSpNq1FQ8jQsdHrBlC9ltUIy4-dww%3A1580247831346&source=hp&ei=F6swXruQE4iZ_Qa7xpKQCA&q=mazefood&oq=mazefood&gs_l=psy-ab.3..35i39j0i10l8.2631.5039..5292...1.0..0.333.2149.2-1j6......0....1..gws-wiz.......0.fCWhfRPienY&ved=0ahUKEwj795H1oafnAhWITN8KHTujBIIQ4dUDCAY&uact=5";
            }

            var uri = new Uri(url);
            Console.WriteLine($"URL: {url}");
            Console.WriteLine($"Scheme: {uri.Scheme}");
            Console.WriteLine($"Port: {uri.Port}");
            Console.WriteLine($"Host: {uri.Host}");
            Console.WriteLine($"Path: {uri.AbsolutePath}");
            Console.WriteLine($"Query: {uri.Query}");

            var ip = Dns.GetHostEntry(uri.Host);
            foreach (IPAddress ipAddress in ip.AddressList)
            {
                Console.WriteLine(ipAddress);
            }


            try
            {
                var ping = new Ping();
                Console.WriteLine("Pinging server. Please wait...");
                var pingReply = ping.Send(uri.Host);
                Console.WriteLine($"{uri.Host} was pinged and replied: {pingReply.Status}.");

                if (pingReply.Status == IPStatus.Success)
                {
                    Console.WriteLine("Reply from {0} took {1:N0}ms", pingReply.Address, pingReply.RoundtripTime);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.GetType().ToString()} says {e.Message}");
            }
        }
    }
}