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
using System.Collections.Generic;

namespace expressage.App_Code
{
    public class Result
    {
        
        
        /// <summary>
        /// 消息体
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 数据集合
        /// </summary>
        public List<ResultData> Resultdate { get; set; }
        /// <summary>
        /// 结果状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 快递单的状态
        /// </summary>
        public int State { get; set; }

        public class ResultData 
        {
            /// <summary>
            /// 每条数据的时间 
            /// </summary>
            public DateTime Time { get; set; }
            /// <summary>
            /// 每条数据的状态 
            /// </summary>
            public string Context { get; set; }
        }
    }
}
