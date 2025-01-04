using Joyersch.Monogame.Console;
using Joyersch.Monogame.Logging;
using Joyersch.Monogame.Ui;
using Joyersch.Monogame.Ui.Buttons;
using Joyersch.Monogame.Ui.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Joyersch.Monogame;

public static class Global
{
    public static readonly CommandProcessor CommandProcessor = new();

    public static void Initialize(ContentManager content)
    {
        SampleObject.Texture = content.GetTexture("placeholder");
        Cursor.Texture = content.GetTexture("cursor");

        DefaultLetters.Texture = content.GetTexture("Font/DefaultLetters");
        ActionSymbols.Texture = content.GetTexture("Font/ActionSymbols");
        ButtonAddonIcons.Texture = content.GetTexture("Font/ButtonAddons");
        SampleButton.Texture = content.GetTexture("Button/empty");
        SquareButton.Texture = content.GetTexture("Button/square");
        RatioBox.Texture = content.GetTexture("Button/ratio");
        Blank.Texture = content.GetTexture("Dot");

        Letter.Initialize();
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