using System;
using System.Linq;
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

        public IWebElement CreateNewButton => FindByCss("a[id='create-new-btn']", DefaultWaitingInterval);

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

        public IWebElement GetEventByName(string eventName)
        {
            return Driver.FindElements(By.CssSelector("div[class*='tm-event-view']"))
                .FirstOrDefault(w => w.FindElement(By.CssSelector("span[class*='tm-event-name']"))
                .Text.Equals(eventName, StringComparison.OrdinalIgnoreCase));
        }

        public bool IsEventExist(string eventBlockId)
        {
            return Driver.FindElements(By.CssSelector("div[id='" + eventBlockId + "']")).Count > 0;
        }

        public bool IsEventExistByName(string eventname)
        {
            var names = Driver.FindElements(By.CssSelector("span[class*='tm-event-name']"));
            var newNames = names
                .FirstOrDefault(w => w.Text.Equals(eventname, StringComparison.OrdinalIgnoreCase));
            return newNames != null;
        }

        public IWebElement GetManageButtonById(string eventBlockId, string buttonClass)
        {
            IWebElement tmevent = GetEvent(eventBlockId);
            return tmevent.FindElement(By.CssSelector("a[class$='" + buttonClass + "']"));
        }
    }
}
