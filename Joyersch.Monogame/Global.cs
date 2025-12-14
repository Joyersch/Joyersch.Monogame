using Joyersch.Monogame.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Joyersch.Monogame;

public static class Global
{
    public static void Initialize(ContentManager content)
    {
        // To be filled!
    }

    public static string ReadFromResources(string file)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        string @return = null;
        foreach (var assembly in assemblies)
        {
            try
            {
                var files = assembly.GetManifestResourceNames();
                if (!files.Contains(file))
                    continue;
                using Stream stream = assembly.GetManifestResourceStream(file);
                using StreamReader reader = new StreamReader(stream);
                @return = reader.ReadToEnd();
            }
            catch(Exception exception)
            {
                Log.Error(exception.Message);
            }

            if (@return is not null)
                break;
        }

        return @return ?? string.Empty;
    }

    private static Color? _color;

    public static Color Color => _color ??= new Color(161, 0, 255); // #a100ff
}