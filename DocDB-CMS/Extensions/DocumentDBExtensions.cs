using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace DocDB_CMS.Extensions
{
    public static class DocumentDBExtensions
    {
        public static async Task<Database> GetDatabase(this DocumentClient client, string databaseId)
        {
            var databases = client.CreateDatabaseQuery().Where(db => db.Id == databaseId).ToArray();

            if (databases.Any())
            {
                return databases.First();
            }

            return await client.CreateDatabaseAsync(new Database {Id = databaseId});
        }

        public static async Task<DocumentCollection> GetDocumentCollection(this DocumentClient client,
            string databaseId, string collectionId)
        {
            var database = await GetDatabase(client, databaseId);

            var collections = client.CreateDocumentCollectionQuery(database.SelfLink).Where(col => col.Id == collectionId).ToArray();

            if (collections.Any())
            {
                return collections.First();
            }

            return await client.CreateDocumentCollectionAsync(database.SelfLink, new DocumentCollection {Id = collectionId});
        }
    }
}