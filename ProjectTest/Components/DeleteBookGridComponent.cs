using Core.Element;

using OpenQA.Selenium;

namespace ProjectTest.Components
{
    /// <summary>
    /// similar to SearchBookGridComponent but has delete button
    /// </summary>
    public class DeleteBookGridComponent : SearchBookGridComponent
    {
        private WebObject DeleteButton(string name) => new WebObject(By.XPath($"//a[text()='{name}']/ancestor::div[@role='row']//span[@title='Delete']"));
        public void ClickDeleteBookBtn(string name)
        {
            DeleteButton(name).ClickOnElement();
        }
    }
}