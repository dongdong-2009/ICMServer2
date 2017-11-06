using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Markup;

namespace ICMServer.WPF
{
    public class CulturesHelper
    {
        private static bool _isFoundInstalledCultures = false;
        private const string _resourcePrefix = "StringResources";
        private const string _culturesFolder = "Cultures";

        #region SupportedCultures
        private static List<CultureInfo> _supportedCultures = new List<CultureInfo>();
        public static List<CultureInfo> SupportedCultures
        {
            get { return _supportedCultures; }
        }
        #endregion

        public CulturesHelper()
        {
            if (!_isFoundInstalledCultures)
            {
                CultureInfo cultureInfo = new CultureInfo("");
                string currentFolder = (ViewModelBase.IsInDesignModeStatic)
                                     //? @"D:\Doorbell_ControlServer\icmserver_sdk_v1.1\ICMServer2\Wpf"
                                     ? @"Wpf"
                                     : System.Windows.Forms.Application.StartupPath;

                List<string> files = Directory.GetFiles(string.Format("{0}\\{1}", currentFolder, _culturesFolder))
                    .Where(s => s.Contains(_resourcePrefix) && s.ToLower().EndsWith("xaml")).ToList();

                // 產生支援的語系列表
                foreach (string file in files)
                {
                    try
                    {
                        string filename = System.IO.Path.GetFileName(file);
                        string cultureName = filename.Substring(filename.IndexOf(".") + 1).Replace(".xaml", "");
                        cultureInfo = CultureInfo.GetCultureInfo(cultureName);
                        if (cultureInfo != null)
                        {
                            _supportedCultures.Add(cultureInfo);
                        }
                    }
                    catch (ArgumentNullException) { }
                    catch (CultureNotFoundException) { }
                }
                //if (_supportedCultures.Count > 0 && Properties.Settings.Default.DefaultCulture != null)
                if (_supportedCultures.Count > 0)
                {
                    //ChangeCulture(Properties.Settings.Default.DefaultCulture);
                    var culture = (from c in SupportedCultures
                                   where c.Name == Config.Instance.AppLanaguage
                                   select c).FirstOrDefault();
                    CurrentCulture = (culture != null) ? culture : SupportedCultures[0];
                }
                _isFoundInstalledCultures = true;
            }
        }

        private static CultureInfo _currentCulture = null;
        public static CultureInfo CurrentCulture
        {
            get { return _currentCulture ?? System.Threading.Thread.CurrentThread.CurrentUICulture; }
            set
            {
                if (value == null)
                    return;

                if (_currentCulture == null || _currentCulture.Name != value.Name)
                {
                    _currentCulture = value;
                    ChangeCulture(_currentCulture);
                }
            }
        }

        private static void ChangeCulture(CultureInfo culture)
        {
            if (_supportedCultures.Contains(culture))
            {
                string CurrentFolder = (ViewModelBase.IsInDesignModeStatic)
                                     // @"D:\Doorbell_ControlServer\icmserver_sdk_v1.1\ICMServer2\Wpf"
                                     ? @"Wpf"
                                     : System.Windows.Forms.Application.StartupPath;
                string LoadedFileName = string.Format("{0}\\{1}\\{2}.{3}.xaml", CurrentFolder, _culturesFolder, 
                    _resourcePrefix, culture.Name);
                FileStream fileStream = new FileStream(@LoadedFileName, FileMode.Open);
                ResourceDictionary resourceDictionary = XamlReader.Load(fileStream) as ResourceDictionary;
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
                if (Application.Current.MainWindow != null)
                    Application.Current.MainWindow.Resources.MergedDictionaries.Add(resourceDictionary);
                Properties.Settings.Default.DefaultCulture = culture;
                Properties.Settings.Default.Save();

                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
                Config.Instance.AppLanaguage = culture.Name;
            }
        }

        /// <summary>
        /// Gets the localised text value for the given key.
        /// </summary>
        /// <param name="key">The key of the localised text to retrieve.</param>
        /// <returns>The localised text if found, otherwise an empty string.</returns>
        public static string GetTextValue(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return "";

            string value = System.Windows.Application.Current.TryFindResource(key) as string;
            return value ?? "";
        }
    }
}
