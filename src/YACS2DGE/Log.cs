using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace YACS2DGE.YACS2DGE
{
    public class Log
    {
        public static void Info(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[Info] {msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Error(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[Error] {msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Warn(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[Warning] {msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}