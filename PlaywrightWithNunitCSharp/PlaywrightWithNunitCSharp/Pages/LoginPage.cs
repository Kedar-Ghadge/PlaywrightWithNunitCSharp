using Microsoft.Playwright;

namespace PlaywrightWithNunitCSharp.Pages
{
  public class LoginPage(IPage page)
  {
    private readonly IPage _page = page;
    private readonly LoginLocator _loginLocator = new LoginLocator(page);

    //[AllureStep("login with url{0} user{1} pass{2}")]
    public async Task<LoginLocator> LogInInfo(string url, string user, string pass)
    {
      await _page.GotoAsync(url);
      await _loginLocator.UsernameTxt.FillAsync(user);
      await _loginLocator.PasswordTxt.FillAsync(pass);
      await _loginLocator.LoginBtn.ClickAsync();
      //await _page.WaitForTimeoutAsync(20000);
      await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      return _loginLocator;
    }

    public async Task<LoginLocator> LoginRagister(string url)
    {
      var loginLocator = new LoginLocator(_page);
      await _page.GotoAsync(url);
      await loginLocator.LoginRegister.ClickAsync();
      await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      return loginLocator;
    }

    public async Task VisitPage(string url)
    {
      await _page.GotoAsync(url);
    }

    public ILocator GetUserNameLocator() => _loginLocator.UsernameTxt;

    public ILocator GetPasswordLocator() => _loginLocator.PasswordTxt;

    public ILocator GetSignInLocator() => _loginLocator.LoginBtn;
  }

  public class LoginLocator(IPage page)
  {
    public readonly IPage _page = page;
    public ILocator UsernameTxt => _page.Locator("#username");
    public ILocator PasswordTxt => _page.Locator("#password");
    public ILocator LoginBtn => _page.Locator("#login");
    public ILocator LoginRegister => _page.Locator(".login_register");
    public ILocator LoginRegisterTitle => _page.Locator(".login_title");
    public ILocator WelcomeMenu => _page.Locator(".welcome_menu").First;
  }
}