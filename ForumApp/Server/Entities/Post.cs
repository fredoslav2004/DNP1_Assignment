namespace Entities;

public class Post : IIdentifiable
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public int AuthorId { get; set; }
    public static Post GetDummy()
    {
        var rnd = new Random();
        return new Post
        {
            Id = rnd.Next(1, 1000),
            Title = new[] { "Hello World", "My First Post", "I love C# <3 #love #intimate" }[rnd.Next(0, 3)],
            Content = "This is a sample post content with random number " + rnd.Next(100, 999),
            AuthorId = rnd.Next(1, 100)
        };
    }    
    public override string ToString()
    {
        return $"[Post: Id={Id}, Title={Title}, Content={Content}, AuthorId={AuthorId}]";
    }
}