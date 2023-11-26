using Common.Models;

namespace EventAPI.Services
{
    public interface ISyncService<T> where T : MongoDocument
    {
        HttpResponseMessage Upsert(T record);

        HttpResponseMessage Delete(T record);
    }
}
