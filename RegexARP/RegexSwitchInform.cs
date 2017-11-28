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

        List<ConstrInfirmation> list = new List<ConstrInfirmation>();
        public SortedDictionary<string, ConstrInfirmation> dictionarySwitch =
            new SortedDictionary<string, ConstrInfirmation>();

       public SortedDictionary<string, ConstrInfirmation> dictionary = new SortedDictionary<string, ConstrInfirmation>();


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
       public List<string> UserList(string text)
        {
            List<string> list = new List<string>();

            list = text.Split('\n').ToList();
           
            return list;
        }

        /// <summary>
        /// Создать лист с нужными данными
        /// </summary>
        public void CreatList(List<string> listtext)
        {
            foreach (string item in listtext)
            {
                string ip = Convert.ToString(regexIP.Match(item));
                string mac = Convert.ToString(regexMac.Match(item));
                string vlan = Convert.ToString(regexVlan.Match(item));
                
                if (ip != "" && mac != "" && vlan != "")
                {
                    list.Add(new ConstrInfirmation(ip, mac, vlan));
                }
            }
        }

        /// <summary>
        /// Пересобираем лист в сортированную директорию, с ключем в виде IP
        /// </summary>
        public void SortDirection()
        {
            string ip = String.Empty;
            foreach (var item in list)
            {
                if (item.SwitchIP[3] >= 100)
                {
                    ip = $"{item.SwitchIP[0]}.{item.SwitchIP[1]}.{item.SwitchIP[2]}.{item.SwitchIP[3]}";
                }
                else
                {
                    ip = item.SwitchIP[3] >= 10 ? $"{item.SwitchIP[0]}.{item.SwitchIP[1]}." +
                        $"{item.SwitchIP[2]}.0{item.SwitchIP[3]}" : $"{item.SwitchIP[0]}." +
                        $"{item.SwitchIP[1]}.{item.SwitchIP[2]}.00{item.SwitchIP[3]}";
                }

                dictionarySwitch.Add($"{TryIp(item.SwitchIP[0], item.SwitchIP[1], item.SwitchIP[2], item.SwitchIP[3])}",
                    new ConstrInfirmation(ip, item.SwitchMAC, item.SwitchVlan));
            }
        }

        /// <summary>
        /// Заполняем свободными IP
        /// </summary>
        /// <returns></returns>
        public void CenericIP()
        {
            byte i = 1;

            foreach (var item in dictionarySwitch)
            {
               while (item.Value.SwitchIP[3] != i)
                {
                    string ip = $"{item.Value.SwitchIP[0]}.{item.Value.SwitchIP[1]}." +
                        $"{item.Value.SwitchIP[2]}.{i}";
                    try
                    {

                    
                    dictionary.Add($"{TryIp(item.Value.SwitchIP[0], item.Value.SwitchIP[1],item.Value.SwitchIP[2], i)}"
                        , new ConstrInfirmation(ip, "-----------------", item.Value.SwitchVlan));
                    }
                    catch (Exception)
                    {
                    }
                    i++;
                }
                i++;

                if (i > 254)
                {
                    i = 1;
                }
            }
        }

        public SortedDictionary<string, ConstrInfirmation> ResultDictionary()
        {
            foreach (var item in dictionary)
            {
                try
                {

              
                dictionarySwitch.Add(item.Key, item.Value);
                }
                catch (Exception)
                {
                }
            }
            return dictionarySwitch;
        }


        /// <summary>
        /// Пересобираем IP в вид: х.х.х.00х
        /// </summary>
        /// <param name="value_a">первый октет</param>
        /// <param name="value_b">второй октет</param>
        /// <param name="value_c">третий октет</param>
        /// <param name="value_d">четвертый октет</param>
        /// <returns></returns>
        string TryIp(int value_a, int value_b, int value_c, int value_d)
        {
            if (value_d >= 100)
            {
                return $"{value_a}.{value_b}.{value_c}.{value_d}";
            }
            else
            {
                return value_d >= 10 ? $"{value_a}.{value_b}." +
                    $"{value_c}.0{value_d}" : $"{value_a}." +
                    $"{value_b}.{value_c}.00{value_d}";
            }
        }
    }
}
