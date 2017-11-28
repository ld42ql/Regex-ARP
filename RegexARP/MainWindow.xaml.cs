using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RegexARP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RegexSwitchInform regex = new RegexSwitchInform();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            regex.CreatList();
            regex.CenericIP();
            foreach (var item in regex.listIP)
            {
                textInform.Text += $"{item.SwitchIP[0]}.{item.SwitchIP[1]}.{item.SwitchIP[2]}.{item.SwitchIP[3]}" +
                    $"	{item.SwitchMAC}	{item.SwitchVlan}\n";
            }
        }
    }
}
