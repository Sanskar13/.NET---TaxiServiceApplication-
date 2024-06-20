using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaxiServiceAPI.Constants;
using TaxiServiceAPI.Data;
using TaxiServiceAPI.Entities;
using TaxiServiceAPI.Enums;
using TaxiServiceAPI.Interfaces.IService;
using TaxiServiceAPI.Mapper;
using TaxiServiceAPI.Models.Request;
using TaxiServiceAPI.Utils;

namespace TaxiServiceAPI.Services
{
    public class AuthService: IAuthService
    {
        private readonly MySQLDBContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(MySQLDBContext context, IConfiguration config)
        {
            _context = context;
            _configuration = config;   
        }

        /// <summary>
        /// Signs up a new rider.
        /// </summary>
        /// <param name="riderSignUpRequest">The rider sign-up request.</param>
        /// <returns>A message indicating the result of the sign-up operation.</returns>
        public string SignUpRider(RiderSignUpRequest riderSignUpRequest)
        {
            User? existingUser = _context.Users.FirstOrDefault(u =>
               (u.Email == riderSignUpRequest.Email || u.Phone == riderSignUpRequest.Phone) && u.UserType == UserType.Rider);

            if (existingUser != null)
            {
                return Messages.UserAlreadyExists;
            }

            User newUser = RiderSignUpRequestToRider.MapToRider(riderSignUpRequest);

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Messages.UserSignUpSuccess;
        }

        /// <summary>
        /// Signs up a new driver.
        /// </summary>
        /// <param name="driverSignUpRequest">The driver sign-up request.</param>
        /// <returns>A message indicating the result of the sign-up operation.</returns>
        public string SignUpDriver(DriverSignUpRequest driverSignUpRequest)
        {
            User? existingUser = _context.Users.FirstOrDefault(u =>
               (u.Email == driverSignUpRequest.Email || u.Phone == driverSignUpRequest.Phone) && u.UserType == UserType.Driver);

            if (existingUser != null)
            {
                return Messages.UserAlreadyExists;
            }

            User newUser = DriverSignUpRequestToDriver.MapToDriver(driverSignUpRequest);

            _context.Users.Add(newUser);
            _context.SaveChanges();

            Guid newUserId = newUser.Id;
            Vehicle newVehicle = DriverToVehicle.MapToVehicle(newUserId, driverSignUpRequest);
            
            _context.Vehicles.Add(newVehicle);
            _context.SaveChanges();

            return Messages.UserSignUpSuccess;
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginRequest">The login request.</param>
        /// <returns>A JWT token upon successful login, or an error message otherwise.</returns>
        public string Login(LoginRequest loginRequest)
        {
            User? user = null;

            UserType userType = UserTypeConverter.GetUserType(loginRequest.UserType.ToLower());

            if (IsEmail.CheckEmail(loginRequest.PhoneOrEmail))
            {
                user = _context.Users.FirstOrDefault(u => u.Email == loginRequest.PhoneOrEmail && u.Password == loginRequest.Password && u.UserType == userType);
            }
            else
            {
                user = _context.Users.FirstOrDefault(u => u.Phone == loginRequest.PhoneOrEmail && u.Password == loginRequest.Password && u.UserType == userType);
            }

            if (user == null)
            {
                throw new UnauthorizedAccessException(Messages.InvalidUsernameOrPassword);
            }

            return GenerateToken(user);
        }

        private string GenerateToken(User user)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserType.ToString())
                };

            JwtSecurityToken token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
