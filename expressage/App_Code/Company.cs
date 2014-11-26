using System;
using System.Collections.Generic;

namespace expressage.App_Code
{
    /// <summary>
    /// 快递公司
    /// </summary>
    public class Company
    {
        public Company() 
        {
            initCompany();
        }

        private Company(string name,string code,string phone)
        {
            Name = name;
            Code = code;
            Phone = phone;
        }

        //private Dictionary<char, Dictionary<string, Company>> _companyList;
        public Dictionary<char, Dictionary<string, Company>> CompanyList { get; set; }
        
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 提交代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 公司电话
        /// </summary>
        public string Phone { get; set; }




        public Dictionary<char, Dictionary<string, Company>> initCompany()
        {
            string indexsource = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] indexarray = indexsource.ToCharArray();
            Dictionary<char, Dictionary<string, Company>> _companyList = new Dictionary<char, Dictionary<string, Company>>();
            foreach (var c in indexarray)
            {
                switch (c)
                {
                    case 'A':
                        _companyList.Add('A', initA());
                        break;
                    case 'B':
                        _companyList.Add('B', initB());
                        break;
                    case 'C':
                        _companyList.Add('C', initC());
                        break;
                    case 'D':
                        _companyList.Add('D', initD());
                        break;
                    case 'E':
                        _companyList.Add('E', initE());
                        break;
                    case 'F':
                        _companyList.Add('F', initF());
                        break;
                    case 'G':
                        _companyList.Add('G', initG());
                        break;
                    case 'H':
                        _companyList.Add('H', initH());
                        break;
                    case 'I':
                        _companyList.Add('I', null);
                        break;
                    case 'J':
                        _companyList.Add('J', initJ());
                        break;
                    case 'K':
                        _companyList.Add('K', initK());
                        break;
                    case 'L':
                        _companyList.Add('L', initL());
                        break;
                    case 'M':
                        _companyList.Add('M', initM());
                        break;
                    case 'N':
                        _companyList.Add('N', initN());
                        break;
                    case 'O':
                        _companyList.Add('O', null);
                        break;
                    case 'P':
                        _companyList.Add('P', initP());
                        break;
                    case 'Q':
                        _companyList.Add('Q', initQ());
                        break;
                    case 'R':
                        _companyList.Add('R', null);
                        break;
                    case 'S':
                        _companyList.Add('S', initS());
                        break;
                    case 'T':
                        _companyList.Add('T', initT());
                        break;
                    case 'U':
                        _companyList.Add('U', initU());
                        break;
                    case 'V':
                        _companyList.Add('V', null);
                        break;
                    case 'W':
                        _companyList.Add('W', initW());
                        break;
                    case 'X':
                        _companyList.Add('X', initX());
                        break;
                    case 'Y':
                        _companyList.Add('Y', initY());
                        break;
                    case 'Z':
                        _companyList.Add('Z', initZ());
                        break;
                }
            }
            return _companyList;
        }
        
        
        private Dictionary<string, Company> initA()
        {
            Dictionary<string, Company> Co = new Dictionary<string, Company>();
            Co.Add("AAE全球专递", new Company("AAE全球专递", "aae",""));
            Co.Add("安捷快递", new Company("安捷快递", "anjiekuaidi", ""));
            Co.Add("安信达快递", new Company("安信达快递", "anxindakuaixi", ""));
            return Co;
        }

        private Dictionary<string, Company> initB()
        {
            Dictionary<string, Company> Co = new Dictionary<string, Company>();
            Co.Add("百福东方", new Company("百福东方", "baifudongfang", ""));
            Co.Add("彪记快递", new Company("彪记快递", "aae", ""));
            Co.Add("BHT", new Company("BHT", "bht", ""));
            return Co;
        }

        private Dictionary<string, Company> initC()
        {
            Dictionary<string, Company> Co = new Dictionary<string, Company>();
            Co.Add("希伊艾斯快递", new Company("希伊艾斯快递", "cces", ""));
            Co.Add("中国东方", new Company("中国东方", "coe", ""));
            Co.Add("长宇物流", new Company("长宇物流", "changyuwuliu", ""));
            return Co;
        }

        private Dictionary<string, Company> initD()
        {
            Dictionary<string, Company> Co = new Dictionary<string, Company>();
            Co.Add("大田物流", new Company("大田物流", "datianwuliu", ""));
            Co.Add("德邦物流", new Company("德邦物流", "debangwuliu", ""));
            Co.Add("DPEX", new Company("DPEX", "dpex", ""));
            Co.Add("DHL", new Company("DHL", "dhl", ""));
            Co.Add("D速快递", new Company("D速快递", "dsukuaidi", ""));

            return Co;
        }

        private Dictionary<string, Company> initE()
        {
            //return null;
            Dictionary<string, Company> Co = new Dictionary<string, Company>();
            Co.Add("ems", new Company("ems", "ems", ""));
            

            return Co;
        }

        private Dictionary<string, Company> initF()
        {
            Dictionary<string, Company> Co = new Dictionary<string, Company>();
            Co.Add("fedex", new Company("fedex", "fedex", ""));
            Co.Add("飞康达物流", new Company("飞康达物流", "feikangda", ""));
            Co.Add("凤凰快递", new Company("凤凰快递", "fenghuangkuaidi", ""));
            return Co;
        }

        private Dictionary<string, Company> initG()
        {
            Dictionary<string, Company> Co = new Dictionary<string, Company>();
            Co.Add("港中能达物流", new Company("港中能达物流", "ganzhongnengda", ""));
            Co.Add("广东邮政物流", new Company("广东邮政物流", "guangdongyouzhengwuliu", ""));
            Co.Add("GLS快递", new Company("GLS快递", "gls", ""));
            return Co;
        }

        private Dictionary<string, Company> initH()
        {
            Dictionary<string, Company> Co = new Dictionary<string, Company>();
            Co.Add("汇通快运", new Company("汇通快运", "huitongkuaidi", ""));
            Co.Add("恒路物流", new Company("恒路物流", "hengluwuliu", ""));
            Co.Add("华夏龙物流快递", new Company("华夏龙物流快递", "huaxialongwuliu", ""));
            Co.Add("海外环球", new Company("海外环球", "haiwaihuanqiu", ""));
            return Co;
        }

        private Dictionary<string, Company> initJ()
        {
            Dictionary<string, Company> Co = new Dictionary<string, Company>();
            Co.Add("京广速递", new Company("京广速递", "jinguangsudikuaijian", ""));
            Co.Add("急先达", new Company("急先达", "jixianda", ""));
            Co.Add("佳吉物流", new Company("佳吉物流", "jiajiwuliu", ""));
            Co.Add("佳怡物流", new Company("佳怡物流", "jiayiwuliu", ""));
            return Co;
        }

        private Dictionary<string, Company> initK()
        {
            Dictionary<string, Company> Co = new Dictionary<string, Company>();
            Co.Add("快捷速递", new Company("快捷速递", "kuaijiesudi", ""));
            return Co;
        }


        private Dictionary<string, Company> initL()
        {
            Dictionary<string, Company> Co = new Dictionary<string, Company>();
            Co.Add("联昊通物流", new Company("联昊通物流", "lianhaowuliu", ""));
            Co.Add("龙邦物流", new Company("龙邦物流", "longbanwuliu", ""));
            Co.Add("蓝镖快递", new Company("蓝镖快递", "lanbiaokuaidi", ""));
            Co.Add("联邦快递", new Company("联邦快递", "lianbangkuaidi", ""));
            return Co;
        }

        private Dictionary<string, Company> initM()
        {
            Dictionary<string, Company> Co = new Dictionary<string, Company>();
            Co.Add("民航快递", new Company("民航快递", "minghangkuaidi", ""));
            return Co;
        }
        private Dictionary<string, Company> initN()
        {
            Dictionary<string, Company> Co = new Dictionary<string, Company>();
            Co.Add("南京100", new Company("南京100", "nanjing", ""));
            return Co;
        }

        private Dictionary<string, Company> initP()
        {
            Dictionary<string, Company> Co = new Dictionary<string, Company>();
            Co.Add("配思货运", new Company("配思货运", "peisihuoyunkuaidi", ""));
            return Co;
        }

        private Dictionary<string, Company> initQ()
        {
            Dictionary<string, Company> Co = new Dictionary<string, Company>();
            Co.Add("全晨快递", new Company("全晨快递", "quanchenkuaidi", ""));
            Co.Add("全际通物流", new Company("全际通物流", "quanjitong", ""));
            Co.Add("全日通快递", new Company("全日通快递", "quanritongkuaidi", ""));
            Co.Add("全一快递", new Company("全一快递", "quanyikuaidi", ""));
            return Co;
        }

        private Dictionary<string, Company> initS()
        {
            Dictionary<string, Company> Co = new Dictionary<string, Company>();
            Co.Add("三态速递", new Company("三态速递", "santaisudi", ""));
            Co.Add("申通快递", new Company("申通快递", "shentong", ""));
            Co.Add("盛辉物流", new Company("盛辉物流", "shenghuiwuliu", ""));
            Co.Add("速尔物流", new Company("速尔物流", "suer", ""));
            Co.Add("盛丰物流", new Company("盛丰物流", "shengfengwuliu", ""));
            Co.Add("上大物流", new Company("上大物流", "shangda", ""));
            Co.Add("顺丰快递", new Company("顺丰快递", "shunfeng", ""));

            return Co;
        }

        private Dictionary<string, Company> initT()
        {
            Dictionary<string, Company> Co = new Dictionary<string, Company>();
            Co.Add("天地华宇", new Company("天地华宇", "tiandihuayu", ""));
            Co.Add("天天快递", new Company("天天快递", "tiantian", ""));
            Co.Add("TNT", new Company("TNT", "tnt", ""));

            return Co;
        }

        private Dictionary<string, Company> initU()
        {
            Dictionary<string, Company> Co = new Dictionary<string, Company>();
            Co.Add("UPS", new Company("UPS", "ups", ""));

            return Co;
        }

        private Dictionary<string, Company> initW()
        {
            Dictionary<string, Company> Co = new Dictionary<string, Company>();
            Co.Add("万家物流", new Company("万家物流", "wanjiawuliu", ""));
            Co.Add("文捷航空速递", new Company("文捷航空速递", "wenjiesudi", ""));
            Co.Add("伍圆速递", new Company("伍圆速递", "wuyuansudi", ""));
            Co.Add("万象物流", new Company("万象物流", "wanxiangwuliu", ""));

            return Co;
        }




        private Dictionary<string, Company> initX()
        {
            Dictionary<string, Company> Co = new Dictionary<string, Company>();
            Co.Add("新邦物流", new Company("新邦物流", "xinbangwuliu", ""));
            Co.Add("信丰物流", new Company("信丰物流", "xinfengwuliu", ""));
            Co.Add("星晨急便", new Company("星晨急便", "xingchengjibian", ""));
            Co.Add("鑫飞鸿物流快递", new Company("鑫飞鸿物流快递", "xinhongyukuaidi", ""));

            return Co;
        }

        private Dictionary<string, Company> initY()
        {
            Dictionary<string, Company> Co = new Dictionary<string, Company>();
            Co.Add("亚风速递", new Company("亚风速递", "yafengsudi", ""));
            Co.Add("一邦速递", new Company("一邦速递", "yibangwuliu", ""));
            Co.Add("优速物流", new Company("优速物流", "youshuwuliu", ""));
            Co.Add("远成物流", new Company("远成物流", "yuanchengwuliu", ""));
            Co.Add("圆通速递", new Company("圆通速递", "yuantong", ""));
            Co.Add("源伟丰快递", new Company("源伟丰快递", "yuanweifeng", ""));
            Co.Add("元智捷诚快递", new Company("元智捷诚快递", "yuanzhijiecheng", ""));
            Co.Add("越丰物流", new Company("越丰物流", "yuefengwuliu", ""));
            Co.Add("韵达快运", new Company("韵达快运", "yunda", ""));
            Co.Add("源安达", new Company("源安达", "yuananda", ""));
            Co.Add("原飞航物流", new Company("原飞航物流", "yuanfeihangwuliu", ""));
            Co.Add("运通快递", new Company("运通快递", "yuntongkuaidi", ""));
            Co.Add("中国邮政国内包裹", new Company("中国邮政国内包裹", "youzhengguonei", ""));
            
            return Co;
        }

        private Dictionary<string, Company> initZ()
        {
            Dictionary<string, Company> Co = new Dictionary<string, Company>();
            Co.Add("宅急送", new Company("宅急送", "zhaijisong", ""));
            Co.Add("中铁快运", new Company("中铁快运", "zhongtiewuliu", ""));
            Co.Add("中通速递", new Company("中通速递", "zhongtong", ""));
            Co.Add("中邮物流", new Company("中邮物流", "zhongyouwuliu", ""));

            return Co;
        }
    }
}
