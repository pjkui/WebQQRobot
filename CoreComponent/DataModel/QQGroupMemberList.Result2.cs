﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebQQRobot.DataModel
{

    public partial class QQGroupMemberList
    {
        public class Result2
        {

            [JsonProperty("stats")]
            public Stat[] Stats;

            [JsonProperty("minfo")]
            public Minfo2[] Minfo;

            [JsonProperty("ginfo")]
            public Ginfo2 Ginfo;

            [JsonProperty("cards")]
            public Card[] Cards;

            [JsonProperty("vipinfo")]
            public Vipinfo2[] Vipinfo;
        }
    }

}
