//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Estetika
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tip_Polzovatel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tip_Polzovatel()
        {
            this.Polzovatel = new HashSet<Polzovatel>();
        }
    
        public int ID_Tip_Polzovatel { get; set; }
        public string Imya_Tip_Polzovatel { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Polzovatel> Polzovatel { get; set; }
    }
}
