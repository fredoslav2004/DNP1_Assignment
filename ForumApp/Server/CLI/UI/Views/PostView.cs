using System;
using CLI.UI.Core;
using RepositoryContracts;

namespace CLI.UI.Views;

public class PostView : IView
{
    public required IPostRepository PostRepository { get; set; }
    public required ICommentRepository CommentRepository { get; set; }
    public required IUserRepository UserRepository { get; set; }
    public required int PostId { get; init; }

    public async Task RenderAsync()
    {
        var post = PostRepository.GetSingleAsync(PostId);
        var comments = CommentRepository.GetManyAsync().Where(c => c.PostId == PostId);

        await post;
        Entities.User author = new() { Name = "???", Id = -1, Password = "???" };
        try
        {
            author = await UserRepository.GetSingleAsync(post.Result.AuthorId);
        }
        catch (Exception)
        {
            Utils.PrintError($"Author of this post not found.");
        }

        Utils.DrawBox($"{post.Result.Id} - {post.Result.Title}", 100);
        Console.WriteLine($"Written by: {author.Name}");
        Console.WriteLine($"Content: {post.Result.Content}");
        Utils.PrintRepeatChar('─', 100);
        Console.WriteLine("Comments:");
        // await comments;
        string[,] table = new string[comments.Count() + 1, 2];
        table[0, 0] = " ◆ Author ◆ ";
        table[0, 1] = " ◆ Comment ◆ ";
        int i = 1;
        foreach (var comment in comments)
        {
            Entities.User commentAuthor = new() { Name = "???", Id = -1, Password = "???" };
            try
            {
                commentAuthor = await UserRepository.GetSingleAsync(comment.AuthorId);
            }
            catch (Exception) { }
            table[i, 0] = commentAuthor.Name;
            table[i, 1] = comment.Content;
            i++;
        }
        Utils.DrawTable(table);
        return;
    }
}
