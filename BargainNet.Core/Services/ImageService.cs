using BargainNet.Core.Contracts.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BargainNet.Core.Services
{
    public class ImageService : IImageService
    {
        public string ConvertImgToString(IFormFile image)
        {
            using (var ms = new MemoryStream())
            {
                image.CopyTo(ms);
                var byteImage = ms.ToArray();
                string stringImage = Convert.ToBase64String(byteImage);
                return "data:image/*;base64," + stringImage;
            }
        }
    }
}
