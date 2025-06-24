using System.Collections.Generic;

using Core.Element;
using Core.Utils;

using OpenQA.Selenium;

using ProjectTest.Components;
using ProjectTest.Const;

using Service.Models.DTOs;

namespace ProjectTest.PageObjects
{
    public class BookStorePage
    {
        #region locators
        private SearchBookGridComponent _bookSearchTbl = new SearchBookGridComponent();
        private WebObject _searchBoxTxt = new WebObject(By.Id("searchBox"), "Search Box");

        #endregion

        #region Actions
        public void NavigateToPage()
        {
            DriverUtils.GoToUrl(ConfigurationUtils.GetConfigurationByKey("TestUrl") + PageUrlConst.BOOKSTORE_PAGE);
        }
        public void Search(string search)
        {
            _searchBoxTxt.EnterText(search);
        }
        public IEnumerable<SearchedBookDto> GetSearchedBooks()
        {
            return _bookSearchTbl.GetAllResults();
        }
        #endregion
    }
}