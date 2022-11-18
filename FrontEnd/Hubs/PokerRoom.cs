using Database;
using DataLayer;
using DataLayer.DTO;
using DataLayer.Interfaces;
using FrontEnd.Classes;
using GameEngine.Classes;
using Microsoft.AspNetCore.SignalR;

namespace FrontEnd.Hubs
{
    public class PokerRoom : Hub
    {
        public async Task StartGame()
        {
            await Clients.All.SendAsync("NewGame");
        }

        public async Task ShowWinner()
        {
            await Clients.All.SendAsync("EndGame");
            await Clients.All.SendAsync("UpdateScore");
        }
    }
}
