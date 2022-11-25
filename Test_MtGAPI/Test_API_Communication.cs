using MtGCards;
using Xunit.Abstractions;

namespace Test_MtGAPI
{
    public class Test_API_Communication
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Test_API_Communication(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        [Fact]
        public async void WillIGetAnAnswer()
        {
            MtGAPI c = new MtGAPI();
            var cards = await c.TestMtG();
            Assert.NotNull(cards);
        }
        [Fact]
        public async void SearchForDelver_ShouldReturnDelver()
        {
            MtGAPI c = new MtGAPI();
            var cards = await c.GetCard("Delver of Secrets");
            Assert.NotNull(cards);
        }

        [Fact]
        public async void SearchForDelverByCardId_ShouldReturnOneCard()
        {
            MtGAPI c = new MtGAPI();
            var cards = await c.GetCardById("ac088667-a4c1-5fe1-96de-f958048644af");
            Assert.True(cards.Value.Count() == 1);
        }
        [Fact]
        public async void SearchForTomeAndDisplay_EveryUniqueCard()
        {
            MtGAPI c = new MtGAPI();
            var cards = await c.GetCard("Tome");
            Assert.NotNull(cards);
            var result = cards.Value.Where(x=>x.MultiverseId !=null).GroupBy(n => n.Name).Select(g => g.First()).ToList();
            _testOutputHelper.WriteLine("Found "+result.Count() + " matches");
            foreach (var card in result)
            {
                _testOutputHelper.WriteLine("Card name: "+card.Name + "\nCard id: "+card.Id+"\nMultiverse code: "+card.MultiverseId
                    +"\nImageUrl: "+card.ImageUrl +"\n");
            }
        }
    }
}