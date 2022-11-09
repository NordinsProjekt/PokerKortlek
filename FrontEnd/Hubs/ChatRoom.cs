using Microsoft.AspNetCore.SignalR;

namespace FrontEnd.Hubs
{
    public class ChatRoom : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
