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

        ////private static string _password = "";

        private static TMEventModelsIndexPage TMEventIndexPage => PageFactory.Get<TMEventModelsIndexPage>();

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

        ////private string SuggestValidPassword()
        ////{
        ////    if (string.IsNullOrEmpty(_password))
        ////    {
        ////        /////_password = System.Web.Security.Membership.GeneratePassword(25, 10)
        ////        _password = "x6@9hkrmWZNjmzY";
        ////    }

        ////    return _password;
        ////}

        [Given(@"User is on TM")]
        public void GivenUserIsOnTM()
        {
            TMEventIndexPage.Open();
        }

        [When(@"User clicks Register button")]
        public async Task WhenUserClicksRegisterButtonAsync()
        {
            await Task.Delay(3000);
            TMEventIndexPage.RegisterButton.Click();
        }

        [When(@"Enters ""(.*)"" to user Email input")]
        public void WhenEntersToUserEmailInput(string p0)
        {
            TMEventIndexPage.EmailInput.SendKeys(p0);
        }

        [When(@"Enters ""(.*)"" to Password field")]
        public void WhenEntersToPasswordField(string p0)
        {
            TMEventIndexPage.PasswordInput.SendKeys(p0);
        }

        [When(@"Enters ""(.*)"" to ConfirmPassword field")]
        public void WhenEntersToConfirmPasswordField(string p0)
        {
            TMEventIndexPage.ConfirmPasswordInput.SendKeys(p0);
        }

        [When(@"User clicks FinalRegister button")]
        public async Task WhenUserClicksFinalRegisterButtonAsync()
        {
            await Task.Delay(3000);
            TMEventIndexPage.FinalRegisterButton.Click();
        }

        [Then(@"Register form has error ""(.*)""")]
        public void ThenRegisterFormHasError(string expectedErrorMsg)
        {
            var errorMsg = "";
            Assert.DoesNotThrow(() => errorMsg = TMEventIndexPage.RegisterFormError(expectedErrorMsg).Text);
            Assert.That(errorMsg, Is.EqualTo(expectedErrorMsg));
        }

        [When(@"User shouse ""(.*)"" role at UserRole field")]
        public void WhenUserShouseRoleAtUserRoleField(string role)
        {
            TMEventIndexPage.SelectRoleFromDropDown(role);
        }

        [Then(@"User see profile link with hello text")]
        public void ThenUserSeeProfileLinkWithHelloText()
        {
            var expectedMsg = "Hello " + GetValidEmail() + "!";
            _email = "";
            var msg = "";
            Assert.DoesNotThrow(() => msg = TMEventIndexPage.UserProfileLink(expectedMsg).Text);

            Assert.That(msg, Is.EqualTo(expectedMsg));

            /////TMEventIndexPage.TakeScrinshot("error.png");
        }

        [When(@"Enters user Email into input")]
        public void WhenEntersUserEmailIntoInput()
        {
            TMEventIndexPage.EmailInput.SendKeys(GetValidEmail());
        }

        [Then(@"User Logout")]
        public async Task ThenUserLogoutAsync()
        {
            await Task.Delay(3000);
            TMEventIndexPage.LogOffButton.Click();
        }
    }
}
