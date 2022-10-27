using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XenUtils.JsonIO {
    public class JsonIO<T> {
        //TODO: Create class that stores default filePath locations for all media types

        public T loadObject(string filePath)
        {
            using (StreamReader r = File.OpenText(filePath))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        public void saveObject(T savedObject, string filePath){
            string json = JsonConvert.SerializeObject(savedObject);
            File.WriteAllText(@filePath, json);
        }
    }
}
