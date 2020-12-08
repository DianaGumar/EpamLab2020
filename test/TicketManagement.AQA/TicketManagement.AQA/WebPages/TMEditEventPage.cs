using OpenQA.Selenium;

namespace AQATM.WebPages
{
    public class TMEditEventPage : AbstractPage
    {
        private const int DefaultWaitingInterval = 1;

        public TMEditEventPage(IWebDriver driver)
            : base(driver)
        {
        }

        public IWebElement FinalEditButton => FindByCss("input[id='tmeditsavebtn']", DefaultWaitingInterval);

        public IWebElement BackToListButtonButton => FindByCss("div[id='backtolisttmeventedit']", DefaultWaitingInterval);

        public IWebElement NameInput => FindByCss("input[id='Name']", DefaultWaitingInterval);

        public IWebElement DescInput => FindByCss("input[id='Description']", DefaultWaitingInterval);

        public IWebElement ImgInput => FindByCss("input[id='Img']", DefaultWaitingInterval);

        public IWebElement StartDateInput => FindByCss("input[id='StartEvent']", DefaultWaitingInterval);

        public IWebElement EndDateInput => FindByCss("input[id='EndEvent']", DefaultWaitingInterval);

        public IWebElement LayoutIdInput => FindByCss("input[id='TMLayoutId']", DefaultWaitingInterval);

        public IWebElement EditFormError(string textPresent) => FindByCssWithText("div[id='TMediteventValidationErrors']", textPresent, DefaultWaitingInterval);
    }
}
