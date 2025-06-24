using System;
using System.Runtime.CompilerServices;

using AventStack.ExtentReports;

using Core.Drivers;
using Core.Reports;
using Core.Utils;

using NUnit.Framework;

using Reqnroll;

namespace ProjectTest.Extensions
{
    public static class ReportContextExtension
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void DefineScenarioNameIntoReport(this ScenarioContext context)
        {
            var tags = String.Join(", ", context.ScenarioInfo.Tags);
            var testName = tags + " - Scenario: " + context.ScenarioInfo.Title;

            if (context.ScenarioInfo.Arguments.Count > 0)
            {
                string args = String.Join(", ", context.ScenarioInfo.Arguments.Values);
                testName += $" - [{args}]";
            }
            ExtentReportHelper.CreateTest(testName);
            if (tags.ToLower().Contains("skip") || tags.ToLower().Contains("notsupport"))
            {
                Assert.Inconclusive("Ignore test case");
            }
            if (tags.ToLower().Contains("bug"))
            {
                Assert.Inconclusive($"Bug is existed {tags}");
            }

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        [Obsolete]
        public static void UpdateBDDStepInfoIntoReport(this ScenarioContext context, string featureName)
        {
            var stepInfo = context.StepContext.StepInfo;
            var stepType = stepInfo.StepDefinitionType.ToString(); // Given, When, Then
            var stepText = stepInfo.Text;
            var stepLog = $"<strong>{stepType}</strong> {stepText}";
            if (stepInfo.Table != null)
            {
                foreach (var row in stepInfo.Table.Rows)
                {
                    stepLog += string.Join(" | ", row.Values);
                    stepLog += "<br/>";
                }
            }
            if (context.StepContext.Status == ScenarioExecutionStatus.OK)
            {
                ExtentReportHelper.GetTestCase().Log(Status.Pass, stepLog);
            }
            else
            {
                ExtentReportHelper.GetTestCase().Log(Status.Fail, stepLog);
                context.LogErrorIntoReport(featureName);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        [Obsolete]
        public static void LogErrorIntoReport(this ScenarioContext context, string featureName)
        {
            var stacktrac = context.TestError.StackTrace;
            Status logStatus = Status.Fail;
            if (context.CurrentScenarioBlock.ToString().ToLower().Contains("skip"))
            {
                logStatus = Status.Skip;
            }
            else if (context.CurrentScenarioBlock.ToString().ToLower().Contains("bug"))
            {
                logStatus = Status.Warning;
            }else if(context.StepContext.Status==ScenarioExecutionStatus.TestError){
                logStatus = Status.Fail;
                if (BrowserFactory.GetWebDriver() != null)
                {
                    string mediaEntity = DriverUtils.CaptureScreenshot(BrowserFactory.GetWebDriver(), featureName, context.ScenarioInfo.Title);
                    ExtentReportHelper.GetTestCase().Log(logStatus, "Image: " + mediaEntity).AddScreenCaptureFromPath(mediaEntity);
                }
                string logValue = "Test ended with result: " + logStatus + "<br/>"
                    + "Error Msg: " + context.TestError.Message + "<br/>"
                    + "Stack Tracce: "+context.TestError.StackTrace;
                ExtentReportHelper.GetTestCase().Log(logStatus, logValue);
            }
        }
    }
}