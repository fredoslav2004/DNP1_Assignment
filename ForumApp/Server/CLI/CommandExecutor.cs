using System;
using CLI.UI.Core;
using RepositoryContracts;

namespace CLI;

public class CommandExecutor
{
    public required IUserRepository UserRepository { private get; init; }
    public required ICommentRepository CommentRepository { private get; init; }
    public required IPostRepository PostRepository { private get; init; }

    private IView currentView = new CLI.UI.Views.HelpView();

    public void Execute(string text)
    {
        var tokens = Tokenize(text);
        ExecuteTokens(tokens);
    }
    private string[] Tokenize(string text)
    {
        return text.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
    }
    public void ExecuteTokens(string[] tokens)
    {
        Action postRender = delegate { };

        if (tokens.Length == 0)
        {
            postRender += () => Utils.PrintError($"You must enter a command. Type 'help' to see available commands.");
            return;
        }
        else
        {
            var command = tokens[0].ToLower();
            var args = tokens[1..];

            switch (command)
            {
                case "help":
                    currentView = new CLI.UI.Views.HelpView();
                    postRender += () => Utils.PrintInfo("Help table displayed.");
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                default:
                    postRender += () => Utils.PrintError($"Unknown command '{command}'. Type 'help' to see available commands.");
                    break;
            }
        }

        Console.Clear();
        currentView.Render();
        postRender.Invoke();
    }
}
