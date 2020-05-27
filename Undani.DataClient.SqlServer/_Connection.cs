using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace Undani.DataClient
{
    public class _Connection : IDisposable
    {
        private bool _isOpen = false;
        private SqlConnection _sqlConnection;

        public _Connection(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
        }

        public string ConnectionString
        {
            get {
                return _sqlConnection.ConnectionString;
            }
        }

        public void Open()
        {            
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    if (!_isOpen)
                    {
                        _sqlConnection.Open();
                        _isOpen = true;
                    }
                }
                catch (Exception ex)
                {
                    if (i == 3)
                    {
                        throw ex;
                    }
                    else
                    {
                        Thread.Sleep(3000);
                    }
                }
            }
        }

        public void Close()
        {
            _sqlConnection.Close();
        }

        internal SqlConnection SqlConnection
        {
            get {
                if (_isOpen)
                {
                    return _sqlConnection;
                }
                else
                {
                    throw new Exception("The connection is not open.");
                }                
            }
        }

        public void Dispose()
        {
            Close();
            _sqlConnection.Dispose();
        }

    }
}
