using System;
using AQATM.Utils;
using AQATM.WebPages;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace AQATM.Steps
{
    [Binding]
#pragma warning disable CA1052 // Static holder types should be Static or NotInheritable
    public class PageFactory
#pragma warning restore CA1052 // Static holder types should be Static or NotInheritable
    {
        private static readonly Lazy<PageFactory> Lazy = new Lazy<PageFactory>(() => new PageFactory());

        private static IWebDriver _driver;

        private PageFactory()
        {
        }

        public static PageFactory Instance => Lazy.Value;

        public static T Get<T>()
            where T : AbstractPage
        {
            object[] args = { _driver };
            return (T) Activator.CreateInstance(typeof(T), args);
        }

        [BeforeFeature]
        public static void OpenBrowser()
        {
            _driver = DriverFactory.GetDriver();
        }

        [AfterFeature]
        public static void CloseBrowser()
        {
            _driver.Close();
            _driver.Dispose();
        }
    }
}
