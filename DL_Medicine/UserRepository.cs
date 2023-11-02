using BL_Medicine.Builders;
using BL_Medicine.Tools;
using BL_Medicine.Domain;
using BL_Medicine.Repositories;
using DL_Medicine.Exceptions;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using BL_Medicine.Managers;

namespace DL_Medicine;

public class UserRepository : IUserRepository
{
    private string _connectionString;
    private string jwtSecret;
    public UserRepository( string connectionstring )
    {
        
        _connectionString = connectionstring;
        jwtSecret = "eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiQWRtaW4iLCJJc3N1ZXIiOiJJc3N1ZXIiLCJVc2VybmFtZSI6IkphdmFJblVzZSIsImV4cCI6MTY5ODg0NzkzNCwiaWF0IjoxNjk4ODQ3OTM0fQ.Kmfv3crnSj7B2du3o9qji1Jg2Ukb5jCidz6LcSbIsys";
    }

    public LoginResponse Login( string email, string password )
    {
        SqlConnection connection = new SqlConnection ( _connectionString );
        try
        {
            var response = new LoginResponse ( );
            
            string query = "SELECT Email, Password FROM [User] WHERE Email = @Email";

            using (SqlCommand cmd = connection.CreateCommand ( ))
            {
                connection.Open ( );
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue ( "@Email", email );
                
                SqlDataReader reader = cmd.ExecuteReader ( );

                while (reader.Read( ))
                {
                    string pw = (string)reader["password"];
                    Console.WriteLine ( pw );
                    Console.WriteLine ( pw.DecryptString ( ) );

                    if (string.Equals ( password, pw.DecryptString ( ) ))
                    {
                        response.HasError = false;
                        response.ErrorMessage = "Succesfully logged in";
                        response.Token = jwtSecret;
                        return response;
                        };
                    response.HasError = true;
                    response.ErrorMessage = "Password is incorrect";
                    response.Token = "";
                    return response;
                }
            }
            response.HasError = true;
            response.ErrorMessage = "User does not exist";
            response.Token = "";
            return response;
        }
        catch (Exception ex)
        {
            // Handle exceptions
            throw;
        }
        finally 
        {
            connection.Close ( );
        }
    }


    public LoginResponse Register( string firstname, string surname, string email, string password, string confirmPassword )
    {
        var response = new LoginResponse ( );
        SqlConnection connection = new SqlConnection ( _connectionString );
        try
        {
            
            string query = "INSERT INTO [User] (Firstname, Surname, Email, Password) VALUES (@Firstname, @Surname, @Email, @Password)";
            {
                connection.Open ( );
                
                using (SqlCommand command = connection.CreateCommand ( ))
                {
                    // You should use parameterized queries to prevent SQL injection
                    command.CommandText = query;
                    command.Parameters.AddWithValue ( "@Firstname", firstname );
                    command.Parameters.AddWithValue ( "@Surname", surname );
                    command.Parameters.AddWithValue ( "@Email", email );
                    command.Parameters.AddWithValue ( "@Password", password.EncryptString() );

                    int rowsAffected = command.ExecuteNonQuery ( );

                    if (rowsAffected > 0)
                    {
                        response.HasError = false;
                        response.ErrorMessage = "Succesfully registered";
                        response.Token = jwtSecret;
                        return response;
                    }
                }
                response.HasError = true;
                response.ErrorMessage = "Error registering";
                response.Token = "";
                return response;
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            connection.Close ( );
        }
    }


    public void UpdateUser()
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        throw new NotImplementedException();
    }

    public bool userExists(string email)
    {
        SqlConnection connection = new SqlConnection ( _connectionString );
        try
        {
            
            string query = "select count(*) from [User] where Email = @Email";

            using (SqlCommand cmd = connection.CreateCommand ( ))
            {
                connection.Open ( );
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue ( "@Email", email );


                int userCount = (int)cmd.ExecuteScalar ( );

                return userCount > 0;
            }
        }
        catch( Exception)
        {
            throw;
        }
        finally
        {
            connection.Close ( );
        }

    }

    public User GetProfile(string email)
    {
        SqlConnection connection = new SqlConnection ( _connectionString );
        try
        {
            var user = new User ( );
            
            string query = "SELECT Firstname, Surname, Email FROM [User] WHERE Email = @email";

            using (SqlCommand cmd = connection.CreateCommand ( ))
            {
                connection.Open ( );
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue ( "@email", email );

                SqlDataReader reader = cmd.ExecuteReader ( );

                
                
                    while (reader.Read ( ))
                    {
                        
                      
                            var b = new UserBuilder ( )
                                .SetFirstname ( (string)reader["Firstname"].ToString ( ) )
                                .SetSurname ( (string)reader["Surname"].ToString ( ) )
                                .SetEmail ( (string)reader["Email"].ToString ( ) ); 
                            user = b.Build ( );
                        
                    }
                connection.Close ( );
            }
            return user;
        }
        catch (Exception ex)
        {
            // Handle exceptions
            throw new UserRepositoryException ( "Error in UserRepositorySQL - Login - exception: " + ex.Message, ex );
        }
        finally
        {
            connection.Close ( );
        }
    }


}