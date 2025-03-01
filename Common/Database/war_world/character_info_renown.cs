﻿using FrameWork;
using System;

namespace Common
{
    [DataTable(PreCache = false, TableName = "character_info_renown", DatabaseName = "World", BindMethod = EBindingMethod.StaticBound)]
    [Serializable]
    public class character_info_renown : DataObject
    {
        private byte _id;
        private byte _tree;
        private byte _position;
        private ushort _spellId;
        private byte _renownCosts;
        private byte _slotreq;
        private string _name;
        private bool _passive;
        private string _unk;
        private string _commandName;
        private byte _stat;
        private int _value;

        [PrimaryKey]
        public ushort SpellId
        {
            get { return _spellId; }
            set { _spellId = value; }
        }

        [DataElement(AllowDbNull = false)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [DataElement(AllowDbNull = false)]
        public byte ID
        {
            get { return _id; }
            set { _id = value; }
        }

        [DataElement(AllowDbNull = false)]
        public bool Passive
        {
            get { return _passive; }
            set { _passive = value; }
        }

        [DataElement(AllowDbNull = false)]
        public byte Tree
        {
            get { return _tree; }
            set { _tree = value; }
        }

        [DataElement(AllowDbNull = false)]
        public byte Position
        {
            get { return _position; }
            set { _position = value; }
        }

        [DataElement(AllowDbNull = false)]
        public byte Renown_Costs
        {
            get { return _renownCosts; }
            set { _renownCosts = value; }
        }

        [DataElement(AllowDbNull = false)]
        public byte Slotreq
        {
            get { return _slotreq; }
            set { _slotreq = value; }
        }

        [DataElement(AllowDbNull = false)]
        public string Unk
        {
            get { return _unk; }
            set { _unk = value; }
        }

        [DataElement(AllowDbNull = false)]
        public string CommandName
        {
            get { return _commandName; }
            set { _commandName = value; }
        }

        [DataElement(AllowDbNull = false)]
        public byte Stat
        {
            get { return _stat; }
            set { _stat = value; }
        }

        [DataElement(AllowDbNull = false)]
        public int Value
        {
            get { return _value; }
            set { _value = value; ; }
        }
    }
}