using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace Undani.DataClient
{
    public class _DataAdapter : DbDataAdapter
    {
        public _DataAdapter(_Command selectCommand)
        {
            SelectCommand = selectCommand.SqlCommand;
        }

    }
}