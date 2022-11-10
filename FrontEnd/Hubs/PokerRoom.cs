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
        public async Task Join(PokerWrapper game,int playerId)
        {
            if (game.playerList.Count <=2 && game.playerList.Contains(Context.ConnectionId) == false)
            {
                game.playerList.Add(Context.ConnectionId);
                Console.WriteLine(Context.ConnectionId);
                await Clients.Caller.SendAsync("JoinRoom", true, game.playerList.Count);
                await Clients.Caller.SendAsync("SendHand", game.GetHand(game.playerList.Count - 1));
                if (game.playerList.Count == 2)
                    await Clients.All.SendAsync("StartGame", true);
            }
            else
            {
                await Clients.Caller.SendAsync("JoinRoom", false,0);
            }
        }
        
        public async Task GetCards(int numOfCards, PokerWrapper game)
        {
            List<CardRecord> cards = new List<CardRecord>();
            var deck = game.GetDeck();
            for (int i = 0; i < numOfCards; i++)
                cards.Add(deck.Draw());
            await Clients.Caller.SendAsync("DealCards",cards);
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
