using System.ComponentModel.DataAnnotations.Schema;


namespace LoggingBestPract.Domain.Entities
{
    [Table("Users")]
    public class Users : Entity <int>
    {
        public Users(string name, string password, string surname, string phone, string email)
        {
            Name = name;
            Password = password;
            Surname = surname;
            Phone = phone;
            Email = email;
        }
        
        [Column("Name")]
        public string Name { get; set; }
        
        [Column("Surname")]
        public string Surname { get; set; }
        
        [Column("Phone")]
        public string Phone { get; set; }
        
        [Column("Email")]
        public string Email { get; set; }
        
        [Column("Password")]
        public string Password { get; set; }

    }
}
