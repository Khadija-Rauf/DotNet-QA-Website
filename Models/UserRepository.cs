using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Identity.Client;
using Semester_Project.Models.Interfaces;

namespace Semester_Project.Models
{
    public class UserRepository:IUserRepository
    {
        ProjectContext context = new ProjectContext();
         public void Add(User u)
        {
                context.Users.Add(u);
        }
        public void Save() { 
                context.SaveChanges();
        }
        public int[] authenticateUser(User user)
        {
            int []val = new int[2];
            //LINQ query]]]
            var data = context.Users.SingleOrDefault(s => s.Name.Equals(user.Name) && s.Password.Equals(user.Password));
            if (data != null)
            {
                val[0] = int.Parse(data.UserType);
                val[1]= 1;
            }
            else
            {
                val[0] = 2;
                val[1]= 0;
            }
            return val;
        }
        public User show(string Name)
        {
            User u = new User();
            var users = from m in context.Users
                         select m;
            foreach (var user in users) {
                if(Name.Equals(user.Name))
                {
                    u = user;
                }
            }
            return  u;
        }
        }

    }
