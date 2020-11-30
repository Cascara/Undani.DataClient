using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Undani.DataClient
{
    public class _Command : IDisposable
    {
        private SqlCommand _sqlCommand;

        public _Command(string commandText, _Connection connection)
        {
            Parameters = new _ParameterCollection();
            _sqlCommand = new SqlCommand(commandText, connection.SqlConnection);
        }

        public string CommandText
        {
            get
            {
                return _sqlCommand.CommandText;
            }
        }

        public _ParameterCollection Parameters { get; set; }

        public CommandType CommandType
        {
            get
            {
                return _sqlCommand.CommandType;
            }
            set
            {
                _sqlCommand.CommandType = value;
            }
        }

        internal SqlCommand SqlCommand
        {
            get
            {
                _sqlCommand.Parameters.Clear();

                foreach (_Parameter parameter in Parameters)
                {
                    _sqlCommand.Parameters.Add(parameter.SqlParameter);
                }

                return _sqlCommand;
            }
        }

        public void ExecuteNonQuery()
        {
            _sqlCommand.Parameters.Clear();

            foreach (_Parameter parameter in Parameters)
            {
                _sqlCommand.Parameters.Add(parameter.SqlParameter);
            }

            _sqlCommand.ExecuteNonQuery();

            foreach (_Parameter parameter in Parameters)
            {
                Parameters[parameter.Name].Value = _sqlCommand.Parameters[parameter.Name].Value;
            }
        }

        public IDataReader ExecuteReader()
        {
            foreach (_Parameter parameter in Parameters)
            {
                _sqlCommand.Parameters.Add(parameter.SqlParameter);
            }

            return (DbDataReader)_sqlCommand.ExecuteReader();
        }

        public DataTable ExecuteDataTable()
        {
            foreach (_Parameter parameter in Parameters)
            {
                _sqlCommand.Parameters.Add(parameter.SqlParameter);
            }

            SqlDataAdapter da = new SqlDataAdapter(_sqlCommand);

            DataTable dt = new DataTable();

            da.Fill(dt);

            return dt;
        }

        public void Dispose()
        {
            _sqlCommand.Dispose();
        }

    }
}
