using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace expressage.App_Code
{
    public class CompanyManager
    {
        private Company com = new Company();
        private const string COMMONLYCOMPANYFILEPATH = "CommonlyCompany";//常用公司文件路径
       
        
        public CompanyManager()
        {
            com.CompanyList=com.initCompany();
        }


        public List<Index> ShowAvailableIndex()
        {
            List<char> charlist = getAvailableIndex();
            List<Index> indexlist = new List<Index>();
            foreach (char c in charlist)
            {
                indexlist.Add(new Index(c.ToString()));
            }
            return indexlist;
        }


        public List<String> ShowAvailableCompanyName(ref int no)
        {
            List<char> indexlist = getAvailableIndex();
            List<string> comnamelist = new List<string>();
            if (no < indexlist.Count - 1)
            {
                Dictionary<string, Company> comlist = ShowCompanyByIndex(indexlist[no]);
                foreach (var cname in comlist.Keys)
                {
                    comnamelist.Add(cname);
                }
            }
            no++;
            return comnamelist;
        }
        
        
        private List<char> getAvailableIndex()
        {
            string indexsource = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] indexarray = indexsource.ToCharArray();
            List<char> availableindex = new List<char>();
            foreach (var c in indexarray)
            {
                if (com.CompanyList[c] != null)
                {
                    availableindex.Add(c);
                }
            }
            return availableindex;
        }


        /// <summary>
        /// 按索引显示相应的公司列表
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public Dictionary<string, Company> ShowCompanyByIndex(char index)
        {
            Dictionary<string, Company> companylist = com.CompanyList[index];
            if (companylist != null)
            {
                return companylist;
            }
            return null;
        }

        /// <summary>
        /// 或的单个公司信息
        /// </summary>
        /// <param name="companyname">公司名称</param>
        /// <returns></returns>
        public Company GetCompany(string companyname)
        {
            if (companyname != null)
            { 
                //查找对应的公司
                List<char> indexs = getAvailableIndex();
                foreach (char c in indexs)
                {
                    Dictionary<string, Company> companylist = ShowCompanyByIndex(c);
                    foreach (string coname in companylist.Keys)
                    {
                        if (coname == companyname)
                        {
                            return companylist[companyname];
                        }
                    }
                }
            }
            return null;
        }


        public List<string> ReadCommonlyCompany()
        {
            List<string> comlist = new List<string>();
            IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
            if (isf.FileExists(COMMONLYCOMPANYFILEPATH))
            {
                using (IsolatedStorageFileStream istream = new IsolatedStorageFileStream(COMMONLYCOMPANYFILEPATH, FileMode.Open, isf))
                using (StreamReader sr = new StreamReader(istream))
                {
                    string compname = null;
                    while (!string.IsNullOrEmpty((compname = sr.ReadLine())))
                    {
                        comlist.Add(compname);
                    }
                }
            }
            else
            {
                comlist = new List<string>
                {
                   "顺丰快递",
                    "ems",
                    //"宅急送",
                    "圆通速递",
                    "中通速递",                 
                   "韵达快运",              
                  "申通快递"
                };
                WriteCommonlyCompany(comlist);
            }
            return comlist;
        }


        /// <summary>
        /// 添加常用公司文件
        /// </summary>
        /// <param name="comname">公司名称</param>
        /// <returns></returns>
        public bool AddCommonlyCompany(List<string> comnames,string comname)
        {
            foreach (var item in comnames)
            {
                if (comname == item)
                {
                    MessageBox.Show("常用列表中已存在该公司");
                    return false;
                }
            }
            if (comnames.Count >= 10)
            {
                MessageBox.Show("对不起！常用列表已达到上限无法添加，\r\n请删除常用列表中不用的快递公司后再试！");
                return false;
            }
            IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream istream = null;
            using(istream = isf.FileExists(COMMONLYCOMPANYFILEPATH) ? new IsolatedStorageFileStream(COMMONLYCOMPANYFILEPATH, FileMode.Append, isf) : new IsolatedStorageFileStream(COMMONLYCOMPANYFILEPATH, FileMode.Create, isf))
            using (StreamWriter sw = new StreamWriter(istream))
            {
                try { sw.WriteLine(comname); }
                catch { return false; }
            }
            return true;
        }

        /// <summary>
        /// 删除常用公司
        /// </summary>
        /// <param name="comnames">公司名列表</param>
        /// <returns></returns>
        public bool DelCommonlyCompany(List<string> comnames,string name)
        {
            int index=0;
            if (comnames != null && name != null)
            {
                for (int i = 0; i < comnames.Count; i++)
                {
                    if (name == comnames[i])
                    {
                        index = i;
                        break;
                    }
                }
            }
            
            try
            {
                comnames.RemoveAt(index);
                WriteCommonlyCompany(comnames);
            }
            catch
            {
                return false;
            }
            return true;
        }

       
        private void WriteCommonlyCompany(List<string> comnames)
        {
            IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream istream = null;
            using (istream = new IsolatedStorageFileStream(COMMONLYCOMPANYFILEPATH, FileMode.Create, isf))
            using (StreamWriter sw = new StreamWriter(istream))
            {
                foreach (string name in comnames)
                {
                    sw.WriteLine(name);
                }
            }
        }
    }
}
