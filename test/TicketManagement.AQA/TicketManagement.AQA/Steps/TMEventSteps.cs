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
        private static TMRegistratePage TMRegPage => PageFactory.Get<TMRegistratePage>();

        private static TMAutorizedPage AutorizedPage => PageFactory.Get<TMAutorizedPage>();

        private static TMEventIndexPage EventIndexPage => PageFactory.Get<TMEventIndexPage>();

        private static TMSetPricePage PricePage => PageFactory.Get<TMSetPricePage>();

        private static TMEditEventPage EditEventPage => PageFactory.Get<TMEditEventPage>();

        private static TMCreateEventPage CreateEventPage => PageFactory.Get<TMCreateEventPage>();

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

        [BeforeScenario("layout_busy_seats")]
        public static void SetSomeSeatBusy()
        {
            // make seats busy on layout 2
            throw new NotImplementedException();
        }

        ////[BeforeScenario("delete_event")]
        ////public static void SetSomeSeatBusy()
        ////{
        ////    // make seats busy on layout 2
        ////    throw new NotImplementedException();
        ////}

        [Then(@"User can't see event with Name ""(.*)"" and Id ""(.*)"" at index page")]
        public void ThenUserCanTSeeEventWithNameAndIdAtIndexPage(string p0, string p1)
        {
            _ = p0;
            bool tmevent = EventIndexPage.IsEventExist(p1);
            Assert.AreEqual(false, tmevent);
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
            if (EventIndexPage.IsEventExist(eventId))
            {
                var but = EventIndexPage.GetManageButtonById(eventId, buttonClass);
                but.Click();
            }
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

        [When(@"User set Description event ""(.*)""")]
        public void WhenUserSetDescriptionEvent(string p0)
        {
            EditEventPage.DescInput.SendKeys(Keys.Control + "a");
            EditEventPage.DescInput.SendKeys(p0);
        }

        [When(@"User clicks FinalEdit button")]
        public void WhenUserClicksFinalEditButton()
        {
            EditEventPage.FinalEditButton.Click();
        }

        [Then(@"User can see event with Description ""(.*)"" and Id ""(.*)"" at index page")]
        public void ThenUserCanSeeEventWithDescriptionAndIdAtIndexPage(string desc, string p1)
        {
            var tmevent = EventIndexPage.GetEvent(p1);
            string realDesc = tmevent.FindElement(By.CssSelector("span[id='tm-event-desc']")).Text;
            Assert.AreEqual(desc, realDesc);
        }

        [When(@"User set Layout event ""(.*)""")]
        public void WhenUserSetLayoutEvent(string p0)
        {
            EditEventPage.LayoutIdInput.SendKeys(Keys.Control + "a");
            EditEventPage.LayoutIdInput.SendKeys(p0);
        }

        [Then(@"Event edit form has error ""(.*)""")]
        public void ThenEventEditFormHasError(string expectedErrorMsg)
        {
            var errorMsg = "";
            Assert.DoesNotThrow(() => errorMsg = EditEventPage.EditFormError(expectedErrorMsg).Text);
            Assert.That(errorMsg, Is.EqualTo(expectedErrorMsg));
        }

        [When(@"User set StartDate event ""(.*)""")]
        public void WhenUserSetStartDateEvent(string p0)
        {
            EditEventPage.StartDateInput.SendKeys(Keys.Control + "a");
            EditEventPage.StartDateInput.SendKeys(p0);
        }

        [When(@"User set EndDate event ""(.*)""")]
        public void WhenUserSetEndDateEvent(string p0)
        {
            EditEventPage.EndDateInput.SendKeys(Keys.Control + "a");
            EditEventPage.EndDateInput.SendKeys(p0);
        }

        [When(@"User clicks CreateNew button")]
        public void WhenUserClicksCreateNewButton()
        {
            EventIndexPage.CreateNewButton.Click();
        }

        [When(@"User set ""(.*)"" field ""(.*)"" in create event form")]
        public void WhenUserSetFieldInCreateEventForm(string fieldid, string value)
        {
            CreateEventPage.GetFieldByPartialId(fieldid).SendKeys(value);
        }

        [When(@"User clicks FinalCreate button")]
        public void WhenUserClicksFinalCreateButton()
        {
            CreateEventPage.FinalCreateButton.Click();
        }

        [Then(@"User can see event with Name ""(.*)"" at index page")]
        public void ThenUserCanSeeEventWithNameAtIndexPage(string p0)
        {
            bool tmevent = EventIndexPage.IsEventExistByName(p0);
            Assert.AreEqual(true, tmevent);
        }

        [When(@"User set datetime ""(.*)"" field date ""(.*)"" time ""(.*)"" in create event form")]
        public void WhenUserSetDatetimeFieldDateTimeInCreateEventForm(string fieldid, string date, string time)
        {
            var picker = CreateEventPage.GetFieldByPartialId(fieldid);
            picker.SendKeys(date);
            picker.SendKeys(Keys.Tab);
            picker.SendKeys(time);
        }

        [When(@"User select from dropDown ""(.*)"" layoutId")]
        public void WhenUserSelectFromDropDownLayoutId(string p0)
        {
            CreateEventPage.SelectLayoutIdFromDropDown(p0);
        }

        [When(@"User set Price ""(.*)"" at all Price fields")]
        public void WhenUserSetPriceAtAllPriceFields(string p0)
        {
            var spf = PricePage.GetSetPriseFields();
            var spb = PricePage.GetSetPriseBtns();

            for (int i = 0; i < spf.Count; i++)
            {
                spf[i].SendKeys(Keys.Control + "a");
                spf[i].SendKeys(p0);
                spb[i].Click();
                CreateEventPage.TakeDelay(1, "input[id='Price']");
                spf = PricePage.GetSetPriseFields();
                spb = PricePage.GetSetPriseBtns();
            }
        }

        [Then(@"User clicks ""(.*)"" button on event with Name ""(.*)""")]
        public void ThenUserClicksButtonOnEventWithName(string btn, string eventName)
        {
            var tmevent = EventIndexPage.GetEventByName(eventName);

            var btnb = tmevent.FindElement(By.CssSelector("a[class*='" + btn + "']"));
            btnb.Click();
        }
    }
}
