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
        RegexSwitchInform regex;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            if (Convert.ToString(btbStart.Content) == "Start")
            {
                regex = new RegexSwitchInform(textInform.Text);
                regex.CreatList();
                textInform.Clear();
                ViewResult();
                btbStart.Content = "Reset";
            }
            else
            {
                textInform.Clear();
                btbStart.Content = "Start";
            }
        }

        private void ViewResult()
        {
           
            foreach (var item in chekIpAll.IsChecked == true ? regex.ResultDictionary() :
                regex.FreeIP())
            {
                textInform.Text += $"{item.Value.SwitchListIP[0]}.{item.Value.SwitchListIP[1]}." +
                    $"{item.Value.SwitchListIP[2]}." +
                    $"{item.Value.SwitchListIP[3]}" +
                    $"	{item.Value.SwitchMAC}	{item.Value.SwitchVlan}\n";
            }
        }

        private void chekIpAll_Checked(object sender, RoutedEventArgs e)
        {
            //if (chekIpAll.IsEnabled)
            //{
            //    textInform.Clear();
            //    foreach (var item in regex.ResultDictionary())
            //    {
            //        textInform.Text += $"{item.Value.SwitchListIP[0]}.{item.Value.SwitchListIP[1]}." +
            //            $"{item.Value.SwitchListIP[2]}." +
            //            $"{item.Value.SwitchListIP[3]}" +
            //            $"	{item.Value.SwitchMAC}	{item.Value.SwitchVlan}\n";
            //    }
            //}
            //else
            //{
            //    textInform.Clear();
            //    foreach (var item in regex.FreeIP())
            //    {
            //        textInform.Text += $"{item.Value.SwitchListIP[0]}.{item.Value.SwitchListIP[1]}." +
            //            $"{item.Value.SwitchListIP[2]}." +
            //            $"{item.Value.SwitchListIP[3]}" +
            //            $"	{item.Value.SwitchMAC}	{item.Value.SwitchVlan}\n";
            //    }
            //}
        }
    }
}
