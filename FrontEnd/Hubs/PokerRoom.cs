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
        public async Task Join(PokerWrapper game)
        {
            if (game.playerList.Count <=2 && game.playerList.Contains(Context.ConnectionId) == false)
            {
                game.playerList.Add(Context.ConnectionId);
                Console.WriteLine(Context.ConnectionId);
                await Clients.Caller.SendAsync("JoinRoom", true, game.playerList.Count);
                await Clients.Caller.SendAsync("SendHand", game.GetHand(game.playerList.Count - 1));
                if (game.playerList.Count == 2)
                {
                    await Clients.All.SendAsync("StartGame", true);
                }
            }
            else
            {
                await Clients.Caller.SendAsync("JoinRoom", false,0);
            }
        }
        
        public async Task GetCards(int numOfCards,string player,PokerWrapper game)
        {
            int x = numOfCards;
            string t = player;
            List<CardRecord> newCards = new List<CardRecord>();
            await Clients.All.SendAsync("DealCards",newCards);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task DealCards(PokerWrapper game)
        {
            if (game.playerList.Count == 2)
            {
                //await Clients.All.SendAsync("SendHand",)
                //Tänkt fel, vet inte vilken hand som skall skickas.
            }
        }
    }
}
