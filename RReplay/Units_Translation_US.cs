//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RReplay
{
    using System;
    using System.Collections.Generic;
    
    public partial class Units_Translation_US
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Units_Translation_US()
        {
            this.TUniteAuSol = new HashSet<TUniteAuSol>();
        }
    
        public byte[] Hash { get; set; }
        public string Translation { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TUniteAuSol> TUniteAuSol { get; set; }
    }
}