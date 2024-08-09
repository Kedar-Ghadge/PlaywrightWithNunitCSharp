using PlaywrightWithNunitCSharp.Assets.Models;

namespace PlaywrightWithNunitCSharp.Utilities
{
  public class TestDataProvider
  {
    private static object collectionLock = new object();

    public static IEnumerable<Login> GetLogins()
    {
      var logins = JsonDataManager.Instance.DeserializeJsonFile<Login>("Login.json");
      foreach (var login in logins.Values.ToList())
      {
        Monitor.Enter(collectionLock);
        yield return login;
        Monitor.Exit(collectionLock);
      }
    }
  }
}