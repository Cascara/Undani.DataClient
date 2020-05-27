using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace Undani.DataClient
{
    public class _Parameter
    {
        private SqlParameter _sqlParameter;
        private _ParameterType _parameterType;

        public _Parameter(string name, _ParameterType type, int size)
        {
            SqlDbType sqlDbType = SqlDbType.VarChar;
            switch (type)
            {
                case _ParameterType.Bit:
                    sqlDbType = SqlDbType.Bit;
                    break;
                case _ParameterType.Char:
                    sqlDbType = SqlDbType.Char;
                    break;
                case _ParameterType.Decimal:
                    sqlDbType = SqlDbType.Decimal;
                    break;
                case _ParameterType.Date:
                    sqlDbType = SqlDbType.Date;
                    break;
                case _ParameterType.DateTime:
                    sqlDbType = SqlDbType.DateTime;
                    break;
                case _ParameterType.Int:
                    sqlDbType = SqlDbType.Int;
                    break;
                case _ParameterType.Time:
                    sqlDbType = SqlDbType.Time;
                    break;
                case _ParameterType.UniqueIdentifier:
                    sqlDbType = SqlDbType.UniqueIdentifier;
                    break;
                case _ParameterType.VarChar:
                    break;
                default:
                    throw new Exception("ParameterType unasigned.");
            }

            Type = type;
            _sqlParameter = new SqlParameter(name, sqlDbType, size);
        }

        public _Parameter(string name, _ParameterType type) : this(name, type, 0) { }

        public string Name
        {
            get
            {
                return _sqlParameter.ParameterName;
            }
        }

        public _ParameterType Type { get; }

        public int Size
        {
            get
            {
                return _sqlParameter.Size;
            }
        }

        public object Value
        {
            get { return _sqlParameter.Value; }
            set { _sqlParameter.Value = value; }
        }

        public ParameterDirection Direction
        {
            get { return _sqlParameter.Direction; }
            set { _sqlParameter.Direction = value; }
        }

        public SqlParameter SqlParameter
        {
            get { return _sqlParameter; }
        }

    }
}
