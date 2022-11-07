
namespace Database
{
    public class Communicate
    {
        private PokerSimulator2022Context _context = new PokerSimulator2022Context();
        public List<Cards> GetCardList()
        {
            return _context.Cards.OrderBy(x=>x.Id).ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}