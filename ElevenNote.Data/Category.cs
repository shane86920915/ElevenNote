using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Data
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        [ForeignKey(nameof(Note))]
        public int NoteId { get; set; }
        public virtual  Note Note { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }   
}
