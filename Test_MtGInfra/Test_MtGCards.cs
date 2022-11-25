using MtG_Infra;
using MtG_Application;

namespace Test_MtGInfra
{
    public class Test_MtGCards
    {
        [Fact]
        public async void SearchForCardsWithTomeInTheName_ShouldGetListOfCustomDTO()
        {
            SearchForCard rep = new SearchForCard();
            MtGCardService mtGCardService = new MtGCardService(rep);
            var cardList = await mtGCardService.GetCardByName("Tome");
            Assert.Equal(22,cardList.Count);
        }
    }
}