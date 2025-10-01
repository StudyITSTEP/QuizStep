using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizStep.Application.DTOs.User
{
    public class UserWithRolesDto
    {
        public string Id { get; set; } = default!;
        public string Email { get; set; } = default!;
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
