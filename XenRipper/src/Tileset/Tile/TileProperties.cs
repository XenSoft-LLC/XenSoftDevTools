using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace XenRipper.src.Tileset {
    [JsonArray]
    public class TileProperties: ArrayList {
        List<TileProperty> Properties { get; set; }
    }
}