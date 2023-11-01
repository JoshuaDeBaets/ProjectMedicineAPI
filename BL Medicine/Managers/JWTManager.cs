using BL_Medicine.Domain;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using BL_Medicine.Exceptions;

namespace BL_Medicine.Managers
{
    public class JWTManager
    {
        public string SecretKey { get; set; } = "";
        public JWTManager( string secretKey )
        {
            SecretKey = secretKey;
        }
        private SecurityKey GetSymmetricSecurityKey()
        {
            byte[] symmetricKey = Convert.FromBase64String ( SecretKey );
            return new SymmetricSecurityKey ( symmetricKey );
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters ( )
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = GetSymmetricSecurityKey ( ),
            };
        }

        public bool IsTokenValid( string token )
        {
            if (string.IsNullOrEmpty ( token )) throw new JWTManagerException ( "Given token is null or empty" );

            TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters ( );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler ( );
            try
            {
                ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken ( token, tokenValidationParameters, out SecurityToken validatedToken );
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public string GenerateToken( JWTContainer container )
        {
            if (container == null || container.Claims == null || container.Claims.Length == 0)
                throw new JWTManagerException ( "Arguments to create token not valid" );

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity ( container.Claims ),
                Expires = DateTime.UtcNow.AddMinutes ( Convert.ToInt32 ( container.ExpireMinutes ) ),
                SigningCredentials = new SigningCredentials ( GetSymmetricSecurityKey ( ), container.SecurityAlgorithm )
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler ( );
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken ( securityTokenDescriptor );
            string token = jwtSecurityTokenHandler.WriteToken ( securityToken );

            return token;
        }

        public IEnumerable<Claim> GetTokenClaims( string token )
        {
            if (string.IsNullOrEmpty ( token ))
                throw new JWTManagerException ( "Given token is null or empty" );

            TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters ( );
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler ( );

            try
            {
                ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken ( token, tokenValidationParameters, out SecurityToken validatedToken );
                return tokenValid.Claims;
            }
            catch (Exception ex)
            {
                throw new JWTManagerException ( "Exception: ", ex );
            }
        }

        public static JWTContainer GetJWTContainer( string id, string email, string role )
        {
            return new JWTContainer ( )
            {
                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role),
                }
            };
        }
    }
}
