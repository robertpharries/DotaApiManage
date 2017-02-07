using System;
using System.Collections.Generic;

namespace DotaApiManage
{
    class Program
    {
        static void Main()
        {
            MatchDetails.ApiAccess apia = new MatchDetails.ApiAccess("079");

            Console.WriteLine(apia.playerResult("48767249"));

            Console.ReadLine();
        }
    }
}
