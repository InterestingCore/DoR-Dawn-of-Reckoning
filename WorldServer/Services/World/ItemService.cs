﻿using Common;
using Common.Database.World.Items;
using FrameWork;
using System.Collections.Generic;

namespace WorldServer.Services.World
{
    [Service]
    public class ItemService : ServiceBase
    {
        public static Dictionary<uint, item_infos> _Item_Info;

        [LoadingFunction(true)]
        public static void LoadItem_Info()
        {
            Log.Debug("WorldMgr", "Loading Item_Info...");

            int i;

            _Item_Info = Database.MapAllObjects<uint, item_infos>("Entry", "Name != ''", 100000);

            foreach (item_infos Info in _Item_Info.Values)
            {
                foreach (KeyValuePair<byte, ushort> Kp in Info._Stats)
                {
                    if (Kp.Key >= byte.MaxValue || Kp.Value >= ushort.MaxValue)
                    {
                        Info.Stats = "";
                        Info.SellPrice = 0;
                        Info._Stats.Clear();
                        break;
                    }
                }

                foreach (KeyValuePair<byte, ushort> Kp in Info._Crafts)
                {
                    if (Kp.Key >= byte.MaxValue || Kp.Value > ushort.MaxValue)
                    {
                        Info.Crafts = "";
                        Info.SellPrice = 0;
                        Info._Crafts.Clear();
                        break;
                    }
                }

                if (Info.Speed != 0 && Info.Dps == 0)
                {
                    // Why is this here? - Az
                    //Info.SellPrice = 0;
                    Info.Speed = 0;
                }
                else if (Info.Dps != 0 && Info.Speed == 0)
                {
                    Info.Dps = 0;
                    // Ditto - Az
                    //Info.SellPrice = 0;
                }

                if (Info.Unk27[4] != 3 || Info.Unk27[5] != 2)
                {
                    for (i = 0; i < Info.Unk27.Length; ++i)
                    {
                        Info.Unk27[i] = 0;
                    }

                    Info.Unk27[4] = 3;
                    Info.Unk27[5] = 2;
                }
            }

            Log.Success("LoadItem_Info", "Loaded " + _Item_Info.Count + " Item_Info");

            foreach (item_infos Info in _Item_Info.Values)
            {
                Info.RequiredItems = new List<KeyValuePair<item_infos, ushort>>(Info._SellRequiredItems.Count);
                foreach (KeyValuePair<uint, ushort> Kp in Info._SellRequiredItems)
                {
                    Info.RequiredItems.Add(new KeyValuePair<item_infos, ushort>(GetItem_Info(Kp.Key), Kp.Value));
                }
            }
        }

        public static item_infos GetItem_Info(uint Entry)
        {
            item_infos info;
            _Item_Info.TryGetValue(Entry, out info);
            return info;
        }

        public static Dictionary<uint, item_sets> _Item_Sets;

        [LoadingFunction(true)]
        public static void LoadItem_Set()
        {
            Log.Debug("WorldMgr", "Loading Item_Set...");

            _Item_Sets = new Dictionary<uint, item_sets>();

            IList<item_sets> Infos = Database.SelectAllObjects<item_sets>();

            foreach (item_sets Info in Infos)
                _Item_Sets.Add(Info.Entry, Info);

            Log.Success("LoadItem_Set", "Loaded " + _Item_Sets.Count + " Item_Set");
        }

        public static item_sets GetItem_Set(uint Entry)
        {
            if (_Item_Sets.ContainsKey(Entry))
                return _Item_Sets[Entry];
            return null;
        }

        public static List<black_market_vendor_items> _BlackMarket_Items;

        [LoadingFunction(true)]
        public static void LoadBlackMarketItems()
        {
            Log.Debug("WorldMgr", "LoadBlackMarketItems...");

            _BlackMarket_Items = new List<black_market_vendor_items>();

            IList<black_market_vendor_items> items = Database.SelectAllObjects<black_market_vendor_items>();

            foreach (var blackMarketItem in items)
            {
                _BlackMarket_Items.Add(blackMarketItem);
            }

            Log.Success("LoadBlackMarketItems", "Loaded " + _Item_Sets.Count + " LoadBlackMarketItems");
        }
    }
}