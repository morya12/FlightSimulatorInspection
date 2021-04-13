using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorInspection.Models
{
    #region NativeMethods
    public static class NativeMethods
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);
    }
    #endregion
    #region API Functions
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr CreateDetector(string trainCSV, string testCSV);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr detect(IntPtr ad);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int getARVectorSize(IntPtr vec);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr CreateAnomalyReportWrapperByIndx(IntPtr vec, int indx);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void getDescription(IntPtr ar, StringBuilder str, int strlen);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int getTimeStep(IntPtr ar);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr getNormalModel(IntPtr ad);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int getCFVectorSize(IntPtr vec);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr CreateCorrelatedFeaturesWrapperByIndx(IntPtr vec, int indx);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void getFeature1(IntPtr cf, StringBuilder str, int strlen);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void getFeature2(IntPtr cf, StringBuilder str, int strlen);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate float getLineA(IntPtr cf);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate float getLineB(IntPtr cf);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate float getRadius(IntPtr cf);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate float getCX(IntPtr cf);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate float getCY(IntPtr cf);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void setThreshold(IntPtr ad, float thresh);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int getTestFileSize(IntPtr detector);
    #endregion
    public class AnomalyDetection
    {
        private string dllPath;
        private string csvLearnPath;
        private string csvPath;

        public string CsvPath { get => csvPath; set => csvPath = value; }
        public string CsvLearnPath { get => csvLearnPath; set => csvLearnPath = value; }
        public string DllPath { get => dllPath; set => dllPath = value; }

        public void detectAnomalies(ref List<CorrelatedFeatures> cfList, ref List<AnomalyReport> arList)
        {
            int STRING_MAX_LEN = 2064;
            string str;
            StringBuilder strBuilder;
            CorrelatedFeatures cf;
            AnomalyReport ar;
            bool functionOpenedNormaly = true;

            //open AnomalyDetector.dll as Plugin
            IntPtr pDll = NativeMethods.LoadLibrary(DllPath);
            if (pDll == IntPtr.Zero)
            {
                Trace.WriteLine("Error opening the DLL.. closing procedure.");
                return;
            }

            #region Detector Functions

            IntPtr AddressOfCreateDetector = NativeMethods.GetProcAddress(pDll, typeof(CreateDetector).Name);
            if (AddressOfCreateDetector == IntPtr.Zero)
            {
                Trace.WriteLine("Can't find function " + typeof(CreateDetector).Name);
                functionOpenedNormaly = false;
            }
            CreateDetector CreateDetector = (CreateDetector)Marshal.GetDelegateForFunctionPointer(AddressOfCreateDetector, typeof(CreateDetector));

            IntPtr AddressOfsetThreshold = NativeMethods.GetProcAddress(pDll, typeof(setThreshold).Name);
            if (AddressOfsetThreshold == IntPtr.Zero)
            {
                Trace.WriteLine("Can't find function" + typeof(setThreshold).Name);
                functionOpenedNormaly = false;
            }
            setThreshold setThreshold = (setThreshold)Marshal.GetDelegateForFunctionPointer(AddressOfsetThreshold, typeof(setThreshold));

            IntPtr AddressOfgetTestFileSize = NativeMethods.GetProcAddress(pDll, typeof(getTestFileSize).Name);
            if (AddressOfgetTestFileSize == IntPtr.Zero)
            {
                Trace.WriteLine("Can't find function" + typeof(getTestFileSize).Name);
                functionOpenedNormaly = false;
            }
            getTestFileSize getTestFileSize = (getTestFileSize)Marshal.GetDelegateForFunctionPointer(AddressOfgetTestFileSize, typeof(getTestFileSize));

            #endregion

            #region AR Functions

            IntPtr AddressOfDetect = NativeMethods.GetProcAddress(pDll, typeof(detect).Name);
            if (AddressOfDetect == IntPtr.Zero)
            {
                Trace.WriteLine("Can't find function" + typeof(detect).Name);
                functionOpenedNormaly = false;
            }
            detect detect = (detect)Marshal.GetDelegateForFunctionPointer(AddressOfDetect, typeof(detect));

            IntPtr AddressOfVectorARSize = NativeMethods.GetProcAddress(pDll, typeof(getARVectorSize).Name);
            if (AddressOfVectorARSize == IntPtr.Zero)
            {
                Trace.WriteLine("Can't find function" + typeof(getARVectorSize).Name);
                functionOpenedNormaly = false;
            }
            getARVectorSize getARVectorSize = (getARVectorSize)Marshal.GetDelegateForFunctionPointer(AddressOfVectorARSize, typeof(getARVectorSize));

            IntPtr AddressOfARByIndx = NativeMethods.GetProcAddress(pDll, typeof(CreateAnomalyReportWrapperByIndx).Name);
            if (AddressOfARByIndx == IntPtr.Zero)
            {
                Trace.WriteLine("Can't find function" + typeof(CreateAnomalyReportWrapperByIndx).Name);
                functionOpenedNormaly = false;
            }
            CreateAnomalyReportWrapperByIndx CreateAnomalyReportWrapperByIndx = (CreateAnomalyReportWrapperByIndx)Marshal.GetDelegateForFunctionPointer(AddressOfARByIndx, typeof(CreateAnomalyReportWrapperByIndx));

            IntPtr AddressOfgetDescription = NativeMethods.GetProcAddress(pDll, typeof(getDescription).Name);
            if (AddressOfgetDescription == IntPtr.Zero)
            {
                Trace.WriteLine("Can't find function" + typeof(getDescription).Name);
                functionOpenedNormaly = false;
            }
            getDescription getDescription = (getDescription)Marshal.GetDelegateForFunctionPointer(AddressOfgetDescription, typeof(getDescription));

            IntPtr AddressOfgetTimeStep = NativeMethods.GetProcAddress(pDll, typeof(getTimeStep).Name);
            if (AddressOfgetTimeStep == IntPtr.Zero)
            {
                Trace.WriteLine("Can't find function" + typeof(getTimeStep).Name);
                functionOpenedNormaly = false;
            }
            getTimeStep getTimeStep = (getTimeStep)Marshal.GetDelegateForFunctionPointer(AddressOfgetTimeStep, typeof(getTimeStep));

            #endregion

            #region CF Functions

            IntPtr AddressOfgetNormalModel = NativeMethods.GetProcAddress(pDll, typeof(getNormalModel).Name);
            if (AddressOfgetNormalModel == IntPtr.Zero)
            {
                Trace.WriteLine("Can't find function" + typeof(getNormalModel).Name);
                functionOpenedNormaly = false;
            }
            getNormalModel getNormalModel = (getNormalModel)Marshal.GetDelegateForFunctionPointer(AddressOfgetNormalModel, typeof(getNormalModel));

            IntPtr AddressOfgetCFVectorSize = NativeMethods.GetProcAddress(pDll, typeof(getCFVectorSize).Name);
            if (AddressOfgetCFVectorSize == IntPtr.Zero)
            {
                Trace.WriteLine("Can't find function" + typeof(getCFVectorSize).Name);
                functionOpenedNormaly = false;
            }
            getCFVectorSize getCFVectorSize = (getCFVectorSize)Marshal.GetDelegateForFunctionPointer(AddressOfgetCFVectorSize, typeof(getCFVectorSize));

            IntPtr AddressOfCFByIndx = NativeMethods.GetProcAddress(pDll, typeof(CreateCorrelatedFeaturesWrapperByIndx).Name);
            if (AddressOfCFByIndx == IntPtr.Zero)
            {
                Trace.WriteLine("Can't find function" + typeof(CreateCorrelatedFeaturesWrapperByIndx).Name);
                functionOpenedNormaly = false;
            }
            CreateCorrelatedFeaturesWrapperByIndx CreateCorrelatedFeaturesWrapperByIndx = (CreateCorrelatedFeaturesWrapperByIndx)Marshal.GetDelegateForFunctionPointer(AddressOfCFByIndx, typeof(CreateCorrelatedFeaturesWrapperByIndx));

            IntPtr AddressOfgetFeature1 = NativeMethods.GetProcAddress(pDll, typeof(getFeature1).Name);
            if (AddressOfgetFeature1 == IntPtr.Zero)
            {
                Trace.WriteLine("Can't find function" + typeof(getFeature1).Name);
                functionOpenedNormaly = false;
            }
            getFeature1 getFeature1 = (getFeature1)Marshal.GetDelegateForFunctionPointer(AddressOfgetFeature1, typeof(getFeature1));

            IntPtr AddressOfgetFeature2 = NativeMethods.GetProcAddress(pDll, typeof(getFeature2).Name);
            if (AddressOfgetFeature2 == IntPtr.Zero)
            {
                Trace.WriteLine("Can't find function" + typeof(getFeature2).Name);
                functionOpenedNormaly = false;
            }
            getFeature2 getFeature2 = (getFeature2)Marshal.GetDelegateForFunctionPointer(AddressOfgetFeature2, typeof(getFeature2));

            IntPtr AddressOfgetLineA = NativeMethods.GetProcAddress(pDll, typeof(getLineA).Name);
            if (AddressOfgetLineA == IntPtr.Zero)
            {
                Trace.WriteLine("Can't find function" + typeof(getLineA).Name);
                functionOpenedNormaly = false;
            }
            getLineA getLineA = (getLineA)Marshal.GetDelegateForFunctionPointer(AddressOfgetLineA, typeof(getLineA));

            IntPtr AddressOfgetLineB = NativeMethods.GetProcAddress(pDll, typeof(getLineB).Name);
            if (AddressOfgetLineB == IntPtr.Zero)
            {
                Trace.WriteLine("Can't find function" + typeof(getLineB).Name);
                functionOpenedNormaly = false;
            }
            getLineB getLineB = (getLineB)Marshal.GetDelegateForFunctionPointer(AddressOfgetLineB, typeof(getLineB));

            IntPtr AddressOfgetRadius = NativeMethods.GetProcAddress(pDll, typeof(getRadius).Name);
            if (AddressOfgetRadius == IntPtr.Zero)
            {
                Trace.WriteLine("Can't find function" + typeof(getRadius).Name);
                functionOpenedNormaly = false;
            }
            getRadius getRadius = (getRadius)Marshal.GetDelegateForFunctionPointer(AddressOfgetRadius, typeof(getRadius));

            IntPtr AddressOfgetCX = NativeMethods.GetProcAddress(pDll, typeof(getCX).Name);
            if (AddressOfgetCX == IntPtr.Zero)
            {
                Trace.WriteLine("Can't find function" + typeof(getCX).Name);
                functionOpenedNormaly = false;
            }
            getCX getCX = (getCX)Marshal.GetDelegateForFunctionPointer(AddressOfgetCX, typeof(getCX));

            IntPtr AddressOfgetCY = NativeMethods.GetProcAddress(pDll, typeof(getCY).Name);
            if (AddressOfgetCY == IntPtr.Zero)
            {
                Trace.WriteLine("Can't find function" + typeof(getCY).Name);
                functionOpenedNormaly = false;
            }
            getCY getCY = (getCY)Marshal.GetDelegateForFunctionPointer(AddressOfgetCY, typeof(getCY));

            #endregion

            if (Path.GetExtension(CsvLearnPath) != ".csv")
            {
                Trace.WriteLine("CsvLearnPath is not a type of .csv --> can't produce anomalies..");
                functionOpenedNormaly = false;
            }
            if(Path.GetExtension(CsvPath) != ".csv")
            {
                Trace.WriteLine("CsvPath is not a type of .csv --> can't produce anomalies..");
                functionOpenedNormaly = false;
            }

            #region use Dll to fill CF + AR List
            if (functionOpenedNormaly == true)
            {
                IntPtr detector = CreateDetector(CsvLearnPath, CsvPath);
                IntPtr anomalyReportVector = detect(detector);
                int vectorARSize = getARVectorSize(anomalyReportVector);
                IntPtr correlatedFeaturesVector = getNormalModel(detector);


                int vectorCFsize = getCFVectorSize(correlatedFeaturesVector);
                IntPtr correlatedFeatureWrapped;

                for (int i = 0; i < vectorCFsize; i++)
                {
                    cf = new CorrelatedFeatures();
                    strBuilder = new StringBuilder(STRING_MAX_LEN);
                    correlatedFeatureWrapped = CreateCorrelatedFeaturesWrapperByIndx(correlatedFeaturesVector, i);

                    getFeature1(correlatedFeatureWrapped, strBuilder, STRING_MAX_LEN);
                    str = strBuilder.ToString();
                    cf.Feature1 = str;
                    strBuilder = new StringBuilder(STRING_MAX_LEN);
                    getFeature2(correlatedFeatureWrapped, strBuilder, STRING_MAX_LEN);
                    str = strBuilder.ToString();
                    cf.Feature2 = str;
                    cf.LineA = getLineA(correlatedFeatureWrapped);
                    cf.LineB = getLineB(correlatedFeatureWrapped);
                    cf.CX = getCX(correlatedFeatureWrapped);
                    cf.CY = getCY(correlatedFeatureWrapped);
                    cf.Radius = getRadius(correlatedFeatureWrapped);
                    cfList.Add(cf);
                }


                IntPtr anomalyReportWrapped;
                for (int i = 0; i < vectorARSize; i++)
                {
                    ar = new AnomalyReport();
                    strBuilder = new StringBuilder(STRING_MAX_LEN);
                    anomalyReportWrapped = CreateAnomalyReportWrapperByIndx(anomalyReportVector, i);
                    getDescription(anomalyReportWrapped, strBuilder, STRING_MAX_LEN);
                    str = strBuilder.ToString();
                    ar.Description = str;
                    ar.TimeStep = getTimeStep(anomalyReportWrapped);

                    arList.Add(ar);
                }
            }
            #endregion

            //close dll resource
            bool result = NativeMethods.FreeLibrary(pDll);
            if (result == false)
                Trace.WriteLine("Error closing the DLL");
        }
    }
}
