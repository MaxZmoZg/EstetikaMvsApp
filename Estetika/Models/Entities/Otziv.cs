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
    
    public partial class Otziv
    {
        public int ID_Otziv { get; set; }
        public string Napisanny_Otziv { get; set; }
        public Nullable<int> ID_Polzovatel { get; set; }
    
        public virtual Polzovatel Polzovatel { get; set; }
    }
}
