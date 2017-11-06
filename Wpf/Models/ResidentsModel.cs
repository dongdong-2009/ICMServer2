using ICMServer.Models;
using ICMServer.Services.Data;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ICMServer.WPF.Models
{
    public class ResidentsModel : CollectionModelBase<holderinfo>
    {
        public ResidentsModel(IDataService<holderinfo> dataService) : base(dataService)
        {
        }

        protected override Func<holderinfo, bool> IdentityPredicate(holderinfo obj)
        {
            return (d => d.C_id == obj.C_id);
        }
    }
}
