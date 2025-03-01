﻿using Common;
using Common.Database.World.Battlefront;
using GameData;
using NLog;
using System.Collections.Generic;
using WorldServer.Services.World;
using WorldServer.World.Battlefronts.Bounty;
using WorldServer.World.Objects;

namespace WorldServer.World.Battlefronts.Apocalypse
{
    public class BattleFrontStatus
    {
        private static readonly Logger BattlefrontLogger = LogManager.GetLogger("BattlefrontLogger");

        public int BattleFrontId { get; set; }
        public int PairingId { get; set; }
        public SetRealms LockingRealm { get; set; }
        public VictoryPointProgress FinalVictoryPoint { get; set; }
        public int OpenTimeStamp { get; set; }
        public int LockTimeStamp { get; set; }
        public bool Locked { get; set; }
        public int RegionId { get; set; }
        public string Description { get; set; }
        public ContributionManager ContributionManagerInstance { get; set; }
        public ImpactMatrixManager ImpactMatrixManagerInstance { get; set; }
        public RewardManager RewardManagerInstance { get; set; }
        public HashSet<uint> KillContributionSet { get; set; }
        public Player DestructionRealmCaptain { get; set; }
        public Player OrderRealmCaptain { get; set; }
        public List<keep_infos> KeepList { get; set; }
        public List<battlefront_objectives> BattlefieldObjectives { get; set; }
        public int DestroZoneId { get; internal set; }
        public int OrderZoneId { get; internal set; }
        public rvr_progression Progression { get; internal set; }

        public BattleFrontStatus(ImpactMatrixManager impactMatrixManager, int battleFrontId)
        {
            ImpactMatrixManagerInstance = impactMatrixManager;
            BattleFrontId = battleFrontId;

            ContributionManagerInstance = PlayerContributionManager.LoadPlayerContribution(battleFrontId);

            RewardManagerInstance = new RewardManager(ContributionManagerInstance, new StaticWrapper(), RewardService._RewardPlayerKills, ImpactMatrixManagerInstance);

            KeepList = new List<keep_infos>();
            BattlefieldObjectives = new List<battlefront_objectives>();
            if (battleFrontId != 0)
                BattleFrontId = battleFrontId;
        }

        public float DestructionVictoryPointPercentage
        {
            get { return FinalVictoryPoint.DestructionVictoryPointPercentage; }
        }

        public float OrderVictoryPointPercentage
        {
            get { return FinalVictoryPoint.OrderVictoryPointPercentage; }
        }

        /// <summary>
        /// Get the lock status of this battlefront (for use in communicating with the client)
        /// </summary>
        public int LockStatus
        {
            get
            {
                if (Locked)
                {
                    if (LockingRealm == SetRealms.REALMS_REALM_DESTRUCTION)
                        return BattleFrontConstants.ZONE_STATUS_DESTRO_LOCKED;
                    if (LockingRealm == SetRealms.REALMS_REALM_ORDER)
                        return BattleFrontConstants.ZONE_STATUS_ORDER_LOCKED;
                }
                return BattleFrontConstants.ZONE_STATUS_CONTESTED;
            }
        }

        public void AddKillContribution(Player player)
        {
            KillContributionSet.Add(player.CharacterId);
        }

        public void RemoveAsRealmCaptain(Player realmCaptain)
        {
            if (realmCaptain == null)
                return;
            BattlefrontLogger.Info($"Removing player {realmCaptain.Name} as Captain");
            if (realmCaptain.Realm == SetRealms.REALMS_REALM_DESTRUCTION)
                DestructionRealmCaptain = null;
            if (realmCaptain.Realm == SetRealms.REALMS_REALM_ORDER)
                OrderRealmCaptain = null;

            //ScaleDownModel(realmCaptain);
        }

        public void SetAsRealmCaptain(Player realmCaptain)
        {
            if (realmCaptain == null)
                return;

            BattlefrontLogger.Info($"Adding player {realmCaptain.Name} as Captain");
            if (realmCaptain.Realm == SetRealms.REALMS_REALM_DESTRUCTION)
                DestructionRealmCaptain = realmCaptain;
            if (realmCaptain.Realm == SetRealms.REALMS_REALM_ORDER)
                OrderRealmCaptain = realmCaptain;
        }

        //private void ScaleDownModel(Player player)
        //{
        //    player.EffectStates.Remove((byte)ObjectEffectState.OBJECTEFFECTSTATE_SCALE_UP);

        //    var Out = new PacketOut((byte)Opcodes.F_OBJECT_EFFECT_STATE);

        //    Out.WriteUInt16(player.Oid);
        //    Out.WriteByte(1);
        //    Out.WriteByte((byte)ObjectEffectState.OBJECTEFFECTSTATE_SCALE_UP);
        //    Out.WriteByte((byte)(0));
        //    Out.WriteByte(0);

        //    player.DispatchPacket(Out, true);
        //}
    }
}