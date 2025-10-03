using Joyersch.Monogame.Logging;

namespace Joyersch.Monogame;

public sealed class DynamicScaler
{
    List<IScaleable> _scaleables;

    private float _startingScale = 1;
    public DynamicScaler(Display display)
    {
        _scaleables = [];
        display.OnResize += Apply;
        _startingScale = display.Scale;
    }

    public void Apply(float scale)
    {
        foreach (var scaleable in _scaleables)
            scaleable.SetScale(scale / _startingScale);
    }

    public void Register(IScaleable scale)
        => _scaleables.Add(scale);
}