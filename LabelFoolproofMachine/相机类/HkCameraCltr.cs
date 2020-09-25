using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;
using MvCamCtrl.NET;

namespace HkCamera
{
    public class HkCameraCltr
    {
        private static MyCamera.MV_CC_DEVICE_INFO_LIST m_pDeviceList;
        private static List<MyCamera.MV_CC_DEVICE_INFO> CameraInfo = new List<MyCamera.MV_CC_DEVICE_INFO>();
        private MyCamera m_pMyCamera = new MyCamera();
        byte[] m_pDataForRed = new byte[20 * 1024 * 1024];
        byte[] m_pDataForGreen = new byte[20 * 1024 * 1024];
        byte[] m_pDataForBlue = new byte[20 * 1024 * 1024];
        private uint g_nPayloadSize = 0;
        private bool IsOpen = false;

        public static int EnumDevices()
        {
            int nRet;
            // ch:创建设备列表 || en: Create device list
            System.GC.Collect();           
            nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_pDeviceList);
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show("Enum Devices Fail");
                return 0;
            }
            for (int i = 0; i < m_pDeviceList.nDeviceNum; i++)
            {
                MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                bool sign = true;
                foreach(var temp in CameraInfo)
                {
                    IntPtr buffer1 = Marshal.UnsafeAddrOfPinnedArrayElement(temp.SpecialInfo.stGigEInfo, 0);
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo1 = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer1, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                    if(gigeInfo1.chUserDefinedName == gigeInfo.chUserDefinedName || gigeInfo1.chSerialNumber == gigeInfo.chSerialNumber)
                    {
                        sign = false;
                    }
                }
                if(sign == true)
                {
                    CameraInfo.Add(device);
                }
            }
            return (int)m_pDeviceList.nDeviceNum;
        }

        public int OpenDevices(string CamName)
        {
            if(IsOpen == true)
            {
                return 0;
            }
            foreach (var temp in CameraInfo)
            {
                IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(temp.SpecialInfo.stGigEInfo, 0);
                MyCamera.MV_GIGE_DEVICE_INFO gigeInfo1 = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                if (gigeInfo1.chUserDefinedName == CamName || gigeInfo1.chSerialNumber == CamName)
                {
                    int nRet = -1;
                    MyCamera.MV_CC_DEVICE_INFO device = temp;

                    bool bRes=MyCamera.MV_CC_IsDeviceAccessible_NET(ref device, 1);
                    if(bRes == false)
                    {
                        return 1;
                    }

                    nRet = m_pMyCamera.MV_CC_CreateDevice_NET(ref device);
                    if (MyCamera.MV_OK != nRet)
                    {
                        return 2;
                    }
                    nRet = m_pMyCamera.MV_CC_OpenDevice_NET();
                    if (MyCamera.MV_OK != nRet)
                    {
                        //MessageBox.Show("Open Device Fail");
                        return 3;
                    }
                    MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
                    // ch:获取包大小 || en: Get Payload Size                 
                    nRet = m_pMyCamera.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);
                    if (MyCamera.MV_OK != nRet)
                    {
                        MessageBox.Show("Get PayloadSize Fail");
                        return 4;
                    }
                    g_nPayloadSize = stParam.nCurValue;

                    // ch:获取高 || en: Get Height
                    nRet = m_pMyCamera.MV_CC_GetIntValue_NET("Height", ref stParam);
                    if (MyCamera.MV_OK != nRet)
                    {                        
                        return 5;
                    }
                    uint nHeight = stParam.nCurValue;

                    // ch:获取宽 || en: Get Width
                    nRet = m_pMyCamera.MV_CC_GetIntValue_NET("Width", ref stParam);
                    if (MyCamera.MV_OK != nRet)
                    {                       
                        return 6;
                    }
                    uint nWidth = stParam.nCurValue;

                    m_pDataForRed = new byte[nWidth * nHeight];
                    m_pDataForGreen = new byte[nWidth * nHeight];
                    m_pDataForBlue = new byte[nWidth * nHeight];
                    nRet = m_pMyCamera.MV_CC_StartGrabbing_NET();
                    if (MyCamera.MV_OK != nRet)
                    {
                        return 7;
                    }
                    IsOpen = true;
                    return 0;
                }            
            }
            return -1;
        }

        public void CloseDevices()
        {
            int nRet;

            nRet = m_pMyCamera.MV_CC_CloseDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                return;
            }

            nRet = m_pMyCamera.MV_CC_DestroyDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                return;
            }
        }

        public int DoSoftwareOnce()
        {
            int nRet;
            nRet = m_pMyCamera.MV_CC_SetCommandValue_NET("TriggerSoftware");
            
            if (MyCamera.MV_OK != nRet)
            {
                return 1;
            }
            return 0;
        }

        public int Capture( out HObject Himage)
        {
            HOperatorSet.GenEmptyObj(out Himage);
            Himage.Dispose();
            int nRet = MyCamera.MV_OK;
            MyCamera device = m_pMyCamera as MyCamera;
            MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();
            IntPtr pData = Marshal.AllocHGlobal((int)g_nPayloadSize * 3);

            if (pData == IntPtr.Zero)
            {
                return 1;
            }
            IntPtr pImageBuffer = Marshal.AllocHGlobal((int)g_nPayloadSize * 3);
            if (pImageBuffer == IntPtr.Zero)
            {
                return 2;
            }

            uint nDataSize = g_nPayloadSize * 3;
            HObject Hobj = new HObject();
            IntPtr RedPtr = IntPtr.Zero;
            IntPtr GreenPtr = IntPtr.Zero;
            IntPtr BluePtr = IntPtr.Zero;
            IntPtr pTemp = IntPtr.Zero;

            nRet = device.MV_CC_GetOneFrameTimeout_NET(pData, nDataSize, ref pFrameInfo, 1000);
            if (MyCamera.MV_OK == nRet)
            {
                if (IsColorPixelFormat(pFrameInfo.enPixelType))
                {
                    if (pFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed)
                    {
                        pTemp = pData;
                    }
                    else
                    {
                        nRet = ConvertToRGB(m_pMyCamera, pData, pFrameInfo.nHeight, pFrameInfo.nWidth, pFrameInfo.enPixelType, pImageBuffer);
                        if (MyCamera.MV_OK != nRet)
                        {
                            Marshal.FreeHGlobal(pImageBuffer);
                            Marshal.FreeHGlobal(pData);
                            return 3;
                        }
                        pTemp = pImageBuffer;
                    }

                    unsafe
                    {
                        byte* pBufForSaveImage = (byte*)pTemp;

                        UInt32 nSupWidth = (pFrameInfo.nWidth + (UInt32)3) & 0xfffffffc;

                        for (int nRow = 0; nRow < pFrameInfo.nHeight; nRow++)
                        {
                            for (int col = 0; col < pFrameInfo.nWidth; col++)
                            {
                                m_pDataForRed[nRow * nSupWidth + col] = pBufForSaveImage[nRow * pFrameInfo.nWidth * 3 + (3 * col)];
                                m_pDataForGreen[nRow * nSupWidth + col] = pBufForSaveImage[nRow * pFrameInfo.nWidth * 3 + (3 * col + 1)];
                                m_pDataForBlue[nRow * nSupWidth + col] = pBufForSaveImage[nRow * pFrameInfo.nWidth * 3 + (3 * col + 2)];
                            }
                        }
                    }

                    RedPtr = Marshal.UnsafeAddrOfPinnedArrayElement(m_pDataForRed, 0);
                    GreenPtr = Marshal.UnsafeAddrOfPinnedArrayElement(m_pDataForGreen, 0);
                    BluePtr = Marshal.UnsafeAddrOfPinnedArrayElement(m_pDataForBlue, 0);

                    try
                    {
                        HOperatorSet.GenImage3Extern(out Hobj, (HTuple)"byte", pFrameInfo.nWidth, pFrameInfo.nHeight,
                                           (new HTuple(RedPtr)), (new HTuple(GreenPtr)), (new HTuple(BluePtr)), IntPtr.Zero);
                    }
                    catch (HalconException ex)
                    {
                        Marshal.FreeHGlobal(pImageBuffer);
                        Marshal.FreeHGlobal(pData);
                        return 4;
                    }
                }
                else if (IsMonoPixelFormat(pFrameInfo.enPixelType))
                {
                    if (pFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
                    {
                        pTemp = pData;
                    }
                    else
                    {
                        nRet = ConvertToMono8(device, pData, pImageBuffer, pFrameInfo.nHeight, pFrameInfo.nWidth, pFrameInfo.enPixelType);
                        if (MyCamera.MV_OK != nRet)
                        {
                            Marshal.FreeHGlobal(pImageBuffer);
                            Marshal.FreeHGlobal(pData);
                            return 5;
                        }
                        pTemp = pImageBuffer;
                    }
                    try
                    {
                        HOperatorSet.GenImage1Extern(out Hobj, "byte", pFrameInfo.nWidth, pFrameInfo.nHeight, pTemp, IntPtr.Zero);
                    }
                    catch (HalconException ex)
                    {
                        Marshal.FreeHGlobal(pImageBuffer);
                        Marshal.FreeHGlobal(pData);
                        return 6;
                    }
                }

                Himage = Hobj.Clone();
                Hobj.Dispose();
            }
            Marshal.FreeHGlobal(pImageBuffer);
            Marshal.FreeHGlobal(pData);
            return 0;
        }

        public int SetExposureTime(float value)
        {
            m_pMyCamera.MV_CC_SetEnumValue_NET("ExposureAuto", 0);
            int nRet = m_pMyCamera.MV_CC_SetFloatValue_NET("ExposureTime", value);
            return nRet;
        }

        private bool IsColorPixelFormat(MyCamera.MvGvspPixelType enType)
        {
            switch (enType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BGR8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_RGBA8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BGRA8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_YUYV_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12_Packed:
                    return true;
                default:
                    return false;
            }
        }

        public static Int32 ConvertToRGB(object obj, IntPtr pSrc, ushort nHeight, ushort nWidth, MyCamera.MvGvspPixelType nPixelType, IntPtr pDst)
        {
            if (IntPtr.Zero == pSrc || IntPtr.Zero == pDst)
            {
                return MyCamera.MV_E_PARAMETER;
            }
            if (obj==null)
            {
                return -1;
            }
            int nRet = MyCamera.MV_OK;
            MyCamera device = obj as MyCamera;
            MyCamera.MV_PIXEL_CONVERT_PARAM stPixelConvertParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();

            stPixelConvertParam.pSrcData = pSrc;//源数据
            if (IntPtr.Zero == stPixelConvertParam.pSrcData)
            {
                return -1;
            }

            stPixelConvertParam.nWidth = nWidth;//图像宽度
            stPixelConvertParam.nHeight = nHeight;//图像高度
            stPixelConvertParam.enSrcPixelType = nPixelType;//源数据的格式
            stPixelConvertParam.nSrcDataLen = (uint)(nWidth * nHeight * ((((uint)nPixelType) >> 16) & 0x00ff) >> 3);

            stPixelConvertParam.nDstBufferSize = (uint)(nWidth * nHeight * ((((uint)MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed) >> 16) & 0x00ff) >> 3);
            stPixelConvertParam.pDstBuffer = pDst;//转换后的数据
            stPixelConvertParam.enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
            stPixelConvertParam.nDstBufferSize = (uint)nWidth * nHeight * 3;

            nRet = device.MV_CC_ConvertPixelType_NET(ref stPixelConvertParam);//格式转换
            if (MyCamera.MV_OK != nRet)
            {
                return -1;
            }

            return MyCamera.MV_OK;
        }

        public static Int32 ConvertToMono8(object obj, IntPtr pInData, IntPtr pOutData, ushort nHeight, ushort nWidth, MyCamera.MvGvspPixelType nPixelType)
        {
            if (IntPtr.Zero == pInData || IntPtr.Zero == pOutData)
            {
                return MyCamera.MV_E_PARAMETER;
            }
            if(obj == null)
            {
                return -1;
            }

            int nRet = MyCamera.MV_OK;
            MyCamera device = obj as MyCamera;
            MyCamera.MV_PIXEL_CONVERT_PARAM stPixelConvertParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();

            stPixelConvertParam.pSrcData = pInData;//源数据
            if (IntPtr.Zero == stPixelConvertParam.pSrcData)
            {
                return -1;
            }

            stPixelConvertParam.nWidth = nWidth;//图像宽度
            stPixelConvertParam.nHeight = nHeight;//图像高度
            stPixelConvertParam.enSrcPixelType = nPixelType;//源数据的格式
            stPixelConvertParam.nSrcDataLen = (uint)(nWidth * nHeight * ((((uint)nPixelType) >> 16) & 0x00ff) >> 3);

            stPixelConvertParam.nDstBufferSize = (uint)(nWidth * nHeight * ((((uint)MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed) >> 16) & 0x00ff) >> 3);
            stPixelConvertParam.pDstBuffer = pOutData;//转换后的数据
            stPixelConvertParam.enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8;
            stPixelConvertParam.nDstBufferSize = (uint)(nWidth * nHeight * 3);

            nRet = device.MV_CC_ConvertPixelType_NET(ref stPixelConvertParam);//格式转换
            if (MyCamera.MV_OK != nRet)
            {
                return -1;
            }

            return nRet;
        }

        private bool IsMonoPixelFormat(MyCamera.MvGvspPixelType enType)
        {
            switch (enType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                    return true;
                default:
                    return false;
            }
        }
    }
}
