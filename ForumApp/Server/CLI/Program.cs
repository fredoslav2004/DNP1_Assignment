using System.Text;
using CLI;
using CLI.UI.Core;
using InMemoryRepositories;
using RepositoryContracts;

IUserRepository userRepository = new UserInMemoryRepository();
ICommentRepository commentRepository = new CommentInMemoryRepository();
IPostRepository postRepository = new PostInMemoryRepository();

await userRepository.AddAsync(Entities.User.GetDummy());
await userRepository.AddAsync(Entities.User.GetDummy());
await userRepository.AddAsync(Entities.User.GetDummy());
await userRepository.AddAsync(Entities.User.GetDummy());
await userRepository.AddAsync(Entities.User.GetDummy());
await commentRepository.AddAsync(Entities.Comment.GetDummy());
await commentRepository.AddAsync(Entities.Comment.GetDummy());
await commentRepository.AddAsync(Entities.Comment.GetDummy());
await commentRepository.AddAsync(Entities.Comment.GetDummy());
await commentRepository.AddAsync(Entities.Comment.GetDummy());
await postRepository.AddAsync(Entities.Post.GetDummy());
await postRepository.AddAsync(Entities.Post.GetDummy());
await postRepository.AddAsync(Entities.Post.GetDummy());
await postRepository.AddAsync(Entities.Post.GetDummy());
await postRepository.AddAsync(Entities.Post.GetDummy());

Console.OutputEncoding = Encoding.UTF8;

CommandExecutor executor = new()
{
    UserRepository = userRepository,
    CommentRepository = commentRepository,
    PostRepository = postRepository
};

await executor.ExecuteTokens(["help"]);

while (true)
{
    Console.Write("\n>>> ");
    await executor.ExecuteAsync(Console.ReadLine() ?? string.Empty);
}