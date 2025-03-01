﻿using FrameWork;
using System;

namespace Common
{
    [DataTable(PreCache = false, TableName = "random_names", DatabaseName = "World", BindMethod = EBindingMethod.StaticBound)]
    [Serializable]
    public class random_names : DataObject
    {
        private string _Name;

        [PrimaryKey]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
    }
}