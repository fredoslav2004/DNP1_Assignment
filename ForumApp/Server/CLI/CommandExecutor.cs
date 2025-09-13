using System;
using System.Threading.Tasks;
using CLI.UI.Core;
using RepositoryContracts;

namespace CLI;

public class CommandExecutor
{
    public required IUserRepository UserRepository { private get; init; }
    public required ICommentRepository CommentRepository { private get; init; }
    public required IPostRepository PostRepository { private get; init; }

    private IView currentView = new CLI.UI.Views.HelpView();

    public async Task ExecuteAsync(string text)
    {
        var tokens = Tokenize(text);
        await ExecuteTokens(tokens);
    }
    private string[] Tokenize(string text)
    {
        return text.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
    }
    public async Task ExecuteTokens(string[] tokens)
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
                case "users":
                    if (args.Length == 0)
                    {
                        currentView = new CLI.UI.Views.UserListView() { UserRepository = UserRepository };
                        postRender += () => Utils.PrintInfo("User list displayed.");
                    }
                    else
                    {
                        switch (args[0].ToLower())
                        {
                            case "add":
                                var _newUser = await UserRepository.AddAsync(new Entities.User
                                {
                                    Name = args.Length > 1 ? args[1] : "NewUser",
                                    Password = args.Length > 2 ? args[2] : "NewPassword"
                                });
                                postRender += () => Utils.PrintInfo($"New user added. {_newUser}");
                                break;
                            case "rm":
                                if (args.Length > 1 && int.TryParse(args[1], out var userId))
                                {
                                    try
                                    {
                                        await UserRepository.DeleteAsync(userId);
                                        postRender += () => Utils.PrintInfo($"User with ID {userId} removed.");
                                    }
                                    catch (Exception ex)
                                    {
                                        postRender += () => Utils.PrintError($"Error removing user with ID {userId}: {ex.Message}");
                                    }
                                }
                                else
                                {
                                    postRender += () => Utils.PrintError($"You must provide a valid user ID to remove. Example: 'users rm 3'");
                                }
                                break;
                            default:
                                postRender += () => Utils.PrintError($"Unknown argument '{args[0]}' for command 'users'. Type 'help' to see available commands.");
                                break;
                        }
                    }
                    break;
                case "light":
                    // Switches to light theme
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case "dark":
                    // Switches to dark theme
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "feed":
                    currentView = new CLI.UI.Views.FeedView()
                    {
                        PostRepository = PostRepository,
                        UserRepository = UserRepository,
                        CommentRepository = CommentRepository
                    };
                    postRender += () => Utils.PrintInfo("Post feed displayed.");
                    break;
                case "posts":
                    if (args.Length == 0)
                    {
                        currentView = new CLI.UI.Views.FeedView()
                        {
                            PostRepository = PostRepository,
                            UserRepository = UserRepository,
                            CommentRepository = CommentRepository
                        };
                        postRender += () => Utils.PrintInfo("Post list displayed.");
                    }
                    else
                    {
                        switch (args[0].ToLower())
                        {
                            case "add":
                                var _newPost = await PostRepository.AddAsync(new Entities.Post
                                {
                                    Title = args.Length > 1 ? args[1] : "New Post",
                                    Content = args.Length > 2 ? args[2] : "This is a new post.",
                                    AuthorId = args.Length > 3 && int.TryParse(args[3], out var uid) ? uid : 1
                                });
                                postRender += () => Utils.PrintInfo($"New post added. {_newPost}");
                                break;
                            case "rm":
                                if (args.Length > 1 && int.TryParse(args[1], out var postId))
                                {
                                    try
                                    {
                                        await PostRepository.DeleteAsync(postId);
                                        postRender += () => Utils.PrintInfo($"Post with ID {postId} removed.");
                                    }
                                    catch (Exception ex)
                                    {
                                        postRender += () => Utils.PrintError($"Error removing post with ID {postId}: {ex.Message}");
                                    }
                                }
                                else
                                {
                                    postRender += () => Utils.PrintError($"You must provide a valid post ID to remove. Example: 'posts rm 5'");
                                }
                                break;
                            default:
                                postRender += () => Utils.PrintError($"Unknown argument '{args[0]}' for command 'posts'. Type 'help' to see available commands.");
                                break;
                        }
                    }
                    break;
                case "max":
#pragma warning disable CA1416 // Validate platform compatibility
                    Utils.MaximizeWindow();
#pragma warning restore CA1416 // Validate platform compatibility
                    break;
                case "min":
#pragma warning disable CA1416 // Validate platform compatibility
                    Console.SetWindowSize(400, 400);
#pragma warning restore CA1416 // Validate platform compatibility
                    break;
                case "dummy":
                    await UserRepository.AddAsync(Entities.User.GetDummy());
                    await UserRepository.AddAsync(Entities.User.GetDummy());
                    await UserRepository.AddAsync(Entities.User.GetDummy());
                    await UserRepository.AddAsync(Entities.User.GetDummy());
                    await UserRepository.AddAsync(Entities.User.GetDummy());
                    await CommentRepository.AddAsync(Entities.Comment.GetDummy());
                    await CommentRepository.AddAsync(Entities.Comment.GetDummy());
                    await CommentRepository.AddAsync(Entities.Comment.GetDummy());
                    await CommentRepository.AddAsync(Entities.Comment.GetDummy());
                    await CommentRepository.AddAsync(Entities.Comment.GetDummy());
                    await PostRepository.AddAsync(Entities.Post.GetDummy());
                    await PostRepository.AddAsync(Entities.Post.GetDummy());
                    await PostRepository.AddAsync(Entities.Post.GetDummy());
                    await PostRepository.AddAsync(Entities.Post.GetDummy());
                    await PostRepository.AddAsync(Entities.Post.GetDummy());
                    postRender += () => Utils.PrintInfo("5 dummy users, posts, and comments added.");
                    break;
                case "reset":
                    if(args.Length > 0 && args[0].ToLower() == "db")
                    {
                        await UserRepository.ClearAsync();
                        await PostRepository.ClearAsync();
                        await CommentRepository.ClearAsync();
                        postRender += () => Utils.PrintInfo("Database reset: all users, posts, and comments removed.");
                    }
                    else
                    {
                        postRender += () => Utils.PrintError($"Unknown argument '{args[0]}' for command 'reset'. Did you mean 'reset db'? Type 'help' to see available commands.");
                    }
                    break;
                default:
                    postRender += () => Utils.PrintError($"Unknown command '{command}'. Type 'help' to see available commands.");
                    break;
            }
        }

        Console.Clear();
        await currentView.RenderAsync();
        postRender.Invoke();
    }
}
