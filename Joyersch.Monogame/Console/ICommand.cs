using Joyersch.Monogame.Ui;

namespace Joyersch.Monogame.Console;

public interface ICommand
{
    public IEnumerable<string> Execute(DevConsole console, object[] options, ContextProvider context);
}