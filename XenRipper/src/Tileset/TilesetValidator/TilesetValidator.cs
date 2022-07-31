using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using XenRipper.src.Config;
using XenRipper.src.Tileset.MetaConfig;

namespace XenRipper.src.Tileset.Validator {
    public static class TilesetValidator {

        //Check meta.json and validate that it has the properties [Name, Columns, TileHeight, TileWidth, TileCount, Tiles]
        public static void  validateMetaJSONHasRequiredProperties(TilesetMetaConfig tilesetMetaConfig) {
            if (
                    tilesetMetaConfig.Name == "" 
                    & tilesetMetaConfig.TileCount == 0
                    & tilesetMetaConfig.TileHeight == 0
                    & tilesetMetaConfig.TileWidth == 0
                    & (tilesetMetaConfig.Tiles == null || tilesetMetaConfig.Tiles.Length == 0)
                ){
                    throw new Exception("Invalid properties or property values in meta.json file.");
                }
        }

        public static void validateNumberOfTilesPrintedIsEqualToTileCount(TilesetMetaConfig tilesetMetaConfig) {
            Directory.SetCurrentDirectory(XenRipperConfig.TilesetHome);

            string tileImageUrl = $"{Directory.GetCurrentDirectory()}\\{tilesetMetaConfig.Name}\\Tile";
            int actualCount = Directory.GetFiles(tileImageUrl)
                .Select(x => x.Replace(Directory.GetCurrentDirectory(), "")).ToArray().Length;
            if(actualCount != tilesetMetaConfig.TileCount){
                throw new Exception("Incorrect number of files in Tile folder.");
            }

        }
        
        public static void validateAllPrintedTilesAreCorrectDimensions(TilesetMetaConfig tilesetMetaConfig) {
            Directory.SetCurrentDirectory(XenRipperConfig.TilesetHome);

            string tileImageUrl = $"{Directory.GetCurrentDirectory()}\\{tilesetMetaConfig.Name}\\Tile";
            string[] imageUrls = Directory.GetFiles(tileImageUrl)
                .Select(x => x.Replace(Directory.GetCurrentDirectory(), "")).ToArray();
            for(int i=0; i > imageUrls.Length; i++){
                Image currentTile = Image.FromFile($"{tileImageUrl}\\{imageUrls[i]}");
                if(currentTile.Width != tilesetMetaConfig.TileWidth || currentTile.Height != tilesetMetaConfig.TileHeight){
                    throw new Exception($"Tile {i} had the wrong dimensions");
                } 
            }
            
        }

        public static void validateTilesetDivisibleByTileSize(Image tilesetImage, TilesetMetaConfig tilesetMetaConfig)
        {
            if (tilesetImage.Width % tilesetMetaConfig.TileWidth != 0 || tilesetImage.Height % tilesetMetaConfig.TileHeight != 0)
            {
                throw new Exception("Invalid dimensions for tileset image.");
            }
        }
    }
}
