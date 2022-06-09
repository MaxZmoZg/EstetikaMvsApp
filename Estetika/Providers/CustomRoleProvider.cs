using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Estetika.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            string[] roles = new string[] { };
            using (SalonEntities db = new SalonEntities())
            {
                // Получаем пользователя
                Polzovatel user = db.Polzovatel.FirstOrDefault(u => u.Electronnya_Pochta == username);
                if (user != null && user.Tip_Polzovatel != null)
                {
                    // получаем роль
                    roles = new string[] { user.Tip_Polzovatel.Imya_Tip_Polzovatel };
                }
                return roles;
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            bool outputResult = false;
            using (SalonEntities db = new SalonEntities())
            {
                // Получаем пользователя
                Polzovatel user = db.Polzovatel.FirstOrDefault(u => u.Login == username);
                if (user != null)
                {
                    Tip_Polzovatel userType = db.Tip_Polzovatel.Find(user.ID_Tip_Polzovatel);
                    if (userType != null && userType.Imya_Tip_Polzovatel != null && userType.Imya_Tip_Polzovatel == roleName)
                        outputResult = true;
                }
            }
            return outputResult;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}