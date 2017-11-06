using ICMServer.Models;
using ICMServer.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.WPF.Models
{
    class UpgradeTasksModel : CollectionModelBase<UpgradeTask>, IUpgradeTasksModel
    {
        public UpgradeTasksModel(IDataService<UpgradeTask> dataService) : base(dataService)
        {
        }

        protected override void RefillData()
        {
            _data.ReplaceRange(_dataService.SelectAll(t => t.Device, t => t.UpgradeFile));
        }

        protected override Func<UpgradeTask, bool> IdentityPredicate(UpgradeTask obj)
        {
            return (d => d.ID == obj.ID);
        }
    }
}
