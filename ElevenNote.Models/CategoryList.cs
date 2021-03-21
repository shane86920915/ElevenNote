using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class CategoryList
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int NoteId { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? UpdatedUtc { get; set; }
    }
}
