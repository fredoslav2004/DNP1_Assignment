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

while (true)
{
    Console.Write("\n>>> ");
    executor.Execute(Console.ReadLine() ?? string.Empty);
}