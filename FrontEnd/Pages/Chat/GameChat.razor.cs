using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;

namespace FrontEnd.Pages.Chat
{
    public partial class GameChat
    {
        // flag to indicate chat status
        private bool _isChatting = false;

        // name of the user who will be chatting
        private string _username = "";

        // on-screen message
        private string _message;

        // new message input
        private string _newMessage;

        // list of messages in chat
        private List<Message> _messages = new List<Message>();
        private List<string> _users = new List<string>();
        private HubConnection _hubConnection;

        public async Task Chat()
        {
            try
            {
                //Ny metod för lättare läst kod.
                // Start chatting and force refresh UI.
                if (_username == "") throw new Exception("Behöver ett användarnamn");
                //_username = _login.GetUserName();
                _isChatting = true;
                
                await Task.Delay(1);

                // remove old messages if any
                // remove old users
                _messages.Clear();
                _users.Clear();
                _users.Add(_username);

                //-- Ny metod för lättare läst kod.
                // Create the chat client
                string baseUrl = navigationManager.BaseUri;

                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(navigationManager.ToAbsoluteUri("/GameHub"))
                    .Build();

                _hubConnection.On<string, string>("Broadcast", BroadcastMessage);
                _hubConnection.On("Refresh", UpdateClient);
                _hubConnection.On("AnswerCall", PingServer);
                _hubConnection.On<string>("RemoveUser", RemoveClient);

                await _hubConnection.StartAsync();
                await SendAsync($"[Notice] {_username} joined chat room.");
                await _hubConnection.SendAsync("SendMood", _username, "Happy");
            }
            catch (Exception e)
            {
                _message = $"ERROR: Failed to start chat client: {e.Message}";
                _isChatting = false;
            }
        }

        private void BroadcastMessage(string name, string message)
        {
            bool isMine = name.Equals(_username, StringComparison.OrdinalIgnoreCase);
            _messages.Add(new Message(name, message, isMine));
            if (_users.Contains(name) == false && name != "System")
                _users.Add(name);
            // Inform blazor the UI needs updating
            StateHasChanged();
            ScrollIt();
        }

        //private async void PingServer()
        //{
        //    await _hubConnection.SendAsync("ReportingBack");
        //}

        private async void RemoveClient(string username)
        {
            _users.Remove(username);
        }

        private async Task UpdateMood(string mood)
        {
            await _hubConnection.SendAsync("Broadcast", "System", _username + " is " + mood);
        }
        private async Task DisconnectAsync()
        {
            if (_isChatting)
            {
                await SendAsync($"[Notice] {_username} left chat room.");
                await _hubConnection.SendAsync("ClientLoggingOff", _username);
                await _hubConnection.StopAsync();
                await _hubConnection.DisposeAsync();
                _hubConnection = null;
                _isChatting = false;
                StateHasChanged();
            }
        }

        private void UpdateClient()
        {
            StateHasChanged();
        }

        private async Task ScrollIt()
        {
            await JS.InvokeAsync<object>("autoScroll");
        }
        private async Task SendAsync(string message)
        {
            if (_isChatting && !string.IsNullOrWhiteSpace(message))
            {
                await _hubConnection.SendAsync("Broadcast", _username, message);

                _newMessage = string.Empty;
            }
        }

        private class Message
        {
            public Message(string username, string body, bool mine)
            {
                Username = username;
                Body = body;
                Mine = mine;
            }

            public string Username { get; set; }
            public string Body { get; set; }
            public bool Mine { get; set; }

            public bool IsNotice => Body.StartsWith("[Notice]");

            public string CSS => Mine ? "sent" : "received";
        }
    }
}
