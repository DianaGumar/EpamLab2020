using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

        [BeforeScenario("layout_has_free_seats")]
        public static void MakeFreeTickets()
        {
            EventIndexPage.Open();

            EventIndexPage.GetAllEvents()[0]
                .FindElement(By.CssSelector("a[id='tm-buy-ticket-event-btn']")).Click();
            EventIndexPage.ChouseSeatsButton.Click();

            var seats = BuyTicketsPage.GetFreeSeats();

            if (seats.Count == 0)
            {
                BuyTicketsPage.OpenPurchaseHistory();

                var btn = BuyTicketsPage.GetReturnTicketButtons();

                for (int i = 0; i < btn.Count; i++)
                {
                    btn[i].Click();
                    ////BuyTicketsPage.TakeDelay(1, "input[id='Price']");
                    ////btn = BuyTicketsPage.GetReturnTicketButtons();
                }

                EventIndexPage.Open();
            }
        }

        [BeforeScenario("layout_has_busy_seats")]
        public static void MakeSeatBusy()
        {
            EventIndexPage.GetAllEvents()[0]
                .FindElement(By.CssSelector("a[id='tm-buy-ticket-event-btn']")).Click();
            EventIndexPage.ChouseSeatsButton.Click();

            var seats = BuyTicketsPage.GetBusySeats();

            if (seats.Count == 0)
            {
                seats = BuyTicketsPage.GetFreeSeats();
                BuyTicketsPage.SeatsIdInput.SendKeys(seats[0].Text);
                BuyTicketsPage.BuyButton.Click();
            }
        }

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
        public void GivenAutorizedUserHasBalance(decimal p0)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;

            UserRoomPage.Open();
            UserRoomPage.TopUpBalanceButton.Click();
            decimal balance = decimal.Parse(UserRoomPage.CurrentBalance.Text, provider);
            decimal b = p0 - balance;
            UserRoomPage.TopUpBalanceInput.SendKeys(Keys.Control + "a");
            UserRoomPage.TopUpBalanceInput.SendKeys(b.ToString(provider));
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

        [Then(@"User can see ""(.*)"" new seats at own purchase history")]
        public void ThenUserCanSeeNewSeatsAtOwnPurchaseHistory(int seatsCount)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;

            BuyTicketsPage.OpenPurchaseHistory();
            var date = BuyTicketsPage.GetDatePurchase();
            var date_s = date.Select(d => d.Text).ToList();

            var date_d = new List<DateTime>();
            foreach (var item in date_s)
            {
                date_d.Add(DateTime.Parse(item, provider));
            }

            int result = date_d.Count(d => DateTime.Now.AddSeconds(-2) < d);

            Assert.AreEqual(seatsCount, result);
        }

        [Then(@"User can see error text ""(.*)""")]
        public void ThenUserCanSeeErrorText(string expectedErrorMsg)
        {
            var errorMsg = "";
            Assert.DoesNotThrow(() => errorMsg = BuyTicketsPage.BuyTicketFormError(expectedErrorMsg).Text);
            Assert.That(errorMsg, Is.EqualTo(expectedErrorMsg));
        }

        [When(@"User choose busy seats")]
        public void WhenUserChooseBusySeats()
        {
            var seats_b = BuyTicketsPage.GetBusySeats();

            if (seats_b.Count >= 1)
            {
                BuyTicketsPage.SeatsIdInput.SendKeys(seats_b[0].Text);
            }
        }

        [Then(@"User return ticket")]
        public void ThenUserReturnTicket()
        {
            BuyTicketsPage.OpenPurchaseHistory();
            var btn = BuyTicketsPage.GetReturnTicketButtons();

            for (int i = 0; i < btn.Count; i++)
            {
                btn[i].Click();
                btn = BuyTicketsPage.GetReturnTicketButtons();
            }

            EventIndexPage.Open();
        }
    }
}
