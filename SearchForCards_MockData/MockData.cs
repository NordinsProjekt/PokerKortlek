using MtG_Application.DTO;
using MtG_Application.Interface;

namespace SearchForCards_MockData
{
    public class MockData : IMtGCardRepository
    {
        public async Task<List<MtGCardRecordDTO>> GetCardsByName(string name)
        {
            List<MtGCardRecordDTO> list = new List<MtGCardRecordDTO>(){
                new MtGCardRecordDTO("TestCard1", "testxxx1", "Rules Text",
                new List<MtGRulingRecord_DTO> { new MtGRulingRecord_DTO("2022-10-10", "Rule 1") },
                "http://", "rfr04hf"),
                new MtGCardRecordDTO("TestCard2", "testxxx2", "Rules Text2",
                new List<MtGRulingRecord_DTO> { new MtGRulingRecord_DTO("2022-10-11", "Rule 2") },
                "http://", "rfr04hf2"),
            };
            return list.Where(x=>x.Name== name).ToList();
        }
    }
}