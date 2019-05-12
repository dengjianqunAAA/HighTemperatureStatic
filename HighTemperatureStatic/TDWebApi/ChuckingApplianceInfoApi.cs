using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDWebApi
{
    public class ChuckingApplianceInfoApi
    {
        TDWebApi.HttpClient hc = new HttpClient();
        /// <summary>
        /// 夹具表全部数据
        /// </summary>
        /// <returns></returns>
        public string GetCaiAllData()
        {
            string res = hc.Get(TDCommon.PublicInfo.WebAPIRoute + "ChuckingApplianceInfo/GetCaiAllData");
            return res;
        }
        /// <summary>
        /// 查询已入炉夹具信息
        /// </summary>
        /// <param name="data">序列化ApiModel数据</param>
        /// <returns></returns>
        public string GetCaiDataByState(string data)
        {
            string res = hc.Get(TDCommon.PublicInfo.WebAPIRoute + "ChuckingApplianceInfo/GetCaiDataByState?data= " + data + " ");
            return res;
        }

		public string UpdateFixtureInfo(string data)
        {
            return hc.Get(TDCommon.PublicInfo.WebAPIRoute + "ChuckingApplianceInfo/UpdateFixtureInfo?data="+data+"");
        }

        public string InsertFirstInfo(string data)
        {
            return hc.Post(TDCommon.PublicInfo.WebAPIRoute + "ChuckingApplianceInfo/InsertFirstInfo?data=" + data + "",data);
        }
        public string InsertInfo(string data)
        {
            return hc.Post(TDCommon.PublicInfo.WebAPIRoute + "ChuckingApplianceInfo/InsertInfo?data=" + data + "", data);
        }

        
        /// <summary>
        /// 查询入炉中夹具信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetInstoveInfoByState(string data)
        {
            return hc.Get(TDCommon.PublicInfo.WebAPIRoute + "ChuckingApplianceInfo/GetInstoveInfoByState?data=" + data + "");
        }

        /// <summary>
        /// 修改夹具状态
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string UpdateStateById(string data)
        {
            return hc.Get(TDCommon.PublicInfo.WebAPIRoute + "ChuckingApplianceInfo/UpdateStateById?data= " + data + " ");
        }

        /// <summary>
        /// 修改夹具状态
        /// </summary>
        /// <param name="data">实体类</param>
        /// <returns></returns>
        public string UpdateState(string data)
        {
            return hc.Post(TDCommon.PublicInfo.WebAPIRoute + "ChuckingApplianceInfo/UpdateState?data= " + data + " ", data);
        }

        /// <summary>
        /// 查询入炉中夹具信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetInfoByState(string data)
        {
            return hc.Get(TDCommon.PublicInfo.WebAPIRoute + "ChuckingApplianceInfo/GetInfoByState?data=" + data + "");
        }

        /// <summary>
        /// 获取出炉夹具信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetOutStoveInfo(string data)
        {
            return hc.Get(TDCommon.PublicInfo.WebAPIRoute + "ChuckingApplianceInfo/GetOutStoveInfo?data=" + data + "");
        }

        /// <summary>
        /// 根据id查询夹具信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetChuckInfoByid(string data)
        {
            return hc.Get(TDCommon.PublicInfo.WebAPIRoute + "ChuckingApplianceInfo/GetChuckInfoByid?data=" + data + "");
        }

        /// <summary>
        /// 修改备注值1
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string UpdateChuckInfoRemake1(string data)
        {
            return hc.Post(TDCommon.PublicInfo.WebAPIRoute + "ChuckingApplianceInfo/UpdateChuckInfoRemake1?data=" + data + "",data);
        }
    }
}
