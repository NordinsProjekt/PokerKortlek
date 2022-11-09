
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.SignalR;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Blazor_UI.Data
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
            //_userlist.UpdateMood(mood, username);
            await Clients.All.SendAsync("Refresh");
        }

        public async Task ReportBack()
        {
            await Clients.All.SendAsync("AnswerCall");
            //Kanske tar bort folk för tidigt.
            //Kommer fortfarande vara kvar i chatten, men inte synas i listan.
            //_userlist.RemoveAllUsersThatDidntAnswer();
        }

        public void ReportingIn()
        {
           //_userlist.UserIsStillConnected(Context.ConnectionId);
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId} connected");
            await  base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            //_userlist.RemoveUser(Context.ConnectionId);
            await Clients.Others.SendAsync("Refresh");
            await base.OnDisconnectedAsync(e);
        }
    }


}
