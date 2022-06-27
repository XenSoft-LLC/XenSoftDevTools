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
    public class TilesetLoader {
        public string TargetDirectory { get; set; } = "";
        public Tileset ImportTilesetFromJSON(string dirUrl) {
            TilesetMetaConfig tilesetMetaConfig;

            moveToTargetDir(dirUrl);

            using (StreamReader r = new StreamReader("meta.json")) {
                string metaJson = r.ReadToEnd();
                tilesetMetaConfig = JsonConvert.DeserializeObject<TilesetMetaConfig>(metaJson);
            }
            Image tilesetImage = Image.FromFile("tileset.png");
            validateTilesetImageSize(tilesetImage, tilesetMetaConfig);

            int[] tilesetDimensions = new int[2] { tilesetImage.Width / tilesetMetaConfig.TileWidth, tilesetImage.Height / tilesetMetaConfig.TileHeight };

            //Separate image into 32x32px blocks and print them into a folder called "tiles" in the tilesetHomeDir file located in the XenRipperConfig class.
            splitTilesetIntoTiles(tilesetImage, tilesetDimensions, tilesetMetaConfig);

            return new Tileset(tilesetMetaConfig.Name, tilesetMetaConfig, new int[2] { tilesetImage.Width / tilesetMetaConfig.TileWidth, tilesetImage.Height / tilesetMetaConfig.TileHeight }, null);


            //Create Tile object for each tile in the set and include them in a Tile[] array on Tileset
            //Load in Metadata.json to determine which tiles are solid
            //Create a property on Tileset object called bool[] Collision which will be a 2d array equal in size to the Tiles array and will mark wether or not theyre collision tiles.
            //Create a collision.json file in the same folder that the tiles are in, which will be a text representation of the collision data from Metadata.
            //Return the tileset

            //Tileset should ultimately just have a multi-dimensional array of "Tiles" which give you the name of the tile, and their collision status.
            //Create Tile class
        }

        private void splitTilesetIntoTiles(Image tilesetImage, int[] tilesetDimensions, TilesetMetaConfig tilesetMetaConfig) {
            try {
                Directory.CreateDirectory("Tiles");
                splitTilesetImage(tilesetImage, tilesetDimensions[1], true);
                Image[] fileRows = getFileRows();
                foreach(Image file in fileRows) {
                    splitTilesetImage(file, tilesetDimensions[0], false;
                }
            } catch {

            }

        }

        private void splitTilesetImage(Image image, int splitInto, bool horizontal)
        {
            for (int i = 0; i < splitInto; i++)
            {
                Rectangle rect;
                if (horizontal)
                {
                    rect = new Rectangle(0, image.Height / splitInto * i, image.Width, image.Height / splitInto);
                } else {
                    rect = new Rectangle(image.Width / splitInto * i, 0, image.Width / splitInto, image.Height);
                }
                using (Bitmap clonedImage = new Bitmap(image).Clone(rect, new Bitmap(image).PixelFormat)) {
                    if (horizontal){
                        clonedImage.Save($"{Directory.GetCurrentDirectory()}\\Tiles\\TileRow{i + 1}.png");
                    } else {
                        clonedImage.Save($"{Directory.GetCurrentDirectory()}\\Tiles\\Tile{i + 1}.png");
                    }
                }
            }
        }

        private void validateTilesetImageSize(Image tilesetImage, TilesetMetaConfig tilesetMetaConfig)
        {
            if(tilesetImage.Width % tilesetMetaConfig.TileWidth != 0 || tilesetImage.Height % tilesetMetaConfig.TileHeight !=0 ) {
                throw new Exception("Invalid dimensions for tileset image.");
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

            string[] dirFiles = Directory.GetFiles(dirUrl).Select(x => x.Replace(dirUrl, "")).ToArray();

            if (!dirFiles.Contains("tileset.png")) {
                throw new Exception("Target directory does not contain a file named tileset.png");
            }

            if (!dirFiles.Contains("meta.json")) {
                throw new Exception("Target directory does not contain a file named meta.json");
            }
        }

        private Image[] getFileRows()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            List<Image> rowImages = new List<Image>();
            string[] rowFiles = Directory.GetFiles($"{currentDirectory}\\Tiles").Where(x => x.Contains("Row"))
                .Where(x => x.Contains(".png")).Select(x => x.Replace(currentDirectory, "")).ToArray();
            foreach(string imageUrl in rowFiles) {
                rowImages.Add(Image.FromFile($"{currentDirectory}{imageUrl}"));
            }
            return rowImages.ToArray();
        }

        private string scanForFile() { return null; }

        private Dictionary<string, string> importMetaConfig() { return null; }


    }
}
