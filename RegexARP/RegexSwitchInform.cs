using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace RegexARP
{
    delegate SortedDictionary<string, ConstrInformation> listIpResult();

    class RegexSwitchInform
    {
        static Regex regexIP = new Regex(@"((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)");
        static Regex regexMac = new Regex(@"([0-9a-fA-F]{4}).([0-9a-fA-F]{4}).([0-9a-fA-F]{4})");
        static Regex regexVlan = new Regex(@"([A-Z][a-zA-Z]{2,3}\d{1,3})");

        public SortedDictionary<string, ConstrInformation> dictionarySwitch =
            new SortedDictionary<string, ConstrInformation>();

        string text = String.Empty;

        public RegexSwitchInform(string text)
        {
            this.text = text;
        }

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
        /// записываем текст от пользователя в лист
        /// </summary>
        /// <returns></returns>
        List<string> UserList()
        {
            List<string> list = new List<string>();

            list = text.Split('\n').ToList();

            return list;
        }

        /// <summary>
        /// Создать с нужными данными сортированную директорию, с ключем в виде IP
        /// </summary>
       public void CreatList()
        {
            dictionarySwitch.Clear();
            foreach (string item in UserList())
            {
                string ip = Convert.ToString(regexIP.Match(item));
                string mac = Convert.ToString(regexMac.Match(item));
                string vlan = Convert.ToString(regexVlan.Match(item));

                if (ip != "" && mac != "" && vlan != "")
                {
                    ConstrInformation valueIP = new ConstrInformation(ip);
                    dictionarySwitch.Add($"{valueIP.SwitchStringIP}", new ConstrInformation(ip, mac, vlan));
                }
            }
        }

        /// <summary>
        /// Список свободных IP
        /// </summary>
        public SortedDictionary<string, ConstrInformation> FreeIP()
        {
            SortedDictionary<string, ConstrInformation> FreeIP = new SortedDictionary<string, ConstrInformation>();
            byte oktet = 0;

            foreach (var item in dictionarySwitch)
            {
                oktet = item.Value.SwitchListIP[2];
                if (item.Value.SwitchListIP[2] == oktet)
                {
                    GenericIPList list = new GenericIPList(item.Value.SwitchListIP[0],
                        item.Value.SwitchListIP[1], item.Value.SwitchListIP[2],
                        item.Value.SwitchVlan);

                    foreach (var ipList in list.generic)
                    {
                        if (!dictionarySwitch.ContainsKey(ipList.Key) && !FreeIP.ContainsKey(ipList.Key))
                        {
                            FreeIP.Add(ipList.Key, ipList.Value);
                        }
                    }
                }
                else
                {
                }
            }
            return FreeIP;
        }

        /// <summary>
        /// Список со всеми IP
        /// </summary>
        /// <returns></returns>
        public SortedDictionary<string, ConstrInformation> ResultDictionary()
        {
            foreach (var item in FreeIP())
            {
                dictionarySwitch.Add(item.Key, item.Value);
            }

            return dictionarySwitch;
        }
    }
}
