using System;
using System.Collections.Generic;
using System.Text;

namespace XenRipper.src.Tileset {
    public class Tile {
        public string TilesetName { get; set; }
        public int TileIndex { get; set; }
        public bool CanWalk { get; set; }
/*        public bool CanSwim { get; set; }
        public bool CanFly { get; set; }*/

        public Tile(string tilesetName, int tileIndex, bool canWalk) {
            TilesetName = tilesetName;
            TileIndex = tileIndex;
            CanWalk = canWalk;
/*            CanSwim = canSwim;
            CanFly = canFly;*/
        }
    }
}
