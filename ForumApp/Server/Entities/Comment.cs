namespace Entities;

public class Comment : IIdentifiable
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public int PostId { get; set; }
    public required string Content { get; set; }
    public static Comment GetDummy()
    {
        var rnd = new Random();
        return new Comment
        {
            Id = rnd.Next(1, 1000),
            AuthorId = rnd.Next(1, 100),
            PostId = rnd.Next(1, 100),
            Content = new[] { "Pure shit", "Not worth my time", "KYS", "I hate this" }[rnd.Next(0, 4)] + " " + rnd.Next(100, 999)
        };
    }
    public override string ToString()
    {
        return $"[Comment Id={Id}, AuthorId={AuthorId}, PostId={PostId}, Content='{Content}']";
    }    
}