﻿using Common.Database.World.Battlefront;
using FrameWork;
using System.Collections.Generic;

namespace WorldServer.Services.World
{
    [Service]
    public class BountyService : ServiceBase
    {
        public static List<bounty_contribution_definition> _ContributionDefinitions;

        [LoadingFunction(true)]
        public static void LoadContributionDefinition()
        {
            Log.Debug("WorldMgr", "Loading ContributionManagerInstance Definitions...");
            _ContributionDefinitions = Database.SelectAllObjects<bounty_contribution_definition>() as List<bounty_contribution_definition>;
            Log.Success("ContributionDefinition", "Loaded " + _ContributionDefinitions.Count + " ContributionDefinitions");

            if (_ContributionDefinitions.Count == 0)
                Log.Error("Error Loading DB", "No Bounty Contributions Loaded");
        }

        public bounty_contribution_definition GetDefinition(byte value)
        {
            foreach (var contributionDefinition in _ContributionDefinitions)
            {
                if (contributionDefinition.ContributionId == value)
                    return contributionDefinition;
            }
            return null;
        }
    }
}