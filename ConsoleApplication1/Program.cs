using SkyTimer.Utils.Scramble;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
            var tnoodle = Process.Start("javaw.exe", @"-jar D:\Tools\TNoodle-WCA-0.11.1.jar -n");
            Console.ReadKey();
            //tnoodle.Kill();
            Process.GetProcessesByName("javaw")[0].Kill();
            Console.ReadKey();

        }
    }
}
