﻿using FrameWork;
using System;

namespace Common.Database.World.Battlefront
{
    [DataTable(PreCache = false, TableName = "rvr_zone_lock_eligibility_history", DatabaseName = "World", BindMethod = EBindingMethod.StaticBound)]
    [Serializable]
    public class rvr_zone_lock_eligibility_history : DataObject
    {
        [PrimaryKey(AutoIncrement = true)]
        public long HistoryId { get; set; }

        [DataElement(AllowDbNull = false), PrimaryKey]
        public int ZoneId { get; set; }

        [DataElement(AllowDbNull = false)]
        public DateTime Timestamp { get; set; }

        [DataElement(AllowDbNull = false)]
        public int LockingRealm { get; set; }

        [DataElement(AllowDbNull = false)]
        public int CharacterId { get; set; }

        [DataElement(AllowDbNull = false)]
        public string CharacterName { get; set; }

        [DataElement(AllowDbNull = false)]
        public string ZoneName { get; set; }

        [DataElement(AllowDbNull = false)]
        public int ContributionValue { get; set; }
    }
}