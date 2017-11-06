using FluentValidation;
using ICMServer.Models;
using ICMServer.WPF.Models;

namespace ICMServer.WPF.ViewModels
{
    public class ResidentViewModel : SingleDataViewModelBase<holderinfo, ResidentViewModel>
    {
#if DEBUG
        public ResidentViewModel() : base() { }

        protected override void InitSampleData()
        {
            this.Name = "王小明";
            this.Sex = null;
            this.Phone = "03-12345678";
            this.Address = "01-01-01-01-01";
            this.IsHead = true;
        }
#endif

        /// <summary>
        /// Initializes a new instance of the ResidentViewModel class.
        /// </summary>
        public ResidentViewModel(
            IValidator<ResidentViewModel> validator,
            ICollectionModel<holderinfo> dataModel,
            holderinfo data = null) : base(validator, dataModel, data)
        { }

        #region Data Mapping Properties
        public int ID { get; protected set; }

        // 住户名称
        private string _name;
        public string Name
        {
            get { return _name; }
            set { this.Set(ref _name, value); }
        }

        // 住户性别
        private Sex? _sex;
        public Sex? Sex
        {
            get { return _sex; }
            set
            {
                //DebugLog.TraceMessage(string.Format("sex: {0}", (value != null ? value.ToString() : "null")));
                this.Set(ref _sex, value);
            }
        }

        // 电话号码
        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { this.Set(ref _phone, value); }
        }

        // 住户门牌号
        private string _address;
        public string Address
        {
            get { return _address; }
            set { this.Set(ref _address, value); }
        }

        // 是否户主/戶長
        private bool _isHead;
        public bool IsHead
        {
            get { return _isHead; }
            set { this.Set(ref _isHead, value); }
        }
        #endregion

        protected override void ModelToViewModel()
        {
            this.ID = this._data.C_id;
            this.Name = this._data.C_name;
            this.Sex = null;
            if (this._data.C_sex != null)
            {
                switch (this._data.C_sex)
                {
                    case (int)ICMServer.Models.Sex.Male:
                        this.Sex = ICMServer.Models.Sex.Male;
                        break;

                    case (int)ICMServer.Models.Sex.Female:
                        this.Sex = ICMServer.Models.Sex.Female;
                        break;
                }
            }
            this.Phone = this._data.C_phoneno;
            this.Address = this._data.C_roomid;
            this.IsHead = (this._data.C_isholder.HasValue && this._data.C_isholder != 0) ? true : false;
        }

        protected override void ViewModelToModel()
        {
            this._data.C_name = this.Name;
            this._data.C_phoneno = this.Phone;
            this._data.C_roomid = this.Address;
            this._data.C_isholder = this.IsHead ? 1 : 0;
            this._data.C_sex = (int?)this.Sex;
        }

        protected override void InitDefaultValue()
        {
        }
    }
}
