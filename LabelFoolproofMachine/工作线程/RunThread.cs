using HalconDotNet;
using LabelFoolproofMachine.Halcon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabelFoolproofMachine
{
    public class RunThread
    {
        private bool ThreadExit = false;
        public bool CheckModel = true;
        private Thread thread;

        public event EventHandler<TheadWorkResultEventArgs> TheadWorkResultEvent; //定义一个委托类型的事件 
        public int currentCount = 0;
        //public HObject CameraImage;

        public void StartWork()
        {

            ThreadExit = false;
            thread = new Thread(WorkThread);
            thread.Start();


        }

        public void StopWork()
        {
            ThreadExit = true;
        }


        private void WorkThread()
        {

            HTuple Socre = null;
            HTuple CircleNumber;
            HObject CircleRegion;
            HObject DistanceRegion1;
                HObject DistanceRegion2;
            double 标签尺寸1;
            double 标签尺寸2;
            HObject Edges1;
                HObject Edges2;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Restart();//开始计时
            while (ThreadExit == false)
            {
                if (PublicData.调试模式 == 2 || PublicData.调试模式 == 0)
                {
                    HOperatorSet.GenEmptyObj(out PublicData.CheckModel.ModelImage);
                    PublicData.CheckModel.ModelImage.Dispose();
                    if (0 != PublicData.hkCameraCltr.Capture(out PublicData.CheckModel.ModelImage))
                    {
                        TheadWorkResultEvent?.Invoke(this, new TheadWorkResultEventArgs { nResult = 1, sResult = "找不到图片" });
                        break;
                    }
                }
                HalconCommonFunc.DisplayImage(PublicData.CheckModel.ModelImage, PublicData.WindowsHandle);
                HObject TranContours;
                try
                {
                    HalconCommonFunc.FindShapedModel(PublicData.WindowsHandle, PublicData.CheckModel.ModelImage, PublicData.CheckModel.VisualModelID,
                out TranContours, out Socre);
                }
                catch
                {
                    TheadWorkResultEvent?.Invoke(this, new TheadWorkResultEventArgs { nResult = 1, sResult = "找不到图片" });
                    break;
                }
                if (stopwatch.ElapsedMilliseconds > 500)
                {
                    TheadWorkResultEvent?.Invoke(this, new TheadWorkResultEventArgs { nResult = 1, sResult = "找不到大标签" });
                    break;
                }

                HOperatorSet.GenRegionContourXld(TranContours, out HObject region, "filled");
                HOperatorSet.Union1(region, out HObject RegionUnion);
                HOperatorSet.AreaCenter(RegionUnion, out HTuple area, out HTuple row, out HTuple column);
                region.Dispose();
                RegionUnion.Dispose();
                if (Socre < 0.6 || row < 400 || row > 800)
                {
                    Thread.Sleep(10);
                    //TheadWorkResultEvent?.Invoke(this, new TheadWorkResultEventArgs { nResult = 1, sResult = "定位失败" });
                    continue;
                }
                HalconCommonFunc.DisplayRegionOrXld(TranContours, "green", PublicData.WindowsHandle, 2);
                HalconCommonFunc.CheckBigLable(PublicData.CheckModel.ModelImage, out HObject AnglehObject, out HObject AnglehObject1,
                out HObject IntervalLable, out HTuple AnglehObjectNumber, out HTuple AnglehObjectNumber1, out HTuple IntervalLableNumber);

                if (AnglehObjectNumber < 1 || AnglehObjectNumber1 < 1)
                {
                    TheadWorkResultEvent?.Invoke(this, new TheadWorkResultEventArgs { nResult = 2, sResult = "没有大标签角" });
                    Thread.Sleep(1000);
                    break;
                }
                HalconCommonFunc.DisplayRegionOrXld(AnglehObject, "green", PublicData.WindowsHandle, 2);
                HalconCommonFunc.DisplayRegionOrXld(AnglehObject1, "green", PublicData.WindowsHandle, 2);
                if (IntervalLableNumber > 0)
                {
                    TheadWorkResultEvent?.Invoke(this, new TheadWorkResultEventArgs { nResult = 3, sResult = "大标签间隙间有标签" });
                    HalconCommonFunc.DisplayRegionOrXld(IntervalLable, "red", PublicData.WindowsHandle, 2);
                    Thread.Sleep(1000);
                    break;
                }
                HalconCommonFunc.DisplayRegionOrXld(IntervalLable, "green", PublicData.WindowsHandle, 2);
                HalconCommonFunc.AffineModel(PublicData.CheckModel.chickMineLableModel.LableNothingRegion, out HObject NothingRegion);
                HalconCommonFunc.SmallLableNothing(PublicData.CheckModel.ModelImage, NothingRegion, out HTuple NothingRegionMean);
                if (NothingRegionMean > 110)
                {
                    TheadWorkResultEvent?.Invoke(this, new TheadWorkResultEventArgs { nResult = 4, sResult = "没有小标签" });
                    HalconCommonFunc.DisplayRegionOrXld(NothingRegion, "red", PublicData.WindowsHandle, 2);
                    Thread.Sleep(1000);
                    break;
                }
                HalconCommonFunc.DisplayRegionOrXld(NothingRegion, "green", PublicData.WindowsHandle, 2);
                try
                {
                    
                    HalconCommonFunc.CheckSmallLable(PublicData.CheckModel.ModelImage, out  CircleRegion,
                   out  DistanceRegion1, out  DistanceRegion2, out CircleNumber,
                   out  标签尺寸1, out  标签尺寸2, out  Edges1, out  Edges2);
                }
                catch (Exception ex)
                {
                    TheadWorkResultEvent?.Invoke(this, new TheadWorkResultEventArgs { nResult = 4, sResult = ex.ToString() });
                    break;
                }
               
                if (CircleNumber>0)
                {
                    TheadWorkResultEvent?.Invoke(this, new TheadWorkResultEventArgs { nResult = 5, sResult = "小标签圆弧有未切割好的纸" });
                    HalconCommonFunc.DisplayRegionOrXld(CircleRegion, "red", PublicData.WindowsHandle, 2);
                    Thread.Sleep(1000);
                    break;
                }
                HalconCommonFunc.DisplayRegionOrXld(CircleRegion, "green", PublicData.WindowsHandle, 2);
                if (标签尺寸1 > 10.15 || 标签尺寸1 < 9.42)
                {
                    TheadWorkResultEvent?.Invoke(this, new TheadWorkResultEventArgs { nResult = 6, sResult = "小标签宽度不对" });
                    HalconCommonFunc.DisplayRegionOrXld(DistanceRegion1, "red", PublicData.WindowsHandle, 2);
                    Thread.Sleep(1000);
                    break;
                }
                if (标签尺寸2 > 10.15 || 标签尺寸2 < 9.42)
                {
                    TheadWorkResultEvent?.Invoke(this, new TheadWorkResultEventArgs { nResult = 6, sResult = "小标签宽度不对" });
                    HalconCommonFunc.DisplayRegionOrXld(DistanceRegion2, "red", PublicData.WindowsHandle, 2);
                    Thread.Sleep(1000);
                    break;
                }
                if (标签尺寸1- 标签尺寸2<-0.3|| 标签尺寸1 - 标签尺寸2 >0.3)
                {
                    TheadWorkResultEvent?.Invoke(this, new TheadWorkResultEventArgs { nResult = 6, sResult = "小标签倾斜" });
                    
                    Thread.Sleep(1000);
                    break;
                }
                HalconCommonFunc.DisplayRegionOrXld(DistanceRegion1, "green", PublicData.WindowsHandle, 2);
                HalconCommonFunc.DisplayRegionOrXld(DistanceRegion2, "green", PublicData.WindowsHandle, 2);
                HalconCommonFunc.DisplayRegionOrXld(Edges1, "green", PublicData.WindowsHandle, 2);
                HalconCommonFunc.DisplayRegionOrXld(Edges2, "green", PublicData.WindowsHandle, 2);
                HalconCommonFunc.CheckOtherLable(PublicData.CheckModel.ModelImage, out HObject OtherhObject, out HObject OtherhObject1, out HTuple 左边灰度值, out HTuple 右边灰度值);
                //HalconCommonFunc.DisplayImage(CameraImage, PublicData.WindowsHandle);
                if (左边灰度值 <253 || 右边灰度值 <253)
                {
                    TheadWorkResultEvent?.Invoke(this, new TheadWorkResultEventArgs { nResult = 7, sResult = "标签两边有多余的纸" });
                    Thread.Sleep(1000);
                    break;
                }

                HalconCommonFunc.DisplayRegionOrXld(OtherhObject, "green", PublicData.WindowsHandle, 2);
                HalconCommonFunc.DisplayRegionOrXld(OtherhObject1, "green", PublicData.WindowsHandle, 2);

                stopwatch.Restart();
                if (PublicData.调试模式 == 1 || PublicData.调试模式 == 2)
                {
                    break;
                }
            }
        }







        public class TheadWorkResultEventArgs
        {
            public int nResult;
            public string sResult;
        }

    }
}

