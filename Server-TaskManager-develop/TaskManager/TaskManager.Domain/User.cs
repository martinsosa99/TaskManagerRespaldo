namespace TaskManager.Domain;

public class User
{

    public int Id { get; set; }

    public string EmailAddress { get; set; }

    public string Password { get; set; }

    public User()
    {
        this.EmailAddress = "";
        this.Password = "";
    }

    public override bool Equals(Object obj)
    {
        bool equals = false;

        if (obj == null)
        {
            equals = false;
        }
        else if (!(obj is User))
        {
            equals = false;
        }
        else
        {
            User secondUser = ((User)obj);
            equals = this.EmailAddress == secondUser.EmailAddress;
        }

        return equals;
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }

}
