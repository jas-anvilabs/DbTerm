using Gui = Terminal.Gui.App.Application;
using Terminal.Gui.App;
using Terminal.Gui.Input;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

namespace DbTerm.Cli;

public class DbTermTui(CommandProcessor processor)
{
    public void Run()
    {
        using IApplication app = Gui.Create().Init();
        var win = new Window { Title = "DbTerm" };

        var outputView = new TextView
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill() - 3,
            ReadOnly = true,
            Text = "DbTerm ready. Type \\? for help.\n"
        };

        var promptText = "DbTerm >> ";

        var promptLabel = new Label
        {
            Text = "DbTerm >> ",
            X = 0,
            Y = Pos.Bottom(outputView) + 1,
            Width = promptText.Length
        };

        var inputField = new TextField
        {
            X = promptText.Length,
            Y = Pos.Bottom(outputView) + 1,
            Width = Dim.Fill()
        };

        var statusBar = new StatusBar
        {
            X = 0,
            Y = Pos.Bottom(inputField),
            Width = Dim.Fill(),
            Text = "DbTerm v0.1.0"
        };

        inputField.KeyDown += (sender, key) =>
        {
            if (key == Key.Enter)
            {
                var input = inputField.Text ?? string.Empty;
                inputField.Text = string.Empty;

                var result = processor.Process(input);
                if (result.Output.Length > 0)
                {
                    outputView.Text += result.Output + "\n";
                }

                if (result.ShouldExit)
                {
                    app.RequestStop();
                }
            }
        };

        win.Add(outputView, promptLabel, inputField, statusBar);
        app.Run(win);
    }
}