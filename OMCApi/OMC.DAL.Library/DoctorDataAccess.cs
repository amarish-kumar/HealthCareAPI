using Newtonsoft.Json;
using OMC.DAL.Interface;
using OMC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace OMC.DAL.Library
{
    public class DoctorDataAccess : DataAccessBase, IDoctorDataAccess
    {
        #region Declaration
        #endregion

        #region Methods

        public DoctorProfileResponse InsertUpdateDoctorProfile(DoctorProfile doctorProfile, string operation)
        {
            try
            {
                Log.Info("Started call to InsertUpdateDoctorProfile");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { doctorProfile = doctorProfile, operation = operation }));
                Command.CommandText = "SP_DOCTOR_PROFILE_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@DOCTOR_PROFILE_XML", GetXMLFromObject(doctorProfile));
                if (!string.IsNullOrEmpty(operation))
                {
                    Command.Parameters.AddWithValue("@OPERATION", operation);
                }
                if (doctorProfile.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", doctorProfile.AddedBy.Value);
                }
                if (doctorProfile.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", doctorProfile.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                DoctorProfileResponse result = new DoctorProfileResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new DoctorProfileResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateDoctorProfile");

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

        public List<DoctorProfileDisplay> GetDoctorProfileList(int userId, int? doctorId)
        {
            try
            {
                Log.Info("Started call to GetDoctorProfileList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { userId = userId, doctorId = doctorId }));
                Command.CommandText = "SP_GET_DOCTOR_PROFILE_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@USER_ID", userId);
                Command.Parameters.AddWithValue("@DOCTOR_ID", doctorId);
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                List<DoctorProfileDisplay> result = new List<DoctorProfileDisplay>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new DoctorProfileDisplay
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            DoctorId = Convert.ToInt32(reader["DoctorId"].ToString()),
                            IsPublished = Convert.ToBoolean(reader["IsPublished"].ToString()),
                            EmailAddress1 = reader["EmailAddress1"] != DBNull.Value ? reader["EmailAddress1"].ToString() : null,
                            EmailAddress2 = reader["EmailAddress2"] != DBNull.Value ? reader["EmailAddress2"].ToString() : null,
                            EmailAddress3 = reader["EmailAddress3"] != DBNull.Value ? reader["EmailAddress3"].ToString() : null,
                            PhoneNumber1 = reader["PhoneNumber1"] != DBNull.Value ? reader["PhoneNumber1"].ToString() : null,
                            PhoneNumber2 = reader["PhoneNumber2"] != DBNull.Value ? reader["PhoneNumber2"].ToString() : null,
                            PhoneNumber3 = reader["PhoneNumber3"] != DBNull.Value ? reader["PhoneNumber3"].ToString() : null,
                            IsEmailAddress1Default = Convert.ToBoolean(reader["IsEmailAddress1Default"].ToString()),
                            IsEmailAddress2Default = Convert.ToBoolean(reader["IsEmailAddress2Default"].ToString()),
                            IsEmailAddress3Default = Convert.ToBoolean(reader["IsEmailAddress3Default"].ToString()),
                            IsPhoneNumber1Default = Convert.ToBoolean(reader["IsPhoneNumber1Default"].ToString()),
                            IsPhoneNumber2Default = Convert.ToBoolean(reader["IsPhoneNumber2Default"].ToString()),
                            IsPhoneNumber3Default = Convert.ToBoolean(reader["IsPhoneNumber3Default"].ToString()),
                            DefaultAddressId = reader["DefaultAddressId"] != DBNull.Value ? Convert.ToInt32(reader["DefaultAddressId"].ToString()) : (int?)null,
                            WebsiteAddress = reader["WebsiteAddress"] != DBNull.Value ? reader["WebsiteAddress"].ToString() : null,
                            TimezoneId = reader["TimezoneId"] != DBNull.Value ? Convert.ToInt32(reader["TimezoneId"].ToString()) : (int?)null,
                            TimezoneDescription = reader["TimezoneDescription"] != DBNull.Value ? reader["TimezoneDescription"].ToString() : null,
                            DoctorName = reader["DoctorName"] != DBNull.Value ? reader["DoctorName"].ToString() : null,
                            Active = Convert.ToBoolean(reader["Active"].ToString()),
                        });
                    }
                }
                Log.Info("End call to GetDoctorProfileList result " + JsonConvert.SerializeObject(result));

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

        public DoctorProfileResponse DeleteDoctorProfile(int doctorProfileId, int userId)
        {
            try
            {
                Log.Info("Started call to DeleteDoctorProfile");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { doctorProfileId = doctorProfileId, userId = userId }));
                Command.CommandText = "SP_DOCTOR_PROFILE_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();
                var doctorProfile = new DoctorProfile { Id = doctorProfileId, DeletedBy = userId };
                Command.Parameters.AddWithValue("@DOCTOR_PROFILE_XML", GetXMLFromObject(doctorProfile));
                Command.Parameters.AddWithValue("@OPERATION", "SOFT_DELETE");
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                DoctorProfileResponse result = new DoctorProfileResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new DoctorProfileResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to DeleteDoctorProfile");

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

        public UserAddressResponse InsertUpdateUserAddress(UserAddress userAddress, string operation)
        {
            try
            {
                Log.Info("Started call to InsertUpdateUserAddress");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { userAddress = userAddress, operation = operation }));
                Command.CommandText = "SP_ADDRESS_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@USER_ADDRESS_XML", GetXMLFromObject(userAddress));
                if (!string.IsNullOrEmpty(operation))
                {
                    Command.Parameters.AddWithValue("@OPERATION", operation);
                }
                if (userAddress.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", userAddress.AddedBy.Value);
                }
                if (userAddress.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", userAddress.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                UserAddressResponse result = new UserAddressResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new UserAddressResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateUserAddress");

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

        public UserAddressResponse GetUserAddressList(int userId, int? addressId)
        {
            try
            {
                Log.Info("Started call to GetUserAddressList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { userId = userId, addressId = addressId }));
                Command.CommandText = "SP_GET_ADDRESS_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@USER_ID", userId);
                if (addressId.HasValue)
                {
                    Command.Parameters.AddWithValue("@ADDRESS_ID", addressId);
                }
                Connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                UserAddressResponse result = new UserAddressResponse();
                result.UserAddressList = new List<UserAddressDisplay>();
                foreach (DataRow drUserAddress in ds.Tables[0].Rows)
                {
                    result.UserAddressList.Add(new UserAddressDisplay
                    {
                        Id = Convert.ToInt32(drUserAddress["Id"].ToString()),
                        UserId = Convert.ToInt32(drUserAddress["UserId"].ToString()),
                        AddressTypeId = Convert.ToInt32(drUserAddress["AddressTypeId"].ToString()),
                        AddressType = drUserAddress["AddressType"] != DBNull.Value ? drUserAddress["AddressType"].ToString() : null,
                        CountryId = Convert.ToInt32(drUserAddress["CountryId"].ToString()),
                        Address1 = drUserAddress["Address1"] != DBNull.Value ? drUserAddress["Address1"].ToString() : null,
                        Address2 = drUserAddress["Address2"] != DBNull.Value ? drUserAddress["Address2"].ToString() : null,
                        ZipCode = drUserAddress["ZipCode"] != DBNull.Value ? drUserAddress["ZipCode"].ToString() : null,
                        City = drUserAddress["City"] != DBNull.Value ? drUserAddress["City"].ToString() : null,
                        StateId = drUserAddress["StateId"] != DBNull.Value ? drUserAddress["StateId"].ToString() : null,
                        StateName = drUserAddress["StateName"] != DBNull.Value ? drUserAddress["StateName"].ToString() : null,
                        CountryName = drUserAddress["CountryName"] != DBNull.Value ? drUserAddress["CountryName"].ToString() : null,
                        AddedBy = drUserAddress["AddedBy"] != DBNull.Value ? Convert.ToInt32(drUserAddress["AddedBy"].ToString()) : (int?)null,
                        AddedDate = drUserAddress["AddedDate"] != DBNull.Value ? DateTime.Parse(drUserAddress["AddedDate"].ToString()) : (DateTime?)null,
                        ModifiedBy = drUserAddress["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(drUserAddress["ModifiedBy"].ToString()) : (int?)null,
                        ModifiedDate = drUserAddress["ModifiedDate"] != DBNull.Value ? DateTime.Parse(drUserAddress["ModifiedDate"].ToString()) : (DateTime?)null,
                        
                    });
                }
                Log.Info("End call to GetUserAddressList result " + JsonConvert.SerializeObject(result));

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
