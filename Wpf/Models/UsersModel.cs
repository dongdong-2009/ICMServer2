using ICMServer.Models;
using ICMServer.Services.Data;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ICMServer.WPF.Models
{
    class UsersModel : CollectionModelBase<user>
    {
        public UsersModel(IDataService<user> dataService) : base(dataService)
        {
        }

        protected override Func<user, bool> IdentityPredicate(user obj)
        {
            return (d => d.C_id == obj.C_id);
        }
    }
}
