using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Joyersch.Monogame;

/// <summary>
/// NOTE: Experimental! Use at your own risk!
/// Executes a given Func(bool) repeated for as long as FrameLimit (ms) per frame or Func(bool) returns true
/// </summary>
public sealed class LazyUpdater
{
    private Func<bool> _loadingTasks;

    public float FrameLimit { get; set; } = 6F;
    private Stopwatch _stopwatch = new Stopwatch();
    private bool _success;

    public void Update(GameTime gameTime)
    {
        _stopwatch.Restart();

        while (!_success && _stopwatch.ElapsedMilliseconds < FrameLimit)
        {
            _success = _loadingTasks.Invoke();
        }

        _stopwatch.Stop();
    }

    public void SetFunc(Func<bool> func)
    {
        _loadingTasks = func;
        _success = false;
    }
}