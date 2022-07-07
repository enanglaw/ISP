using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISPoliceAppApi.DTOs
{
   
        public partial class PlanCreationDTO
        {
            public string Title { get; set; }
            public string documentUrl { get; set; }

        public bool isActive;

        }
        
        public partial class PlanDownLoadDTO
        {
            public string title { get; set; }
        public string documentUrl { get; set; }
        public bool isActive;

    }
   
}
