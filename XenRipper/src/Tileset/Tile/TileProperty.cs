using System.Collections;
#pragma warning disable IDE1006 // Naming Styles
namespace XenRipper.src.Tileset {
    public class TileProperty {

        public string name { get; set; }
        public string type { get; set; }
        public bool value { get; set; }

        public TileProperty(string _name, string _type, string _value)
        {
            name = _name;
            type = _type;
            value = bool.Parse(_value);
        }


    }
}
#pragma warning restore IDE1006 // Naming Styles