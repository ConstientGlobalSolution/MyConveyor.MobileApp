using System;
using System.IO;
using System.Text;

namespace MyConveyor.MobileApp.Classes
{
    public class ErrorReport
    {
        /// ------------------------------------------------------------------------------------------------
        /// Name        ErrorReport
        /// 
        /// <summary>   Creates a new instance of the ErrorReport class instantiated from an 
        ///             unmanaged native exception.
        /// </summary>
        /// <param name="title">        The title for the report.</param>
        /// <param name="ex">           The exception details</param>
        /// ------------------------------------------------------------------------------------------------
        public ErrorReport(string title, IOException ex)
        {
            StringBuilder sb;
            sb = new StringBuilder();
            sb.AppendLine(title);
            sb.AppendLine();
            sb.AppendLine("Exception:");
            sb.AppendLine(ex.GetType().ToString());
            sb.AppendLine(ex.Message);
            sb.AppendLine();
            sb.AppendLine("Stack Trace:");
            if (ex.StackTrace != null)
            {
                sb.AppendLine(ex.StackTrace);
            }

            Serialised = sb.ToString();
        }

        /// ------------------------------------------------------------------------------------------------
        /// Name        ErrorReport
        /// 
        /// <summary>   Creates a new instance of the ErrorReport class instantiated from a 
        ///             managed exception.
        /// </summary>
        /// <param name="title">        The title for the report.</param>
        /// <param name="ex">           The exception details</param>
        /// ------------------------------------------------------------------------------------------------
        public ErrorReport(string title, Exception ex)
        {
            StringBuilder sb;
            sb = new StringBuilder();
            sb.AppendLine(title);
            sb.AppendLine();
            sb.AppendLine("Exception");
            sb.AppendLine(ex.GetType().ToString());
            sb.AppendLine(ex.Message);
            sb.AppendLine();
            sb.AppendLine("Stack Trace:");
            sb.AppendLine(ex.StackTrace);
            Serialised = sb.ToString();
        }

        /// ------------------------------------------------------------------------------------------------
        /// Name        ErrorReport
        /// 
        /// <summary>   Creates a new instance of the ErrorReport class instantiated from an
        ///             unrecognised exception.
        /// </summary>
        /// <param name="title">        The title for the report.</param>
        /// <param name="contents">     The string contents available for identifying the error.</param>
        /// ------------------------------------------------------------------------------------------------
        public ErrorReport(string title, string contents)
        {
            StringBuilder sb;
            sb = new StringBuilder();
            sb.AppendLine(title);
            sb.AppendLine();
            sb.AppendLine("Exception:");
            sb.AppendLine(contents);
            Serialised = sb.ToString();
        }

        /// ------------------------------------------------------------------------------------------------
        /// Name        AddFurtherInfo
        /// 
        /// <summary>   Append the Text into existing string 
        /// </summary>
        /// <param name="text"> The text to be append</param>
        /// ------------------------------------------------------------------------------------------------
        public void AddFurtherInfo(string text)
        {
            StringBuilder sb;
            sb = new StringBuilder(Serialised);
            sb.AppendLine();
            sb.Append(text);
            Serialised = sb.ToString();
        }

        public string Serialised { get; private set; }
    }
}
