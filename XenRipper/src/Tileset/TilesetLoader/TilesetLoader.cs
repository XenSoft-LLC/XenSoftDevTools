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
        public Tileset ImportTilesetFromDirectory(string dirUrl) {
            TilesetMetaConfig tilesetMetaConfig;

            moveToTargetDir(dirUrl);

            using (StreamReader r = new StreamReader("meta.json")) {
                string metaJson = r.ReadToEnd();
                tilesetMetaConfig = JsonConvert.DeserializeObject<TilesetMetaConfig>(metaJson);
            }
            Image tilesetImage = Image.FromFile("tileset.png");
            validateTilesetImageSize(tilesetImage, tilesetMetaConfig);

            //Separate image into 32x32px blocks and print them into a folder called "tiles" in the tilesetHomeDir file located in the XenRipperConfig class.
            Tileset newTileset = new Tileset(tilesetImage, tilesetMetaConfig,
                new int[2] { tilesetImage.Width / tilesetMetaConfig.TileWidth, tilesetImage.Height / tilesetMetaConfig.TileHeight });
            splitTilesetIntoTiles(newTileset);
            return newTileset;
        }

        private void splitTilesetIntoTiles(Tileset tileset) {
            try {
                Directory.CreateDirectory("Tiles");
                splitTilesetImage(tileset, tileset.TilesetImage, tileset.Dimensions[1], true, 0);
                Image[] fileRows = getFileRows();
                int rowNumber = 0;
                foreach(Image file in fileRows) {
                    splitTilesetImage(tileset, file, tileset.Dimensions[0], false, rowNumber);
                    rowNumber++;
                }
            } catch {
                throw new Exception("Error: Could not split tiles correctly");
            }

        }

        private void splitTilesetImage(Tileset tileset, Image image, int splitInto, bool horizontal, int rowNumber)
        {
            for (int i = 0; i < splitInto; i++) {
                Rectangle rect;
                string pngName;
                if (horizontal) { 
                    rect = new Rectangle(0, image.Height / splitInto * i, image.Width, image.Height / splitInto);
                    pngName = "TileRow";
                } else {
                    rect = new Rectangle(image.Width / splitInto * i, 0, image.Width / splitInto, image.Height);
                    pngName = "Tile";
                }
                using (Bitmap clonedImage = new Bitmap(image).Clone(rect, new Bitmap(image).PixelFormat)) {
                    clonedImage.Save($"{Directory.GetCurrentDirectory()}\\Tiles\\{pngName}{i + rowNumber*(tileset.Dimensions[0])}.png");
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
