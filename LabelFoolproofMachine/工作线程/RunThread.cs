using HalconDotNet;
using LabelFoolproofMachine.Halcon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace LabelFoolproofMachine
{
    public class RunThread
    {
        private bool ThreadExit = false;
        private Thread thread;
        


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
            while (ThreadExit == false)
            {
                if (0 == PublicData.hkCameraCltr.DoSoftwareOnce())
                {
                    if (0 == PublicData.hkCameraCltr.Capture(out HObject CameraImage))
                    {
                        Form1 Form = new Form1();
                        HObject TranContours;
                        HalconCommonFunc.FindShapedModel(Form.WindowsHandle, CameraImage, PublicData.CheckModel.VisualModelID,
                            out TranContours);
                        HalconCommonFunc.CheckBigLable(CameraImage, out HObject AnglehObject, out HObject AnglehObject1,
                            out HObject IntervalLable, out HTuple AnglehObjectNumber, out HTuple AnglehObjectNumber1, out HTuple IntervalLableNumber);
                        //
                        HalconCommonFunc.CheckSmallLable(CameraImage, out HObject NothingRegion, out HObject CircleRegion,
                            out HObject DistanceRegion1, out HObject DistanceRegion2, out HTuple NothingRegionNumber, out HTuple Mean,
                            out HTuple Distance1, out HTuple Distance2, out HObject Edges1, out HObject Edges2);
                        HalconCommonFunc.CheckOtherLable(CameraImage, out HObject OtherhObject, out HTuple OtherhNumber);

                        //获取相机图片
                        //视觉定位
                        //检测区域偏移过去
                        //小标签检测
                        //大标签检测
                        //其他区域


                        //ok: .....
                        //ng: .....
                        //System.Console.WriteLine("。。。。。。。。。。。。。。");
                        //Thread.Sleep(10);
                    }
                }
            }
        }
    }
}
