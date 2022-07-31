using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XenRipper.src.Tileset {
    public class Tile {
        public string TilesetName { get; set; }
        public int ID { get; set; }
        public TileProperties Properties { get; set; }
        /*        public bool CanSwim { get; set; }
                public bool CanFly { get; set; }*/

        public Tile(string tilesetName, int Id, TileProperties properties) {
            TilesetName = tilesetName;
            ID = Id;
            Properties = properties;
/*          CanSwim = canSwim;
            CanFly = canFly;*/
        }
    }
}
