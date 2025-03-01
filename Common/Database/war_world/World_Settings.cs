﻿using FrameWork;
using System;

namespace Common
{
    [DataTable(PreCache = false, TableName = "world_settings", DatabaseName = "World")]
    [Serializable]
    public class world_settings : DataObject
    {
        private int _SettingId, _Setting;

        [PrimaryKey(AutoIncrement = true)]
        public int SettingId
        {
            get { return _SettingId; }
            set { _SettingId = value; Dirty = true; }
        }

        [DataElement]
        public int Setting
        {
            get { return _Setting; }
            set { _Setting = value; Dirty = true; }
        }

        [DataElement]
        public string Description
        { get; set; }
    }
}