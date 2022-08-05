using System;

namespace TaskManager;

public class SessionDTO
{
    public string token { get; set; }

    public int userId { get; set; }

    public SessionDTO()
    {
        this.token = "";
    }

}