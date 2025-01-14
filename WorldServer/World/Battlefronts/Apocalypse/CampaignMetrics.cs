﻿using Common.Database.World.Battlefront;
using NLog;
using System;
using System.Linq;
using WorldServer.Managers;
using WorldServer.World.Map;
using WorldServer.World.Objects;

namespace WorldServer.World.Battlefronts.Apocalypse
{
    public static class CampaignMetrics
    {
        public static void RecordMetrics(Logger logger, int tier, RegionMgr region, IBattleFrontManager battleFrontManager)
        {
            try
            {
                var groupId = Guid.NewGuid().ToString();

                logger.Trace($"There are {battleFrontManager.GetBattleFrontStatusList().Count} battlefront statuses ({battleFrontManager.GetType().ToString()}).");
                foreach (var status in battleFrontManager.GetBattleFrontStatusList())
                {
                    lock (status)
                    {
                        if (status.RegionId == region.RegionId)
                        {
                            logger.Trace($"Recording metrics for BF Status : ({status.BattleFrontId}) {status.Description}");
                            if (!status.Locked)
                            {
                                var metrics = new rvr_metrics
                                {
                                    BattlefrontId = status.BattleFrontId,
                                    BattlefrontName = status.Description,
                                    DestructionVictoryPoints = battleFrontManager.GetActiveBattleFrontFromProgression(status.PairingId).DestroVP,
                                    OrderVictoryPoints = (int)battleFrontManager.GetActiveBattleFrontFromProgression(status.PairingId).OrderVP,
                                    Locked = status.LockStatus,
                                    OrderPlayersInLake = PlayerUtil.GetTotalOrderPVPPlayerCountInZone(battleFrontManager.GetActiveBattleFrontFromProgression(status.PairingId).OrderZoneId),
                                    DestructionPlayersInLake = PlayerUtil.GetTotalDestPVPPlayerCountInZone(battleFrontManager.GetActiveBattleFrontFromProgression(status.PairingId).DestroZoneId),
                                    Tier = tier,
                                    Timestamp = DateTime.UtcNow,
                                    GroupId = groupId,
                                    TotalPlayerCountInRegion = PlayerUtil.GetTotalPVPPlayerCountInRegion(status.RegionId),
                                    TotalDestPlayerCountInRegion = PlayerUtil.GetTotalDestPVPPlayerCountInRegion(status.RegionId),
                                    TotalOrderPlayerCountInRegion = PlayerUtil.GetTotalOrderPVPPlayerCountInRegion(status.RegionId),
                                    TotalPlayerCount = Player._Players.Count(x => !x.IsDisposed && x.IsInWorld() && x != null),
                                    TotalFlaggedPlayerCount = Player._Players.Count(x => !x.IsDisposed && x.IsInWorld() && x != null && x.CbtInterface.IsPvp)
                                };
                                WorldMgr.Database.AddObject(metrics);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error($"Could not write rvr metrics..continuing. {e.Message} {e.StackTrace}");
            }
        }
    }
}