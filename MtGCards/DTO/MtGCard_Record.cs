using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtGCards.DTO
{
    public record MtGCard_Record(string Type,string Toughness,string Power, string[] Rulings,
        string Name,string[] Colors,float Cmc,string Text,string Id,string ImageUrl);
}
