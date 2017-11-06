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
    public class ProvincesModel : CollectionModelBase<fs_province>
    {
        public ProvincesModel(IDataService<fs_province> dataService) : base(dataService)
        {
        }

        protected override void RefillData()
        {
            IValueConverter provinceNameConverter = new StringResourceIDToStringConverter();
            _data.ReplaceRange(_dataService.Select(p => p.ProvinceID > 0));
            //_data.ReplaceRange(_dataService.Select(p => p.ProvinceID > 0).Select(p => new fs_province
            //{
            //    ProvinceID = p.ProvinceID,
            //    ProvinceName = provinceNameConverter.Convert(p.ProvinceName, typeof(string), null, CultureInfo.CurrentCulture) as string,
            //}));
        }

        protected override Func<fs_province, bool> IdentityPredicate(fs_province obj)
        {
            return (d => d.ProvinceID == obj.ProvinceID);
        }
    }
}