using TaskManager.Domain;

namespace TaskManager.DTO;

public class UserDTO
{

    public int id { get; set; }

    public string emailAddress { get; set; }

    public string password { get; set; }

    public UserDTO()
    {
    }

    public static User UserDTOToUser(UserDTO userDTO)
    {
        User user = new User();

        user.Id = userDTO.id;
        user.EmailAddress = userDTO.emailAddress;
        user.Password = userDTO.password;

        return user;
    }

    public static UserDTO UserToUserDTO(User user)
    {
        UserDTO userDTO = new UserDTO();

        userDTO.id = user.Id;
        userDTO.emailAddress = user.EmailAddress;
        userDTO.password = "";

        for (int index = 0; index < user.Password.Length; index++)
            userDTO.password += "*";

        return userDTO;
    }

}