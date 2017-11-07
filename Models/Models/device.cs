using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICMServer.Models
{
    // 设备列表
    public partial class Device : BusinessObjectBase, IEditableObject
    {
        private int _id;
        public int id
        {
            get { return _id; }
            set { Set(ref _id, value); }
        }

        private string _ip;
        /// <summary>
        /// 设备的IP address
        /// </summary>
        public string ip
        {
            get { return _ip; }
            set { Set(ref _ip, value); }
        }

        private string _roomid;
        /// <summary>
        /// "ro", "00-00-00-00-00-01"
        /// </summary>
        public string roomid
        {
            get { return _roomid; }
            set { Set(ref _roomid, value); }
        }

        private string _alias;
        /// <summary>
        /// 设备別名，譬如"Control Server"，由使用者自行填寫
        /// </summary>
        public string Alias
        {
            get { return _alias; }
            set { Set(ref _alias, value); }
        }

        private string _group;
        /// <summary>
        /// multicast時的group ip address
        /// </summary>
        public string group
        {
            get { return _group; }
            set { Set(ref _group, value); }
        }

        private string _mac;
        /// <summary>
        /// 设备的mac address
        /// </summary>
        public string mac
        {
            get { return _mac; }
            set { Set(ref _mac, value); }
        }

        private int _online;
        public int online
        {
            get { return _online; }
            set { Set(ref _online, value); }
        }

        private int? _type;
        /// <summary>
        /// 设备種類，0->Control Server
        /// </summary>
        public int? type
        {
            get { return _type; }
            set { Set(ref _type, value); }
        }

        private string _sm;
        /// <summary>
        /// 设备的subnet mask
        /// </summary>
        public string sm
        {
            get { return _sm; }
            set { Set(ref _sm, value); }
        }

        private string _gw;
        /// <summary>
        /// 设备的gateway ip address
        /// </summary>
        public string gw
        {
            get { return _gw; }
            set { Set(ref _gw, value); }
        }

        private string _cameraid;
        /// <summary>
        /// 设备如果关联到某ip cam，登入该ip cam所需的账号和密码
        /// </summary>
        public string cameraid
        {
            get { return _cameraid; }
            set { Set(ref _cameraid, value); }
        }

        private string _camerapw;
        public string camerapw
        {
            get { return _camerapw; }
            set { Set(ref _camerapw, value); }
        }

        private int? _sd;
        /// <summary>
        /// 是否带有sd卡
        /// </summary>
        public int? sd
        {
            get { return _sd; }
            set { Set(ref _sd, value); }
        }

        private string _aVer;
        /// <summary>
        /// 设备地址薄版本
        /// </summary>
        public string AVer
        {
            get { return _aVer; }
            set { Set(ref _aVer, value); }
        }

        private string _cVer;
        /// <summary>
        /// 设备卡列表版本
        /// </summary>
        public string cVer
        {
            get { return _cVer; }
            set { Set(ref _cVer, value); }
        }

        private string _fVer;
        /// <summary>
        /// 设备軟件版本
        /// </summary>
        public string fVer
        {
            get { return _fVer; }
            set { Set(ref _fVer, value); }
        }

        private string _laVer;
        /// <summary>
        /// 设备在服务器最新地址薄版本
        /// </summary>
        public string laVer
        {
            get { return _laVer; }
            set { Set(ref _laVer, value); }
        }

        private string _lcVer;
        /// <summary>
        /// 设备在服务器最新卡列表版本
        /// </summary>
        public string lcVer
        {
            get { return _lcVer; }
            set { Set(ref _lcVer, value); }
        }

        private string _lfVer;
        /// <summary>
        /// 设备在服务器最新軟件版本
        /// </summary>
        public string lfVer
        {
            get { return _lfVer; }
            set { Set(ref _lfVer, value); }
        }

        #region IDataErrorInfo members
        private bool inEdit;
        private Device backupCopy;
        public void BeginEdit()
        {
            if (inEdit) return;
            inEdit = true;
            backupCopy = this.MemberwiseClone() as Device;
        }

        public void CancelEdit()
        {
            if (backupCopy != null)
            {
                this.id = backupCopy.id;
                this.ip = backupCopy.ip;
                this.roomid = backupCopy.roomid;
                this.Alias = backupCopy.Alias;
                this.group = backupCopy.group;
                this.mac = backupCopy.mac;
                this.online = backupCopy.online;
                this.type = backupCopy.type;
                this.sm = backupCopy.sm;
                this.gw = backupCopy.gw;
                this.cameraid = backupCopy.cameraid;
                this.camerapw = backupCopy.camerapw;
                this.sd = backupCopy.sd;
                this.AVer = backupCopy.AVer;
                this.cVer = backupCopy.cVer;
                this.fVer = backupCopy.fVer;
                this.laVer = backupCopy.laVer;
                this.lcVer = backupCopy.lcVer;
                this.lfVer = backupCopy.lfVer;
            }
        }

        public void EndEdit()
        {
            if (!inEdit) return;
            inEdit = false;
            backupCopy = null;
        }
        #endregion IDataErrorInfo members
    }

    public enum DeviceType
    {
        Control_Server = 0,
        Door_Camera,
        Lobby_Phone_Unit,
        Lobby_Phone_Building,
        Lobby_Phone_Area,
        Indoor_Phone,
        Administrator_Unit,
        Indoor_Phone_SD,
        Mobile_Phone,
        Emergency_Intercom_Unit,
        IPCAM,
    }

    public enum OnlineStatus
    {
        Offline = 0,
        Online,
    }
}
