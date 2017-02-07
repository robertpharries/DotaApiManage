using System;
using System.Collections.Generic;

namespace DotaApiManage
{
    class Program
    {
        static void Main()
        {
            ApiAccess apia = new ApiAccess("48767249", "25");

            List<string> matchids = apia.GetMatchIds();
            foreach(var id in matchids)
            {
                Console.WriteLine(id);
            }

            Console.ReadLine();
        }
    }
}
