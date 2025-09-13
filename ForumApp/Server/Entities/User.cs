namespace Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public static User GetDummy()
    {
        var rnd = new Random();
        return new User
        {
            Id = rnd.Next(1, 1000),
            Name = new[] { "Line", "Bjorn", "Lars", "Jens", "Kari", "Mette", "Eduard" }[rnd.Next(0, 6)] + rnd.Next(1, 1000),
            Password = "password" + rnd.Next(100, 999)
        };
    }
    public override string ToString()
    {
        return $"[User: Id={Id}, Name={Name}, Password={Password}]";
    }
}
