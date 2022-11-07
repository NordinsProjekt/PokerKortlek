using DataLayer.DTO;
using DataLayer.Interfaces;
using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class GameStateToCSV : IGameState
    {
        public void RemoveGameSave(string nameOfSave)
        {
            throw new NotImplementedException();
        }

        public GameStateRecord RestoreGameState(string nameOfSave)
        {
            throw new NotImplementedException();
        }

        public void SaveGameState(GameStateRecord gsr,string nameOfSave)
        {
            throw new NotImplementedException();
        }

    }
}
