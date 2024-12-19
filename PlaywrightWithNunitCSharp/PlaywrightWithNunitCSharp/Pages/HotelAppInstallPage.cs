using Microsoft.Playwright;

namespace PlaywrightWithNunitCSharp.Pages
{
  public class HotelAppInstallPage(IPage page)
  {
    private readonly IPage _page = page;
    private readonly LoginLocator _loginLocator = new LoginLocator(page);
    private ILocator downloadAppLink => page.GetByRole(AriaRole.Cell, new() { Name = "Adactin Hotel Mobile App DOWNLOAD the Hotel Mobile App and extend your experience. Click to know more about how to get the app on Android or IOS.", Exact = true }).GetByRole(AriaRole.Link);

    public async Task<ILocator> VisitPage(string url, string user, string pass)
    {
      await _page.GotoAsync(url);
      await _loginLocator.UsernameTxt.FillAsync(user);
      await _loginLocator.PasswordTxt.FillAsync(pass);
      await _loginLocator.LoginBtn.ClickAsync();
      //await _page.WaitForTimeoutAsync(20000);
      await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
      return downloadAppLink;
    }

    public ILocator GetUserNameLocator() => _loginLocator.UsernameTxt;

    public ILocator GetPasswordLocator() => _loginLocator.PasswordTxt;

    public ILocator GetSignInLocator() => _loginLocator.LoginBtn;
  }
}