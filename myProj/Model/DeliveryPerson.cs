using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myProj.Model
{
    public class DeliveryPerson
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public static async Task<DeliveryPerson> GetDeliveryPerson(string id)
        {
            DeliveryPerson person = new DeliveryPerson();

            person = (await AzureHelper.MobileService.GetTable<DeliveryPerson>().Where(d=>d.Id == id).ToListAsync()).FirstOrDefault();

            return person;
        }

        public static async Task<string> Login(string email, string password)
        {
            string userId = String.Empty;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                 userId = String.Empty;
            }
            else
            {
                var user = (await AzureHelper.MobileService.GetTable<DeliveryPerson>().Where(u => u.Email == email).ToListAsync()).FirstOrDefault();

                if (user.Password == password)
                {
                    userId = user.Id;
                }
                else
                {
                    userId = String.Empty;
                }
            }

            return userId;
        }

        public static async Task<bool> RegisterUser(string email, string password, string confirmPassword)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(password))
            {
                if (password == confirmPassword)
                {
                    var user = new DeliveryPerson()
                    {
                        Email = email,
                        Password = password
                    };
                    await AzureHelper.Insert(user);

                    result = true;
                }
            }
            return result;
        }
    }
}
