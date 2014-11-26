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
using expressage.App_Code;
using System.Threading;
using System.Windows.Threading;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.IO;
using System.IO.IsolatedStorage;

namespace expressage
{
    public partial class ResultPage : PhoneApplicationPage
    {
        private ResultManager resultManager = new ResultManager();
        private DispatcherTimer rueslutListen;// 返回数据监听
        private ProgressIndicator _progressIndicator = new ProgressIndicator();
        private string lastcomname;
        private string comName;
        private string comNum;
        private string firstname;
        private string comCode;
        private void loadresult()
        {
            if (!string.IsNullOrEmpty(comName) && !string.IsNullOrEmpty(comNum))
            {
                resultManager.GetResult(comName, comNum,comCode);
            }
        }

        private HyperlinkButton FindLastVisualChild(DependencyObject obj, string childName, string value)
        {
            HyperlinkButton comgd = null;
            if (obj == null) return null;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is TextBlock && child.GetValue(System.Windows.FrameworkElement.NameProperty).ToString() == childName)
                {
                    TextBlock tb = (TextBlock)child;
                    if (tb.Text == value)
                    {
                        if (tb.Parent is Grid)
                        {
                            Grid gd = (Grid)tb.Parent;
                            if (gd != null && gd.Parent is StackPanel)
                            {
                                StackPanel sp = (StackPanel)gd.Parent;
                                if (sp != null)
                                {
                                    comgd = (HyperlinkButton)sp.FindName("gotosupply");
                                    return comgd;
                                }
                            }
                        }
                    }
                }
                else
                {
                    HyperlinkButton childOfChild = FindLastVisualChild(child, childName, value);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }



        public ResultPage()
        {
            InitializeComponent();
        }

       





        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (NavigationContext.QueryString["comName"] != null && NavigationContext.QueryString["Num"] != null)
            {
                SystemTray.ProgressIndicator = new ProgressIndicator();
                SystemTray.ProgressIndicator.IsIndeterminate = true;
                SystemTray.ProgressIndicator.IsVisible = true;
                SystemTray.ProgressIndicator.Text = "加载中,请稍后";
                comName = NavigationContext.QueryString["comName"];
                comNum = NavigationContext.QueryString["Num"];
               // comCode = NavigationContext.QueryString["code"];
                tbComNanme.Text = comName;
                tbcomNum.Text = comNum;
                Thread loadthread = new Thread(loadresult);
                loadthread.Start();
                rueslutListen = new DispatcherTimer();
                rueslutListen.Interval = new TimeSpan(1000);
                rueslutListen.Tick += new EventHandler(getrueslut);
                rueslutListen.Start();
            }
        }

        private void getrueslut(object sender, EventArgs e)
        {
            if (resultManager.QueriesResult != null)
            {
                lbresult.ItemsSource = resultManager.QueriesResult.Resultdate;
                rueslutListen.Stop();
                SystemTray.ProgressIndicator.IsIndeterminate = false;
                SystemTray.ProgressIndicator.IsVisible = false;
                if (resultManager.QueriesResult.Resultdate != null)
                {        
                    
                    int lastindex = resultManager.QueriesResult.Resultdate.Count - 1;                 
                    if (lastindex >= 0)
                    {
                        Result.ResultData rd = resultManager.QueriesResult.Resultdate[lastindex];
                        lastcomname = rd.Context;
                        Result.ResultData rd1 = resultManager.QueriesResult.Resultdate[0];
                        firstname = rd1.Context;
                    }
                }
            }
        }

        private void gotosupply_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri("http://www.kuaidi100.com/orderIndex.jsp?orderSource=orderIndex");//(" http://wpdevhub.sinaapp.com/");
            wbt.Show();
        }

        private void tbContext_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;//lastcomname
            TextBlock tb1 = (TextBlock)sender;
            if (tb1.Text == firstname)
            {
                IsolatedStorageFile myStore = IsolatedStorageFile.GetUserStoreForApplication();
                myStore.CreateDirectory("FavorFolder");
                StreamWriter writeFile = new StreamWriter(new IsolatedStorageFileStream("FavorFolder\\myFile.txt", FileMode.OpenOrCreate, myStore));
                writeFile.WriteLine(tb1.Text);
                writeFile.Close();
            }
      
            if (tb.Text == lastcomname)
            {
                if (tb.Parent is Grid)
                {
                    Grid gd = (Grid)tb.Parent;
                    if (gd.Parent is StackPanel)
                    {
                        StackPanel sp = (StackPanel)gd.Parent;
                        HyperlinkButton link = (HyperlinkButton)sp.FindName("gotosupply");
                        if (link != null)
                        {
                            link.Visibility = Visibility.Visible;
                        }
                    }

                }  
               
            

            }
        }

        private void gotosupply_Click_1(object sender, RoutedEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri(" http://www.kuaidi100.com");
            wbt.Show();
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
             IsolatedStorageFile myStore = IsolatedStorageFile.GetUserStoreForApplication();
            StreamReader readFile = null;
         
                readFile = new StreamReader(new IsolatedStorageFileStream("FavorFolder\\myFile.txt", FileMode.Open, myStore));
                string fileText = readFile.ReadLine();
                SmsComposeTask sct = new SmsComposeTask();
                sct.Body = fileText;
                sct.Show();readFile.Close();
            
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {

        }

        private void tile_Click(object sender, EventArgs e)
        {
            IsolatedStorageFile myStore = IsolatedStorageFile.GetUserStoreForApplication();
            StreamReader readFile = null;

            readFile = new StreamReader(new IsolatedStorageFileStream("FavorFolder\\myFile.txt", FileMode.Open, myStore));
            string fileText = readFile.ReadLine();
          
            ShellTile TileToFind = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("DefaultTitle=FromTile"));

            if (TileToFind == null)
            {
                StandardTileData NewTileData = new StandardTileData
                {
                    BackgroundImage = new Uri("Green.jpg", UriKind.Relative),
                    Title = tbComNanme.Text,
                    Count = 1,
                    BackTitle = tbcomNum.Text,//"back of tile",
                    BackContent = fileText,//"Welcome to the back of the Tile",
                    BackBackgroundImage = new Uri("Blue.jpg", UriKind.Relative)
                };
                ShellTile.Create(new Uri("/MainPage.xaml", UriKind.Relative), NewTileData);
            }
        }

        private void call_Click(object sender, EventArgs e)
        {
             //String call = tbcomNum.Text;
             //if (tbcomNum.Text == "顺丰快递") { call = "87550569"; }
           
            PhoneCallTask pct = new PhoneCallTask();
            pct.DisplayName = tbComNanme.Text;
            pct.PhoneNumber = "4008111111";
            pct.Show();
        }
    }
}