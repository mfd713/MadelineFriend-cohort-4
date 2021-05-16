using System.Collections.Generic;
using System.Linq;
using FieldAgent.Entities;
using FieldAgent.Interfaces;
using FieldAgent.DTOs;
using Microsoft.Data.SqlClient;
using System.Data;
using System;

namespace FieldAgent.DAL
{
    public class ReportsADORepository : IReportsRepository
    {
        private string connectionString;

        public ReportsADORepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Response<List<ClearanceAuditListItem>> AuditClearance(int securityClearanceId, int agencyId)
        {
            Response<List<ClearanceAuditListItem>> response = new Response<List<ClearanceAuditListItem>>();
            response.Data = new List<ClearanceAuditListItem>();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("ClearanceAudit", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@agencyId", agencyId);
                command.Parameters.AddWithValue("@securityClearanceId", securityClearanceId);
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var row = new ClearanceAuditListItem();

                            row.NameLastFirst = $"{reader["LastName"]},{reader["FirstName"]}";
                            row.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                            row.BadgeId = new Guid(reader["BadgeId"].ToString());
                            row.ActivationDate = DateTime.Parse(reader["ActivationDate"].ToString());

                            if (reader["DeactivationDate"] != DBNull.Value)
                            {
                                row.DeactivationDate = DateTime.Parse(reader["DeactivationDate"].ToString());
                            }


                            response.Data.Add(row);
                        }

                    }
                }
                catch
                {
                    response.Message = $"Could not connect to database";
                    response.Success = false;
                }

                response.Success = response.Data.Count > 0;
                response.Message = response.Success ? "" : "No results for that query";
                return response;
            }
        }

        public Response<List<PensionListItem>> GetPensionList(int agencyId)
        {
            Response<List<PensionListItem>> response = new Response<List<PensionListItem>>();
            response.Data = new List<PensionListItem>();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("PensionList", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@agencyId", agencyId);
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var row = new PensionListItem();

                            row.NameLastFirst = $"{reader["LastName"]},{reader["FirstName"]}";
                            row.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                            row.BadgeId = new Guid(reader["BadgeId"].ToString());
                            row.DeactivationDate = DateTime.Parse(reader["DeactivationDate"].ToString());
                            row.AgencyName = reader["ShortName"].ToString();

                            response.Data.Add(row);
                        }

                    }
                }
                catch
                {
                    response.Message = $"Could not connect to database";
                    response.Success = false;
                }

                response.Success = response.Data.Count > 0;
                response.Message = response.Success ? "" : "No results for that query";
                return response;
            }
        }

        public Response<List<TopAgentListItem>> GetTopAgents()
        {
            Response<List<TopAgentListItem>> response = new Response<List<TopAgentListItem>>();
            response.Data = new List<TopAgentListItem>();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("TopAgents", connection);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var row = new TopAgentListItem();

                            row.NameLastFirst = $"{reader["LastName"]},{reader["FirstName"]}";
                            row.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                            row.CompletedMissionCount = (int)reader["Completed"];

                            response.Data.Add(row);
                        }

                    }
                }
                catch
                {
                    response.Message = $"Could not connect to database";
                    response.Success = false;
                }

                response.Success = response.Data.Count > 0;
                response.Message = response.Success ? "" : "Database empty";
                return response;
            }
        }
    }
}
