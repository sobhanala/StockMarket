using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.DataAccess.Model
{
    [Table("Users")]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; }
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}