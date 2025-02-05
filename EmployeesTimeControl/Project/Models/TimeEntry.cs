using System.ComponentModel.DataAnnotations;

namespace EmployeesTimeControl.Models
{
    public class TimeEntry
    {
        public int entryId { get; set; }
        public int employeeId { get; set; }
        public string date { get; set; }

        [Required]
        [Range(1, 24)]
        public int hoursWorked { get; set; }

    } 
}

