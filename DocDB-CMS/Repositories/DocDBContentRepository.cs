using System;
using System.Collections.Generic;
using System.Linq;
using DocDB_CMS.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace DocDB_CMS.Repositories
{
    public class DocDBContentRepository : IContentRepository
    {
        private static string endpointUrl = "https://bd-cms.documents.azure.com:443/";
        private static string authKey = "<fill in later>";

        public List<Content> GetList()
        {
            var client = new DocumentClient(new Uri(endpointUrl), authKey);
            Database database = client.CreateDatabaseAsync(new Database {Id = "ContentDB"}).Result;
            DocumentCollection collection = client.CreateDocumentCollectionAsync(database.SelfLink,
                new DocumentCollection {Id = "ContentCollection"}).Result;
            var contentList = client.CreateDocumentQuery<Content>(collection.DocumentsLink).ToList();
            return contentList;
        }

        public Content Get(long id)
        {
            throw new NotImplementedException();
        }

        public Content Add(Content content)
        {
            throw new NotImplementedException();
        }

        public Content Update(long id, Content content)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}