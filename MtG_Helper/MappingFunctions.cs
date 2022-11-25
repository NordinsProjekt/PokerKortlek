using Mapster;
using MtG_Application.DTO;
using MtgApiManager.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtG_Infra
{
    public static class MappingFunctions
    {
        public static MtGRecordDTO MapICardToNewDto(ICard card)
        {
            var cardDto = card.Adapt<MtGRecordDTO>();
            return cardDto;
        }
    }
}
