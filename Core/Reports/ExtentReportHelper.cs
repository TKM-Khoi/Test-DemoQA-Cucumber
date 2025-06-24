using System.Runtime.CompilerServices;

using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

using Core.Drivers;
using Core.Utils;

namespace Core.Reports
{
    public class ExtentReportHelper
    {
        static ExtentReports ExtentManager;
        public static ThreadLocal<ExtentTest> Feature = new ThreadLocal<ExtentTest>();
        public static ThreadLocal<ExtentTest> Test = new ThreadLocal<ExtentTest>();
        public static void InitualizeReport(string[] reportPaths, string configFilePath="")
        {
            ExtentManager = new ExtentReports();
            foreach (string reportPath in reportPaths)
            {
                ExtentSparkReporter htmlReporter = new ExtentSparkReporter(reportPath);
                if (!string.IsNullOrEmpty(configFilePath))
                {
                    htmlReporter.LoadXMLConfig(configFilePath);
                }
                ExtentManager.AttachReporter(htmlReporter);
            }
            ExtentManager.AddSystemInfo("Enviroment", "Staging");
            ExtentManager.AddSystemInfo("Browser", ConfigurationUtils.GetConfigurationByKey("Browser:Name"));
            Console.WriteLine("Initualize report");
        }
        public static void CreateFeature(string name){
            Feature.Value = ExtentManager.CreateTest(name);
            Console.WriteLine("create test");
        }  
        public static void CreateTest(string name){
            Test.Value = Feature.Value.CreateNode(name);
            Console.WriteLine("create node");
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetFeature()
        {
            return Feature.Value;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetTestCase()
        {
            return Test.Value;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void LogInfo(string step)
        {
            Test.Value.Info(step);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
            public static void LogPass(string step){
            Test.Value.Pass(step);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void LogFail(string step){
            Test.Value.Fail(step);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void LogSkip(string step){
            Test.Value.Skip(step);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void CreateTestResult(string status, string stackTrace, string className, string testName)
        {
            Status logStatus;
            switch (status)
            {
                case "Failed":
                    {
                        logStatus = Status.Fail;
                        if (BrowserFactory.GetWebDriver() != null)
                        {
                            string mediaEntity = DriverUtils.CaptureScreenshot(BrowserFactory.GetWebDriver(), className, testName);
                            Test.Value.Fail($"#Test Name: {testName}, #Status: {logStatus + stackTrace}, #Img: {mediaEntity}").AddScreenCaptureFromPath(mediaEntity);
                        }
                        else
                        {
                            Test.Value.Fail($"#Test Name: {testName}, #Status: {logStatus + stackTrace}");
                        }
                        break;
                    }
                case "Passed":
                    {
                        logStatus = Status.Pass;
                        Test.Value.Pass($"====> Test Name: {testName}, Status: {logStatus}");
                        break;
                    }
                case "Inconclusive":
                    {
                        logStatus = Status.Warning;
                        Test.Value.Pass($"====> Test Name: {testName}, Status: {logStatus}");
                        break;
                    }
                case "Skipped":
                    {
                        logStatus = Status.Skip;
                        Test.Value.Pass($"====> Test Name: {testName}, Status: {logStatus}");
                        break;
                    }
                default:
                    {
                        logStatus = Status.Pass;
                        Test.Value.Pass($"====> Test Name: {testName}, Status: {logStatus}");
                        break;
                    }
            }
        }
        public static void Flush(){
            Console.WriteLine("Before flush");
            ExtentManager.Flush();
            Console.WriteLine("After flush");
        }   
    }
}