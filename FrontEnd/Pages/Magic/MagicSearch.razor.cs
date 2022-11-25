using MtG_Application.DTO;
using MtG_Application;
using Microsoft.AspNetCore.Components;
using MtG_Application.Interface;

namespace FrontEnd.Pages.Magic
{
    public partial class MagicSearch
    {
        [Inject]
        IMtGCardRepository Rep { get; set; }
        List<MtGCardRecordDTO> searchResult = new List<MtGCardRecordDTO>();
        string SearchText { get; set; }
        //protected override async void OnInitialized()
        //{
        //    MtGCardService mtg = new MtGCardService(Rep);
        //    searchResult = await mtg.GetCardByName("Tome");
        //    StateHasChanged();
        //}

        public async void Search()
        {
            MtGCardService mtg = new MtGCardService(Rep);
            if (SearchText !="")
            {
                searchResult = await mtg.GetCardByName(SearchText);
                StateHasChanged();
            }
        }
    }
}
