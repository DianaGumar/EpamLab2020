using System;
using OpenQA.Selenium;

namespace AQATM.WebPages
{
    public class TMAutorizedPage : AbstractPage
    {
        private const int DefaultWaitingInterval = 1;

        public TMAutorizedPage(IWebDriver driver)
            : base(driver)
        {
        }

        public IWebElement FinalLogInButton => FindByCss("input[id='TMLogInButton']", DefaultWaitingInterval);

        public IWebElement LogInButton => FindByCss("a[id='loginLink']", DefaultWaitingInterval);

        public IWebElement EmailInput => FindByCss("input[id='Email']", DefaultWaitingInterval);

        public IWebElement PasswordInput => FindByCss("input[id='Password']", DefaultWaitingInterval);

        public IWebElement LogInFormError(string textPresent) => FindByCssWithText("div[id='validationLogInFormMs']", textPresent, DefaultWaitingInterval);

        public IWebElement LogInEmailFormError(string textPresent) => FindByCssWithText("span[id='validationFieldEmail']", textPresent, DefaultWaitingInterval);

        public IWebElement LogInPasswordFormError(string textPresent) => FindByCssWithText("span[id='validationFieldPassword']", textPresent, DefaultWaitingInterval);

        public bool LoginButtonIsExist()
        {
            return Driver.FindElements(By.CssSelector("a[id='loginLink']")).Count > 0;
        }

        public void Open()
        {
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            Driver.Url = "https://localhost:44366";
        }
    }
}
