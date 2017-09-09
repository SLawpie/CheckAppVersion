using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckAppVersion
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckVersion.CheckVersion checkVersion = new CheckVersion.CheckVersion();

            if (checkVersion.NewVersionCheck())
            {
                Console.WriteLine("NEW version avaiable !");
            }
            else
            {
                Console.WriteLine("NO new version !");
                Console.WriteLine(checkVersion.ErrorMessage);
            }
        }
    }
}
