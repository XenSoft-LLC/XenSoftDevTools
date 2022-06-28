using System;
using Xunit;
using XenRipper.TilesetLoader;
using XenRipper.src.Tileset;
using System.Drawing;
using XenRipper.src.Tileset.TilesetLoader;
using Newtonsoft.Json;

namespace XenRipper_Tests {
    public class TilesetLoaderTests {
        [Fact]
        public void ImportTilesetFromJSONReturnsCorrectTilesetObject() {

            Tileset expectedTilset = new Tileset(Image.FromFile(@"F:\Repository\XenSoftDevTools\XenRipper Tests\TestTilesetImages\tileset.png"), 
                new TilesetMetaConfig("PTE_DemoMap", 10, 32, 32, 20), new int[2] { 10, 2 });
            Tileset actualTileset = TilesetLoader.ImportTilesetFromDirectory(@"F:\Repository\XenSoftDevTools\XenRipper Tests\TestTilesetImages\");

            Assert.Equal(JsonConvert.SerializeObject(expectedTilset), JsonConvert.SerializeObject(actualTileset));
            //Check integrity of generated tiles??

        }
    }
}
