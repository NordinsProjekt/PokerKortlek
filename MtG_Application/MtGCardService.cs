using MtG_Application.DTO;
using MtG_Application.Interface;

namespace MtG_Application
{
    public class MtGCardService
    {
        private IMtGCardRepository _repository;
        public MtGCardService(IMtGCardRepository _rep) =>
            _repository = _rep;

        public async Task<List<MtGCardRecordDTO>> GetCardByName(string name)
        {
            var result = await _repository.GetCardsByName(name);
            //Bara kort som har unikt namn och som har en bild
            return result.Where(m=>m.MultiverseId!=null).GroupBy(x=>x.Name).Select(f=>f.First()).ToList();
        }
    }
}