using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DocDB_CMS.Models;
using DocDB_CMS.Repositories;

namespace DocDB_CMS.Controllers
{
    public class ContentController : ApiController
    {
        private readonly IContentRepository _repo = new DocDBContentRepository();

        // GET: api/Content
        public IEnumerable<Content> Get()
        {
            return _repo.GetList();
        }

        // GET: api/Content/5
        public Content Get(long id)
        {
            return _repo.Get(id);
        }

        // POST: api/Content
        public Content Post([FromBody]Content value)
        {
            return _repo.Add(value);
        }

        // PUT: api/Content/5
        public Content Put(long id, [FromBody]Content value)
        {
            return _repo.Update(id, value);
        }

        // DELETE: api/Content/5
        public void Delete(long id)
        {
            _repo.Delete(id);
        }
    }
}
