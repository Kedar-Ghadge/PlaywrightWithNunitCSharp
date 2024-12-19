using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightWithNunitCSharp.Assets.Models;
using PlaywrightWithNunitCSharp.Pages;
using PlaywrightWithNunitCSharp.Utilities;
using System.Text.RegularExpressions;

namespace PlaywrightWithNunitCSharp.Tests
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class Tests : PageTest
    {
        private string _baseUrl;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _baseUrl = JsonDataManager.Instance.GetApplicationUrl();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        //[Parallelizable(ParallelScope.Self)]
        public async Task UserNameEditable()
        {
            //var page = Browser
            //    .NewContextAsync(new CustomBrowserNewContextOptions().Create())
            //    .Result.NewPageAsync()
            //    .Result;
            try
            {
                //* Arrange

                //var newPage = await Browser.NewContextAsync(new CustomBrowserNewContextOptions().Create()).Result.NewPageAsync();
                LoginPage loginPage = new LoginPage(Page);
                //* Act
                await loginPage.VisitPage(_baseUrl);
                var userNameField = loginPage.GetUserNameLocator();
                //* Assert
                await Assertions.Expect(userNameField).ToBeEditableAsync();
            }
            catch (Exception ex)
            {
                Assert.Fail($"Username field on login page is not editable : {ex.Message}");
            }
        }

        [Test]
        //[Parallelizable(ParallelScope.Self)]
        public async Task PasswordEditable()
        {
            //var page = Browser
            //    .NewContextAsync(new CustomBrowserNewContextOptions().Create())
            //    .Result.NewPageAsync()
            //    .Result;
            try
            {
                //* Arrange
                LoginPage loginPage = new LoginPage(Page);
                //* Act
                await loginPage.VisitPage(_baseUrl);
                var passwordField = loginPage.GetPasswordLocator();
                //* Assert
                await Assertions.Expect(passwordField).ToBeEditableAsync();
            }
            catch (Exception ex)
            {
                Assert.Fail($"Password field on login page is not editable : {ex.Message}");
            }
        }

        [Test]
        //[Parallelizable(ParallelScope.Self)]
        public async Task SignInClickable()
        {
            //var page = Browser
            //    .NewContextAsync(new CustomBrowserNewContextOptions().Create())
            //    .Result.NewPageAsync()
            //    .Result;
            try
            {
                //* Arrange
                LoginPage loginPage = new LoginPage(Page);
                //* Act
                await loginPage.VisitPage(_baseUrl);
                var signInBtn = loginPage.GetSignInLocator();
                //* Assert
                await Assertions.Expect(signInBtn).ToBeEnabledAsync();
            }
            catch (Exception ex)
            {
                Assert.Fail($"Sign In button on login page is not clickable : {ex.Message}");
            }
        }

        [Test]
        [TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.GetLogins))]
        public async Task LogInInfo(Login login)
        {
            //page = Browser.NewContextAsync(new CustomBrowserNewContextOptions().Create()).Result.NewPageAsync().Result;
            //page = Browser.NewPageAsync().Result;
            try
            {
                //* Arrange
                LoginPage loginPage = new LoginPage(Page);
                //* Act
                var loginLocator = await loginPage.LogInInfo(url: _baseUrl, user: login.username, pass: login.password);

                //* Assert
                await Assertions.Expect(loginLocator.WelcomeMenu).ToHaveTextAsync(new Regex("Welcome to Adactin Group of Hotels"));

            }
            catch (Exception ex)
            {

                Assert.Fail($"Failed to log in with user '{login.username}': {ex.Message}");
            }
        }
    }
}