using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    public class AdvertisementDesignDataService : DesignDataServiceBase<advertisement>
    {
        protected override void InitSampleData()
        {
            _objects.Add(new advertisement
            {
                C_id = 1,
                C_no = 1,
                C_title = "玩具總動員",
                C_time = DateTime.Now,
                C_path = @"D:\玩具總動員.mp4"
            });
            _objects.Add(new advertisement
            {
                C_id = 2,
                C_no = 2,
                C_title = "玩具總動員2",
                C_time = new DateTime(2016, 12, 23, 20, 23, 12),
                C_path = @"D:\玩具總動員2.mp4"
            });
        }
    }
}
