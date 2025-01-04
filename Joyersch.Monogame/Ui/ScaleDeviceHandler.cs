using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Joyersch.Monogame.UI;

public sealed class ScaleDeviceHandler
{
    private readonly GraphicsDeviceManager _graphicsDeviceManager;
    private readonly GraphicsAdapter _graphicsAdapter;

    public ScaleDeviceHandler(GraphicsDeviceManager graphicsDeviceManager, GraphicsAdapter graphicsAdapter)
    {
        _graphicsDeviceManager = graphicsDeviceManager;
        _graphicsAdapter = graphicsAdapter;
    }

    public ScaleDeviceHandler ScaleToScreen(float scale = 1F)
    {
        _graphicsDeviceManager.PreferredBackBufferWidth = (int)(_graphicsAdapter.CurrentDisplayMode.Width * scale);
        _graphicsDeviceManager.PreferredBackBufferHeight = (int)(_graphicsAdapter.CurrentDisplayMode.Height * scale);
        _graphicsDeviceManager.ApplyChanges();
        return this;
    }

    public ScaleDeviceHandler Fullscreen()
    {
        _graphicsDeviceManager.IsFullScreen = true;
        _graphicsDeviceManager.ApplyChanges();
        return this;
    }

    public ScaleDeviceHandler Windowed()
    {
        _graphicsDeviceManager.IsFullScreen = false;
        _graphicsDeviceManager.ApplyChanges();
        return this;
    }

}