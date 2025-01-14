﻿using FrameWork;
using System;

namespace Common
{
    [DataTable(PreCache = false, TableName = "battlefront_status", DatabaseName = "World", BindMethod = EBindingMethod.StaticBound)]
    [Serializable]
    public class battlefront_status : DataObject
    {
        private int _openZoneIndex;
        private int _activeRegionOrZone;
        private int _controlingRealm;

        public battlefront_status()
        {
        }

        public battlefront_status(int regionId)
        {
            RegionId = regionId;
            OpenZoneIndex = 1;
        }

        [PrimaryKey(AutoIncrement = true)]
        public int RegionId { get; set; }

        [DataElement]
        public int OpenZoneIndex
        {
            get { return _openZoneIndex; }
            set { _openZoneIndex = value; Dirty = true; }
        }

        [DataElement]
        public int ActiveRegionOrZone
        {
            get { return _activeRegionOrZone; }
            set { _activeRegionOrZone = value; Dirty = true; }
        }

        [DataElement]
        public int ControlingRealm
        {
            get { return _controlingRealm; }
            set { _controlingRealm = value; Dirty = true; }
        }
    }
}