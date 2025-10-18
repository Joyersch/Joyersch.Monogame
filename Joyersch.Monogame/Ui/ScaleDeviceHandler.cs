using Joyersch.Monogame.Logging;
using Joyersch.Monogame.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Joyersch.Monogame.UI;

public sealed class ScaleDeviceHandler
{
    private readonly Scene _scene;
    private readonly GraphicsDeviceManager _graphicsDeviceManager;
    private readonly GraphicsDevice _graphicsDevice;
    private readonly GraphicsAdapter _graphicsAdapter;

    public event Action ScaleChanged;

    public ScaleDeviceHandler(Scene scene, GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicsDevice, GraphicsAdapter graphicsAdapter)
    {
        _scene = scene;
        _graphicsDeviceManager = graphicsDeviceManager;
        _graphicsDevice = graphicsDevice;
        _graphicsAdapter = graphicsAdapter;
        Log.Information("Before applying settings");
        Log.Warning("Adapter: " +_graphicsAdapter.CurrentDisplayMode.Width + "x" + _graphicsAdapter.CurrentDisplayMode.Height);
        Log.Warning("Scene scale: " +scene.Display.Scale.ToString());
        Log.Warning("DeviceManager: "+_graphicsDeviceManager.PreferredBackBufferWidth + "x" + _graphicsDeviceManager.PreferredBackBufferHeight);
        Log.Information("----------------------------");
        Log.Information("AllowedModes");
        foreach (var mode in _graphicsAdapter.SupportedDisplayModes)
        {
            if (mode.AspectRatio == 16f/9f)
            Log.Information($"{mode.Width}x{mode.Height}");
        }
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
        if (!_graphicsDeviceManager.IsFullScreen)
            _graphicsDeviceManager.ToggleFullScreen();
        //Log.Information("After applying settings");
        //Log.Warning("Adapter: " +_graphicsAdapter.CurrentDisplayMode.Width + "x" + _graphicsAdapter.CurrentDisplayMode.Height);
        //Log.Warning("Scene scale: " +_scene.Display.Scale.ToString());
        //Log.Warning("DeviceManager: "+_graphicsDeviceManager.PreferredBackBufferWidth + "x" + _graphicsDeviceManager.PreferredBackBufferHeight);
        //Log.Information("----------------------------");
        return this;
    }

    public ScaleDeviceHandler Windowed()
    {
       if (_graphicsDeviceManager.IsFullScreen)
           _graphicsDeviceManager.ToggleFullScreen();
       //Log.Information("After applying settings");
        // Log.Warning("Adapter: " +_graphicsAdapter.CurrentDisplayMode.Width + "x" + _graphicsAdapter.CurrentDisplayMode.Height);
        //Log.Warning("Scene scale: " +_scene.Display.Scale.ToString());
        //Log.Warning("DeviceManager: "+_graphicsDeviceManager.PreferredBackBufferWidth + "x" + _graphicsDeviceManager.PreferredBackBufferHeight);
        // Log.Information("----------------------------");
        return this;
    }

    public void ApplyResolution(Resolution resolution)
    {
        _graphicsDeviceManager.PreferredBackBufferWidth = resolution.Width;
        _graphicsDeviceManager.PreferredBackBufferHeight = resolution.Height;
        _graphicsDeviceManager.ApplyChanges();

        _scene.Display.Update();
        //_scene.Camera.Calculate();
        
        // Log.Information("After applying settings");
        //Log.Warning("Adapter: " +_graphicsAdapter.CurrentDisplayMode.Width + "x" + _graphicsAdapter.CurrentDisplayMode.Height);
        //Log.Warning("Scene scale: " +_scene.Display.Scale.ToString());
        //Log.Warning("DeviceManager: "+_graphicsDeviceManager.PreferredBackBufferWidth + "x" + _graphicsDeviceManager.PreferredBackBufferHeight);
        //Log.Information("----------------------------");

        ScaleChanged?.Invoke();
    }
}