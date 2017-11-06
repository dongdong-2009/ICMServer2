using GalaSoft.MvvmLight;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ICMServer.WPF.ViewModels
{
    class DialogDisplayQRCodeViewModel : ViewModelBase
    {
        public DialogDisplayQRCodeViewModel(string content)
        {
            this.Content = content;
        }

        private string _Content;
        private string Content
        {
            set
            {
                if (this.Set(ref _Content, value))
                {
                    try
                    {
                        this.QRCode = BitmapToBitmapSource(ICMServer.QRCode.Encode(value, 300, 300));
                    }
                    catch (Exception) { }
                }
            }
        }

        private BitmapSource _QRCode;
        public BitmapSource QRCode
        {
            get { return _QRCode; }
            private set { this.Set(ref _QRCode, value); }
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        private BitmapSource BitmapToBitmapSource(System.Drawing.Bitmap bitmap)
        {
            IntPtr ptr = bitmap.GetHbitmap();
            BitmapSource result =
                System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    ptr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            //release resource
            DeleteObject(ptr);

            return result;
        }
    }
}
