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
    /// <summary>
    /// 字母索引
    /// </summary>
    public class Index
    {
        public Index() { }
        public Index(string index)
        {
            IndexStr = index;
        }
        
        public string _index;
        public string IndexStr 
        {
            get { return _index; }
            set 
            {
                try
                {
                    char ind = Convert.ToChar(value);
                    _index = "字母" + value;
                }
                catch 
                {
                    if (value != null && value.IndexOf("字母") != -1)
                    {
                        _index = value;
                    }
                }
            } 
        }
    }
}
