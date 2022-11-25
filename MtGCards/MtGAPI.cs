using Microsoft.Extensions.DependencyInjection;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.Service;
using MtGCards.DTO;
using System;

namespace MtGCards
{
    public class MtGAPI
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

        public async Task<IOperationResult<List<ICard>>> GetCardById(string id)
        {
            IMtgServiceProvider serviceProvider = new MtgServiceProvider();
            ICardService service = serviceProvider.GetCardService();
            var result = await service.Where(x => x.Id, id)
                                      .AllAsync();           
            return result;
        }
    }
}