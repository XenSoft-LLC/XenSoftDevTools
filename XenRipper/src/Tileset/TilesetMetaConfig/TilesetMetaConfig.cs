using System;
using System.Collections.Generic;
using System.Text;

namespace XenRipper.src.Tileset.MetaConfig {
    public class TilesetMetaConfig {
        public string Name { get; set; }
        public int Columns { get; set; }
        public Tile[] Tiles {get; set;}
        public int TileHeight { get; set; }
        public int TileWidth { get; set; }
        public int TileCount { get; set; }

        public TilesetMetaConfig(string name, int columns, Tile[] tiles, int tileHeight, int tileWidth, int tileCount)
        {
            Name = name;
            Columns = columns;
            Tiles = tiles;
            TileHeight = tileHeight;
            TileWidth = tileWidth;
            TileCount = tileCount;
        }
    }
}
