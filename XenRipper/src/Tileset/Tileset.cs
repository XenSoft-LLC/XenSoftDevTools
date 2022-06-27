using System;
using System.Collections.Generic;
using System.Text;

namespace XenRipper.src.Tileset {
    class Tileset {
        public string Name { get; set; }
        public int[] Dimensions { get; set; }
        
        public Tileset(string name, int[] dimensions) {
            Name = name;
            Dimensions = dimensions;
        }

    }
}
