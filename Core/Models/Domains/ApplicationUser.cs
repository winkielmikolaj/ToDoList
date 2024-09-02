using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;

namespace ToDoList.Core.Models.Domains
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Tasks = new Collection<Task>();
        }

        public ICollection<Task> Tasks { get; set; }
    }
}
