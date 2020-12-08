using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AQATM.WebPages
{
    public class TMCreateEventPage : AbstractPage
    {
        private const int DefaultWaitingInterval = 1;

        public TMCreateEventPage(IWebDriver driver)
            : base(driver)
        {
        }

        public IWebElement FinalCreateButton => FindByCss("input[id='create-event-btn']", DefaultWaitingInterval);

        public IWebElement BackToListButtonButton => FindByCss("div[id='back-to-list-create-event']", DefaultWaitingInterval);

        public IWebElement NameInput => FindByCss("input[id='Name']", DefaultWaitingInterval);

        public IWebElement DescInput => FindByCss("input[id='Description']", DefaultWaitingInterval);

        public IWebElement ImgInput => FindByCss("input[id='Img']", DefaultWaitingInterval);

        public IWebElement StartDateInput => FindByCss("input[id='StartEvent']", DefaultWaitingInterval);

        public IWebElement EndDateInput => FindByCss("input[id='EndEvent']", DefaultWaitingInterval);

        public IWebElement CreateFormError(string textPresent) => FindByCssWithText("div[id='valid-form-create-event']", textPresent, DefaultWaitingInterval);

        public IWebElement GetFieldByPartialId(string fieldId)
        {
            return Driver.FindElement(By.CssSelector("input[id*='" + fieldId + "']"));
        }

        public void SelectLayoutIdFromDropDown(string layoutId)
        {
            var dropDownList = Driver.FindElement(By.Id("layout_list"));
            var selectElement = new SelectElement(dropDownList);

            selectElement.SelectByValue(layoutId);
        }
    }
}
