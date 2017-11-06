using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    public class AlarmDesignDataService : DesignDataServiceBase<eventwarn>
    {
        protected override void InitSampleData()
        {
            _objects.Add(new eventwarn
            {
                srcaddr = "01-01-10-01-01-01",
                time = DateTime.Now,
                handlestatus = 0,
                handletime = null,
                type = "smoke",
                channel = 4,
                action = "enable",
                handler = null,
            });
            _objects.Add(new eventwarn
            {
                srcaddr = "01-01-10-01-01-01",
                time = DateTime.Now,
                handlestatus = 0,
                handletime = null,
                type = "gas",
                channel = 5,
                action = "enable",
                handler = null,
            });
            _objects.Add(new eventwarn
            {
                srcaddr = "01-01-10-01-01-01",
                time = DateTime.Now,
                handlestatus = 0,
                handletime = null,
                type = "smoke",
                channel = 4,
                action = "trig",
                handler = null,
            });
            _objects.Add(new eventwarn
            {
                srcaddr = "01-01-10-01-01-01",
                time = DateTime.Now,
                handlestatus = 0,
                handletime = null,
                type = "gas",
                channel = 5,
                action = "trig",
                handler = null,
            });
            _objects.Add(new eventwarn
            {
                srcaddr = "01-01-10-01-01-01",
                time = DateTime.Now,
                handlestatus = 0,
                handletime = null,
                type = "smoke",
                channel = 4,
                action = "unalarm",
                handler = null,
            });
            _objects.Add(new eventwarn
            {
                srcaddr = "01-01-10-01-01-01",
                time = DateTime.Now,
                handlestatus = 0,
                handletime = null,
                type = "gas",
                channel = 5,
                action = "unalarm",
                handler = null,
            });
            _objects.Add(new eventwarn
            {
                srcaddr = "01-01-10-01-01-01",
                time = DateTime.Now,
                handlestatus = 0,
                handletime = null,
                type = "smoke",
                channel = 4,
                action = "disable",
                handler = null,
            });
            _objects.Add(new eventwarn
            {
                srcaddr = "01-01-10-01-01-01",
                time = DateTime.Now,
                handlestatus = 0,
                handletime = null,
                type = "gas",
                channel = 5,
                action = "disable",
                handler = null,
            });
        }
    }
}
