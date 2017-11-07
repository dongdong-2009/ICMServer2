using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICMServer
{
    // [TODO] 有點亂，要整理
    public class Path
    {
        /// <summary>
        /// 如果该目录不存在的話，試著建立目录
        /// </summary>
        /// <param name="folderPath">目录路徑</param>
        private static void CreateFolderIfNotExist(string folderPath)
        {
            bool exists = System.IO.Directory.Exists(folderPath);
            if (!exists)
                System.IO.Directory.CreateDirectory(folderPath);
        }

        /// <summary>
        /// 取得應用程式可以存取的資料目录路徑
        /// </summary>
        /// <returns>應用程式可以存取的資料目录路徑</returns>
        public static string GetAppDataFolderPath()
        {
            string folderPath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                              + @"\" + System.AppDomain.CurrentDomain.FriendlyName.Split('.').First();
            CreateFolderIfNotExist(folderPath);
            return folderPath;
        }

        /// <summary>
        /// 取得應用程式执行檔所在路徑
        /// </summary>
        /// <returns>取得應用程式执行檔所在路徑</returns>
        public static string GetAppExeFolderPath()
        {
            return System.IO.Path.GetDirectoryName(Application.ExecutablePath);
        }

        private static readonly string ADDRESSBOOOK_FOLDER = @"data\addressbook";
        private static readonly string PUBLISH_INFO_FOLDER = @"data\publish_informations";
        private static readonly string CARDLIST_BASE_FOLDER = @"data\cardlist";
        private static readonly string WEATHER_XML_FOLDER = @"data\weather";

        // data\addressbook\temp\addressbook.xml
        public static string GetAddressBookTempXmlFilePath()
        {
            return GetAddressBookTempFolderPath() + @"\addressbook.xml";
        }

        public static string GetAddressBookUclTempFolderPath()
        {
            string folderPath = GetAddressBookTempFolderPath() + @"\ucl";
            CreateFolderIfNotExist(folderPath);
            return folderPath;
        }

        // data\addressbook\ADDRESS.PKG
        public static string GetAddressBookPkgFilePath()
        {
            return GetAddressBookFolderPath() + @"\ADDRESS.PKG";
        }

        public static string GetAddressBookPkgFileRelativePath()
        {
            return GetAddressBookFolderRelativePath() + @"\ADDRESS.PKG"; 
        }

        public static string GetAddressBookTempFolderPath()
        {
            string folderPath = GetAddressBookFolderPath() + @"\temp";
            CreateFolderIfNotExist(folderPath);
            return folderPath;
        }

        public static string GetAddressBookFolderPath()
        {
            string folderPath = GetAppExeFolderPath() + @"\" + ADDRESSBOOOK_FOLDER;
            CreateFolderIfNotExist(folderPath);
            return folderPath;
        }

        public static string GetAddressBookFolderRelativePath()
        {
            return ADDRESSBOOOK_FOLDER;
        }

        public static string GetPublishInfoFolderPath()
        {
            string folderPath = GetAppExeFolderPath() + @"\" + PUBLISH_INFO_FOLDER;
            CreateFolderIfNotExist(folderPath);
            return folderPath;
        }

        public static string GetPublishInfoFolderRelativePath()
        {
            return PUBLISH_INFO_FOLDER;
        }

        public static string GetCardListTempFolderPath(string DeviceId)
        {
            string folderPath = GetCardListFolderPath(DeviceId) + @"\temp";
            CreateFolderIfNotExist(folderPath);
            return folderPath;
        }

        public static string GetCardListFolderPath(string DeviceId)
        {
            string folderPath = GetCardListBaseFolderPath() + @"\" + DeviceId;
            CreateFolderIfNotExist(folderPath);
            return folderPath;
        }

        public static string GetCardListFolderRelativePath(string DeviceId)
        {
            return GetCardListBaseFolderRelativePath() + @"\" + DeviceId;
        }

        public static string GetCardListBaseFolderPath()
        {
            string folderPath = GetAppExeFolderPath() + @"\" + CARDLIST_BASE_FOLDER;
            CreateFolderIfNotExist(folderPath);
            return folderPath;
        }

        public static string GetCardListBaseFolderRelativePath()
        {
            return CARDLIST_BASE_FOLDER;
        }

        public static string GetWeatherXmlFilePath()
        {
            string folderPath = GetAppExeFolderPath() + @"\" + WEATHER_XML_FOLDER;
            CreateFolderIfNotExist(folderPath);
            return folderPath + @"\weather.xml";
        }
    }
}
