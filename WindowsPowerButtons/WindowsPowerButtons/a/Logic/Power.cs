using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPowerButtons.a.Logic
{
    public static class Power
    {
        /// <summary>
        /// Method to shutdown the computer.
        /// </summary>
        /// <param name="isForced"></param>
        /// <param name="waitTime"></param>
        /// <remarks>
        /// /f = Forced
        /// /s = Shutdown
        /// /t = Time to wait
        /// </remarks>
        public static void Shutdown(bool isForced, int waitTime)
        {
            string strCmdText;
            if (isForced)
            {
                strCmdText = "/f /s /t " + waitTime;
            }
            else
            {
                strCmdText = "/s /t " + waitTime;
            }
            System.Diagnostics.Process.Start("shutdown", strCmdText);
        }

        /// <summary>
        /// Method to restart the computer.
        /// </summary>
        /// <param name="isForced"></param>
        /// <param name="waitTime"></param>
        /// <remarks>
        /// /f = Forced
        /// /r = Restart
        /// /t = Time to wait
        /// </remarks>
        public static void Restart(bool isForced, int waitTime)
        {
            string strCmdText;
            if (isForced)
            {
                strCmdText = "/f /r /t " + waitTime;
            }
            else
            {
                strCmdText = "/r /t " + waitTime;
            }
            System.Diagnostics.Process.Start("shutdown", strCmdText);
        }

        /// <summary>
        /// Cancels any pending shutdowns or restarts
        /// </summary>
        /// <param name="isForced"></param>
        /// <param name="waitTime"></param>
        /// <remarks>
        /// /C = CMD somehing?? (i forgot)
        /// /a = Cancel any pending Shutdowns/restarts
        /// </remarks>
        public static void Cancel()
        {
            string strCmdText = "/C shutdown /a";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        }
    }
}
