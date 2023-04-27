using System.ComponentModel;

namespace StudentApp.Models
{
    public class updateStudentViewmodel
    {
     
            public int Id { get; set; }

            [DisplayName("First Name")]
            public string FirstName { get; set; }

            [DisplayName("Last Name")]
            public string LastName { get; set; }

            public string Email { get; set; }
        
    }
}
