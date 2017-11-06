using FluentValidation;
using ICMServer.Models;
using ICMServer.WPF.Models;
using System;

namespace ICMServer.WPF.ViewModels
{
    public class AlarmViewModel : SingleDataViewModelBase<eventwarn, AlarmViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the AlarmViewModel class.
        /// </summary>
        public AlarmViewModel(
            IValidator<AlarmViewModel> validator,
            ICollectionModel<eventwarn> dataModel,
            eventwarn data = null) : base(validator, dataModel, data)
        {
        }

#if DEBUG
        public AlarmViewModel() : base() { }

        protected override void InitSampleData()
        {
            this.SrcDeviceAddress = "01-01-10-01-01-01";
            this.InsertedTime = DateTime.Now - new TimeSpan(0, 10, 5, 2);
            this.Channel = 5;
            this.Event = "trig";
            this.ProcessState = ICMServer.Models.ProcessState.Processed;
            this.ProcessTime = DateTime.Now;
            this.Detector = "gas";
            this.Handler = "王小名";
        }
#endif

        protected override void InitDefaultValue()
        {
        }

        protected override void ModelToViewModel()
        {
            SrcDeviceAddress = this._data.srcaddr;
            InsertedTime = this._data.time;
            try
            {
                ProcessState = this._data.handlestatus.HasValue 
                             ? (ProcessState)this._data.handlestatus
                             : ProcessState.Unprocessed;
            } catch (Exception) { }
            ProcessTime = this._data.handletime ?? DateTime.Now;
            Detector = this._data.type;
            Channel = this._data.channel;
            Event = this._data.action;
            Handler = this._data.handler;
        }

        protected override void ViewModelToModel()
        {
            this._data.srcaddr = SrcDeviceAddress;
            this._data.time = InsertedTime;
            this._data.handlestatus = (int)ProcessState;
            this._data.handletime = ProcessTime;
            this._data.type = Detector;
            this._data.channel = Channel;
            this._data.action = Event;
            this._data.handler = Handler;
        }

        #region Data Mapping Properties

        private string _SrcDeviceAddress;
        /// <summary>
        /// 报警事件上报位置
        /// </summary>
        public string SrcDeviceAddress
        {
            get { return _SrcDeviceAddress; }
            set { this.Set(ref _SrcDeviceAddress, value); }
        }

        private DateTime _insertedTime;
        /// <summary>
        /// 报警事件产生時間
        /// </summary>
        public DateTime InsertedTime
        {
            get { return _insertedTime; }
            set { this.Set(ref _insertedTime, value); }
        }

        private ProcessState _ProcessState;
        /// <summary>
        /// 处理状态
        /// </summary>
        public ProcessState ProcessState
        {
            get { return _ProcessState; }
            set
            {
                if (this.Set(ref _ProcessState, value))
                {
                    this.RaisePropertyChanged(() => ProcessTime);
                    this.RaisePropertyChanged(() => Handler);
                }
            }
        }

        private DateTime? _ProcessTime;
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? ProcessTime
        {
            get
            {
                if (this.ProcessState == ProcessState.Processed)
                    return _ProcessTime;
                return null;
            }
            set { _ProcessTime = value; }
        }

        private string _Detector;
        /// <summary>
        /// 报警事件类型: "emergency", "infrared", "door", "window", "smoke", "gas", "area", "rob"
        /// </summary>
        public string Detector
        {
            get { return _Detector; }
            set { this.Set(ref _Detector, value); }
        }

        private int _Channel;
        /// <summary>
        /// 通道位置
        /// </summary>
        public int Channel
        {
            get { return _Channel; }
            set { this.Set(ref _Channel, value); }
        }

        private string _Event;
        /// <summary>
        /// 事件: "enable", "disable", "trig", "unalarm"
        /// </summary>
        public string Event
        {
            get { return _Event; }
            set { this.Set(ref _Event, value); }
        }

        private string _Handler;
        /// <summary>
        /// 处理者
        /// </summary>
        public string Handler
        {
            get
            {
                if (this.ProcessState != ProcessState.Unprocessed)
                    return _Handler;
                return null;
            }
            set { this.Set(ref _Handler, value); }
        }
        #endregion
    }
}
