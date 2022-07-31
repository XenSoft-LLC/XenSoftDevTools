using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using XenRipper.src.Tileset.MetaConfig;

namespace XenRipper.src.Tileset {
    public class Tileset {
        public Image TilesetImage { get; set; }
        public TilesetMetaConfig TilesetMetaConfig { get; set; }
        public int[] Dimensions { get; set; }

        public Tileset(Image tilesetImage, TilesetMetaConfig tilesetMetaConfig, int[] dimensions)
        {
            TilesetImage = tilesetImage;
            TilesetMetaConfig = tilesetMetaConfig;
            Dimensions = dimensions;
        }

        public string Name
        {
            get { return TilesetMetaConfig.Name; }
        }
    }
}
