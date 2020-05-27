using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Undani.DataClient
{
    public class _ParameterCollection : List<_Parameter>
    {
        public _Parameter this[string parameterName]
        {
            get
            {
                return (_Parameter)GetParameter(parameterName);
            }
            set
            {
                SetParameter(parameterName, value);
            }
        }

        private void SetParameter(string parameterName, _Parameter value)
        {
            int index = IndexOf(parameterName);
            if (index > -1)
            {
                this[index] = value;
            }
        }

        private int IndexOf( string parameterName)
        {
            int i = 0;
            foreach (var parameter in this)
            {
                if (parameter.Name == parameterName)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        private _Parameter GetParameter(string parameterName)
        {
            foreach (var parameter in this)
            {
                if (parameter.Name == parameterName)
                {
                    return parameter;
                }
            }

            return null;
        }

        public _Parameter Add(string parameterName, _ParameterType sqlDbType)
        {
            _Parameter parameter = new _Parameter(parameterName, sqlDbType);
            this.Add(parameter);
            return parameter;
        }

        public _Parameter Add(string parameterName, _ParameterType sqlDbType, int size)
        {
            _Parameter parameter = new _Parameter(parameterName, sqlDbType, size);
            this.Add(parameter);
            return parameter;
        }

        public void AddRange(_Parameter[] values)
        {
            this.AddRange(values);
        }
    }
}
