using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Undani.DataClient.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string cns = "Server=karakcloud.ddns.net\\sqlexpress01;Initial catalog=Tracking;Persist Security Info=False;User Id=sa;Password=AsdfgH45-4;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True;Connection Timeout=60;";

            Reader(cns);

            Console.WriteLine("Termino DataReader");

            Table(cns);

            Console.WriteLine("Termino Tabla");

            Parameters(cns);

            Console.WriteLine("Termino DataReader");
        }

        public static void Reader(string cns)
        {
            using (_Connection cn = new _Connection(cns))
            {
                cn.Open();

                using (_Command cmd = new _Command("CUSTOM.usp_Get_Empleados", cn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new _Parameter("@UserId", _ParameterType.UniqueIdentifier) { Value = Guid.Parse("9B101EE0-2120-475E-8721-C8A82B0EBB3A") });
                    cmd.Parameters.Add(new _Parameter("@RolId", _ParameterType.Int) { Value = 1 });

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Id: " + reader.GetInt32(0).ToString());
                            Console.WriteLine("Nombre: " + reader.GetString(1));
                            Console.WriteLine("Apellidos: " + reader.GetString(2));
                            Console.WriteLine("UserName: " + reader.GetString(3));
                            Console.WriteLine("Reference: " + reader.GetString(4));
                            Console.WriteLine("UserId: " + reader.GetGuid(5).ToString());
                        }
                    }
                }
            }
        }

        public static void Table(string cns)
        {
            using (_Connection cn = new _Connection(cns))
            {
                cn.Open();

                using (_Command cmd = new _Command("CUSTOM.usp_Get_Empleados", cn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new _Parameter("@UserId", _ParameterType.UniqueIdentifier) { Value = Guid.Parse("9B101EE0-2120-475E-8721-C8A82B0EBB3A") });
                    cmd.Parameters.Add(new _Parameter("@RolId", _ParameterType.Int) { Value = 1 });

                    using (_DataAdapter da = new _DataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        foreach (DataRow dr in dt.Rows)
                        {
                            Console.WriteLine("Id: " + dr["Id"].ToString());
                            Console.WriteLine("Nombre: " + dr["GivenName"].ToString());
                            Console.WriteLine("Apellidos: " + dr["FamilyName"].ToString());
                            Console.WriteLine("UserName: " + dr["UserName"].ToString());
                            Console.WriteLine("Reference: " + dr["Reference"].ToString());
                            Console.WriteLine("UserId: " + dr["UserId"].ToString());
                        }
                    }
                }

            }
        }

        private static void Parameters(string cns)
        {
            using (_Connection cn = new _Connection(cns))
            {
                cn.Open();

                using (_Command cmd = new _Command("CUSTOM.usp_Get_Empleado", cn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new _Parameter("@UserId", _ParameterType.UniqueIdentifier) { Value = Guid.Parse("9B101EE0-2120-475E-8721-C8A82B0EBB3A") });
                    cmd.Parameters.Add(new _Parameter("@Id", _ParameterType.Int) { Value = 1 });
                    cmd.Parameters.Add(new _Parameter("@Nombre", _ParameterType.VarChar, 100) { Direction = ParameterDirection.Output });
                    cmd.Parameters.Add(new _Parameter("@Apellidos", _ParameterType.VarChar, 100) { Direction = ParameterDirection.Output });
                    cmd.Parameters.Add(new _Parameter("@UserName", _ParameterType.VarChar, 256) { Direction = ParameterDirection.Output });
                    cmd.Parameters.Add(new _Parameter("@Reference", _ParameterType.VarChar, 13) { Direction = ParameterDirection.Output });
                    cmd.Parameters.Add(new _Parameter("@EmpleadoUserId", _ParameterType.UniqueIdentifier) { Direction = ParameterDirection.Output });

                    cmd.ExecuteNonQuery();

                    Console.WriteLine("@Nombre: " + (string)cmd.Parameters["@Nombre"].Value);
                    Console.WriteLine("@Apellidos: " + (string)cmd.Parameters["@Apellidos"].Value);
                    Console.WriteLine("@UserName: " + (string)cmd.Parameters["@UserName"].Value);
                    Console.WriteLine("@Reference: " + (string)cmd.Parameters["@Reference"].Value);
                    Console.WriteLine("@EmpleadoUserId: " + (string)cmd.Parameters["@EmpleadoUserId"].Value.ToString());
                }

            }
        }
    }
}
