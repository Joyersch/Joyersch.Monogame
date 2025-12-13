using Joyersch.Monogame.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Joyersch.Monogame.Storage;

public class JsonFileFormatHandler : IFileFormatHandler
{
    public void ReadFile(Type[] implementations, Dictionary<string, object> collection, byte[] data)
    {
        JObject jsonObject = JObject.Parse(System.Text.Encoding.Default.GetString(data));

        foreach (var pair in jsonObject)
        {
            try
            {
                string key = pair.Key;
                JToken value = pair.Value;

                Type settingsType = implementations.First(i => i.Namespace.Split('.')[^1] + "." + i.Name == key);
                if (settingsType != null && collection.ContainsKey(key))
                {
                    object instance = JsonConvert.DeserializeObject(value.ToString(), settingsType);
                    collection[key] = instance;
                }
            }
            catch (Exception exception)
            {
                Log.Error($"Error while loading file with pair: {pair.Key} into: {nameof(collection)}");
                Log.Error(exception.Message);
            }
        }
    }

    public byte[] WriteFile(Dictionary<string, object> data)
    {
        return System.Text.Encoding.Default.GetBytes(JsonConvert.SerializeObject(data));
    }
}