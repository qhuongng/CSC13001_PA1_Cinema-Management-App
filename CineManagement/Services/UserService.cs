using Microsoft.EntityFrameworkCore;
using BCryptNet = BCrypt.Net.BCrypt;
using CineManagement.Models;

namespace CineManagement.Services
{
    public class UserService
    {
        public bool addUser(User user) // if exists return null, else return this user
        {
            using (var _context = new CinemaManagementContext())
            {
                var existingUser = _context.Users.FirstOrDefault(x => x.UserName == user.UserName);
                if (existingUser == null)
                {
                    _context.Users.Add(user);
                    try
                    {
                        _context.SaveChanges();
                        return true;
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
            using (var _context = new CinemaManagementContext())
            {
                var existingUser = _context.Users.FirstOrDefault(x => x.UserId == user.UserId);
                if (existingUser != null)
                {
                    existingUser.UserName = user.UserName;
                    existingUser.Dob = user.Dob;
                    existingUser.IsAdmin = user.IsAdmin;
                    existingUser.Password = user.Password;
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
                    throw new Exception("User does not exists!");
                }
            }
        }
        public User CheckLogin(string name, string password)
        {
            using (var _context = new CinemaManagementContext())
            {
                var existingUser = _context.Users.FirstOrDefault(x => x.UserName == name);
                if (existingUser != null)
                {
                    if(password.Equals(existingUser.Password))
                    {
                        existingUser.Tickets = _context.Entry(existingUser).Collection(m => m.Tickets).Query().ToList();
                        return existingUser;
                    }
                    else
                    {
                        throw new Exception("Invalid Password!");
                    }
                }
                else
                {
                    throw new Exception("User does not exists!");
                }
            }
        }
        public bool DeleteUserById(int id)
        {
            using (var _context = new CinemaManagementContext())
            {
                var existingUser = _context.Users.FirstOrDefault(x => x.UserId == id);
                if (existingUser != null)
                {
                    try
                    {
                        _context.Users.Remove(existingUser);
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
}