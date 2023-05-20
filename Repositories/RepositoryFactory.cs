using Repositories.IRepositories;
using Repositories.MSSQLRepositories;
using Repositories.XMLRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RepositoryFactory
    {
        private string sqlConnStr;
        private string xmlPath;
        public RepositoryFactory(string sql, string xml)
        {
            sqlConnStr = sql;
            xmlPath = xml;
        }

        public IToDoRepository ToDoRepCreate(string dataSrc)
        {
            switch (dataSrc)
            {
                case "SQL":
                default: return new MSSQLToDoRepository(sqlConnStr);
                case "XML": return  new XMLToDoRepository(xmlPath);
            }
        }

        public ICategoryRepository CategoryCreate(string dataSrc)
        {
            switch (dataSrc)
            {
                case "SQL":
                default: return new MSSQLCategoryRepository(sqlConnStr);
                case "XML": return  new XMLCategoryRepository(xmlPath);
            }
        }
    }
}
