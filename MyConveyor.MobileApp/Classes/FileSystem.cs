using MyConveyor.MobileApp.DependencyInterfaces;
using System;
using Xamarin.Forms;

namespace MyConveyor.MobileApp.Classes
{
    public class FileSystem : IFileSystem
    {
        public FileSystem()
        {

        }

        /// ------------------------------------------------------------------------------------------------
        /// Name		GetRootDir
        /// 
        /// <summary>	gets the root directory
        /// </summary>
        /// ------------------------------------------------------------------------------------------------
        public string GetRootDir()
        {
            string rootDir = DependencyService.Get<IFileSystem>().GetRootDir();
            return rootDir;
        }

        /// -----------------------------------------------------------------------------------------------
        /// Name        Exists
        /// 
        /// <summary>   Checks the file whether it exists in storage.
        /// </summary>
        /// <param name="filename">     The filename.</param>
        /// -----------------------------------------------------------------------------------------------
        public bool Exists(string filename)
        {
            bool result = false;

            try
            {
                result = DependencyService.Get<IFileSystem>().Exists(filename);
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.ToString());
            }

            return result;
        }

        /// -----------------------------------------------------------------------------------------------
        /// Name        LoadText
        /// 
        /// <summary>   Reads the text file content as a string.
        /// </summary>
        /// <param name="filename">     The filename.</param>
        /// -----------------------------------------------------------------------------------------------
        public string LoadText(string filename)
        {
            string result = null;
            try
            {
                result = DependencyService.Get<IFileSystem>().LoadText(filename);
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.ToString());
            }

            return result;
        }

        /// -----------------------------------------------------------------------------------------------
        /// Name        Save
        /// 
        /// <summary>   Saves text data to storage.
        /// </summary>
        /// <param name="filename">     The filename.</param>
        /// <param name="text">         The text data.</param>
        /// -----------------------------------------------------------------------------------------------
        public void Save(string filename, string text)
        {
            try
            {
                DependencyService.Get<IFileSystem>().Save(filename, text);
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.ToString());
            }
        }

        /// ------------------------------------------------------------------------------------------------
        /// Name		AppendText
        /// 
        /// <summary> writes the string to a file, or if file doesn't exist, creating a new file.
        /// </summary>
        /// <param name="contents">			The string to write.</param>
        /// <param name="filename">			The file path.</param>
        /// ------------------------------------------------------------------------------------------------
        public bool AppendText(string contents, string filename)
        {
            try
            {
                return DependencyService.Get<IFileSystem>().AppendText(contents, filename);
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.ToString());
                return false;
            }
        }
    }
}
