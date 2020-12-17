using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Image = SixLabors.ImageSharp.Image;

namespace FashionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShopPage : ContentPage
    {
        public ShopPage()
        {
            InitializeComponent();
            this.BindingContext = this;
        }

        private Timer timer;
        public List<Banner> Banners { get => GetBanners(); }
        public List<Product> CollectionsList { get => GetCollections(); }
        public List<Product> TrendsList { get => GetTrends(); }

        private List<Banner> GetBanners()
        {
            var bannerList = new List<Banner>();
            bannerList.Add(new Banner { Heading = "SUMMER COLLECTION", Message = "40% Discount", Caption = "BEST DISCOUNT THIS SEASON", Image = "classic.png" });
            bannerList.Add(new Banner { Heading = "WOMEN'S CLOTHINGS", Message = "UP TO 50% OFF", Caption = "GET 50% OFF ON EVERY ITEM", Image = "womenCol.png" });
            bannerList.Add(new Banner { Heading = "ELEGANT COLLECTION", Message = "20% Discount", Caption = "UNIQUE COMBINATIONS OF ITEMS", Image = "elegantCol.png" });
            return bannerList;
        }

        private List<Product> GetCollections()
        {
            var trendList = new List<Product>();
            var floral = ImageSource.FromResource("floral.png");
            var satchel = ImageSource.FromResource("satchel.png");
            var leatherBag = ImageSource.FromResource("leatherBag.png");

            Func<Stream> GetStreamFunc(string hash)
            {
                Stream GetImage()
                {
                    var decoder = new Blurhash.ImageSharp.Decoder();
                    var image = decoder.Decode(hash, 150, 150);
                    var ms = new MemoryStream();
                    image.SaveAsPng(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    return ms;
                }

                return GetImage;
            }

            var p1 = new Product { Image = ImageSource.FromStream(GetStreamFunc("KcM%TJxatR?^W=ae%hM{aL")), Name = "Floral Bag + Hat", Price = "$123.50" };
            Task.Run(async () =>
            {
                var rng = new Random();
                await Task.Delay(rng.Next(1000, 4000));
                p1.Image = floral;
            });

            var p2 = new Product { Image = ImageSource.FromStream(GetStreamFunc("KsMsTXt7T0-oWBW;.mayr=")), Name = "Satchel Bag", Price = "$49.99" };
            Task.Run(async () =>
            {
                var rng = new Random();
                await Task.Delay(rng.Next(1000, 4000));
                p2.Image = satchel;
            });

            var p3 = new Product { Image = ImageSource.FromStream(GetStreamFunc("KyLD_g_4-;%0R+ofxZW=NG")), Name = "Leather Bag", Price = "$40.99" };
            Task.Run(async () =>
            {
                var rng = new Random();
                await Task.Delay(rng.Next(1000, 4000));
                p3.Image = leatherBag;
            });

            trendList.Add(p1);
            trendList.Add(p2);
            trendList.Add(p3);
            trendList.Add(p1);
            trendList.Add(p2);
            trendList.Add(p3);
            trendList.Add(p1);
            trendList.Add(p2);
            trendList.Add(p3);
            trendList.Add(p1);
            trendList.Add(p2);
            trendList.Add(p3);
            trendList.Add(p1);
            trendList.Add(p2);
            trendList.Add(p3);
            trendList.Add(p1);
            trendList.Add(p2);
            trendList.Add(p3);
            trendList.Add(p1);
            trendList.Add(p2);
            trendList.Add(p3);

            return trendList;
        }

        private List<Product> GetTrends()
        {
            var colList = new List<Product>();
            colList.Add(new Product { Image = "heeledShoe.png", Name = "Beige Heeled Shoe", Price = "$109.99" });
            colList.Add(new Product { Image = "dressShoe.png", Name = "Shoe + Addons", Price = "$225.99" });
            return colList;
        }

        protected override void OnAppearing()
        {
            timer = new Timer(TimeSpan.FromSeconds(5).TotalMilliseconds) { AutoReset = true, Enabled = true };
            timer.Elapsed += Timer_Elapsed;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            timer?.Dispose();
            base.OnDisappearing();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {

                if (cvBanners.Position == 2)
                {
                    cvBanners.Position = 0;
                    return;
                }

                cvBanners.Position += 1;
            });
        }
    }

    public class Banner
    {
        public string Heading { get; set; }
        public string Message { get; set; }
        public string Caption { get; set; }
        public string Image { get; set; }
    }

    public class Product : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Price { get; set; }

        public ImageSource PlaceHolderImage { get; set; }

        private ImageSource image;
        public ImageSource Image {
            get => image;
            set {
                image = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}