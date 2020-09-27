using HalconDotNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabelFoolproofMachine.Halcon
{
    public class PublicModelParam
    {
        public PublicModelParam()
        {
            HOperatorSet.GenEmptyObj(out modelRegion);
            HOperatorSet.GenEmptyObj(out createModelImage);
            HOperatorSet.GenEmptyObj(out Image);
            HOperatorSet.GenEmptyObj(out TestImage);

            DetermineModelParamArr.Clear();
            DetermineModelParamArr.Add("num_levels", 1);
            DetermineModelParamArr.Add("angle_start", 0);
            DetermineModelParamArr.Add("angle_extent", 360);
            DetermineModelParamArr.Add("angle_step", 0.1127);
            DetermineModelParamArr.Add("scale_min", 1.00);
            DetermineModelParamArr.Add("scale_max", 1.00);
            DetermineModelParamArr.Add("scale_step", 0.0078);
            DetermineModelParamArr.Add("optimization", "point_reduction_low");
            DetermineModelParamArr.Add("metric", "use_polarity");
            DetermineModelParamArr.Add("contrast_low", 1);
            DetermineModelParamArr.Add("contrast_high", 1);
            DetermineModelParamArr.Add("min_size", 1);
            DetermineModelParamArr.Add("min_contrast", 1);

            ParamIsAutoArr.Clear();
            ParamIsAutoArr.Add("num_levels", true);
            ParamIsAutoArr.Add("angle_step", true);
            ParamIsAutoArr.Add("scale_step", true);
            ParamIsAutoArr.Add("optimization", true);
            ParamIsAutoArr.Add("contrast", true);
            ParamIsAutoArr.Add("min_size", true);
            ParamIsAutoArr.Add("min_contrast", true);

            FindModelParamArr.Clear();
            FindModelParamArr.Add("AngleStart", 0);
            FindModelParamArr.Add("AngleExtent", 360);
            FindModelParamArr.Add("ScaleMin", 1.00);
            FindModelParamArr.Add("ScaleMax", 1.00);
            FindModelParamArr.Add("MinScore", 0.5);
            FindModelParamArr.Add("NumMatches", 0);
            FindModelParamArr.Add("MaxOverlap", 0.5);
            FindModelParamArr.Add("SubPixel", "least_squares");
            FindModelParamArr.Add("NumLevels", 1);
            FindModelParamArr.Add("MaxLevels", 1);
            FindModelParamArr.Add("Greediness", 0.75);
            FindModelParamArr.Add("MaxDeformation", 0);
        }

        private HObject modelRegion = new HObject();
        public HObject ModelRegion
        {
            get { return modelRegion; }
            set
            {
                if (value != null)
                {
                    modelRegion = value.Clone();
                }
            }
        }

        public HObject createModelImage = new HObject();

        public HTuple shapeModelID = new HTuple();
        public HObject Image = new HObject();
        public HObject TestImage = new HObject();
        public HTuple SmoothValue = new HTuple(0.5);
        public float ExposureTime = 10000.0F;

        //存储创建模板的参数
        public Hashtable DetermineModelParamArr = new Hashtable();
        public Hashtable ParamIsAutoArr = new Hashtable { };
        //存储寻找模板的参数
        public Hashtable FindModelParamArr = new Hashtable();

        public void ClearModelRegion()
        {
            modelRegion.Dispose();
            HOperatorSet.GenEmptyObj(out modelRegion);
        }



        private void DetermineShapeModelAtuoParams(HObject modelImage)
        {
            HTuple hParamters = new HTuple();
            HTuple hIsAutoNumLevels = new HTuple();
            HTuple hIsAutoOptimization = new HTuple();
            HTuple hIsAutoContrast = new HTuple();
            HTuple hIsAutoMinContrast = new HTuple();
            if ((bool)ParamIsAutoArr["num_levels"])
            {
                hIsAutoNumLevels = "auto";
                HOperatorSet.TupleConcat(hParamters, "num_levels", out hParamters);
            }
            else
            {
                hIsAutoNumLevels = Convert.ToInt32(DetermineModelParamArr["num_levels"]);
            }

            if ((bool)ParamIsAutoArr["optimization"])
            {
                hIsAutoOptimization = "auto";
                HOperatorSet.TupleConcat(hParamters, "optimization", out hParamters);
            }
            else
            {
                hIsAutoOptimization = (string)DetermineModelParamArr["optimization"];
            }

            if ((bool)ParamIsAutoArr["contrast"])
            {
                HOperatorSet.TupleConcat(hParamters, "contrast_hyst", out hParamters);
                HOperatorSet.TupleConcat(hIsAutoContrast, "auto_contrast_hyst", out hIsAutoContrast);
            }
            else
            {
                HOperatorSet.TupleConcat(hIsAutoContrast, Convert.ToInt32(DetermineModelParamArr["contrast_low"]), out hIsAutoContrast);
                HOperatorSet.TupleConcat(hIsAutoContrast, Convert.ToInt32(DetermineModelParamArr["contrast_high"]), out hIsAutoContrast);
            }


            if ((bool)ParamIsAutoArr["min_size"])
            {
                HOperatorSet.TupleConcat(hParamters, "min_size", out hParamters);
                HOperatorSet.TupleConcat(hIsAutoContrast, "auto_min_size", out hIsAutoContrast);
            }
            else
            {
                HOperatorSet.TupleConcat(hIsAutoContrast, Convert.ToInt32(DetermineModelParamArr["min_size"]), out hIsAutoContrast);
            }


            if ((bool)ParamIsAutoArr["min_contrast"])
            {
                HOperatorSet.TupleConcat(hParamters, "min_contrast", out hParamters);
                HOperatorSet.TupleConcat(hIsAutoMinContrast, "auto", out hIsAutoMinContrast);
            }
            else
            {
                HOperatorSet.TupleConcat(hIsAutoMinContrast, Convert.ToInt32(DetermineModelParamArr["min_contrast"]), out hIsAutoMinContrast);
            }


            if ((bool)ParamIsAutoArr["angle_step"])
            {
                HOperatorSet.TupleConcat(hParamters, "angle_step", out hParamters);
            }
            if ((bool)ParamIsAutoArr["scale_step"])
            {
                HOperatorSet.TupleConcat(hParamters, "scale_step", out hParamters);
            }

            try
            {
                HOperatorSet.DetermineShapeModelParams(modelImage, hIsAutoNumLevels, -0.39, 0.79, 0.9, 1.1,
                   hIsAutoOptimization, "use_polarity",
                   hIsAutoContrast, hIsAutoMinContrast, hParamters,
                   out HTuple hv_ParameterName, out HTuple hv_ParameterValue);
                for (int i = 0; i < hv_ParameterName.TupleLength(); i++)
                {
                    switch (hv_ParameterValue[i].Type)
                    {
                        case HTupleType.DOUBLE:
                            string s = hv_ParameterName[i].S;
                            if (hv_ParameterName[i].S.IndexOf("angle") >= 0)
                            {
                                DetermineModelParamArr[hv_ParameterName[i].S] = hv_ParameterValue[i].D / 3.14159 * 180;
                            }
                            else
                            {
                                DetermineModelParamArr[hv_ParameterName[i].S] = hv_ParameterValue[i].D;
                            }
                            break;
                        case HTupleType.INTEGER:
                            DetermineModelParamArr[hv_ParameterName[i].S] = hv_ParameterValue[i].I;
                            break;
                        case HTupleType.STRING:
                            DetermineModelParamArr[hv_ParameterName[i].S] = hv_ParameterValue[i].S;
                            break;
                    }

                }
            }
            catch (HalconException e)
            {

            }

        }

        private void CreateVisualModel(HObject InImage, HObject ModelRegion, out HObject hModelXld, out HTuple ModelID)
        {
            HOperatorSet.ReduceDomain(InImage, ModelRegion, out HObject ho_TemplateImage);
            HOperatorSet.GenEmptyObj(out hModelXld);
            ModelID = new HTuple();
            try
            {
                HTuple optimization = new HTuple("no_pregeneration");
                HOperatorSet.TupleConcat(optimization, DetermineModelParamArr["optimization"].ToString(), out optimization);
                HTuple contrast = new HTuple(Convert.ToInt32(DetermineModelParamArr["contrast_low"]));
                HOperatorSet.TupleConcat(contrast, Convert.ToInt32(DetermineModelParamArr["contrast_high"]), out contrast);
                HOperatorSet.TupleConcat(contrast, Convert.ToInt32(DetermineModelParamArr["min_size"]), out contrast);
                HOperatorSet.CreateScaledShapeModel(ho_TemplateImage
                     , new HTuple(Convert.ToInt32(DetermineModelParamArr["num_levels"]))
                     , (new HTuple(Convert.ToDouble(DetermineModelParamArr["angle_start"]))).TupleRad()
                     , (new HTuple(Convert.ToDouble(DetermineModelParamArr["angle_extent"]))).TupleRad()
                     , (new HTuple(Convert.ToDouble(DetermineModelParamArr["angle_step"]))).TupleRad()
                     , new HTuple(Convert.ToDouble(DetermineModelParamArr["scale_min"]))
                     , new HTuple(Convert.ToDouble(DetermineModelParamArr["scale_max"]))
                     , new HTuple(Convert.ToDouble(DetermineModelParamArr["scale_step"]))
                     , optimization
                     , new HTuple(DetermineModelParamArr["metric"].ToString())
                     , contrast
                     , new HTuple(Convert.ToInt32(DetermineModelParamArr["min_contrast"]))
                     , out ModelID
                    );
                FindModelParamArr["NumLevels"] = DetermineModelParamArr["num_levels"];
                FindModelParamArr["AngleStart"] = DetermineModelParamArr["angle_start"];
                FindModelParamArr["AngleExtent"] = DetermineModelParamArr["angle_extent"];
                FindModelParamArr["ScaleMin"] = DetermineModelParamArr["scale_min"];
                FindModelParamArr["ScaleMax"] = DetermineModelParamArr["scale_max"];
                HOperatorSet.GenEmptyObj(out HObject ho_ModelContours);
                ho_ModelContours.Dispose();
                HOperatorSet.GetShapeModelContours(out ho_ModelContours, ModelID, 1);
                //
                //Matching 01: Get the reference position
                HOperatorSet.AreaCenter(ModelRegion, out HTuple hv_ModelRegionArea, out HTuple hv_RefRow,
                    out HTuple hv_RefColumn);
                HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_RefRow, hv_RefColumn, 0, out HTuple hv_HomMat2D);
                hModelXld.Dispose();
                HOperatorSet.AffineTransContourXld(ho_ModelContours, out hModelXld, hv_HomMat2D);
            }
            catch (HalconException e)
            {

            }
        }

        public void FindVisualModel(HObject InImage, HTuple ModelID, out HTuple Row, out HTuple Col, out HTuple Angle, out HTuple Scale, out HTuple Score)
        {
            Row = 0; Col = 0; Angle = 0; Score = 0; Scale = 0;
            HTuple SubPixel = new HTuple("max_deformation " + Convert.ToInt32(FindModelParamArr["MaxDeformation"]));
            HOperatorSet.TupleConcat(SubPixel, FindModelParamArr["SubPixel"].ToString(), out SubPixel);
            HTuple NumLevel = new HTuple(Convert.ToInt32(FindModelParamArr["NumLevels"]));
            HOperatorSet.TupleConcat(NumLevel, Convert.ToInt32(FindModelParamArr["MaxLevels"]), out NumLevel);
            HOperatorSet.FindScaledShapeModel(InImage, ModelID
                , (new HTuple(Convert.ToInt32(FindModelParamArr["AngleStart"]))).TupleRad()
                , (new HTuple(Convert.ToInt32(FindModelParamArr["AngleExtent"]))).TupleRad()
                , new HTuple(Convert.ToDouble(FindModelParamArr["ScaleMin"]))
                , new HTuple(Convert.ToDouble(FindModelParamArr["ScaleMax"]))
                , new HTuple(Convert.ToDouble(FindModelParamArr["MinScore"]))
                , new HTuple(Convert.ToInt32(FindModelParamArr["NumMatches"]))
                , new HTuple(Convert.ToDouble(FindModelParamArr["MaxOverlap"]))
                , SubPixel
                , NumLevel
                , new HTuple(Convert.ToDouble(FindModelParamArr["Greediness"]))
                , out Row, out Col, out Angle, out Scale, out Score);
        }

        public void CreateVisualModelAllStep(HTuple InWindowHandle)
        {
            //HalconCommonFunc.DisplayImage(Image, InWindowHandle,pictureBox);
            HalconCommonFunc.DisplayRegionOrXld(ModelRegion, "gray", InWindowHandle, 1);
            HOperatorSet.ReduceDomain(Image, ModelRegion, out HObject imageReduced);
            DetermineShapeModelAtuoParams(imageReduced);
            CreateVisualModel(Image, ModelRegion, out HObject hModelXld, out shapeModelID);
            HalconCommonFunc.DisplayRegionOrXld(hModelXld, "red", InWindowHandle, 1);
            imageReduced.Dispose();
            hModelXld.Dispose();
        }
    }

    public enum OperatorModel
    {
        Union = 0,
        Intersection = 1,
        Difference,
    }

    public enum DrawModel
    {
        Rectangle1 = 0,
        Rectangle2,
        Circle,
    }

    public static class HalconCommonFunc
    {/// <summary>
     /// 显示图片
     /// </summary>
     /// <param name="InImage">图片</param>
     /// <param name="InWindowHandle">窗口</param>
        public static void DisplayImage(HObject InImage, HTuple InWindowHandle, PictureBox pictureBox)
        {
            try
            {
                HTuple Width = null, Height = null;
                HOperatorSet.GetImageSize(InImage, out Width, out Height);
                double ratioWidth = (1.0) * Width[0].I / pictureBox.Width;
                double ratioHeight = 1.0 * Height[0].I / pictureBox.Height;
                HTuple row1, column1, row2, column2;
                if (ratioWidth >= ratioHeight)
                {
                    row1 = -(1.0) * ((pictureBox.Height * ratioWidth) - Height) / 2;
                    column1 = 0;
                    row2 = row1 + pictureBox.Height * ratioWidth;
                    column2 = column1 + pictureBox.Width * ratioWidth;
                    HOperatorSet.SetPart(InWindowHandle, row1, column1, row2, column2);
                }
                HOperatorSet.DispObj(InImage, InWindowHandle);
            }
            catch (HalconException ex)
            {
                string s = ex.Message;
            }
        }
        /// <summary>
        /// 合并，差集，交集
        /// </summary>
        /// <param name="ModelRegion"></param>
        /// <param name="newRegion"></param>
        /// <param name="opModel"></param>
        /// <param name="hObject"></param>
        public static void RegionOperatorset(HObject ModelRegion, HObject newRegion, OperatorModel opModel, out HObject hObject)
        {
            HOperatorSet.GenEmptyRegion(out hObject);
            hObject.Dispose();
            switch (opModel)
            {
                case OperatorModel.Union:
                    HOperatorSet.Union2(ModelRegion, newRegion, out hObject);
                    break;
                case OperatorModel.Difference:
                    HOperatorSet.Difference(ModelRegion, newRegion, out hObject);
                    break;
                case OperatorModel.Intersection:
                    HOperatorSet.Intersection(ModelRegion, newRegion, out hObject);
                    break;
                default: return;
            }

        }
        /// <summary>
        /// 显示轮廓
        /// </summary>
        /// <param name="InObj"></param>
        /// <param name="Color"></param>
        /// <param name="InWindowHandle"></param>
        /// <param name="LineWeight"></param>
        public static void DisplayRegionOrXld(HObject InObj, string Color, HTuple InWindowHandle, HTuple LineWeight)
        {
            try
            {
                HOperatorSet.SetLineWidth(InWindowHandle, LineWeight);
                HOperatorSet.SetColor(InWindowHandle, Color);
                HOperatorSet.SetDraw(InWindowHandle, "margin");
                HOperatorSet.DispObj(InObj, InWindowHandle);
            }
            catch (HalconException ex)
            {
                string s = ex.Message;
            }
        }
        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="InWindowHandle">画框的窗体</param>
        /// <param name="drawModel">框的样式</param>
        /// <param name="hRegion"></param>
        public static void DrawRegion(HTuple InWindowHandle, DrawModel drawModel, out HObject hRegion)
        {
            hRegion = new HObject();
            HOperatorSet.SetLineWidth(InWindowHandle, 1);
            HOperatorSet.SetColor(InWindowHandle, "blue");
            HOperatorSet.SetDraw(InWindowHandle, "margin");
            switch (drawModel)
            {
                case DrawModel.Rectangle1:
                    HOperatorSet.DrawRectangle1(InWindowHandle, out HTuple row1, out HTuple column1, out HTuple row2, out HTuple column2);
                    HOperatorSet.GenRectangle1(out hRegion, row1, column1, row2, column2);
                    HOperatorSet.DispObj(hRegion, InWindowHandle);
                    break;
                case DrawModel.Rectangle2:
                    HOperatorSet.DrawRectangle2(InWindowHandle, out HTuple row, out HTuple column, out HTuple phi, out HTuple length1, out HTuple length2);
                    HOperatorSet.GenRectangle2(out hRegion, row, column, phi, length1, length2);
                    HOperatorSet.DispObj(hRegion, InWindowHandle);
                    break;
                case DrawModel.Circle:
                    HOperatorSet.DrawCircle(InWindowHandle, out HTuple row_1, out HTuple column_1, out HTuple radius);
                    HOperatorSet.GenCircle(out hRegion, row_1, column_1, radius);
                    HOperatorSet.DispObj(hRegion, InWindowHandle);
                    break;
                default: return;

            }
        }
        /// <summary>
        /// 获得模板ID
        /// </summary>
        /// <param name="ModelID"></param>
        /// <param name="ModelXLD"></param>
        public static void GetXldFromModelID(HTuple ModelID, out HObject ModelXLD)
        {
            HOperatorSet.GenEmptyObj(out ModelXLD);
            ModelXLD.Dispose();
            HOperatorSet.GetShapeModelContours(out ModelXLD, ModelID, 1);
        }
        /// <summary>
        /// 仿射变换
        /// </summary>
        /// <param name="ModelXLD"></param>
        /// <param name="Row"></param>
        /// <param name="Col"></param>
        /// <param name="Angle"></param>
        /// <param name="Scale"></param>
        /// <param name="TransContours"></param>
        public static void AffineModelXld(HObject ModelXLD, HTuple Row, HTuple Col, HTuple Angle, HTuple Scale, out HObject TransContours)
        {
            HOperatorSet.GenEmptyObj(out TransContours);
            for (int i = 0; i < Row.TupleLength(); i++)
            {
                HObject hobject = new HObject();
                HOperatorSet.GenEmptyObj(out hobject);
                hobject.Dispose();
                HOperatorSet.HomMat2dIdentity(out HTuple Mat2DIdentity);
                HOperatorSet.HomMat2dScale(Mat2DIdentity, Scale[i], Scale[i], 0, 0, out HTuple Mat2DScale);
                HOperatorSet.HomMat2dRotate(Mat2DScale, Angle[i], 0, 0, out HTuple Mat2DRotate);
                HOperatorSet.HomMat2dTranslate(Mat2DRotate, Row[i], Col[i], out HTuple Mat2DTranslate);
                HOperatorSet.AffineTransContourXld(ModelXLD, out hobject, Mat2DTranslate);
                HOperatorSet.ConcatObj(TransContours, hobject, out TransContours);
            }

        }
        /// <summary>
        /// 关联窗口
        /// </summary>
        public static void OpenWindow(HObject InImage, PictureBox pictureBox, HTuple InWindowHandle)
        {
            HOperatorSet.OpenWindow(0, 0, pictureBox.Width, pictureBox.Height, pictureBox.Handle, "visible", "", out InWindowHandle);
            InImage.Dispose();
            HOperatorSet.GenEmptyObj(out InImage);

        }
        /// <summary>
        /// 读取本地图片
        /// </summary>
        /// <param name="InImage"></param>
        /// <param name="InWindouHandle"></param>
        public static void ReadImage(out HObject InImage, HTuple InWindouHandle, PictureBox pictureBox)
        {
            HOperatorSet.GenEmptyObj(out InImage);
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹";
            dialog.Filter = "图片文件(*.jpg,*.gif,*.bmp)|*.jpg;*.gif;*.bmp";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = 0; i < dialog.FileNames.Length; i++)
                {
                    string file = dialog.FileNames[i];
                    InImage.Dispose();
                    HOperatorSet.ReadImage(out InImage, file);
                    HalconCommonFunc.DisplayImage(InImage, InWindouHandle, pictureBox);
                }
            }
        }
        /// <summary>
        /// 创建模板
        /// </summary>
        public static void CreateModel(HObject InImage, HObject hRegion, HTuple InWindouHandle, out HTuple hv_ModelID)
        {
            try
            {
                HOperatorSet.ReduceDomain(InImage, hRegion, out HObject ho_TemplateImage);
                HOperatorSet.CreateShapeModel(ho_TemplateImage, 7, new HTuple(0).TupleRad()
            , (new HTuple(360)).TupleRad(), (new HTuple(0.1632)).TupleRad(), (new HTuple("point_reduction_high")).TupleConcat(
            "no_pregeneration"), "use_polarity", ((new HTuple(26)).TupleConcat(67)).TupleConcat(
            85), 8, out hv_ModelID);
                HOperatorSet.GetShapeModelContours(out HObject ho_ModelContours, hv_ModelID, 1);
                HOperatorSet.AreaCenter(hRegion, out HTuple hv_ModelRegionArea, out HTuple hv_RefRow,
                    out HTuple hv_RefColumn);
                HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_RefRow, hv_RefColumn, 0, out HTuple hv_HomMat2D);
                HOperatorSet.AffineTransContourXld(ho_ModelContours, out HObject ho_TransContours, hv_HomMat2D);
                HOperatorSet.DispObj(ho_TransContours, InWindouHandle);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        /// <summary>
        /// 大标签角的特征提取
        /// </summary>
        /// <param name="InImage"></param>
        /// <param name="Rectangle"></param>
        /// <param name="Area"></param>
        public static void Blob(HObject InImage, HObject Rectangle, HTuple InWindouHandle, out HTuple Number, out HObject ho_SelectedRegions)
        {
            HOperatorSet.SetDraw(InWindouHandle, "fill");
            HOperatorSet.Rgb3ToGray(InImage, InImage, InImage, out HObject ho_ImageGray);
            //HOperatorSet.GenRectangle1(out HObject ho_Rectangle, 505.0, 10.0164, 907.0, 328.514);
            HOperatorSet.ReduceDomain(ho_ImageGray, Rectangle, out HObject ho_ImageReduced);
            HOperatorSet.Threshold(ho_ImageReduced, out HObject ho_Regions, 11, 213);
            HOperatorSet.Connection(ho_Regions, out HObject ho_ConnectedRegions);
            HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                "and", 1256.88, 8614.68);
            HOperatorSet.CountObj(ho_SelectedRegions, out Number);
            HOperatorSet.DispObj(ho_SelectedRegions, InWindouHandle);
        }
        /// <summary>
        /// 小标签的有无
        /// </summary>
        public static void SmallLableNothing(HObject InImage, HObject Rectangle, HTuple InWindouHandle, out HTuple hv_Number, out HObject ho_SelectedRegions)
        {
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            ho_SelectedRegions.Dispose();
            HOperatorSet.SetDraw(InWindouHandle, "margin");
            HOperatorSet.Rgb3ToGray(InImage, InImage, InImage, out HObject ImageGray);
            HOperatorSet.ReduceDomain(ImageGray, Rectangle, out HObject ImageReduced);
            HOperatorSet.Threshold(ImageReduced, out HObject ho_Regions, 0, 67);
            HOperatorSet.Connection(ho_Regions, out HObject ho_ConnectedRegions);
            HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                "and", 0, 402294);
            HOperatorSet.CountObj(ho_SelectedRegions, out hv_Number);
            HOperatorSet.SetColor(InWindouHandle, "green");
            HOperatorSet.DispObj(ho_SelectedRegions, InWindouHandle);

        }
        /// <summary>
        /// 小标签圆弧
        /// </summary>
        /// <param name="InImage"></param>
        /// <param name="Rectangle"></param>
        /// <param name="InWindouHandle"></param>
        /// <param name="mean"></param>
        public static void SmallLableCircle(HObject InImage, HObject Rectangle, HTuple InWindouHandle, out HTuple mean)
        {
            HOperatorSet.GenEmptyObj(out HObject ImageGray);
            ImageGray.Dispose();
            HOperatorSet.SetDraw(InWindouHandle, "fill");
            HOperatorSet.Rgb3ToGray(InImage, InImage, InImage, out ImageGray);
            HOperatorSet.Intensity(Rectangle, InImage, out  mean, out HTuple deviation);
            

        }
        /// <summary>
        /// 小标签翘起
        /// </summary>
        /// <param name="InImage"></param>
        /// <param name="Rectangle"></param>
        /// <param name="InWindouHandle"></param>
        /// <param name="Area"></param>
        /// <param name="ho_SelectedRegions"></param>
        public static void SmallLableAngle(HObject InImage, HObject Rectangle, HTuple InWindouHandle, out HTuple Area, out HObject ho_SelectedRegions)
        {
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            ho_SelectedRegions.Dispose();
            HOperatorSet.SetDraw(InWindouHandle, "margin");
            HOperatorSet.Rgb3ToGray(InImage, InImage, InImage, out HObject ImageGray);
            HOperatorSet.ReduceDomain(ImageGray, Rectangle, out HObject ImageReduced);
            HOperatorSet.Threshold(ImageReduced, out HObject ho_Regions, 0, 128);
            HOperatorSet.Connection(ho_Regions, out HObject ho_ConnectedRegions);
            HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                "and", 67431.2, 420642);
            HOperatorSet.AreaCenter(ho_SelectedRegions, out Area, out HTuple Row, out HTuple Column);
            HOperatorSet.SetColor(InWindouHandle, "red");
            HOperatorSet.DispObj(ho_SelectedRegions, InWindouHandle);

        }
        public static void SmallLabledistance(HObject InImage, HObject Rectangle, out HTuple DistanceMin, out HTuple DistanceMax,out HObject Edges1)
        {
            HOperatorSet.Rgb3ToGray(InImage, InImage, InImage, out HObject ImageGray);
            //裁剪二值化

            HOperatorSet.ReduceDomain(ImageGray, Rectangle, out HObject ImageReduced);
            HOperatorSet.Threshold(ImageReduced, out HObject Regions, 59, 158);
            //提取亚像素边缘

            HOperatorSet.EdgesSubPix(ImageReduced, out  Edges1, "canny", 1, 20, 40);
            //计算边缘数量
            HOperatorSet.CountObj(Edges1, out HTuple Number);
            //排序边缘轮廓
            HOperatorSet.SortContoursXld(Edges1, out HObject ho_SortedContours, "upper_left",
                "true", "row");
            //选择轮廓1，2
            HOperatorSet.SelectObj(ho_SortedContours, out HObject ho_ObjectSelected2, 1);
            HOperatorSet.SelectObj(ho_SortedContours, out HObject ho_ObjectSelected3, 2);
            HTuple RowBegin;
            //将两个轮廓拟合，得出尺寸
            HOperatorSet.FitLineContourXld(ho_ObjectSelected2, "tukey", -1, 0, 5, 2, out RowBegin,
                out HTuple ColBegin, out HTuple RowEnd, out HTuple ColEnd, out HTuple Nr, out HTuple Nc, out HTuple Dist);
            HOperatorSet.FitLineContourXld(ho_ObjectSelected3, "tukey", -1, 0, 5, 2, out HTuple RowBegin1,
                out HTuple ColBegin1, out HTuple RowEnd1, out HTuple ColEnd1, out HTuple Nr1, out HTuple Nc1,
                out HTuple Dist1);
            //计算最大和最小尺寸
            HOperatorSet.DistanceSs(RowBegin, ColBegin, RowEnd, ColEnd, RowBegin1,
                ColBegin1, RowEnd1, ColEnd1, out DistanceMin, out DistanceMax);


        }
    }
}
