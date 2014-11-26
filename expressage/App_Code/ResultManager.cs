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
using Microsoft.Phone.Net.NetworkInformation;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace expressage.App_Code
{
    public class ResultManager
    {
        private CompanyManager comManager = new CompanyManager();
        private HistoryManager hisMangaer = new HistoryManager();
        private const string APIKEY = "82c5ac97b58a2327";
        private string ApiUrl = "http://api.kuaidi100.com/api?id=" + APIKEY + "&com={0}&nu={1}&valicode={2}&show=1&muti=1&order=desc";
        private Result _result;
        private string indexsource = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private string comname;
        private string comnum;
        private string valicode;
        public Result QueriesResult { get { return _result; } }

        /// <summary>
        /// 获得返回结果
        /// </summary>
        /// <param name="com">公司代码</param>
        /// <param name="num">运单号</param>
        public void GetResult(string com, string num,string code1)
        {
            char[] indexarray = indexsource.ToCharArray();
            string comcode="";
            if (!string.IsNullOrEmpty(com))
            {
                comname = com;
                foreach (var item in indexarray)
                {
                    Dictionary<string, Company> comlist = comManager.ShowCompanyByIndex(item);
                    if (comlist != null)
                    {
                        foreach (var cn in comlist.Keys)
                        {
                            if (cn.Trim() == com.Trim())
                            {
                                Company comparny = comlist[com];
                                if (comparny != null)
                                {
                                    comcode = comparny.Code;
                                }
                            }
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(comcode) && !string.IsNullOrEmpty(num))
            {
                comnum = num;
                valicode = code1;
                ApiUrl = string.Format(ApiUrl, comcode, num, valicode);
                if (DeviceNetworkInformation.IsNetworkAvailable || DeviceNetworkInformation.IsWiFiEnabled)
                {
                    try
                    {
                        WebClient client = new WebClient();
                        client.OpenReadAsync(new Uri(ApiUrl, UriKind.Absolute));
                        client.OpenReadCompleted += new OpenReadCompletedEventHandler(client_OpenReadCompleted);
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://www.bing.com/");
                            if (myReq.HaveResponse)
                            {
                                MessageBox.Show("服务器连接错误，请稍后再试！");
                            }
                        }
                        catch
                        {
                            ExceptionManager.SendEMail(ex.Message + "\r\nGetResult");
                        }
                    }
                }
            }
        }


        //读取返回信息
        private void client_OpenReadCompleted(object send, OpenReadCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    Stream stream = e.Result;
                    XmlReader reader = XmlReader.Create(stream);
                    Result tempresult = new Result();
                    tempresult.Resultdate = new List<Result.ResultData>();
                    Result.ResultData tempdate = null;
                    bool isnext = true;
                    do
                    {
                        switch (reader.Name)
                        {
                            case "xml":
                                if (reader.NodeType == XmlNodeType.EndElement) isnext = false;
                                reader.Read();
                                break;
                            case "message":
                                tempresult.Message = reader.ReadInnerXml();
                                break;
                            case "status":
                                string tempstatus = reader.ReadInnerXml();
                                try { tempresult.Status = Convert.ToInt32(tempstatus); }
                                catch { }
                                break;
                            case "state":
                                string tempstate = reader.ReadInnerXml();
                                try { tempresult.State = Convert.ToInt32(tempstate); }
                                catch { }
                                break;
                            case "data":
                                if (reader.NodeType == XmlNodeType.Element) tempdate = new Result.ResultData();
                                else tempresult.Resultdate.Add(tempdate);
                                reader.Read();
                                break;
                            case "time":
                                tempdate.Time = Convert.ToDateTime(reader.ReadInnerXml());
                                break;
                            case "context":
                                tempdate.Context = reader.ReadInnerXml();
                                break;
                            default:
                                reader.Read();
                                break;
                        }
                    } while (isnext);
                    if (tempresult.Status == 0 && tempresult.Resultdate.Count == 0)
                    {
                        tempdate = new Result.ResultData();
                        tempdate.Context = tempresult.Message;
                        tempdate.Time = DateTime.Now;
                        tempresult.Resultdate.Add(tempdate);
                    }
                    if (comname != "" && comnum != "")
                    {
                        History his = new History();
                        his.MailCom = comname;
                        his.MailNum = comnum;
                        hisMangaer.WriteHistory(his);
                    }
                    _result = tempresult;
                }
                catch (InvalidCastException)
                {
                    MessageBox.Show("与服务器连接失败，请稍后再试！");
                }
                catch (XmlException)
                {
                    MessageBox.Show("读取数据有误，请重试！");
                }
            }
            else
            {
                MessageBox.Show(e.Error.Message);
            }
        }
    }
}
