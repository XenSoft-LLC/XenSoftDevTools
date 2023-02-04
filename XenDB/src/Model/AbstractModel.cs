namespace XenDB.Model {
    public abstract class AbstractModel
    {
        public abstract string TableName { get; }
        public int ID { get; set; }

        public AbstractModel()  { }
    }
}
