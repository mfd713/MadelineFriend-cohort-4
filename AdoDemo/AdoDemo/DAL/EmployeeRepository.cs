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
                    Console.WriteLine("Error!" + ex.Message);

                }
            }
            return employee;
        }

        public void Delete(int employeeId)
        {
            using(SqlConnection conn = new SqlConnection(_sqlConnectionString))
            {
                string deleteSQL = @"DELETE FROM Employee " +
                    @"WHERE EmployeeId = @ID";

                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(deleteSQL, conn);
                    command.Parameters.AddWithValue("@ID", employeeId);

                    command.ExecuteNonQuery();
                }
                catch (SqlException ex) 
                {
                    Console.WriteLine("Error!" + ex.Message);
                }
            }
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
            using (SqlConnection conn = new SqlConnection(_sqlConnectionString))
            {
                var employeeUpdateSQL = @"UPDATE Employee "+
                    "SET FirstName = @FirstName, LastName = @LastName, StartDate = @StartDate, EndDate = @EndDate " +
                    "WHERE EmployeeID = @ID;";
                try
                {
                    conn.Open();
                    var command = new SqlCommand(employeeUpdateSQL, conn);
                    command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    command.Parameters.AddWithValue("@LastName", employee.LastName);
                    command.Parameters.AddWithValue("@StartDate", employee.StartDate);
                    command.Parameters.AddWithValue("@EndDate", employee.EndDate);
                    command.Parameters.AddWithValue("@ID", employee.EmployeeId);

                    command.ExecuteNonQuery();

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
        }
    }
}
