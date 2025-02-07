﻿using Newtonsoft.Json;
using Pubg.Net.Models.Base;
using Pubg.Net.Models.Seasons;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Pubg.Net
{
    public class PubgMatch : PubgShardedEntity, IComparable<PubgMatch>
    {
        [JsonProperty]
        public string CreatedAt { get; set; }

        [JsonProperty]
        public long Duration { get; set; }

        [JsonProperty]
        public IEnumerable<PubgRoster> Rosters { get; set; }

        [JsonProperty]
        public PubgRound Rounds { get; set; }

        [JsonProperty]
        public IEnumerable<PubgAsset> Assets { get; set; }

        [JsonProperty]
        public PubgMatchStats Stats { get; set; }

        [JsonProperty]
        public PubgGameMode GameMode { get; set; }

        [JsonProperty]
        public string PatchVersion { get; set; }

        [JsonProperty]
        public string TitleId { get; set; }

        [JsonProperty]
        public Dictionary<string, object> Links { get; set; }

        [JsonProperty]
        public PubgMap MapName { get; set; }

        [JsonProperty]
        public bool IsCustomMatch { get; set; }

        [JsonProperty]
        public PubgSeasonState SeasonState { get; set; }

        public int CompareTo(PubgMatch other)
        {
            return DateTime.Compare(DateTime.Parse(other.CreatedAt), DateTime.Parse(CreatedAt));
        }
    }
}
