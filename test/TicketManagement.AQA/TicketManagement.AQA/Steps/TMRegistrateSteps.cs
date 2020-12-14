using System;
using System.Linq;
using System.Threading.Tasks;
using AQATM.WebPages;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace AQATM.Steps
{
    [Binding]
    public class TMRegistrateSteps
    {
        private static readonly Random _random = new Random((int)DateTime.Now.Ticks);

        private static string _email = "";

        private static TMRegistratePage TMRegPage => PageFactory.Get<TMRegistratePage>();

        private static string RandomString(int length)
        {
            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, length)
                .Select(x => pool[_random.Next(0, pool.Length)]);
            return new string(chars.ToArray());
        }

        private string GetValidEmail()
        {
            if (string.IsNullOrEmpty(_email))
            {
                _email = RandomString(10) + "@gmail.com";
            }

            return _email;
        }

        [Given(@"User is on TM")]
        public void GivenUserIsOnTM()
        {
            TMRegPage.Open();
        }

        [When(@"User clicks Register button")]
        public async Task WhenUserClicksRegisterButtonAsync()
        {
            await Task.Delay(3000);
            TMRegPage.RegisterButton.Click();
        }

        [When(@"Enters ""(.*)"" to user Email input")]
        public void WhenEntersToUserEmailInput(string p0)
        {
            TMRegPage.EmailInput.SendKeys(p0);
        }

        [When(@"Enters ""(.*)"" to Password field")]
        public void WhenEntersToPasswordField(string p0)
        {
            TMRegPage.PasswordInput.SendKeys(p0);
        }

        [When(@"Enters ""(.*)"" to ConfirmPassword field")]
        public void WhenEntersToConfirmPasswordField(string p0)
        {
            TMRegPage.ConfirmPasswordInput.SendKeys(p0);
        }

        [When(@"User clicks FinalRegister button")]
        public async Task WhenUserClicksFinalRegisterButtonAsync()
        {
            await Task.Delay(3000);
            TMRegPage.FinalRegisterButton.Click();
        }

        [Then(@"Register form has error ""(.*)""")]
        public void ThenRegisterFormHasError(string expectedErrorMsg)
        {
            var errorMsg = "";
            Assert.DoesNotThrow(() => errorMsg = TMRegPage.RegisterFormError(expectedErrorMsg).Text);
            Assert.That(errorMsg, Is.EqualTo(expectedErrorMsg));
        }

        [When(@"User shouse ""(.*)"" role at UserRole field")]
        public void WhenUserShouseRoleAtUserRoleField(string role)
        {
            TMRegPage.SelectRoleFromDropDown(role);
        }

        [Then(@"User see profile link with hello text")]
        public void ThenUserSeeProfileLinkWithHelloText()
        {
            var expectedMsg = "Hello";
            _email = "";
            var msg = "";
            Assert.DoesNotThrow(() => msg = TMRegPage.UserProfileLink(expectedMsg).Text);

            Assert.IsTrue(msg.Contains(expectedMsg));

            /////TMRegPage.TakeScrinshot("error.png");
        }

        [When(@"Enters user Email into input")]
        public void WhenEntersUserEmailIntoInput()
        {
            TMRegPage.EmailInput.SendKeys(GetValidEmail());
        }

        [Then(@"User Logout")]
        public async Task ThenUserLogoutAsync()
        {
            await Task.Delay(3000);
            TMRegPage.LogOffButton.Click();
        }
    }
}
