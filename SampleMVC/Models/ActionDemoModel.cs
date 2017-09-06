using System.ComponentModel.DataAnnotations;

namespace SampleMVC.Models
{
    public class ActionDemoModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Address { get; set; }

        [EmailAddress]
        public string Email { get; set; }

    }
}