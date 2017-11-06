using System;
using System.Threading;
using System.Threading.Tasks;

namespace ICMServer.Net
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// GetStringAsync和ReadAsStringAsync的差异請見 
    /// http://www.cnblogs.com/TianFang/archive/2012/03/10/2389480.html 
    /// </remarks>
    public static class HttpClient
    {
        static System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
        public static TimeSpan Timeout = new TimeSpan(0, 0, 3);

        // * 取得 PPHook Service Token
        // GET /GetPPHookServiceToken
        // Sample Request:
        // GET /api/sipaccounts/groups/6000
        // Content-Type: application/json; charset=utf-8
        // ________________________________________
        // Status: 200 OK
        // Content-Type: application/json; charset=utf-8
        // {"token":"9b1d4530301de2ff30e2cfe22de1ab7ecfcf"}
        public static async Task<string> GetPPHookServiceToken(string pphookServiceHttpPort)
        {
            string result = "";
            string uri = string.Format("http://localhost:{0}/GetPPHookServiceToken", pphookServiceHttpPort);

            try
            {
                var response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                result = await response.Content.ReadAsStringAsync();
            }
            catch (Exception) { }
            return result;
        }

        public static async Task<bool> SendNewTextMsgCount(string clientIP, int newMsgCount)
        {
            bool result = false;
            string uri = string.Format("http://{0}/message_new?count={1}&type=text", clientIP, newMsgCount);
            //client.Timeout = Timeout;

            try
            {
                var response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                result = true;
            }
            catch (Exception) { }

            return result;
        }

        public static async Task<bool> SendNewVideoMsgCount(string clientIP, int newMsgCount)
        {
            bool result = false;
            string uri = string.Format("http://{0}/message_new?count={1}&type=video", clientIP, newMsgCount);
            //client.Timeout = Timeout;

            try
            {
                var response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                result = true;
            }
            catch (Exception) { }

            return result;
        }

        /// <summary>
        /// 撤消安防
        /// </summary>
        /// <param name="clientIP"></param>
        /// <param name="DeviceId"></param>
        /// <returns></returns>
        public static async Task<bool> SendDisableSafties(string clientIP, string DeviceId)
        {
            bool result = false;
            string uri = string.Format("http://{0}/unguard?ro={1}", clientIP, DeviceId);
            //client.Timeout = Timeout;

            try
            {
                var response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                result = true;
            }
            catch (Exception) { }

            return result;
        }

        /// <summary>
        /// 升級通知
        /// </summary>
        /// <param name="clientIP"></param>
        /// <param name="serverIP"></param>
        /// <param name="filePath"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        private static async Task<bool> SendUpgradeNotification(
            string clientIP,
            string serverIP,
            string filePath,
            string function)
        {
            bool result = false;
            string uri = string.Format("http://{0}/dev/info.cgi?action={3}&url=ftp://{1}/{2}",
                    clientIP, serverIP, filePath.Replace(@"\", "/"), function);
            //client.Timeout = Timeout;
            try
            {
                var response = await client.GetStringAsync(uri);
                result = true;
            }
            catch (Exception) { }

            return result;
        }

        /// <summary>
        /// 設定安防模式
        /// </summary>
        /// <param name="clientIP"></param>
        /// <param name="DeviceId"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        private static async Task<bool> SendSetSaftiesMode(
            string clientIP, 
            string DeviceId,
            int    mode)
        {
            bool result = false;
            string uri = string.Format("http://{0}/guard?ro={1}&mode={2}", clientIP, DeviceId, mode);
            //client.Timeout = Timeout;
            try
            {
                var response = await client.GetStringAsync(uri);
                result = true;
            }
            catch (Exception) { }

            return result;
        }

        /// <summary>
        /// 地址簿升級通知
        /// </summary>
        /// <param name="clientIP"></param>
        /// <param name="serverIP"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static async Task<bool> SendAddressBookUpgradeNotification(
            string clientIP,
            string serverIP,
            string filePath)
        {
            return await SendUpgradeNotification(clientIP, serverIP, filePath, "upgrade_addressbook");
        }

        /// <summary>
        /// 門禁卡列表升級通知
        /// </summary>
        /// <param name="clientIP"></param>
        /// <param name="serverIP"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static async Task<bool> SendCardListUpgradeNotification(
            string clientIP,
            string serverIP,
            string filePath)
        {
            return await SendUpgradeNotification(clientIP, serverIP, filePath, "upgrade_cardlist");
        }

        /// <summary>
        /// 韌體升級通知
        /// </summary>
        /// <param name="clientIP"></param>
        /// <param name="serverIP"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static async Task<bool> SendFirmwareUpgradeNotification(
            string clientIP,
            string serverIP,
            string filePath)
        {
            return await SendUpgradeNotification(clientIP, serverIP, filePath, "upgrade_fw");
        }

        /// <summary>
        /// 開門
        /// </summary>
        /// <param name="clientIP"></param>
        /// <param name="roomID"></param>
        /// <returns></returns>
        public static async Task<bool> SendOpenDoor(
           string clientIP,
           string roomID)
        {
            bool result = false;
            string uri = string.Format("http://{0}/open_door?ro={1}", clientIP, roomID);

            try
            {
                var response = await client.GetStringAsync(uri);
                result = true;
            }
            catch (Exception) { }

            return result;
        }

        /// <summary>
        /// 對 SIP Server 新增一筆 SIP account
        /// </summary>
        /// <param name="sipServerIP"></param>
        /// <param name="sipServerPort"></param>
        /// <param name="accountName"></param>
        /// <param name="accountPassword"></param>
        /// <returns></returns>
        public static async Task<bool> AddSipAccount(
            string              sipServerIP,
            int                 sipServerPort,
            string              accountName,
            string              accountPassword,
            CancellationToken   CancelToken = new CancellationToken())
        {
            bool result = false;
            string uri = string.Format("http://{0}:{1}/AddSipAccount?sip_account={2}&sip_pwd={3}&apply=0",
                sipServerIP, sipServerPort, accountName, accountPassword);

            //try
            {
                var response = await client.GetAsync(uri, CancelToken);
                response.EnsureSuccessStatusCode();
                if ("0" == await response.Content.ReadAsStringAsync())
                    result = true;
            }
            //catch (Exception) { }
            result = true;

            return result;
        }

        public static async Task<bool> AddRingGroup(
            string              sipServerIP,
            int                 sipServerPort,
            string              groupName,
            string              accountsList,
            CancellationToken   CancelToken = new CancellationToken())
        {
            bool result = false;
            string uri = string.Format("http://{0}:{1}/AddRingGroup?group_num={2}&group_list={3}",
                sipServerIP, sipServerPort, groupName, accountsList);

            //try
            {
                var response = await client.GetAsync(uri, CancelToken);
                response.EnsureSuccessStatusCode();
                if ("0" == await response.Content.ReadAsStringAsync())
                    result = true;
            }
            //catch (Exception) { }
            //result = true;

            return result;
        }

        public static async Task<bool> IsSipAccountOnline(
            string              sipServerIP,
            int                 sipServerPort,
            string              accountName,
            CancellationToken   CancelToken = new CancellationToken())
        {
            bool result = false;
            string uri = string.Format("http://{0}:{1}/QuerySipAccountOnline?account={2}",
                sipServerIP, sipServerPort, accountName);

            var response = await client.GetAsync(uri, CancelToken);
            response.EnsureSuccessStatusCode();
            if ("100" == await response.Content.ReadAsStringAsync())
                result = true;

            return result;
        }


        public static async Task<bool> PushNotification(
            string              sipServerIP,
            int                 sipServerPort,
            string              accountName,
            CancellationToken   CancelToken = new CancellationToken())
        {
            bool result = false;
            string uri = string.Format("http://{0}:{1}/PushSipAccount?from_account=ICMServer&to_account={2}",
                sipServerIP, sipServerPort, accountName);

            var response = await client.GetAsync(uri, CancelToken);
            response.EnsureSuccessStatusCode();
            if ("0" == await response.Content.ReadAsStringAsync())
                result = true;

            return result;
        }

    }
}
