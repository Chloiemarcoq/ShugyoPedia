using System;
using System.IO;

namespace ShugyopediaApp.Data
{
    /// <summary>
    /// Path Manager
    /// </summary>
    public class PathManager
    {
        /// <summary>
        /// Gets or sets the setup root directory.
        /// </summary>
        public static string SetupRootDirectory { get; set; }
        public static string StorageServerDirectory { get; set; }
        public static string StorageServerUrl { get; set; }

        /// <summary>
        /// Setups the specified setup root directory.
        /// </summary>
        /// <param name="setupRootDirectory">The setup root directory.</param>
        public static void Setup(string setupRootDirectory)
        {
            SetupRootDirectory = setupRootDirectory;
        }
        public static void Storage(string storageServerDirectory)
        {
            StorageServerDirectory = storageServerDirectory;
        }
        public static void StorageUrl(string storageServerUrl)
        {
            StorageServerUrl = storageServerUrl;
        }

        /// <summary>
        /// Directory Path
        /// </summary>
        public static class DirectoryPath
        {
            /// <summary>
            /// Log file storage directory path
            /// </summary>
            public static string LogDirectory
            {
                get { return GetFolderPath(SetupRootDirectory, "logs"); }
            }
            public static string AssetsStorageDirectory
            {
                get { return GetFolderPath(StorageServerDirectory, "assets"); }
            }
            public static string WWWRootStorageDirectory
            {
                get { return GetFolderPath(StorageServerDirectory, "wwwroot_common"); }
            }
            public static string TrainingImagesDirectory
            {
                get { return GetFolderPath(StorageServerDirectory, "training_images"); }
            }


            /// <summary>
            /// application log directory path
            /// </summary>
            /// <param name="appName">application name</param>
            /// <returns>directory path</returns>
            public static string ApplicationLogsDirectory(string appName)
            {
                return GetFolderPath(Path.Combine(LogDirectory, appName));
            }
        }
        public static class UrlPath
        {
            public static string TrainingImagesUrl
            {
                get { return StorageServerUrl + "training_images/"; }
            }
            public static string WWWRootCommonUrl
            {
                get { return StorageServerUrl + "wwwroot_common/"; }
            }
        }

            /// <summary>
            /// File Path
            /// </summary>
            public static class FilePath
        {
        }

        /// <summary>
        /// Gets the folder path and create the directory
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <returns>Directory path</returns>
        private static string GetFolderPath(string path, string folderName = "")
        {
            string result = Path.Combine(path, folderName);
            if (!Directory.Exists(result))
            {
                Directory.CreateDirectory(result);
            }
            return result;
        }
        private static string GetUrlPath(string path, string directoryName = "")
        {
            string result = Path.Combine(path, directoryName);
            return result;
        }
    }
}
