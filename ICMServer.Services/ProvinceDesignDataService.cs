using ICMServer.Models;
using ICMServer.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    public class ProvinceDesignDataService : DesignDataServiceBase<fs_province>
    {
        protected override void InitSampleData()
        {
            _objects.Add(new fs_province
            {
                ProvinceID = 1,
                ProvinceName = "北京市"
            });
            _objects.Add(new fs_province
            {
                ProvinceID = 2,
                ProvinceName = "天津市"
            });
        }
    }
}
