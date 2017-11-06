using ICMServer.Models;
using ICMServer.Services.Data;
using ICMServer.WPF.Converter;
using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ICMServer.WPF.Models
{
    public class CitiesModel : CollectionModelBase<fs_city>
    {
        public CitiesModel(IDataService<fs_city> dataService) : base(dataService)
        {
        }

        protected override void RefillData()
        {
            IValueConverter cityNameConverter = new StringResourceIDToStringConverter();
            _data.ReplaceRange(_dataService.Select(c => c.CityID > 0));
            //_data.ReplaceRange(_dataService.Select(c => c.CityID > 0).Select(c => new fs_city
            //{
            //    Country = c.Country,
            //    CityID = c.CityID,
            //    CityName = cityNameConverter.Convert(c.CityName, typeof(string), null, CultureInfo.CurrentCulture) as string,
            //    ZipCode = c.ZipCode,
            //    ProvinceID = c.ProvinceID
            //}));
        }

        protected override Func<fs_city, bool> IdentityPredicate(fs_city obj)
        {
            return (d => d.CityID == obj.CityID);
        }
    }
}