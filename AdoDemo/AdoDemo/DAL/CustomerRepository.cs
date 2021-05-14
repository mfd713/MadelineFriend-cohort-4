using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AdoDemo
{
    public class CustomerRepository : ICustomerRepository
    {
        private string _sqlConnectionString;

        public CustomerRepository(string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
        }

        public Customer CreateCustomer(Customer customer)
        {
            int newId = 0;
            using(var connection = new SqlConnection(_sqlConnectionString))
            {
                var createSQL = @"INSERT INTO Customer (FirstName, LastName, EmailAddress, Phone, Address, City, Province, PostalCode, CustomerSince) " +
                    "VALUES (@FirstName, @LastName, @EmailAddress, @Phone, @Address, @City, @Province, @PostalCode, @CustomerSince); " +
                    "SELECT SCOPE_IDENTITY() ";
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(createSQL, connection);

                    command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    command.Parameters.AddWithValue("@LastName", customer.LastName);
                    command.Parameters.AddWithValue("@EmailAddress", customer.Email);
                    command.Parameters.AddWithValue("@Phone", customer.Phone);
                    command.Parameters.AddWithValue("@Address", customer.Address);
                    command.Parameters.AddWithValue("@City", customer.City);
                    command.Parameters.AddWithValue("@Province", customer.Province);
                    command.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
                    command.Parameters.AddWithValue("@CustomerSince", customer.CustomerSince);

                    newId = Convert.ToInt32(command.ExecuteScalar());
                 }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    if (connection != null && connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            customer.CustomerID = newId;
            Console.WriteLine($"Customer with id {customer.CustomerID} was created");
            return customer;
        }

        public void Delete(int CustomerId)
        {
            using(SqlConnection connection = new SqlConnection(_sqlConnectionString))
            {
                string deleteSQL = @"DELETE FROM Customer WHERE CustomerId = @ID";
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(deleteSQL, connection);
                    command.Parameters.AddWithValue("@ID", CustomerId);

                    command.ExecuteNonQuery();
                    Console.WriteLine($"Customer with id {CustomerId} was deleted");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    if (connection != null && connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        public List<Customer> ReadAll()
        {
            List<Customer> result = new List<Customer>();

            using (var connection = new SqlConnection(_sqlConnectionString))
            {
                string sql = @"SELECT CustomerID, FirstName, LastName, EmailAddress, Phone, Address, City, Province, PostalCode, CustomerSince " +
                    "FROM Customer";

                try
                {
                    connection.Open();

                    var command = new SqlCommand(sql, connection);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.HasRows && reader.Read())
                    {
                        Customer customer = new Customer();
                        customer.CustomerID = (int)reader["CustomerID"];
                        customer.FirstName = reader["FirstName"].ToString();
                        customer.LastName = reader["LastName"].ToString();
                        customer.Email = reader["EmailAddress"].ToString();
                        customer.Phone = reader["Phone"].ToString();
                        customer.Address = reader["Address"].ToString();
                        customer.City = reader["City"].ToString();
                        customer.Province = reader["Province"].ToString();
                        customer.PostalCode = reader["PostalCode"].ToString();
                        customer.CustomerSince = DateTime.Parse(reader["CustomerSince"].ToString());

                        result.Add(customer);
                    }

                }catch(SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    if(connection != null && connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            return result;
        }

        public List<Customer> ReadByName(string prefix)
        {
            return ReadAll().Where(c => c.LastName.ToLower().StartsWith(prefix.ToLower())).OrderBy(c => c.LastName).ToList();
        }

        public void Update(Customer customer)
        {
            using(var connection = new SqlConnection(_sqlConnectionString))
            {
                var replaceSQL = @"UPDATE Customer SET FirstName = @FirstName, LastName = @LastName, EmailAddress = @EmailAddress, Phone = @Phone, Address = @Address, " +
                                @"City = @City, Province = @Province, PostalCode = @PostalCode, CustomerSince = @CustomerSince " +
                                @"WHERE CustomerId = @ID; ";

                try 
                {
                    connection.Open();
                    var command = new SqlCommand(replaceSQL, connection);

                    command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    command.Parameters.AddWithValue("@LastName", customer.LastName);
                    command.Parameters.AddWithValue("@EmailAddress", customer.Email);
                    command.Parameters.AddWithValue("@Phone", customer.Phone);
                    command.Parameters.AddWithValue("@Address", customer.Address);
                    command.Parameters.AddWithValue("@City", customer.City);
                    command.Parameters.AddWithValue("@Province", customer.Province);
                    command.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
                    command.Parameters.AddWithValue("@CustomerSince", customer.CustomerSince);
                    command.Parameters.AddWithValue("@ID", customer.CustomerID);

                    command.ExecuteNonQuery();
                    Console.WriteLine($"Customer with id {customer.CustomerID} was updated");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    if (connection != null && connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }
    }
}
