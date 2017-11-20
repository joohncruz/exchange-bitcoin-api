using System.Collections.Generic;

namespace WebApi.Models
{
    public static class BaseUsuarios
    {
        public static IEnumerable<User> Users()
        {
            return new List<User>
            {
                new User {  Id = 1, UserName = "joohncruz", Email = "joohncruzrocha@gmail.com", Password = "123"}
            };
        }
    }
}