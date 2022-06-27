using System;
using System.Collections.Generic;
using System.Text;

namespace XenRipper.src.Tileset {
    public class Tile {
        public string TileSetName { get; set; }
        public int TileIndex { get; set; }
        public int Name { get; set; }
        public bool CanWalk { get; set; }
        public bool CanSwim { get; set; }
        public bool CanFly { get; set; }
    }
}
