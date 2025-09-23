using System;
using CLI.UI.Core;
using Entities;
using RepositoryContracts;
using CLIUtils = CLI.UI.Core.CLIUtils;

namespace CLI.UI.Views;

public class PostCreationView : IView
{
    public required IRepository<User> UserRepository { get; init; }
    public required IRepository<Post> PostRepository { get; init; }
    private bool isCreating = true;
    private string lastTitle = "";
    private string lastContent = "";
    private int lastAuthorId = -1;
    public async Task RenderAsync()
    {
        if (isCreating)
        {
            await CreatePost();
            isCreating = false;
        }
        else
        {
            ShowLastCreation();
        }
        return;
    }

    private async Task CreatePost()
    {
        CLIUtils.DrawBox(" Create New Post ", 100);
        Console.Write("Title: ");
        var title = Console.ReadLine() ?? "???";
        Console.Write("Content: ");
        var content = Console.ReadLine() ?? "???";
        Console.Write("Author ID: ");
        var userIdInput = Console.ReadLine() ?? "-1";

        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content) || string.IsNullOrWhiteSpace(userIdInput) || !int.TryParse(userIdInput, out int userId))
        {
            CLIUtils.PrintError("Invalid input. Please ensure all fields are filled correctly.");
            return;
        }

        lastTitle = title;
        lastContent = content;
        lastAuthorId = userId;

        try
        {
            var post = await PostRepository.AddAsync(new Post
            {
                Title = title,
                Content = content,
                AuthorId = userId
            });
            CLIUtils.PrintInfo("Post created successfully.");
        }
        catch (ArgumentException ex)
        {
            CLIUtils.PrintError(ex.Message);
            return;
        }
        catch (Exception)
        {
            CLIUtils.PrintError("An error occurred while creating the post. Please try again.");
            return;
        }
    }

    private void ShowLastCreation()
    {
        CLIUtils.DrawBox(" Last Created Post ", 100);
        Console.WriteLine($"Title: {lastTitle}");
        Console.WriteLine($"Content: {lastContent}");
        Console.WriteLine($"Author ID: {lastAuthorId}");
        return;
    }
}
