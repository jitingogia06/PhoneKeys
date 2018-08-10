using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace GAS.TestCode
{
    public class PhoneKeys
    {
        /// <summary>
        /// Returns dictionary of phone keys
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetPhoneKeys()
        {
            var contentxml = new XmlDocument();
            contentxml.Load(@"../../PhoneKeys.xml");
            
            var data = XElement.Parse(contentxml.InnerXml)
            .Elements("key")
            .ToDictionary(
                el => (string)el.Attribute("number"),
                el => (string)el.Attribute("text")
            );
            
            return data;
        }

        /// <summary>
        /// Processes a dictionary of phone keys into individual characters and positions
        /// </summary>
        /// <param name="phoneKeys"></param>
        /// <returns></returns>

        public static Dictionary<char, int> Process(Dictionary<string, string> phoneKeys)
        {
            var processedKeys = new Dictionary<char, int>();
            char[] values;

            foreach (string key in phoneKeys.Keys)
            {
                values = phoneKeys[key].ToCharArray();

                for (int i = 0; i < values.Length; i++)
                {
                    processedKeys.Add(values[i], i);
                }
            }

            return processedKeys;
        }
    }
}
