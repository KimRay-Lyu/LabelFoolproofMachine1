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
using ConfigManager;

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

        public static CheckModel CheckModel = new CheckModel();
        public static CheckModel createNewCheckModel = new CheckModel();//Jason文件读取写入

        public static HTuple WindowsHandle = new HTuple();
        public static bool GetImage;
        public static int 调试模式;
        public static int OpenCrame;
    }

    public class SettingMessage
    {
        public SettingMessage()
        {
            CaremerName = "";
        }

        public string CaremerName { get; set; }
    }

    public class HalconModel
    {
        public HalconModel()
        {



        }
    }

    public class CheckModel
    {
        public CheckModel()
        {
            ModelImage = new HObject();
            VisualModelID = new HTuple();
            VisualModelRegion = new HObject();
            //ModelTransContours = new HObject();
            HOperatorSet.GenEmptyObj(out ModelImage);
            HOperatorSet.GenEmptyObj(out VisualModelRegion);
            //HOperatorSet.GenEmptyObj(out ModelTransContours);
            chickMineLableModel = new ChickMineLableModel();
            checkBigLableModel = new CheckBigLableModel();
            checkOtherModel = new CheckOtherModel();
        }
        [JsonIgnore]
        public HObject ModelImage;
        [JsonIgnore]
        public HTuple VisualModelID;
        [JsonIgnore]
        public HObject VisualModelRegion;
        //[JsonIgnore]
        //public HObject ModelTransContours;//定位出的轮廓

        public ChickMineLableModel chickMineLableModel;
        public CheckBigLableModel checkBigLableModel;
        public CheckOtherModel checkOtherModel;


        public void ReadModel(string sPath)
        {
            HOperatorSet.ReadShapeModel(sPath + "\\VisualModelID.shm", out VisualModelID);
            ModelImage.Dispose();
            HOperatorSet.ReadImage(out ModelImage, sPath + "\\ModelImage.bmp");
            VisualModelRegion.Dispose();
            HOperatorSet.ReadRegion(out VisualModelRegion, sPath + "\\VisualModelRegion.hobj");
            //ModelTransContours.Dispose();
            //HOperatorSet.ReadRegion(out ModelTransContours, sPath + "\\ModelTransContours.hobj");
            chickMineLableModel.ReadModel(sPath);
            checkBigLableModel.ReadModel(sPath);
            checkOtherModel.ReadModel(sPath);
        }

        public void WriteModel(string sPath)
        {
            HOperatorSet.WriteShapeModel(VisualModelID, sPath + "\\VisualModelID.shm");
            HOperatorSet.WriteImage(ModelImage, "bmp", 0, sPath + "\\ModelImage.bmp");
            HOperatorSet.WriteRegion(VisualModelRegion, sPath + "\\VisualModelRegion.hobj");
            //HOperatorSet.WriteRegion(ModelTransContours, sPath + "\\ModelTransContours.hobj");
            chickMineLableModel.WriteModel(sPath);
            checkBigLableModel.WriteModel(sPath);
            checkOtherModel.WriteModel(sPath);
        }
    }

    public class ChickMineLableModel
    {
        public ChickMineLableModel()
        {
            DistanceMin = 0.0f; ;
            DistanceMin1 = 0.0f; ;
            LableNothingRegion = new HObject();
            LableCircleRegion = new HObject();
            LableDistanceRegion1 = new HObject();
            LableDistanceRegion2 = new HObject();
            //SmallSelectedRegions = new HObject();
            //SmallLableMean = 0.0f;
            LableNothingMean = 0.0f;

        }
        public double DistanceMin;
        public double DistanceMin1;
        [JsonIgnore]
        public HObject LableNothingRegion;
        [JsonIgnore]
        public HObject LableCircleRegion;
        [JsonIgnore]
        public HObject LableDistanceRegion1;
        [JsonIgnore]
        public HObject LableDistanceRegion2;
        //[JsonIgnore]
        //public HObject SmallSelectedRegions;
        //public double SmallLableMean;//平均灰度
        public double LableNothingMean;//标签有无数量

        public void WriteModel(string sPath)
        {
            HOperatorSet.WriteRegion(LableNothingRegion, sPath + "\\LableNothingRegion.hobj");
            HOperatorSet.WriteRegion(LableCircleRegion, sPath + "\\LableCircleRegion.hobj");
            HOperatorSet.WriteRegion(LableDistanceRegion1, sPath + "\\LableDistanceRegion1.hobj");
            HOperatorSet.WriteRegion(LableDistanceRegion2, sPath + "\\LableDistanceRegion2.hobj");
            //HOperatorSet.WriteRegion(SmallSelectedRegions, sPath + "\\SmallSelectedRegions.hobj");
        }
        public void ReadModel(string sPath)
        {
            LableNothingRegion.Dispose();
            LableCircleRegion.Dispose();
            LableDistanceRegion1.Dispose();
            LableDistanceRegion2.Dispose();
            //SmallSelectedRegions.Dispose();
            HOperatorSet.ReadRegion(out LableNothingRegion, sPath + "\\LableNothingRegion.hobj");
            HOperatorSet.ReadRegion(out LableCircleRegion, sPath + "\\LableCircleRegion.hobj");
            HOperatorSet.ReadRegion(out LableDistanceRegion1, sPath + "\\LableDistanceRegion1.hobj");
            HOperatorSet.ReadRegion(out LableDistanceRegion2, sPath + "\\LableDistanceRegion2.hobj");
            // HOperatorSet.ReadRegion(out SmallSelectedRegions, sPath + "\\SmallSelectedRegions.hobj");

        }


    }
    public class CheckBigLableModel
    {
        public CheckBigLableModel()
        {
            BigLableAngleNumber1 = 0.0f;
            BigLableAngleNumber2 = 0.0f;
            BigLableNumber = 0.0f;
            BigLableAngleSelected1 = new HObject();
            BigLableAngleSelected2 = new HObject();
            BigLableAngleRegion1 = new HObject();
            BigLableAngleRegion2 = new HObject();
            BigLableRegion = new HObject();
            BigLableSelect = new HObject();
        }
        public double BigLableAngleNumber1;
        public double BigLableAngleNumber2;
        public double BigLableNumber;
        [JsonIgnore]
        public HObject BigLableAngleSelected1;
        [JsonIgnore]
        public HObject BigLableAngleSelected2;
        [JsonIgnore]
        public HObject BigLableAngleRegion1;
        [JsonIgnore]
        public HObject BigLableAngleRegion2;
        [JsonIgnore]
        public HObject BigLableRegion;
        [JsonIgnore]
        public HObject BigLableSelect;

        public void WriteModel(string sPath)
        {
            HOperatorSet.WriteRegion(BigLableAngleRegion1, sPath + "\\BigLableAngleRegion1.hobj");
            HOperatorSet.WriteRegion(BigLableAngleRegion2, sPath + "\\BigLableAngleRegion2.hobj");
            HOperatorSet.WriteRegion(BigLableAngleSelected1, sPath + "\\BigLableAngleSelected1.hobj");
            HOperatorSet.WriteRegion(BigLableAngleSelected2, sPath + "\\BigLableAngleSelected2.hobj");
            HOperatorSet.WriteRegion(BigLableRegion, sPath + "\\BigLableRegion.hobj");
            HOperatorSet.WriteRegion(BigLableSelect, sPath + "\\BigLableSelect.hobj");
        }
        public void ReadModel(string sPath)
        {
            BigLableAngleRegion1.Dispose();
            BigLableAngleRegion2.Dispose();
            BigLableAngleSelected1.Dispose();
            BigLableAngleSelected2.Dispose();
            BigLableRegion.Dispose();
            BigLableSelect.Dispose();
            HOperatorSet.ReadRegion(out BigLableAngleRegion1, sPath + "\\BigLableAngleRegion1.hobj");
            HOperatorSet.ReadRegion(out BigLableAngleRegion2, sPath + "\\BigLableAngleRegion2.hobj");
            HOperatorSet.ReadRegion(out BigLableAngleSelected1, sPath + "\\BigLableAngleSelected1.hobj");
            HOperatorSet.ReadRegion(out BigLableAngleSelected2, sPath + "\\BigLableAngleSelected2.hobj");
            HOperatorSet.ReadRegion(out BigLableRegion, sPath + "\\BigLableRegion.hobj");
            HOperatorSet.ReadRegion(out BigLableSelect, sPath + "\\BigLableSelect.hobj");

        }


    }
    public class CheckOtherModel
    {
        public CheckOtherModel()
        {
            OtherRegion = new HObject();
            OtherRegion1 = new HObject();
            //OtherSelect = new HObject();
            OtherNumber = 0.0f;
        }
        [JsonIgnore]
        public HObject OtherRegion;
        [JsonIgnore]
        public HObject OtherRegion1;
        //[JsonIgnore]
        //public HObject OtherSelect;
        public double OtherNumber;

        public void WriteModel(string sPath)
        {
            HOperatorSet.WriteRegion(OtherRegion, sPath + "\\OtherRegion.hobj");
            HOperatorSet.WriteRegion(OtherRegion1, sPath + "\\OtherRegion1.hobj");
            //HOperatorSet.WriteRegion(OtherSelect, sPath + "\\OtherSelect.hobj");

        }
        public void ReadModel(string sPath)
        {
            OtherRegion.Dispose();
            //OtherSelect.Dispose();
            OtherRegion1.Dispose();
            HOperatorSet.ReadRegion(out OtherRegion, sPath + "\\OtherRegion.hobj");
            HOperatorSet.ReadRegion(out OtherRegion1, sPath + "\\OtherRegion1.hobj");
            //HOperatorSet.ReadRegion(out OtherSelect, sPath + "\\OtherSelect.hobj");

        }
    }
}
