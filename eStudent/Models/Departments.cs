using System.ComponentModel.DataAnnotations;

namespace eStudent.Models
{
    public class Departments
    {
        [Key]
        public int ID { get; set; }

        public string Department { get; set; }

    }
}
