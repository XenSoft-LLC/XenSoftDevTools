using System;
using Xunit;
using XenRipper.src.Tileset;
using System.Drawing;
using XenRipper.src.Tileset.TilesetLoader;
using Newtonsoft.Json;
using XenRipper.src.Tileset.MetaConfig;
using System.Collections.Generic;
using XenRipper_Tests.TestTileArrayJSONs;

namespace XenRipper_Tests {
    public class TilesetLoaderFixture : ICollectionFixture<TilesetLoaderFixture> { }

    [CollectionDefinition("TilesetLoaderTests")]
    public class TileLoaderTestsCollection : ICollectionFixture<TilesetLoaderFixture>{ }

    [Collection("TilesetLoaderTests")]
    public class ImportTilesetFromJSONReturnsCorrectTilesetObject {
        [Fact]
        public void TestImportTilesetFromJSONReturnsCorrectTilesetObject()
        {
            Tileset expectedTileset = new Tileset(Image.FromFile(@"F:\Repository\XenSoftDevTools\XenRipper Tests\TestTilesetImages\tileset.png"),
                new TilesetMetaConfig("PTE_DemoMap", 10, TestJSONTileData.Tiles, 32, 32, 20), new int[2] { 10, 2 });
            Tileset actualTileset = TilesetLoader.ImportTilesetFromHome();

            Assert.Equal(JsonConvert.SerializeObject(expectedTileset), JsonConvert.SerializeObject(actualTileset));
        }
    }

    [Collection("TilesetLoaderTests")]
    public class LoadTilesetFromHomeReturnsCorrectTilesetObject {
        [Fact]
        public void TestLoadTilesetFromHomeReturnsCorrectTilesetObject()
        {
            Tile[] expectedArray = LoadTilesetFromHome.TestTiles;
            Tileset expectedTileset = new Tileset(Image.FromFile("F:\\Repository\\XenSoftDevTools\\XenRipper Tests\\TestTilesetImages\\tileset.png"),
                new TilesetMetaConfig("PTE_DemoMap", 10, expectedArray, 32, 32, 20), new int[2] { 10, 2 });
            Tileset actualTileset = TilesetLoader.LoadTilesetFromHome("PTE_DemoMap");

            Assert.Equal(JsonConvert.SerializeObject(expectedTileset), JsonConvert.SerializeObject(actualTileset));
        }
    }
};
