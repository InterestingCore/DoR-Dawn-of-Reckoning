﻿using FrameWork;
using System;

namespace Common.Database.World.Battlefront
{
    [DataTable(PreCache = false, TableName = "rvr_player_contribution", DatabaseName = "World", BindMethod = EBindingMethod.StaticBound)]
    [Serializable]
    public class rvr_player_contribution : DataObject
    {
        [PrimaryKey(AutoIncrement = true)]
        public int Id { get; set; }

        [DataElement(AllowDbNull = false)]
        public int BattleFrontId { get; set; }

        [DataElement(AllowDbNull = false)]
        public uint CharacterId { get; set; }

        [DataElement(AllowDbNull = false)]
        public string ContributionSerialised { get; set; }

        [DataElement(AllowDbNull = false)]
        public DateTime Timestamp { get; set; }
    }
}