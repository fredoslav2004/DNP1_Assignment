using System;
using CLI.UI.Core;
using Entities;
using RepositoryContracts;

namespace CLI.UI.Views;

public class FeedView : IView
{
    public required IRepository<Post> PostRepository { private get; init; }
    public required IRepository<User> UserRepository { private get; init; }
    public required IRepository<Comment> CommentRepository { private get; init; }

    public async Task RenderAsync()
    {
        var posts = PostRepository.GetMany();
        var tableOfPosts = new string[posts.Count()+1, 4];
        int i = 1; // I didn't want to deal with IQueryable, whoops
        foreach (var post in posts)
        {
            User? user = null;
            try
            {
                user = await UserRepository.GetSingleAsync(post.AuthorId);
            }
            catch (Exception)
            {
                user = new User { Name = "???" };
            }
            var comments = CommentRepository.GetMany().Where(c => c.PostId == post.Id).ToList();
            // Note: I'm a tiny bit confused about comments not really being async despite the code being taken from the assignment

            tableOfPosts[i, 0] = post.Id.ToString();
            tableOfPosts[i, 1] = post.Title;
            tableOfPosts[i, 2] = user?.Name ?? "Unknown";
            tableOfPosts[i, 3] = comments.Count.ToString();
            i++;
        }

        tableOfPosts[0, 0] = " ◆ Id ◆ ";
        tableOfPosts[0, 1] = " ◆ Title ◆ ";
        tableOfPosts[0, 2] = " ◆ Author ◆ ";
        tableOfPosts[0, 3] = " ◆ Comments ◆ ";
        CLIUtils.DrawTable(tableOfPosts);
    }
}
