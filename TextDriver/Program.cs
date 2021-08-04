using System;
using System.Threading;

namespace TextDriver
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ConsoleKeyInfo cki = new ConsoleKeyInfo();
            string line = null;
            do
            {
                while (Console.KeyAvailable == false) //await for input
                    Thread.Sleep(250);

                cki = Console.ReadKey(); //reading for every key
                line += cki.KeyChar; // saving every key pressed

                if (cki.Key == ConsoleKey.E) // end reading when 'E' pressed
                {
                    if (line.Contains(":E"))
                    {
                        Driver(line); // function for line check
                        line = null; //Nullification of the line for new text
                    }
                }
            } while (cki.Key != ConsoleKey.F2);// Exit on F2 key press
        }

        public static string PT(string line) // Function for reading text PT:text:E
        {
            if (line.Contains("PT:") && line.Contains(":E"))
            {
                int pFrom = line.IndexOf("PT:") + "PT:".Length; //get length from Function start (FT)
                int pTo = line.LastIndexOf(":E");//get last index for function (:E)

                return line.Substring(pFrom, pTo - pFrom);
            }
            return null;
        }

        public static string PS(string line)// Function for producing beep sound PS:Hz amount, millisecond amount:E
        {
            if (line.Contains("PS:") && line.Contains(":E"))
            {
                int pFrom = line.IndexOf("PS:") + "PS:".Length;
                int pTo = line.LastIndexOf(":E");

                string[] parameters = line.Substring(pFrom, pTo - pFrom).Split(',');
                int.TryParse(parameters[0], out int gerz);
                int.TryParse(parameters[0], out int milisec);
                if (gerz >= 37 && milisec > 0)
                {
                    Console.Beep(gerz, milisec);
                    return "sound of frequency " + parameters[0] + " Hz for " + parameters[1] + " ms is made";
                }
            }
            return null;
        }

        public static void Driver(string line)
        {
            string error = "NACK";
            string approve = "ACK";
            string result = null;

            if ((result = PT(line)) != null)
            {
                Console.WriteLine('\n' + approve + '\n' + result); ;
            }
            else if ((result = PS(line)) != null)
            {
                Console.WriteLine('\n' + approve + '\n' + result);
            }
            else
                Console.WriteLine('\n' + error);
        }
    }
}