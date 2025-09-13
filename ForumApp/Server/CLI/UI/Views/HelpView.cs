using System;
using CLI.UI.Core;

namespace CLI.UI.Views;

public class HelpView : IView
{
    const int LINE_WIDTH = 50;
    public void Render()
    {
        Utils.DrawBox(" Forum App ", LINE_WIDTH);

        Utils.DrawTable(new string[,]
        {
            { " ◆ Command ◆ ", " ◆ Description ◆ " },
            { "help", "Display this table again" },
            { "exit", "Exit the application" },
            { "feed", "View posts overview" }
        });
    }
}
