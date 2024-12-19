using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.Playwright.NUnit;
using PlaywrightWithNunitCSharp.Assets.Models;
using PlaywrightWithNunitCSharp.Pages;
using PlaywrightWithNunitCSharp.Utilities;

namespace PlaywrightWithNunitCSharp.Tests
{
  public class HotelAppInstallTest : PageTest
  {
    private string _baseUrl;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
      _baseUrl = JsonDataManager.Instance.GetApplicationUrl();
    }

    /// <summary>
    /// This test is not working for now as playwright doesnt support redirection to pdf file
    /// </summary>
    /// <param name="login">provides user login details</param>
    /// <returns></returns>
    [Test]
    [TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.GetLogins))]
    public async Task GetDownloadappLink(Login login)
    {
      try
      {
        //* Arrange
        var loginPage = new HotelAppInstallPage(Page);
        //* Act
        var downloadAppLocator = await loginPage.VisitPage(url: _baseUrl, user: login.username, pass: login.password);

        var page1 = await Page.RunAndWaitForPopupAsync(async () =>
        {
          await downloadAppLocator.ClickAsync();
        });
        //await page1.GotoAsync("https://adactinhotelapp.com/resources/AdactinHotelApp_SetupGuide.pdf");
        //var pdfBuffer = await page1.PdfAsync(new Microsoft.Playwright.PagePdfOptions { Format = "A4" });
        //var download = await page1.WaitForDownloadAsync();
        //var pdfContent = await page1.ContentAsync();
        //var pdfDocument = new PdfReader(pdfContent);
        //* Assert
        //using (var ms = new MemoryStream(pdfBuffer))
        //{
        //  var pdfReader = new PdfReader(ms);
        //  var text = PdfTextExtractor.GetTextFromPage(pdfReader,2);
        //}
        //bool isHeadless = Environment.GetEnvironmentVariable("PLAYWRIGHT_HEADLESS") == "true";
        //if (isHeadless)

        Assert.That(page1.Url, Is.EqualTo("https://adactinhotelapp.com/resources/AdactinHotelApp_SetupGuide.pdf"));
      }
      catch (Exception ex)
      {
        Assert.Fail($"Failed to log in with user '{login.username}': {ex.Message}");
      }
    }
  }
}