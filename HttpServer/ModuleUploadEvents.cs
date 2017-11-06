using ICMServer.Models;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ICMServer.Net
{
    public class ModuleUploadEvents : NancyModule
    {
        public ModuleUploadEvents()
            : base("/doorbell")
        {
            //  http://xxx.xxx.xxx.xxx/doorbell/upload_event
            Post["/upload_event"] = parameters =>
            {
                int alarmCount = 0;
                int calloutCount = 0;
                int commonEventCount = 0;
                int opendoorEventCount = 0;
                List<eventwarn> newAlarms = new List<eventwarn>();

                /*******************************************
                    <?xml version="1.0" encoding="UTF-8"?>
                    <EventList>
                        <packet addr="01-01-10-00-00-01" type="2" event="open-door">
                            <item time="2014-04-10 21:10:10" XXXXX/>
                        </packet>
                    </EventList>
                ********************************************/
                XDocument doc = XDocument.Load(this.Request.Body);
                var DeviceNodes = doc.Root.Elements("packet");
                foreach (var DeviceNode in DeviceNodes)
                {
                    string DeviceId = DeviceNode.Attribute("addr").Value;
                    int DeviceType = int.Parse(DeviceNode.Attribute("type").Value);
                    string eventType = DeviceNode.Attribute("event").Value;
                    var events = DeviceNode.Elements("item");
                    switch (eventType)
                    {
                    case "warn":            ////安防报警信息上传
                        AddAlarms(DeviceId, events, newAlarms);
                        alarmCount++;
                        break;

                    case "callout":         ////呼叫记录上传
                        AddCalls(DeviceId, events);
                        calloutCount++;
                        break;

                    case "help-info":       ////信息上报
                        AddRepairs(DeviceId, events);
                        commonEventCount++;
                        break;

                    case "opendoor-password":   ////密码上传
                        AddPasswords(DeviceId, events);
                        opendoorEventCount++;
                        break;

                    case "open-door":       ////开门记录上传
                        AddEntrances(DeviceId, events);
                        opendoorEventCount++;
                        break;

                    case "snapshot":        ////拍照记录上传
                        AddSnapshotsAsync(DeviceId, this.Request.UserHostAddress, events);
                        opendoorEventCount++;
                        break;

                    case "register-Device": ////设备信息上传
                        var DeviceInfo = DeviceNode.Element("dev");
                        UpdateDevice(DeviceId, DeviceType, DeviceInfo);
                        break;
                    }
                }
                if (alarmCount > 0)
                    HttpServer.Instance.RaiseReceivedAlarmEvent(newAlarms);
                if (calloutCount > 0)
                    HttpServer.Instance.RaiseReceivedCallOutEvent();
                if (commonEventCount > 0)
                    HttpServer.Instance.RaiseReceivedCommonEvent();
                if (opendoorEventCount > 0)
                    HttpServer.Instance.RaiseReceivedOpenDoorEvent();
                return "";
            };
        }

        private void AddAlarms(string DeviceId, IEnumerable<XElement> events, List<eventwarn> newAlarms)
        {
            //int trig = 0;
            if (events.Count() == 0)
                return;

            using (var db = new ICMDBContext())
            {
                foreach (var e in events)
                {
                    eventwarn alarm = new eventwarn
                    {
                        channel = int.Parse(e.Attribute("chanel").Value),
                        type = e.Attribute("type").Value,
                        handlestatus = 0,
                        Action = e.Attribute("action").Value,
                        time = DateTime.Parse(e.Attribute("time").Value),
                        srcaddr = DeviceId
                    };

                    eventwarn alarmInDB = (from a in db.Eventwarns
                                           where a.srcaddr == alarm.srcaddr
                                              && a.channel == alarm.channel
                                              && a.Action == alarm.Action
                                              && a.time == alarm.time
                                           select a).FirstOrDefault();
                    if (alarmInDB != null)
                        continue;   // redudency data

                    if (alarm.Action == "trig")
                        newAlarms.Add(alarm);
                    db.Eventwarns.Add(alarm);

                    //DebugLog.TraceMessage(string.Format("{0}: {1} - {2}",
                    //    alarm.srcaddr, alarm.type, alarm.action));
                }

                try
                {
                    db.SaveChangesAsync();
                }
                catch (Exception) { }
                // TODO: Send message to UI.
                //if (trig > 0)
                //{
                //    //DialogEventWarn dlgWarn = new DialogEventWarn();
                //    //dlgWarn.ShowDialog();
                //}
            }
        }

        private void AddCalls(string DeviceId, IEnumerable<XElement> events)
        {
            if (events.Count() == 0)
                return;

            using (var db = new ICMDBContext())
            {
                foreach (var e in events)
                {
                    eventcallout call = new eventcallout
                    {
                        from = DeviceId,
                        to = e.Attribute("to").Value,
                        type = 2,
                        time = DateTime.Parse(e.Attribute("time").Value),
                        action = e.Attribute("action").Value
                    };

                    try
                    {
                        db.Eventcallouts.Add(call);
                        db.SaveChangesAsync();
                    }
                    catch (Exception) { }
                }
            }
        }

        private void AddRepairs(string DeviceId, IEnumerable<XElement> events)
        {
            if (events.Count() == 0)
                return;

            using (var db = new ICMDBContext())
            {
                foreach (var e in events)
                {
                    eventcommon repair = new eventcommon
                    {
                        action = e.Attribute("action").Value,
                        srcaddr = DeviceId,
                        time = DateTime.Parse(e.Attribute("time").Value),
                        content = e.Attribute("text").Value,
                        type = int.Parse(e.Attribute("type").Value)
                    };

                    try
                    {
                        db.Eventcommons.Add(repair);
                        db.SaveChangesAsync();
                    }
                    catch (Exception) { }
                }
            }
        }

        private void AddPasswords(string DeviceId, IEnumerable<XElement> events)
        {
            if (events.Count() == 0)
                return;

            using (var db = new ICMDBContext())
            {
                foreach (var e in events)
                {
                    doorbellpassword password = new doorbellpassword
                    {
                        C_roomid = DeviceId,
                        C_password = e.Attribute("password").Value,
                        C_time = DateTime.Parse(e.Attribute("time").Value)
                    };

                    db.Doorbellpasswords.Add(password);
                }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception) { }
            }
        }

        private void AddEntrances(string DeviceId, IEnumerable<XElement> events)
        {
            if (events.Count() == 0)
                return;

            using (var db = new ICMDBContext())
            {
                foreach (var e in events)
                {
                    eventopendoor entrance = new eventopendoor
                    {
                        C_from = DeviceId
                    };

                    string mode = e.Attribute("mode").Value;
                    switch (mode)
                    {
                    case "pwd":
                        if (e.Attribute("ro") != null)
                        {
                            entrance.C_open_object = e.Attribute("ro").Value;
                            entrance.C_time = Convert.ToDateTime(e.Attribute("time").Value);
                            entrance.C_mode = mode;
                            entrance.C_verified = e.Attribute("verified").Value == "true" ? 1 : 0;
                        }
                        break;

                    case "card":
                        if (e.Attribute("card") != null)
                        {
                            entrance.C_open_object = e.Attribute("card").Value;
                            entrance.C_time = Convert.ToDateTime(e.Attribute("time").Value);
                            entrance.C_mode = mode;
                            entrance.C_verified = e.Attribute("verified").Value == "true" ? 1 : 0;
                        }
                        break;

                    case "remote":
                        if (e.Attribute("ro") != null)
                        {
                            entrance.C_open_object = e.Attribute("ro").Value;
                            entrance.C_time = Convert.ToDateTime(e.Attribute("time").Value);
                            entrance.C_mode = mode;
                            entrance.C_verified = 1;
                        }
                        break;

                    default:
                        continue;
                    }

                    db.Eventopendoors.Add(entrance);
                }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception) { }
            }
        }

        private async void AddSnapshotsAsync(string DeviceId, string clientIP, IEnumerable<XElement> events)
        {
            if (events.Count() == 0)
                return;

            using (var db = new ICMDBContext())
            {
                foreach (var e in events)
                {
                    if (e.Attribute("time").Value != null)
                    {
                        photograph snapshot = new photograph
                        {
                            C_srcaddr = DeviceId,
                            C_time = DateTime.Parse(e.Attribute("time").Value)
                        };

                        string imagePath = string.Format("http://{0}/photo?ro={1}&time={2}",
                            clientIP,
                            DeviceId,
                            e.Attribute("time").Value.Replace(" ", "%20"));

                        System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                        try
                        {
                            snapshot.C_img = await client.GetByteArrayAsync(new Uri(imagePath));
                            db.Photographs.Add(snapshot);
                        }
                        catch (Exception) { }
                    }
                }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception) { }
            }
        }

        private void UpdateDevice(string DeviceId, int DeviceType, XElement DeviceInfo)
        {
            using (var db = new ICMDBContext())
            {
                var Device = (from d in db.Devices
                              where d.roomid == DeviceId
                              select d).FirstOrDefault();
                if (Device != null)
                {
                    if (DeviceInfo.Attribute("ip") != null) // TODO: error check
                        Device.ip = DeviceInfo.Attribute("ip").Value;
                    if (DeviceInfo.Attribute("mc") != null) // TODO: error check
                        Device.mac = DeviceInfo.Attribute("mc").Value;
                    Device.type = DeviceType;
                    Device.online = 1;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception) { }
                }
                // TODO: Send message to UI.
            }
        }
    }
}
