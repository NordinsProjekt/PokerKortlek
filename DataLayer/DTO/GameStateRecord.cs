using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO
{
    public sealed record GameStateRecord(
            List<List<CardRecord>> Hands,
            List<CardRecord> Deck, 
            List<CardRecord>DeckOrgState
        );
}
