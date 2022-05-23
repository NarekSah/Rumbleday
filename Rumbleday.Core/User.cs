using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace Rumbleday.Core
{
    public class User:IEquatable<User>
    {
        private string path = Path.Combine(Directory.GetCurrentDirectory(), "users.json");
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            User objAsPart = obj as User;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }

        public bool Equals(User other)
        {
            if (other == null) return false;
            return (this.Login.Equals(other.Login) && this.Password.Equals(other.Password));
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public void Save()
        {
            if (SaveValidator())
            {
                string user = JsonConvert.SerializeObject(this);
                File.AppendAllText(path, user);
            }
            else
            {
                throw new Exception("User already exists");
            }            
        }

        public bool ValidLogin()
        {
            string usersStr = File.ReadAllText(path);

            if (!String.IsNullOrEmpty(usersStr))
            {
                List<User> users = JsonConvert.DeserializeObject<List<User>>(usersStr);
                if (users.Contains(this))
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
            return false;
        }

        private bool SaveValidator()
        {            
            string usersStr = File.ReadAllText(path);
            if (!String.IsNullOrEmpty(usersStr))
            {
                List<User> users = JsonConvert.DeserializeObject<List<User>>(usersStr);
                foreach(var user in users)
                {
                    if(this.Login == user.Login)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                return true;
            }  
            return false;
        }
    }
}
