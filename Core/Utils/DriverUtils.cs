using Core.Drivers;

using OpenQA.Selenium;

using SeleniumExtras.WaitHelpers;

namespace Core.Utils
{
    public class DriverUtils
    {
        public static void GoToUrl(string url)
        {
            BrowserFactory.GetWebDriver().Navigate().GoToUrl(url);
        }
        public static void MaximizeWindow()
        {
            BrowserFactory.GetWebDriver().Manage().Window.Maximize();
        }
        public static string CaptureScreenshot(IWebDriver driver, string featureName, string testName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            var screenshotDirectory = Path.Combine(Directory.GetCurrentDirectory(), ConfigurationUtils.GetConfigurationByKey("Screenshot.Folder"));
            testName = testName.Replace("\"", "");
            var fileName = $"Screenshot_{featureName}_{testName}_{DateTime.Now.ToString("yyyyMMdd_HHmmssff")}";
            Directory.CreateDirectory(screenshotDirectory);
            var fileFullName = $"{screenshotDirectory}\\{fileName}.png";
            screenshot.SaveAsFile(fileFullName);
            return fileFullName;
        }
        public static string GetUrl()
        {
            return BrowserFactory.GetWebDriver().Url;
        }
        public static string GetAlertMessage()
        {
            IAlert alert =  BrowserFactory.GetDriverWait().Until(ExpectedConditions.AlertIsPresent());
            return alert.Text;
        }
        public static void AcceptAlert()
        {
            IAlert alert =  BrowserFactory.GetDriverWait().Until(ExpectedConditions.AlertIsPresent());
            alert.Accept();
        }
        public static void CloseAndCleanUp()
        {
            var driver = BrowserFactory.GetWebDriver();
            driver.Quit();
            driver.Dispose();
        }
    }
}