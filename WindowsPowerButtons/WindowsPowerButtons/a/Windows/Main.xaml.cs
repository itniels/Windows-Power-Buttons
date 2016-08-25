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
        private bool isExit = false;

        public Main()
        {
            InitializeComponent();
        }

        #region Window

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
            chb_StartMinimized.IsChecked = Properties.Settings.Default.StartMinimized;

            // Hide window on startup if chosen.
            if (Properties.Settings.Default.StartMinimized)
            {
                this.Hide();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isExit)
            {
                Properties.Settings.Default.WaitTime = ParseWaitTime();
                Properties.Settings.Default.Save();
                Application.Current.Shutdown();
            }
            else
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        #endregion

        #region CheckBoxes

        private bool ConfirmAction(string text)
        {
            if (chb_Confirmation.IsChecked.Value == true)
            {
                var result = MessageBox.Show("This will " + text + " the system!\n\nAre you sure?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                return result == MessageBoxResult.Yes;
            }

            return true;
        }

        private void chb_ForcedActions_Click(object sender, RoutedEventArgs e)
        {
            if (chb_ForcedActions.IsChecked == null) return;
            Properties.Settings.Default.Force = chb_ForcedActions.IsChecked.Value;
            Properties.Settings.Default.Save();
        }

        private void chb_Confirmation_Click(object sender, RoutedEventArgs e)
        {
            if (chb_Confirmation.IsChecked == null) return;
            Properties.Settings.Default.Confirmation = chb_Confirmation.IsChecked.Value;
            Properties.Settings.Default.Save();
        }

        private void chb_startWithWin_Click(object sender, RoutedEventArgs e)
        {
            if (chb_startWithWin.IsChecked != null && chb_startWithWin.IsChecked.Value == true)
            {
                NovaKittySoftware.Wpf.StartupManager.CurrentUser.AddApplicationToStartup("Windows Power Buttons");
            }
            else
            {
                NovaKittySoftware.Wpf.StartupManager.CurrentUser.RemoveApplicationFromStartup("Windows Power Buttons");
            }
        }

        private void chb_StartMinimized_Click(object sender, RoutedEventArgs e)
        {
            if (chb_StartMinimized.IsChecked == null) return;
            Properties.Settings.Default.StartMinimized = chb_StartMinimized.IsChecked.Value;
            Properties.Settings.Default.Save();
        }

        #endregion

        #region Helper Methods

        private int ParseWaitTime()
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

        #endregion

        #region Power Buttons

        private void btn_Shutdown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ConfirmAction("Shutdown"))
                    a.Logic.Power.Shutdown(Properties.Settings.Default.Force, ParseWaitTime());
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to shutdown!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_Restart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ConfirmAction("Restart"))
                    a.Logic.Power.Restart(Properties.Settings.Default.Force, ParseWaitTime());
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to reboot!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                a.Logic.Power.Cancel();
                MessageBox.Show("Pending action has been canceled.", "Cancled", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to cancel!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region NotifyMenu Items

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            isExit = true;
            Application.Current.Shutdown();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Show();
        }

        #endregion

        #region Texbox Methods

        private void txb_WaitTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default.WaitTime = ParseWaitTime();
            Properties.Settings.Default.Save();
        }

        private void txb_WaitTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Prevent from inputting anything other than numbers
            if (!char.IsDigit(Char.Parse(e.Text)))
            {
                e.Handled = true;
            }

        }

        #endregion
    }
}
