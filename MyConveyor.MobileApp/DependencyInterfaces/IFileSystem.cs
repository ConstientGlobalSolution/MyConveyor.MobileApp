namespace MyConveyor.MobileApp.DependencyInterfaces
{
    public interface IFileSystem
    {
        /// -----------------------------------------------------------------------------------------------
        /// Name        AppendText
        /// 
        /// <summary>   Appends a file with the contents provided.
        /// </summary>
        /// <param name="contents">     The content to append.</param>
        /// <param name="filename">     The file name.</param>
        /// -----------------------------------------------------------------------------------------------
        bool AppendText(string contents, string filename);

        /// -----------------------------------------------------------------------------------------------
        /// Name        Exists
        /// 
        /// <summary>   Checks to see if the file exists in storage.
        /// </summary>
        /// <param name="filename">     The filename.</param>
        /// -----------------------------------------------------------------------------------------------
        bool Exists(string filename);

        /// -----------------------------------------------------------------------------------------------
        /// Name        GetRootDir
        /// 
        /// <summary>   Gets the root directory for the app.
        /// </summary>
        /// -----------------------------------------------------------------------------------------------
        string GetRootDir();

        /// -----------------------------------------------------------------------------------------------
        /// Name        LoadText
        /// 
        /// <summary>   Reads a files content as string and returns the results.
        /// </summary>
        /// <param name="filename">     The filename.</param>
        /// -----------------------------------------------------------------------------------------------
        /// 
        string LoadText(string filename);

        /// -----------------------------------------------------------------------------------------------
        /// Name        Save
        /// 
        /// <summary>   Saves text data to storage.
        /// </summary>
        /// <param name="filename">     The filename.</param>
        /// <param name="text">         The text data.</param>
        /// -----------------------------------------------------------------------------------------------
        void Save(string filename, string text);
    }
}
