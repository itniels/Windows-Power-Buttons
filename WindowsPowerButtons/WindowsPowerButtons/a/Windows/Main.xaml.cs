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
            chb_ForcedActions.IsChecked = Properties.Settings.Default.Force;
            chb_Confirmation.IsChecked = Properties.Settings.Default.Confirmation;
            txb_WaitTime.Text = Properties.Settings.Default.WaitTime.ToString();
        }

        private bool ConfirmAction(string text)
        {
            if (chb_Confirmation.IsChecked.Value == true)
            {
                var result = MessageBox.Show("This will " + text + "\n\nAre you sure?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                return result == MessageBoxResult.Yes;
            }

            return true;
        }

        private void chb_ForcedActions_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Force = chb_ForcedActions.IsChecked.Value;
            Properties.Settings.Default.Save();
        }

        private void chb_Confirmation_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Confirmation = chb_Confirmation.IsChecked.Value;
            Properties.Settings.Default.Save();
        }

        private void chb_startWithWin_Click(object sender, RoutedEventArgs e)
        {
            if (chb_startWithWin.IsChecked.Value == true)
            {
                NovaKittySoftware.Wpf.StartupManager.CurrentUser.AddApplicationToStartup("Windows Power Buttons");
            }
            else
            {
                NovaKittySoftware.Wpf.StartupManager.CurrentUser.RemoveApplicationFromStartup("Windows Power Buttons");
            }
        }

        private int parseWaitTime()
        {
            try
            {
                int wait = Convert.ToInt32(txb_WaitTime.Text);
                return wait;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private void txb_WaitTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Prevent from inputting anything other than numbers
            if (!char.IsDigit(Char.Parse(e.Text)))
            {
                e.Handled = true;
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.WaitTime = parseWaitTime();
            Properties.Settings.Default.Save();
        }
    }
}
