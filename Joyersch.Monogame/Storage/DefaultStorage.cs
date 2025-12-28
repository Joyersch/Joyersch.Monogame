namespace Joyersch.Monogame.Storage;

public class DefaultStorage
{
    public static string GetStorage(string gameName)
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
            $"/Joyersch/{gameName}/";
    }

    public static void CreateStorage(string gameName)
    {
        string directory = GetStorage(gameName);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
    }
}