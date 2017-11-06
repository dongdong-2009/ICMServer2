using System;
using System.IO;
using System.Windows.Forms;

namespace ICMServer
{
    public partial class DialogSystemSet : Form
    {
        //public string m_filename = "mm.cfg";
        //SetupIni m_ini = new SetupIni();
        public DialogSystemSet()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Config.Instance.AppName              = textBoxSysName.Text;
            Config.Instance.LocalIP              = ipAddressControlSubIP.Text;
            Config.Instance.LocalSubnetMask      = ipAddressControlSubM.Text;
            Config.Instance.LocalGateway         = ipAddressControlGW.Text;
            Config.Instance.OutboundIP           = ipAddressControlOutIP.Text;
            Config.Instance.WeatherOfProvince    = ComboBoxProvince.SelectedIndex;
            Config.Instance.WeatherOfCity        = ComboBoxArea.SelectedIndex;
            Config.Instance.WeatherOfCityName    = QueryCityID(ComboBoxProvince.SelectedIndex, ComboBoxArea.SelectedIndex);
            Config.Instance.SIPServerIP          = textBoxSIP.Text;
            Config.Instance.SIPServerPort        = Int32.Parse(textBoxSipPort.Text);
            Config.Instance.SIPCommunicationPort = Int32.Parse(textBoxSipCommunicationPort.Text);
            if (radioBtnChooseSipServer.Checked)
                Config.Instance.CloudSolution = CloudSolution.SIPServer;
            if (radioBtnChoosePPHook.Checked)
                Config.Instance.CloudSolution = CloudSolution.PPHook;
            this.Close();
        }

        private string QueryCityID(int p1, int p2)
        {
            if (p1 == 29 && p2 == 0)
                return "1665148";
            else if (p1 == 29 && p2 == 1)
                return "1673820";
            else
                return "0000000";
        }

        private void SystemSet_Load(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + "\\" + "mm.cfg"))
            {
                try{ textBoxSysName.Text = Config.Instance.AppName; }
                catch { textBoxSysName.Text = ""; }
                try { ipAddressControlSubIP.Text = Config.Instance.LocalIP; }
                catch { ipAddressControlSubIP.Text = ""; }
                try { ipAddressControlSubM.Text = Config.Instance.LocalSubnetMask; }
                catch { ipAddressControlSubM.Text = ""; }
                try { ipAddressControlGW.Text = Config.Instance.LocalGateway; }
                catch { ipAddressControlGW.Text = ""; }
                try { ipAddressControlOutIP.Text = Config.Instance.OutboundIP; }
                catch { ipAddressControlOutIP.Text = ""; }
                try { ComboBoxProvince.SelectedIndex = Config.Instance.WeatherOfProvince; }
                catch { ComboBoxProvince.SelectedIndex = 0; }
                try { ComboBoxArea.SelectedIndex = Config.Instance.WeatherOfCity; }
                catch { ComboBoxArea.SelectedIndex = 0; }
                try { textBoxSIP.Text = Config.Instance.SIPServerIP; }
                catch { textBoxSIP.Text = ""; }
                try { textBoxSipPort.Text = Config.Instance.SIPServerPort.ToString(); }
                catch { textBoxSipPort.Text = ""; }
                textBoxSipCommunicationPort.Text = Config.Instance.SIPCommunicationPort.ToString();
                switch (Config.Instance.CloudSolution)
                {
                default:
                case CloudSolution.SIPServer:
                    radioBtnChooseSipServer.Checked = true;
                    break;

                case CloudSolution.PPHook:
                    radioBtnChoosePPHook.Checked = true;
                    break;
                }
            }
        }

        private void ComboBoxProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxArea.Items.Clear();
            if (ComboBoxProvince.Text == PlaceName.Municipality)//"直轄市")
            {
                ComboBoxArea.Items.Add(PlaceName.Beijing);//"北京");
                ComboBoxArea.Items.Add(PlaceName.Shanghai);//"上海");
                ComboBoxArea.Items.Add(PlaceName.Tianjin);//"天津");
                ComboBoxArea.Items.Add(PlaceName.Chongqing);//"重慶");
            }
            else if (ComboBoxProvince.Text == PlaceName.Taiwan)//"台灣")
            {
                ComboBoxArea.Items.Add(PlaceName.Taipei);//"台北");
                ComboBoxArea.Items.Add(PlaceName.Kaohsiung);//"高雄");
            }
            else if (ComboBoxProvince.Text == PlaceName.SpecialArea)//"特別行政区")
            {
                ComboBoxArea.Items.Add(PlaceName.HongKong);//"香港");
                ComboBoxArea.Items.Add(PlaceName.Macao);//"澳门");
            }
            else if (ComboBoxProvince.Text == PlaceName.Heilongjiang)//"黑龍江")
            {
                ComboBoxArea.Items.Add(PlaceName.Harbin);//"哈爾濱");
                ComboBoxArea.Items.Add(PlaceName.Qiqihar);//"齊齊哈爾");
                ComboBoxArea.Items.Add(PlaceName.Mudanjiang);//"牡丹江");
                ComboBoxArea.Items.Add(PlaceName.Daqing);//"大慶");
                ComboBoxArea.Items.Add(PlaceName.Yichun);//"伊春");
                ComboBoxArea.Items.Add(PlaceName.Shuangyashan);//"雙鴨山");
                ComboBoxArea.Items.Add(PlaceName.Tsuruoka);//"鶴岡");
                ComboBoxArea.Items.Add(PlaceName.Jixi);//"雞西");
                ComboBoxArea.Items.Add(PlaceName.Jiamusi);//"佳木斯");
                ComboBoxArea.Items.Add(PlaceName.Qitaihe);//"七台河");
                ComboBoxArea.Items.Add(PlaceName.Heihe);//"黑河");
                ComboBoxArea.Items.Add(PlaceName.Suihua);//"綏化");
                ComboBoxArea.Items.Add(PlaceName.Daxinganling);//"大興安嶺");
            }
            else if (ComboBoxProvince.Text == PlaceName.Jilin)//"吉林")
            {
                ComboBoxArea.Items.Add(PlaceName.Changchun);//"長春");
                ComboBoxArea.Items.Add(PlaceName.Yanbian);//"延邊");
                ComboBoxArea.Items.Add(PlaceName.Jilin);//"吉林");
                ComboBoxArea.Items.Add(PlaceName.WhiteMountain);//"白山");
                ComboBoxArea.Items.Add(PlaceName.WhiteCity);//"白城");
                ComboBoxArea.Items.Add(PlaceName.Siping);//"四平");
                ComboBoxArea.Items.Add(PlaceName.Matsubara);//"松原");
                ComboBoxArea.Items.Add(PlaceName.Liaoyuan);//"遼源");
                ComboBoxArea.Items.Add(PlaceName.Daan);//"大安");
                ComboBoxArea.Items.Add(PlaceName.Tonghua);//"通化");
            }
            else if (ComboBoxProvince.Text == PlaceName.Liaoning)//"遼寧")
            {
                ComboBoxArea.Items.Add(PlaceName.Shenyang);//"瀋陽");
                ComboBoxArea.Items.Add(PlaceName.Dalian);//"大连");
                ComboBoxArea.Items.Add(PlaceName.Huludao);//"葫蘆島");
                ComboBoxArea.Items.Add(PlaceName.Panjin);//"盤錦");
                ComboBoxArea.Items.Add(PlaceName.Benxi);//"本溪");
                ComboBoxArea.Items.Add(PlaceName.Fushun);//"撫順");
                ComboBoxArea.Items.Add(PlaceName.Tieling);//"鐵嶺");
                ComboBoxArea.Items.Add(PlaceName.Liaoyang);//"遼陽");
                ComboBoxArea.Items.Add(PlaceName.Yingkou);//"營口");
                ComboBoxArea.Items.Add(PlaceName.Fuxin);//"阜新");
                ComboBoxArea.Items.Add(PlaceName.Chaoyang);//"朝陽");
                ComboBoxArea.Items.Add(PlaceName.Jinzhou);//"錦州");
                ComboBoxArea.Items.Add(PlaceName.Dandong);//"丹東");
                ComboBoxArea.Items.Add(PlaceName.Anshan);//"鞍山");
            }
            else if (ComboBoxProvince.Text == PlaceName.InnerMongolia)//"內蒙古")
            {
                ComboBoxArea.Items.Add(PlaceName.Hohhot);//"呼和浩特");
                ComboBoxArea.Items.Add(PlaceName.Hulunbeier);//"呼倫貝爾");
                ComboBoxArea.Items.Add(PlaceName.Xilinhot);//"錫林浩特");
                ComboBoxArea.Items.Add(PlaceName.Baotou);//"包頭");
                ComboBoxArea.Items.Add(PlaceName.Chifeng);//"赤峰");
                ComboBoxArea.Items.Add(PlaceName.Hailar);//"海拉爾");
                ComboBoxArea.Items.Add(PlaceName.Wuhai);//"烏海");
                ComboBoxArea.Items.Add(PlaceName.Ordos);//"鄂爾多斯");
                ComboBoxArea.Items.Add(PlaceName.Tongliao);//"通遼");
            }
            else if (ComboBoxProvince.Text == PlaceName.Hebei)//"河北")
            {
                ComboBoxArea.Items.Add(PlaceName.Shijiazhuang);//"石家莊");
                ComboBoxArea.Items.Add(PlaceName.Tangshan);//"唐山");
                ComboBoxArea.Items.Add(PlaceName.Zhangjiakou);//"張家口");
                ComboBoxArea.Items.Add(PlaceName.Langfang);//"廊坊");
                ComboBoxArea.Items.Add(PlaceName.Xingtai);//"邢台");
                ComboBoxArea.Items.Add(PlaceName.Handan);//"邯鄲");
                ComboBoxArea.Items.Add(PlaceName.Cangzhou);//"滄州");
                ComboBoxArea.Items.Add(PlaceName.Hengshui);//"衡水");
                ComboBoxArea.Items.Add(PlaceName.Chengde);//"承德");
                ComboBoxArea.Items.Add(PlaceName.Baoding);//"保定");
                ComboBoxArea.Items.Add(PlaceName.Qinhuangdao);//"秦皇島");
            }
            else if (ComboBoxProvince.Text == PlaceName.Henan)//"河南")
            {
                ComboBoxArea.Items.Add(PlaceName.Zhengzhou);//"鄭州");
                ComboBoxArea.Items.Add(PlaceName.Kaifeng);//"開封");
                ComboBoxArea.Items.Add(PlaceName.Luoyang);//"洛陽");
                ComboBoxArea.Items.Add(PlaceName.Pingdingshan);//"平頂山");
                ComboBoxArea.Items.Add(PlaceName.Jiaozuo);//"焦作");
                ComboBoxArea.Items.Add(PlaceName.Hebi);//"鶴壁");
                ComboBoxArea.Items.Add(PlaceName.Xinxiang);//"新鄉");
                ComboBoxArea.Items.Add(PlaceName.Anyang);//"安陽");
                ComboBoxArea.Items.Add(PlaceName.Puyang);//"濮陽");
                ComboBoxArea.Items.Add(PlaceName.Xuchang);//"許昌");
                ComboBoxArea.Items.Add(PlaceName.Luohe);//"漯河");
                ComboBoxArea.Items.Add(PlaceName.Sanmenxia);//"三门峽");
                ComboBoxArea.Items.Add(PlaceName.Nanyang);//"南陽");
                ComboBoxArea.Items.Add(PlaceName.Shangqiu);//"商丘");
                ComboBoxArea.Items.Add(PlaceName.Xinyang);//"信陽");
                ComboBoxArea.Items.Add(PlaceName.Zhoukou);//"周口");
                ComboBoxArea.Items.Add(PlaceName.Zhumadian);//"駐馬店");
            }
            else if (ComboBoxProvince.Text == PlaceName.Shandong)//"山東")
            {
                ComboBoxArea.Items.Add(PlaceName.Jinan);//"濟南");
                ComboBoxArea.Items.Add(PlaceName.Qingdao);//"青島");
                ComboBoxArea.Items.Add(PlaceName.Zibo);//"淄博");
                ComboBoxArea.Items.Add(PlaceName.Weihai);//"威海");
                ComboBoxArea.Items.Add(PlaceName.Qufu);//"曲阜");
                ComboBoxArea.Items.Add(PlaceName.Linyi);//"临沂");
                ComboBoxArea.Items.Add(PlaceName.Yantai);//"煙臺");
                ComboBoxArea.Items.Add(PlaceName.Zaozhuang);//"棗莊");
                ComboBoxArea.Items.Add(PlaceName.Liaocheng);//"聊城");
                ComboBoxArea.Items.Add(PlaceName.Jining);//"濟寧");
                ComboBoxArea.Items.Add(PlaceName.Heze);//"菏澤");
                ComboBoxArea.Items.Add(PlaceName.Taian);//"泰安");
                ComboBoxArea.Items.Add(PlaceName.sunshine);//"日照");
                ComboBoxArea.Items.Add(PlaceName.Dongying);//"東營");
                ComboBoxArea.Items.Add(PlaceName.Texas);//"德州");
                ComboBoxArea.Items.Add(PlaceName.Binzhou);//"濱州");
                ComboBoxArea.Items.Add(PlaceName.Laiwu);//"萊蕪");
                ComboBoxArea.Items.Add(PlaceName.Weifang);//"濰坊");
            }
            else if (ComboBoxProvince.Text == PlaceName.Shanxi)//"山西")
            {
                ComboBoxArea.Items.Add(PlaceName.Taiyuan);//"太原");
                ComboBoxArea.Items.Add(PlaceName.Yangquan);//"陽泉");
                ComboBoxArea.Items.Add(PlaceName.Jincheng);//"晉城");
                ComboBoxArea.Items.Add(PlaceName.Jinzhong);//"晉中");
                ComboBoxArea.Items.Add(PlaceName.Linfen);//"临汾");
                ComboBoxArea.Items.Add(PlaceName.Yuncheng);//"運城");
                ComboBoxArea.Items.Add(PlaceName.Changzhi);//"長治");
                ComboBoxArea.Items.Add(PlaceName.Shuozhou);//"朔州");
                ComboBoxArea.Items.Add(PlaceName.Xinzhou);//"忻州");
                ComboBoxArea.Items.Add(PlaceName.Datong);//"大同");
                ComboBoxArea.Items.Add(PlaceName.Luliang);//"呂梁");
            }
            else if (ComboBoxProvince.Text == PlaceName.Jiangsu)//"江蘇")
            {
                ComboBoxArea.Items.Add(PlaceName.Nanjing);//"南京");
                ComboBoxArea.Items.Add(PlaceName.Suzhou);//"蘇州");
                ComboBoxArea.Items.Add(PlaceName.Kunshan);//"昆山");
                ComboBoxArea.Items.Add(PlaceName.Nantong);//"南通");
                ComboBoxArea.Items.Add(PlaceName.Taicang);//"太倉");
                ComboBoxArea.Items.Add(PlaceName.WuCounty);//"吳縣");
                ComboBoxArea.Items.Add(PlaceName.Xuzhou);//"徐州");
                ComboBoxArea.Items.Add(PlaceName.Yixing);//"宜興");
                ComboBoxArea.Items.Add(PlaceName.Zhenjiang);//"鎮江");
                ComboBoxArea.Items.Add(PlaceName.Huaian);//"淮安");
                ComboBoxArea.Items.Add(PlaceName.Changshu);//"常熟");
                ComboBoxArea.Items.Add(PlaceName.Yancheng);//"鹽城");
                ComboBoxArea.Items.Add(PlaceName.Taizhou);//"泰州");
                ComboBoxArea.Items.Add(PlaceName.Wuxi);//"无錫");
                ComboBoxArea.Items.Add(PlaceName.Lianyungang);//"连云港");
                ComboBoxArea.Items.Add(PlaceName.Yangzhou);//"揚州");
                ComboBoxArea.Items.Add(PlaceName.Changzhou);//"常州");
                ComboBoxArea.Items.Add(PlaceName.Suqian);//"宿遷");
            }
            else if (ComboBoxProvince.Text == PlaceName.Anhui)//"安徽")
            {
                ComboBoxArea.Items.Add(PlaceName.Hefei);//"合肥");
                ComboBoxArea.Items.Add(PlaceName.Bengbu);//"蚌埠");
                ComboBoxArea.Items.Add(PlaceName.Anqing);//"安慶");
                ComboBoxArea.Items.Add(PlaceName.Luan);//"六安");
                ComboBoxArea.Items.Add(PlaceName.Chuzhou);//"滁州");
                ComboBoxArea.Items.Add(PlaceName.MaOnShan);//"馬鞍山");
                ComboBoxArea.Items.Add(PlaceName.Fuyang);//"阜陽");
                ComboBoxArea.Items.Add(PlaceName.Xuancheng);//"宣城");
                ComboBoxArea.Items.Add(PlaceName.Tongling);//"銅陵");
                ComboBoxArea.Items.Add(PlaceName.Huaibei);//"淮北");
                ComboBoxArea.Items.Add(PlaceName.Wuhu);//"蕪湖");
                ComboBoxArea.Items.Add(PlaceName.Bozhou);//"亳州");
                ComboBoxArea.Items.Add(PlaceName.Suzhou2);//"宿州");
                ComboBoxArea.Items.Add(PlaceName.Huainan);//"淮南");
                ComboBoxArea.Items.Add(PlaceName.Chizhou);//"池州");
                ComboBoxArea.Items.Add(PlaceName.Huangshan);//"黃山");
            }
            else if (ComboBoxProvince.Text == PlaceName.Shaanxi)//"陝西")
            {
                ComboBoxArea.Items.Add(PlaceName.Xian);//"西安");
                ComboBoxArea.Items.Add(PlaceName.Hancheng);//"韓城");
                ComboBoxArea.Items.Add(PlaceName.Ankang);//"安康");
                ComboBoxArea.Items.Add(PlaceName.Hanzhong);//"漢中");
                ComboBoxArea.Items.Add(PlaceName.Baoji);//"寶雞");
                ComboBoxArea.Items.Add(PlaceName.Xianyang);//"咸陽");
                ComboBoxArea.Items.Add(PlaceName.Yulin);//"榆林");
                ComboBoxArea.Items.Add(PlaceName.Weinan);//"渭南");
                ComboBoxArea.Items.Add(PlaceName.Shangluo);//"商洛");
                ComboBoxArea.Items.Add(PlaceName.Tongchuan);//"銅川");
                ComboBoxArea.Items.Add(PlaceName.Yanan);//"延安");
            }
            else if (ComboBoxProvince.Text == PlaceName.Ningxia)//"寧夏")
            {
                ComboBoxArea.Items.Add(PlaceName.Yinchuan);//"銀川");
                ComboBoxArea.Items.Add(PlaceName.Guyuan);//"固原");
                ComboBoxArea.Items.Add(PlaceName.Defender);//"中衛");
                ComboBoxArea.Items.Add(PlaceName.Shizuishan);//"石嘴山");
                ComboBoxArea.Items.Add(PlaceName.WuZhong);//"吳忠");
            }
            else if (ComboBoxProvince.Text == PlaceName.Gansu)//"甘肅")
            {
                ComboBoxArea.Items.Add(PlaceName.Lanzhou);//"蘭州");
                ComboBoxArea.Items.Add(PlaceName.silver);//"白銀");
                ComboBoxArea.Items.Add(PlaceName.Qingyang);//"慶陽");
                ComboBoxArea.Items.Add(PlaceName.Jiuquan);//"酒泉");
                ComboBoxArea.Items.Add(PlaceName.Tianshui);//"天水");
                ComboBoxArea.Items.Add(PlaceName.Wuwei);//"武威");
                ComboBoxArea.Items.Add(PlaceName.Zhangye);//"張掖");
                ComboBoxArea.Items.Add(PlaceName.Gannan);//"甘南");
                ComboBoxArea.Items.Add(PlaceName.Linxia);//"临夏");
                ComboBoxArea.Items.Add(PlaceName.Pingliang);//"平涼");
                ComboBoxArea.Items.Add(PlaceName.Dingxi);//"定西");
                ComboBoxArea.Items.Add(PlaceName.Jinchang);//"金昌");
            }
            else if (ComboBoxProvince.Text == PlaceName.Qinghai)//"青海")
            {
                ComboBoxArea.Items.Add(PlaceName.Xining);//"西寧");
                ComboBoxArea.Items.Add(PlaceName.Haibei);//"海北");
                ComboBoxArea.Items.Add(PlaceName.Hercynian);//"海西");
                ComboBoxArea.Items.Add(PlaceName.HuangNan);//"黃南");
                ComboBoxArea.Items.Add(PlaceName.Guoluo);//"果洛");
                ComboBoxArea.Items.Add(PlaceName.Yushu);//"玉樹");
                ComboBoxArea.Items.Add(PlaceName.Haidong);//"海東");
                ComboBoxArea.Items.Add(PlaceName.Hainan);//"海南");
            }
            else if (ComboBoxProvince.Text == PlaceName.Hubei)//"湖北")
            {
                ComboBoxArea.Items.Add(PlaceName.Wuhan);//"武漢");
                ComboBoxArea.Items.Add(PlaceName.Yichang);//"宜昌");
                ComboBoxArea.Items.Add(PlaceName.Huanggang);//"黃岡");
                ComboBoxArea.Items.Add(PlaceName.Enshi);//"恩施");
                ComboBoxArea.Items.Add(PlaceName.Jingzhou);//"荊州");
                ComboBoxArea.Items.Add(PlaceName.Shennongjia);//"神農架");
                ComboBoxArea.Items.Add(PlaceName.Shiyan);//"十堰");
                ComboBoxArea.Items.Add(PlaceName.Xianning);//"咸寧");
                ComboBoxArea.Items.Add(PlaceName.Xiangyang);//"襄陽");
                ComboBoxArea.Items.Add(PlaceName.Xiaogan);//"孝感");
                ComboBoxArea.Items.Add(PlaceName.Suizhou);//"隨州");
                ComboBoxArea.Items.Add(PlaceName.Yellowstone);//"黃石");
                ComboBoxArea.Items.Add(PlaceName.Jingmen);//"荊门");
                ComboBoxArea.Items.Add(PlaceName.Ezhou);//"鄂州");
            }
            else if (ComboBoxProvince.Text == PlaceName.Hunan)//"湖南")
            {
                ComboBoxArea.Items.Add(PlaceName.Changsha);//"長沙");
                ComboBoxArea.Items.Add(PlaceName.Shaoyang);//"邵陽");
                ComboBoxArea.Items.Add(PlaceName.Changde);//"常德");
                ComboBoxArea.Items.Add(PlaceName.Chenzhou);//"郴州");
                ComboBoxArea.Items.Add(PlaceName.Jishou);//"吉首");
                ComboBoxArea.Items.Add(PlaceName.Zhuzhou);//"株洲");
                ComboBoxArea.Items.Add(PlaceName.Loudi);//"婁底");
                ComboBoxArea.Items.Add(PlaceName.Xiangtan);//"湘潭");
                ComboBoxArea.Items.Add(PlaceName.Yiyang);//"益陽");
                ComboBoxArea.Items.Add(PlaceName.Yongzhou);//"永州");
                ComboBoxArea.Items.Add(PlaceName.Yueyang);//"岳陽");
                ComboBoxArea.Items.Add(PlaceName.Hengyang);//"衡陽");
                ComboBoxArea.Items.Add(PlaceName.Huaihua);//"懷化");
                ComboBoxArea.Items.Add(PlaceName.Shaoshan);//"韶山");
                ComboBoxArea.Items.Add(PlaceName.Zhangjiajie);//"張家界");
            }
            else if (ComboBoxProvince.Text == PlaceName.Zhejiang)//"浙江")
            {
                ComboBoxArea.Items.Add(PlaceName.Hangzhou);//"杭州");
                ComboBoxArea.Items.Add(PlaceName.Huzhou);//"湖州");
                ComboBoxArea.Items.Add(PlaceName.Jinhua);//"金華");
                ComboBoxArea.Items.Add(PlaceName.Ningbo);//"寧波");
                ComboBoxArea.Items.Add(PlaceName.Lishui);//"麗水");
                ComboBoxArea.Items.Add(PlaceName.Shaoxing);//"紹興");
                ComboBoxArea.Items.Add(PlaceName.YandangMountain);//"雁盪山");
                ComboBoxArea.Items.Add(PlaceName.Quzhou);//"衢州");
                ComboBoxArea.Items.Add(PlaceName.Jiaxing);//"嘉興");
                ComboBoxArea.Items.Add(PlaceName.Taizhou2);//"台州");
                ComboBoxArea.Items.Add(PlaceName.Zhoushan);//"舟山");
                ComboBoxArea.Items.Add(PlaceName.Wenzhou);//"溫州");
            }
            else if (ComboBoxProvince.Text == PlaceName.Jiangxi)//"江西")
            {
                ComboBoxArea.Items.Add(PlaceName.Nanchang);//"南昌");
                ComboBoxArea.Items.Add(PlaceName.Pingxiang);//"萍鄉");
                ComboBoxArea.Items.Add(PlaceName.Jiujiang);//"九江");
                ComboBoxArea.Items.Add(PlaceName.Shangrao);//"上饒");
                ComboBoxArea.Items.Add(PlaceName.Fuzhou);//"撫州");
                ComboBoxArea.Items.Add(PlaceName.Gian);//"吉安");
                ComboBoxArea.Items.Add(PlaceName.Yingtan);//"鷹潭");
                ComboBoxArea.Items.Add(PlaceName.Yichun2);//"宜春");
                ComboBoxArea.Items.Add(PlaceName.Xinyu);//"新余");
                ComboBoxArea.Items.Add(PlaceName.Jingdezhen);//"景德鎮");
                ComboBoxArea.Items.Add(PlaceName.Ganzhou);//"贛州");
            }
            else if (ComboBoxProvince.Text == PlaceName.Fujian)//"福建")
            {
                ComboBoxArea.Items.Add(PlaceName.Fuzhou2);//"福州");
                ComboBoxArea.Items.Add(PlaceName.Xiamen);//"廈门");
                ComboBoxArea.Items.Add(PlaceName.Longyan);//"龍巖");
                ComboBoxArea.Items.Add(PlaceName.Nanping);//"南平");
                ComboBoxArea.Items.Add(PlaceName.Ningde);//"寧德");
                ComboBoxArea.Items.Add(PlaceName.Putian);//"莆田");
                ComboBoxArea.Items.Add(PlaceName.Quanzhou);//"泉州");
                ComboBoxArea.Items.Add(PlaceName.Sanming);//"三明");
                ComboBoxArea.Items.Add(PlaceName.Zhangzhou);//"漳州");
            }
            else if (ComboBoxProvince.Text == PlaceName.Guizhou)//"貴州")
            {
                ComboBoxArea.Items.Add(PlaceName.Guiyang);//"貴陽");
                ComboBoxArea.Items.Add(PlaceName.Anshun);//"安順");
                ComboBoxArea.Items.Add(PlaceName.Chishui);//"赤水");
                ComboBoxArea.Items.Add(PlaceName.Zunyi);//"遵義");
                ComboBoxArea.Items.Add(PlaceName.Tongren);//"銅仁");
                ComboBoxArea.Items.Add(PlaceName.Liupanshui);//"六盤水");
                ComboBoxArea.Items.Add(PlaceName.Bijie);//"畢節");
                ComboBoxArea.Items.Add(PlaceName.Carey);//"凱里");
                ComboBoxArea.Items.Add(PlaceName.Duyun);//"都勻");
            }
            else if (ComboBoxProvince.Text == PlaceName.Sichuan)//"四川")
            {
                ComboBoxArea.Items.Add(PlaceName.Chengdu);//"成都");
                ComboBoxArea.Items.Add(PlaceName.Luzhou);//"瀘州");
                ComboBoxArea.Items.Add(PlaceName.Neijiang);//"內江");
                ComboBoxArea.Items.Add(PlaceName.Liangshan);//"涼山");
                ComboBoxArea.Items.Add(PlaceName.Aba);//"阿壩");
                ComboBoxArea.Items.Add(PlaceName.Pakistan);//"巴中");
                ComboBoxArea.Items.Add(PlaceName.Guangyuan);//"廣元");
                ComboBoxArea.Items.Add(PlaceName.Leshan);//"樂山");
                ComboBoxArea.Items.Add(PlaceName.Mianyang);//"綿陽");
                ComboBoxArea.Items.Add(PlaceName.Deyang);//"德陽");
                ComboBoxArea.Items.Add(PlaceName.Panzhihua);//"攀枝花");
                ComboBoxArea.Items.Add(PlaceName.Yaan);//"雅安");
                ComboBoxArea.Items.Add(PlaceName.Yibin);//"宜賓");
                ComboBoxArea.Items.Add(PlaceName.Zigong);//"自貢");
                ComboBoxArea.Items.Add(PlaceName.GanziPrefecture);//"甘孜州");
                ComboBoxArea.Items.Add(PlaceName.Dazhou);//"達州");
                ComboBoxArea.Items.Add(PlaceName.Ziyang);//"資陽");
                ComboBoxArea.Items.Add(PlaceName.Guangan);//"廣安");
                ComboBoxArea.Items.Add(PlaceName.Suining);//"遂寧");
            }
            else if (ComboBoxProvince.Text == PlaceName.Guangdong)//"廣東")
            {
                ComboBoxArea.Items.Add(PlaceName.Guangzhou);//"廣州");
                ComboBoxArea.Items.Add(PlaceName.Shenzhen);//"深圳");
                ComboBoxArea.Items.Add(PlaceName.Chaozhou);//"潮州");
                ComboBoxArea.Items.Add(PlaceName.Shaoguan);//"韶开");
                ComboBoxArea.Items.Add(PlaceName.Zhanjiang);//"湛江");
                ComboBoxArea.Items.Add(PlaceName.Huizhou);//"惠州");
                ComboBoxArea.Items.Add(PlaceName.Qingyuan);//"清遠");
                ComboBoxArea.Items.Add(PlaceName.Dongguan);//"東莞");
                ComboBoxArea.Items.Add(PlaceName.Jiangmen);//"江门");
                ComboBoxArea.Items.Add(PlaceName.Maoming);//"茂名");
                ComboBoxArea.Items.Add(PlaceName.Zhaoqing);//"肇慶");
                ComboBoxArea.Items.Add(PlaceName.Shanwei);//"汕尾");
                ComboBoxArea.Items.Add(PlaceName.Heyuan);//"河源");
                ComboBoxArea.Items.Add(PlaceName.Jieyang);//"揭陽");
                ComboBoxArea.Items.Add(PlaceName.Meizhou);//"梅州");
                ComboBoxArea.Items.Add(PlaceName.Zhongshan);//"中山");
                ComboBoxArea.Items.Add(PlaceName.Deqing);//"德慶");
                ComboBoxArea.Items.Add(PlaceName.Yangjiang);//"陽江");
                ComboBoxArea.Items.Add(PlaceName.Yunfu);//"云浮");
            }
            else if (ComboBoxProvince.Text == PlaceName.Guangxi)//"廣西")
            {
                ComboBoxArea.Items.Add(PlaceName.Nanning);//"南寧");
                ComboBoxArea.Items.Add(PlaceName.Guilin);//"桂林");
                ComboBoxArea.Items.Add(PlaceName.Yangshuo);//"陽朔");
                ComboBoxArea.Items.Add(PlaceName.Liuzhou);//"柳州");
                ComboBoxArea.Items.Add(PlaceName.Wuzhou);//"梧州");
                ComboBoxArea.Items.Add(PlaceName.Yulin2);//"玉林");
                ComboBoxArea.Items.Add(PlaceName.Guiping);//"桂平");
                ComboBoxArea.Items.Add(PlaceName.Hezhou);//"賀州");
                ComboBoxArea.Items.Add(PlaceName.Qinzhou);//"欽州");
                ComboBoxArea.Items.Add(PlaceName.Guigang);//"貴港");
                ComboBoxArea.Items.Add(PlaceName.Fangchenggang);//"防城港");
                ComboBoxArea.Items.Add(PlaceName.Baise);//"百色");
                ComboBoxArea.Items.Add(PlaceName.NorthSea);//"北海");
                ComboBoxArea.Items.Add(PlaceName.Hechi);//"河池");
                ComboBoxArea.Items.Add(PlaceName.guest);//"來賓");
                ComboBoxArea.Items.Add(PlaceName.Chongzuo);//"崇左");
            }
            else if (ComboBoxProvince.Text == PlaceName.Yunnan)//"云南")
            {
                ComboBoxArea.Items.Add(PlaceName.Kunming);//"昆明");
                ComboBoxArea.Items.Add(PlaceName.Baoshan);//"保山");
                ComboBoxArea.Items.Add(PlaceName.Chuxiong);//"楚雄");
                ComboBoxArea.Items.Add(PlaceName.Dehong);//"德宏");
                ComboBoxArea.Items.Add(PlaceName.RedRiver);//"紅河");
                ComboBoxArea.Items.Add(PlaceName.Lincang);//"临滄");
                ComboBoxArea.Items.Add(PlaceName.NuRiver);//"怒江");
                ComboBoxArea.Items.Add(PlaceName.Qujing);//"曲靖");
                ComboBoxArea.Items.Add(PlaceName.Simao);//"思茅");
                ComboBoxArea.Items.Add(PlaceName.Wenshan);//"文山");
                ComboBoxArea.Items.Add(PlaceName.Yuxi);//"玉溪");
                ComboBoxArea.Items.Add(PlaceName.Zhaotong);//"昭通");
                ComboBoxArea.Items.Add(PlaceName.Lijiang);//"麗江");
                ComboBoxArea.Items.Add(PlaceName.Dali);//"大理");
            }
            else if (ComboBoxProvince.Text == PlaceName.Hainan2)//"海南")
            {
                ComboBoxArea.Items.Add(PlaceName.Haikou);//"海口");
                ComboBoxArea.Items.Add(PlaceName.Sanya);//"三亞");
                ComboBoxArea.Items.Add(PlaceName.Danzhou);//"儋州");
                ComboBoxArea.Items.Add(PlaceName.Qiongshan);//"瓊山");
                ComboBoxArea.Items.Add(PlaceName.Through);//"通什");
                ComboBoxArea.Items.Add(PlaceName.Wenchang);//"文昌");
            }
            else if (ComboBoxProvince.Text == PlaceName.Xinjiang)//"新疆")
            {
                ComboBoxArea.Items.Add(PlaceName.Urumqi);//"烏魯木齊");
                ComboBoxArea.Items.Add(PlaceName.Altay);//"阿勒泰");
                ComboBoxArea.Items.Add(PlaceName.Aksu);//"阿克蘇");
                ComboBoxArea.Items.Add(PlaceName.Changji);//"昌吉");
                ComboBoxArea.Items.Add(PlaceName.Hami);//"哈密");
                ComboBoxArea.Items.Add(PlaceName.Hotan);//"和田");
                ComboBoxArea.Items.Add(PlaceName.Kashgar);//"喀什");
                ComboBoxArea.Items.Add(PlaceName.Karamay);//"克拉瑪依");
                ComboBoxArea.Items.Add(PlaceName.Shihezi);//"石河子");
                ComboBoxArea.Items.Add(PlaceName.Tacheng);//"塔城");
                ComboBoxArea.Items.Add(PlaceName.Korla);//"库爾勒");
                ComboBoxArea.Items.Add(PlaceName.Turpan);//"吐魯番");
                ComboBoxArea.Items.Add(PlaceName.Yining);//"伊寧");
            }
            else if (ComboBoxProvince.Text == PlaceName.Tibet)//"西藏")
            {
                ComboBoxArea.Items.Add(PlaceName.Lhasa);//"拉薩");
                ComboBoxArea.Items.Add(PlaceName.Ali);//"阿里");
                ComboBoxArea.Items.Add(PlaceName.Qamdo);//"昌都");
                ComboBoxArea.Items.Add(PlaceName.Nagqu);//"那曲");
                ComboBoxArea.Items.Add(PlaceName.Shigatse);//"日喀則");
                ComboBoxArea.Items.Add(PlaceName.Shannan);//"山南");
                ComboBoxArea.Items.Add(PlaceName.Nyingchi);//"林芝");
            }
        }
    }
}
