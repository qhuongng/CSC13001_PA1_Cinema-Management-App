using Microsoft.EntityFrameworkCore;
using BCryptNet = BCrypt.Net.BCrypt;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineManagement
{
    public class User
    {
        [Key]
        [Column(name: "id")]
        public int id { get; set; }
        [Column(name: "userName")]
        public string userName { get; set; }
        [Column(name: "password")]
        public string password { get; set; }
        [Column(name: "DOB")]
        public DateTime DOB { get; set; }
        [Column(name: "gender")]
        public string gender { get; set; }
        [Column(name: "isAdmin")]
        public bool isAdmin { get; set; }
    }

    public class UserDbContext : BaseDbContext
    {
        public DbSet<User> User { get; set; }
    }

    public class UserManager
    {
        public User addUser(User user) // if exists return null, else return this user
        {
            using (var _context = new UserDbContext())
            {
                var existingUser = _context.User.FirstOrDefault(x => x.userName == user.userName);
                if (existingUser == null)
                {
                    string hashPassword = PasswordHandler.hashPassword(user.password);
                    user.password = hashPassword;
                    _context.User.Add(user);
                    try
                    {
                        _context.SaveChanges();
                        return user;
                    }
                    catch (DbUpdateException ex)
                    {
                        // Handle database update exception
                        throw new Exception("An error occurred while saving the user.", ex);
                    }

                }
                else
                {
                    throw new Exception("User name already exists!");
                }
            }
        }
        public User updateUser(User user)
        {
            using(var _context = new UserDbContext())
            {
                var existingUser = _context.User.FirstOrDefault(x => x.id == user.id);
                if (existingUser != null)
                {
                    existingUser.userName = user.userName;
                    existingUser.DOB = user.DOB;
                    existingUser.gender = user.gender;
                    existingUser.isAdmin = user.isAdmin;
                    existingUser.password = user.password;
                    try
                    {
                        _context.SaveChanges();
                        return user;
                    }
                    catch (DbUpdateException ex)
                    {
                        throw new Exception("An error occurred while updating the user.", ex);
                    }
                }
                else
                {
                    throw new Exception("User is not exists!");
                }
            }
        }
        public User GetUserByName(string userName)
        {
            using (var _context = new UserDbContext())
            {
                var existingUser = _context.User.FirstOrDefault(x => x.userName == userName);
                if (existingUser != null)
                {
                    return existingUser;
                }
                else
                {
                    throw new Exception("User is not exists!");
                }
            }
        }
        public bool DeleteUserById(int id) 
        {
            using (var _context = new UserDbContext())
            {
                var existingUser = _context.User.FirstOrDefault(x => x.id == id);
                if (existingUser != null)
                {
                    try
                    {
                        _context.User.Remove(existingUser);
                        _context.SaveChanges();
                        return true;
                    }
                    catch (DbUpdateException ex)
                    {
                        throw new Exception("Delete user failed");
                    }
                }
                else { throw new Exception("User is not exists!"); }
            }
        }
    }

    public class PasswordHandler
    {
        public static string hashPassword(string password)
        {
            return BCryptNet.HashPassword(password);
        }
        public static bool verifyPassword(string password, string hashedPassword)
        {
            return BCryptNet.Verify(password, hashedPassword);
        }
    }
}