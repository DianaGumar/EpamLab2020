using System;
using AQATM.WebPages;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace AQATM.Steps
{
    [Binding]
    public class TMEventSteps
    {
        ////private readonly string _email = "eventmanagerexample@gmail.com";

        ////private readonly string _password = "x6@9hkrmWZNjmzY34";

        private static TMRegistratePage TMRegPage => PageFactory.Get<TMRegistratePage>();

        private static TMAutorizedPage AutorizedPage => PageFactory.Get<TMAutorizedPage>();

        private static TMEventIndexPage EventIndexPage => PageFactory.Get<TMEventIndexPage>();

        private static TMSetPricePage PricePage => PageFactory.Get<TMSetPricePage>();

        [BeforeFeature("user_eventmanager_account_exist")]
        public static void RegistrateAsTMEventManager()
        {
            TMRegPage.Open();
            TMRegPage.RegisterButton.Click();
            TMRegPage.EmailInput.SendKeys("eventmanagerexample@gmail.com");
            TMRegPage.PasswordInput.SendKeys("x6@9hkrmWZNjmzY34");
            TMRegPage.ConfirmPasswordInput.SendKeys("x6@9hkrmWZNjmzY34");
            TMRegPage.FinalRegisterButton.Click();
            TMRegPage.Open();

            if (AutorizedPage.LogInButton != null)
            {
                AutorizedPage.LogInButton.Click();
                AutorizedPage.EmailInput.SendKeys("eventmanagerexample@gmail.com");
                AutorizedPage.PasswordInput.SendKeys("x6@9hkrmWZNjmzY34");
                AutorizedPage.FinalLogInButton.Click();
            }
        }

        [AfterFeature("user_eventmanager_account_exist")]
        public static void UserLogOut()
        {
            TMRegPage.LogOffButton.Click();
        }

        [Then(@"User can't see event with Name ""(.*)"" and Id ""(.*)"" at index page")]
        public void ThenUserCanTSeeEventWithNameAndIdAtIndexPage(string p0, string p1)
        {
            _ = p0;
            var tmevent = EventIndexPage.GetEvent(p1);
            Assert.IsNull(tmevent);
        }

        [Then(@"User can see event with Name ""(.*)"" and Id ""(.*)"" at index page")]
        public void ThenUserCanSeeEventWithNameAndIdAtIndexPage(string p0, string p1)
        {
            _ = p0;
            var tmevent = EventIndexPage.GetEvent(p1);
            Assert.IsNotNull(tmevent);
        }

        [Given(@"event with Name ""(.*)"" and Id ""(.*)"" has busy seat")]
        public void GivenEventWithNameAndIdHasBusySeat(string p0, int p1)
        {
            throw new NotImplementedException();
        }

        [When(@"User clicks ""(.*)"" button on event with Name ""(.*)"" and Id ""(.*)""")]
        public void WhenUserClicksButtonOnEventWithNameAndId(string buttonClass,
            string eventName, string eventId)
        {
            _ = eventName;
            var but = EventIndexPage.GetManageButtonById(eventId, buttonClass);
            but.Click();
        }

        [Then(@"User can see buyTicketButton")]
        public void ThenUserCanSeeBuyTicketButton()
        {
            Assert.IsNotNull(EventIndexPage.ChouseSeatsButton);
        }

        [When(@"User set Price ""(.*)"" at Price field")]
        public void WhenUserSetPriceAtPriceField(string p0)
        {
            PricePage.PriceField.SendKeys(Keys.Control + "a");
            PricePage.PriceField.SendKeys(p0);
        }

        [When(@"User clicks SetPrise button")]
        public void WhenUserClicksSetPriseButton()
        {
            PricePage.FinalSetPriseButton.Click();
        }

        [When(@"User clicks BackToList button")]
        public void WhenUserClicksBackToListButton()
        {
            PricePage.BackToListButton.Click();
        }

        [Then(@"User can see middle price = ""(.*)""")]
        public void ThenUserCanSeeMiddlePrice(string price)
        {
            var realPrice = "";
            Assert.DoesNotThrow(() => realPrice = EventIndexPage.MiddlePriseSpan.Text);
            Assert.That(realPrice, Is.EqualTo(price));
        }

        [Then(@"User clicks SeeAllEvents button")]
        public void ThenUserClicksSeeAllEventsButton()
        {
            EventIndexPage.SeeAllEventButton.Click();
        }

        [Then(@"User take back event price")]
        public void ThenUserTakeBackEventPrice()
        {
            PricePage.PriceField.SendKeys("10");
            PricePage.FinalSetPriseButton.Click();
        }
    }
}
