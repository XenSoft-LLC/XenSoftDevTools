using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XenRipper.src.Tileset;
using System.Drawing;
using Newtonsoft.Json.Linq;
using XenRipper.src.Tileset.TilesetLoader;
using Newtonsoft.Json;

namespace XenRipper.TilesetLoader {
    class TilesetLoader {

        //Refactor to use XenRipper
        public Tileset ImportTilesetFromJSON(string dirUrl) {
            TilesetMetaConfig tilesetMetaConfig;

            moveToTargetDir(dirUrl);

            using (StreamReader r = new StreamReader("meta.json")) {
                string metaJson = r.ReadToEnd();
                tilesetMetaConfig = JsonConvert.DeserializeObject<TilesetMetaConfig>("meta.json");
            }
            Image tilesetImage = Image.FromFile("tileset.png");
            validateTilesetImageSize(tilesetImage, tilesetMetaConfig);

            //Separate image into 32x32px blocks and print them into a folder called "tiles" in the tilesetHomeDir file located in the XenRipperConfig class.
            //Create Tile object for each tile in the set and include them in a Tile[] array on Tileset
            //Load in Metadata.json to determine which tiles are solid
            //Create a property on Tileset object called bool[] Collision which will be a 2d array equal in size to the Tiles array and will mark wether or not theyre collision tiles.
            //Create a collision.json file in the same folder that the tiles are in, which will be a text representation of the collision data from Metadata.
            //Return the tileset

            //Tileset should ultimately just have a multi-dimensional array of "Tiles" which give you the name of the tile, and their collision status.
            //Create Tile class
            Tileset newTileSet = new Tileset(name, null);

            return null;
        }

        private void validateTilesetImageSize(Image tilesetImage, TilesetMetaConfig tilesetMetaConfig)
        {
            if(tilesetImage.Width % tilesetMetaConfig.TileWidth != 0 || tilesetImage.Height % tilesetMetaConfig.TileHeight !=0 ) {
                throw new Exception("Invalid dimensions for tileset image.")
            }
        }

        private void moveToTargetDir(string dirUrl)
        {
            try {
                Directory.SetCurrentDirectory(dirUrl);
            }
            catch (Exception e) {
                //Log error in both log file and console
                Console.WriteLine(e);
            }


            if (!Directory.GetFiles(dirUrl).Contains("tileset.png")) {
                throw new Exception("Target directory does not contain a file named tileset.png");
            }

            if (!Directory.GetFiles(dirUrl).Contains("meta.json")) {
                throw new Exception("Target directory does not contain a file named meta.json");
            }
        }

        private string scanForFile() { return null; }

        private Dictionary<string, string> importMetaConfig() { return null; }


    }
}
