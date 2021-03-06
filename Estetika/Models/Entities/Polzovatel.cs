//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Estetika.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Polzovatel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Polzovatel()
        {
            this.Otvet = new HashSet<Otvet>();
            this.Otziv = new HashSet<Otziv>();
            this.Tovar = new HashSet<Tovar>();
            this.Zapis = new HashSet<Zapis>();
        }
    
        public int ID_Polzovatel { get; set; }
        public string Imya { get; set; }
        public string Phamilia { get; set; }
        public string Electronnya_Pochta { get; set; }
        public Nullable<long> Telephon { get; set; }
        public string Login { get; set; }
        public string Parol { get; set; }
        public int ID_Tip_Polzovatel { get; set; }
        public bool IsDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Otvet> Otvet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Otziv> Otziv { get; set; }
        public virtual Tip_Polzovatel Tip_Polzovatel { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tovar> Tovar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zapis> Zapis { get; set; }
    }
}
