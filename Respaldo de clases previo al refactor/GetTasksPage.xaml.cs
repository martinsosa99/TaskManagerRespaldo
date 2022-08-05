using System.Collections.ObjectModel;
using System.Text.Json;

namespace TaskManager;

public partial class GetTasksPage : ContentPage
{

    HttpClient _client;
    JsonSerializerOptions _serializerOptions;

    public UserSessionData UserSessionData { get; set; }

    public List<TodoTaskDTO> TodoTasksDTO { get; set; }

    public GetTasksPage(UserSessionData userSessionData)
    {
        this.TodoTasksDTO = new List<TodoTaskDTO>();
        this.UserSessionData = userSessionData;
        InitializeComponent();

        this.GetTasks();
    }

    public async Task GetTasks()
    {
        try
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            Uri uri = new Uri(string.Format("http://localhost:5000/api/tasks", string.Empty));

            HttpResponseMessage response = null;

            _client.DefaultRequestHeaders.Add("token", this.UserSessionData.Token);

            response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                this.TodoTasksDTO = JsonSerializer.Deserialize<List<TodoTaskDTO>>(content, _serializerOptions);
                TasksListView.ItemsSource = TodoTasksDTO;
            }

        }
        catch (Exception ex)
        {
        }

    }

}