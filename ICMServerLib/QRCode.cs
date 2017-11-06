using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer
{
    public static class QRCode
    {
        public static Bitmap Encode(
            string strToBeEncoded,
            int width = 180,
            int height = 180,
            string charSet = "UTF-8")
        {
            ZXing.BarcodeWriter writer = new ZXing.BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.QrCode.QrCodeEncodingOptions
                {
                    //生成出图片的高度
                    Height = height,
                    //生成出图片的寬度
                    Width = width,
                    //文字是使用哪種編碼方式
                    CharacterSet = charSet,

                    //錯誤修正容量
                    //L水平    7%的字碼可被修正
                    //M水平    15%的字碼可被修正
                    //Q水平    25%的字碼可被修正
                    //H水平    30%的字碼可被修正
                    ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.H
                }
            };
            //将要編碼的文字生成出QRCode的图檔
            return writer.Write(strToBeEncoded);
        }
    }
}
