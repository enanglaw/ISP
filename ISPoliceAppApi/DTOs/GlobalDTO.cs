using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.DTOs
{
    public class GlobalCreationDTO
    {
        public string Name { get; set; }
    }
    public class GlobalUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
