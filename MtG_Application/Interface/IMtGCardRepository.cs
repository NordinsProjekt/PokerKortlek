using MtG_Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtG_Application.Interface
{
    public interface IMtGCardRepository
    {
        public Task<List<MtGRecordDTO>> GetCardsByName(string name);
    }
}
