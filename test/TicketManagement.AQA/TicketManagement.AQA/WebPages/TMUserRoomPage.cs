using System;
using OpenQA.Selenium;

namespace AQATM.WebPages
{
    public class TMUserRoomPage : AbstractPage
    {
        private const int DefaultWaitingInterval = 1;

        public TMUserRoomPage(IWebDriver driver)
            : base(driver)
        {
        }

        public IWebElement TopUpBalanceButton => FindByCss("a[id='tm-topup_balance_btn']", DefaultWaitingInterval);

        public IWebElement FinalTopUpBalanceButton => FindByCss("input[id='tm-topup-balanse-btn']", DefaultWaitingInterval);

        public IWebElement TopUpBalanceInput => FindByCss("input[id='TopUpSum']", DefaultWaitingInterval);

        public IWebElement CurrentBalance => FindByCss("span[id='tm-current-balance']", DefaultWaitingInterval);

        public void Open()
        {
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            Driver.Url = "https://localhost:44366/Manage";
        }
    }
}
