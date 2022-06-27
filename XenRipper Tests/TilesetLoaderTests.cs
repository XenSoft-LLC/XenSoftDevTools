using System;
using Xunit;
using XenRipper.TilesetLoader;
using XenRipper.src.Tileset;

namespace XenRipper_Tests {
    public class TilesetLoaderTests {
        [Fact]
        public void ImportTilesetFromJSON() {
            TilesetLoader tilesetLoader = new TilesetLoader();

            Assert.Equal(new Tileset(null, null, null, null), tilesetLoader.ImportTilesetFromJSON(@"F:\Repository\XenSoftDevTools\XenRipper Tests\TestTilesetImages\"));

        }
    }
}
