using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace expressage.App_Code
{
    public class ExceptionManager
    {
        public static void SendEMail(string mess)
        {
            if (MessageBox.Show(":( 程序出错了，是否将错误信息发送！") == MessageBoxResult.OK)
            {
                Microsoft.Phone.Tasks.EmailComposeTask emialTask = new Microsoft.Phone.Tasks.EmailComposeTask();
                emialTask.To = "280369851@qq.com";
                emialTask.Body = mess;
                emialTask.Subject = "expressageEorr";
                emialTask.Show();
            }
        }
    }
}
