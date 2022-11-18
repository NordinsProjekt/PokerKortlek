
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.SignalR;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace FrontEnd.Hubs
{
    public class GameHub : Hub
    {
        public const string HubUrl = "/game";
        public async Task Broadcast(string username, string message)
        {
            //Behöver bättre namn, lägg till om ny.
            await Clients.All.SendAsync("Broadcast", username, message);
        }

        public async Task SendMood(string username, string mood)
        {
            await Clients.All.SendAsync("Refresh");
        }

        public override async Task OnConnectedAsync()
        {
            await  base.OnConnectedAsync();
        }
        public async Task ClientLoggingOff(string username)
        {
            await Clients.Others.SendAsync("RemoveUser",username);
        }
        public override async Task OnDisconnectedAsync(Exception e)
        {
            await Clients.Others.SendAsync("Refresh");
            await base.OnDisconnectedAsync(e);
        }
    }


}
