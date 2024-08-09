using System.Text.Json;

namespace PlaywrightWithNunitCSharp.Utilities
{
  public sealed record JsonDataManager
  {
    private static JsonDataManager? instance;
    private string jsonDataPath;

    private JsonDataManager() { }

    public static JsonDataManager Instance => instance ??= new();

    public string GetJsonDataPath() =>
        jsonDataPath ??= TestContext.Parameters["JsonDataPath"]
            ?? throw new Exception("JsonDataPath is not configured as a parameter.");

    public string GetApplicationUrl() =>
      TestContext.Parameters["ApplicationUrl"]
            ?? throw new Exception("JsonDataPath is not configured as a parameter.");

    private bool IsMultipleUserFlagEnabled()
    {
      var flag = TestContext.Parameters["IsMultipleUserEnabled"]
            ?? throw new Exception("JsonDataPath for IsMultipleUserEnabled  is not configured as a parameter.");
      return flag == "Y";
    }

    private string GetSingleUserKey() =>
      TestContext.Parameters["SingleUserKey"]
            ?? throw new Exception("JsonDataPath for SingleUserKey is not configured as a parameter.");
    //public void SetJsonDataPath(string path) => jsonDataPath = path;

    public string GetJsonFilePath(string fileName) => Path.Combine(GetJsonDataPath(), fileName);

    public Dictionary<string, T> DeserializeJsonFile<T>(string fileName)
    {
      string filePath = GetJsonFilePath(fileName);

      if (!File.Exists(filePath))
      {
        throw new FileNotFoundException("JSON file not found.", filePath);
      }

      string jsonContent = File.ReadAllText(filePath);
      if (!IsMultipleUserFlagEnabled())
      {
        var singleUserDetails = JsonSerializer.Deserialize<Dictionary<string, T>>(jsonContent) ?? throw new NullReferenceException();
        var singleUserKey = GetSingleUserKey();
        return new Dictionary<string, T> { { singleUserKey, singleUserDetails[singleUserKey] } };
      }

      return JsonSerializer.Deserialize<Dictionary<string, T>>(jsonContent) ?? throw new NullReferenceException();
    }
  }
}