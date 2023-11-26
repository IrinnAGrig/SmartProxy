using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
     public class Event : MongoDocument
     {
          public string Title { get; set; }
          public string Date { get; set; }
          public string Hour { get; set; }
          public string Location { get; set; }
          public string Description { get; set; }
          public List<string> Organizators { get; set; }
     }
}
