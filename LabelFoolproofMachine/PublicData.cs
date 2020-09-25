using HkCamera;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabelFoolproofMachine.Halcon;
using HalconDotNet;

namespace LabelFoolproofMachine
{
    public static class PublicData
    {
        /// <summary>
        /// 相机对象
        /// </summary>
        public static HkCameraCltr hkCameraCltr = new HkCameraCltr();

        public static SettingMessage settingMessage = new SettingMessage();
        public static HalconModel HalconModel = new HalconModel();
    }
    public class SettingMessage
    {
        public SettingMessage()
        {
            定位模板保存地址 = "";
            CaremerName = "";
        }

        public string CaremerName { get; set; }
        public string 定位模板保存地址 { get; set; }
    }
    public class HalconModel
    {
        public HalconModel()
        {
            Area = "";
         

        }
        public HTuple Area { get; set; }
        public HObject 画框后生成的region { get; set; }
    }
}
