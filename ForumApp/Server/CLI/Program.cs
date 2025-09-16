using System.Text;
using CLI;
using CLI.UI.Core;
using Entities;
using InMemoryRepositories;
using RepositoryContracts;

IRepository<User> userRepository = new InMemoryRepository<User>();
IRepository<Comment> commentRepository = new InMemoryRepository<Comment>();
IRepository<Post> postRepository = new InMemoryRepository<Post>();

Console.OutputEncoding = Encoding.UTF8;

CommandExecutor executor = new()
{
    UserRepository = userRepository,
    CommentRepository = commentRepository,
    PostRepository = postRepository
};

executor.ExecuteTokens(["help"]);
executor.ExecuteTokens(["light"]);
executor.ExecuteTokens(["max"]);
executor.ExecuteTokens(["scrolltop"]);

while (true)
{
    Console.Write("\n>>> ");
    await executor.ExecuteAsync(Console.ReadLine() ?? string.Empty);
}