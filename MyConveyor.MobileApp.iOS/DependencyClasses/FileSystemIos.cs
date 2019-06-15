using MyConveyor.MobileApp.DependencyInterfaces;
using MyConveyor.MobileApp.StaticClasses;
using System;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(MyConveyor.MobileApp.iOS.DependencyClasses.FileSystemiOS))]

namespace MyConveyor.MobileApp.iOS.DependencyClasses
{
    public class FileSystemiOS : IFileSystem
    {
        private string directoryName;

        public string DirectoryName
        {
            get
            {
                if (directoryName == null)
                {
                    directoryName = GetRootDir();
                }

                return directoryName;
            }
            set => directoryName = value;
        }

        public FileSystemiOS()
        {
        }

        /// -----------------------------------------------------------------------------------------------
        /// Name        AppendText
        /// 
        /// <summary>   Appends a file with the contents provided.
        /// </summary>
        /// <param name="filename">     The file name.</param>
        /// <param name="contents">     The content to append.</param>
        /// -----------------------------------------------------------------------------------------------
        public bool AppendText(string contents, string filename)
        {
            string path = Path.Combine(DirectoryName, filename);
            File.AppendAllText(path, contents);
            return true;
        }

        /// ------------------------------------------------------------------------------------------------
        /// Name		Exists
        /// 
        /// <summary>	Checks if a file exists.
        /// </summary>
        /// <param name="filename">			The path segments for the file.</param>
        /// ------------------------------------------------------------------------------------------------
        public bool Exists(string filename)
        {
            string filePath;

            if (!filename.Contains("/"))
            {
                filePath = Path.Combine(DirectoryName, filename);
            }
            else
            {
                filePath = filename;
            }

            bool result = File.Exists(filePath);
            return result;
        }

        /// ------------------------------------------------------------------------------------------------        
        /// 
        /// Name		GetRootDir
        /// 
        /// <summary> Gets the Root Directory.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------
        public string GetRootDir()
        {
            string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            DirectoryName = AppData.DirectoryPath = Path.Combine(documents, "MyConveyor");

            if (!Directory.Exists(DirectoryName))
            {
                Directory.CreateDirectory(DirectoryName);
            }

            return DirectoryName;
        }

        /// -----------------------------------------------------------------------------------------------
        /// Name        LoadText
        /// 
        /// <summary>   Reads a files content as string and returns the results.
        /// </summary>
        /// <param name="filename">     The filename.</param>
        /// -----------------------------------------------------------------------------------------------
        public string LoadText(string filename)
        {
            string text;
            string directorypath = string.Empty;

            if (!filename.Contains("/"))
            {
                directorypath = Path.Combine(DirectoryName, filename);
            }
            else
            {
                directorypath = filename;
            }

            using (FileStream fs = new FileStream(directorypath, FileMode.Open, FileAccess.Read))
            {
                StreamReader reader = new StreamReader(fs);
                text = reader.ReadToEnd();
            }

            return text;
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
            string directorypath = string.Empty;
            if (!filename.Contains("/"))
            {
                directorypath = Path.Combine(DirectoryName, filename);
            }
            else
            {
                directorypath = filename;
            }
            File.WriteAllText(directorypath, text);
        }

    }
}