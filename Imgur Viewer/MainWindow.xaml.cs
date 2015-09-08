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
        //ToDo Fix this code
        public string subreddit; //String used to hold user's choice of subreddit
        public string url; //String holding the url for chosen subreddit
        public List<string> imageUrls = new List<string>(); //Array of links loaded from XML of imgur subreddit
        public List<string> imageTitles = new List<string>(); //Array of corresponding titles for the images in imageUrls
        public int currentPage = 0; //Counter of current page on imgur. When this value increases new images are loaded into imageUrls
        public int imageCount = 0; //Counter for the current image
        public static string baseUrl = string.Format(@"https://i.imgur.com/"); //Url used to load direct images. Ex. https://i.imgur.com/fw3f.jpg

        public MainWindow()
        {
            InitializeComponent(); //Create Window
        }

        private void onLoad(object sender, RoutedEventArgs e) //ActionListener for btnLoad, once loaded variables are reset
        {
            imageCount = 0; //Reset current image to 0 
            currentPage = 0; // Set Current page back to 0
            imageUrls.Clear(); //Clear all loaded urls
            imageTitles.Clear(); //Clear all loaded titles
            subreddit = txtLink.Text; //Set subreddit string to the user input
            loadXML(subreddit); //Load/Read subreddit from XML

        }

        private void loadXML(string subreddit) //This method loads images from a subreddit's xml 
        {
            url = string.Format(@"https://imgur.com/r/{0}/page/{1}.xml", subreddit,currentPage); //Create url
            XmlDocument doc = new XmlDocument();
            doc.Load(url); //Load url to read XML
            foreach (XmlNode node in doc.DocumentElement) //Iterate through XML document 
            {
                string hash = node["hash"].InnerText; //Grab and record the hash of current image
                string ext = node["ext"].InnerText; //Find the extension of current image
                string title = node["title"].InnerText; //Find the title of the current image
                bool nsfw = Convert.ToBoolean(node["nsfw"].InnerText); //NSFW test
                
                    if (ext == ".gif") //Skip the loading of gifs until implemented 
                    {
                    continue;
                    }

                imageUrls.Add(baseUrl + hash + ext);//Add hash and extenion to imageUrls array
                imageTitles.Add(title); //same as imageUrls
            }
            try
            {
                loadImage(imageUrls[0], imageCount);//Load the first image
            } catch (ArgumentOutOfRangeException ex) //Catch if subreddit does not exist. No images loaded
            {
                MessageBox.Show("Error", "Subreddit does not exist"); // 
                imageUrls.Clear(); //Clear array
                imageCount = 0; //Reset current image back to 0
            }
        }
        private void loadImage(string url, int imageCount) //This method will load the images into the image view
        {
            var fullImage = url; //Set the image to the url
            BitmapImage bitmap = new BitmapImage(); //Create new bitmap
            bitmap.BeginInit();//Initialize bitmap
            bitmap.UriSource = new Uri(fullImage, UriKind.Absolute); //Set bitmap to the image in the url
            bitmap.EndInit();//End initializing
            imgMain.Source = bitmap; //Set the image view to the loaded picture
            txtTitle.Text = imageTitles[imageCount]; //Set the title 
        }

        private void onNext(object sender, RoutedEventArgs e) //This method is used for btnNext
        {
            try
            {
                imageCount++;//Counter to load next picture
                loadImage(imageUrls[imageCount], imageCount);//load the next picture using new counter
            }
            catch (ArgumentOutOfRangeException ex) //if out of bounds, load new page
            {
                currentPage++;
                loadXML(subreddit);
            }         
        }

        private void onPrevious(object sender, RoutedEventArgs e)//Listener for btnPrevious
        {
            if(imageCount != 0)//If the image counter is not 0 load the previous image
            {
                imageCount--;
                loadImage(imageUrls[imageCount], imageCount);
            }
        }

        private void loadCombo()
        {
           
        }

        private void loop(object sender, RoutedEventArgs e)//Experimental .gif loading
        {
            mediaElement.Position = new TimeSpan(0, 0, 1);
            mediaElement.Play();
        }
    }
}
