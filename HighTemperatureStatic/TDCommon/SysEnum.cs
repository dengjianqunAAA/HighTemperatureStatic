using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDCommon
{
    public class SysEnum
    {    /// <summary>
         /// 炉子状态
         /// </summary>
        public enum StoveStateType
        {
            [Description("生产中")]
            Working = 0,
            [Description("待机中")]
            Standby = 1,
            [Description("报警中")]
            Alarm = 2,
        }
        /// <summary>
        /// 是否可用
        /// </summary>
        public enum YesOrNoType
        {
            [Description("是")]
            Yes = 1,
            [Description("否")]
            No = 0,
        }
        public enum CavityStateType
        {
            [Description("禁用")]
            forbidden = 10,
            [Description("空闲")]
            Leisure = 20,
            [Description("运行")]
            Run = 30,
            [Description("异常")]
            Error = 40,
        }


        public enum FixtureState
        {
            /// <summary>
            /// 空夹具扫码
            /// </summary>
            EmptyFixtureScan =0,
            /// <summary>
            /// 电芯绑定中
            /// </summary>
            CodeBinding=10,
            /// <summary>
            /// 电芯绑定完成
            /// </summary>
            CodeBindEnd=20,
            /// <summary>
            /// 待取料
            /// </summary>
            TobeReclaimed=25,
            /// <summary>
            /// 夹具入炉中
            /// </summary>
            Instoveing =30,
            /// <summary>
            /// 夹具已入炉
            /// </summary>
            InstoveEnd=40,
            /// <summary>
            /// 夹具出炉中
            /// </summary>
            Outstoveing=50,
            /// <summary>
            /// 夹具已出炉
            /// </summary>
            OutstoveEnd=60,
            /// <summary>
            /// 待入下料缓存
            /// </summary>
            UnloadedBuffer=65,
            /// <summary>
            /// 下料缓存
            /// </summary>
            BlankingBuff = 70,

            /// <summary>
            /// 待入下料扫码
            /// </summary>
            UnloadedBlankingScan=75,

            /// <summary>
            /// 下料扫码
            /// </summary>
            BlankingScan =80,
            
            /// <summary>
            /// 夹具流程结束
            /// </summary>
            ProcessEnd =90,
        }


        public static int GetFixtureStateInt(string state)
        {
            switch (state)
            {
                case "EmptyFixtureScan":
                    return 0;
                case "CodeBinding":
                    return 10;
                case "CodeBindEnd":
                    return 20;
                case "TobeReclaimed":
                    return 25;
                case "Instoveing":
                    return 30;
                case "InstoveEnd":
                    return 40;
                case "Outstoveing":
                    return 50;
                case "OutstoveEnd":
                    return 60;
                case "UnloadedBuffer":
                    return 65;
                case "BlankingBuff":
                    return 70;
                case "UnloadedBlankingScan":
                    return 75;
                case "BlankingScan":
                    return 80;
                
                case "ProcessEnd":
                    return 90;
            }
            return -1;
        }


        public static string GetFixtureStateString(string state)
        {
            switch (state)
            {
                case "EmptyFixtureScan":
                    return "空夹具扫码";
                case "CodeBinding":
                    return "电芯绑定中";
                case "CodeBindEnd":
                    return "电芯绑定完成";
                case "TobeReclaimed":
                    return "待取料";
                case "Instoveing":
                    return "夹具入炉中";
                case "InstoveEnd":
                    return "夹具已入炉";
                case "Outstoveing":
                    return "夹具出炉中";
                case "OutstoveEnd":
                    return "夹具已出炉";
                case "UnloadedBuffer":
                    return "待入下料缓存";
                case "BlankingBuff":
                    return "下料缓存";
                case "UnloadedBlankingScan":
                    return "待入下料扫码";
                case "BlankingScan":
                    return "电芯下料扫码";
                case "ProcessEnd":
                    return "流程结束";
            }
            return "";
        }

    }
}
