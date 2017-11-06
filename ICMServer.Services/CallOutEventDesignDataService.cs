using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    public class CallOutEventDesignDataService : DesignDataServiceBase<eventcallout>
    {
        protected override void InitSampleData()
        {
            _objects.Add(new eventcallout
            {
                from = "01-01-10-00-00-02",
                to = "00-00-00-00-00",
                time = new DateTime(2012, 1, 1, 0, 4, 34),
                type = 2,
                action = "start"
            });
            _objects.Add(new eventcallout
            {
                from = "01-01-10-00-00-02",
                to = "00-00-00-00-00",
                time = new DateTime(2012, 1, 1, 0, 5, 34),
                type = 2,
                action = "end"
            });
            _objects.Add(new eventcallout
            {
                from = "01-01-10-00-00-02",
                to = "00-00-00-00-00",
                time = new DateTime(2012, 1, 1, 1, 4, 34),
                type = 2,
                action = "noack"
            });
        }
    }
}
