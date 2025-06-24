using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

using Core.Element;

using OpenQA.Selenium;

namespace ProjectTest.Components
{
    public class RegisterSuccessPopupComponent : WebObject
    {
        public RegisterSuccessPopupComponent(By by, string name = "") : base(by, name)
        {
        }
        private WebObject _confirmTbl = new WebObject(By.CssSelector(".modal-body table tbody"), "Register Success Confirm Table");
        private WebObject _closePopupBtn = new WebObject(By.XPath("//button[contains(@aria-label, 'Close')]"), "Close popup button");
        private WebObject _thankYouHdr = new WebObject(By.Id("example-modal-sizes-title-lg"), "Thank you header");
        public dynamic GetRegisterResult()
        {
            dynamic result = new ExpandoObject();
            var dict = (IDictionary<string, object>)result;
            var rows = _confirmTbl.WaitForElementToBeVisible().FindElements(By.TagName("tr")).ToList();
            foreach (var row in rows)
            {
                var cells = row.FindElements(By.TagName("td"));
                string key = cells[0].Text;
                string value = cells[1].Text;
                dict[key] = value;
            }
            return result;
        }
        public string GetHeaderText()
        {
            return _thankYouHdr.GetTextFromElement();
        }
    }
}