
using System.Text.Json;

namespace TaskManager;

public partial class MenuPage : ContentPage
{

    public UserSessionData UserSessionData { get; set; }

    HttpClient _client;
    JsonSerializerOptions _serializerOptions;

    public MenuPage(UserSessionData userSessionData)
    {
        InitializeComponent();
        this.UserSessionData = userSessionData;

        _client = new HttpClient();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    private void OnAddTaskBtnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddTaskPage(this.UserSessionData));
        //Navigation.PopAsync();
        //App.Current.MainPage = new NavigationPage(this);
    }

    private void OnUpdateTaskBtnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new UpdateTaskPage(this.UserSessionData));
    }

    private void OnDeleteTaskBtnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new DeleteTaskPage(this.UserSessionData));
    }

    private void OnViewTasksBtnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new GetTasksPage(this.UserSessionData));
    }

    private void OnLogoutBtnClicked(object sender, EventArgs e)
    {
        this.Logout();
    }

    public async Task Logout()
    {
        Uri uri = new Uri(string.Format("http://localhost:5000/api/logout", string.Empty));

        try
        {
            HttpResponseMessage response = null;

            _client.DefaultRequestHeaders.Add("token", this.UserSessionData.Token);

            response = await _client.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                this.UserSessionData.Token = "";

                App.Current.MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                
            }

        }
        catch (Exception ex)
        {
           
        }

    }

}