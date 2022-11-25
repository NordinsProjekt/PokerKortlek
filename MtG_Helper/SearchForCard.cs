using MtG_Application.DTO;
using MtG_Application.Interface;
using MtG_Infra;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.Service;

namespace MtG_Infra
{
    public class SearchForCard : IMtGCardRepository
    {
        //Super coolt.
        //await behövs i unittestet också om det skall testas.
        public async Task<IOperationResult<List<ICard>>> TestMtG()
        {
            IMtgServiceProvider serviceProvider = new MtgServiceProvider();
            ISetService service = serviceProvider.GetSetService();
            var result = await service.GenerateBoosterAsync("ktk");
            return result;
        }

        public async Task<IOperationResult<List<ICard>>> GetCard(string cardName)
        {
            IMtgServiceProvider serviceProvider = new MtgServiceProvider();
            ICardService service = serviceProvider.GetCardService();
            var result = await service.Where(x => x.Name, cardName.ToString())
                                      .AllAsync();

            return result;

        }

        public async Task<IOperationResult<List<ICard>>> GetCardsByCMC(string CMC)
        {
            IMtgServiceProvider serviceProvider = new MtgServiceProvider();
            ICardService service = serviceProvider.GetCardService();
            var result = await service.Where(x => x.Cmc,CMC)
                                      .AllAsync();
            return result;
        }

        public async Task<IOperationResult<List<ICard>>> GetCardById(string id)
        {
            IMtgServiceProvider serviceProvider = new MtgServiceProvider();
            ICardService service = serviceProvider.GetCardService();
            var result = await service.Where(x => x.Id, id)
                                      .AllAsync();
            return result;
        }

        private async Task<List<MtGRecordDTO>> ConvertICardToDTO(IOperationResult<List<ICard>> list)
        {
            List<MtGRecordDTO> dtoList = new List<MtGRecordDTO>();
            foreach (var card in list.Value)
                dtoList.Add(MappingFunctions.MapICardToNewDto(card));
            return dtoList;
        }

        public async Task<List<MtGRecordDTO>> GetCardsByName(string name)
        {
            IMtgServiceProvider serviceProvider = new MtgServiceProvider();
            ICardService service = serviceProvider.GetCardService();
            var result = await service.Where(x => x.Name, name)
                                      .AllAsync();
            return await ConvertICardToDTO(result);
        }
    }
}