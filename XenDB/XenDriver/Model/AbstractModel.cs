using System;
using System.Collections.Generic;
using System.Linq;
using XenDB.XenDriver.Model;

namespace XenDriver.Model
{
    public abstract class AbstractModel : IModel
    {

        abstract public string TableName { get; set; }
        public int ID { get; set; }

        abstract public List<Tuple<string, string>> TableParams { get; set; }

        protected AbstractModel() {

        }

        public string getTableParam(string tableParamKey)
        {
            return TableParams.Where(x => x.Item1 == tableParamKey).FirstOrDefault().Item2;
        }

        public Dictionary<string, string> getTableParamDict()
        {
            Dictionary<string, string> paramDict = new Dictionary<string, string>();
            TableParams.ForEach(x =>
            {
                paramDict.Add(x.Item1, x.Item2);
            });
            return paramDict;

        }

    }
}
