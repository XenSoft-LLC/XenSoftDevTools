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
    public class TilesetLoaderTests {
        [Fact]
        public void ImportTilesetFromJSONReturnsCorrectTilesetObject() {
            Tile[] expectedArray = ImportTilesetFromJSONArray.Tiles;
            Tileset expectedTilset = new Tileset(Image.FromFile(@"F:\Repository\XenSoftDevTools\XenRipper Tests\TestTilesetImages\tileset.png"), 
                new TilesetMetaConfig("PTE_DemoMap", 10, expectedArray, 32, 32, 20), new int[2] { 10, 2 });
            Tileset actualTileset = TilesetLoader.ImportTilesetFromDirectory(@"F:\Repository\XenSoftDevTools\XenRipper Tests\TestTilesetImages\");

            Assert.Equal(JsonConvert.SerializeObject(expectedTilset), JsonConvert.SerializeObject(actualTileset));
        }

/*        [Fact]
        public void LoadTilesetFromJSONReturnsCorrectTilesetObject()
        {
            Tile[] expectedArray = new Tile[] {
                new Tile(null, 0,
                    new TileProperties() {
                        new TileProperty("canFly", "bool", "false"),
                        new TileProperty("canSwim", "bool", "false"),
                        new TileProperty("canTunnel", "bool", "false"),
                        new TileProperty("canWalk", "bool", "true"),
                    }
                ),
                new Tile(null, 1,
                    new TileProperties() {
                        new TileProperty("canFly", "bool", "false"),
                        new TileProperty("canSwim", "bool", "false"),
                        new TileProperty("canTunnel", "bool", "false"),
                        new TileProperty("canWalk", "bool", "true"),
                    }
                ),
                new Tile(null, 2,
                   new TileProperties() {
                        new TileProperty("canFly", "bool", "false"),
                        new TileProperty("canSwim", "bool", "false"),
                        new TileProperty("canTunnel", "bool", "false"),
                        new TileProperty("canWalk", "bool", "true"),
                    }
                ),
                new Tile(null, 3,
                    new TileProperties() {
                        new TileProperty("canFly", "bool", "false"),
                        new TileProperty("canSwim", "bool", "false"),
                        new TileProperty("canTunnel", "bool", "false"),
                        new TileProperty("canWalk", "bool", "true"),
                    }
                ),
                new Tile(null, 4,
                    new TileProperties() {
                        new TileProperty("canFly", "bool", "false"),
                        new TileProperty("canSwim", "bool", "false"),
                        new TileProperty("canTunnel", "bool", "false"),
                        new TileProperty("canWalk", "bool", "true"),
                    }
                ),
                new Tile(null, 5,
                    new TileProperties() {
                        new TileProperty("canFly", "bool", "false"),
                        new TileProperty("canSwim", "bool", "false"),
                        new TileProperty("canTunnel", "bool", "false"),
                        new TileProperty("canWalk", "bool", "true"),
                    }
                ),
                new Tile(null, 6,
                    new TileProperties() {
                        new TileProperty("canFly", "bool", "false"),
                        new TileProperty("canSwim", "bool", "false"),
                        new TileProperty("canTunnel", "bool", "false"),
                        new TileProperty("canWalk", "bool", "true"),
                    }
                ),
                new Tile(null, 7,
                    new TileProperties() {
                        new TileProperty("canFly", "bool", "false"),
                        new TileProperty("canSwim", "bool", "false"),
                        new TileProperty("canTunnel", "bool", "false"),
                        new TileProperty("canWalk", "bool", "true"),
                    }
                ),
                new Tile(null, 8,
                    new TileProperties() {
                        new TileProperty("canFly", "bool", "false"),
                        new TileProperty("canSwim", "bool", "false"),
                        new TileProperty("canTunnel", "bool", "false"),
                        new TileProperty("canWalk", "bool", "false"),
                    }
                ),
                new Tile(null, 9,
                    new TileProperties() {
                        new TileProperty("canFly", "bool", "false"),
                        new TileProperty("canSwim", "bool", "false"),
                        new TileProperty("canTunnel", "bool", "false"),
                        new TileProperty("canWalk", "bool", "true"),
                    }
                ),
                new Tile(null, 10,
                   new TileProperties() {
                        new TileProperty("canFly", "bool", "false"),
                        new TileProperty("canSwim", "bool", "false"),
                        new TileProperty("canTunnel", "bool", "false"),
                        new TileProperty("canWalk", "bool", "true"),
                    }
                ),
                new Tile(null, 11,
                    new TileProperties() {
                        new TileProperty("canFly", "bool", "false"),
                        new TileProperty("canSwim", "bool", "false"),
                        new TileProperty("canTunnel", "bool", "false"),
                        new TileProperty("canWalk", "bool", "true"),
                    }
                ),
                new Tile(null, 12,
                    new TileProperties() {
                        new TileProperty("canFly", "bool", "false"),
                        new TileProperty("canSwim", "bool", "false"),
                        new TileProperty("canTunnel", "bool", "false"),
                        new TileProperty("canWalk", "bool", "true"),
                    }
                ),
                new Tile(null, 13,
                    new TileProperties() {
                        new TileProperty("canFly", "bool", "false"),
                        new TileProperty("canSwim", "bool", "false"),
                        new TileProperty("canTunnel", "bool", "false"),
                        new TileProperty("canWalk", "bool", "true"),
                    }
                ),
                new Tile(null, 14,
                    new TileProperties() {
                        new TileProperty("canFly", "bool", "false"),
                        new TileProperty("canSwim", "bool", "false"),
                        new TileProperty("canTunnel", "bool", "false"),
                        new TileProperty("canWalk", "bool", "true"),
                    }
                ),
                new Tile(null, 15,
                    new TileProperties() {
                        new TileProperty("canFly", "bool", "false"),
                        new TileProperty("canSwim", "bool", "false"),
                        new TileProperty("canTunnel", "bool", "false"),
                        new TileProperty("canWalk", "bool", "true"),
                    }
                ),
                new Tile(null, 16,
                    new TileProperties() {
                        new TileProperty("canFly", "bool", "false"),
                        new TileProperty("canSwim", "bool", "false"),
                        new TileProperty("canTunnel", "bool", "false"),
                        new TileProperty("canWalk", "bool", "true"),
                    }
                ),
                new Tile(null, 17,
                    new TileProperties() {
                        new TileProperty("canFly", "bool", "false"),
                        new TileProperty("canSwim", "bool", "false"),
                        new TileProperty("canTunnel", "bool", "false"),
                        new TileProperty("canWalk", "bool", "true"),
                    }
                ),
                 new Tile(null, 18,
                    new TileProperties() {
                        new TileProperty("canFly", "bool", "false"),
                        new TileProperty("canSwim", "bool", "false"),
                        new TileProperty("canTunnel", "bool", "false"),
                        new TileProperty("canWalk", "bool", "false"),
                    }
                ),
                  new Tile(null, 19,
                    new TileProperties() {
                        new TileProperty("canFly", "bool", "false"),
                        new TileProperty("canSwim", "bool", "false"),
                        new TileProperty("canTunnel", "bool", "false"),
                        new TileProperty("canWalk", "bool", "false"),
                    }
                )
            };
            Tileset expectedTilset = new Tileset(Image.FromFile(@"F:\Repository\XenSoftDevTools\XenRipper Tests\TestTilesetImages\tileset.png"),
                new TilesetMetaConfig("PTE_DemoMap", 10, expectedArray, 32, 32, 20), new int[2] { 10, 2 });
            Tileset actualTileset = TilesetLoader.ImportTilesetFromDirectory(@"F:\Repository\XenSoftDevTools\XenRipper Tests\TestTilesetImages\");

            Assert.Equal(JsonConvert.SerializeObject(expectedTilset), JsonConvert.SerializeObject(actualTileset));
        }*/
    }
}
