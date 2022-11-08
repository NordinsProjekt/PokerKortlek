using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO
{
    public sealed record HandRecord(List<CardRecord> CardList, string HandName, int HandId);
}
