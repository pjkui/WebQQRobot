﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebQQRobot.DataModel
{

    public partial class QQMemberList
    {
        public class Stat
        {

            [JsonProperty("client_type")]
            public int ClientType;

            [JsonProperty("uin")]
            public long Uin;

            [JsonProperty("stat")]
            public int Stats;
        }
    }

}