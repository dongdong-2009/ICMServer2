using ICMServer.Models;
using ICMServer.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    public class CityDesignDataService : DesignDataServiceBase<fs_city>
    {
        protected override void InitSampleData()
        {
            _objects.Add(new fs_city
            {
                CityID = 1,
                CityName = "北京市",
                ZipCode = "100000",
                ProvinceID = 1,
            });
            _objects.Add(new fs_city
            {
                CityID = 2,
                CityName = "天津市",
                ZipCode = "100000",
                ProvinceID = 2,
            });
        }
    }
}
