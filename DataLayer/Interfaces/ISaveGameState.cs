using DataLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IGameState
    {
        public void SaveGameState(GameStateRecord gsr,string nameOfSave);
        public GameStateRecord RestoreGameState(string nameOfSave);
        public void RemoveGameSave(string nameOfSave);
    }
}
