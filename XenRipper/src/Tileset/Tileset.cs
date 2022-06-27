using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using XenRipper.src.Tileset.TilesetLoader;

namespace XenRipper.src.Tileset {
    public class Tileset {
        public string Name { get; set; }
        public Image TilesetImage { get; set; }
        public TilesetMetaConfig TilesetMetaConfig { get; set; }
        public int[] Dimensions { get; set; }
        public Tile[] Tiles { get; set; }

        public Tileset(string name, Image tilesetImage, TilesetMetaConfig tilesetMetaConfig, int[] dimensions, Tile[] tiles) {
            Name = name;
            TilesetImage = tilesetImage;
            TilesetMetaConfig = tilesetMetaConfig;
            Dimensions = dimensions;
            Tiles = tiles;
        }

        public Tileset(Image tilesetImage, TilesetMetaConfig tilesetMetaConfig, int[] dimensions)
        {
            Name = tilesetMetaConfig.Name;
            TilesetImage = tilesetImage;
            TilesetMetaConfig = tilesetMetaConfig;
            Dimensions = dimensions;
            Tiles = new Tile[tilesetMetaConfig.TileCount];
        }

    }
}
