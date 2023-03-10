using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FileSizeTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            //Destination to the file we are retreiving information from.
            string url = "https://cdn.cloudflare.steamstatic.com/client/installer/SteamSetup.exe";

            //Transfers the information from 'string url' to GetFileSizeAsync
            GetFileSizeAsync(url).Wait();
        }
        //This method is used to retrieve the file size from the URL using the 'Content-Length' header
        static async Task GetFileSizeAsync(string url)
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, url)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.Content.Headers.TryGetValues("Content-Length", out var values))
                        {
                            long filesize = long.Parse(values.First());
                            Console.WriteLine($"The file size is {filesize} bytes");
                        }
                        else
                        {
                            Console.WriteLine("Cannot retrieve file size. Content-Length Header is missing!");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error retreiving the file size: {response.StatusCode}");
                    }
                }
            }
        }
    }
}