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
                else if (doctorProfile.ModifiedBy.HasValue)
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
                else if (userAddress.ModifiedBy.HasValue)
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

        public DoctorAwardsResponse InsertUpdateDoctorAward(DoctorAwards doctorAward, string operation)
        {
            try
            {
                Log.Info("Started call to InsertUpdateDoctorAward");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { doctorAward = doctorAward, operation = operation }));
                Command.CommandText = "SP_DOCTOR_AWARD_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@DOCTOR_AWARD_XML", GetXMLFromObject(doctorAward));
                if (doctorAward.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", doctorAward.AddedBy.Value);
                }
                else if (doctorAward.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", doctorAward.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                DoctorAwardsResponse result = new DoctorAwardsResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new DoctorAwardsResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateDoctorAward");

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

        public DoctorAwardsResponse GetDoctorAwardList(int doctorId, int? doctorAwardId)
        {
            try
            {
                Log.Info("Started call to GetDoctorAwardList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { doctorId = doctorId, doctorAwardId = doctorAwardId }));
                Command.CommandText = "SP_GET_DOCTOR_AWARD_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@DOCTOR_ID", doctorId);
                if (doctorAwardId.HasValue)
                {
                    Command.Parameters.AddWithValue("@DOCTOR_AWARD_ID", doctorAwardId);
                }
                Connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                DoctorAwardsResponse result = new DoctorAwardsResponse();
                result.DoctorAwardsList = new List<DoctorAwardsDisplay>();
                foreach (DataRow drDoctorAward in ds.Tables[0].Rows)
                {
                    result.DoctorAwardsList.Add(new DoctorAwardsDisplay
                    {
                        Id = Convert.ToInt32(drDoctorAward["Id"].ToString()),
                        DoctorId = Convert.ToInt32(drDoctorAward["DoctorId"].ToString()),
                        DoctorName = drDoctorAward["DoctorName"] != DBNull.Value ? drDoctorAward["DoctorName"].ToString() : null,
                        YearReceived = Convert.ToInt32(drDoctorAward["YearReceived"].ToString()),
                        InstitutionName = drDoctorAward["InstitutionName"] != DBNull.Value ? drDoctorAward["InstitutionName"].ToString() : null,
                        AwardName = drDoctorAward["AwardName"] != DBNull.Value ? drDoctorAward["AwardName"].ToString() : null,
                        AddedBy = drDoctorAward["AddedBy"] != DBNull.Value ? Convert.ToInt32(drDoctorAward["AddedBy"].ToString()) : (int?)null,
                        AddedDate = drDoctorAward["AddedDate"] != DBNull.Value ? DateTime.Parse(drDoctorAward["AddedDate"].ToString()) : (DateTime?)null,
                        ModifiedBy = drDoctorAward["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(drDoctorAward["ModifiedBy"].ToString()) : (int?)null,
                        ModifiedDate = drDoctorAward["ModifiedDate"] != DBNull.Value ? DateTime.Parse(drDoctorAward["ModifiedDate"].ToString()) : (DateTime?)null,
                    });
                }
                Log.Info("End call to GetDoctorAwardList result " + JsonConvert.SerializeObject(result));

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

        public DoctorBoardResponse InsertUpdateDoctorBoard(DoctorBoard doctorBoard, string operation)
        {
            try
            {
                Log.Info("Started call to InsertUpdateDoctorBoard");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { doctorBoard = doctorBoard, operation = operation }));
                Command.CommandText = "SP_DOCTOR_BOARD_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@DOCTOR_BOARD_XML", GetXMLFromObject(doctorBoard));
                if (doctorBoard.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", doctorBoard.AddedBy.Value);
                }
                else if (doctorBoard.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", doctorBoard.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                DoctorBoardResponse result = new DoctorBoardResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new DoctorBoardResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateDoctorBoard");

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

        public DoctorBoardResponse GetDoctorBoardList(int doctorId, int? doctorBoardId)
        {
            try
            {
                Log.Info("Started call to GetDoctorBoardList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { doctorId = doctorId, doctorBoardId = doctorBoardId }));
                Command.CommandText = "SP_GET_DOCTOR_BOARD_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@DOCTOR_ID", doctorId);
                if (doctorBoardId.HasValue)
                {
                    Command.Parameters.AddWithValue("@DOCTOR_BOARD_ID", doctorBoardId);
                }
                Connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                DoctorBoardResponse result = new DoctorBoardResponse();
                result.DoctorBoardList = new List<DoctorBoardDisplay>();
                foreach (DataRow drDoctorBoard in ds.Tables[0].Rows)
                {
                    result.DoctorBoardList.Add(new DoctorBoardDisplay
                    {
                        Id = Convert.ToInt32(drDoctorBoard["Id"].ToString()),
                        DoctorId = Convert.ToInt32(drDoctorBoard["DoctorId"].ToString()),
                        DoctorName = drDoctorBoard["DoctorName"] != DBNull.Value ? drDoctorBoard["DoctorName"].ToString() : null,
                        BoardId = Convert.ToInt32(drDoctorBoard["BoardId"].ToString()),
                        BoardName = drDoctorBoard["BoardName"] != DBNull.Value ? drDoctorBoard["BoardName"].ToString() : null,
                        OtherDescription = drDoctorBoard["OtherDescription"] != DBNull.Value ? drDoctorBoard["OtherDescription"].ToString() : null,
                        AddedBy = drDoctorBoard["AddedBy"] != DBNull.Value ? Convert.ToInt32(drDoctorBoard["AddedBy"].ToString()) : (int?)null,
                        AddedDate = drDoctorBoard["AddedDate"] != DBNull.Value ? DateTime.Parse(drDoctorBoard["AddedDate"].ToString()) : (DateTime?)null,
                        ModifiedBy = drDoctorBoard["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(drDoctorBoard["ModifiedBy"].ToString()) : (int?)null,
                        ModifiedDate = drDoctorBoard["ModifiedDate"] != DBNull.Value ? DateTime.Parse(drDoctorBoard["ModifiedDate"].ToString()) : (DateTime?)null,
                    });
                }
                Log.Info("End call to GetDoctorBoardList result " + JsonConvert.SerializeObject(result));

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

        public DoctorEducationResponse InsertUpdateDoctorEducation(DoctorEducation doctorEducation, string operation)
        {
            try
            {
                Log.Info("Started call to InsertUpdateDoctorEducation");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { doctorEducation = doctorEducation, operation = operation }));
                Command.CommandText = "SP_DOCTOR_EDUCATION_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@DOCTOR_EDUCATION_XML", GetXMLFromObject(doctorEducation));
                if (doctorEducation.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", doctorEducation.AddedBy.Value);
                }
                else if (doctorEducation.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", doctorEducation.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                DoctorEducationResponse result = new DoctorEducationResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new DoctorEducationResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateDoctorEducation");

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

        public DoctorEducationResponse GetDoctorEducationList(int doctorId, int? doctorEducationId)
        {
            try
            {
                Log.Info("Started call to GetDoctorEducationList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { doctorId = doctorId, doctorEducationId = doctorEducationId }));
                Command.CommandText = "SP_GET_DOCTOR_EDUCATION_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@DOCTOR_ID", doctorId);
                if (doctorEducationId.HasValue)
                {
                    Command.Parameters.AddWithValue("@DOCTOR_EDUCATION_ID", doctorEducationId);
                }
                Connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                DoctorEducationResponse result = new DoctorEducationResponse();
                result.DoctorEducationList = new List<DoctorEducationDisplay>();
                foreach (DataRow drDoctorEducation in ds.Tables[0].Rows)
                {
                    result.DoctorEducationList.Add(new DoctorEducationDisplay
                    {
                        Id = Convert.ToInt32(drDoctorEducation["Id"].ToString()),
                        DoctorId = Convert.ToInt32(drDoctorEducation["DoctorId"].ToString()),
                        DoctorName = drDoctorEducation["DoctorName"] != DBNull.Value ? drDoctorEducation["DoctorName"].ToString() : null,
                        BeginingYear = Convert.ToInt32(drDoctorEducation["BeginingYear"].ToString()),
                        EndingYear = Convert.ToInt32(drDoctorEducation["EndingYear"].ToString()),
                        CollegeName = drDoctorEducation["CollegeName"] != DBNull.Value ? drDoctorEducation["CollegeName"].ToString() : null,
                        City = drDoctorEducation["City"] != DBNull.Value ? drDoctorEducation["City"].ToString() : null,
                        StateId = Convert.ToInt32(drDoctorEducation["StateId"].ToString()),
                        StateName = drDoctorEducation["StateName"] != DBNull.Value ? drDoctorEducation["StateName"].ToString() : null,
                        CountryId = Convert.ToInt32(drDoctorEducation["CountryId"].ToString()),
                        CountryName = drDoctorEducation["CountryName"] != DBNull.Value ? drDoctorEducation["CountryName"].ToString() : null,
                        AddedBy = drDoctorEducation["AddedBy"] != DBNull.Value ? Convert.ToInt32(drDoctorEducation["AddedBy"].ToString()) : (int?)null,
                        AddedDate = drDoctorEducation["AddedDate"] != DBNull.Value ? DateTime.Parse(drDoctorEducation["AddedDate"].ToString()) : (DateTime?)null,
                        ModifiedBy = drDoctorEducation["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(drDoctorEducation["ModifiedBy"].ToString()) : (int?)null,
                        ModifiedDate = drDoctorEducation["ModifiedDate"] != DBNull.Value ? DateTime.Parse(drDoctorEducation["ModifiedDate"].ToString()) : (DateTime?)null,
                    });
                }
                Log.Info("End call to GetDoctorEducationList result " + JsonConvert.SerializeObject(result));

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

        public DoctorFellowshipResponse InsertUpdateDoctorFellowship(DoctorFellowship doctorFellowship, string operation)
        {
            try
            {
                Log.Info("Started call to InsertUpdateDoctorFellowship");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { doctorFellowship = doctorFellowship, operation = operation }));
                Command.CommandText = "SP_DOCTOR_FELLOWSHIP_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@DOCTOR_FELLOWSHIP_XML", GetXMLFromObject(doctorFellowship));
                if (doctorFellowship.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", doctorFellowship.AddedBy.Value);
                }
                else if (doctorFellowship.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", doctorFellowship.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                DoctorFellowshipResponse result = new DoctorFellowshipResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new DoctorFellowshipResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateDoctorFellowship");

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

        public DoctorFellowshipResponse GetDoctorFellowshipList(int doctorId, int? doctorFellowshipId)
        {
            try
            {
                Log.Info("Started call to GetDoctorFellowshipList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { doctorId = doctorId, doctorFellowshipId = doctorFellowshipId }));
                Command.CommandText = "SP_GET_DOCTOR_FELLOWSHIP_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@DOCTOR_ID", doctorId);
                if (doctorFellowshipId.HasValue)
                {
                    Command.Parameters.AddWithValue("@DOCTOR_FELLOWSHIP_ID", doctorFellowshipId);
                }
                Connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                DoctorFellowshipResponse result = new DoctorFellowshipResponse();
                result.DoctorFellowshipList = new List<DoctorFellowshipDisplay>();
                foreach (DataRow drDoctorFellowship in ds.Tables[0].Rows)
                {
                    result.DoctorFellowshipList.Add(new DoctorFellowshipDisplay
                    {
                        Id = Convert.ToInt32(drDoctorFellowship["Id"].ToString()),
                        DoctorId = Convert.ToInt32(drDoctorFellowship["DoctorId"].ToString()),
                        DoctorName = drDoctorFellowship["DoctorName"] != DBNull.Value ? drDoctorFellowship["DoctorName"].ToString() : null,
                        BeginingYear = Convert.ToInt32(drDoctorFellowship["BeginingYear"].ToString()),
                        EndingYear = Convert.ToInt32(drDoctorFellowship["EndingYear"].ToString()),
                        HospitalName = drDoctorFellowship["HospitalName"] != DBNull.Value ? drDoctorFellowship["HospitalName"].ToString() : null,
                        City = drDoctorFellowship["City"] != DBNull.Value ? drDoctorFellowship["City"].ToString() : null,
                        StateId = Convert.ToInt32(drDoctorFellowship["StateId"].ToString()),
                        StateName = drDoctorFellowship["StateName"] != DBNull.Value ? drDoctorFellowship["StateName"].ToString() : null,
                        CountryId = Convert.ToInt32(drDoctorFellowship["CountryId"].ToString()),
                        CountryName = drDoctorFellowship["CountryName"] != DBNull.Value ? drDoctorFellowship["CountryName"].ToString() : null,
                        AddedBy = drDoctorFellowship["AddedBy"] != DBNull.Value ? Convert.ToInt32(drDoctorFellowship["AddedBy"].ToString()) : (int?)null,
                        AddedDate = drDoctorFellowship["AddedDate"] != DBNull.Value ? DateTime.Parse(drDoctorFellowship["AddedDate"].ToString()) : (DateTime?)null,
                        ModifiedBy = drDoctorFellowship["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(drDoctorFellowship["ModifiedBy"].ToString()) : (int?)null,
                        ModifiedDate = drDoctorFellowship["ModifiedDate"] != DBNull.Value ? DateTime.Parse(drDoctorFellowship["ModifiedDate"].ToString()) : (DateTime?)null,
                    });
                }
                Log.Info("End call to GetDoctorFellowshipList result " + JsonConvert.SerializeObject(result));

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

        public DoctorImagesResponse InsertUpdateDoctorImage(DoctorImages doctorImage, string operation)
        {
            try
            {
                Log.Info("Started call to InsertUpdateDoctorImage");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { doctorImage = doctorImage, operation = operation }));
                Command.CommandText = "SP_DOCTOR_IMAGE_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@DOCTOR_IMAGE_XML", GetXMLFromObject(doctorImage));
                if (doctorImage.FileData != null)
                {
                    Command.Parameters.AddWithValue("@FILE_DATA", doctorImage.FileData);
                }
                if (doctorImage.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", doctorImage.AddedBy.Value);
                }
                else if (doctorImage.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", doctorImage.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                DoctorImagesResponse result = new DoctorImagesResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new DoctorImagesResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateDoctorImage");

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

        public DoctorImagesResponse GetDoctorImageList(int doctorId, int? doctorImageId)
        {
            try
            {
                Log.Info("Started call to GetDoctorImageList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { doctorId = doctorId, doctorImageId = doctorImageId }));
                Command.CommandText = "SP_GET_DOCTOR_IMAGE_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@DOCTOR_ID", doctorId);
                if (doctorImageId.HasValue)
                {
                    Command.Parameters.AddWithValue("@DOCTOR_IMAGE_ID", doctorImageId);
                }
                Connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                DoctorImagesResponse result = new DoctorImagesResponse();
                result.DoctorImagesList = new List<DoctorImageDisplay>();
                foreach (DataRow drDoctorImage in ds.Tables[0].Rows)
                {
                    result.DoctorImagesList.Add(new DoctorImageDisplay
                    {
                        Id = Convert.ToInt32(drDoctorImage["Id"].ToString()),
                        DoctorId = Convert.ToInt32(drDoctorImage["DoctorId"].ToString()),
                        DoctorName = drDoctorImage["DoctorName"] != DBNull.Value ? drDoctorImage["DoctorName"].ToString() : null,
                        IsPrimary = Convert.ToBoolean(drDoctorImage["IsPrimary"].ToString()),
                        FileName = drDoctorImage["FileName"] != DBNull.Value ? drDoctorImage["FileName"].ToString() : null,
                        FileData = drDoctorImage["FileData"] != DBNull.Value ? (byte[])drDoctorImage["FileData"] : null,
                        AddedBy = drDoctorImage["AddedBy"] != DBNull.Value ? Convert.ToInt32(drDoctorImage["AddedBy"].ToString()) : (int?)null,
                        AddedDate = drDoctorImage["AddedDate"] != DBNull.Value ? DateTime.Parse(drDoctorImage["AddedDate"].ToString()) : (DateTime?)null,
                        ModifiedBy = drDoctorImage["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(drDoctorImage["ModifiedBy"].ToString()) : (int?)null,
                        ModifiedDate = drDoctorImage["ModifiedDate"] != DBNull.Value ? DateTime.Parse(drDoctorImage["ModifiedDate"].ToString()) : (DateTime?)null,
                    });
                }
                Log.Info("End call to GetDoctorImageList result " + JsonConvert.SerializeObject(result));

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

        public DoctorResidencyResponse InsertUpdateDoctorResidency(DoctorResidency doctorResidency, string operation)
        {
            try
            {
                Log.Info("Started call to InsertUpdateDoctorResidency");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { doctorResidency = doctorResidency, operation = operation }));
                Command.CommandText = "SP_DOCTOR_RESIDENCY_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@DOCTOR_RESIDENCY_XML", GetXMLFromObject(doctorResidency));
                if (doctorResidency.AddedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", doctorResidency.AddedBy.Value);
                }
                else if (doctorResidency.ModifiedBy.HasValue)
                {
                    Command.Parameters.AddWithValue("@USER_ID", doctorResidency.ModifiedBy.Value);
                }
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                DoctorResidencyResponse result = new DoctorResidencyResponse();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new DoctorResidencyResponse
                        {
                            Message = reader["ReturnMessage"] != DBNull.Value ? reader["ReturnMessage"].ToString() : null,
                            IsSuccess = Convert.ToBoolean(reader["Result"].ToString())
                        };
                    }
                }
                Log.Info("End call to InsertUpdateDoctorResidency");

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

        public DoctorResidencyResponse GetDoctorResidencyList(int doctorId, int? doctorResidencyId)
        {
            try
            {
                Log.Info("Started call to GetDoctorResidencyList");
                Log.Info("parameter values" + JsonConvert.SerializeObject(new { doctorId = doctorId, doctorResidencyId = doctorResidencyId }));
                Command.CommandText = "SP_GET_DOCTOR_RESIDENCY_LIST";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.Clear();

                Command.Parameters.AddWithValue("@DOCTOR_ID", doctorId);
                if (doctorResidencyId.HasValue)
                {
                    Command.Parameters.AddWithValue("@DOCTOR_RESIDENCY_ID", doctorResidencyId);
                }
                Connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(Command);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                DoctorResidencyResponse result = new DoctorResidencyResponse();
                result.DoctorResidencyList = new List<DoctorResidencyDisplay>();
                foreach (DataRow drDoctorResidency in ds.Tables[0].Rows)
                {
                    result.DoctorResidencyList.Add(new DoctorResidencyDisplay
                    {
                        Id = Convert.ToInt32(drDoctorResidency["Id"].ToString()),
                        DoctorId = Convert.ToInt32(drDoctorResidency["DoctorId"].ToString()),
                        DoctorName = drDoctorResidency["DoctorName"] != DBNull.Value ? drDoctorResidency["DoctorName"].ToString() : null,
                        BeginingYear = Convert.ToInt32(drDoctorResidency["BeginingYear"].ToString()),
                        EndingYear = Convert.ToInt32(drDoctorResidency["EndingYear"].ToString()),
                        HospitalName = drDoctorResidency["HospitalName"] != DBNull.Value ? drDoctorResidency["HospitalName"].ToString() : null,
                        City = drDoctorResidency["City"] != DBNull.Value ? drDoctorResidency["City"].ToString() : null,
                        StateId = Convert.ToInt32(drDoctorResidency["StateId"].ToString()),
                        StateName = drDoctorResidency["StateName"] != DBNull.Value ? drDoctorResidency["StateName"].ToString() : null,
                        CountryId = Convert.ToInt32(drDoctorResidency["CountryId"].ToString()),
                        CountryName = drDoctorResidency["CountryName"] != DBNull.Value ? drDoctorResidency["CountryName"].ToString() : null,
                        AddedBy = drDoctorResidency["AddedBy"] != DBNull.Value ? Convert.ToInt32(drDoctorResidency["AddedBy"].ToString()) : (int?)null,
                        AddedDate = drDoctorResidency["AddedDate"] != DBNull.Value ? DateTime.Parse(drDoctorResidency["AddedDate"].ToString()) : (DateTime?)null,
                        ModifiedBy = drDoctorResidency["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(drDoctorResidency["ModifiedBy"].ToString()) : (int?)null,
                        ModifiedDate = drDoctorResidency["ModifiedDate"] != DBNull.Value ? DateTime.Parse(drDoctorResidency["ModifiedDate"].ToString()) : (DateTime?)null,
                    });
                }
                Log.Info("End call to GetDoctorResidencyList result " + JsonConvert.SerializeObject(result));

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
