using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtG_Application.DTO
{
    public sealed record MtGCardRecordDTO(string Name,string Id,string Text, 
        List<MtGRulingRecord_DTO> Rulings,string ImageUrl, string MultiverseId);
}
