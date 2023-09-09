using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectMtuci.Domain.Entity
{
    public class Subject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Course { get; set; }

        public string Degree { get; set; }

        public string PathToFile { get; set; }
    }
}
