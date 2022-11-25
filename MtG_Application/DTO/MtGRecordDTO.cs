using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtG_Application.DTO
{
    public record MtGRecordDTO(string Name,string Id,string Text, string[] Rules,string ImageUrl, string MultiverseId);
}
