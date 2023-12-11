using QRCoder.Xaml;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.QR
{
    public class QRManager
    {
        public System.Windows.Media.DrawingImage Generate(object info)
        {

            var options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };
            var jsonString = JsonSerializer.Serialize(info, options);

            var generator = new QRCodeGenerator();
            var data = generator.CreateQrCode(jsonString, QRCodeGenerator.ECCLevel.L);
            var qrCode = new XamlQRCode(data);
            var image = qrCode.GetGraphic(100);

            return image;

        }

    }
}
