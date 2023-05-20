using GraphQL;
using Repositories.IRepositories;
using Repositories.MSSQLRepositories;
using Repositories.XMLRepositories;

namespace ToDoList.Services
{
    public class RepositoryService
    {
        private string sqlConnStr;
        private string xmlPath;

        private MSSQLToDoRepository mssqlToDoRep;
        private MSSQLCategoryRepository mssqlCategoryRep;

        private XMLToDoRepository xmlToDoRep;
        private XMLCategoryRepository xmlCategoryRep;

        public RepositoryService(string sql, string xml)
        {
            sqlConnStr = sql;
            xmlPath = xml;
        }

        public IToDoRepository ToDoRepository(IResolveFieldContext context)
        {
            try
            {
                var httpContextAccessor = context.RequestServices.GetRequiredService<IHttpContextAccessor>();
                var dbType = httpContextAccessor.HttpContext.Request.Headers["Data-Source"];

                switch (dbType)
                {
                    case "SQL":
                        {
                            if (mssqlToDoRep == null)
                                mssqlToDoRep = new MSSQLToDoRepository(sqlConnStr);
                            return mssqlToDoRep;
                        }
                    case "XML":
                        {
                            if (xmlToDoRep == null)
                                xmlToDoRep = new XMLToDoRepository(xmlPath);
                            return xmlToDoRep;
                        }
                    default: throw new ArgumentException("Wrong data source");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICategoryRepository CategoryRepository(IResolveFieldContext context)
        {
            var httpContextAccessor = context.RequestServices.GetRequiredService<IHttpContextAccessor>();
            var dbType = httpContextAccessor.HttpContext.Request.Headers["Data-Source"];

            switch (dbType)
            {
                case "SQL":
                    {
                        if (mssqlCategoryRep == null)
                            mssqlCategoryRep = new MSSQLCategoryRepository(sqlConnStr);
                        return mssqlCategoryRep;
                    }
                case "XML":
                    {
                        if (xmlCategoryRep == null)
                            xmlCategoryRep = new XMLCategoryRepository(xmlPath);
                        return xmlCategoryRep;
                    }
                default: throw new ArgumentException("Wrong data sourse");
            }
        }
    }
}
