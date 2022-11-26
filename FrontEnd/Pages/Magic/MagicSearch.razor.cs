using MtG_Application.DTO;
using MtG_Application;
using Microsoft.AspNetCore.Components;
using MtG_Application.Interface;
using Microsoft.JSInterop;

namespace FrontEnd.Pages.Magic
{
    public partial class MagicSearch
    {
        [Inject]
        IMtGCardRepository Rep { get; set; }
        [Inject]
        IJSRuntime JsRuntime { get; set; }
        List<MtGCardRecordDTO> searchResult = new List<MtGCardRecordDTO>();
        MtGCardRecordDTO? clickedCard;
        string? SearchText { get; set; }

        public async void Search()
        {
            MtGCardService mtg = new MtGCardService(Rep);
            if (SearchText !="")
            {
                searchResult = await mtg.GetCardByName(SearchText);
                StateHasChanged();
            }
        }

        public async void ShowCard(string id)
        {
            clickedCard = searchResult.Where(x => x.Id == id).FirstOrDefault();
            await JsRuntime.InvokeVoidAsync("OnScrollEvent");
            StateHasChanged();
        }
    }
}
