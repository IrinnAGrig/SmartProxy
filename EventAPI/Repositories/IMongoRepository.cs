using Common.Models;

namespace EventAPI.Repositories
{
    public interface IMongoRepository<T> where T : MongoDocument
    {
        List<T> GetAllRecords();

        T InsertRecord (T record);  

        T GetRecordById (Guid id);

        void UpsertRecord( T record);
        void DeleteRecord (Guid id);

    }
}
