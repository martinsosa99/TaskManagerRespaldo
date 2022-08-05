using System;
namespace TaskManager;

public class UserSessionData
{

    public string Token { get; set; }

    public int UserId { get; set; }


    public UserSessionData()
    {
        this.Token = "";

    }

}