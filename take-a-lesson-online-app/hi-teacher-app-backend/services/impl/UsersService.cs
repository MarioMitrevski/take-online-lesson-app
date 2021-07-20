using hi_teacher_app_backend.DomainModels;
using hi_teacher_app_backend.DTOs;
using hi_teacher_app_backend.Exceptions;
using hi_teacher_app_backend.Models;
using hi_teacher_app_backend.repositories.impl;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace hi_teacher_app_backend.services.impl
{
    public class UsersService : IUsersService<User>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ITeachersService<Teacher> _teachersService;
        private readonly IStudentsService<Student> _studentsService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration Configuration;

        public UsersService(IUsersRepository usersRepository, IConfiguration configuration, ITeachersService<Teacher> ITeachersService, IStudentsService<Student> IStudentsService, IWebHostEnvironment webHostEnvironment)
        {
            _usersRepository = usersRepository;
            Configuration = configuration;
            _teachersService = ITeachersService;
            _studentsService = IStudentsService;
            _webHostEnvironment = webHostEnvironment;
        }

        public UserDTO Authenticate(string username, string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashedPassword = Encoding.UTF8.GetString(md5data);

            var user = _usersRepository.GetAll()
                .SingleOrDefault(x => x.UserName == username && x.Password == hashedPassword);

            if (user == null) return null;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("userId", user.UserId.ToString()),
                new Claim("role",user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: Configuration["Jwt:Issuer"],
                audience: Configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(90),
                signingCredentials: credentials
            );
           
            var userDTO = new UserDTO
            {
                FullName = user.FullName,
                BirthDate = user.BirthDate,
                ImageUrl = user.ImageUrl,
                JwtToken = new JwtSecurityTokenHandler().WriteToken(token)
            };
            return userDTO;

        }

        public void Register(RegisterDTO model,string scheme,string host,string pathBase)
        {
            //Validations
            if (string.IsNullOrEmpty(model.FullName))
                throw new Exception("Full name is required");


            if (string.IsNullOrEmpty(model.UserName))
                throw new Exception("Username is required");

            if (!ValidUsername(model.UserName))
                throw new Exception("Username already in use");

            if (string.IsNullOrEmpty(model.Password))
                throw new Exception("Password is required");

            if (model.ImageFile != null)
            {
                UploadFile(model);
            }

            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.UTF8.GetBytes(model.Password));
            var hasshedPassword = Encoding.UTF8.GetString(md5data);

            var role = Policies.Student;
            if (model.IsTeacher)
            {
                role = Policies.Teacher;
            }
            var user = new User
            {
                FullName = model.FullName,
                BirthDate = model.BirthDate,
                UserName = model.UserName,
                Password = hasshedPassword,
                Role = role,
                ImageUrl = String.Format("{0}://{1}{2}/Images/{3}", scheme, host, pathBase, model.ImageUrl)
            };

            User savedUser = _usersRepository.Add(user);

            if (model.IsTeacher)
            {
                _teachersService.AddTeacher(savedUser.UserId,model.TeacherDescription);
            }
            else
            {
                _studentsService.AddStudent(savedUser.UserId);
            }
        }

    /*    private static bool ValidPassword(string password)
        {
            var passwordRegex = new Regex("^(?=.*[0-9])(?=.*[a-z]).{6,20}$");
            var match = passwordRegex.Match(password);
            return match.Success;
        }*/

        private bool ValidUsername(string username)
        {
            return _usersRepository.GetAll().All(x => x.UserName != username);
        }

        private void UploadFile(RegisterDTO userDTO)
        {

            try
            {
                if (userDTO.ImageFile.Length > 0)
                {
                    string fileName = FormFileName(userDTO.UserName);

                    var pathToSave = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", fileName);

                    using (var stream = new FileStream(pathToSave, FileMode.Create))
                    {
                        userDTO.ImageFile.CopyTo(stream);
                    }
                    userDTO.ImageUrl = fileName;
                }
                else
                {
                    throw new FileUploadException();
                }
            }
            catch (Exception)
            {
                throw new ImageUploadFailedException();
            }

        
        }

        private static string FormFileName(string userName)
        {
            var fileNameForStorage = $"{userName}.png";
            return fileNameForStorage;
        }
    }
}
