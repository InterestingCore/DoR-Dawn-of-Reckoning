﻿using FrameWork;
using System;
using SystemData;

namespace Common
{
    [DataTable(PreCache = false, TableName = "banned_names", DatabaseName = "Characters", BindMethod = EBindingMethod.StaticBound)]
    [Serializable]
    public class banned_names : DataObject
    {
        [PrimaryKey]
        public string NameString { get; set; }

        [DataElement]
        public string FilterTypeString
        {
            get
            {
                return FilterType.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    FilterType = (NameFilterType)Enum.Parse(typeof(NameFilterType), value);
            }
        }

        public NameFilterType FilterType;
    }
}