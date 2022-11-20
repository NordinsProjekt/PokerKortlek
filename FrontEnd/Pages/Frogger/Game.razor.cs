using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace FrontEnd.Pages.Frogger
{
    public partial class Game
    {
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JS.InvokeVoidAsync("AddListeners");
            await JS.InvokeVoidAsync("StartGame");
        }
    }
}
