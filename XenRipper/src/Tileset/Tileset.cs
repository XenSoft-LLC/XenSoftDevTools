using System;
using System.Collections.Generic;
using System.Text;
using XenRipper.src.Tileset.TilesetLoader;

namespace XenRipper.src.Tileset {
    public class Tileset {
        public string Name { get; set; }
        public TilesetMetaConfig TilesetMetaConfig { get; set; }
        public int[] Dimensions { get; set; }
        public Tile[] Tiles { get; set; }
        
        public Tileset(string name, TilesetMetaConfig tilesetMetaConfig, int[] dimensions, Tile[] tiles) {
            Name = name;
            TilesetMetaConfig = tilesetMetaConfig;
            Dimensions = dimensions;
            Tiles = tiles;
        }

    }
}
