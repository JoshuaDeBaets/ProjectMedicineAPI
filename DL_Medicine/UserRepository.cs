using BL_Medicine.Builders;
using BL_Medicine.Tools;
using BL_Medicine.Domain;
using BL_Medicine.Repositories;
using DL_Medicine.Exceptions;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using BL_Medicine.Managers;
using System.Security.Claims;

namespace DL_Medicine;

public class UserRepository : IUserRepository
{
    private string _connectionString;
    private string jwtSecret;
    public UserRepository( string connectionstring )
    {
        
        _connectionString = connectionstring;
        jwtSecret = @"c1+D+ixsmMqtoQADJyXMFMjsOCp1yErBB6WNLvN7sGfMOS2m20s6HSE0UP7KjkePMEM6/p/oP/c6Zod7TjT5ww==";
    }

    public LoginResponse Login( string email, string password )
    {
        SqlConnection connection = new SqlConnection ( _connectionString );
        try
        {
            
            string query = "SELECT UserID, Email, Password FROM [User] WHERE Email = @Email";

            using (SqlCommand cmd = connection.CreateCommand ( ))
            {
                connection.Open ( );
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue ( "@Email", email );
                
                SqlDataReader reader = cmd.ExecuteReader ( );

                while (reader.Read( ))
                {
                    string pw = (string)reader["password"];
                    int id = (int)reader["userid"];
                    string email2 = (string)reader["email"];
                   

                    if (string.Equals ( password, pw.DecryptString ( ) ))
                    {
                        
                        JWTContainer container = JWTManager.GetJWTContainer ( id.ToString(), email2 );
                        JWTManager jWTManager = new JWTManager ( jwtSecret );
                        string token = jWTManager.GenerateToken ( container );
                        return new LoginResponse { HasError = false, ErrorMessage = "Succesfully logged in", Token = token };
                        };
                    return new LoginResponse { HasError = true, ErrorMessage = "Password is incorrect" };
                }
            }
            return new LoginResponse { HasError = true, ErrorMessage = "User does not exist" };
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

    public void UpdateUser()
    {
        throw new NotImplementedException ( );
    }

    public void Update()
    {
        throw new NotImplementedException ( );
    }

    public ErrorModel DeleteUser(string id)
    {
        SqlConnection conncetion = new SqlConnection ( _connectionString );
        try
        {
            string query = "DELETE FROM [User] WHERE UserID = @UserId";

            using (SqlCommand cmd = conncetion.CreateCommand ( ))
            {
                conncetion.Open ( );
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue ( "@UserId", id );

                int rowsAffected = cmd.ExecuteNonQuery ( );

                if (rowsAffected > 0)
                {
                    return new ErrorModel { HasError = false, ErrorMessage = "Succesfully deleted" };
                }
                return new ErrorModel { HasError = true, ErrorMessage = "Error deleting" };
            }   
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            conncetion.Close ( );
        }
    }

    public ErrorModel UpdateUser( string id, string? firstname, string? surname, int? weight, int? height )
    {
        SqlConnection connection = new SqlConnection ( _connectionString );

        try
        {
            string query = "UPDATE [User] SET ";

            List<string> updateColumns = new List<string> ( );

            if (firstname != null) updateColumns.Add ( "Firstname = @Firstname" );
            if (surname != null) updateColumns.Add ( "Surname = @Surname" );
            if (weight != null) updateColumns.Add ( "Weight = @Weight" );
            if (height != null) updateColumns.Add ( "Height = @Height" );

            query += string.Join ( ", ", updateColumns );

            query += " WHERE UserID = @UserId";


            using (SqlCommand command = new SqlCommand ( query, connection ))
            {
                command.Parameters.AddWithValue ( "@Firstname", firstname );
                command.Parameters.AddWithValue ( "@Surname", surname );
                command.Parameters.AddWithValue ( "@Weight", weight );
                command.Parameters.AddWithValue ( "@Height", height );
                command.Parameters.AddWithValue ( "@UserId", id );

                connection.Open ( );
                int rowsAffected = command.ExecuteNonQuery ( );

                if (rowsAffected > 0)
                {
                    return new ErrorModel { HasError = false, ErrorMessage = "Update successful" };
                }
                else
                {
                    return new ErrorModel { HasError = true, ErrorMessage = "User not found or no update necessary" };
                }
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

}