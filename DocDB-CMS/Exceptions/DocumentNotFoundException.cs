using System;

namespace DocDB_CMS.Exceptions
{
    public class DocumentNotFoundException : Exception
    {
        public DocumentNotFoundException()
        {
        }

        public DocumentNotFoundException(string message) : base(message)
        {
        }
    }
}