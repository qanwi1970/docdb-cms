using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using DocDB_CMS.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace DocDB_CMS.Repositories
{
    public class DocDBContentRepository : IContentRepository
    {
        public List<Content> GetList()
        {
            var documentsLink = Collection.DocumentsLink;
            var contentList = client.CreateDocumentQuery<Content>(documentsLink).AsEnumerable().ToList();
            return contentList;
        }

        public Content Get(string id)
        {
            //var content =
            var query = client.CreateDocumentQuery<Content>(Collection.DocumentsLink).AsEnumerable().ToList();
            var filtered = query.Where(d => d.Id == id);
            var enumerable = filtered.AsEnumerable();
            var content = enumerable.FirstOrDefault();
            return content;
        }

        public Content Add(Content content)
        {
            var result = Client.CreateDocumentAsync(Collection.SelfLink, content).Result;

            return content;
        }

        public Content Update(string id, Content content)
        {
            var doc =
                Client.CreateDocumentQuery<Document>(Collection.DocumentsLink)
                    .Where(d => d.Id == id)
                    .AsEnumerable()
                    .FirstOrDefault();

            var result = Client.ReplaceDocumentAsync(doc.SelfLink, content);

            return content;
        }

        public void Delete(string id)
        {
            var doc =
                Client.CreateDocumentQuery<Document>(Collection.DocumentsLink)
                    .Where(d => d.Id == id)
                    .AsEnumerable()
                    .FirstOrDefault();

            var result = Client.DeleteDocumentAsync(doc.SelfLink).Result;
        }

        private static Database database;

        private static Database Database
        {
            get
            {
                if (database == null)
                {
                    ReadOrCreateDatabase();
                }

                return database;
            }
        }

        private static DocumentCollection collection;

        private static DocumentCollection Collection
        {
            get
            {
                if (collection == null)
                {
                    ReadOrCreateCollection(Database.SelfLink);
                }

                return collection;
            }
        }

        private static string databaseId;

        private static String DatabaseId
        {
            get
            {
                if (string.IsNullOrEmpty(databaseId))
                {
                    databaseId = "ContentDB"; // ConfigurationManager.AppSettings["database"];
                }

                return databaseId;
            }
        }

        private static string collectionId;

        private static String CollectionId
        {
            get
            {
                if (string.IsNullOrEmpty(collectionId))
                {
                    collectionId = "ContentCollection"; // ConfigurationManager.AppSettings["collection"];
                }

                return collectionId;
            }
        }

        private static DocumentClient client;

        private static DocumentClient Client
        {
            get
            {
                if (client == null)
                {
                    String endpoint = "https://bd-cms.documents.azure.com:443/";
                        // ConfigurationManager.AppSettings["endpoint"];
                    string authKey =
                        "<Don't forget to set this>";
                        // ConfigurationManager.AppSettings["authKey"];
                    Uri endpointUri = new Uri(endpoint);
                    client = new DocumentClient(endpointUri, authKey);
                }
                return client;
            }
        }

        private static void ReadOrCreateCollection(string databaseLink)
        {
            var collections = Client.CreateDocumentCollectionQuery(databaseLink)
                .Where(col => col.Id == CollectionId).ToArray();

            if (collections.Any())
            {
                collection = collections.First();
            }
            else
            {
                collection = Client.CreateDocumentCollectionAsync(databaseLink,
                    new DocumentCollection {Id = CollectionId}).Result;
            }
        }

        private static void ReadOrCreateDatabase()
        {
            var databases = Client.CreateDatabaseQuery()
                .Where(db => db.Id == DatabaseId).ToArray();

            if (databases.Any())
            {
                database = databases.First();
            }
            else
            {
                Database database = new Database {Id = DatabaseId};
                database = Client.CreateDatabaseAsync(database).Result;
            }
        }
    }
}