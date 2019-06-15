using MyConveyor.MobileApp.StaticClasses;
using System;
using System.Diagnostics;
using System.Text;

namespace MyConveyor.MobileApp.Classes
{
    public static class LogTracking
    {
        public static void LogTrace(string logText)
        {
            try
            {
                if (AppData.LogDetails == null)
                {
                    AppData.LogDetails = new StringBuilder();
                }

                AppData.LogDetails.AppendLine();
                AppData.LogDetails.AppendLine(logText);
                Debug.WriteLine(logText);
            }
            catch (Exception ex)
            {
                LogTrace(ex.ToString());
            }
        }

        public static void EntryToLogFileAsync()
        {
            try
            {
                if (AppData.LogDetails != null)
                {
                    AppData.FileAccess.AppendText(AppData.LogDetails.ToString(), "log.txt");
                }

            }
            catch (Exception ex)
            {
                LogTrace(ex.ToString());
            }
        }
    }
}
