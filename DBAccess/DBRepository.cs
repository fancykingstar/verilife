using LSOmni.Common.Util;
using LSRetail.Omni.Domain.DataModel.Base.Retail;
using LSRetail.Omni.Domain.DataModel.Loyalty.Setup;
using LSRetail.Omni.Domain.DataModel.Base.Replication;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DBAccess
{
    public interface IDBRepository
    {
        List<Notification> NotificationsGetByContactId(string contactId);
    }
    public class DBRepository : IDBRepository
    {
        string connectionString = null;
        string navCompanyName = null;

        private string sqlimgfrom = string.Empty;
        private string sqlimgfields = string.Empty;

        private string sqllinkfrom = string.Empty;
        private string sqllinkfields = string.Empty;

        const int IMAGE_TABLEID = 99009063;
        const int LINK_TABLEID = 99009064;

        public DBRepository(string ConnectionString, string CompanyName)
        {
            connectionString = ConnectionString;
            navCompanyName = CompanyName;

            sqlimgfields = "mt.[Code],mt.[Type],mt.[Image Location],mt.[Image Blob],mt.[Description]";
            sqlimgfrom = " FROM [" + navCompanyName + "Retail Image] mt";

            sqllinkfields = "mt.[Image Id],mt.[Display Order],mt.[TableName],mt.[KeyValue],mt.[Description]";
            sqllinkfrom = " FROM [" + navCompanyName + "Retail Image Link] mt";
        }
        public List<Notification> NotificationsGetByContactId(string contactId)
        {
            List<Notification> list = new List<Notification>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT " +
                         "mt.[No_],mt.[Status],mt.[Type],mt.[Code],mt.[Contact No_],mt.[Primary Text],mt.[Secondary Text],mt.[When Display],mt.[Valid From Date],mt.[Valid To Date],mt.[Created],mt.[Created by]," +
                         "nl.[Id],nl.[ContactId],nl.[DateDisplayed],nl.[DeviceId],nl.[DateClosed],nl.[ReplicationCounter],nl.[NotificationStatus] " +
                         "FROM [" + navCompanyName + "Member Notification] mt " +
                         "LEFT OUTER JOIN [" + navCompanyName + "Member Notification Log] nl ON nl.[Id]=mt.[No_] AND nl.[ContactId]=@id " +
                         "WHERE mt.[Status]=1 AND mt.[No_] IN (" +
                         "SELECT mt.[No_] FROM [" + navCompanyName + "Member Notification] mt " +
                         "INNER JOIN [" + navCompanyName + "Member Contact] c ON c.[Contact No_]=mt.[Code] " +
                         "WHERE mt.[Type]=1 AND c.[Contact No_]=@id " +
                         "UNION " +
                         "SELECT mt.[No_] FROM [" + navCompanyName + "Member Notification] mt " +
                         "INNER JOIN [" + navCompanyName + "Member Contact] c ON c.[Account No_]=mt.[Code] " +
                         "WHERE mt.[Type]=0 AND c.[Contact No_]=@id " +
                         "UNION " +
                         "SELECT mt.[No_] FROM [" + navCompanyName + "Member Notification] mt " +
                         "INNER JOIN [" + navCompanyName + "Member Scheme] s on s.[Club Code]=mt.[Code] " +
                         "INNER JOIN [" + navCompanyName + "Member Account] a on a.[Scheme Code]=s.[Code] " +
                         "INNER JOIN [" + navCompanyName + "Member Contact] c ON c.[Account No_]=a.[No_] " +
                         "WHERE mt.[Type]=2 AND c.[Contact No_]=@id " +
                         "UNION " +
                         "SELECT mt.[No_] FROM [" + navCompanyName + "Member Notification] mt " +
                         "INNER JOIN [" + navCompanyName + "Member Account] a on a.[Scheme Code]=mt.[Code] " +
                         "INNER JOIN [" + navCompanyName + "Member Contact] c ON c.[Account No_]=a.[No_] " +
                         "WHERE mt.[Type]=3 AND c.[Contact No_]=@id) " +
                         "ORDER BY mt.[Created] DESC";

                    command.Parameters.AddWithValue("@id", contactId);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(ReaderToNotification(reader));
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
            }
            return list;
        }

        private Notification ReaderToNotification(SqlDataReader reader)
        {
            Notification noti = new Notification()
            {
                ContactId = SQLHelper.GetString(reader["ContactId"]),
                Created = SQLHelper.GetDateTime(reader["Valid From Date"]),
                Description = SQLHelper.GetString(reader["Primary Text"]),
                Details = SQLHelper.GetString(reader["Secondary Text"]),
                ExpiryDate = SQLHelper.GetDateTime(reader["Valid To Date"]),
                Id = SQLHelper.GetString(reader["No_"]),
                NotificationType = (NotificationType)SQLHelper.GetInt32(reader["Type"]),
                Status = (NotificationStatus)SQLHelper.GetInt32(reader["Status"])
            };

            //ImageRepository imgrep = new ImageRepository();
            noti.Images = ImageGetByKey("Member Notification", noti.Id, string.Empty, string.Empty, 0, false);
            return noti;
        }

        public List<ImageView> ImageGetByKey(string tableName, string key1, string key2, string key3, int imgCount, bool includeBlob)
        {
            try
            {
                List<ImageView> list = new List<ImageView>();
                string sqlcnt = string.Empty;
                if (imgCount > 0)
                    sqlcnt = " TOP(" + imgCount.ToString() + ") ";

                string sql = "SELECT " + sqlcnt + "mt.[Type],mt.[Image Location],il.[Image Id],il.[Display Order]" +
                            ((includeBlob) ? ",mt.[Image Blob]" : string.Empty) +
                             sqlimgfrom + " INNER JOIN [" + navCompanyName + "Retail Image Link] il ON mt.[Code]=il.[Image Id]" +
                             " WHERE il.[KeyValue]=@key AND il.[TableName]=@table " +
                             " ORDER BY il.[Display Order]";

                string keyvalue = key1;
                if (string.IsNullOrWhiteSpace(key2) == false)
                    keyvalue += "," + key2;
                if (string.IsNullOrWhiteSpace(key3) == false)
                    keyvalue += "," + key3;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        command.Parameters.AddWithValue("@key", keyvalue);
                        command.Parameters.AddWithValue("@table", tableName);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(ReaderToImage(reader, includeBlob));
                            }
                            reader.Close();
                        }
                        connection.Close();
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ImageView ReaderToImage(SqlDataReader reader, bool includeblob)
        {
            ImageView view = new ImageView()
            {
                Id = SQLHelper.GetString(reader["Image Id"]),
                DisplayOrder = SQLHelper.GetInt32(reader["Display Order"]),
                Location = SQLHelper.GetString(reader["Image Location"]),
                LocationType = (LocationType)SQLHelper.GetInt32(reader["Type"])
            };

           // if (includeblob)
            //    view.ImgBytes = ImageConverter.NAVUnCompressImage(reader["Image Blob"] as byte[]);
            return view;
        }

    }

}
