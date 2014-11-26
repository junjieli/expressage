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
using Microsoft.Phone.Net.NetworkInformation;
using System.Windows.Media.Imaging;




using Microsoft.Phone.Tasks;
namespace expressage
{
    public partial class MainPage : PhoneApplicationPage
    {
        private CompanyManager comManager = new CompanyManager();
        private HistoryManager hisManager = new HistoryManager();
        private List<Index> ComIndexList = null;//公司索引
        private Dictionary<string, bool> Selindex = new Dictionary<string, bool>();//用于公司名称是否已加载
        private List<string> commonlyCompanyname = null;//常用公司名称
        private string toolkitcomName = null;
        private List<History> hislist = null;
        private int loadindex = 0;

        #region 私有方法
        //查找子控件
        private T FindVisualChild<T>(DependencyObject obj, string childName, int selectindex) where T : DependencyObject
        {
            if (obj == null) return null;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T && child.GetValue(System.Windows.FrameworkElement.NameProperty).ToString() == childName)
                {
                    if (loadindex < selectindex)
                    {
                        loadindex++;
                        FindVisualChild<T>(child, childName, selectindex);
                    }
                    else
                    {
                        return (T)child;
                    }
                }
                else
                {
                    T childOfChild = FindVisualChild<T>(child, childName, selectindex);
                    if (childOfChild != null)
                    {
                        loadindex = 0;
                        return childOfChild;
                    }
                }
            }
            return null;
        }

        private Grid FindValueVisualChild(DependencyObject obj, string childName, string value)
        {
            Grid comgd = null;
            if (obj == null) return null;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is TextBlock && child.GetValue(System.Windows.FrameworkElement.NameProperty).ToString() == childName)
                {
                    TextBlock tb = (TextBlock)child;
                    if (tb.Text == value)
                    {
                        if (tb.Parent != null && tb.Parent is Grid)
                        {
                            Grid gd = (Grid)tb.Parent;
                            Image imgarrow = (Image)gd.FindName("imgindex");
                            if (imgarrow != null)
                            {
                                BitmapImage bm = (BitmapImage)imgarrow.Source;
                                string bmurl = bm.UriSource.ToString();
                                if (Selindex.ContainsKey(value))
                                {
                                    if (Selindex[value])
                                    {
                                        bm.UriSource = new Uri("/expressage;component/Images/btn02.png", UriKind.Relative);
                                    }
                                    else
                                    {
                                        bm.UriSource = new Uri("/expressage;component/Images/btn03.png", UriKind.Relative);
                                    }
                                }
                                else
                                {
                                    bm.UriSource = new Uri("/expressage;component/Images/btn03.png", UriKind.Relative);
                                }
                                imgarrow.Source = bm;
                            }

                            if (gd.Parent != null && gd.Parent is StackPanel)
                            {
                                StackPanel sp = (StackPanel)gd.Parent;
                                object obje = sp.FindName("dgcomname");
                                if (obje != null && obje is Grid)
                                {
                                    comgd = (Grid)obje;
                                    return comgd;
                                }
                            }
                        }
                    }
                }
                else
                {
                    Grid childOfChild = FindValueVisualChild(child, childName, value);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }



        //加载公司名称
        private void ShowComList(Grid gridcomname, char index)
        {
            gridcomname.Children.Clear();
            gridcomname.RowDefinitions.Clear();
            gridcomname.ColumnDefinitions.Clear();
            Dictionary<string, Company> comnamelist = comManager.ShowCompanyByIndex(index);
            int rowcount = comnamelist.Count % 2 == 0 ? comnamelist.Count / 2 : comnamelist.Count / 2 + 1;
            gridcomname.Width = 440;
            // gridcomname.Visibility = Visibility.Visible;
            //添加2列
            gridcomname.ColumnDefinitions.Add(new ColumnDefinition());
            gridcomname.ColumnDefinitions.Add(new ColumnDefinition());
            //添加行
            for (int i = 0; i < rowcount; i++)
            {
                gridcomname.RowDefinitions.Add(new RowDefinition());
            }

            int currentloadcount = 0;//当前加载数
            int currentloadRowcount = 0;
            foreach (string name in comnamelist.Keys)
            {
                //创建弹出式菜单
                ContextMenu contextMenu = new ContextMenu();
                contextMenu.Opened += new RoutedEventHandler(cmcomlist_Opened);
                MenuItem menuItem1 = new MenuItem() { Header = "添加至常用列表" };
                menuItem1.Click += new RoutedEventHandler(miadd_Click);
                contextMenu.Items.Add(menuItem1);
                Button but = new Button();
                ContextMenuService.SetContextMenu(but, contextMenu);
                but.Content = name;
                but.Click += new RoutedEventHandler(Button_Click);
                but.HorizontalAlignment = HorizontalAlignment.Center;
                but.VerticalAlignment = VerticalAlignment.Center;
                but.FontSize = 24;
                but.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                but.BorderThickness = new Thickness(0);
                //选择列
                Grid.SetColumn(but, currentloadcount);
                Grid.SetRow(but, currentloadRowcount);
                if (currentloadcount % 2 == 0) currentloadcount++;
                else
                {
                    currentloadRowcount++;
                    currentloadcount = 0;
                }
                gridcomname.Children.Add(but);
            }
        }

        //加载常用列表
        public void loadcommonlylist()
        {
            if (commonlyCompanyname != null)
            {
                gdcommonlycomName.Children.Clear();
                int currentloadcount = 0;//当前加载数
                int currentloadRowcount = 0;
                foreach (string comName in commonlyCompanyname)
                {
                    ContextMenu contextMenu = new ContextMenu();
                    contextMenu.Opened += new RoutedEventHandler(cmcomlist_Opened);
                    MenuItem menuItem1 = new MenuItem() { Header = "从常用列表中删除" };
                    menuItem1.Click += new RoutedEventHandler(miadel_Click);
                    contextMenu.Items.Add(menuItem1);
                    Button but = new Button();
                    ContextMenuService.SetContextMenu(but, contextMenu);
                    but.Content = comName;
                    but.Click += new RoutedEventHandler(Button_Click);
                    but.HorizontalAlignment = HorizontalAlignment.Center;
                    but.VerticalAlignment = VerticalAlignment.Center;
                    but.FontSize = 24;
                    but.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                    but.BorderThickness = new Thickness(0);
                    //选择列
                    Grid.SetColumn(but, currentloadcount);
                    Grid.SetRow(but, currentloadRowcount);
                    if (currentloadcount % 2 == 0) currentloadcount++;
                    else
                    {
                        currentloadRowcount++;
                        currentloadcount = 0;
                    }
                    gdcommonlycomName.Children.Add(but);
                }
            }
        }

        private bool verificationPostData()
        {
            if (string.IsNullOrEmpty(txtCom.Text.Trim()))
            {
                MessageBox.Show("快递公司名称不能为空,请选择你要查询的快递公司");
                return false;
            }
            else
            {
                if (txtCom.Text.Trim() == "选择快递公司")
                {
                    MessageBox.Show("请选择你要查询的快递公司");
                    return false;
                }
            }
            if (string.IsNullOrEmpty(txtNum.Text.Trim()))
            {
                MessageBox.Show("快递单号不能为空,请输入你要查询的快递单号");
                txtNum.Focus();
                return false;
            }
            else
            {
                if (txtNum.Text.Trim() == "输入运单号")
                {
                    MessageBox.Show("请输入你要查询的快递单号");
                    txtNum.Focus();
                    return false;
                }
            }
            return true;
        }
        #endregion



        public MainPage()
        {
            InitializeComponent();
          //  image1.Source = new BitmapImage(new Uri("http://api.kuaidi100.com/verifyCode?id=82c5ac97b58a2327&com=shunfeng", UriKind.RelativeOrAbsolute));
          //  image2.Source = new BitmapImage(new Uri("http://api.kuaidi100.com/verifyCode?id=82c5ac97b58a2327&com=ems", UriKind.RelativeOrAbsolute));

            ComIndexList = comManager.ShowAvailableIndex();//初始化公司索引
            commonlyCompanyname = comManager.ReadCommonlyCompany();//初始化常用列表
            hislist = hisManager.ReadHistory();
            if (!DeviceNetworkInformation.IsNetworkAvailable)
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.UriSource = new Uri("/expressage;component/Images/myad.png", UriKind.Relative);
                Image myad = new Image();
                myad.Source = bitmapImage;
                //adcontorl.IsEnabled = false;
                //adcontorl.Visibility = Visibility.Collapsed;
                //stpad.Children.Add(myad);
            }
        //    txtPhoneNo.InputScope = new InputScope
        //    {
        //        Names = { new InputScopeName { NameValue = InputScopeNameValue.TelephoneNumber } }
        //    };
        }

        private void txtNum_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtNum.Text.Trim() == "输入运单号")
            {
                txtNum.Text = "";
                txtNum.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            }
        }

        private void txtNum_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtNum.Text.Trim() == "")
            {
                txtNum.Text = "输入运单号";
                txtNum.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            }
        }

        private void btnimg_gotocom_MouseEnter(object sender, MouseEventArgs e)
        {
            mainPivoit.SelectedItem = pitcomlist;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (DeviceNetworkInformation.IsNetworkAvailable)
            {
                if (verificationPostData())
                {
                    NavigationService.Navigate(new Uri(string.Format("/ResultPage.xaml?comName={0}&Num={1}", txtCom.Text.Trim(), txtNum.Text.Trim()), UriKind.Relative));
                }
            }
            else
            {
                MessageBox.Show("网络连接失败,请检查你的网络设置");
            }
        }


        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            //加载索引列表
            if (ComIndexList != null)
            {
                lbcomlist.ItemsSource = ComIndexList;
            }
            if (hislist != null)
            {
                lbHistory.ItemsSource = hislist;
            }


        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Transform form = btn.RenderTransform;
            mainPivoit.SelectedIndex = 0;
            txtCom.Text = btn.Content.ToString();
        }

        private void lbcomlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbcomlist.SelectedIndex != -1)
            {
                string text = ((Index)lbcomlist.SelectedItem).IndexStr;
                char index = text.Trim()[2];
                if (index >= 65 && index <= 91)
                {
                    Grid gridcomname = FindValueVisualChild(lbcomlist, "tbkIndex", text);
                    if (gridcomname != null)
                    {
                        //没加载过
                        if (!Selindex.ContainsKey(text))
                        {
                            ShowComList(gridcomname, index);
                            gridcomname.Visibility = Visibility.Visible;
                            Selindex.Add(text, true);
                        }
                        else
                        {
                            if (Selindex[text] == true)//显示着
                            {
                                gridcomname.Visibility = Visibility.Collapsed;
                                Selindex[text] = false;
                            }
                            else
                            {
                                gridcomname.Visibility = Visibility.Visible;
                                Selindex[text] = true;
                            }
                        }
                    }
                }
            }
            lbcomlist.SelectedIndex = -1;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Selindex.Clear();
            Grid gd = (Grid)sender;
            gd.Children.Clear();
            gd.RowDefinitions.Clear();
            gd.ColumnDefinitions.Clear();
            gd.Visibility = Visibility.Collapsed;
        }

        //添加至常用列表
        private void miadd_Click(object sender, RoutedEventArgs e)
        {
            if (toolkitcomName != null)
            {
                if (commonlyCompanyname != null)
                {
                    if (comManager.AddCommonlyCompany(commonlyCompanyname, toolkitcomName))
                    {
                        commonlyCompanyname = comManager.ReadCommonlyCompany();
                        loadcommonlylist();
                        mainPivoit.SelectedItem = pitsel;
                        toolkitcomName = null;
                    }
                }
            }
        }

        private void cmcomlist_Opened(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = (ContextMenu)sender;
            if (cm.Owner is Button)
            {
                Button bt = (Button)cm.Owner;
                toolkitcomName = bt.Content.ToString();
            }
        }

        private void miadel_Click(object sender, RoutedEventArgs e)
        {
            if (toolkitcomName != null)
            {
                comManager.DelCommonlyCompany(commonlyCompanyname, toolkitcomName);
                commonlyCompanyname = comManager.ReadCommonlyCompany();
                loadcommonlylist();
                toolkitcomName = null;
            }
        }

        private void gdcommonlycomName_Loaded(object sender, RoutedEventArgs e)
        {
            loadcommonlylist();
        }


        private void miDelhis_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            if (mi.Parent is ContextMenu)
            {
                ContextMenu cm = (ContextMenu)mi.Parent;
                if (cm.Owner is StackPanel)
                {
                    StackPanel sp = (StackPanel)cm.Owner;
                    TextBlock btname = (TextBlock)sp.FindName("tbthisName");
                    TextBlock btnNum = (TextBlock)sp.FindName("tbhisNum");
                    if (btname != null && btnNum != null)
                    {
                        History his = new History();
                        his.MailCom = btname.Text.Trim();
                        his.MailNum = btnNum.Text.Trim();
                        hisManager.DelHistory(his);
                        lbHistory.ItemsSource = hisManager.ReadHistory();
                    }
                }
            }
        }

        private void lbHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbHistory.SelectedIndex != -1)
            {
                TextBlock btname = FindVisualChild<TextBlock>(lbHistory, "tbthisName", lbHistory.SelectedIndex);
                TextBlock btnNum = FindVisualChild<TextBlock>(lbHistory, "tbhisNum", lbHistory.SelectedIndex);
                if (btname != null && btnNum != null)
                {
                    if (DeviceNetworkInformation.IsNetworkAvailable)
                    {
                        NavigationService.Navigate(new Uri(string.Format("/ResultPage.xaml?comName={0}&Num={1}", btname.Text.Trim(), btnNum.Text.Trim()), UriKind.Relative));
                    }
                    else
                    {
                        MessageBox.Show("网络连接失败,请检查你的网络设置");
                    }
                }
            }
        }

        private void mainPivoit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mainPivoit.SelectedItem == piHistory)
            {
                hislist = hisManager.ReadHistory();
                lbHistory.ItemsSource = hislist;
            }
        }

        private void miabout_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {                    
        }

        private void btn_1_Click(object sender, RoutedEventArgs e)
        {
          //this.NavigationService.Navigate(new Uri("/sinaapi.xaml, UriKind.Relative));
          // NavigationService.Navigate(new Uri ("/sinaapi.xaml"));

           NavigationService.Navigate(new Uri("/sinaapi.xaml", UriKind.Relative));
        }

        private void sendthing_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();

            wbt.Uri = new Uri("http://www.kuaidi100.com/orderIndex.jsp?orderSource=orderIndex");
            wbt.Show();
        }

        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wbt = new WebBrowserTask();

            wbt.Uri = new Uri("http://www.kuaidi100.com/orderIndex.jsp?orderSource=orderIndex");
            wbt.Show();
        }

       //private void btn1_click(object sender, RoutedEventArgs e)
       // {
       //     image1.Visibility = Visibility.Visible;
       //     txtCom.Text = "顺丰快递";


       // }

       // private void btn2_click(object sender, RoutedEventArgs e)
       // {
       //     image2.Visibility = Visibility.Visible;
       //     txtCom.Text = "ems";
       // }
        
        


    }
}