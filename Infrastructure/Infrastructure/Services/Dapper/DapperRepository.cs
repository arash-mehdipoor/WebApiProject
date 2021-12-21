using Application.Interfaces.Dapper;
using System.Data.Common;
namespace Infrastructure.Services.Dapper
{


    public class DapperRepository : IDapperRepository
    {
        private readonly string Connectionstring = "Data Source=.;Initial Catalog=WebApiProject;Integrated Security=True;MultipleActiveResultSets=true";

        public DbConnection GetDbconnection()
        {
            return new System.Data.SqlClient.SqlConnection(Connectionstring);
        }
    }
}
