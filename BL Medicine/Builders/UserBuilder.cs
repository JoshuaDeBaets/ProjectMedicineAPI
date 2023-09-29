using BL_Medicine.Domain;
using BL_Medicine.Exceptions;
using BL_Medicine.RegexChecks;

namespace BL_Medicine.Builders
{
    public class UserBuilder
    {
        private readonly User _user = new();
        
        public UserBuilder SetFirstname( string firstname )
        {
            if ( string.IsNullOrEmpty( firstname ) )
            {
                throw new UserException( "firstname is not filled in" );
            }

            if ( !firstname.IsValidName() )
            {
                throw new UserException( "please give in a valid firstname" );
            }
            _user.Firstname = firstname;
            return this;
        }

        public UserBuilder SetSurname( string surname )
        {
            if ( string.IsNullOrEmpty( surname ) )
            {
                throw new UserException( "surname is not filled in" );
            }
            
            if ( !surname.IsValidName() )
            {
                throw new UserException( "please give in a valid surname" );
            }
            _user.Surname = surname;
            return this;
        }

        public UserBuilder SetEmail( string email )
        {
            if ( string.IsNullOrEmpty( email ) )
            {
                throw new UserException( "No Email", "X?", DateTime.Now );
            }
            if ( !email.IsValidEmail() )
            {
                throw new UserException( "Invalid Email", "X!", DateTime.Now );
            }
            
            _user.Email = email;
            return this;
        }

        public UserBuilder Password( string password )
        {
            if ( string.IsNullOrEmpty( password ) )
            {
                throw new UserException( "No Password" );
            }

            _user.Password = password;
            return this;
        }

        public UserBuilder SetDateOfBirth( DateTime dateOfBirth )
        {
            //empty datetime will converter to the minimum date in c#.
            if ( dateOfBirth == DateTime.MinValue )
            {
                throw new UserException( "Please fill in a proper date" );
            }
            if ( dateOfBirth > DateTime.Now )
            {
                throw new UserException( "You cannot be born in the future" );
            }
            _user.DayOfBirth = dateOfBirth;
            return this;
        }

        public UserBuilder SetGender( Enum gender )
        {
            // Add any additional gender validation if required.
            _user.Gender = gender;
            return this;
        }

        public UserBuilder SetWeight( int weight )
        {
            if ( weight <= 0 )
            {
                throw new UserException( "Weight cannot be zero or lower" );
            }

            _user.Weight = weight;
            return this;
        }

        public UserBuilder SetHeight( int height )
        {
            if ( height <= 0 )
            {
                throw new UserException( "Height cannot be zero or lower" );
            }
            _user.Height = height;
            return this;
        }

        public UserBuilder SetIsValidated( bool isEmailValidated )
        {
            _user.IsEmailValidated = isEmailValidated;
            return this;
        }

        public UserBuilder SetUserMedicines( List<UserMedicine> medicines )
        {
            _user.UserMedicines = medicines;
            return this;
        }

        public UserBuilder AddUserMedicines ( UserMedicine medicine )
        {
            _user.UserMedicines.Add( medicine );
            return this;
        }

        public User Build()
        {
            if ( _user.Firstname == null ) throw new UserException( "A first name is required and must be provided" );
            if ( _user.Surname == null ) throw new UserException( "A surname is required and must be provided" );
            if ( _user.Email == null ) throw new UserException( "An email is required and must be provided" );
            return _user;
        }

    }
}
