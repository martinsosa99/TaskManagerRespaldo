using System.Text.Json;

namespace TaskManager;

public partial class MainPage : ContentPage
{

    HttpClient _client;
    JsonSerializerOptions _serializerOptions;

    public UserSessionData UserSessionData { get; set; }

    public MainPage()
    {
        InitializeComponent();

        _client = new HttpClient();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        this.UserSessionData = new UserSessionData();
    }

    private void OnLoginBtnClicked(object sender, EventArgs e)
    {
        //Navigation.PushAsync(new LoginPage("hello"));

         this.Login();
    }

    private void OnCreateUserBtnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CreateUserPage(this.UserSessionData));
    }


    public async Task Login()
    {
        string AnEmailAddress = emailAddressEntry.Text;
        string Apasword = passwordEntry.Text;

        Uri uri = new Uri(string.Format("http://localhost:5000/api/login?email=" + AnEmailAddress + "&password=" + Apasword, string.Empty));

        try
        {
             HttpResponseMessage response = null;

             response = await _client.PostAsync(uri, null);

            if (response.IsSuccessStatusCode)
            { 
                var responseContent = await response.Content.ReadAsStringAsync();

                var sessionDTO = JsonSerializer.Deserialize<SessionDTO>(responseContent);

                this.UserSessionData.Token = sessionDTO.token;
                this.UserSessionData.UserId = sessionDTO.userId;

                App.Current.MainPage = new NavigationPage(new MenuPage(this.UserSessionData));
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var responseDTO = JsonSerializer.Deserialize<ResponseDTO>(responseContent);

                messageErrorLabel.Text = responseDTO.errorMessage;
            }

        }
        catch (Exception ex)
        {
            messageErrorLabel.Text = ex.Message;
        }
    }

}