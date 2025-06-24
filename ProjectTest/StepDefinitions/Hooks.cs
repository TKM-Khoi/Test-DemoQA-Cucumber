using System;
using System.IO;
using System.Linq;

using Core.Client;
using Core.Drivers;
using Core.Reports;
using Core.Utils;

using NUnit.Framework;

using ProjectTest.Extensions;

using Reqnroll;

using Service.ApiServices;
using Service.Const;
using Service.Models.DTOs;

[assembly: Parallelizable(ParallelScope.Fixtures)]
namespace ProjectTest.StepDefinitions
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;

        public Hooks(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            TestContext.Progress.WriteLine("================> Global OneTimeSetUp");
            ConfigurationUtils.ReadConfiguration("Configurations\\appsettings.json");
            var reportPaths = new string[]{
                Directory.GetCurrentDirectory()+"\\TestResults\\Latest-test.html",
                Directory.GetCurrentDirectory()+$"\\TestResults\\Test-{DateTime.Now.ToString("yyyyMMdd HHmmss")}.html"
            };
            ExtentReportHelper.InitualizeReport(reportPaths, FileConst.REPORT_CONFIG_FILE.GetAbsolutePath());
        }
        [AfterTestRun]
        public static void AfterTestRun()
        {
            ExtentReportHelper.Flush();
        }
        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            ExtentReportHelper.CreateFeature(featureContext.FeatureInfo.Title);
        }
        [AfterFeature]
        public static void AfterFeature()
        {

        }
        [BeforeScenario]
        public void BeforeScenario(ScenarioContext sContext)
        {
            sContext.DefineScenarioNameIntoReport();
            string browserName = ConfigurationUtils.GetConfigurationByKey("Browser:Name");
            var args = ConfigurationUtils.GetBrowserArgs($"Browser:{browserName}:BrowserArgs");
            var prefs = ConfigurationUtils.GetBrowserPrefs(browserName);
            BrowserFactory.InitializeDriver(browserName, args.ToArray(), prefs);
            DriverUtils.MaximizeWindow();
        }
        [AfterScenario("@delete")]
        public void AfterDeleteScenario(ScenarioContext scenarioContext)
        {
            ApiClient apiClient = new ApiClient(ConfigurationUtils.GetConfigurationByKey("TestUrl"));
            BookApiService bookService = new BookApiService(apiClient);
            DeleteBookLaterDto deleteLater = null;
            bool hasDeleteLater =scenarioContext.TryGetValue<DeleteBookLaterDto>(ContextKeyConst.DELETE_BOOK_LATER, out deleteLater);
            if (hasDeleteLater && String.IsNullOrWhiteSpace(deleteLater.userToken) is true)
            {
                bookService.DeleteBookWithUnameAndPass(deleteLater.userId, deleteLater.isbn, deleteLater.username, deleteLater.password);
            } else if (hasDeleteLater && String.IsNullOrWhiteSpace(deleteLater.userToken) is false)
            {
                bookService.DeleteBookWithToken(deleteLater.userId, deleteLater.isbn, deleteLater.userToken);
            } 
        }
        [AfterScenario]
        public void AfterScenario()
        {
            DriverUtils.CloseAndCleanUp();
        }
        [AfterStep]
        public void AfterStep()
        {
            _scenarioContext.UpdateBDDStepInfoIntoReport(_featureContext.FeatureInfo.Title);
        }

    }
}