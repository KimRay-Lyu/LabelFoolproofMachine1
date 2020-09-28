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
                //获取相机图片
                //视觉定位
                //检测区域偏移过去
                //小标签检测
                //大标签检测
                //其他区域


                //ok: .....
                //ng: .....
                System.Console.WriteLine("。。。。。。。。。。。。。。");
                Thread.Sleep(10);
            }
        }
    }
}
