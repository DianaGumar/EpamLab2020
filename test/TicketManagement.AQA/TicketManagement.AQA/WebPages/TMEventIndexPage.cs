using System;
using OpenQA.Selenium;

namespace AQATM.WebPages
{
    public class TMEventIndexPage : AbstractPage
    {
        private const int DefaultWaitingInterval = 1;

        public TMEventIndexPage(IWebDriver driver)
            : base(driver)
        {
        }

        public IWebElement SeeAllEventButton => FindByCss("a[id='TMLinkLogOff']", DefaultWaitingInterval);

        public IWebElement SeeAllRelevantEventButton => FindByCss("a[id='registerLink']", DefaultWaitingInterval);

        public IWebElement CreateNewButton => FindByCss("input[id='TMFinalRegisterButton']", DefaultWaitingInterval);

        public IWebElement ChouseSeatsButton => FindByCss("a[id='TMChouseSeatsButton']", DefaultWaitingInterval);

        public IWebElement MiddlePriseSpan => FindByCss("span[id='tmMiddlePrise']", DefaultWaitingInterval);

        public void Open()
        {
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            Driver.Url = "https://localhost:44366";
        }

        public IWebElement GetEvent(string eventBlockId)
        {
            return FindByCss("div[id='" + eventBlockId + "']", DefaultWaitingInterval);
        }

        public IWebElement GetManageButtonById(string eventBlockId, string buttonClass)
        {
            IWebElement tmevent = GetEvent(eventBlockId);

            return tmevent.FindElement(By.CssSelector("a[class$='" + buttonClass + "']"));
        }
    }
}
