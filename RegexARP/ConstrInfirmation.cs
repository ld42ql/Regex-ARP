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
    class ConstrInfirmation
    {
        private string switchIP;
        public string SwitchMAC, SwitchVlan;

        public List<byte> SwitchIP { get => ListIP(); }

        public ConstrInfirmation(string SwitchIP, string SwitchMAC, string SwitchVlan)
        {
            this.switchIP = SwitchIP;
            this.SwitchMAC = SwitchMAC;
            this.SwitchVlan = SwitchVlan;
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
    }
}
