using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.ValueObject
{
    public class ThongTinSanPham
    {
        public string SysId { get; set; }
        public string ProductID { get; set; }

        public string ProductName { get; set; }
        public string Description { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool Status { get; set; }
        public string LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public string DonViTinh { get; set; }
        public Int32 HeSoAnToan { get; set; }
        public string ProductGroup { get; set; }
        
    }
}
