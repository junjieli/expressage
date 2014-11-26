using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Xml.Linq;
using expressage.Models;

namespace expressage
{
    public partial class sinaapi : PhoneApplicationPage
    {
        public sinaapi()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            //返回信息
            WebClient clientUser = new WebClient();
            clientUser.DownloadStringCompleted += new DownloadStringCompletedEventHandler(userInfo_DownloadStringCompleted);

            string urlUser = "http://api.t.sina.com.cn/statuses/user_timeline.xml?source=3808021724&user_id=1865244911&feature=1&page=1&count=1";

            clientUser.DownloadStringAsync(new Uri(urlUser));

            //返回信息
            WebClient clientUserMz = new WebClient();
            clientUserMz.DownloadStringCompleted += new DownloadStringCompletedEventHandler(userInfoMz_DownloadStringCompleted);
            string urlUserMz = "http://api.t.sina.com.cn/statuses/user_timeline.xml?source=3808021724&user_id=1792539413&feature=1&page=1&count=1";

            clientUserMz.DownloadStringAsync(new Uri(urlUserMz));

            WebClient clientUser1 = new WebClient();
            clientUser1.DownloadStringCompleted += new DownloadStringCompletedEventHandler(userInfo1_DownloadStringCompleted);

            string urlUser1 = "http://api.t.sina.com.cn/statuses/user_timeline.xml?source=3808021724&user_id=1739731902&feature=1&page=1&count=1";

            clientUser1.DownloadStringAsync(new Uri(urlUser1));

            WebClient clientUser2 = new WebClient();
            clientUser2.DownloadStringCompleted += new DownloadStringCompletedEventHandler(userInfo2_DownloadStringCompleted);
            string urlUser2 = "http://api.t.sina.com.cn/statuses/user_timeline.xml?source=3808021724&user_id=1843040243&feature=1&page=1&count=1";

            clientUser2.DownloadStringAsync(new Uri(urlUser2));

            WebClient clientUser3 = new WebClient();
            clientUser3.DownloadStringCompleted += new DownloadStringCompletedEventHandler(userInfo3_DownloadStringCompleted);
            string urlUser3 = "http://api.t.sina.com.cn/statuses/user_timeline.xml?source=3808021724&user_id=1725540697&feature=1&page=1&count=1";
            clientUser3.DownloadStringAsync(new Uri(urlUser3));

            WebClient clientUser4 = new WebClient();
            clientUser4.DownloadStringCompleted += new DownloadStringCompletedEventHandler(userInfo4_DownloadStringCompleted);
            string urlUser4 = "http://api.t.sina.com.cn/statuses/user_timeline.xml?source=3808021724&user_id=1769592990&feature=1&page=1&count=1";
            clientUser4.DownloadStringAsync(new Uri(urlUser4));




        }
        public void userInfo_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var xml = XElement.Parse(e.Result);
                var videosTemp = (
                    from p in xml.Descendants("user")
                    select new usersInfo()
                    {
                        Title = p.Element("screen_name").Value,
                        Message = p.Element("description").Value,
                        ImageUri = p.Element("profile_image_url").Value,
                        id = p.Element("id").Value,

                    }).ToList();

                ycHubTile.DataContext = videosTemp[0];
            }
        }
        public void userInfoMz_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var xml = XElement.Parse(e.Result);
                var videosTemp = (
                    from p in xml.Descendants("user")
                    select new usersInfo()
                    {
                        Title = p.Element("screen_name").Value,
                        Message = p.Element("description").Value,
                        ImageUri = p.Element("profile_image_url").Value,
                        id = p.Element("id").Value,

                    }).ToList();

                mzHubTile.DataContext = videosTemp[0];
            }
        }
        public void userInfo1_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var xml = XElement.Parse(e.Result);
                var videosTemp = (
                    from p in xml.Descendants("user")
                    select new usersInfo()
                    {
                        Title = p.Element("screen_name").Value,
                        Message = p.Element("description").Value,
                        ImageUri = p.Element("profile_image_url").Value,
                        id = p.Element("id").Value,

                    }).ToList();

                hubTile1.DataContext = videosTemp[0];
            }
        }

        public void userInfo2_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var xml = XElement.Parse(e.Result);
                var videosTemp = (
                    from p in xml.Descendants("user")
                    select new usersInfo()
                    {
                        Title = p.Element("screen_name").Value,
                        Message = p.Element("description").Value,
                        ImageUri = p.Element("profile_image_url").Value,
                        id = p.Element("id").Value,

                    }).ToList();

                hubTile2.DataContext = videosTemp[0];
            }
        }
        public void userInfo3_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var xml = XElement.Parse(e.Result);
                var videosTemp = (
                    from p in xml.Descendants("user")
                    select new usersInfo()
                    {
                        Title = p.Element("screen_name").Value,
                        Message = p.Element("description").Value,
                        ImageUri = p.Element("profile_image_url").Value,
                        id = p.Element("id").Value,

                    }).ToList();

                hubTile3.DataContext = videosTemp[0];
            }
        }
        public void userInfo4_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var xml = XElement.Parse(e.Result);
                var videosTemp = (
                    from p in xml.Descendants("user")
                    select new usersInfo()
                    {
                        Title = p.Element("screen_name").Value,
                        Message = p.Element("description").Value,
                        ImageUri = p.Element("profile_image_url").Value,
                        id = p.Element("id").Value,

                    }).ToList();

                hubTile4.DataContext = videosTemp[0];
            }
        }





        private void ycHubTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            HubTile ht = sender as HubTile;

            this.NavigationService.Navigate(new Uri("/Views/weiboList.xaml?user_id=" + ht.GroupTag + "&name=" + ht.Title, UriKind.Relative));
        }

        private void mzHubTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            HubTile ht = sender as HubTile;

            this.NavigationService.Navigate(new Uri("/Views/weiboList.xaml?user_id=" + ht.GroupTag + "&name=" + ht.Title, UriKind.Relative));
        }

        private void hubTile1_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            HubTile ht = sender as HubTile;

            this.NavigationService.Navigate(new Uri("/Views/weiboList.xaml?user_id=" + ht.GroupTag + "&name=" + ht.Title, UriKind.Relative));
        }

        private void hubTile2_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            HubTile ht = sender as HubTile;

            this.NavigationService.Navigate(new Uri("/Views/weiboList.xaml?user_id=" + ht.GroupTag + "&name=" + ht.Title, UriKind.Relative));
        }

        private void hubTile3_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            HubTile ht = sender as HubTile;

            this.NavigationService.Navigate(new Uri("/Views/weiboList.xaml?user_id=" + ht.GroupTag + "&name=" + ht.Title, UriKind.Relative));
        }

        private void hubTile4_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            HubTile ht = sender as HubTile;

            this.NavigationService.Navigate(new Uri("/Views/weiboList.xaml?user_id=" + ht.GroupTag + "&name=" + ht.Title, UriKind.Relative));
        }
    }
}