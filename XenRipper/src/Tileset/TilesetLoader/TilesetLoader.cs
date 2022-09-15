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
    public static class TilesetLoader {
        public static Tileset ImportTilesetFromDirectory(string targetDirectory) {

            //Move to Target Directory
            moveToTargetDirectory(targetDirectory);

            //Import + Validate integrity of tileset.png/meta.json
            TilesetMetaConfig tilesetMetaConfig = readMetaJSON();
            Image tilesetImage = Image.FromFile("tileset.png");
            TilesetValidator.validateTilesetDivisibleByTileSize(tilesetImage, tilesetMetaConfig);

            //Generate Tileset object from meta.json and tileset.png
            Tileset newTileset = new Tileset(tilesetImage, tilesetMetaConfig,
                new int[2] { tilesetImage.Width / tilesetMetaConfig.TileWidth, tilesetImage.Height / tilesetMetaConfig.TileHeight });

            createTilesetDirectory(newTileset.Name);

            //Create new Tileset files and migrate meta.json/tileset.png 
            generateTilesFromTileset(newTileset);
            copyMetaFiles(newTileset.Name, targetDirectory);
            return newTileset;
        }

        public static Tileset LoadTilesetFromHardDrive(string tilesetName){

            moveToTargetDirectory(getTilesetHomeDirectory());

            try {
                Directory.SetCurrentDirectory($"{getTilesetHomeDirectory()}\\{tilesetName}");
            } catch(TiledExceptionManager.MissingDirectoryException e) {
                Console.WriteLine(e);
                return null;
            }

            TilesetMetaConfig tilesetMetaConfig;
            //TODO: Find out what exception this would be and log a better message 
            try {
                tilesetMetaConfig = readMetaJSON();
            } catch (TiledExceptionManager.UnreadableFileException e){
                Console.WriteLine(e);
                return null;
            }

            Image tilesetImage;

            try {
                tilesetImage = Image.FromFile("tileset.png");
            } catch (TiledExceptionManager.UnreadableFileException e) {
                Console.WriteLine(e);
                return null;
            }

            return new Tileset(tilesetImage, tilesetMetaConfig,
                new int[2] { tilesetImage.Width / tilesetMetaConfig.TileWidth, tilesetImage.Height / tilesetMetaConfig.TileHeight });

        }

        private static TilesetMetaConfig readMetaJSON()
        {
            using (StreamReader r = new StreamReader("meta.json"))
            {
                string metaJson = r.ReadToEnd();
                return JsonConvert.DeserializeObject<TilesetMetaConfig>(metaJson);
            }
        }

        private static void generateTilesFromTileset(Tileset tileset) {
            try{
                splitTilesetImage(tileset, tileset.TilesetImage, tileset.Dimensions[1], true, 0);
                Image[] fileRows = getFileRows(tileset.Name);
                int rowNumber = 0;
                foreach(Image file in fileRows) {
                    splitTilesetImage(tileset, file, tileset.Dimensions[0], false, rowNumber);
                    rowNumber++;
                    file.Dispose();
                }
                Directory.Delete($"{Directory.GetCurrentDirectory()}\\{tileset.Name}\\TileRow", true);
            } catch(TiledExceptionManager.UnreadableFileException Exception) {
                throw Exception;
            }

        }

        private static void splitTilesetImage(Tileset tileset, Image image, int splitInto, bool horizontal, int rowNumber)
        {
            Rectangle rect;
            string imageDirectory;
            string fileName;

            if (horizontal) {
                imageDirectory = "TileRow";
            } else {
                imageDirectory = "Tile";
            }

            try {
                Directory.CreateDirectory($"{tileset.Name}\\{imageDirectory}");
            } catch(TiledExceptionManager.CouldntCreateDirectoryException e) {
                Console.WriteLine(e);
            }

            for (int i = 0; i < splitInto; i++) {
                if (horizontal) {
                    rect = new Rectangle(0, image.Height / splitInto * i, image.Width, image.Height / splitInto);
                } else {
                    rect = new Rectangle(image.Width / splitInto * i, 0, image.Width / splitInto, image.Height);
                }
                int imageIndex = i + rowNumber * (tileset.Dimensions[0]);

                fileName = $"{imageDirectory}{imageIndex}.png";
                using (Bitmap clonedImage = new Bitmap(image).Clone(rect, new Bitmap(image).PixelFormat)) {
                    printTileImage(tileset.Name, clonedImage, imageDirectory, fileName);
                }
            }
        }

        private static void createTilesetDirectory(string directoryName)
        {
            try {
                Directory.Delete(directoryName, true);
                Directory.CreateDirectory(directoryName);
            } catch (TiledExceptionManager.CouldntCreateDirectoryException e) {
                Directory.CreateDirectory(directoryName);
                Console.WriteLine(e);
            }
        }

        private static void printTileImage(string tilesetName, Bitmap image, string subDirectory, string fileName){

            string parentDirectoy;
            if(XenRipperConfig.TilesetHome != ""){
                parentDirectoy = XenRipperConfig.TilesetHome;
            } else {
                parentDirectoy = Directory.GetCurrentDirectory();
            }

            
            image.Save($"{parentDirectoy}\\{tilesetName}\\{subDirectory}\\{fileName}");
        }

        private static void copyMetaFiles(string tilesetName, string dirUrl)
        {
            string parentDirectory = getTilesetHomeDirectory();

            string fileToCopy = $"{dirUrl}\\meta.json";
            string destinationFile = $"{parentDirectory}\\{tilesetName}\\meta.json";
            File.Copy(fileToCopy, destinationFile);

            fileToCopy = $"{dirUrl}\\tileset.png";
            destinationFile = $"{parentDirectory}\\{tilesetName}\\tileset.png";
            File.Copy(fileToCopy, destinationFile);

        }

        private static string getTilesetHomeDirectory()
        {
            string parentDirectory;
            if (XenRipperConfig.TilesetHome != "")
            {
                parentDirectory = XenRipperConfig.TilesetHome;
            }
            else
            {
                parentDirectory = Directory.GetCurrentDirectory();
            }
            return parentDirectory;
        }

        //Move into TilesetValidator class
        private static void moveToTargetDirectory(string targetDirectory)
        {
            try {
                Directory.SetCurrentDirectory(targetDirectory);
            }
            catch (TiledExceptionManager.MissingDirectoryException e) {
                //Log error in both log file and console
                Console.WriteLine(e);
            }

            string[] dirFiles = Directory.GetFiles(targetDirectory).Select(x => x.Replace(targetDirectory, "")).ToArray();

            if (!dirFiles.Contains("tileset.png")) {
                throw new TiledExceptionManager.UnreadableFileException("Target directory does not contain a file named tileset.png");
            }

            if (!dirFiles.Contains("meta.json")) {
                throw new TiledExceptionManager.UnreadableFileException("Target directory does not contain a file named meta.json");
            }
        }

        private static Image[] getFileRows(string tilesetName)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            List<Image> rowImages = new List<Image>();
            string[] rowFiles = Directory.GetFiles($"{currentDirectory}\\{tilesetName}\\TileRow").Where(x => x.Contains("Row"))
                .Where(x => x.Contains(".png")).Select(x => x.Replace(currentDirectory, "")).ToArray();
            foreach(string imageUrl in rowFiles) {
                rowImages.Add(Image.FromFile($"{currentDirectory}{imageUrl}"));
            }
            return rowImages.ToArray();
        }

    }
}
