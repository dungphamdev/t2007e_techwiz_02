using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Helpers
{
    public class UploadAdapter
    {
        public static void UploadImage(string path, string imageName, string imageBase64)
        {
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
            }

            //set the image path
            string imgPath = Path.Combine(path, imageName);

            byte[] imageBytes = Convert.FromBase64String(imageBase64);

            System.IO.File.WriteAllBytes(imgPath, imageBytes);
        }
    }
}
