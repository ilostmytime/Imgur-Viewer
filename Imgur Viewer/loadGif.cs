using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Imgur_Viewer
{
    class loadGif
    {
        public loadGif(string url)
        {
            BitmapImage gif = new BitmapImage();
            gif.BeginInit();
            gif.UriSource = new Uri(url, UriKind.Absolute);
            gif.EndInit();

        }
    }
}
