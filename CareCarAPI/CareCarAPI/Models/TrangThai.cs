using System;
using System.Collections.Generic;

namespace CareCarAPI.Models
{
    public partial class TrangThai
    {
        public TrangThai()
        {
            LichHens = new HashSet<LichHen>();
        }

        public int TrangThaiId { get; set; }
        public string? TenTrangThai { get; set; }

        public virtual ICollection<LichHen> LichHens { get; set; }
    }
}
