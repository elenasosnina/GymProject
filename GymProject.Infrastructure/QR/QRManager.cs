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
        public System.Windows.Media.DrawingImage Generate(object info)// Метод для генерации изображения QR-кода на основе переданной информации.
        {
            // Настройки сериализации в JSON
            var options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };
            var jsonString = JsonSerializer.Serialize(info, options);// Преобразование объекта информации в JSON-строку.
            // Создание генератора QR-кода.
            var generator = new QRCodeGenerator();
            var data = generator.CreateQrCode(jsonString, QRCodeGenerator.ECCLevel.L);
            var qrCode = new XamlQRCode(data);
            var image = qrCode.GetGraphic(100); // Получение изображения QR-кода.

            return image;

        }

    }
}
