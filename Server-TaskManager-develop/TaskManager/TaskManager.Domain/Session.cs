namespace TaskManager.Domain;

public class Session
{
    public int Id { get; set; }

    public User User { get; set; }

    public string Token { get; set; }

    public Session()
    {

    }

}

