using GalaSoft.MvvmLight;
using ICMServer.Models;
using ICMServer.WPF.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ICMServer.WPF.ViewModels
{
    public class DialogAlarmViewModel : ViewModelBase
    {
        private readonly ICollectionModel<eventwarn> _dataModel;

        public DialogAlarmViewModel(
            ICollectionModel<eventwarn> dataModel)
            //List<eventwarn> alarms)
        {
            this._dataModel = dataModel;
            //this.Alarms = (ListCollectionView)new ListCollectionView((IList)alarms);
            this.Alarms = (ListCollectionView)new ListCollectionView((IList)_dataModel.Data);
            using (Alarms.DeferRefresh())
            {

                Alarms.Filter = delegate (object obj)
                {
                    eventwarn alarm = obj as eventwarn;
                    if (alarm != null && (!alarm.handlestatus.HasValue || alarm.handlestatus == 0) && alarm.action == "trig")
                    {
                        return true;
                    }
                    return false;
                };
                Alarms.SortDescriptions.Add(new SortDescription("time", ListSortDirection.Descending));
            }
        }

        #region Alarms
        private ListCollectionView _alarms;
        public ListCollectionView Alarms
        {
            get { return _alarms; }
            private set { this.Set(ref _alarms, value); }
        }
        #endregion

    }
}
