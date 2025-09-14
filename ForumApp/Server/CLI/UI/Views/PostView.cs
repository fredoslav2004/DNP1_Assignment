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
        var author = UserRepository.GetSingleAsync(post.Result.Id);

        Utils.DrawBox($"{post.Result.Id} - {post.Result.Title}", 100);
        await author;
        Console.WriteLine($"Written by: {author.Result.Name}");
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
            var commentAuthor = UserRepository.GetSingleAsync(comment.AuthorId);
            await commentAuthor;
            table[i, 0] = commentAuthor.Result.Name;
            table[i, 1] = comment.Content;
            i++;
        }
        Utils.DrawTable(table);
        return;
    }
}
