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
using System.Xml.Linq;
using Microsoft.Phone.Controls;
using expressage.Models;

namespace expressage.Views
{
    public partial class weiboList : PhoneApplicationPage
    {
        public weiboList()
        {
            InitializeComponent();
        }
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            string user_id = Convert.ToString(NavigationContext.QueryString["user_id"]);
            PageTitle.Text = Convert.ToString(NavigationContext.QueryString["name"]);
            //返回文化传播
            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
            //source请填写您自己微博key
            string url = "http://api.t.sina.com.cn/statuses/user_timeline.xml?source=3808021724&user_id=" + user_id + "&feature=1&page=1&count=20";
            client.DownloadStringAsync(new Uri(url));
        }
        public void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var xml = XElement.Parse(e.Result);
                var videosTemp = (
                    from p in xml.Descendants("status")
                    select new expressage.Models.weiboList()
                    {
                        text = p.Element("text").Value,
                        created_at = p.Element("created_at").Value,
                        thumbnail_pic = p.Element("thumbnail_pic") == null ? "" : p.Element("thumbnail_pic").Value
                    }).ToList();

                weiboListBox.Items.Clear();
                videosTemp.ForEach(p => weiboListBox.Items.Add(p));
                //pop.Visibility = Visibility.Collapsed;
            }
        }

    }
}