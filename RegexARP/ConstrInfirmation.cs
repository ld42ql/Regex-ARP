using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexARP
{
    /// <summary>
    /// Класс для хранения данных о коммутаторах
    /// </summary>
    class ConstrInformation
    {
        private string switchIP;
        private string switchMAC;
        private string switchVlan;

        /// <summary>
        /// IP в виде массива
        /// </summary>
        public List<byte> SwitchListIP { get => ListIP(); }

        /// <summary>
        /// IP в виде строки
        /// </summary>
        public string SwitchStringIP { get => TryIp(); }

        public string SwitchMAC { get => switchMAC; }
        public string SwitchVlan { get => switchVlan; }

        public ConstrInformation(string SwitchIP)
        {
            this.switchIP = SwitchIP;
        }

        public ConstrInformation(string SwitchIP, string SwitchMAC, string SwitchVlan)
        {
            this.switchIP = SwitchIP;
            this.switchMAC = SwitchMAC;
            this.switchVlan = SwitchVlan;
        }

        /// <summary>
        /// Преобразовать IP из строки в лист
        /// </summary>
        /// <returns></returns>
        List<byte> ListIP()
        {
            List<byte> listiP = new List<byte>(4);
            string[] array = switchIP.Split('.');
            for (byte i = 0; i < 4; i++)
            {
                listiP.Add(Convert.ToByte(array[i]));
            }
            return listiP;
        }

        /// <summary>
        /// Пересобираем IP в вид: 00х.00х.00х.00х
        /// </summary>
        /// <returns></returns>
        string TryIp()
        {
            string IP = String.Empty;
            foreach (var item in ListIP())
            {
                IP += Modification(item) + ".";
            }
            return IP;
        }

        /// <summary>
        /// Записываем число в трехзначном виде
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Modification(int value)
        {
            if (value >= 100)
            {
                return $"{value}";
            }
            else
            {
                return value >= 10 ? $"0{value}" : $"00{value}";
            }
        }
    }
}
