////using System;
////using System.Globalization;
////using System.Linq;
using AQATM.WebPages;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace AQATM.Steps
{
    [Binding]
    public class PurchaseSteps
    {
        private static TMRegistratePage TMRegPage => PageFactory.Get<TMRegistratePage>();

        private static TMAutorizedPage AutorizedPage => PageFactory.Get<TMAutorizedPage>();

        private static TMEventIndexPage EventIndexPage => PageFactory.Get<TMEventIndexPage>();

        private static TMBuyTicketsPage BuyTicketsPage => PageFactory.Get<TMBuyTicketsPage>();

        private static TMUserRoomPage UserRoomPage => PageFactory.Get<TMUserRoomPage>();

        [BeforeFeature("registeruser_account_exist")]
        public static void RegistrateAsTMEventManager()
        {
            TMRegPage.Open();
            TMRegPage.RegisterButton.Click();
            TMRegPage.EmailInput.SendKeys("simpleuserexample@gmail.com");
            TMRegPage.PasswordInput.SendKeys("x6@9hkrmWZNjmzY68");
            TMRegPage.ConfirmPasswordInput.SendKeys("x6@9hkrmWZNjmzY68");
            TMRegPage.SelectRoleFromDropDown("authorizeduser");
            TMRegPage.FinalRegisterButton.Click();
            TMRegPage.Open();

            if (AutorizedPage.LoginButtonIsExist())
            {
                AutorizedPage.LogInButton.Click();
                AutorizedPage.EmailInput.SendKeys("simpleuserexample@gmail.com");
                AutorizedPage.PasswordInput.SendKeys("x6@9hkrmWZNjmzY68");
                AutorizedPage.FinalLogInButton.Click();
            }
        }

        [AfterFeature("registeruser_account_exist")]
        public static void UserLogOut()
        {
            TMRegPage.LogOffButton.Click();
        }

        [Given(@"Autorized User has balance ""(.*)""")]
        public void GivenAutorizedUserHasBalance(string p0)
        {
            UserRoomPage.Open();
            UserRoomPage.TopUpBalanceButton.Click();
            UserRoomPage.TopUpBalanceInput.SendKeys(p0);
            UserRoomPage.FinalTopUpBalanceButton.Click();
            EventIndexPage.Open();
        }

        [When(@"User go to event description")]
        public void WhenUserGoToEventDescription()
        {
            EventIndexPage.GetAllEvents()[0]
                .FindElement(By.CssSelector("a[id='tm-buy-ticket-event-btn']")).Click();
            EventIndexPage.ChouseSeatsButton.Click();
        }

        [When(@"User choose free seats")]
        public void WhenUserChooseFreeSeats()
        {
            var seats = BuyTicketsPage.GetFreeSeats();

            if (seats.Count >= 1)
            {
                BuyTicketsPage.SeatsIdInput.SendKeys(seats[0].Text + ",");
            }
        }

        [When(@"User buy tickets")]
        public void WhenUserBuyTickets()
        {
            BuyTicketsPage.BuyButton.Click();
        }

        [When(@"User choose one free and one busy seats")]
        public void WhenUserChooseOneFreeAndOneBusySeats()
        {
            var seats = BuyTicketsPage.GetFreeSeats();
            var seats_b = BuyTicketsPage.GetBusySeats();

            if (seats.Count >= 1 && seats_b.Count >= 1)
            {
                BuyTicketsPage.SeatsIdInput.SendKeys(seats[0].Text + "," + seats_b[0].Text);
            }
        }

        [Then(@"User can see ""(.*)"" new seats at own purchase history")]
        public void ThenUserCanSeeNewSeatsAtOwnPurchaseHistory(int seatsCount)
        {
            ////CultureInfo provider = CultureInfo.InvariantCulture;

            BuyTicketsPage.OpenPurchaseHistory();
            var date = BuyTicketsPage.GetDatePurchase();
            ////var date_d = date.Select(d => DateTime.ParseExact(d.Text, "d", provider));

            ////int result = date_d.Count(d => DateTime.Today.AddSeconds(-10) < d);

            int result = date.Count;

            Assert.AreEqual(true, result >= seatsCount);
        }
    }
}
