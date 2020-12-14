using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace AQATM.WebPages
{
    public class TMBuyTicketsPage : AbstractPage
    {
        private const int DefaultWaitingInterval = 1;

        public TMBuyTicketsPage(IWebDriver driver)
            : base(driver)
        {
        }

        public IWebElement BuyButton => FindByCss("input[id='tm-seats-buy-btn']", DefaultWaitingInterval);

        public IWebElement SeatsIdInput => FindByCss("input[id='SeatsId']", DefaultWaitingInterval);

        public IWebElement BuyTicketFormError(string textPresent)
            => FindByCssWithText("div[class *= 'validation-summary-errors']", textPresent, DefaultWaitingInterval);

        public List<IWebElement> GetBusySeats()
        {
            return Driver.FindElements(By.CssSelector("span[class='tm-seat-busy']")).ToList();
        }

        public List<IWebElement> GetFreeSeats()
        {
            return Driver.FindElements(By.CssSelector("span[class='tm-seat-free']")).ToList();
        }

        public List<IWebElement> GetDatePurchase()
        {
            return Driver.FindElements(By.CssSelector("td[class='tm-booking-date']")).ToList();
        }

        public List<IWebElement> GetReturnTicketButtons()
        {
            return Driver.FindElements(By.CssSelector("a[class='return-tiket-btn']")).ToList();
        }

        public void OpenPurchaseHistory()
        {
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            Driver.Url = "https://localhost:44366/Purchase";
        }
    }
}
