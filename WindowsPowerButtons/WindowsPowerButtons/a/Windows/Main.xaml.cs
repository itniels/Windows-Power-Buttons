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
using System.Windows.Shapes;

namespace WindowsPowerButtons.a.Windows
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                chb_startWithWin.IsChecked = NovaKittySoftware.Wpf.StartupManager.CurrentUser.IsStartup(a.Assets.Vars.AppName);
            }
            catch (Exception)
            {
                chb_startWithWin.Foreground = new SolidColorBrush(Colors.Red);
            }
        }
    }
}
