using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;

namespace GAS.TestCode
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputString;
            var phoneKeys = PhoneKeys.GetPhoneKeys();
            var processedPhoneKeys = PhoneKeys.Process(phoneKeys);

            Console.WriteLine("Enter text [a-z, *,#] or press / and enter key to end the program");
            inputString = Console.ReadLine();

            while (inputString != "/")
            {
                if (Validate(inputString))
                {
                    var minTime = GetMinInputTime(inputString, processedPhoneKeys);
                    Console.WriteLine("Minimum time required to input given text is {0} seconds", minTime);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Invalid characters. Please try again"); 
                }

                Console.WriteLine("Enter text [a-z, *,#] or / and enter key to end the program ");
                inputString = Console.ReadLine();
            }
        }

        /// <summary>
        /// Returns minimun imput time required for a given string
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="phoneKeys"></param>
        /// <returns></returns>
        private static decimal GetMinInputTime(string inputString, Dictionary<char, int> phoneKeys)
        {
            char[] inputChars =  inputString.ToCharArray();
            decimal minTime = 0;
            int pos;

            var keyPressTime = Convert.ToDecimal(ConfigurationManager.AppSettings["KeyPressTime"]);
            var keyPauseTime = Convert.ToDecimal(ConfigurationManager.AppSettings["KeyPauseTime"]);

            foreach (char c in inputChars)
            {
                if (phoneKeys.ContainsKey(char.ToLower(c)))
                {
                    pos = phoneKeys[char.ToLower(c)];
                    minTime = minTime + (pos * keyPressTime) + keyPauseTime;
                } 
            }

            return minTime;
        }

        private static bool Validate(string text)
        {
            var r = new Regex("^[a-zA-Z0-9*#]*$");

            return (text.Length > 0 && r.IsMatch(text));
        }

        
    }
}
