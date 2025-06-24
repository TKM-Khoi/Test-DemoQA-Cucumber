using System.Collections.Generic;

using Core.Element;
using Core.Utils;

using OpenQA.Selenium;

using ProjectTest.Components;

using Service.Models.DTOs;

namespace ProjectTest.PageObjects
{
    public class ProfilePage
    {
        private DeleteBookGridComponent _bookSearchTbl = new DeleteBookGridComponent();
        private WebObject _searchBoxTxt = new WebObject(By.Id("searchBox"), "Search Box");
        private WebObject _confirmDeleteBtn = new WebObject(By.Id("closeSmallModal-ok"));
        public void Search(string search)
        {
            _searchBoxTxt.EnterText(search);
        }
        public void ClickDeleteBookBtn(string name)
        {
            _bookSearchTbl.ClickDeleteBookBtn(name);
        }
        public void ConfirmDelete()
        {
            _confirmDeleteBtn.ClickOnElement();
        }

        public void CloseAlert()
        {
            DriverUtils.AcceptAlert();
        }
        public IEnumerable<SearchedBookDto> GetBooks()
        {
            return _bookSearchTbl.GetAllResults();
        }
        public string GetNoDataWarningMsg()
        {
            return _bookSearchTbl.GetNoDataWarningMsg();
        }
    }
}