using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexARP
{
    class GenericIPList
    {
        int firstOktet;
        int secondIktet;
        int threeIP;
        string vlan = String.Empty;

        /// <summary>
        /// Коллекция со всеми ip по заданным ранее параметрам
        /// </summary>
        public SortedDictionary<string, ConstrInformation> generic = 
            new SortedDictionary<string, ConstrInformation>();

        /// <summary>
        /// Генерирует список со всеми ip
        /// </summary>
        public GenericIPList(int firstOktet, int secondIktet, int threeIP, string vlan)
        {
            this.firstOktet = firstOktet;
            this.secondIktet = secondIktet;
            this.threeIP = threeIP;
            this.vlan = vlan;
            GenericIP();
        }

        void GenericIP()
        {
            ConstrInformation con;

            for (byte b = 1; b < 255; b++)
            {
                string ip = $"{firstOktet}.{secondIktet}.{threeIP}.{b}";
                con = new ConstrInformation(ip);
                generic.Add(con.SwitchStringIP, new ConstrInformation(ip, "-----------------", vlan));
            }
        }
    }
}
