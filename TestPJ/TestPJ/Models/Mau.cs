using System;
using System.Collections.Generic;

namespace TestPJ.Models
{
    public partial class Mau
    {
        public Mau()
        {
            PhuongTiens = new HashSet<PhuongTien>();
        }

        public string Mamau { get; set; } = null!;
        public string? Tenmau { get; set; }

        public virtual ICollection<PhuongTien> PhuongTiens { get; set; }
    }
}
