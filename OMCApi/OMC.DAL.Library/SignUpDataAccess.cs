using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMC.Models;
using OMC.DAL.Interface;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace OMC.DAL.Library
{
    public class SignUpDataAccess : DataAccessBase, ISignUpDataAccess
    {
        #region Declaration
        SqlConnection _connection;
        SqlCommand _command;
        #endregion

        #region Constructor
        public SignUpDataAccess()
        {
            this._connection = Connection;
            this._command = Command;
        }
        #endregion

        #region Methods

        public bool InitiateSignUpProcess(UserSignUp signupdetails)
        {
            try
            {
                Command.CommandText = "SP_USER_DETAIL_MANAGER";
                Command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(Command);
                da.SelectCommand.Parameters.Add(new SqlParameter("@USER_DETAIL_XML", SqlDbType.NVarChar, 10000));
                da.SelectCommand.Parameters["@USER_DETAIL_XML"].Value = GetXMLFromObject(signupdetails);
                da.SelectCommand.Parameters.Add(new SqlParameter("@OPERATION", SqlDbType.NVarChar, 100));
                da.SelectCommand.Parameters["@OPERATION"].Value = DBNull.Value;
                da.SelectCommand.Parameters.Add(new SqlParameter("@USER_ID", SqlDbType.BigInt, 100));
                da.SelectCommand.Parameters["@USER_ID"].Value = 1;
                Connection.Open();

                int? result = (int?)Command.ExecuteScalar();

                if (result == null || result == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                Connection.Close();
            }

        }

        #endregion

        #region XML Serialize/De-Serialize

        public static string GetXMLFromObject(object o)
        {
            // removes version
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;

            XmlSerializer xsSubmit = new XmlSerializer(o.GetType());
            //StringWriter sw = new StringWriter();
            try
            {
                using (StringWriter sw = new StringWriter())
                using (XmlWriter writer = XmlWriter.Create(sw, settings))
                {
                    // removes namespace
                    var xmlns = new XmlSerializerNamespaces();
                    xmlns.Add(string.Empty, string.Empty);

                    xsSubmit.Serialize(writer, o, xmlns);
                    return sw.ToString(); // Your XML
                }
            }

            //StringWriter sw = new StringWriter();
            //XmlTextWriter tw = null;
            //try
            //{
            //    XmlSerializer serializer = new XmlSerializer(o.GetType());
            //    tw = new XmlTextWriter(sw);
            //    serializer.Serialize(tw, o);
            //}
            catch (Exception ex)
            {
                //Handle Exception Code
                return null;
            }
            finally
            {
                //sw.Close();
                //if (tw != null)
                //{
                //    tw.Close();
                //}
            }
            //return sw.ToString();
        }


        public static Object ObjectToXML(string xml, Type objectType)
        {
            StringReader strReader = null;
            XmlSerializer serializer = null;
            XmlTextReader xmlReader = null;
            Object obj = null;
            try
            {
                strReader = new StringReader(xml);
                serializer = new XmlSerializer(objectType);
                xmlReader = new XmlTextReader(strReader);
                obj = serializer.Deserialize(xmlReader);
            }
            catch (Exception exp)
            {
                //Handle Exception Code
            }
            finally
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                }
                if (strReader != null)
                {
                    strReader.Close();
                }
            }
            return obj;
        }


        #endregion
    }
}
