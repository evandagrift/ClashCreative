using RoyaleTrackerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyaleTrackerAPI.Repos.IRepos
{
    interface IGameModesRepo
    {
        void AddGameMode(GameMode GameMode);
        List<GameMode> GetAllGameModes();
        GameMode GetGameModeByID(int GameModeID);
        void DeleteGameMode(int GameModeID);
        void UpdateGameMode(GameMode GameMode);
    }
}