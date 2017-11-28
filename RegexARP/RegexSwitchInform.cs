using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace RegexARP
{
    class RegexSwitchInform
    {
        static Regex regexIP = new Regex(@"((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)");
        static Regex regexMac = new Regex(@"([0-9a-fA-F]{4}).([0-9a-fA-F]{4}).([0-9a-fA-F]{4})");
        static Regex regexVlan = new Regex(@"([A-Z][a-zA-Z]{2,3}\d{1,3})");

        public List<ConstrInfirmation> listIP = new List<ConstrInfirmation>();

        /// <summary>
        /// Открываем шаблон и записываем его в лист
        /// </summary>
        /// <returns></returns>
        List<string> OpenShablon()
        {
            List<string> list = new List<string>();
            try
            {
                using (StreamReader inputFile = File.OpenText("text.txt"))
                {
                    string line = String.Empty;
                    while ((line = inputFile.ReadLine()) != null)
                    {
                        list.Add(line);
                    }
                }
            }
            catch (Exception)
            {
            }
            return list;
        }

        /// <summary>
        /// Создать лист с нужными данными
        /// </summary>
        public void CreatList()
        {
            foreach (string item in OpenShablon())
            {
                string ip = Convert.ToString(regexIP.Match(item));
                string mac = Convert.ToString(regexMac.Match(item));
                string vlan = Convert.ToString(regexVlan.Match(item));
                if (ip != "" && mac != "" && vlan != "")
                {
                    listIP.Add(new ConstrInfirmation(ip, mac, vlan));
                }
            }
        }

    }
}
