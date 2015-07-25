using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace Imgur_Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string subreddit;
        public string url;
        public List<string> imageUrls = new List<string>();
        public List<string> imageTitles = new List<string>();
        public int currentPage = 0;
        public int imageCount = 45;
        public string baseUrl = string.Format(@"https://i.imgur.com/");

        public MainWindow()
        {
            InitializeComponent();

        }

        private void onLoad(object sender, RoutedEventArgs e)
        {
            subreddit = txtLink.Text;
            loadXML(subreddit);

      
        }

        private void loadXML(string subreddit)
        {
            url = string.Format(@"https://imgur.com/r/{0}/page/{1}.xml", subreddit,currentPage);
            XmlDocument doc = new XmlDocument();
            doc.Load(url);
            foreach (XmlNode node in doc.DocumentElement)
            {
                // string item = node.;
                string hash = node["hash"].InnerText;
                string ext = node["ext"].InnerText;
                string title = node["title"].InnerText;

                if (ext != ".gif")
                {
                    imageUrls.Add(baseUrl + hash + ext);
                    imageTitles.Add(title);
                    Console.WriteLine(baseUrl + hash + ext);
                }
            }
            loadImage(imageUrls[0],imageCount);
        }
        private void loadImage(string url, int imageCount)
        {
            var image = new Image();
            var fullImage = url;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fullImage, UriKind.Absolute);
            bitmap.EndInit();
            imgMain.Source = bitmap;
            txtTitle.Text = imageTitles[imageCount];
            Console.WriteLine("Clicked");
        }

        private void onNext(object sender, RoutedEventArgs e)
        {
            try
            {
                loadImage(imageUrls[imageCount], imageCount);
                imageCount++;
            }
            catch(ArgumentOutOfRangeException ex)
            {
                currentPage++;
                loadXML(subreddit);
            }
            
            
            
        }
    }
}
