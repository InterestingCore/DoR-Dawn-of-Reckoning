﻿using Common;
using Common.Database.World.Characters;
using System.Collections.Generic;
using System.Linq;
using WorldServer.Services.World;

namespace WorldServer.World.Objects
{
    public class HonorVendorItem
    {
        public List<creature_vendors> items = new List<creature_vendors>();

        public HonorVendorItem(Player player)
        {
            items = GetHonorRankItems(player, player.Info.HonorRank);
        }

        private List<creature_vendors> GetHonorRankItems(Player player, int rank)
        {
            var rankItems = HonorService.HonorRewards.Where(x => x.HonorRank <= rank);

            foreach (var honorReward in rankItems)
            {
                if (IsValidItemForPlayer(player, honorReward))
                {
                    var item = new creature_vendors
                    {
                        Info = ItemService.GetItem_Info((uint)honorReward.ItemId),
                        ItemId = (uint)honorReward.ItemId,
                        Price = 0,
                        VendorId = 0
                    };
                    items.Add(item);
                }
            }
            return items;
        }

        public bool IsValidItemForPlayer(Player player, honor_rewards honorReward)
        {
            if (honorReward.Realm == 0 || honorReward.Realm == (int)player.Realm)
            {
                if (honorReward.Class == 0 || honorReward.Class == player.Info.CareerLine)
                {
                    // Ensure the player doesn't have more than max count of these items.
                    if (!player.GetCountOfPlayerItems(honorReward.ItemId, honorReward.MaxCount))
                    {
                        if (HonorItemCooldown(honorReward.ItemId, player) <
                            FrameWork.TCPManager.GetTimeStamp() || honorReward.Cooldown == 0)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        // What is the time (seconds) that this item will be re-purchasble.

        private long HonorItemCooldown(int honorRewardItemId, Player player)
        {
            var cooldown = player.Info.HonorCooldowns?.SingleOrDefault(x => x.ItemId == honorRewardItemId);
            if (cooldown == null)
                return 0;
            else
                return cooldown.Cooldown;
        }
    }
}