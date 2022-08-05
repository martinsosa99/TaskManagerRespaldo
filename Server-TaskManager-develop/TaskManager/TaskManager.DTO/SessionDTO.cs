using TaskManager.Domain;

namespace TaskManager.DTO;

public class SessionDTO
{

    public string token { get; set; }

    public int userId { get; set; }

    public SessionDTO()
    {
        this.token = "";
    }

    public static SessionDTO SessionToSessionDTO(Session session)
    {
        SessionDTO sessionDTO = new SessionDTO();

        sessionDTO.token = session.Token;
        sessionDTO.userId = session.User.Id;

        return sessionDTO;
    }

}