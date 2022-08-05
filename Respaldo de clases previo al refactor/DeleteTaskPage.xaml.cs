using System.Text.Json;

namespace TaskManager;

public partial class DeleteTaskPage : ContentPage
{

    public UserSessionData UserSessionData { get; set; }

    HttpClient _client;
    JsonSerializerOptions _serializerOptions;

    public DeleteTaskPage(UserSessionData userSessionData)
    {
        InitializeComponent();

        this.UserSessionData = userSessionData;
    }

    private void OnDeleteTaskBtnClicked(object sender, EventArgs e)
    {
        this.DeleteTodoTask();
    }

    public async Task DeleteTodoTask()
    {
        _client = new HttpClient();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        int todoTaskId = 0;

        try
        {
            todoTaskId = Int32.Parse(TaskIdEntry.Text);

            Uri uri = new Uri(string.Format("http://localhost:5000/api/tasks/" + todoTaskId, string.Empty));

            HttpResponseMessage response = null;

            _client.DefaultRequestHeaders.Add("token", this.UserSessionData.Token);

            response = await _client.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                this.UserSessionData.Token = "";

                messageError.Text = "Task deleted successfully!";
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var responseDTO = JsonSerializer.Deserialize<ResponseDTO>(responseContent);

                messageError.Text = responseDTO.errorMessage;
            }

        }
        catch (Exception ex)
        {
            messageError.Text = ex.Message;
        }
    }

}