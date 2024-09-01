using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.ApplicationService.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public UserDto(DataAccess.Model.User userModel)
        {
            this.Id = userModel.Id;
            this.Name = userModel.Name;
        }
    }
}