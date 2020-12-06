using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AQATM.WebPages
{
    public class TMEventModelsIndexPage : AbstractPage
    {
        private const int DefaultWaitingInterval = 1;

        public TMEventModelsIndexPage(IWebDriver driver)
            : base(driver)
        {
        }

        public IWebElement LogOffButton => FindByCss("a[id='TMLinkLogOff']", DefaultWaitingInterval);

        public IWebElement RegisterButton => FindByCss("a[id='registerLink']", DefaultWaitingInterval);

        public IWebElement FinalRegisterButton => FindByCss("input[id='TMFinalRegisterButton']", DefaultWaitingInterval);

        public IWebElement EmailInput => FindByCss("input[id='Email']", DefaultWaitingInterval);

        public IWebElement PasswordInput => FindByCss("input[id='Password']", DefaultWaitingInterval);

        public IWebElement ConfirmPasswordInput => FindByCss("input[id='ConfirmPassword']", DefaultWaitingInterval);

        public IWebElement RegisterFormError(string textPresent) => FindByCssWithText("div[id='TMRegisterValidationErrors']", textPresent, DefaultWaitingInterval);

        public IWebElement UserProfileLink(string textPresent) => FindByCssWithText("a[id='TMUserRoomLink']", textPresent, DefaultWaitingInterval);

        public void Open()
        {
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            Driver.Url = "https://localhost:44366";
        }

        public void SelectRoleFromDropDown(string role)
        {
            var dropDownList = Driver.FindElement(By.Id("UserRole"));
            var selectElement = new SelectElement(dropDownList);

            selectElement.SelectByValue(role);
        }
    }
}
