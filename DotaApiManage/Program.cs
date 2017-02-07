using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DotaApiManage
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main()
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri()
        }
    }
}
