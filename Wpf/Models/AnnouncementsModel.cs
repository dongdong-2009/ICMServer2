using ICMServer.Models;
using ICMServer.Services.Data;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ICMServer.WPF.Models
{
    public class AnnouncementsModel : CollectionModelBase<Announcement>
    {
        public AnnouncementsModel(IDataService<Announcement> dataService) : base(dataService)
        {
        }

        protected override Func<Announcement, bool> IdentityPredicate(Announcement obj)
        {
            return (d => d.id == obj.id);
        }
    }
}
