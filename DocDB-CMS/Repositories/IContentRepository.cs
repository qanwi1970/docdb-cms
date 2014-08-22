using System.Collections.Generic;
using DocDB_CMS.Models;

namespace DocDB_CMS.Repositories
{
    public interface IContentRepository
    {
        List<Content> GetList();
        Content Get(string id);
        Content Add(Content content);
        Content Update(string id, Content content);
        void Delete(string id);
    }
}
