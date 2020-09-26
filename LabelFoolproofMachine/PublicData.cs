using HkCamera;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabelFoolproofMachine.Halcon;
using HalconDotNet;
using Newtonsoft;
using Newtonsoft.Json;

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

        public static ChickModel chickModel = new ChickModel();
        public static ChickModel createNewChickModel = new ChickModel();
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

    public class ChickModel
    {
        public ChickModel()
        {
            ModelImage = new HObject();
            VisualModelID = new HTuple();
            VisualModelRegion = new HObject();
            HOperatorSet.GenEmptyObj(out ModelImage);
            HOperatorSet.GenEmptyObj(out VisualModelRegion);
        }
        [JsonIgnore]
        public HObject ModelImage;
        [JsonIgnore]
        public HTuple VisualModelID;
        [JsonIgnore]
        public HObject VisualModelRegion;


        public void ReadModel(string sPath)
        {
            HOperatorSet.ReadShapeModel(sPath + "\\VisualModelID.shm", out VisualModelID);
            ModelImage.Dispose();
            HOperatorSet.ReadImage(out ModelImage, sPath + "\\ModelImage.bmp");
            VisualModelRegion.Dispose();
            HOperatorSet.ReadRegion(out VisualModelRegion, sPath + "\\VisualModelRegion.hobj");
        }

        public void WriteModel(string sPath)
        {
            HOperatorSet.WriteShapeModel(VisualModelID, sPath + "\\VisualModelID.shm");
            HOperatorSet.WriteImage(ModelImage, "bmp", 0, sPath + "\\ModelImage.bmp");
            HOperatorSet.WriteRegion(VisualModelRegion, sPath + "\\VisualModelRegion.hobj");
        }
    }
}
