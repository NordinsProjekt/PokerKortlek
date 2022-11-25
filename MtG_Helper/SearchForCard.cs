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
        private async Task<List<MtGCardRecordDTO>> ConvertICardToDTO(IOperationResult<List<ICard>> list)
        {
            List<MtGCardRecordDTO> dtoList = new List<MtGCardRecordDTO>();

            foreach (var card in list.Value)
            {
                dtoList.Add(MappingFunctions.MapICardToNewDto(card));
            }
            return dtoList;
        }

        public async Task<List<MtGCardRecordDTO>> GetCardsByName(string name)
        {
            IMtgServiceProvider serviceProvider = new MtgServiceProvider();
            ICardService service = serviceProvider.GetCardService();
            try
            {
                var result = await service.Where(x => x.Name, name)
                          .AllAsync();
                return await ConvertICardToDTO(result);
            }
            catch (Exception ex)
            {
                return new List<MtGCardRecordDTO>();
            }
        }
    }
}