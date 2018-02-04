using Newtonsoft.Json;
using OMC.DAL.Interface;
using OMC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace OMC.DAL.Library
{
    public class MasterDataAccess : DataAccessBase, IMasterDataAccess
    {
        #region Declaration
        #endregion

        #region Methods
        public List<Role> GetRoles(bool? isActive, string roleName)
        {
            try
            {
                Log.Info("Started call to GetRoles");
                Log.Info("parameter values isActive=" + JsonConvert.SerializeObject(new { isActive = isActive, roleDescription = roleName }));
                Command.CommandText = "SP_GET_ROLES";
                Command.CommandType = CommandType.StoredProcedure;

                Command.Parameters.Clear();
                if (!string.IsNullOrEmpty(roleName))
                {
                    Command.Parameters.AddWithValue("@ROLE_NAME", roleName);
                }
                Command.Parameters.AddWithValue("@ACTIVE", isActive);

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                List<Role> result = new List<Role>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new Role
                        {
                            RoleName = reader["RoleName"] != DBNull.Value ? reader["RoleName"].ToString() : null,
                            Id = Convert.ToInt32(reader["Id"].ToString())
                        });
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                Connection.Close();
            }
        }
        #endregion
    }
}
