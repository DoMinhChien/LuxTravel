//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rate_Types
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rate_Types()
        {
            this.Rates = new HashSet<Rate>();
        }
    
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string Icon { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.Guid> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rate> Rates { get; set; }
    }
}