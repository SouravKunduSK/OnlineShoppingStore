//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineShoppingStore.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_CartStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_CartStatus()
        {
            this.tbl_Cart = new HashSet<tbl_Cart>();
        }
    
        public int CartStatusId { get; set; }
        public string CartStatus { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Cart> tbl_Cart { get; set; }
    }
}
