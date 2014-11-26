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
using System.Windows.Data;
using System.Globalization;

namespace expressage.Utils
{
    public class DateConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime dt = DateTime.ParseExact(value.ToString(), "ddd MMM dd HH:mm:ss zzz yyyy", provider, DateTimeStyles.AllowWhiteSpaces);

            return DateStringFromNow(dt);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public string DateStringFromNow(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;

            if (span.TotalDays > 1)
            {
                return dt.ToString("MM月dd日");
            }
            else
                if (span.TotalHours > 1)
                {
                    return string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
                }
                else
                    if (span.TotalMinutes > 1)
                    {
                        return
                        string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
                    }
                    else
                        if (span.TotalSeconds >= 1)
                        {
                            return
                            string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
                        }
                        else
                        {
                            return
                            "1秒前";
                        }

        }

    }
}
