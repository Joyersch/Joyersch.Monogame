using Joyersch.Monogame.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Joyersch.Monogame.UI;

public sealed class ScaleDeviceHandler
{
    private readonly Scene _scene;
    private readonly GraphicsDeviceManager _graphicsDeviceManager;
    private readonly GraphicsAdapter _graphicsAdapter;

    public event Action ScaleChanged;

    public ScaleDeviceHandler(Scene scene, GraphicsDeviceManager graphicsDeviceManager, GraphicsAdapter graphicsAdapter)
    {
        _scene = scene;
        _graphicsDeviceManager = graphicsDeviceManager;
        _graphicsAdapter = graphicsAdapter;
    }

    public ScaleDeviceHandler ScaleToScreen(float scale = 1F)
    {
        _graphicsDeviceManager.PreferredBackBufferWidth = (int)(_graphicsAdapter.CurrentDisplayMode.Width * scale);
        _graphicsDeviceManager.PreferredBackBufferHeight = (int)(_graphicsAdapter.CurrentDisplayMode.Height * scale);
        _graphicsDeviceManager.ApplyChanges();
        ScaleChanged?.Invoke();
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

    public void ApplyResolution(Resolution resolution)
    {
        _graphicsDeviceManager.PreferredBackBufferWidth = resolution.Width;
        _graphicsDeviceManager.PreferredBackBufferHeight = resolution.Height;
        _graphicsDeviceManager.ApplyChanges();

        _scene.Display.Update();
        _scene.Camera.Calculate();

        ScaleChanged?.Invoke();
    }
}