using System.ComponentModel.DataAnnotations;

namespace CRUD.DB
{
    public class EmployeeDomain
    {
        /// <summary>
        /// Employee id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Employee name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Employee age
        /// </summary>
        public int Age { get; set; }

        public DateTime DateOfJoining { get; set; }
    }
}
