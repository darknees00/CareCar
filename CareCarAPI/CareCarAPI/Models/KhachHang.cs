using System;
using System.Collections.Generic;

namespace CareCarAPI.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            LichHens = new HashSet<LichHen>();
        }

        public int KhachHangId { get; set; }
        public string? TenKhachHang { get; set; }
        public string? SoDienThoai { get; set; }
        public string? DiaChi { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<LichHen> LichHens { get; set; }
    }
}
