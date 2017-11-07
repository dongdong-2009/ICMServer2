using System;
using System.IO;
using System.Windows.Forms;
using Utilities;

namespace ICMServer
{
    public class Config
    {
        private static volatile Config m_Instance;
        private static object m_SyncRoot = new Object();

        private const string SECTION_SIPSERVER      = "SIPSERVER";
        private const string SECTION_GENERAL        = "SECTION_CONFIG";
        private const string SECTION_DATABASE       = "DATABASE";
        private const string SECTION_ADDRESSBOOK    = "AddrBook";
        private const string SECTION_GROUP_IP       = "GROUPIP";
        private const string SECTION_WEATHER        = "WEATHER";
        private const string SECTION_ADVERTISEMENT  = "ADVERTISEMENT";
        private const string SECTION_PORT           = "PORT";
        private const string m_FileName             = "mm.cfg";
        private IniFile m_IniFile;

        private Config()
        {
            string filePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\" + m_FileName;
            m_IniFile = new IniFile(filePath);
        }

        public static Config Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    lock (m_SyncRoot)
                    {
                        if (m_Instance == null)
                            m_Instance = new Config();
                    }
                }

                return m_Instance;
            }
        }

        public string FTPServerRootDir
        {
            get
            {
                string path = m_IniFile.GetString(SECTION_GENERAL, "FTPDIR", System.IO.Path.GetDirectoryName(Application.ExecutablePath));
                if (!Directory.Exists(path))
                    path = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                return path;
            }
            set { m_IniFile.WriteValue(SECTION_GENERAL, "FTPDIR", value); }
        }

        // TODO 資料库相關栏位必須加密
        public string DatabaseName
        {
            get { return m_IniFile.GetString(SECTION_DATABASE, "INITIALCATALOG", "icmdb"); }
            set { m_IniFile.WriteValue(SECTION_DATABASE, "INITIALCATALOG", value); }
        }

        public string DatabaseUser
        {
            get { return m_IniFile.GetString(SECTION_DATABASE, "LOGINNAME", "root"); }
            set { m_IniFile.WriteValue(SECTION_DATABASE, "LOGINNAME", value); }
        }

        public string DatabasePassword
        {
            get { return m_IniFile.GetString(SECTION_DATABASE, "LOGINPWD", ""); }
            set { m_IniFile.WriteValue(SECTION_DATABASE, "LOGINPWD", value); }
        }

        public string LocalIP
        {
            get { return m_IniFile.GetString(SECTION_ADDRESSBOOK, "INDEX_AB_SERVER_IP", "127.0.0.1"); }
            set { m_IniFile.WriteValue(SECTION_ADDRESSBOOK, "INDEX_AB_SERVER_IP", value); }
        }
        public string LocalGateway
        {
            get { return m_IniFile.GetString(SECTION_ADDRESSBOOK, "INDEX_AB_GATEWAY", "0.0.0.0"); }
            set { m_IniFile.WriteValue(SECTION_ADDRESSBOOK, "INDEX_AB_GATEWAY", value); }
        }

        public string LocalSubnetMask
        {
            get { return m_IniFile.GetString(SECTION_ADDRESSBOOK, "INDEX_AB_SUBMASK", "255.255.255.0"); }
            set { m_IniFile.WriteValue(SECTION_ADDRESSBOOK, "INDEX_AB_SUBMASK", value); }
        }

        public string OutboundIP
        {
            get
            {
                const string defaultValue = "0.0.0.0";
                string value = m_IniFile.GetString(SECTION_ADDRESSBOOK, "INDEX_AB_OUTBOUND_IP", defaultValue);
                if (value == defaultValue)
                    return LocalIP;
                return value;
            }
            set { m_IniFile.WriteValue(SECTION_ADDRESSBOOK, "INDEX_AB_OUTBOUND_IP", value); }
        }

        public string AppName
        {
            get { return m_IniFile.GetString(SECTION_ADDRESSBOOK, "INDEX_AB_SERVER_NAME", System.Diagnostics.Process.GetCurrentProcess().ProcessName); }
            set { m_IniFile.WriteValue(SECTION_ADDRESSBOOK, "INDEX_AB_SERVER_NAME", value); }
        }

        public string AppLanaguage
        {
            get { return m_IniFile.GetString(SECTION_ADDRESSBOOK, "INDEX_SYS_LANGUAGE", System.Threading.Thread.CurrentThread.CurrentUICulture.ToString()); }
            set { m_IniFile.WriteValue(SECTION_ADDRESSBOOK, "INDEX_SYS_LANGUAGE", value); }
        }

        public string AppVersion
        {
            get { return m_IniFile.GetString(SECTION_ADDRESSBOOK, "INDEX_SYS_VERSION", "1.0"); }
        }

        public int WeatherOfProvince
        {
            get { return m_IniFile.GetInt32(SECTION_WEATHER, "INDEX_WEATHER_PROVINCE", 0); }
            set { m_IniFile.WriteValue(SECTION_WEATHER, "INDEX_WEATHER_PROVINCE", value); }
        }

        public int WeatherOfCity
        {
            get { return m_IniFile.GetInt32(SECTION_WEATHER, "INDEX_WEATHER_CITY", 0); }
            set { m_IniFile.WriteValue(SECTION_WEATHER, "INDEX_WEATHER_CITY", value); }
        }

        public string WeatherOfCityName
        {
            get { return m_IniFile.GetString(SECTION_WEATHER, "INDEX_WEATHER_CITYNAME", ""); }
            set { m_IniFile.WriteValue(SECTION_WEATHER, "INDEX_WEATHER_CITYNAME", value); }
        }

        public string AdvertisementBeginTime
        {
            get { return m_IniFile.GetString(SECTION_ADVERTISEMENT, "INDEX_AD_BEGINTIME", "00:00:00"); }
            set { m_IniFile.WriteValue(SECTION_ADVERTISEMENT, "INDEX_AD_BEGINTIME", value); }
        }

        public string AdvertisementEndTime
        {
            get { return m_IniFile.GetString(SECTION_ADVERTISEMENT, "INDEX_AD_ENDTIME", "23:59:59"); }
            set { m_IniFile.WriteValue(SECTION_ADVERTISEMENT, "INDEX_AD_ENDTIME", value); }
        }

        public int HTTPServerPort
        {
            get { return m_IniFile.GetInt32(SECTION_PORT, "INDEX_PORT_HTTP", 80); }
            set { m_IniFile.WriteValue(SECTION_PORT, "INDEX_PORT_HTTP", value); }
        }

        public int FTPServerPort
        {
            get { return m_IniFile.GetInt32(SECTION_PORT, "INDEX_PORT_FTP", 21); }
            set { m_IniFile.WriteValue(SECTION_PORT, "INDEX_PORT_FTP", value); }
        }

        public int HeartbeatServicePort
        {
            get { return m_IniFile.GetInt32(SECTION_PORT, "INDEX_PORT_HEARTBEAT", 49201); }
            set { m_IniFile.WriteValue(SECTION_PORT, "INDEX_PORT_HEARTBEAT", value); }
        }

        public int SIPCommunicationPort
        {
            get { return m_IniFile.GetInt32(SECTION_PORT, "INDEX_PORT_SIP", 5060); }
            set { m_IniFile.WriteValue(SECTION_PORT, "INDEX_PORT_SIP", value); }
        }

        public int SIPServerPort
        {
            get { return m_IniFile.GetInt32(SECTION_ADDRESSBOOK, "INDEX_SIPSERVER_PORT", 5050); }
            set { m_IniFile.WriteValue(SECTION_ADDRESSBOOK, "INDEX_SIPSERVER_PORT", value); }
        }

        public string SIPServerIP
        {
            get { return m_IniFile.GetString(SECTION_ADDRESSBOOK, "INDEX_SIPSERVER_IP", ""); }
            set { m_IniFile.WriteValue(SECTION_ADDRESSBOOK, "INDEX_SIPSERVER_IP", value); }
        }

        public bool IsMulticastEnabled
        {
            get { return m_IniFile.GetInt32(SECTION_GENERAL, "INDEX_MULTICAST_ON", 0) != 0; }
            set { m_IniFile.WriteValue(SECTION_GENERAL, "INDEX_MULTICAST_ON", (value ? 1 : 0)); }
        }

        public CloudSolution CloudSolution
        {
            get { return (CloudSolution)m_IniFile.GetInt32(SECTION_GENERAL, "CLOUD_SOLUCTION", (int)(CloudSolution.SIPServer)); }
            set { m_IniFile.WriteValue(SECTION_GENERAL, "CLOUD_SOLUCTION", (int)value); }
        }
    }

    public enum CloudSolution
    {
        SIPServer = 0,
        PPHook
    }
}
