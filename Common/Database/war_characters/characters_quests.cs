﻿using FrameWork;
using System;
using System.Collections.Generic;

namespace Common
{
    public class Character_Objectives
    {
        public characters_quests Quest;
        public quests_objectives Objective;
        public int ObjectiveID;
        public int _Count;

        public int Count
        {
            get
            {
                return _Count;
            }
            set
            {
                _Count = value;
                Quest.Dirty = true;
            }
        }

        public bool IsDone()
        {
            return Count >= Objective?.ObjCount;
        }
    }

    [DataTable(PreCache = false, TableName = "characters_quests", DatabaseName = "Characters")]
    [Serializable]
    public class characters_quests : DataObject
    {
        [PrimaryKey]
        public uint CharacterId { get; set; }

        [PrimaryKey]
        public ushort QuestID { get; set; }

        [DataElement(AllowDbNull = false, Varchar = 64)]
        public string Objectives
        {
            get
            {
                string value = "";
                foreach (Character_Objectives obj in _Objectives)
                    value += obj.ObjectiveID + ":" + obj.Count + "|";
                return value;
            }
            set
            {
                if (value.Length <= 0)
                    return;

                string[] Objectives = value.Split('|');

                foreach (string objectiveString in Objectives)
                {
                    if (objectiveString.Length <= 0)
                        continue;

                    int objectiveID = int.Parse(objectiveString.Split(':')[0]);
                    int count = int.Parse(objectiveString.Split(':')[1]);

                    Character_Objectives cObj = new Character_Objectives
                    {
                        Quest = this,
                        ObjectiveID = objectiveID,
                        _Count = count
                    };
                    _Objectives.Add(cObj);
                }
            }
        }

        [DataElement(AllowDbNull = false)]
        public bool Done { get; set; }

        public bool IsDone()
        {
            return _Objectives.TrueForAll(obj => obj.IsDone());
        }

        public List<Character_Objectives> _Objectives = new List<Character_Objectives>();

        public quests Quest;

        public List<byte> SelectedRewards = new List<byte>();
    }
}