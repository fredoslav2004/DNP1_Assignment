using System;
using CLI.UI.Core;

namespace CLI.UI.Views;

public class HelpView : IView
{
    const int LINE_WIDTH = 50;
    public Task RenderAsync()
    {
        Utils.DrawBox(" Forum App ", LINE_WIDTH);

        Utils.DrawTable(new string[,]
        {
            { " ◆ Command ◆ ", " ◆ Description ◆ " },
            { "help", "Display this table again" },
            { "exit", "Exit the application" },
            { "feed", "View posts overview" },
            { "users", "View users overview. Args: [add] <name> <password> | [rm] <userId>" },
            { "posts", "View posts. Args: [add] <title> <body> <userId> | [rm] <postId>" },
            { "dummy", "Add 5 dummy users, posts, and comments" },
            { "max", "Maximize the console window" },
            { "min", "Minimize the console window (just make smaller)" },
            { "reset", "Use with argument db to reset the database" },
            { "write", "Arg [post] - opens a post writing menu. Arg [comment] requires another arg <postId> <userId> <body>." }
        });

        return Task.CompletedTask;
    }
}
