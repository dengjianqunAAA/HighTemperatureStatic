using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TDModel;
using TDModel.HT;
using TDModel.Product;
using TengDa.Plc.PLC_CommType.PLC_TCP;

namespace PLCHelper
{
    public class PLCConn
    {
        PLCAddressModel plcAddress = new PLCAddressModel();
        /// <summary>
        /// rgv连接  0为A  1为B
        /// </summary>
        Socket[] RgvSocket = new Socket[2];
        /// <summary>
        /// 上料机器人连接
        /// </summary>
        Socket RobotSocket;
        /// <summary>
        /// 炉子连接 i+1 等于炉号
        /// </summary>
        Socket[] stoveSocketlist = new Socket[22];
        /// <summary>
        /// 扫码枪连接 0 电芯扫码枪 1 A扫空夹具扫码枪  2 B扫空夹具扫码枪
        /// </summary>
        Socket[] ScanningSocket = new Socket[3];
        /// <summary>
        /// PLC连接类
        /// </summary>
        OmronPLC_TCP OmronTCP = new OmronPLC_TCP();
        /// <summary>
        /// 扫码枪连接类
        /// </summary>
        KeyenceHelps keyence = new KeyenceHelps();
        /// <summary>
        /// 所有IP地址
        /// </summary>
        List<PLCInfoModel> AllIPlist = new List<PLCInfoModel>();
        /// <summary>
        /// 扫码完成电芯
        /// </summary>
        List<CellInfoModel> scanoverlist = new List<CellInfoModel>();
        /// <summary>
        /// 缓存位电芯
        /// </summary>
        List<CellInfoModel> buffcelllist = new List<CellInfoModel>();
        /// <summary>
        /// 是否运行上料线程
        /// </summary>
        public static volatile bool isLeftRun;
        /// <summary>
        /// 是否运行下料线程
        /// </summary>
        public static volatile bool isRightRun;
        /// <summary>
        /// 是否运行独立线程
        /// </summary>
        public static volatile bool IsIndependentRun;
        /// <summary>
        /// 与PLC交互地址配置文件
        /// </summary>
        TDCommon.XmlHelper xmlhelper = new TDCommon.XmlHelper();
        /// <summary>
        /// 炉子IP
        /// </summary>
        List<PLCInfoModel> stoveIPlist = new List<PLCInfoModel>();
        /// <summary>
        /// RGVIP
        /// </summary>
        List<PLCInfoModel> rgvIPlist = new List<PLCInfoModel>();
        /// <summary>
        /// 上料IP
        /// </summary>
        List<PLCInfoModel> RobotIPlist = new List<PLCInfoModel>();
        /// <summary>
        /// 扫码枪IP
        /// </summary>
        List<PLCInfoModel> Scanninglist = new List<PLCInfoModel>();

        /// <summary>
        /// RGV任务
        /// </summary>
        List<RgvTaskModel> tasklist = new List<RgvTaskModel>();
        /// <summary>
        /// 执行中任务
        /// </summary>
        List<RgvTaskModel> ingtasklist = new List<RgvTaskModel>();
        /// <summary>
        /// 待执行任务
        /// </summary>
        List<RgvTaskModel> stayTasklist = new List<RgvTaskModel>();

        /// <summary>
        /// 夹具管控
        /// </summary>
        TDWebApi.FixtrueControlApi fixControlApi = new TDWebApi.FixtrueControlApi();
        /// <summary>
        /// 夹具信息
        /// </summary>
        TDWebApi.ChuckingApplianceInfoApi chuckapi = new TDWebApi.ChuckingApplianceInfoApi();

        TDWebApi.RgvTaskApi rgvapi = new TDWebApi.RgvTaskApi();
        /// <summary>
        /// PLCIP
        /// </summary>
        TDWebApi.PLCInfoApi plcipapi = new TDWebApi.PLCInfoApi();
        /// <summary>
        /// 炉子详细
        /// </summary>
        TDWebApi.HT.HighTemperatureDetailApi hideatlapi = new TDWebApi.HT.HighTemperatureDetailApi();
        /// <summary>
        /// 电芯API
        /// </summary>
        TDWebApi.CellInfoApi cellapi = new TDWebApi.CellInfoApi();
        /// <summary>
        /// 修改PLC连接sql语句集合
        /// </summary>
        List<string> sqllist = new List<string>();
        /// <summary>
        /// 左边夹具扫码失败次数
        /// </summary>
        int LeftFixScanErr = 0;
        /// <summary>
        /// 右边夹具扫码失败次数
        /// </summary>
        int RightFixScanErr = 0;
        /// <summary>
        /// 左边电芯扫码失败次数
        /// </summary>
        int CellScanErr = 0;
        /// <summary>
        /// 右边电芯扫码失败次数
        /// </summary>
        int rightCellScanErr = 0;
        /// <summary>
        /// 左电芯NG个数 三次报警
        /// </summary>
        int NGNum = 0;
        /// <summary>
        /// 右电芯NG个数 三次报警
        /// </summary>
        int rightNGNum = 0;
        /// <summary>
        /// 计数器
        /// </summary>
        int Conncount = 0;

        /// <summary>
        /// 下料夹具
        /// </summary>
        ChuckingApplianceInfoModel leftBlankFixlist = new ChuckingApplianceInfoModel();
        ChuckingApplianceInfoModel rightBlankFixlist = new ChuckingApplianceInfoModel();

        int LeftCellCount = 0;
        int RightCellCount = 0;

        string lastCellBarCode = string.Empty;
        string[] CellBarCodelist = new string[2];
        List<PLCInfoModel> plcinfolist = new List<PLCInfoModel>();

        /// <summary>
        /// 扫码位夹具 0左1右
        /// </summary>
        ChuckingModel[] chuckbufflist = new ChuckingModel[2];



        /// <summary>
        /// 主方法
        /// </summary>
        public void Data()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(StartRunHandle));
        }

        /// <summary>
        /// 检查是否有PLC断线
        /// </summary>
        void isCheckConn()
        {
            Conncount++;
            string sql = string.Empty;
            #region 炉子连接
            //if (stoveIPlist.Count > 0)
            //{
            //    for (int i = 0; i < stoveIPlist.Count; i++)
            //    {
            //        bool flag = TengDa.Plc.Common.PingHelps.PingIp(stoveIPlist[i].PIIp);//ping  IP
            //        if (flag)
            //        {
            //            bool result = OmronTCP.WriteSingleD(stoveSocketlist[i], plcAddress.HeartBeat, 1);//心跳
            //            if (result)
            //            {
            //                stoveIPlist[i].IsConn = true;

            //                PLCInfoModel model = new PLCInfoModel()
            //                {
            //                    PIId = stoveIPlist[i].PIId,
            //                    PIName = stoveIPlist[i].PIName,
            //                    PIIp = stoveIPlist[i].PIIp,
            //                    PIType = stoveIPlist[i].PIType,
            //                    PIIsConnect = stoveIPlist[i].PIIsConnect,
            //                    ConnTime = stoveIPlist[i].ConnTime,
            //                    isFlag = true
            //                };
            //                plcinfolist.Add(model);
            //            }
            //            else
            //            {
            //                stoveIPlist[i].IsConn = false;

            //                PLCInfoModel model = new PLCInfoModel()
            //                {
            //                    PIId = stoveIPlist[i].PIId,
            //                    PIName = stoveIPlist[i].PIName,
            //                    PIIp = stoveIPlist[i].PIIp,
            //                    PIType = stoveIPlist[i].PIType,
            //                    PIIsConnect = stoveIPlist[i].PIIsConnect,
            //                    ConnTime = stoveIPlist[i].ConnTime,
            //                    isFlag = false
            //                };
            //                plcinfolist.Add(model);
            //            }
            //        }
            //        else
            //        {
            //            stoveIPlist[i].IsConn = false;
            //            PLCInfoModel model = new PLCInfoModel()
            //            {
            //                PIId = stoveIPlist[i].PIId,
            //                PIName = stoveIPlist[i].PIName,
            //                PIIp = stoveIPlist[i].PIIp,
            //                PIType = stoveIPlist[i].PIType,
            //                PIIsConnect = stoveIPlist[i].PIIsConnect,
            //                ConnTime = stoveIPlist[i].ConnTime,
            //                isFlag = false
            //            };
            //            plcinfolist.Add(model);
            //        }
            //    }

            //}
            #endregion

            #region rgv连接
            //if (rgvIPlist.Count > 0)
            //{
            //    for (int i = 0; i < rgvIPlist.Count; i++)
            //    {
            //        bool flag = TengDa.Plc.Common.PingHelps.PingIp(rgvIPlist[i].PIIp);//ping  IP
            //        if (flag)
            //        {
            //            bool result = OmronTCP.WriteSingleD(RgvSocket[i], plcAddress.HeartBeat, 1);
            //            if (result)
            //            {
            //                rgvIPlist[i].IsConn = true;
            //                PLCInfoModel model = new PLCInfoModel()
            //                {
            //                    PIId = rgvIPlist[i].PIId,
            //                    PIName = rgvIPlist[i].PIName,
            //                    PIIp = rgvIPlist[i].PIIp,
            //                    PIType = rgvIPlist[i].PIType,
            //                    PIIsConnect = rgvIPlist[i].PIIsConnect,
            //                    ConnTime = rgvIPlist[i].ConnTime,
            //                    isFlag = true
            //                };
            //                plcinfolist.Add(model);
            //            }
            //            else
            //            {
            //                rgvIPlist[i].IsConn = false;
            //                PLCInfoModel model = new PLCInfoModel()
            //                {
            //                    PIId = rgvIPlist[i].PIId,
            //                    PIName = rgvIPlist[i].PIName,
            //                    PIIp = rgvIPlist[i].PIIp,
            //                    PIType = rgvIPlist[i].PIType,
            //                    PIIsConnect = rgvIPlist[i].PIIsConnect,
            //                    ConnTime = rgvIPlist[i].ConnTime,
            //                    isFlag = false
            //                };
            //                plcinfolist.Add(model);
            //            }
            //        }
            //        else
            //        {
            //            rgvIPlist[i].IsConn = false;
            //            PLCInfoModel model = new PLCInfoModel()
            //            {
            //                PIId = rgvIPlist[i].PIId,
            //                PIName = rgvIPlist[i].PIName,
            //                PIIp = rgvIPlist[i].PIIp,
            //                PIType = rgvIPlist[i].PIType,
            //                PIIsConnect = rgvIPlist[i].PIIsConnect,
            //                ConnTime = rgvIPlist[i].ConnTime,
            //                isFlag = false
            //            };
            //            plcinfolist.Add(model);
            //        }
            //    }
            //}
            #endregion

            #region 上料PLC连接
            //if (RobotIPlist.Count > 0)
            //{
            //    bool flag = TengDa.Plc.Common.PingHelps.PingIp(RobotIPlist[0].PIIp);//ping IP地址
            //    if (flag)
            //    {
            //        bool result = OmronTCP.WriteSingleD(RobotSocket, plcAddress.HeartBeat, 1);
            //        if (result)
            //        {
            //            RobotIPlist[0].IsConn = true;
            //            PLCInfoModel model = new PLCInfoModel()
            //            {
            //                PIId = RobotIPlist[0].PIId,
            //                PIName = RobotIPlist[0].PIName,
            //                PIIp = RobotIPlist[0].PIIp,
            //                PIType = RobotIPlist[0].PIType,
            //                PIIsConnect = RobotIPlist[0].PIIsConnect,
            //                ConnTime = RobotIPlist[0].ConnTime,
            //                isFlag = true
            //            };
            //            plcinfolist.Add(model);
            //        }
            //        else
            //        {
            //            RobotIPlist[0].IsConn = false;
            //            PLCInfoModel model = new PLCInfoModel()
            //            {
            //                PIId = RobotIPlist[0].PIId,
            //                PIName = RobotIPlist[0].PIName,
            //                PIIp = RobotIPlist[0].PIIp,
            //                PIType = RobotIPlist[0].PIType,
            //                PIIsConnect = RobotIPlist[0].PIIsConnect,
            //                ConnTime = RobotIPlist[0].ConnTime,
            //                isFlag = false
            //            };
            //            plcinfolist.Add(model);
            //        }
            //    }
            //    else
            //    {
            //        RobotIPlist[0].IsConn = false;
            //        PLCInfoModel model = new PLCInfoModel()
            //        {
            //            PIId = RobotIPlist[0].PIId,
            //            PIName = RobotIPlist[0].PIName,
            //            PIIp = RobotIPlist[0].PIIp,
            //            PIType = RobotIPlist[0].PIType,
            //            PIIsConnect = RobotIPlist[0].PIIsConnect,
            //            ConnTime = RobotIPlist[0].ConnTime,
            //            isFlag = false
            //        };
            //        plcinfolist.Add(model);
            //    }
            //}
            #endregion
            if (Conncount == 2)//修改数据库连接属性
            {
                Conncount = 0;
                string data = TDCommom.ObjectExtensions.ToJsonString(plcinfolist);
                plcipapi.UpdateConnect(data);
            }
        }

        /// <summary>
        /// PLC连接
        /// </summary>
        /// <returns></returns>
        public bool ConnPLC(string str)
        {
            try
            {
                #region IP分类
                if (str == "A")
                {
                    if (AllIPlist.Count > 0)
                    {
                        stoveIPlist = AllIPlist.Where(x => x.PIType == 1).OrderBy(a => a.PIId).ToList();//炉子
                        rgvIPlist = AllIPlist.Where(x => x.PIType == 3 || x.PIType == 4).ToList();//RGV
                        RobotIPlist = AllIPlist.Where(x => x.PIType == 5).ToList();//上料机器人
                        Scanninglist = AllIPlist.Where(x => x.PIType == 7 || x.PIType == 8 || x.PIType == 9).OrderBy(a => a.PIType).ToList();//扫码枪
                    }
                }
                else if (str == "B")
                {
                    if (AllIPlist.Count > 0)
                    {
                        stoveIPlist = AllIPlist.Where(x => x.PIType == 2).OrderBy(a => a.PIId).ToList();//炉子
                        rgvIPlist = AllIPlist.Where(x => x.PIType == 3 || x.PIType == 4).OrderBy(a => a.PIId).ToList();//RGV
                        RobotIPlist = AllIPlist.Where(x => x.PIType == 6).ToList();//上料机器人
                    }
                }

                #endregion

                #region RGV连接
                //if (rgvIPlist.Count > 0)
                //{
                //    for (int i = 0; i < rgvIPlist.Count; i++)
                //    {
                //        RgvSocket[i] = OmronTCP.ConnectPLC(rgvIPlist[i].PIIp, int.Parse(plcAddress.PLCconnPort), int.Parse(plcAddress.PLCconnTimeOut));
                //        if (RgvSocket[i].Connected)
                //        {
                //            rgvIPlist[i].IsConn = true;
                //            TDCommon.LogManager.WriteLocalLog(string.Format("RGV{0}连接成功！", i + 1));
                //            TDCommom.Logs.Info(string.Format("RGV{0}连接成功！", i + 1));
                //        }
                //        else
                //        {
                //            rgvIPlist[i].IsConn = false;
                //            TDCommon.LogManager.WriteLocalLog(string.Format("RGV{0}连接失败！", i + 1));
                //            TDCommom.Logs.Error(string.Format("RGV{0}连接失败！", i + 1));
                //        }
                //    }
                //}

                #endregion

                #region  上下料连接
                if (RobotIPlist.Count > 0)
                {
                    RobotSocket = OmronTCP.ConnectPLC(RobotIPlist[0].PIIp, int.Parse(plcAddress.PLCconnPort), int.Parse(plcAddress.PLCconnTimeOut));
                    if (RobotSocket.Connected)
                    {
                        RobotIPlist[0].IsConn = true;
                        TDCommon.LogManager.WriteLocalLog(str == "A" ? "上料连接成功！" : "下料连接成功");
                        TDCommom.Logs.Info(string.Format(str == "A" ? "上料连接成功！" : "下料连接成功"));

                    }
                    else
                    {
                        RobotIPlist[0].IsConn = false;
                        TDCommon.LogManager.WriteLocalLog(str == "A" ? "上料连接失败！" : "下料连接失败");
                        TDCommom.Logs.Error(string.Format(str == "A" ? "上料连接失败！" : "下料连接失败"));
                    }
                }
                else
                {
                    TDCommon.LogManager.WriteLocalLog(str == "A" ? "未查询到上料IP！" : "未查询到下料IP");
                    TDCommom.Logs.Error(str == "A" ? "未查询到上料IP！" : "未查询到下料IP");
                }

                #endregion

                #region 炉子连接
                //if (stoveIPlist != null)
                //{
                //    for (int i = 0; i < stoveIPlist.Count; i++)
                //    {
                //        if (stoveIPlist[i].isFlag)
                //        {
                //            stoveSocketlist[i] = OmronTCP.ConnectPLC(stoveIPlist[i].PIIp, int.Parse(plcAddress.PLCconnPort), int.Parse(plcAddress.PLCconnTimeOut));
                //            if (stoveSocketlist[i].Connected == true)
                //            {
                //                stoveIPlist[i].IsConn = true;
                //                TDCommon.LogManager.WriteLocalLog(string.Format("{0}炉{1} PLC连接成功！", str, i + 1));
                //                TDCommom.Logs.Info(string.Format("{0}炉{1} PLC连接成功！", str, i + 1));
                //            }
                //            else
                //            {
                //                stoveIPlist[i].IsConn = false;
                //                TDCommon.LogManager.WriteLocalLog(string.Format("{0}炉{1} PLC连接失败！", str, i + 1));
                //                TDCommom.Logs.Error(string.Format("{0}炉{1} PLC连接失败！", str, i + 1));
                //            }
                //        }
                //        else
                //        {
                //            stoveIPlist[i].IsConn = false;
                //            stoveSocketlist[i] = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //            TDCommon.LogManager.WriteLocalLog(string.Format("{0}炉{1} PLC已禁用！", str, i + 1));
                //            TDCommom.Logs.Info(string.Format("{0}炉{1} PLC已禁用！", str, i + 1));
                //        }
                //    }
                //}
                #endregion

                #region  扫码枪连接
                if (Scanninglist.Count > 0)
                {

                    for (int i = 0; i < Scanninglist.Count; i++)
                    {
                        string scanName = "";
                        if (i == 0)
                        {
                            scanName = "电芯";
                        }
                        else if (i == 1)
                        {
                            scanName = "左夹具";
                        }
                        else if (i == 2)
                        {

                            scanName = "右夹具";
                        }
                        ScanningSocket[i] = keyence.Connect(Scanninglist[i].PIIp, 9004, int.Parse(plcAddress.PLCconnTimeOut));
                        if (ScanningSocket[i].Connected)
                        {
                            
                            Scanninglist[i].IsConn = true;
                            TDCommon.LogManager.WriteLocalLog(string.Format("{0}扫码枪连接成功！", scanName));
                            TDCommom.Logs.Info(string.Format("{0}扫码枪连接成功！", scanName));
                        }
                        else
                        {
                            Scanninglist[i].IsConn = false;
                            TDCommon.LogManager.WriteLocalLog(string.Format("{0}扫码枪连接失败！", scanName));
                            TDCommom.Logs.Error(string.Format("{0}扫码枪连接失败！", scanName));
                        }
                    }
                }

                #endregion

                if (RobotSocket.Connected)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("{0}PLC连接异常 \r\n ErrMsg:{1}", str, ex.Message));
                TDCommon.LogManager.WriteLocalLog(string.Format("{0}PLC连接异常 \r\n ErrMsg:{1}", str, ex.Message));
                return false;
            }
        }

        /// <summary>
        /// 通讯断开重连
        /// </summary>
        /// <param name="str"></param>
        public void ConnReset()
        {

            #region 炉子PLC通讯重连
            //if (stoveIPlist.Count > 0)
            //{
            //    for (int i = 0; i < stoveIPlist.Count; i++)
            //    {
            //        if (!stoveIPlist[i].IsConn)
            //        {
            //            stoveSocketlist[i] = OmronTCP.ConnectPLC(stoveIPlist[i].PIIp, int.Parse(plcAddress.PLCconnPort), int.Parse(plcAddress.PLCconnTimeOut));
            //            if (stoveSocketlist[i].Connected)
            //            {
            //                stoveIPlist[i].IsConn = true;
            //            }
            //            else
            //            {
            //                stoveIPlist[i].IsConn = false;
            //            }
            //        }
            //    }
            //}
            #endregion

            #region RGVPLC通讯重连
            //if (rgvIPlist.Count > 0)
            //{
            //    for (int i = 0; i < rgvIPlist.Count; i++)
            //    {
            //        if (!rgvIPlist[i].IsConn)
            //        {
            //            RgvSocket[i] = OmronTCP.ConnectPLC(rgvIPlist[i].PIIp, int.Parse(plcAddress.PLCconnPort), int.Parse(plcAddress.PLCconnTimeOut));
            //            if (RgvSocket[i].Connected)
            //            {
            //                rgvIPlist[i].IsConn = true;
            //            }
            //            else
            //            {
            //                rgvIPlist[i].IsConn = false;
            //            }
            //        }
            //    }
            //}
            #endregion

            #region 上料PLC重连
            if (RobotIPlist.Count > 0)
            {
                if (!RobotSocket.Connected)
                {
                    string str = isLeftRun == true ? "上料" : "下料";
                    RobotSocket = OmronTCP.ConnectPLC(RobotIPlist[0].PIIp, int.Parse(plcAddress.PLCconnPort), int.Parse(plcAddress.PLCconnTimeOut));
                    if (RobotSocket.Connected)
                    {
                        RobotIPlist[0].IsConn = true;
                        TDCommom.Logs.Info(string.Format("{0}PLC重连成功",str));
                        TDCommon.LogManager.WriteLocalLog(string.Format("{0}PLC重连成功", str));
                    }
                    else
                    {
                        RobotIPlist[0].IsConn = false;
                        TDCommom.Logs.Info(string.Format("{0}PLC重连失败", str));
                        TDCommon.LogManager.WriteLocalLog(string.Format("{0}PLC重连失败", str));
                    }
                }
            }
            #endregion


            if (Scanninglist.Count > 0)
            {
                for (int i = 0; i < Scanninglist.Count; i++)
                {
                    if (!ScanningSocket[i].Connected)
                    {
                        ScanningSocket[i] = keyence.Connect(Scanninglist[i].PIIp, 9004, int.Parse(plcAddress.PLCconnTimeOut));
                        if (ScanningSocket[i].Connected)
                        {
                            Scanninglist[i].IsConn = true;
                            TDCommom.Logs.Info(string.Format("扫码枪{0}重连成功", i + 1));
                            TDCommon.LogManager.WriteLocalLog(string.Format("扫码枪{0}重连成功", i + 1));
                        }
                        else
                        {
                            Scanninglist[i].IsConn = false;
                            TDCommom.Logs.Info(string.Format("扫码枪{0}重连失败", i + 1));
                            TDCommon.LogManager.WriteLocalLog(string.Format("扫码枪{0}重连失败", i + 1));
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 程序初始化
        /// </summary>
        void Initialization(string paramater)
        {
            plcAddress = xmlhelper.ReadXML<PLCAddressModel>("PlcAddress.xml");
            #region 初始化查询PLC所有IP地址
            string plcipres = plcipapi.Getplclist();//查询PLCIP
            if (!string.IsNullOrEmpty(plcipres))
            {
                ApiResultModel resmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(plcipres);
                if (resmodel.Data != null)
                {
                    AllIPlist = TDCommom.ObjectExtensions.FromJsonString<List<PLCInfoModel>>(resmodel.Data.ToString());
                }
            }
            #endregion

            #region 查找状态为10（扫码完成）的电芯  扫码时间降序排序
            string res = cellapi.GetCellInfoBystate(TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
            {
                Data = "10,20"
            }));
            if (!string.IsNullOrEmpty(res))
            {
                ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(res);
                if (apiresmodel.Data != null && apiresmodel.Data != "")
                {
                    List<CellInfoModel> celllist = TDCommom.ObjectExtensions.FromJsonString<List<CellInfoModel>>(apiresmodel.Data.ToString());
                    if (celllist.Count > 0)
                    {
                        scanoverlist = celllist.Where(x => x.State == 10).OrderByDescending(a => a.CellScanTime).ToList();
                        buffcelllist = celllist.Where(x => x.State == 20).OrderByDescending(a => a.CellScanTime).ToList();
                    }
                }
                if (scanoverlist != null && scanoverlist.Count > 0)
                {
                    lastCellBarCode = scanoverlist[0].BarCode;
                }
            }
            #endregion

            #region 查询扫码位夹具
            string result = chuckapi.GetCaiDataByState(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { State = 10 }));
            if (!string.IsNullOrEmpty(result))
            {
                ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(result);
                if (apiresmodel.Data != null)
                {
                    List<ChuckingModel> list = TDCommom.ObjectExtensions.FromJsonString<List<ChuckingModel>>(apiresmodel.Data.ToString());
                    if (list.Count == 2)
                    {
                        chuckbufflist[0] = list.Where(x => x.FixPosition == "A").ToList().FirstOrDefault();
                        chuckbufflist[1] = list.Where(x => x.FixPosition == "B").ToList().FirstOrDefault();
                    }
                    else if (list.Count == 1)
                    {
                        if (list[0].FixPosition == "A")
                        {
                            chuckbufflist[0] = list[0];
                        }
                        else
                        {
                            chuckbufflist[1] = list[0];
                        }
                    }
                }
            }
            #endregion

            #region 扫码位夹具左右计数
            if (chuckbufflist[0] != null)
            {
                if (chuckbufflist[0].CAId != 0)
                {
                    string cellres = cellapi.GetCellInfoByCaId(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = chuckbufflist[0].CAId }));
                    if (string.IsNullOrEmpty(cellres))
                    {
                        ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(cellres);
                        if (apiresmodel.Data != null && apiresmodel.Data != "")
                        {
                            List<CellInfoModel> list = TDCommom.ObjectExtensions.FromJsonString<List<CellInfoModel>>(apiresmodel.Data.ToString());
                            LeftCellCount = list.Count;
                        }
                    }
                }
            }
            if (chuckbufflist[1] != null)
            {
                if (chuckbufflist[1].CAId != 0)
                {
                    string cellres = cellapi.GetCellInfoByCaId(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = chuckbufflist[0].CAId }));
                    if (string.IsNullOrEmpty(cellres))
                    {
                        ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(cellres);
                        if (apiresmodel.Data != null && apiresmodel.Data != "")
                        {
                            List<CellInfoModel> list = TDCommom.ObjectExtensions.FromJsonString<List<CellInfoModel>>(apiresmodel.Data.ToString());
                            RightCellCount = list.Count;
                        }
                    }
                }
            }
            #endregion

            #region 查询任务表 是否有未执行任务
            string taskres = rgvapi.GetRgvTaskInfoByState(TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
            {
                FixState = "0,1",
                FixPosition = paramater
            }));
            if (!string.IsNullOrEmpty(taskres))
            {
                ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(taskres);
                if (apiresmodel.Data != null && apiresmodel.Data != "")
                {
                    tasklist = TDCommom.ObjectExtensions.FromJsonString<List<RgvTaskModel>>(apiresmodel.Data.ToString());
                }
            }
            #endregion
        }

        /// <summary>
        /// 开始运行线程处理方法
        /// </summary>
        /// <param name="state"></param>
        private void StartRunHandle(object state)
        {
            //程序加载

            if (isLeftRun)
            {
                Initialization("A");
                ConnPLC("A");
                StartLeftMethod();
            }
            else if (isRightRun)
            {
                Initialization("B");
                ConnPLC("B");
                StartRightMethod();
            }

            ConnMethod();
        }


        /// <summary>
        /// PLC连接线程
        /// </summary>
        private void ConnMethod()
        {
            //检查PLC是否掉线
            Thread thCheckConn = new Thread(new ThreadStart(() =>
            {
                while (IsIndependentRun)
                {
                    isCheckConn();
                    Thread.Sleep(1000);
                }
            }));
            thCheckConn.IsBackground = true;
            //thCheckConn.Start();

            //PLC断线重连
            Thread thResetConn = new Thread(new ThreadStart(() =>
            {
                while (IsIndependentRun)
                {
                    ConnReset();
                    Thread.Sleep(1000);
                }
            }));
            thResetConn.IsBackground = true;
            thResetConn.Start();
        }

        
        /// <summary>
        /// 启动上料线程
        /// </summary>
        private void StartLeftMethod()
        {
            //空夹具扫码
            Thread thFixScanInfo = new Thread(new ThreadStart(() =>
            {
                while (isLeftRun)
                {
                    EmptyFixtureScan();
                    FixOver();
                    Thread.Sleep(3000);
                }
            }));
            thFixScanInfo.IsBackground = true;
            thFixScanInfo.Start();

            //电芯扫码
            Thread thBarCodeScan = new Thread(new ThreadStart(BarCodeScan));
            thBarCodeScan.IsBackground = true;
            thBarCodeScan.Start();

            //电芯移动
            Thread thCellToBuff = new Thread(new ThreadStart(CellToBuff));
            thCellToBuff.IsBackground = true;
            thCellToBuff.Start();

            //查找RGV任务线程
            Thread thLookupRgvTask = new Thread(new ThreadStart(() =>
            {
                while (isLeftRun)
                {
                    LookupRgvTask("A");
                    Thread.Sleep(5000);
                }
            }));
            thLookupRgvTask.IsBackground = true;
            //thLookupRgvTask.Start();

            //执行RGV任务线程
            Thread thImplementRgvTask = new Thread(new ThreadStart(() =>
            {
                while (isLeftRun)
                {
                    ImplementRgvTask("A");
                    Thread.Sleep(5000);
                }
            }));
            thImplementRgvTask.IsBackground = true;
            //thImplementRgvTask.Start();



        }

        /// <summary>
        /// 空夹具扫码
        /// </summary>
        void EmptyFixtureScan()
        {
            try
            {
                string LeftFixCode = string.Empty;
                string RightFixCode = string.Empty;

                if (RobotIPlist[0].IsConn == true)
                {
                    if (OmronTCP.ReadSingleD(RobotSocket, plcAddress.IsEmptyScaninLeft) == 1)//判断左边是否有空夹具扫码
                    {
                        if (Scanninglist[1].IsConn)//判断扫码枪通讯是否OK
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                LeftFixCode = keyence.Send(ScanningSocket[1], "LON"); //发送指令让扫码枪扫码，获取返回值
                                //LeftFixCode = "ABC0001";
                                if (!string.IsNullOrEmpty(LeftFixCode) && LeftFixCode != "ERROR")
                                {
                                    CheckFixtureCode(LeftFixCode, "A");//验证条码
                                    break;
                                }
                                if (i == 2)
                                {
                                    //夹具扫码失败，失败信号写给上料PLC
                                    OmronTCP.WriteSingleD(RobotSocket, plcAddress.IsFixScanCheckLeft, 2);
                                    //复位左边有空夹具扫码信号
                                    OmronTCP.WriteSingleD(RobotSocket, plcAddress.IsEmptyScaninLeft, 0);
                                }


                            }
                        }
                        else
                        {
                            TDCommom.Logs.Error("夹具扫码枪1通讯异常，请检查！");
                            TDCommon.LogManager.WriteLocalLog("夹具扫码枪1通讯异常，请检查！");
                        }
                    }
                    else if (OmronTCP.ReadSingleD(RobotSocket, plcAddress.IsEmptyScaninRight) == 1)//右边是否有空夹具扫码
                    {
                        if (Scanninglist[1].IsConn)//判断扫码枪通讯是否OK
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                RightFixCode = keyence.Send(ScanningSocket[2], "LON"); //发送指令让扫码枪扫码，获取返回值
                                if (!string.IsNullOrEmpty(RightFixCode) && RightFixCode != "ERROR")
                                {
                                    CheckFixtureCode(RightFixCode, "B");//验证条码
                                    break;
                                }
                                if (i == 2)
                                {
                                    //夹具扫码失败，失败信号写给上料PLC
                                    OmronTCP.WriteSingleD(RobotSocket, plcAddress.IsFixScanCheckRight, 2);
                                    //复位左边有空夹具扫码信号
                                    OmronTCP.WriteSingleD(RobotSocket, plcAddress.IsEmptyScaninRight, 0);
                                }
                            }
                        }
                        else
                        {
                            TDCommom.Logs.Error("夹具扫码枪2通讯异常，请检查！");
                            TDCommon.LogManager.WriteLocalLog("夹具扫码枪2通讯异常，请检查！");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TDCommon.LogManager.WriteLocalLog(string.Format("空夹具扫码异常：ErrorMsg:\r\n {0}", ex.Message));
                TDCommom.Logs.Error(string.Format("空夹具扫码异常：ErrorMsg:\r\n {0}", ex.Message));
            }
        }



        /// <summary>
        /// 夹具是否已满
        /// </summary>
        /// <param name="paramater"></param>
        void FixOver()
        {
            if (RobotIPlist[0].IsConn)
            {
                if (OmronTCP.ReadSingleD(RobotSocket, plcAddress.LeftFixState) == 2)//左夹具已满
                {
                    int leftCellCount = OmronTCP.ReadSingleD(RobotSocket, plcAddress.LeftCellCount);//左夹具电芯数量
                    chuckbufflist[0].CellNumber = leftCellCount;
                    chuckbufflist[0].CAState = int.Parse(TDCommon.SysEnum.FixtureState.CodeBindEnd.ToString("d"));
                    //修改夹具状态
                    chuckapi.UpdateState(TDCommom.ObjectExtensions.ToJsonString(chuckbufflist[0]));

                    //OmronTCP.WriteSingleD(RobotSocket, plcAddress.LeftCellCount, 0);
                    OmronTCP.WriteSingleD(RobotSocket, plcAddress.LeftFixState, 0);

                }
                
                else if (OmronTCP.ReadSingleD(RobotSocket, plcAddress.RightFixState) == 2)//右夹具已满
                {
                    List<ChuckingApplianceInfoModel> Fixlist = new List<ChuckingApplianceInfoModel>();
                    string EmptyFixres = chuckapi.GetInfoByState(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { State = int.Parse(TDCommon.SysEnum.FixtureState.CodeBindEnd.ToString("d")), Data = "A" }));//生产流程结束
                    if (!string.IsNullOrEmpty(EmptyFixres))
                    {
                        ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(EmptyFixres);
                        if (apiresmodel.Data != null && apiresmodel.Data != "")
                        {
                            Fixlist = TDCommom.ObjectExtensions.FromJsonString<List<ChuckingApplianceInfoModel>>(apiresmodel.Data.ToString());
                        }
                    }
                    if (Fixlist == null || Fixlist.Count == 0)
                    {
                        //return;
                        TDCommom.Logs.Info(string.Format("右夹具已满 待RGV取料"));
                        TDCommon.LogManager.WriteLocalLog(string.Format("右夹具已满 待RGV取料"));
                        int rightCellCount = OmronTCP.ReadSingleD(RobotSocket, plcAddress.RightCellCount);//右夹具电芯数量

                        chuckbufflist[0].CellNumber = rightCellCount;
                        chuckbufflist[0].CAState = int.Parse(TDCommon.SysEnum.FixtureState.CodeBindEnd.ToString("d"));
                        //修改夹具状态
                        chuckapi.UpdateState(TDCommom.ObjectExtensions.ToJsonString(chuckbufflist[0]));

                        //OmronTCP.WriteSingleD(RobotSocket, plcAddress.RightCellCount, 0);
                        OmronTCP.WriteSingleD(RobotSocket, plcAddress.RightFixState, 0);
                    }
                }
                int res = OmronTCP.ReadSingleD(RobotSocket, plcAddress.RightFixState);
            }
        }

        /// <summary>
        /// 验证夹具是否可用
        /// </summary>
        /// <param name="FixCode"></param>
        private void CheckFixtureCode(string FixCode, string str)
        {

            string checkaddress = str == "A" ? plcAddress.IsFixScanCheckLeft : plcAddress.IsFixScanCheckRight;//夹具是否可用地址
            string IsEmptyScanaddress = str == "A" ? plcAddress.IsEmptyScaninLeft : plcAddress.IsEmptyScaninRight;//是否有夹具扫码地址
            int fixbuff = str == "A" ? 0 : 1; //夹具缓存位置

            FixtureControlModel model = new FixtureControlModel();
            //判断夹具条码是否为可用状态
            string FixRes = fixControlApi.GetFixtureInfoByCode(FixCode);
            if (!string.IsNullOrEmpty(FixRes))
            {
                ApiResultModel apires = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(FixRes);

                if (apires.Data != null && apires.Data != "")
                {
                    model = TDCommom.ObjectExtensions.FromJsonString<FixtureControlModel>(apires.Data.ToString());
                    if (model != null && model.FixtureState == "0")//如果夹具为正常可使用状态，将夹具状态写给上料PLC
                    {
                        ChuckingModel chuckingmodel = new ChuckingModel()
                        {
                            CABarCode = FixCode,
                            FixPosition = str,
                            ProductModelId = int.Parse(TDCommon.SysInfo.ProductModelId.ToString()),
                            CAState = int.Parse(TDCommon.SysEnum.FixtureState.CodeBinding.ToString("d")),
                            UpdateTime = DateTime.Now,
                            FixScanTime = DateTime.Now,
                            CreateUser = TDCommon.SysInfo.LoginName,
                            Remark1 = "",
                            Remark2 = ""
                        };
                        string chuckres = chuckapi.InsertInfo(TDCommom.ObjectExtensions.ToJsonString(chuckingmodel));//将夹具记录添加到实时夹具信息表

                        if (!string.IsNullOrEmpty(chuckres))
                        {
                            ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(chuckres);
                            ApiModel apimodel = new ApiModel();
                            if (!string.IsNullOrEmpty(apiresmodel.Data.ToString()))
                            {
                                apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(apiresmodel.Data.ToString());
                            }
                            if (apimodel.ResultCode > 0)
                            {
                                chuckingmodel.CAId = apimodel.ResultCode;
                                chuckbufflist[fixbuff] = chuckingmodel;
                                //给上料PLC右夹具OK信号
                                OmronTCP.WriteSingleD(RobotSocket, checkaddress, 1);
                                OmronTCP.WriteSingleD(RobotSocket, IsEmptyScanaddress, 0);
                                TDCommom.Logs.Info(string.Format("{1}夹具：{0}已到位，可以放料！", FixCode, str));
                                TDCommon.LogManager.WriteLocalLog(string.Format("{1}夹具：{0}已到位，可以放料！", FixCode, str));
                            }
                            else
                            {
                                //NG
                                OmronTCP.WriteSingleD(RobotSocket, checkaddress, 2);
                                OmronTCP.WriteSingleD(RobotSocket, IsEmptyScanaddress, 0);
                                TDCommom.Logs.Error(string.Format("{1}夹具任务添加失败：夹具条码：{0}", FixCode, str));
                                //夹具任务添加到数据库失败
                            }
                        }

                        TDCommom.Logs.Info(string.Format("空夹具扫码,夹具当前状态：可用 ,夹具条码：{0}", FixCode));
                        TDCommon.LogManager.WriteLocalLog(string.Format("空夹具扫码,夹具当前状态：可用,夹具条码：{0}", FixCode));

                    }
                    else//夹具为非正常状态，发指令让上料PLC报警
                    {
                        OmronTCP.WriteSingleD(RobotSocket, checkaddress, 2);
                        OmronTCP.WriteSingleD(RobotSocket, IsEmptyScanaddress, 0);

                        TDCommom.Logs.Error(string.Format("空夹具扫码,夹具当前状态异常：不可用\r\n 如夹具状态不符，请进入夹具管控界面修改！"));
                        TDCommon.LogManager.WriteLocalLog(string.Format("空夹具扫码,夹具当前状态异常：不可用\r\n 如夹具状态不符，请进入夹具管控界面修改！"));
                    }
                }

                else
                {
                    string data = TDCommom.ObjectExtensions.ToJsonString(new FixtureControlModel()
                    {
                        FixtureCode = FixCode,
                        FixtureState = "0",
                        Remark = ""
                    });
                    ApiModel apimodel = new ApiModel();
                    //数据库不存在此夹具条码  添加到数据库
                    string Fixresult = fixControlApi.InsertFixture(data);
                    if (!string.IsNullOrEmpty(Fixresult))
                    {
                        ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(Fixresult);
                        if (apiresmodel.Data != null)
                        {
                            apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(apiresmodel.Data.ToString());
                        }
                        if (apimodel.ResultCode > 0)
                        {
                            TDCommom.Logs.Info(string.Format("新夹具：{0}已添加管控！", FixCode));
                            TDCommon.LogManager.WriteLocalLog(string.Format("新夹具：{0}已添加管控！", FixCode));


                            ChuckingModel chuckingmodel = new ChuckingModel()
                            {
                                CABarCode = FixCode,
                                FixPosition = str,
                                ProductModelId = int.Parse(TDCommon.SysInfo.ProductModelId.ToString()),
                                CAState = int.Parse(TDCommon.SysEnum.FixtureState.CodeBinding.ToString("d")),
                                UpdateTime = DateTime.Now,
                                FixScanTime = DateTime.Now,
                                CreateUser = TDCommon.SysInfo.LoginName,
                                Remark1 = "",
                                Remark2 = ""
                            };
                            string chuckres = chuckapi.InsertInfo(TDCommom.ObjectExtensions.ToJsonString(chuckingmodel));//将夹具记录添加到实时夹具信息表

                            if (!string.IsNullOrEmpty(chuckres))
                            {
                                ApiResultModel seapiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(chuckres);
                                ApiModel seapimodel = new ApiModel();
                                if (!string.IsNullOrEmpty(apiresmodel.Data.ToString()))
                                {
                                    seapimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(apiresmodel.Data.ToString());
                                }
                                if (seapimodel.ResultCode > 0)
                                {
                                    chuckingmodel.CAId = seapimodel.ResultCode;
                                    chuckbufflist[fixbuff] = chuckingmodel;
                                    //给上料PLC右夹具OK信号
                                    OmronTCP.WriteSingleD(RobotSocket, checkaddress, 1);
                                    OmronTCP.WriteSingleD(RobotSocket, IsEmptyScanaddress, 0);
                                    TDCommom.Logs.Info(string.Format("{1}夹具：{0}已到位，可以放料！", FixCode, str));
                                    TDCommon.LogManager.WriteLocalLog(string.Format("{1}夹具：{0}已到位，可以放料！", FixCode, str));
                                }
                            }
                        }
                        else
                        {
                            TDCommom.Logs.Error(string.Format("新夹具：{0}添加管控失败！", FixCode));
                            TDCommon.LogManager.WriteLocalLog(string.Format("新夹具：{0}添加管控失败！", FixCode));
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 电芯扫码线程
        /// </summary>
        void BarCodeScan()
        {
            string CellBarCode = string.Empty;
            try
            {
                while (isLeftRun)
                {
                    if (RobotIPlist[0].IsConn)
                    {
                        DateTime startdate = DateTime.Now;
                        //判断是否有电芯扫码信号
                        int tttt = OmronTCP.ReadSingleD(RobotSocket, plcAddress.IsCellScan);
                        if (OmronTCP.ReadSingleD(RobotSocket, plcAddress.IsCellScan) == 1)
                        {
                            //return;
                            for (int i = 0; i < 3; i++)
                            {
                                CellBarCode = keyence.Send(ScanningSocket[0], @"LON");//发送指令让扫码枪扫码 
                                if (!string.IsNullOrEmpty(CellBarCode) && CellBarCode != "ERROR")
                                {
                                    NGNum = 0;
                                    CheckCellBarCode(CellBarCode);
                                    break;
                                }

                                if (i == 2)
                                {
                                    //CellBarCode = "BarCode" + DateTime.Now.ToString("yyyy:mm:hh:dd:ss").Replace(":", "");
                                    //CheckCellBarCode(CellBarCode);
                                    NGNum = NGNum + 1;
                                    OmronTCP.WriteSingleD(RobotSocket, plcAddress.IsCheckCellOK, 2);//扫码三次失败，给上料PLC发送NG指令
                                    OmronTCP.WriteSingleD(RobotSocket, plcAddress.IsCellScan, 0);//复位电芯扫码信号s
                                    TDCommom.Logs.Info(string.Format("电芯扫码NG!"));
                                    TDCommon.LogManager.WriteLocalLog(string.Format("电芯扫码NG!"));
                                }
                            }
                            TimeSpan ts = DateTime.Now - startdate;
                            TDCommom.Logs.Info(string.Format("电芯扫码流程时间：{0}", ts.TotalMilliseconds.ToString()));
                            TDCommon.LogManager.WriteLocalLog(string.Format("电芯扫码流程时间：{0}", ts.TotalMilliseconds.ToString()));
                        }

                        if (NGNum == 3) //连续扫码NG数量
                        {
                            OmronTCP.WriteSingleD(RobotSocket, plcAddress.ScanContinuityNG, 1);//连续三个扫码NG
                            NGNum = 0;
                        }
                    }
                    Thread.Sleep(500);
                }
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("电芯扫码线程异常：ErrorMsg:\r\n{0}", ex.Message));
                Thread.Sleep(1000);
            }
        }



        /// <summary>
        /// 校验电芯是否OK
        /// </summary>
        /// <param name="CellBarCode"></param>
        private void CheckCellBarCode(string CellBarCode)
        {
            string cellres = cellapi.GetCellInfoByCode(TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
            {
                Data = CellBarCode
            }));

            if (!string.IsNullOrEmpty(cellres))
            {
                ApiResultModel cellapiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(cellres);
                if (cellapiresmodel.Data != null && cellapiresmodel.Data != "")
                {
                    CellInfoModel cellinfomodel = TDCommom.ObjectExtensions.FromJsonString<CellInfoModel>(cellapiresmodel.Data.ToString());
                    if (cellinfomodel != null && cellinfomodel.CellInfoId > 0)
                    {
                        CellInfoModel cellmodel = new CellInfoModel()
                        {
                            CellInfoId = cellinfomodel.CellInfoId,
                            State = 10
                        };
                        string res = cellapi.UpdateCellState(TDCommom.ObjectExtensions.ToJsonString(cellmodel));
                        scanoverlist.Add(cellinfomodel);
                        TDCommom.Logs.Info(string.Format("电芯重复扫码OK：BarCode: {0}", CellBarCode));
                        TDCommon.LogManager.WriteLocalLog(string.Format("电芯重复扫码OK：BarCode: {0}", CellBarCode));
                        OmronTCP.WriteSingleD(RobotSocket, plcAddress.IsCheckCellOK, 1);//给PLC电芯OK信号
                        OmronTCP.WriteSingleD(RobotSocket, plcAddress.IsCellScan, 0);//复位电芯扫码信号
                    }
                    //else
                    //{
                    //    if (1 == 1)//校验MFG 校验成功 OK信号写给上料PLC
                    //    {
                    //        string data = TDCommom.ObjectExtensions.ToJsonString(new CellInfoModel()
                    //        {
                    //            State = 10,
                    //            BarCode = CellBarCode,
                    //            CellScanTime = DateTime.Now
                    //        });
                    //        string res = cellapi.InsertCellCount(data);//条码添加到数据库
                    //        if (!string.IsNullOrEmpty(res))
                    //        {
                    //            ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(res);
                    //            if (apiresmodel.Data != null)
                    //            {
                    //                ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(apiresmodel.Data.ToString());
                    //                if (apimodel.ResultCode > 0)
                    //                {
                    //                    CellInfoModel model = new CellInfoModel()
                    //                    {
                    //                        CellInfoId = apimodel.ResultCode,
                    //                        BarCode = CellBarCode
                    //                    };
                    //                    scanoverlist.Clear();
                    //                    scanoverlist.Add(model);
                    //                    TDCommom.Logs.Info(string.Format("电芯扫码OK：BarCode: {0}", CellBarCode));
                    //                    TDCommon.LogManager.WriteLocalLog(string.Format("电芯扫码OK：BarCode: {0}", CellBarCode));
                    //                    OmronTCP.WriteSingleD(RobotSocket, plcAddress.IsCheckCellOK, 1);//给PLC电芯OK信号
                    //                    OmronTCP.WriteSingleD(RobotSocket, plcAddress.IsCellScan, 0);//复位电芯扫码信号
                    //                }
                    //            }
                    //        }
                    //    }
                    //    else//校验失败
                    //    {
                    //        TDCommom.Logs.Error(string.Format("电芯校验NG：BarCode: {0}", CellBarCode));
                    //        TDCommon.LogManager.WriteLocalLog(string.Format("电芯校验NG：BarCode: {0}", CellBarCode));
                    //        OmronTCP.WriteSingleD(RobotSocket, plcAddress.IsCheckCellOK, 2);//给PLC电芯NG信号
                    //        OmronTCP.WriteSingleD(RobotSocket, plcAddress.IsCellScan, 0);//复位电芯扫码信号

                    //    }
                    //}
                }
                else
                {
                    if (1 == 1)//校验MFG 校验成功 OK信号写给上料PLC
                    {
                        string data = TDCommom.ObjectExtensions.ToJsonString(new CellInfoModel()
                        {
                            State = 10,
                            BarCode = CellBarCode,
                            CellScanTime = DateTime.Now
                        });
                        string res = cellapi.InsertCellCount(data);//条码添加到数据库
                        if (!string.IsNullOrEmpty(res))
                        {
                            ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(res);
                            if (apiresmodel.Data != null)
                            {
                                ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(apiresmodel.Data.ToString());
                                if (apimodel.ResultCode > 0)
                                {
                                    CellInfoModel model = new CellInfoModel()
                                    {
                                        CellInfoId = apimodel.ResultCode,
                                        BarCode = CellBarCode
                                    };
                                    scanoverlist.Clear();
                                    scanoverlist.Add(model);
                                    TDCommom.Logs.Info(string.Format("电芯扫码OK：BarCode: {0}", CellBarCode));
                                    TDCommon.LogManager.WriteLocalLog(string.Format("电芯扫码OK：BarCode: {0}", CellBarCode));
                                    OmronTCP.WriteSingleD(RobotSocket, plcAddress.IsCheckCellOK, 1);//给PLC电芯OK信号
                                    OmronTCP.WriteSingleD(RobotSocket, plcAddress.IsCellScan, 0);//复位电芯扫码信号
                                }
                            }
                        }
                    }
                    else//校验失败
                    {
                        TDCommom.Logs.Error(string.Format("电芯校验NG：BarCode: {0}", CellBarCode));
                        TDCommon.LogManager.WriteLocalLog(string.Format("电芯校验NG：BarCode: {0}", CellBarCode));
                        OmronTCP.WriteSingleD(RobotSocket, plcAddress.IsCheckCellOK, 2);//给PLC电芯NG信号
                        OmronTCP.WriteSingleD(RobotSocket, plcAddress.IsCellScan, 0);//复位电芯扫码信号

                    }
                }
            }

        }


        /// <summary>
        /// 电芯移入翻转工位
        /// </summary>
        void CellToBuff()
        {
            try
            {
                while (isLeftRun)
                {
                    if (RobotIPlist[0].IsConn)
                    {
                        #region 电芯进入缓存位

                        if (scanoverlist.Count > 0)
                        {
                            if (OmronTCP.ReadSingleD(RobotSocket, plcAddress.CellToBuff) == 1)//电芯进入缓存位信号
                            {
                                DateTime starttime = DateTime.Now;
                                //查找状态为10（扫码完成）的电芯  扫码时间降序排序
                                CellInfoModel cellmodel = new CellInfoModel()
                                {
                                    CellInfoId = scanoverlist[0].CellInfoId,
                                    State = 20,BarCode= scanoverlist[0].BarCode

                                };
                                string data = TDCommom.ObjectExtensions.ToJsonString(cellmodel);
                                string res = cellapi.UpdateCellState(data);
                                if (!string.IsNullOrEmpty(res))
                                {
                                    ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(res);
                                    if (apiresmodel.Data != null)
                                    {
                                        ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(apiresmodel.Data.ToString());
                                        if (apimodel.ResultCode > 0)
                                        {
                                            TDCommom.Logs.Info(string.Format("电芯进缓存位：{0}", scanoverlist.FirstOrDefault().BarCode));
                                            TDCommon.LogManager.WriteLocalLog(string.Format("电芯进缓存位：{0}", scanoverlist.FirstOrDefault().BarCode));
                                            scanoverlist.Clear();
                                            buffcelllist.Add(cellmodel);
                                            //改变状态 改成20 （进入缓存位）
                                            OmronTCP.WriteSingleD(RobotSocket, plcAddress.CellToBuff, 0);//复位电芯进入缓存位信号
                                        }
                                    }
                                }
                                TimeSpan ts = DateTime.Now - starttime;
                                TDCommom.Logs.Info(string.Format("电芯进缓存位时间：{0}", ts.TotalMilliseconds.ToString()));
                                TDCommon.LogManager.WriteLocalLog(string.Format("电芯进缓存位时间：{0}", ts.TotalMilliseconds.ToString()));
                            }
                        }
                        #endregion

                        #region 电芯进入夹具
                        if (buffcelllist.Count > 1)
                        {
                            int count = 0;
                            if (OmronTCP.ReadSingleD(RobotSocket, plcAddress.CellToFix) == 1)//电芯入夹具信号
                            {
                                //判断是左还是右夹具
                                DateTime startTime = DateTime.Now;
                                for (int i = 0; i < 2; i++)
                                {
                                    string data = TDCommom.ObjectExtensions.ToJsonString(new CellInfoModel()
                                    {
                                        CellInfoId = buffcelllist[i].CellInfoId,
                                        CAId = chuckbufflist[0].CAId,
                                        CellPosition = LeftCellCount + 1,
                                        State = 30

                                    });
                                    string res = cellapi.UpdateCellState(data);//修改电芯状态为30 进入夹具
                                    if (!string.IsNullOrEmpty(res))
                                    {
                                        ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(res);
                                        if (apiresmodel.Data != null)
                                        {
                                            ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(apiresmodel.Data.ToString());
                                            if (apimodel.ResultCode > 0)
                                            {

                                                LeftCellCount = LeftCellCount + 1;
                                                TDCommom.Logs.Info(string.Format("电芯{0}进入A:{1}夹具", buffcelllist[i].BarCode,chuckbufflist[0].CABarCode));
                                                TDCommon.LogManager.WriteLocalLog(string.Format("电芯{0}进入A:{1}夹具", buffcelllist[i].BarCode, chuckbufflist[0].CABarCode));
                                                count++;
                                                //改变状态 改成20 （进入缓存位）
                                            }
                                        }
                                    }
                                }
                                if (count == 2)
                                {
                                    buffcelllist.Clear();
                                    OmronTCP.WriteSingleD(RobotSocket, plcAddress.CellToFix, 0);//复位电芯进入夹具信号
                                }
                                else
                                {
                                    //给NG信号
                                }
                                TimeSpan tspan = DateTime.Now - startTime;
                                TDCommom.Logs.Info(string.Format("电芯进夹具时间：{0}", tspan.TotalMilliseconds.ToString()));
                                TDCommon.LogManager.WriteLocalLog(string.Format("电芯进夹具时间：{0}", tspan.TotalMilliseconds.ToString()));
                            }
                            else if (OmronTCP.ReadSingleD(RobotSocket, plcAddress.CellToFix) == 2)//电芯去右夹具
                            {
                                DateTime startTime = DateTime.Now;
                                for (int i = 0; i < 2; i++)
                                {
                                    string data = TDCommom.ObjectExtensions.ToJsonString(new CellInfoModel()
                                    {
                                        CAId = chuckbufflist[1].CAId,
                                        CellPosition = RightCellCount + 1,
                                        State = 30,
                                        CellInfoId = buffcelllist[i].CellInfoId


                                    });
                                    string res = cellapi.UpdateCellState(data);//修改电芯状态为30 进入夹具
                                    if (!string.IsNullOrEmpty(res))
                                    {
                                        ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(res);
                                        if (apiresmodel.Data != null)
                                        {
                                            ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(apiresmodel.Data.ToString());
                                            if (apimodel.ResultCode > 0)
                                            {

                                                RightCellCount = RightCellCount + 1;
                                                count++;
                                                TDCommom.Logs.Info(string.Format("电芯{0}进入B:夹具", buffcelllist[i].BarCode, chuckbufflist[1].CABarCode));
                                                TDCommon.LogManager.WriteLocalLog(string.Format("电芯{0}进入B:{1}夹具", buffcelllist[i].BarCode,chuckbufflist[1].CABarCode));
                                                //改变状态 改成30 （进入缓存位）
                                            }
                                        }
                                    }
                                }
                                if (count == 2)
                                {
                                    buffcelllist.Clear();
                                    OmronTCP.WriteSingleD(RobotSocket, plcAddress.CellToFix, 0);//复位电芯进入夹具信号
                                }
                                else
                                {
                                    //给NG信号
                                }
                            }

                        }
                        #endregion
                    }
                }

            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("电芯进入缓存位异常： ErrorMsg：{0}", ex.Message));
                TDCommon.LogManager.WriteLocalLog(string.Format("电芯进入缓存位异常： ErrorMsg：{0}", ex.Message));
            }
        }



        /// <summary>
        /// 查找RGV任务线程
        /// </summary>
        /// <param name="paramater"></param>
        void LookupRgvTask(string paramater)
        {
            #region  出炉任务
            //查找出炉夹具信息 
            ChuckingApplianceInfoModel model = new ChuckingApplianceInfoModel();
            string res = chuckapi.GetOutStoveInfo(TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
            {
                Data = paramater
            }));
            if (!string.IsNullOrEmpty(res))
            {
                ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(res);
                if (apiresmodel.Data != null && apiresmodel.Data != "")
                {
                    model = TDCommom.ObjectExtensions.FromJsonString<ChuckingApplianceInfoModel>(apiresmodel.Data.ToString());
                }
            }
            if (model != null && model.Remark1 != "1" && model.CAId > 0)//判断有出炉夹具信息
            {
                //判断下料位是否有夹具 无夹具 放入下料位 
                List<ChuckingApplianceInfoModel> BlankingBufflist = new List<ChuckingApplianceInfoModel>();

                string blankingbuffres = chuckapi.GetInfoByState(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { State = int.Parse(TDCommon.SysEnum.FixtureState.BlankingScan.ToString("d")), Data = paramater }));//查询下料扫码位是否有夹具

                if (!string.IsNullOrEmpty(blankingbuffres))
                {
                    ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(blankingbuffres);
                    if (apiresmodel.Data != null && apiresmodel.Data != "")
                    {
                        BlankingBufflist = TDCommom.ObjectExtensions.FromJsonString<List<ChuckingApplianceInfoModel>>(apiresmodel.Data.ToString());
                    }
                }

                if (BlankingBufflist.Count == 0)//下料位有夹具正在下料 放入缓存炉
                {
                    RgvTaskModel rgvtaskData = new RgvTaskModel()
                    {
                        TaskType = 3,
                        RgvType = paramater,
                        CAId = model.CAId,
                        TaskName = "出炉到下料扫码位",
                        TaskGrade = 1,
                        TaskState = 0,
                        TakeCode = 2,//model.HTDId,
                        ReleaseCode = 4,//135,//下料位
                        StoveIp = "",
                        StartDoorAddress = model.HTDLayer,
                        EndDoorAddress = 0,
                        TaskStep = 0,
                        Remark = "",
                        CreateTime=DateTime.Now
                    };
                    
                    string rgvres = rgvapi.InsertRgvTask(TDCommom.ObjectExtensions.ToJsonString(rgvtaskData)); //添加到rgv任务表
                    if (!string.IsNullOrEmpty(rgvres))
                    {
                        ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(rgvres);
                        if (apiresmodel.Data != null && apiresmodel.Data != null)
                        {
                            ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(apiresmodel.Data.ToString());
                            if (apimodel.ResultCode > 0)
                            {
                                rgvtaskData.RgvTaskId = apimodel.ResultCode;
                                tasklist.Add(rgvtaskData);
                                chuckapi.UpdateChuckInfoRemake1(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = model.CAId, Data = "1" }));//修改夹具的任务值
                                TDCommom.Logs.Info(string.Format("{0}RGV去炉子取满夹具出炉至下料扫码位任务已添加!即将开始执行任务-----------------", paramater));
                                TDCommon.LogManager.WriteLocalLog(string.Format("{0}RGV去炉子取满夹具出炉至下料扫码位任务已添加!即将开始执行任务-----------------", paramater));
                            }
                            else
                            {
                                TDCommom.Logs.Error("任务添加异常");
                            }
                        }
                    }
                }
            }
            #endregion

            #region 入炉任务
            List<ChuckingApplianceInfoModel> Instovelist = new List<ChuckingApplianceInfoModel>();

            string instoveres = chuckapi.GetInfoByState(TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
            {
                Data = paramater,
                State = int.Parse(TDCommon.SysEnum.FixtureState.CodeBindEnd.ToString("d"))
            }));
            if (!string.IsNullOrEmpty(instoveres))
            {
                ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(instoveres);
                if (apiresmodel.Data != null)
                {
                    Instovelist = TDCommom.ObjectExtensions.FromJsonString<List<ChuckingApplianceInfoModel>>(apiresmodel.Data.ToString());
                }
            }
            if (Instovelist != null && Instovelist.Count > 0 && Instovelist[0].Remark1 != "1")
            {
                int Rgvsimcard = -1;
                HighTemperatureDetailModel hidateilmodel = new HighTemperatureDetailModel();
                string ids = TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                {
                    id = paramater == "A" ? 1 : 2,
                    Data = Instovelist[0].ProductModelId.ToString(),
                    Type = 1
                });
                string hires = hideatlapi.GetInstoveSimcard(ids);
                if (!string.IsNullOrEmpty(hires))
                {
                    ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(hires);
                    if (apiresmodel.Data != null)
                    {
                        hidateilmodel = TDCommom.ObjectExtensions.FromJsonString<HighTemperatureDetailModel>(apiresmodel.Data.ToString());
                    }
                }

                if (hidateilmodel != null && hidateilmodel.SimCard > 0)
                { }
              
                RgvTaskModel rgvtaskData = new RgvTaskModel()
                { 
                    TaskType = 1,
                    RgvType = paramater,
                    CAId = Instovelist[0].CAId,
                    TaskName = "上料入炉",
                    TaskGrade = 3,
                    TaskState = 0,
                    TakeCode = 1,
                    ReleaseCode = hidateilmodel.SimCard,//4,hidateilmodel.SimCard,//下料位   2 
                    StoveIp = hidateilmodel.Remarke.ToString(),//hidateilmodel.Remarke.ToString(),   0
                    StartDoorAddress = 0,
                    EndDoorAddress = 0,
                    TaskStep = 0,
                    Remark = "",
                    CreateTime = DateTime.Now
                };
                string rgvres = rgvapi.InsertRgvTask(TDCommom.ObjectExtensions.ToJsonString(rgvtaskData)); //添加到rgv任务表

                if (!string.IsNullOrEmpty(rgvres))
                {
                    ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(rgvres);
                    if (apiresmodel.Data != null && apiresmodel.Data != null)
                    {
                        ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(apiresmodel.Data.ToString());
                        if (apimodel.ResultCode > 0)
                        {
                            rgvtaskData.RgvTaskId = apimodel.ResultCode;
                            tasklist.Add(rgvtaskData);
                            chuckapi.UpdateChuckInfoRemake1(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = Instovelist[0].CAId, Data = "1" }));//修改夹具的任务值
                            TDCommom.Logs.Info(string.Format("{0}RGV去上料位取满夹具入炉任务已添加! 即将开始执行-----------------", paramater));
                            TDCommon.LogManager.WriteLocalLog(string.Format("{0}RGV去上料位取满夹具入炉任务已添加! 即将开始执行-----------------", paramater));
                        }
                        else
                        {
                            TDCommom.Logs.Error("任务添加异常");
                        }
                    }
                }
            }

            #endregion

            #region 空夹具回流任务
            List<ChuckingApplianceInfoModel> EmptyFixlist = new List<ChuckingApplianceInfoModel>();
            string EmptyFixres = chuckapi.GetInfoByState(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { State = int.Parse(TDCommon.SysEnum.FixtureState.ProcessEnd.ToString("d")), Data = paramater }));//生产流程结束
            if (!string.IsNullOrEmpty(EmptyFixres))
            {
                ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(EmptyFixres);
                if (apiresmodel.Data != null && apiresmodel.Data != "")
                {
                    EmptyFixlist = TDCommom.ObjectExtensions.FromJsonString<List<ChuckingApplianceInfoModel>>(apiresmodel.Data.ToString());
                }
            }

            if (EmptyFixlist.Count > 0 && EmptyFixlist[0].Remark1 != "1")//是否有夹具已结束下料扫码
            {
                List<ChuckingApplianceInfoModel> loadinglist = new List<ChuckingApplianceInfoModel>();

                string loadingres = chuckapi.GetInfoByState(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { State = int.Parse(TDCommon.SysEnum.FixtureState.CodeBinding.ToString("d")), Data = paramater }));//查询上料扫码位是否有夹具

                if (!string.IsNullOrEmpty(loadingres))
                {
                    ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(loadingres);
                    if (apiresmodel.Data != null && apiresmodel.Data != "")
                    {
                        loadinglist = TDCommom.ObjectExtensions.FromJsonString<List<ChuckingApplianceInfoModel>>(apiresmodel.Data.ToString());
                    }
                }

                if (loadinglist.Count > 0)//上料位料位有夹具正在上料 放入空夹具缓存炉
                {
                    //HighTemperatureDetailModel hidateilmodel = new HighTemperatureDetailModel();
                    //string ids = TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                    //{
                    //    id = paramater == "A" ? 1 : 2,
                    //    Data = "空夹具",
                    //    Type = 3
                    //});
                    //string hires = hideatlapi.GetInstoveSimcard(ids);//获取缓存炉门号
                    //if (!string.IsNullOrEmpty(hires))
                    //{
                    //    ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(hires);
                    //    if (apiresmodel.Data != null)
                    //    {
                    //        hidateilmodel = TDCommom.ObjectExtensions.FromJsonString<HighTemperatureDetailModel>(apiresmodel.Data.ToString());
                    //        if (hidateilmodel != null && hidateilmodel.SimCard > 0)
                    //        {
                    RgvTaskModel rgvtaskData = new RgvTaskModel()
                    {
                        TaskType = 4,
                        RgvType = paramater,
                        CAId = EmptyFixlist[0].CAId,
                        TaskName = "下料扫码位到上料扫码位",
                        TaskGrade = 1,
                        TaskState = 0,
                        TakeCode = 4,//135,
                        ReleaseCode = 1,//下料位
                        StoveIp = "",
                        StartDoorAddress = 0,
                        EndDoorAddress = 0,
                        TaskStep = 0,
                        Remark = "",
                        CreateTime = DateTime.Now
                    };
                    string  rgvres = rgvapi.InsertRgvTask(TDCommom.ObjectExtensions.ToJsonString(rgvtaskData)); //添加到rgv任务表
                    if (!string.IsNullOrEmpty(rgvres))
                    {
                        ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(rgvres);
                        if (apiresmodel.Data != null && apiresmodel.Data != null)
                        {
                            ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(apiresmodel.Data.ToString());
                            if (apimodel.ResultCode > 0)
                            {
                                rgvtaskData.RgvTaskId = apimodel.ResultCode;
                                tasklist.Add(rgvtaskData);
                                chuckapi.UpdateChuckInfoRemake1(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = EmptyFixlist[0].CAId, Data = "1" }));//修改夹具的任务值
                                TDCommom.Logs.Info(string.Format("{0}RGV下料扫码位到上料扫码位任务已添加!即将开始执行任务-----------------", paramater));
                                TDCommon.LogManager.WriteLocalLog(string.Format("{0}RGV下料扫码位到上料扫码位任务已添加!即将开始执行任务-----------------", paramater));
                            }
                            else
                            {
                                TDCommom.Logs.Error("任务添加异常");
                            }
                        }
                    }
                }
                else
                {
                    RgvTaskModel rgvtaskData = new RgvTaskModel()
                    {
                        TaskType = 4,
                        RgvType = paramater,
                        CAId = EmptyFixlist[0].CAId,
                        TaskName = "下料扫码位到上料扫码位",
                        TaskGrade = 1,
                        TaskState = 0,
                        TakeCode = 4,//135,
                        ReleaseCode = 1,//下料位
                        StoveIp = "",
                        StartDoorAddress = 0,
                        EndDoorAddress = 0,
                        TaskStep = 0,
                        Remark = "",
                        CreateTime = DateTime.Now
                    };


                    string rgvres = rgvapi.InsertRgvTask(TDCommom.ObjectExtensions.ToJsonString(rgvtaskData)); //添加到rgv任务表
                    if (!string.IsNullOrEmpty(rgvres))
                    {
                        ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(rgvres);
                        if (apiresmodel.Data != null && apiresmodel.Data != null)
                        {
                            ApiModel apimodel = TDCommom.ObjectExtensions.FromJsonString<ApiModel>(apiresmodel.Data.ToString());
                            if (apimodel.ResultCode > 0)
                            {
                                rgvtaskData.RgvTaskId = apimodel.ResultCode;
                                tasklist.Add(rgvtaskData);
                                chuckapi.UpdateChuckInfoRemake1(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = EmptyFixlist[0].CAId, Data = "1" }));//修改夹具的任务值
                                TDCommom.Logs.Info(string.Format("{0}RGV下料扫码位到上料扫码位任务已添加!即将开始执行任务-----------------", paramater));
                                TDCommon.LogManager.WriteLocalLog(string.Format("{0}RGV下料扫码位到上料扫码位任务已添加!即将开始执行任务-----------------", paramater));
                            }
                            else
                            {
                                TDCommom.Logs.Error("任务添加异常");
                            }
                        }
                    }
                }

            }
            #endregion
        }

        /// <summary>
        /// RGV任务线程
        /// </summary>
        /// <param name="paramater"></param>
        void ImplementRgvTask(string paramater)
        {
            try
            {
                if (tasklist.Count > 0)
                {
                    ingtasklist = tasklist.Where(x => x.TaskState == 1).OrderByDescending(a => a.CreateTime).ToList();//执行中任务
                    stayTasklist = tasklist.Where(x => x.TaskState == 0).OrderByDescending(a => a.CreateTime).ToList();//待执行任务

                    List<RgvTaskModel> Firstlevel = tasklist.Where(x => x.TaskState == 0 && x.TaskGrade == 1).ToList();//第一级别
                    List<RgvTaskModel> Secondlevel = tasklist.Where(x => x.TaskState == 0 && x.TaskGrade == 2).ToList();//第二级别
                    List<RgvTaskModel> Thirdlevel = tasklist.Where(x => x.TaskState == 0 && x.TaskGrade == 3).ToList();//第三级别


                    #region 处理正在执行任务

                    if (ingtasklist.Count > 0)//当前有正在执行中的任务
                    {
                        PerformTasks(paramater, ingtasklist[0]);
                    }
                    else  //无正在执行任务 且RGV为空闲中 查找等级优先任务执行
                    {
                        if (Firstlevel.Count > 0)
                        {
                            PerformTasks(paramater, Firstlevel[0]);
                        }
                        else if (Secondlevel.Count > 0)
                        {
                            PerformTasks(paramater, Secondlevel[0]);
                        }
                        else if (Thirdlevel.Count > 0)
                        {
                            PerformTasks(paramater, Thirdlevel[0]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TDCommom.Logs.Error(string.Format("RGV执行任务异常：ErrMsg：{0}", ex.Message));
                TDCommon.LogManager.WriteLocalLog(string.Format("RGV执行任务异常：ErrMsg：{0}", ex.Message));
            }
            #endregion
        }

        /// <summary>
        /// rgv调度
        /// </summary>
        /// <param name="paramater"></param>
        /// <param name="model"></param>
        private void PerformTasks(string paramater, RgvTaskModel model)
        {
            if (model.TaskState != 1)
            {
                rgvapi.UpdateRgvTaskInfoByid(TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                {
                    id = model.RgvTaskId,
                    State = 1
                }));//修改RGV任务状态为执行中
                tasklist.Remove(model);//从集合中移除当前任务 
                model.TaskState = 1;//将任务状态修改为正在执 行 
                tasklist.Add(model);//然后添加到集合中
            }
            int StartSimcard = model.TakeCode;//写给RGV开始地址
            int EndSimcard = model.ReleaseCode;//写给RGV结束地址
            int stovesimcard = model.StartDoorAddress;//写给炉子门号
            int endstovesimcard = model.EndDoorAddress;
            int stoveip = model.StoveIp == "" ? 0 : int.Parse(model.StoveIp) - 1;//第几个炉子

            Socket socket = paramater == "A" ? RgvSocket[0] : RgvSocket[1];
            PLCInfoModel plcinfoip = paramater == "A" ? rgvIPlist[0] : rgvIPlist[1];

            ChuckingApplianceInfoModel chuckmodel = new ChuckingApplianceInfoModel();//夹具信息

            string chuckres = chuckapi.GetChuckInfoByid(TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
            {
                id = model.CAId
            }));
            if (!string.IsNullOrEmpty(chuckres))
            {
                ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(chuckres);
                if (apiresmodel.Data != null)
                {
                    chuckmodel = TDCommom.ObjectExtensions.FromJsonString<ChuckingApplianceInfoModel>(apiresmodel.Data.ToString());
                }
            }//获取夹具当前信息
            if (chuckmodel != null)
            {
                if (model.TaskType == 1)//上料位到炉子（入炉）
                {
                    #region RGV去上料位取满夹具
                    if (model.TaskStep == 0)
                    {
                        if (plcinfoip.IsConn)
                        {
                            if (OmronTCP.ReadSingleD(socket, plcAddress.RgvState) == 2 && OmronTCP.ReadSingleD(socket,plcAddress.Automation)==1)//RGV正常等待中
                            {
                                OmronTCP.WriteSingleD(socket, plcAddress.RgvSimcard, StartSimcard);//下指令让RGV去上料位取弹夹
                                OmronTCP.WriteSingleD(socket, plcAddress.FetchOrRelease, 2);//取放信号
                                OmronTCP.WriteSingleD(socket, plcAddress.IsFetchOrRelease, 1);//是否可取放
                                OmronTCP.WriteSingleD(socket, plcAddress.RGVStart, 1);//启动信号

                                string parameter = TDCommom.ObjectExtensions.ToJsonString(new ChuckingApplianceInfoModel()
                                {
                                    CAId = chuckmodel.CAId,
                                    CAState = int.Parse(TDCommon.SysEnum.FixtureState.TobeReclaimed.ToString("d"))//待取料
                                });
                                //chuckapi.UpdateState(parameter);//改变夹具的状态
                                rgvapi.UpdateTaskStepById(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = model.RgvTaskId, Type = 1 }));//将步骤改为1
                                tasklist.Remove(model);//从集合中移除当前任务 
                                model.TaskStep = 1;//将任务状态修改为正在执行 
                                tasklist.Add(model);//然后添加到集合中
                                TDCommom.Logs.Info("RGV去上料位取夹具!");
                                TDCommon.LogManager.WriteLocalLog("RGV去上料位取夹具!");
                            }
                        }
                    }
                    #endregion

                    #region RGV去炉子放料
                    if (model.TaskStep == 1)
                    {
                        if (OmronTCP.ReadSingleD(socket, plcAddress.FeRefinish) == 1)//取放完成信号
                        {
                            if (EndSimcard > 0 && EndSimcard < 133 &&  OmronTCP.ReadSingleD(socket, plcAddress.Automation) == 1)//门号正确写入Rgv， 
                            {
                                OmronTCP.WriteSingleD(socket, plcAddress.RgvSimcard, EndSimcard);//下指令让RGV去上料位取弹夹
                                OmronTCP.WriteSingleD(socket, plcAddress.FetchOrRelease, 1);//取放信号
                                OmronTCP.WriteSingleD(socket, plcAddress.IsFetchOrRelease, 1);//是否可取放
                                OmronTCP.WriteSingleD(socket, plcAddress.RGVStart, 1);//启动信号

                                //修改夹具状态
                                string parameter = TDCommom.ObjectExtensions.ToJsonString(new ChuckingApplianceInfoModel()
                                {
                                    CAId = chuckmodel.CAId,
                                    HTDId = EndSimcard,
                                    CAState = int.Parse(TDCommon.SysEnum.FixtureState.Instoveing.ToString("d"))

                                });
                                string result = chuckapi.UpdateState(parameter);
                                rgvapi.UpdateTaskStepById(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = model.RgvTaskId, Type = 2 }));//将步骤改为2
                                tasklist.Remove(model);//从集合中移除当前任务 
                                model.TaskStep = 2;//将任务状态修改为正在执行 
                                tasklist.Add(model);//然后添加到集合中
                                OmronTCP.WriteSingleD(socket, plcAddress.FeRefinish, 0);//复位取放完成信号
                                TDCommom.Logs.Info(string.Format("RGV去炉子放料 门号:{0}", EndSimcard));
                                TDCommon.LogManager.WriteLocalLog(string.Format("RGV去炉子放料 门号:{0}", EndSimcard));
                            }
                            else
                            {
                                TDCommom.Logs.Error(string.Format("门号获取异常：请检查任务状态"));
                            }
                        }
                    }
                    #endregion

                    #region 完成入炉
                    if (model.TaskStep == 2)
                    {
                        if (OmronTCP.ReadSingleD(socket, plcAddress.FeRefinish) == 1)//RGV放料完成
                        {
                            string result = chuckapi.UpdateState(TDCommom.ObjectExtensions.ToJsonString(new ChuckingApplianceInfoModel()
                            {
                                CAId = chuckmodel.CAId,
                                CAState = int.Parse(TDCommon.SysEnum.FixtureState.InstoveEnd.ToString("d")),
                                Remark1 = "99"
                            }));
                            //修改任务状态
                            rgvapi.UpdateRgvTaskInfoByid(TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                            {
                                id = model.RgvTaskId,
                                State = 99
                            }));

                            rgvapi.UpdateTaskStepById(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = model.RgvTaskId, Type = 99 }));//将步骤改为99
                            tasklist.Remove(model);//从任务集合移除当前已完成任务

                            TDCommom.Logs.Info(string.Format("夹具入炉完成，夹具条码：{0}", chuckmodel.CABarCode));
                            TDCommon.LogManager.WriteLocalLog(string.Format("夹具入炉完成，夹具条码：{0}", chuckmodel.CABarCode));
                            OmronTCP.WriteSingleD(socket, plcAddress.FeRefinish, 0);//复位信号
                        }
                    }
                    #endregion
                }
                #region 出炉到缓存炉
                //else if (model.TaskType == 2) //出炉到缓存炉
                //{
                //    #region RGV去炉子取料
                //    if (model.TaskStep == 0 && OmronTCP.ReadSingleD(socket, plcAddress.Paramater) == 1)// 并且RGV状态为空闲
                //    {
                //        OmronTCP.WriteSingleD(socket, plcAddress.Paramater, StartSimcard);//Rgv去取料门号等待

                //        rgvapi.UpdateTaskStepById(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = model.RgvTaskId, Type = 1 }));//将步骤改为1
                //        tasklist.Remove(model);//从集合中移除当前任务 
                //        model.TaskStep = 1;//将任务状态修改为正在执行 
                //        tasklist.Add(model);//然后添加到集合中

                //        //修改夹具状态
                //        string data = TDCommom.ObjectExtensions.ToJsonString(new ChuckingApplianceInfoModel()
                //        {
                //            CAId = chuckmodel.CAId,
                //            CAState = int.Parse(TDCommon.SysEnum.FixtureState.Outstoveing.ToString("d"))
                //        });
                //        chuckapi.UpdateState(data);//改变夹具的状态

                //        TDCommom.Logs.Info(string.Format("夹具出炉中：夹具条码：{0}", chuckmodel.CABarCode));
                //        TDCommon.LogManager.WriteLocalLog(string.Format("夹具出炉中：夹具条码：{0}", chuckmodel.CABarCode));
                //    }
                //    #endregion

                //    #region 处理出炉中夹具
                //    if (model.TaskStep == 1)
                //    {
                //        if (OmronTCP.ReadSingleD(socket, plcAddress.Paramater) == 1)//rgv到达取料指定位置
                //        {
                //            OmronTCP.WriteSingleD(stoveSocketlist[stoveip], plcAddress.Paramater, stovesimcard);//打开炉门
                //            OmronTCP.WriteSingleD(socket, plcAddress.Paramater, 0);//复位rgv到达取料指定位置信号
                //        }

                //        if (OmronTCP.ReadSingleD(stoveSocketlist[stoveip], plcAddress.Paramater) == 1)//炉门打开完成
                //        {
                //            OmronTCP.WriteSingleD(socket, plcAddress.Paramater, 1);//RGV取料
                //            OmronTCP.WriteSingleD(stoveSocketlist[stoveip], plcAddress.Paramater, 0);//复位炉门打开完成信号
                //        }

                //        if (OmronTCP.ReadSingleD(socket, plcAddress.Paramater) == 1)//rgv取料完成
                //        {
                //            OmronTCP.WriteSingleD(stoveSocketlist[stoveip], plcAddress.Paramater, stovesimcard);//关闭炉门
                //            rgvapi.UpdateTaskStepById(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = model.RgvTaskId, Type = 2 }));//将步骤改为2

                //            tasklist.Remove(model);//从集合中移除当前任务 
                //            model.TaskStep = 2;//将任务状态修改为正在执行 
                //            tasklist.Add(model);//然后添加到集合中

                //            chuckapi.UpdateState(TDCommom.ObjectExtensions.ToJsonString(new ChuckingApplianceInfoModel()
                //            {
                //                CAId = chuckmodel.CAId,
                //                CAState = int.Parse(TDCommon.SysEnum.FixtureState.UnloadedBuffer.ToString("d"))
                //            }));//修改夹具状态为待下料缓存
                //            OmronTCP.WriteSingleD(socket, plcAddress.Paramater, EndSimcard);//给RGV写入缓存炉门号
                //            OmronTCP.WriteSingleD(socket, plcAddress.Paramater, 0);//复位rgv取料完成信号
                //                                                                   //修改炉子状态
                //            hideatlapi.UpdateHtdState(TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                //            {
                //                HTDId = chuckmodel.HTDId,
                //                State = 20
                //            }));//空闲
                //            TDCommom.Logs.Info(string.Format("夹具：{0} 出炉完成，待入缓存炉！", chuckmodel.CABarCode));
                //            TDCommon.LogManager.WriteLocalLog(string.Format("夹具：{0} 出炉完成，待入缓存炉！", chuckmodel.CABarCode));
                //        }
                //    }
                //    #endregion

                //    #region 处理去缓存炉夹具
                //    if (model.TaskStep == 2)
                //    {
                //        if (OmronTCP.ReadSingleD(socket, plcAddress.Paramater) == 1)//rgv到达放料指定位置
                //        {
                //            OmronTCP.WriteSingleD(stoveSocketlist[stoveip], plcAddress.Paramater, endstovesimcard);//打开炉门
                //            OmronTCP.WriteSingleD(socket, plcAddress.Paramater, 0);//复位rgv到达放料指定位置信号
                //        }

                //        if (OmronTCP.ReadSingleD(stoveSocketlist[stoveip], plcAddress.Paramater) == 1)//炉门打开完成
                //        {
                //            OmronTCP.WriteSingleD(socket, plcAddress.Paramater, 1);//RGV放料
                //            OmronTCP.WriteSingleD(stoveSocketlist[stoveip], plcAddress.Paramater, 0);//复位炉门打开完成信号
                //        }

                //        if (OmronTCP.ReadSingleD(socket, plcAddress.Paramater) == 1)//rgv放料完成
                //        {
                //            OmronTCP.WriteSingleD(stoveSocketlist[stoveip], plcAddress.Paramater, endstovesimcard);//关闭炉门
                //            chuckapi.UpdateState(TDCommom.ObjectExtensions.ToJsonString(new ChuckingApplianceInfoModel()
                //            {
                //                CAId = chuckmodel.CAId,
                //                CAState = int.Parse(TDCommon.SysEnum.FixtureState.BlankingBuff.ToString("d")),
                //                Remark1 = "0"
                //            }));//修改夹具状态为下料缓存

                //            rgvapi.UpdateTaskStepById(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = model.RgvTaskId, Type = 99 }));//将步骤改为99
                //            rgvapi.UpdateRgvTaskInfoByid(TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                //            {
                //                id = model.RgvTaskId,
                //                State = 99
                //            })); //修改任务状态
                //            tasklist.Remove(model);//从任务集合移除当前已完成任务
                //            OmronTCP.WriteSingleD(socket, plcAddress.Paramater, 0);//复位rgv放料完成信号
                //            TDCommom.Logs.Info(string.Format("夹具：{0} 入下料缓存完成，待入下料扫码位！", chuckmodel.CABarCode));
                //            TDCommon.LogManager.WriteLocalLog(string.Format("夹具：{0} 出炉完成，待入下料扫码位！", chuckmodel.CABarCode));
                //        }
                //    }

                //    #endregion
                //}
                #endregion
                else if (model.TaskType == 3)//出炉到下料位
                {
                    #region RGV去炉子取料
                    if (model.TaskStep == 0)
                    {
                        if ( OmronTCP.ReadSingleD(socket, plcAddress.RgvState) == 2 &&  OmronTCP.ReadSingleD(socket, plcAddress.Automation) == 1)
                        {
                            OmronTCP.WriteSingleD(socket, plcAddress.RgvSimcard, StartSimcard);//下指令让RGV去上料位取弹夹
                            OmronTCP.WriteSingleD(socket, plcAddress.FetchOrRelease, 2);//取放信号
                            OmronTCP.WriteSingleD(socket, plcAddress.IsFetchOrRelease, 1);//是否可取放
                            OmronTCP.WriteSingleD(socket, plcAddress.RGVStart, 1);//启动信号

                            rgvapi.UpdateTaskStepById(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = model.RgvTaskId, Type = 1 }));//将步骤改为1
                            tasklist.Remove(model);//从集合中移除当前任务 
                            model.TaskStep = 1;//将任务状态修改为正在执行 
                            tasklist.Add(model);//然后添加到集合中
                                                //修改夹具状态
                            string data = TDCommom.ObjectExtensions.ToJsonString(new ChuckingApplianceInfoModel()
                            {
                                CAId = chuckmodel.CAId,
                                CAState = int.Parse(TDCommon.SysEnum.FixtureState.Outstoveing.ToString("d"))
                            });
                            chuckapi.UpdateState(data);//改变夹具的状态
                            TDCommom.Logs.Info(string.Format("夹具出炉中：夹具条码：{0}", chuckmodel.CABarCode));
                            TDCommon.LogManager.WriteLocalLog(string.Format("夹具出炉中：夹具条码：{0}", chuckmodel.CABarCode));
                        }
                    }
                    #endregion

                    #region 处理出炉中夹具
                    if (model.TaskStep == 1)
                    {
                        if (OmronTCP.ReadSingleD(socket, plcAddress.FeRefinish) == 1)//rgv取料完成
                        {
                            if (OmronTCP.ReadSingleD(socket, plcAddress.Automation) == 1)
                            {
                                OmronTCP.WriteSingleD(socket, plcAddress.RgvSimcard, EndSimcard);//下指令让RGV去上料位取弹夹
                                OmronTCP.WriteSingleD(socket, plcAddress.FetchOrRelease, 1);//取放信号
                                OmronTCP.WriteSingleD(socket, plcAddress.IsFetchOrRelease, 1);//是否可取放
                                OmronTCP.WriteSingleD(socket, plcAddress.RGVStart, 1);//启动信号

                                rgvapi.UpdateTaskStepById(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = model.RgvTaskId, Type = 2 }));//将步骤改为2
                                tasklist.Remove(model);//从集合中移除当前任务 
                                model.TaskStep = 2;//将任务状态修改为正在执行 
                                tasklist.Add(model);//然后添加到集合中
                                string data = TDCommom.ObjectExtensions.ToJsonString(new ChuckingApplianceInfoModel()
                                {
                                    CAId = chuckmodel.CAId,
                                    CAState = int.Parse(TDCommon.SysEnum.FixtureState.UnloadedBlankingScan.ToString("d"))
                                });
                                chuckapi.UpdateState(data);//修改夹具状态为待下料扫码
                                OmronTCP.WriteSingleD(socket, plcAddress.FeRefinish, 0);//复位rgv取料完成信号

                                TDCommom.Logs.Info(string.Format("夹具：{0} 出炉完成，待入下料扫码位！", chuckmodel.CABarCode));
                                TDCommon.LogManager.WriteLocalLog(string.Format("夹具：{0} 出炉完成，待入下料扫码位！", chuckmodel.CABarCode));
                            }
                        }
                    }
                    #endregion

                    #region 处理去下料扫码位
                    if (model.TaskStep == 2)
                    {
                        if (OmronTCP.ReadSingleD(socket, plcAddress.FeRefinish) == 1)//rgv放料完成
                        {
                            chuckapi.UpdateState(TDCommom.ObjectExtensions.ToJsonString(new ChuckingApplianceInfoModel()
                            {
                                CAId = chuckmodel.CAId,
                                CAState = int.Parse(TDCommon.SysEnum.FixtureState.BlankingScan.ToString("d")),
                                Remark1 = "0"
                            }));//修改夹具状态为下料扫描
                            rgvapi.UpdateTaskStepById(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = model.RgvTaskId, Type = 99 }));//将步骤改为2
                                                                                                                                                  //修改任务状态
                            rgvapi.UpdateRgvTaskInfoByid(TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                            {
                                id = model.RgvTaskId,
                                State = 99
                            }));
                            tasklist.Remove(model);//从任务集合移除当前已完成任务
                            OmronTCP.WriteSingleD(socket, plcAddress.FeRefinish, 0);//复位rgv放料完成信号
                            TDCommom.Logs.Info(string.Format("夹具：{0} 到达下料扫码位！", chuckmodel.CABarCode));
                            TDCommon.LogManager.WriteLocalLog(string.Format("夹具：{0} 到达下料扫码位！", chuckmodel.CABarCode));
                        }
                        
                    }
                    #endregion
                }
                else if (model.TaskType == 4)//下料位到上料位
                {
                    #region RGV去下料位取夹具
                    if (model.TaskStep == 0)//未开始执行
                    {
                        if (OmronTCP.ReadSingleD(socket, plcAddress.RgvState) == 2 &&   OmronTCP.ReadSingleD(socket, plcAddress.Automation) == 1)
                        {
                            OmronTCP.WriteSingleD(socket, plcAddress.RgvSimcard, StartSimcard);//下指令让RGV去下料位取弹夹
                            OmronTCP.WriteSingleD(socket, plcAddress.FetchOrRelease, 2);//取放信号
                            OmronTCP.WriteSingleD(socket, plcAddress.IsFetchOrRelease, 1);//是否可取放
                            OmronTCP.WriteSingleD(socket, plcAddress.RGVStart, 1);//启动信号

                            rgvapi.UpdateTaskStepById(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = model.RgvTaskId, Type = 1 }));//将步骤改为1
                            tasklist.Remove(model);//从集合中移除当前任务 
                            model.TaskStep = 1;//将任务状态修改为正在执行 
                            tasklist.Add(model);//然后添加到集合中
                            TDCommon.LogManager.WriteLocalLog(string.Format("RGV去下料位取夹具！"));
                            TDCommom.Logs.Info(string.Format("RGV去下料位取夹具！"));
                        }
                    }
                    #endregion

                    #region RGV去上料位放夹具
                    if (model.TaskStep == 1)//已将起始位置地址写个RGV  等待完成返回信号
                    {
                        if (OmronTCP.ReadSingleD(socket, plcAddress.FeRefinish) == 1)
                        {
                            if (OmronTCP.ReadSingleD(socket, plcAddress.Automation) == 1)
                            {
                                OmronTCP.WriteSingleD(socket, plcAddress.RgvSimcard, EndSimcard);//下指令让RGV去上料放取弹夹
                                OmronTCP.WriteSingleD(socket, plcAddress.FetchOrRelease, 1);//取放信号
                                OmronTCP.WriteSingleD(socket, plcAddress.IsFetchOrRelease, 1);//是否可取放
                                OmronTCP.WriteSingleD(socket, plcAddress.RGVStart, 1);//启动信号

                                rgvapi.UpdateTaskStepById(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = model.RgvTaskId, Type = 2 }));//将步骤改为2
                                tasklist.Remove(model);//从集合中移除当前任务 
                                model.TaskStep = 2;//将任务状态修改为正在执行 
                                tasklist.Add(model);//然后添加到集合中
                                OmronTCP.WriteSingleD(socket, plcAddress.FeRefinish, 0);//复位到位信号
                                TDCommon.LogManager.WriteLocalLog(string.Format("RGV去上料位放夹具！"));
                                TDCommom.Logs.Info(string.Format("RGV去上料位放夹具！"));
                            }
                        }
                    }

                    if (model.TaskStep == 2)
                    {
                        if (OmronTCP.ReadSingleD(socket, plcAddress.FeRefinish) == 1)
                        {
                            rgvapi.UpdateTaskStepById(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = model.RgvTaskId, Type = 99 }));//将步骤改为99
                            rgvapi.UpdateRgvTaskInfoByid(TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                            {
                                id = model.RgvTaskId,
                                State = 99
                            }));
                            tasklist.Remove(model);//从任务集合移除当前已完成任务
                            //将夹具添加到历史表
                            OmronTCP.WriteSingleD(socket, plcAddress.FeRefinish, 0);//复位RGV取料完成信号

                            string data = TDCommom.ObjectExtensions.ToJsonString(new ChuckingApplianceInfoModel()
                            {
                                CAId = model.CAId,
                                CAState = int.Parse(TDCommon.SysEnum.FixtureState.CodeBinding.ToString("d"))
                            });
                            chuckapi.UpdateState(data);//改变夹具的状态
                            chuckapi.UpdateChuckInfoRemake1(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = model.CAId, Data = "0" }));//修改夹具的任务值
                            chuckbufflist[0].CAId = model.CAId;
                            chuckbufflist[0].Remark1 = "0";
                            TDCommon.LogManager.WriteLocalLog(string.Format("RGV空夹具回流完成！"));
                            TDCommom.Logs.Info(string.Format("RGV空夹具回流完成！"));
                        }
                    }

                    #endregion
                }
                #region 下料位到缓存炉
                //else if (model.TaskType == 5)//下料位到缓存炉
                //{
                //    #region RGV去下料位取夹具
                //    if (model.TaskStep == 0)//未开始执行
                //    {
                //        if (OmronTCP.ReadSingleD(socket, plcAddress.Paramater) == 1)// 并且RGV状态为空闲
                //        {
                //            OmronTCP.WriteSingleD(socket, plcAddress.Paramater, StartSimcard);//Rgv去取料位等待
                //            rgvapi.UpdateTaskStepById(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = model.RgvTaskId, Type = 1 }));//将步骤改为1
                //            tasklist.Remove(model);//从集合中移除当前任务 
                //            model.TaskStep = 1;//将任务状态修改为正在执行 
                //            tasklist.Add(model);//然后添加到集合中
                //        }
                //    }
                //    #endregion

                //    #region RGV去上料位放夹具
                //    if (model.TaskStep == 1)//已将起始位置地址写个RGV  等待完成返回信号
                //    {
                //        if (OmronTCP.ReadSingleD(socket, plcAddress.Paramater) == 1)// RGV到位 等待取料信号
                //        {
                //            OmronTCP.WriteSingleD(socket, plcAddress.Paramater, 1);//让RGV取料
                //            rgvapi.UpdateTaskStepById(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = model.RgvTaskId, Type = 2 }));//将步骤改为1
                //            tasklist.Remove(model);//从集合中移除当前任务 
                //            model.TaskStep = 2;//将任务状态修改为正在执行 
                //            tasklist.Add(model);//然后添加到集合中
                //            OmronTCP.WriteSingleD(socket, plcAddress.Paramater, 1);//复位到位信号
                //        }
                //    }

                //    if (model.TaskStep == 2)//已让RGV取料 等待完成信号
                //    {
                //        if (OmronTCP.ReadSingleD(socket, plcAddress.Paramater) == 1)//   RGV取料完成信号
                //        {
                //            OmronTCP.WriteSingleD(socket, plcAddress.Paramater, EndSimcard);//Rgv去放料位等待
                //            rgvapi.UpdateTaskStepById(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = model.RgvTaskId, Type = 3 }));//将步骤改为3
                //            tasklist.Remove(model);//从集合中移除当前任务 
                //            model.TaskStep = 3;//将任务状态修改为正在执行 
                //            tasklist.Add(model);//然后添加到集合中
                //            OmronTCP.WriteSingleD(socket, plcAddress.Paramater, 1);//复位RGV取料完成信号
                //        }
                //    }

                //    if (model.TaskStep == 3)//rgv到达料位后 让炉子开门
                //    {
                //        if (OmronTCP.ReadSingleD(socket, plcAddress.Paramater) == 1)//   RGV到达放料位信号
                //        {
                //            OmronTCP.WriteSingleD(stoveSocketlist[stoveip], plcAddress.Paramater, endstovesimcard);//让炉子开门
                //            rgvapi.UpdateTaskStepById(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = model.RgvTaskId, Type = 4 }));//将步骤改为4
                //            tasklist.Remove(model);//从集合中移除当前任务 
                //            model.TaskStep = 4;//将任务状态修改为正在执行 
                //            tasklist.Add(model);//然后添加到集合中
                //            OmronTCP.WriteSingleD(socket, plcAddress.Paramater, 1);//复位RGV到达放料位信号
                //        }
                //    }

                //    if (model.TaskStep == 4)//炉子开门完成 让RGV放料
                //    {
                //        if (OmronTCP.ReadSingleD(stoveSocketlist[stoveip], plcAddress.Paramater) == 1)//   炉子开门完成
                //        {
                //            OmronTCP.WriteSingleD(socket, plcAddress.Paramater, 2);//让RGV放料
                //            rgvapi.UpdateTaskStepById(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = model.RgvTaskId, Type = 5 }));//将步骤改为5
                //            tasklist.Remove(model);//从集合中移除当前任务 
                //            model.TaskStep = 5;//将任务状态修改为正在执行 
                //            tasklist.Add(model);//然后添加到集合中
                //            OmronTCP.WriteSingleD(socket, plcAddress.Paramater, 1);//复位炉子开门完成信号
                //        }
                //    }

                //    if (model.TaskStep == 5)//rgv放料完成 修改任务状态
                //    {
                //        if (OmronTCP.ReadSingleD(socket, plcAddress.Paramater) == 1)//   RGV放料完成信号
                //        {
                //            OmronTCP.WriteSingleD(stoveSocketlist[stoveip], plcAddress.Paramater, endstovesimcard);//让炉子关门
                //            rgvapi.UpdateTaskStepById(TDCommom.ObjectExtensions.ToJsonString(new ApiModel() { id = model.RgvTaskId, Type = 99 }));//将步骤改为99
                //            OmronTCP.WriteSingleD(socket, plcAddress.Paramater, 1);//复位RGV放料完成信号
                //            rgvapi.UpdateRgvTaskInfoByid(TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                //            {
                //                id = model.RgvTaskId,
                //                State = 99
                //            }));
                //            tasklist.Remove(model);//从任务集合移除当前已完成任务
                //        }
                //    }

                //    #endregion
                //}
                #endregion 

            }
        }







        #region 下料线程-----------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// 启动右边线程
        /// </summary>
        private void StartRightMethod()
        {
            //查找RGV任务线程
            Thread thLookupRgvTask = new Thread(new ThreadStart(() =>
            {
                while (isRightRun)
                {
                    LookupRgvTask("B");
                    Thread.Sleep(5000);
                }
            }));
            thLookupRgvTask.IsBackground = true;
            //thLookupRgvTask.Start();

            //执行RGV任务线程
            Thread thImplementRgvTask = new Thread(new ThreadStart(() =>
            {
                while (isRightRun)
                {
                    ImplementRgvTask("B");
                    Thread.Sleep(5000);
                }
            }));
            thImplementRgvTask.IsBackground = true;
            //thImplementRgvTask.Start();

            Thread thBlanking = new Thread(new ThreadStart(() =>
            {
                while (isRightRun)
                {
                    Blanking();
                    BlankOver();
                    Thread.Sleep(1000);
                }
            }));
            thBlanking.IsBackground = true;
            thBlanking.Start();
        }




        void Blanking()
        {
            if (OmronTCP.ReadSingleD(RobotSocket, plcAddress.LeftBlankFixState) == 1)//判断是否左下料有夹具 
            {
                //判断是否有
                string result = chuckapi.GetCaiDataByState(TDCommom.ObjectExtensions.ToJsonString(new ApiModel()
                {
                    State = int.Parse(TDCommon.SysEnum.FixtureState.BlankingScan.ToString("d"))
                }));
                if (!string.IsNullOrEmpty(result))
                {
                    ApiResultModel apiresmodel = TDCommom.ObjectExtensions.FromJsonString<ApiResultModel>(result);
                    if (apiresmodel.Data != null)
                    {
                        List<ChuckingApplianceInfoModel> list = TDCommom.ObjectExtensions.FromJsonString<List<ChuckingApplianceInfoModel>>(apiresmodel.Data.ToString());
                        if (list.Count > 0)
                        {
                            leftBlankFixlist = list.Where(x => x.FixPosition == "A").FirstOrDefault();
                            rightBlankFixlist = list.Where(x => x.FixPosition == "B").FirstOrDefault();
                        }

                    }
                }
                if (leftBlankFixlist != null && leftBlankFixlist.CAId > 0)
                {
                    OmronTCP.WriteSingleD(RobotSocket, plcAddress.LeftBlankCellCount, 0);
                }
            }

            if (OmronTCP.ReadSingleD(RobotSocket, plcAddress.RightBlankFixState) == 1)//判断右下料是否有夹具
            {
                if (rightBlankFixlist != null && rightBlankFixlist.CAId > 0)
                {
                    OmronTCP.WriteSingleD(RobotSocket, plcAddress.RightBlankCellCount, 0);
                }
            }
        }



        void BlankOver()
        {
            if (RobotIPlist[0].IsConn)
            {
                if (OmronTCP.ReadSingleD(RobotSocket, plcAddress.LeftBlankFixState) == 3 && leftBlankFixlist != null && leftBlankFixlist.CAId > 0)//夹具下料完成 待RGV取走
                {
                    string parameter = TDCommom.ObjectExtensions.ToJsonString(new ChuckingApplianceInfoModel()
                    {
                        CAId = leftBlankFixlist.CAId,
                        CAState = int.Parse(TDCommon.SysEnum.FixtureState.ProcessEnd.ToString("d"))//待取料
                    });
                    chuckapi.UpdateState(parameter);//改变夹具的状态
                }

                if (OmronTCP.ReadSingleD(RobotSocket, plcAddress.RightBlankFixState) == 3 && rightBlankFixlist != null && rightBlankFixlist.CAId > 0)//夹具下料完成 待RGV取走
                {
                    string parameter = TDCommom.ObjectExtensions.ToJsonString(new ChuckingApplianceInfoModel()
                    {
                        CAId = rightBlankFixlist.CAId,
                        CAState = int.Parse(TDCommon.SysEnum.FixtureState.ProcessEnd.ToString("d"))//待取料
                    });
                    chuckapi.UpdateState(parameter);//改变夹具的状态
                }
            }
        }
        #endregion
    }
}