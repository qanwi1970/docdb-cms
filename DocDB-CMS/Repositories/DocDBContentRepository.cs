using System;
using System.Collections.Generic;
using System.Linq;
using DocDB_CMS.Exceptions;
using DocDB_CMS.Extensions;
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
            var contentList = _client.CreateDocumentQuery<Content>(documentsLink).AsEnumerable().ToList();
            return contentList;
        }

        public Content Get(string id)
        {
            //var content =
            var query = _client.CreateDocumentQuery<Content>(Collection.DocumentsLink).AsEnumerable().ToList();
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

            if (doc == null)
            {
                throw new DocumentNotFoundException("Document with Id " + id + " not found.");
            }

            var result = Client.ReplaceDocumentAsync(doc.SelfLink, content);

            return content;
        }

        public async void Delete(string id)
        {
            var doc =
                Client.CreateDocumentQuery<Document>(Collection.DocumentsLink)
                    .Where(d => d.Id == id)
                    .AsEnumerable()
                    .FirstOrDefault();

            if (doc == null)
            {
                throw new DocumentNotFoundException("Document with Id " + id + " not found.");
            }

            await Client.DeleteDocumentAsync(doc.SelfLink);
        }

        private static readonly DocumentCollection Collection =
            Client.GetDocumentCollection(DatabaseId, CollectionId).Result;

        private const string DatabaseId = "ContentDB";

        private const string CollectionId = "ContentCollection";

        private static DocumentClient _client;

        private static DocumentClient Client
        {
            get
            {
                if (_client == null)
                {
                    String endpoint = "https://bd-cms.documents.azure.com:443/";
                        // ConfigurationManager.AppSettings["endpoint"];
                    string authKey =
                        "zTvR2YODODGU6GrvS/aOxmF8LWpvDPWaL25iUF8b1JDglBq4bcnB1TafDvjKOfPJhD/tKftfMdr1i1r3fMDZuw==";
                        // ConfigurationManager.AppSettings["authKey"];
                    Uri endpointUri = new Uri(endpoint);
                    _client = new DocumentClient(endpointUri, authKey);
                }
                return _client;
            }
        }
    }
}