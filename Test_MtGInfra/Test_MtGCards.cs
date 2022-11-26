using SearchForCards_MockData;
using MtG_Application;

namespace Test_MtGInfra
{
    public class Test_MtGCards
    {
        [Fact]
        public async void SearchForCardsWithTomeInTheName_ShouldGetListOfCustomDTO()
        {
            MockData rep = new MockData();
            MtGCardService mtGCardService = new MtGCardService(rep);
            var cardList = await mtGCardService.GetCardByName("TestCard1");
            Assert.Single(cardList);
        }
    }
}