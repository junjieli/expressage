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
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using System.IO;

namespace expressage.App_Code
{
    public class HistoryManager
    {
        private string histroyfilename = "mailhistory.log";
        
        public void WriteHistory(History his)
        {
            List<History> hislist = ReadHistory();
            IsolatedStorageFileStream ifilestream = null;
            if (hislist == null)
            {
                hislist = new List<History>();
            }
            else
            {
                foreach (History h in hislist)
                {
                    if (h.MailCom == his.MailCom&&h.MailNum==his.MailNum)
                    {
                        return;
                    }
                }
            }
            IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
            using (ifilestream =isf.FileExists(histroyfilename)?new IsolatedStorageFileStream(histroyfilename, System.IO.FileMode.Append, isf):new IsolatedStorageFileStream(histroyfilename, System.IO.FileMode.Create, isf))
            using (StreamWriter sw = new StreamWriter(ifilestream))
            {
                string tempstr = his.MailCom + "," + his.MailNum;
                sw.WriteLine(tempstr);
            }
        }

        private void WriteHistory(List<History> hislist)
        {
            IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
            using (IsolatedStorageFileStream ifilestream = new IsolatedStorageFileStream(histroyfilename, System.IO.FileMode.Create, isf))
            using (StreamWriter sw = new StreamWriter(ifilestream))
            {
                foreach (var his in hislist)
                {
                    string tempstr = his.MailCom + "," + his.MailNum;
                    sw.WriteLine(tempstr); 
                }
            }
        }

        public void DelHistory(History his)
        {
            try
            {
                int index = -1;
                List<History> hislist = ReadHistory();
                for (int i = 0; i < hislist.Count; i++)
                {
                    if (hislist[i] != null)
                    {
                        if (hislist[i].MailCom == his.MailCom && hislist[i].MailNum == his.MailNum)
                        {
                            index = i;
                            break;
                        }
                    }
                }
                if (index >= 0 && index < hislist.Count)
                {
                    hislist.RemoveAt(index);
                    WriteHistory(hislist);
                }
            }
            catch (Exception)
            {
              
            }
        }

        public List<History> ReadHistory()
        {
            List<History> historylist = new List<History>();
            IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
            if (isf.FileExists(histroyfilename))
            {
                using (IsolatedStorageFileStream ifilestream = new IsolatedStorageFileStream(histroyfilename, System.IO.FileMode.Open, isf))
                using(StreamReader sr=new StreamReader(ifilestream))
                {
                    string historyliststr=null;
                    while ((historyliststr = sr.ReadLine()) != null)
                    {
                        try
                        {
                            string[] hisarray = historyliststr.Split(',');
                            if (hisarray != null && hisarray.Length == 2)
                            {
                                History h = new History();
                                h.MailCom = hisarray[0];
                                h.MailNum = hisarray[1];
                                historylist.Add(h);
                            } 
                        }
                        catch 
                        {
                           
                        }
                    }
                }
            }
            return historylist;
        }
    }
}
