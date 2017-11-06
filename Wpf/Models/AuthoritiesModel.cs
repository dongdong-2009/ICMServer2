using ICMServer.Models;
using ICMServer.Services.Data;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ICMServer.WPF.Models
{
    class AuthoritiesModel : CollectionModelBase<authority>
    {
        public AuthoritiesModel(IDataService<authority> dataService) : base(dataService)
        {
        }

        protected override Func<authority, bool> IdentityPredicate(authority obj)
        {
            return (d => d.C_id == obj.C_id);
        }
    }
}
