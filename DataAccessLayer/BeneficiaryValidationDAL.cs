using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace KuaiexDashboard.DAL
{
    public class BeneficiaryValidationDAL
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["KUAIEXEntities"].ConnectionString;

        public BeneficiaryValidation GetBeneficiaryValidationByRemittanceTypeId(int remittanceTypeId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetBeneficiaryValidationByRemittanceTypeId", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@RemittanceTypeId", SqlDbType.Int).Value = remittanceTypeId;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                BeneficiaryValidation beneficiaryValidation = new BeneficiaryValidation
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    FieldId = reader.IsDBNull(reader.GetOrdinal("FieldId")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("FieldId")),
                                    FieldName = reader.GetString(reader.GetOrdinal("FieldName")),
                                    FieldValidation = reader.IsDBNull(reader.GetOrdinal("FieldValidation")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("FieldValidation")),
                                    UID = reader.IsDBNull(reader.GetOrdinal("UID")) ? null : (Guid?)reader.GetGuid(reader.GetOrdinal("UID")),
                                    CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                                    CreatedOn = reader.IsDBNull(reader.GetOrdinal("CreatedOn")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("CreatedOn")),
                                    UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("UpdatedBy")),
                                    UpdatedOn = reader.IsDBNull(reader.GetOrdinal("UpdatedOn")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("UpdatedOn")),
                                    RemittanceTypeId = reader.IsDBNull(reader.GetOrdinal("RemittanceTypeId")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("RemittanceTypeId")),
                                };

                                return beneficiaryValidation;
                            }
                        }
                    }

                    return null; 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddRangeBeneficiaryValidations(List<BeneficiaryValidation> beneficiaryValidations)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("AddRangeBeneficiaryValidations", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        DataTable dtBeneficiaryValidations = new DataTable();
                        dtBeneficiaryValidations.Columns.Add("FieldId", typeof(int));
                        dtBeneficiaryValidations.Columns.Add("FieldName", typeof(string));
                        dtBeneficiaryValidations.Columns.Add("FieldValidation", typeof(int));
                        dtBeneficiaryValidations.Columns.Add("UID", typeof(Guid));
                        dtBeneficiaryValidations.Columns.Add("CreatedBy", typeof(int));
                        dtBeneficiaryValidations.Columns.Add("CreatedOn", typeof(DateTime));
                        dtBeneficiaryValidations.Columns.Add("UpdatedBy", typeof(int));
                        dtBeneficiaryValidations.Columns.Add("UpdatedOn", typeof(DateTime));
                        dtBeneficiaryValidations.Columns.Add("RemittanceTypeId", typeof(int));

                        foreach (var beneficiaryValidation in beneficiaryValidations)
                        {
                            dtBeneficiaryValidations.Rows.Add(
                                beneficiaryValidation.FieldId.GetValueOrDefault(),
                                beneficiaryValidation.FieldName,
                                beneficiaryValidation.FieldValidation.GetValueOrDefault(),
                                beneficiaryValidation.UID.GetValueOrDefault(),
                                beneficiaryValidation.CreatedBy.GetValueOrDefault(),
                                beneficiaryValidation.CreatedOn.GetValueOrDefault(),
                                beneficiaryValidation.UpdatedBy.GetValueOrDefault(),
                                beneficiaryValidation.UpdatedOn.GetValueOrDefault(),
                                beneficiaryValidation.RemittanceTypeId.GetValueOrDefault()
                            );
                        }

                        SqlParameter parameter = command.Parameters.AddWithValue("@BeneficiaryValidations", dtBeneficiaryValidations);
                        parameter.SqlDbType = SqlDbType.Structured;
                        parameter.TypeName = "dbo.BeneficiaryValidationType";

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
