using FluentValidation;
using GalaSoft.MvvmLight.CommandWpf;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.WPF.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ICMServer.WPF.ViewModels
{
    public class AnnouncementViewModel : SingleDataViewModelBase<Announcement, AnnouncementViewModel>
    {
        private readonly IDialogService _dialogService;
        private readonly IAnnouncementsRoomsModel _announcementsRoomsDataModel;
        private readonly IRoomsModel _roomDataModel;

        /// <summary>
        /// Initializes a new instance of the AnnouncementViewModel class.
        /// </summary>
        public AnnouncementViewModel(
            IValidator<AnnouncementViewModel> validator,
            ICollectionModel<Announcement> dataModel,
            IAnnouncementsRoomsModel announcementsRoomsDataModel,
            IRoomsModel roomDataModel,
            IDialogService dialogService,
            Announcement data = null) : base(validator, dataModel, data)
        {
            this._announcementsRoomsDataModel = announcementsRoomsDataModel;
            this._roomDataModel = roomDataModel;
            this._dialogService = dialogService;
            this.DstRooms = (ListCollectionView)new ListCollectionView((IList)_roomDataModel.Data);
            using (DstRooms.DeferRefresh())
            {
                DstRooms.Filter = new Predicate<object>(FilterByDstRoomAddress);
                DstRooms.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Ascending));
            }
        }

        private bool FilterByDstRoomAddress(object obj)
        {
            Room room = obj as Room;
            if (room != null
             && (string.IsNullOrEmpty(DstRoomAddress)
              || room.ID.StartsWith(DstRoomAddress)))
            {
                return true;
            }
            return false;
        }

#if DEBUG
        public AnnouncementViewModel() : base() { }

        protected override void InitSampleData()
        {
            ID = 1;
            Title = "社區環境消毒";
            //DstRoomAddress = "01-02-03-04-05";
            InsertedTime = DateTime.Now;
            FilePath = Path.GetAppExeFolderPath() + @"\" + @"data\publish_informations\201704261357595016.jpg";
            Type = MessageType.Text;
            //HasBeenRead = true;
        }
#endif

        protected override void InitDefaultValue()
        {
        }

        protected override void ModelToViewModel()
        {
            ID = this._data.id;
            Title = this._data.title;
            //DstRoomAddress = this._data.dstaddr;
            InsertedTime = this._data.time;
            FilePath = !string.IsNullOrEmpty(this._data.filepath) ? Path.GetAppExeFolderPath() + @"\" + this._data.filepath : null;
            Type = MessageType.Text;
            if (this._data.type.HasValue)
            {
                switch (this._data.type)
                {
                    case (int)MessageType.Image:
                        Type = MessageType.Image;
                        break;
                }
            }
            //HasBeenRead = (this._data.isread.HasValue && this._data.isread != 0);
        }

        protected override void ViewModelToModel()
        {
            this._data.title = Title;
            //this._data.dstaddr = DstRoomAddress ?? "";
            this._data.time = InsertedTime;
            this._data.filepath = FilePath.Substring((Path.GetAppExeFolderPath() + @"\").Length);
            this._data.type = (int)Type;
            //this._data.isread = HasBeenRead ? 1 : 0;
        }

        protected override async Task AddAsync()
        {
            this._data = new Announcement();
            if (this._dataModel != null && this._data != null)
            {
                string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                string filename = string.Format(timeStamp + ".jpg");
                this.FilePath = Path.GetPublishInfoFolderPath() + @"\" + filename;
                switch (this.Type)
                {
                    case MessageType.Text:
                        TextJpgConvert(this.TextContent, this.Font, this.FilePath);
                        break;

                    case MessageType.Image:
                        File.Copy(this.ImageFilePath, this.FilePath, true);
                        break;
                }

                ViewModelToModel();
                // get rooms
                var rooms = this.DstRooms.Cast<Room>().ToList();
                foreach (var room in rooms)
                {
                    this._data.AnnouncementRooms.Add(new AnnouncementRoom
                    {
                        Announcement = this._data,
                        Room = room,
                        HasRead = false
                    });
                }
                await this._dataModel.InsertAsync(this._data);
            }
        }

        #region Data Mapping Properties

        #region ID
        public int ID { get; set; }
        #endregion

        #region Title
        private string _title;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { this.Set(ref _title, value); }
        }
        #endregion

        #region InsertedTime
        private DateTime? _insertedTime;
        /// <summary>
        /// 发布时间
        /// </summary>
        public System.DateTime? InsertedTime
        {
            get { return _insertedTime ?? DateTime.Now; }
            set { this.Set(ref _insertedTime, value); }
        }
        #endregion

        #region FilePath
        private string _filePath;
        /// <summary>
        /// 公告訊息內容图片保存絕對位置
        /// </summary> 
        public string FilePath
        {
            get { return _filePath; }
            private set { this.Set(ref _filePath, value); }
        }
        #endregion

        #region Type
        private MessageType _type;
        /// <summary>
        /// 內容型別
        /// </summary> 
        public MessageType Type
        {
            get { return _type; }
            set { this.Set(ref _type, value); }
        }
        #endregion

        //#region HasBeenRead
        //private bool _hasBeenRead;
        ///// <summary>
        ///// 读取标志位 
        ///// </summary>
        //public bool HasBeenRead
        //{
        //    get { return _hasBeenRead; }
        //    set { this.Set(ref _hasBeenRead, value); }
        //}
        //#endregion
        #endregion

        #region DstRoomAddress
        private string _dstRoomAddress;
        /// <summary>
        /// 发布位置
        /// </summary>
        public string DstRoomAddress
        {
            get { return _dstRoomAddress; }
            set
            {
                if (this.Set(ref _dstRoomAddress, value))
                {
                    DstRooms.Filter = new Predicate<object>(FilterByDstRoomAddress);
                }
            }
        }
        #endregion

        #region ImageFilePath
        private string _imageFilePath;
        /// <summary>
        /// 公告訊息內容图片保存絕對位置
        /// </summary> 
        public string ImageFilePath
        {
            get { return _imageFilePath; }
            private set { this.Set(ref _imageFilePath, value); }
        }
        #endregion

        #region TextContent
        private string _textContent;
        public string TextContent
        {
            get { return _textContent; }
            private set { this.Set(ref _textContent, value); }
        }
        #endregion

        //private Image _textToImageContent;
        //public Image TextToImageContent
        //{
        //    set { this.Set(ref _textToImageContent, value); }
        //}

        #region DstDevices
        private ListCollectionView _DstDevices;
        public ListCollectionView DstRooms
        {
            get { return _DstDevices; }
            private set
            {
                this.Set(ref _DstDevices, value);
            }
        }
        #endregion

        public int TextContentWidth
        {
            get { return 854; }
        }

        public int TextContentHeight
        {
            get { return 314; }
        }

        private System.Drawing.Font _Font;
        public System.Drawing.Font Font
        {
            private get { return _Font; }
            set { this.Set(ref _Font, value); }
        }

        #region PickDstRoomAddressCommand
        private RelayCommand _PickDstRoomAddressCommand;
        public RelayCommand PickDstRoomAddressCommand
        {
            get
            {
                return _PickDstRoomAddressCommand ?? (_PickDstRoomAddressCommand = new RelayCommand(() =>
                {
                    this._dialogService.ShowSelectRoomAddressDialog(
                        this.DstRoomAddress,
                        (roomAddress) => { this.DstRoomAddress = roomAddress; });
                }));
            }
        }
        #endregion

        #region PickImageFileCommand
        private RelayCommand _pickImageFileCommand;
        public RelayCommand PickImageFileCommand
        {
            get
            {
                return _pickImageFileCommand ?? (_pickImageFileCommand = new RelayCommand(() =>
                {
                    _dialogService.ShowOpenFileDialog(
                        "JPEG files (*.jpg)|*.jpg|All files (*.*)|*.*",
                        null,
                        (PickedFilePath) => { this.ImageFilePath = PickedFilePath; });
                }));
            }
        }
        #endregion
        
        private void TextJpgConvert(string txt, System.Drawing.Font font, string filepath)
        {
            Bitmap bmp = new Bitmap(1, 1);
            Graphics graphics = Graphics.FromImage(bmp);
            SizeF stringSize = graphics.MeasureString(txt, font, this.TextContentWidth);
            int width = Math.Max(this.TextContentWidth, (int)stringSize.Width);
            int height = Math.Max(this.TextContentHeight, (int)stringSize.Height);

            width = (width + 7) / 8 * 8;
            height = (height + 7) / 8 * 8;

            bmp = new Bitmap(bmp, width, height);
            graphics = Graphics.FromImage(bmp);
            RectangleF rect = new RectangleF(0, 0, width, height);
            graphics.FillRectangle(new SolidBrush(Color.White), rect);
            graphics.DrawString(txt, font, Brushes.Black, rect);
            graphics.Flush();

            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
            // Create an Encoder object based on the GUID
            // for the Quality parameter category.
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object.
            // An EncoderParameters object has an array of EncoderParameter
            // objects. In this case, there is only one
            // EncoderParameter object in the array.
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            bmp.Save(filepath, jgpEncoder, myEncoderParameters);
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
