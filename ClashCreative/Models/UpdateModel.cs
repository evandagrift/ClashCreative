using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClashCreative.Models
{
    public class UpdateModel
    {
        public UpdateModel()
        {
            UpdateComplete = false;
        }
        public bool UpdateComplete { get; set; }
        public ClashDB ClashDB { get;set; }
        public ClashJson ClashJson { get; set; }


        public async Task<bool> SaveToDBByClan(ClashContext context)
        {
            DateTime now = DateTime.Now;

            var clan = await ClashJson.GetClanData("#8CYPL8R");

            clan.UpdateTime = now;
            context.Clans.Add(clan);

            List<Player> playersToAdd = new List<Player>();
            List<Battle> battlesToAdd = new List<Battle>();
            int savedBattles = 0;

            //fills the list of player data to add to the DB
            // for (int m = 0; m < clan.Members; m++)

            for (int m = 0; m < 15; m++)
            {
                //gets basic player
                var player = await ClashJson.GetPlayerData(clan.MemberList[m].Tag);
                player = await ClashDB.FillPlayerDBData(player);
                playersToAdd.Add(player);
            }


            context.Players.AddRange(playersToAdd);

            for (int p = 0; p < playersToAdd.Count(); p++)
            {
                var battles = await ClashJson.GetListOfBattles(playersToAdd[p].Tag);
                battles = battles.GetRange(0,10);
                savedBattles += await ClashDB.SaveBattles(battles);
            }


            context.SaveChanges();

            UpdateComplete = true;
            return UpdateComplete;
        }

    }
}
