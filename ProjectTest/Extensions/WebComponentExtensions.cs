using Core.Drivers;
using Core.Element;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using ProjectTest.Components;

namespace ProjectTest.Extensions
{
    public static class WebComponentExtensions
    {
        public static WebDriverWait Wait() => BrowserFactory.GetDriverWait();

         public static void ClickSubElement(this CalendarComponent parentWebbObject, WebObject childWebObject)
        {
            WebDriverWait wait = Wait();

            IWebElement parent = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(parentWebbObject.By));

            IWebElement child = wait.Until(drv =>
            {
                IWebElement el = parent.FindElement(childWebObject.By);
                return (el != null && el.Displayed && el.Enabled) ? el : null;
            });

            child.Click();

        }
        public static IWebElement WaitSubElementToBeVisible(this CalendarComponent parentWebbObject, WebObject childWebObject)
        {
            WebDriverWait wait = Wait();

            IWebElement parent = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(parentWebbObject.By));

            IWebElement child = wait.Until(drv =>
            {
                IWebElement el = parent.FindElement(childWebObject.By);
                return (el != null && el.Displayed) ? el : null;
            });
            return child;
        }
        public static IWebElement WaitSubElementToBeClickable(this CalendarComponent parentWebbObject, WebObject childWebObject)
        {
            WebDriverWait wait = Wait();

            IWebElement parent = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(parentWebbObject.By));

            IWebElement child = wait.Until(drv =>
            {
                IWebElement el = parent.FindElement(childWebObject.By);
                return (el != null && el.Displayed && el.Enabled) ? el : null;
            });
            return child;
        }
    }
}