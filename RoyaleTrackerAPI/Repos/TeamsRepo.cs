using RoyaleTrackerAPI.Models;
using RoyaleTrackerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyaleTrackerAPI.Repos
{
    public class TeamsRepo : ITeamsRepo
    {
        private TRContext context;
        public TeamsRepo(TRContext c)
        {
            context = c;
        }

        public void AddTeam(Team team)
        {
            context.Teams.Add(team);
        }

        public void DeleteTeam(int teamID)
        {
            Team team = GetTeamByID(teamID);
            context.Teams.Remove(team);
        }

        public List<Team> GetAllTeams()
        {
            return context.Teams.ToList();
        }

        public Team GetTeamByID(int teamID)
        {
            return context.Teams.Find(teamID);
        }
    }
}
