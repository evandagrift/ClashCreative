using RoyaleTrackerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyaleTrackerAPI.Repos
{
    interface ITeamsRepo
    {

        void AddTeam(Team team);
        List<Team> GetAllTeams();
        Team GetTeamByID(int teamID);
        void DeleteTeam(int teamID);
        void UpdateTeam(Team team);
    }
}
