
using System.Text;
using System.Text.Json;

namespace TaskManager;

public partial class CreateUserPage : ContentPage
{

    HttpClient _client;
    JsonSerializerOptions _serializerOptions;

    public CreateUserPage(UserSessionData userSessionData)
    {
        InitializeComponent();
    }

    private void OnCreateUserBtnClicked(object sender, EventArgs e)
    {
        this.CreateUser();
    }

    public async Task CreateUser()
    {
        _client = new HttpClient();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        UserDTO userDTO = new UserDTO();
        userDTO.id = 0;
        userDTO.emailAddress = emailAddressEntry.Text;
        userDTO.password = passwordEntry.Text;

        Uri uri = new Uri(string.Format("http://localhost:5000/api/users", string.Empty));

        try
        {
            string json = JsonSerializer.Serialize<UserDTO>(userDTO, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            response = await _client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                emailAddressEntry.Text = "";
                passwordEntry.Text = "";
                messageLabel.Text = "User created succesfully!";
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var responseDTO = JsonSerializer.Deserialize<ResponseDTO>(responseContent);

                messageLabel.Text = responseDTO.errorMessage;
            }

        }
        catch (Exception ex)
        {
            messageLabel.Text = ex.Message;
        }

    }

}