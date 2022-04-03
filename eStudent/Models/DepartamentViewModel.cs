using System.ComponentModel.DataAnnotations;

namespace eStudent.Models
{
    public class DepartamentViewModel
    {
        [Key]
        public int ID { get; set; }

        public string Department { get; set; }

    }
}
