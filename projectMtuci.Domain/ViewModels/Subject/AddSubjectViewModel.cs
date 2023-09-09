using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectMtuci.Domain.ViewModels.Subject
{
    public class AddSubjectViewModel : SubjectViewModel
    {
        public IFormFile Upload { get; set; }
    }
}
