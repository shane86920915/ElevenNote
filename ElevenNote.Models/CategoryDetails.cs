﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class CategoryDetails
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int NoteId { get; set; }
        public virtual List<NoteListItem> Notes { get; set; }

    }
}
