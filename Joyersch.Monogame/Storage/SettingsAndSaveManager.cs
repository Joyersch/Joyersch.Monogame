using Joyersch.Monogame.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Joyersch.Monogame.Storage;

public sealed class SettingsAndSaveManager<T>
{
    private string _basePath;
    private T _saveFileIndex;
    private readonly Dictionary<string, object> _settings;
    private readonly Dictionary<string, object> _saves;
    private readonly List<Type> _settingsImplementations;
    private readonly List<Type> _savesImplementations;

    public string SaveFilePrefix { get; set; } = "save";

    public string SaveFileType { get; set; } = "json";

    public string SettingsFileType { get; set; } = "json";

    public SettingsAndSaveManager(string basePath, T saveFileIndex)
    {
        _basePath = basePath;
        _saveFileIndex = saveFileIndex;
        _settings = new Dictionary<string, object>();
        _saves = new Dictionary<string, object>();
        _settingsImplementations = new List<Type>();
        _savesImplementations = new List<Type>();

        var settingsType = typeof(ISettings);
        var saveType = typeof(ISave);
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var assembly in assemblies)
        {
            var settingsImplementations = assembly.GetTypes()
                .Where(type =>
                    settingsType.IsAssignableFrom(type) && type is { IsInterface: false, IsAbstract: false });

            foreach (var implementation in settingsImplementations)
            {
                var instance = Activator.CreateInstance(implementation);
                if (instance is not ISettings)
                    continue;

                var type = instance.GetType();
                _settingsImplementations.Add(type);
                _settings.Add(type.Namespace.Split('.')[^1] + "." + type.Name, instance);
            }

            var saveImplementations = assembly.GetTypes()
                .Where(type =>
                    saveType.IsAssignableFrom(type) && type is { IsInterface: false, IsAbstract: false });

            foreach (var implementation in saveImplementations)
            {
                var instance = Activator.CreateInstance(implementation);
                if (instance is not ISave)
                    continue;

                var type = instance.GetType();
                _savesImplementations.Add(type);
                _saves.Add(type.Namespace.Split('.')[^1] + "." + type.Name, instance);
            }
        }
    }

    public void SetSaveFile(T? save)
        => _saveFileIndex = save;

    public G GetSetting<G>() where G : ISettings
        => (G)_settings[typeof(G).Namespace.Split('.')[^1] + "." + typeof(G).Name];

    public G GetSave<G>() where G : ISave
        => (G)_saves[typeof(G).Namespace.Split('.')[^1] + "." + typeof(G).Name];

    public void Save()
    {
        SaveSave();
        SaveSettings();
    }

    public void SaveSave()
    {
        if (_saveFileIndex is null)
            return;
        string filePath = $@"{_basePath}/{SaveFilePrefix}{_saveFileIndex}.{SaveFileType}";
        SaveFile(filePath, _saves);
    }

    public void SaveSettings()
    {
        string filePath = $@"{_basePath}/settings.{SettingsFileType}";
        SaveFile(filePath, _settings);
    }

    private static void SaveFile(string filePath, Dictionary<string, object> collection)
    {
        FileStream stream = null;
        if (!File.Exists(filePath))
            stream = File.Create(filePath);

        string file = Newtonsoft.Json.JsonConvert.SerializeObject(collection);
        using StreamWriter writer = stream is null ? new StreamWriter(filePath) : new StreamWriter(stream);
        writer.Write(file);
    }

    public bool Load()
    {
        bool successSetting, successSave;
        successSetting = LoadSettings();
        successSave = LoadSaves();
        return successSetting && successSave;
    }

    public bool LoadSaves()
    {
        if (_saveFileIndex is null)
            return false;
        string filePath = $@"{_basePath}/{SaveFilePrefix}{_saveFileIndex}.{SaveFileType}";

        return LoadFile(filePath, _saves, _savesImplementations);
    }

    public bool LoadSettings()
    {
        string filePath = $@"{_basePath}/settings.{SettingsFileType}";

        return LoadFile(filePath, _settings, _settingsImplementations);
    }

    private static bool LoadFile(string filePath, Dictionary<string, object> collection, List<Type> implementations)
    {
        if (!File.Exists(filePath))
            return false;

        string json = File.ReadAllText(filePath);
        JObject jsonObject = JObject.Parse(json);

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

        return true;
    }

    public bool DeleteSave()
    {
        if (_saveFileIndex is null)
            return false;
        string filePath = $@"{_basePath}/{SaveFilePrefix}{_saveFileIndex}.{SaveFileType}";
        return DeleteFile(filePath);
    }

    private static bool DeleteFile(string filePath)
    {
        if (!File.Exists(filePath))
            return true;

        try
        {
            File.Delete(filePath);
        }
        catch (Exception exception)
        {
            Log.Error(exception.Message);
            return false;
        }
        return true;
    }
}