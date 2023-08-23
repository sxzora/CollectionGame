using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoDataAccessLayer
{
    public class Todo
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public DateTime DateLimite { get; set; }
        public int Priorite { get; set; }
        public bool IsFinished { get; set; }
    }
}
