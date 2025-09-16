using System.Text;
using CLI;
using CLI.UI.Core;
using InMemoryRepositories;
using RepositoryContracts;

IUserRepository userRepository = new UserInMemoryRepository();
ICommentRepository commentRepository = new CommentInMemoryRepository();
IPostRepository postRepository = new PostInMemoryRepository();

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