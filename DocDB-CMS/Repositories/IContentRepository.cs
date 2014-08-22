using System.Collections.Generic;
using DocDB_CMS.Models;

namespace DocDB_CMS.Repositories
{
    public interface IContentRepository
    {
        List<Content> GetList();
        Content Get(long id);
        Content Add(Content content);
        Content Update(long id, Content content);
        void Delete(long id);
    }
}
