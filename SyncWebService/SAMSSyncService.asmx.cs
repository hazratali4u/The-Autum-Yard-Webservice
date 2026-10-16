using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

namespace SyncWebService
{
    /// <summary>
    /// Summary description for SAMSSyncService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SAMSSyncService : System.Web.Services.WebService
    {

        [WebMethod]
        public DataTable GetServerData(DateTime p_LASTUPDATE_DATE, int p_TYPE, int p_DISTRIBUTOR_ID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "uspGetServerSyncData";
            cmd.Parameters.AddWithValue("@LASTUPDATE_DATE", p_LASTUPDATE_DATE);
            cmd.Parameters.AddWithValue("@TYPE", p_TYPE);
            cmd.Parameters.AddWithValue("@DISTRIBUTOR_ID", p_DISTRIBUTOR_ID);
            cmd.Connection = dbConnection.con;
            dbConnection.CheckConnectionState();
            dbConnection.con.Open();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dtServerData = new DataTable();
            adp.Fill(dtServerData);
            dtServerData.TableName = "dtServerData";
            return dtServerData;
        }

        [WebMethod]
        public DateTime GetServerLastUpdateDateTime(int p_TYPE, int p_DISTRIBUTOR_ID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "uspGetServerLastUpdateDateTimeForSync";
            cmd.Parameters.AddWithValue("@TYPE", p_TYPE);
            cmd.Parameters.AddWithValue("@DISTRIBUTOR_ID", p_DISTRIBUTOR_ID);
            cmd.Connection = dbConnection.con;
            dbConnection.CheckConnectionState();
            dbConnection.con.Open();
            object obj = cmd.ExecuteScalar();
            if (obj == DBNull.Value)
            {
                return DateTime.MinValue;
            }
            else
            {
                return Convert.ToDateTime(obj);
            }
        }

        [WebMethod]
        public bool InsertSaleInvoiceMasterServer(DataTable dtParams)
        {
            if (dtParams.Rows.Count > 0)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spInsertSALE_INVOICE_MASTERServer";
                cmd.Parameters.AddWithValue("@PAYMENT_MODE_ID", dtParams.Rows[0]["PAYMENT_MODE_ID"]);
                cmd.Parameters.AddWithValue("@CUSTOMER_TYPE_ID", dtParams.Rows[0]["CUSTOMER_TYPE_ID"]);
                cmd.Parameters.AddWithValue("@TABLE_ID", dtParams.Rows[0]["TABLE_ID"]);
                cmd.Parameters.AddWithValue("@AMOUNTDUE", dtParams.Rows[0]["AMOUNTDUE"]);
                cmd.Parameters.AddWithValue("@PAIDIN", dtParams.Rows[0]["PAIDIN"]);
                cmd.Parameters.AddWithValue("@BALANCE", dtParams.Rows[0]["BALANCE"]);
                cmd.Parameters.AddWithValue("@IS_HOLD", dtParams.Rows[0]["IS_HOLD"]);
                cmd.Parameters.AddWithValue("@USER_ID", dtParams.Rows[0]["USER_ID"]);
                cmd.Parameters.AddWithValue("@DOCUMENT_DATE", dtParams.Rows[0]["DOCUMENT_DATE"]);
                cmd.Parameters.AddWithValue("@DISTRIBUTOR_ID", dtParams.Rows[0]["DISTRIBUTOR_ID"]);
                cmd.Parameters.AddWithValue("@orderBookerId", dtParams.Rows[0]["orderBookerId"]);
                cmd.Parameters.AddWithValue("@approvedby", dtParams.Rows[0]["APPROVED_BY"]);
                cmd.Parameters.AddWithValue("@LASTUPDATE_DATE", dtParams.Rows[0]["LASTUPDATE_DATE"]);
                cmd.Parameters.AddWithValue("@SALE_INVOICE_ID", dtParams.Rows[0]["SALE_INVOICE_ID"]);
                cmd.Connection = dbConnection.con;
                dbConnection.CheckConnectionState();
                dbConnection.con.Open();
                int intRows = cmd.ExecuteNonQuery();
                if(intRows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        [WebMethod]
        public bool InsertSaleInvoiceDetailServer(DataTable dtParams)
        {
            if (dtParams.Rows.Count > 0)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spInsertSALE_INVOICE_DETAILServer";
                cmd.Parameters.AddWithValue("@SALE_INVOICE_ID", dtParams.Rows[0]["SALE_INVOICE_ID"]);
                cmd.Parameters.AddWithValue("@SKU_ID", dtParams.Rows[0]["SKU_ID"]);
                cmd.Parameters.AddWithValue("@PRODUCT_CATEGORY_ID", dtParams.Rows[0]["PRODUCT_CATEGORY_ID"]);
                cmd.Parameters.AddWithValue("@PRICE", dtParams.Rows[0]["PRICE"]);
                cmd.Parameters.AddWithValue("@QTY", dtParams.Rows[0]["QTY"]);
                cmd.Parameters.AddWithValue("@REMARKS", dtParams.Rows[0]["REMARKS"]);
                cmd.Parameters.AddWithValue("@IS_VOID", dtParams.Rows[0]["IS_VOID"]);
                cmd.Parameters.AddWithValue("@LASTUPDATE_DATE", dtParams.Rows[0]["LASTUPDATE_DATE"]);                
                cmd.Connection = dbConnection.con;
                dbConnection.CheckConnectionState();
                dbConnection.con.Open();
                int intRows = cmd.ExecuteNonQuery();
                if (intRows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        [WebMethod]
        public bool InsertSKUStockRegisterServer(DataTable dtParams)
        {
            if (dtParams.Rows.Count > 0)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spInsertSKU_STOCK_REGISTERServer";
                cmd.Parameters.AddWithValue("@SKU_ID", dtParams.Rows[0]["SKU_ID"]);
                cmd.Parameters.AddWithValue("@DISTRIBUTOR_ID", dtParams.Rows[0]["DISTRIBUTOR_ID"]);
                cmd.Parameters.AddWithValue("@STOCK_DATE", dtParams.Rows[0]["STOCK_DATE"]);
                cmd.Parameters.AddWithValue("@PRINCIPAL_ID", dtParams.Rows[0]["PRINCIPAL_ID"]);
                cmd.Parameters.AddWithValue("@BATCHNO", dtParams.Rows[0]["BATCHNO"]);
                cmd.Parameters.AddWithValue("@OPENING_STOCK", dtParams.Rows[0]["OPENING_STOCK"]);
                cmd.Parameters.AddWithValue("@PURCHASED", dtParams.Rows[0]["PURCHASED"]);
                cmd.Parameters.AddWithValue("@PURCHASE_RETURN", dtParams.Rows[0]["PURCHASE_RETURN"]);
                cmd.Parameters.AddWithValue("@SALED", dtParams.Rows[0]["SALED"]);
                cmd.Parameters.AddWithValue("@SALE_RETURN", dtParams.Rows[0]["SALE_RETURN"]);
                cmd.Parameters.AddWithValue("@TRANSFER_IN", dtParams.Rows[0]["TRANSFER_IN"]);
                cmd.Parameters.AddWithValue("@TRANSFER_OUT", dtParams.Rows[0]["TRANSFER_OUT"]);
                cmd.Parameters.AddWithValue("@FREE_STOCK_IN", dtParams.Rows[0]["FREE_STOCK_IN"]);
                cmd.Parameters.AddWithValue("@FREE_STOCK_OUT", dtParams.Rows[0]["FREE_STOCK_OUT"]);
                cmd.Parameters.AddWithValue("@DAMAGED_STOCK", dtParams.Rows[0]["DAMAGED_STOCK"]);
                cmd.Parameters.AddWithValue("@DAMAGED_STOCK_RETURN", dtParams.Rows[0]["DAMAGED_STOCK_RETURN"]);                
                cmd.Parameters.AddWithValue("@CLOSING_STOCK", dtParams.Rows[0]["CLOSING_STOCK"]);
                cmd.Parameters.AddWithValue("@SHORT", dtParams.Rows[0]["SHORT"]);
                cmd.Parameters.AddWithValue("@EXCESS", dtParams.Rows[0]["EXCESS"]);
                cmd.Parameters.AddWithValue("@TRADE_PRICE", dtParams.Rows[0]["TRADE_PRICE"]);
                cmd.Parameters.AddWithValue("@RETAIL_PRICE", dtParams.Rows[0]["RETAIL_PRICE"]);
                cmd.Parameters.AddWithValue("@DISTRIBUTOR_PRICE", dtParams.Rows[0]["DISTRIBUTOR_PRICE"]);
                cmd.Parameters.AddWithValue("@RateDifference", dtParams.Rows[0]["RateDifference"]);
                cmd.Parameters.AddWithValue("@TAX_PRICE", dtParams.Rows[0]["TAX_PRICE"]);
                cmd.Parameters.AddWithValue("@SED_PRICE", dtParams.Rows[0]["SED_PRICE"]);
                cmd.Parameters.AddWithValue("@LASTUPDATE_DATE", dtParams.Rows[0]["LASTUPDATE_DATE"]);
                cmd.Connection = dbConnection.con;
                dbConnection.CheckConnectionState();
                dbConnection.con.Open();
                int intRows = cmd.ExecuteNonQuery();
                if (intRows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        [WebMethod]
        public bool InsertCustomerServer(DataTable dtParams)
        {
            if (dtParams.Rows.Count > 0)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spInsertCUSTOMERServer";
                cmd.Parameters.AddWithValue("@CUSTOMER_ID", dtParams.Rows[0]["CUSTOMER_ID"]);
                cmd.Parameters.AddWithValue("@DISTRIBUTOR_ID", dtParams.Rows[0]["DISTRIBUTOR_ID"]);
                cmd.Parameters.AddWithValue("@CUSTOMER_CODE", dtParams.Rows[0]["CUSTOMER_CODE"]);
                cmd.Parameters.AddWithValue("@CUSTOMER_NAME", dtParams.Rows[0]["CUSTOMER_NAME"]);
                cmd.Parameters.AddWithValue("@ADDRESS", dtParams.Rows[0]["ADDRESS"]);
                cmd.Parameters.AddWithValue("@CONTACT_PERSON", dtParams.Rows[0]["CONTACT_PERSON"]);
                cmd.Parameters.AddWithValue("@CONTACT_NUMBER", dtParams.Rows[0]["CONTACT_NUMBER"]);
                cmd.Parameters.AddWithValue("@EMAIL_ADDRESS", dtParams.Rows[0]["EMAIL_ADDRESS"]);
                cmd.Parameters.AddWithValue("@CNIC", dtParams.Rows[0]["CNIC"]);
                cmd.Parameters.AddWithValue("@IS_GST_REGISTERED", dtParams.Rows[0]["IS_GST_REGISTERED"]);
                cmd.Parameters.AddWithValue("@GST_NUMBER", dtParams.Rows[0]["GST_NUMBER"]);
                cmd.Parameters.AddWithValue("@NTN", dtParams.Rows[0]["NTN"]);
                cmd.Parameters.AddWithValue("@IS_ACTIVE", dtParams.Rows[0]["IS_ACTIVE"]);
                cmd.Parameters.AddWithValue("@REGDATE", dtParams.Rows[0]["REGDATE"]);
                cmd.Parameters.AddWithValue("@LASTUPDATE_DATE", dtParams.Rows[0]["LASTUPDATE_DATE"]);
                cmd.Parameters.AddWithValue("@TOWN_ID", dtParams.Rows[0]["TOWN_ID"]);
                cmd.Parameters.AddWithValue("@AREA_ID", dtParams.Rows[0]["AREA_ID"]);
                cmd.Parameters.AddWithValue("@ROUTE_ID", dtParams.Rows[0]["ROUTE_ID"]);
                cmd.Parameters.AddWithValue("@CHANNEL_TYPE_ID", dtParams.Rows[0]["CHANNEL_TYPE_ID"]);
                cmd.Parameters.AddWithValue("@BUSINESS_TYPE_ID", dtParams.Rows[0]["BUSINESS_TYPE_ID"]);
                cmd.Parameters.AddWithValue("@PROMOTION_CLASS", dtParams.Rows[0]["PROMOTION_CLASS"]);
                cmd.Parameters.AddWithValue("@IS_STAND", dtParams.Rows[0]["IS_STAND"]);
                cmd.Parameters.AddWithValue("@IS_COOLER", dtParams.Rows[0]["IS_COOLER"]);
                cmd.Connection = dbConnection.con;
                dbConnection.CheckConnectionState();
                dbConnection.con.Open();
                int intRows = cmd.ExecuteNonQuery();
                if (intRows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
