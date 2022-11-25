using MtG_Application.DTO;
using MtG_Application.Interface;

namespace MtG_Application
{
    public class MtGCardService
    {
        IMtGCardRepository _repository;
        public MtGCardService(IMtGCardRepository _rep) 
        {
            _repository = _rep;
        }
        public async Task<List<MtGRecordDTO>> GetCardByName(string name)
        {
            var result = await _repository.GetCardsByName(name);
            return result.Where(m=>m.MultiverseId!=null).GroupBy(x=>x.Name).Select(f=>f.First()).ToList();
        }
    }
}