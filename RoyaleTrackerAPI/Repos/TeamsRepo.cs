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
        //DB Access
        private TRContext context;
        //constructor assigns the argument DB context
        public TeamsRepo(TRContext c) { context = c; }

        //adds the given team to context
        public void AddTeam(Team team) { context.Teams.Add(team); }

        //deletes the team at given ID from the DB
        public void DeleteTeam(int teamID)
        {
            Team team = GetTeamByID(teamID);
            context.Teams.Remove(team);
        }

        //returns list of all teams from DB
        public List<Team> GetAllTeams() { return context.Teams.ToList(); }

        //returns Team with given ID from DB
        public Team GetTeamByID(int teamID) { return context.Teams.Find(teamID); }

        //updates team at given ID with properties from given argument
        public void UpdateTeam(Team team)
        {
            //fetches team from database
            Team teamToUpdate = GetTeamByID(team.TeamId);

            //changes fetched instance from the DB
            if (teamToUpdate != null)
            {
                teamToUpdate.TeamName = team.TeamName;
                teamToUpdate.TwoVTwo = team.TwoVTwo;
                teamToUpdate.Name = team.Name;
                teamToUpdate.Tag = team.Tag;
                teamToUpdate.Name2 = team.Name2;
                teamToUpdate.Tag2 = team.Tag2;
            }

        }
    }
}
