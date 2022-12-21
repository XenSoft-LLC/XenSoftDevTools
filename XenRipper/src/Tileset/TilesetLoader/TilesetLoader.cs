using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing; 
using Newtonsoft.Json;
using XenRipper.src.Config;
using XenRipper.src.Tileset.MetaConfig;
using XenRipper.src.Tileset.Validator;
using XenRipper.src.ExceptionManager;

namespace XenRipper.src.Tileset.TilesetLoader {
    public class TilesetLoader {
        private static (TilesetMetaConfig, Image) _getTilesetData(string tilesetName = null)
        {
            TilesetValidator.ValidateHomeDirectory(XenRipperConfig.TilesetHome);
            string basePath = tilesetName == null ? $"{XenRipperConfig.TilesetHome}" : $"{XenRipperConfig.TilesetHome}{tilesetName}/";
            using (StreamReader r = new StreamReader($"{basePath}meta.json"))
            {
                string metaJson = r.ReadToEnd();
                TilesetMetaConfig tilesetMetaConfig = JsonConvert.DeserializeObject<TilesetMetaConfig>(metaJson);
                Image tilesetImage = Image.FromFile($"{basePath}tileset.png");
                return (tilesetMetaConfig, tilesetImage);
            }
        }
        private static void _generateTilesFromTileset(Tileset tileset)
        {
            try
            {
                _splitTilesetImage(tileset, tileset.TilesetImage, tileset.Dimensions[1], true, 0);
                Image[] fileRows = _getFileRows(tileset.Name);
                int rowNumber = 0;
                foreach (Image file in fileRows)
                {
                    _splitTilesetImage(tileset, file, tileset.Dimensions[0], false, rowNumber);
                    rowNumber++;
                    file.Dispose();
                }
                Directory.Delete($"{XenRipperConfig.TilesetHome}{tileset.Name}\\TileRow", true);
            }
            catch (TiledExceptionManager.UnreadableFileException Exception)
            {
                throw Exception;
            }

        }
        private static void _splitTilesetImage(Tileset tileset, Image image, int splitInto, bool horizontal, int rowNumber)
        {
            Rectangle rect = new Rectangle(0, 0, horizontal ? image.Width : image.Width / splitInto, horizontal ? image.Height / splitInto : image.Height);
            string imageDirectory = horizontal ? "TileRow" : "Tile";
            string fileName;

            try
            {
                Directory.CreateDirectory($"{XenRipperConfig.TilesetHome}{tileset.Name}\\{imageDirectory}");
            }
            catch (TiledExceptionManager.CouldntCreateDirectoryException e)
            {
                Console.WriteLine(e);
            }

            for (int i = 0; i < splitInto; i++)
            {
                int imageIndex = i + rowNumber * (tileset.Dimensions[0]);
                fileName = $"{imageDirectory}{imageIndex}.png";

                if (!horizontal)
                {
                    rect.X = image.Width / splitInto * i;
                }
                else
                {
                    rect.Y = image.Height / splitInto * i;
                }

                using (Bitmap clonedImage = new Bitmap(image).Clone(rect, new Bitmap(image).PixelFormat))
                {
                    _printTileImage(tileset.Name, clonedImage, imageDirectory, fileName);
                }
            }
        }
        private static void _printTileImage(string tilesetName, Bitmap image, string subDirectory, string fileName)
        {
            image.Save($"{XenRipperConfig.TilesetHome}{tilesetName}\\{subDirectory}\\{fileName}");
        }
        private static void _copyMetaFiles(string tilesetName)
        {
            string[] filesToCopy = { "\\meta.json", "\\tileset.png" };
            filesToCopy.ToList().ForEach(file => {
                string fileToCopy = $"{XenRipperConfig.TilesetHome}{file}";
                string destinationFile = $"{XenRipperConfig.TilesetHome}{tilesetName}{file}";
                if (!File.Exists(destinationFile))
                {
                    File.Copy(fileToCopy, destinationFile);
                }
            });
        }
        private static Image[] _getFileRows(string tilesetName) {
            List<Image> rowImages = new List<Image>();
            string[] rowFiles = Directory.GetFiles($"{XenRipperConfig.TilesetHome}{tilesetName}\\TileRow").Where(x => x.Contains("Row"))
                .Where(x => x.Contains(".png")).Select(x => x.Replace(XenRipperConfig.TilesetHome, "")).ToArray();
            foreach (string imageUrl in rowFiles)
            {
                rowImages.Add(Image.FromFile($"{XenRipperConfig.TilesetHome}{imageUrl}"));
            }
            return rowImages.ToArray();
        }

        public static Tileset ImportTilesetFromHome() { 
            (TilesetMetaConfig tilesetMetaConfig, Image tilesetImage) = _getTilesetData();
            TilesetValidator.validateTilesetDivisibleByTileSize(tilesetImage, tilesetMetaConfig);

            Tileset newTileset = new Tileset(tilesetImage, tilesetMetaConfig,
                new int[2] { tilesetImage.Width / tilesetMetaConfig.TileWidth, tilesetImage.Height / tilesetMetaConfig.TileHeight });

            if (Directory.Exists(newTileset.Name)) {
                Directory.Delete(newTileset.Name, true); Directory.CreateDirectory(newTileset.Name);
            } else {
                Directory.CreateDirectory(newTileset.Name);
            }

            //Create new Tileset files and migrate meta.json/tileset.png 
            _generateTilesFromTileset(newTileset);
            _copyMetaFiles(newTileset.Name);
            return newTileset;
        }

        public static Tileset LoadTilesetFromHome(string tilesetName)
        {
            (TilesetMetaConfig tilesetMetaConfig, Image tilesetImage) = _getTilesetData(tilesetName);
            return new Tileset(tilesetImage, tilesetMetaConfig,
                new int[2] { tilesetImage.Width / tilesetMetaConfig.TileWidth, tilesetImage.Height / tilesetMetaConfig.TileHeight });
        }
    }
}
