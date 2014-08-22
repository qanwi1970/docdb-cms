using System;

namespace DocDB_CMS.Models
{
    public class Content
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Revision { get; set; }
        public bool Published { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public string ContentType { get; set; }
        public object ContentData { get; set; }
    }
}