using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoDemo
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private string _sqlConnectionString;
        public EmployeeRepository(string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
        }
        public Employee CreateEmployee(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(_sqlConnectionString))
            {
                string sql = @"INSERT INTO Employee (firstName,lastName,startDate,endDate) values(@firstName,@lastName,@startDate,@endDate)";

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@firstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@startDate", employee.StartDate);
                    cmd.Parameters.AddWithValue("@endDate", employee.EndDate);
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {

                }
            }
            return employee;
        }

        public void Delete(int employeeId)
        {
            throw new NotImplementedException();
        }

        public List<Employee> ReadAll()
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection conn = new SqlConnection(_sqlConnectionString))
            {
                string sql = @"select EmployeeId, FirstName, LastName, StartDate, EndDate From Employee";

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.HasRows && dr.Read())
                    {
                        Employee employee = new Employee();
                        employee.EmployeeId = int.Parse(dr["employeeId"].ToString());
                        employee.FirstName = dr["FirstName"].ToString();
                        employee.LastName = dr["LastName"].ToString();
                        employee.StartDate = DateTime.Parse(dr["StartDate"].ToString());
                        employee.EndDate = dr["EndDate"] is DBNull ? null : DateTime.Parse(dr["EndDate"].ToString());

                        employees.Add(employee);
                    }

                }
                catch (Exception ex)
                {

                    Console.WriteLine("Error!" + ex.Message);
                }
                finally
                {
                    if (conn != null && conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            return employees;

        }

        public void Update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
