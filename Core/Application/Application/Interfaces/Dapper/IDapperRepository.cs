using System.Data.Common;
using System.Data.SqlClient;

namespace Application.Interfaces.Dapper
{
    public interface IDapperRepository
    {
        #region Connection
        DbConnection GetDbconnection();
        #endregion
    }
}
