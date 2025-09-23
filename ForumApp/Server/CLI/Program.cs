using System.Text;
using CLI;
using CLI.UI.Core;
using Entities;
using FileRepositories;
using RepositoryContracts;

IRepository<User> userRepository = new FileRepository<User>("users");
IRepository<Comment> commentRepository = new FileRepository<Comment>("comments");
IRepository<Post> postRepository = new FileRepository<Post>("posts");

Console.OutputEncoding = Encoding.UTF8;

CommandExecutor executor = new()
{
    UserRepository = userRepository,
    CommentRepository = commentRepository,
    PostRepository = postRepository
};

CLIUtils.PrintInfo("Run the command 'start' to run the startup script for Windows command line.");

while (true)
{
    Console.Write("\n>>> ");
    await executor.ExecuteAsync(Console.ReadLine() ?? string.Empty);
}