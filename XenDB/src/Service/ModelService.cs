using System.Collections.Generic;
using XenDB.Repository;
using XenDB.Model;
using System.Linq;

namespace XenDB.Service {
    public class ModelService<T> where T : AbstractModel, new() {

        public static T GetByID(int objectId) {
            return BaseRepository.SelectByID<T>(objectId);
        }

        public static List<T> GetAll() {
            return BaseRepository.SelectAll<T>();
        }

        public List<T> GetByColumnName(string columnName, string value){
            return BaseRepository.SelectByColumnName<T>(columnName, value);
        }

        public T GetFirstByColumnName(string columnName, string value) {
            return BaseRepository.SelectFirstByColumnName<T>(columnName, value);
        }

        public static void Update(T model) {
            BaseRepository.Update(model);
        }

        public T Insert(T model) {
            return BaseRepository.Insert(model);
        }

        public static T Upsert(T model) {
            return BaseRepository.Upsert(model);
        }
    }
}
