using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Text;

namespace XenRipper.TilesetLoader {
    class TilesetLoader {

        //Refactor to use XenRipper
        public Tileset ImportTilesetFromJSON(string name, string dirUrl)
        {

            //Set current directory to target dir
            try
            {
                Directory.SetCurrentDirectory(dirUrl);
            }
            catch (Exception e)
            {
                //Log error in both log file and console
            }


            if (!Directory.GetFiles(dirUrl).Contains("tileset.png"))
            {
                throw new Exception("Target directory does not contain a file named tileset.png");
            }
            if (!Directory.GetFiles(dirUrl).Contains("meta.json"))
            {
                throw new Exception("Target directory does not contain a file named meta.json");
            }

            string imageUrl = scanForFile("tileset.png");
            string metaJsonUrl = scanForFile("meta.json");

            //Tileset should ultimately just have a multi-dimensional array of "Tiles" which give you the name of the tile, and their collision status.
            //Create Tile class
            Tileset newTileSet = new Tileset();


        }

        private string scanForFile() { }
        private Image
        private Dictionary<string, string> importMetaConfig() { }


    }
}
