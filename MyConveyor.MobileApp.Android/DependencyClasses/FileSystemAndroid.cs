using MyConveyor.MobileApp.Classes;
using MyConveyor.MobileApp.DependencyInterfaces;
using MyConveyor.MobileApp.StaticClasses;
using System;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(MyConveyor.MobileApp.Android.DependencyClasses.FileSystem))]

namespace MyConveyor.MobileApp.Android.DependencyClasses
{
    public class FileSystem : IFileSystem
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

        public FileSystem()
        {
            GetRootDir();
        }

        /// ------------------------------------------------------------------------------------------------
        /// Name		GetRootDir
        /// 
        /// <summary>	
        ///  Creates the directory of the file
        /// </summary>
        /// ------------------------------------------------------------------------------------------------
        public string GetRootDir()
        {
            try
            {
                Java.IO.File rootDir = new Java.IO.File(global::Android.OS.Environment.ExternalStorageDirectory + @"/MyConveyor");
                if (!rootDir.Exists())
                {
                    rootDir.Mkdirs();
                }

                AppData.DirectoryPath = rootDir.Path;
                return rootDir.Path;
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
                return string.Empty;
            }
        }

        /// ------------------------------------------------------------------------------------------------
        /// Name		Exists
        /// 
        /// <summary> checks if the file exists
        /// </summary>
        /// <param name="filename">the source filename</param>
        /// ------------------------------------------------------------------------------------------------
        public bool Exists(string filename)
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

            return File.Exists(directorypath);
        }

        /// ------------------------------------------------------------------------------------------------
        /// 
        /// Name		LoadText
        /// 
        /// <summary> LoadText from the required file 
        /// </summary>
        /// <param name="filename">the source filename</param>
        /// ------------------------------------------------------------------------------------------------
        public string LoadText(string filename)
        {
            return File.ReadAllText(Path.Combine(DirectoryName, filename));
        }

        /// ------------------------------------------------------------------------------------------------
        /// 
        /// Name		Save
        /// 
        /// <summary> save the contents to the required file 
        /// </summary>
        /// <param name="filename">the source filename</param>
        /// <param name="text">text contents</param>
        /// ------------------------------------------------------------------------------------------------
        public void Save(string filename, string text)
        {
            File.WriteAllText(Path.Combine(DirectoryName, filename), text);
        }

        /// ------------------------------------------------------------------------------------------------
        /// 
        /// Name		AppendText
        /// 
        /// <summary> Append the contents to the required file 
        /// </summary>
        /// <param name="contents">contents in string</param>
        /// <param name="filename">the source filename</param>
        /// ------------------------------------------------------------------------------------------------
        public bool AppendText(string contents, string filename)
        {
            string path = Path.Combine(DirectoryName, filename);
            if (File.Exists(path))
            {
                //this is working code for append text in android after fixing sharing violation issue
                using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(contents + System.Environment.NewLine);
                }

                return true;
            }

            return false;
        }

    }
}