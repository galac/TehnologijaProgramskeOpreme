//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TPOZdejPaZares
{
    using System;
    using System.Collections.Generic;
    
    public partial class IzpitniRok
    {
        public IzpitniRok()
        {
            this.PrijavaNaIzpit = new HashSet<PrijavaNaIzpit>();
        }
    
        public int IDIzpitniRok { get; set; }
        public Nullable<System.DateTime> DatumIzpitnegaRoka { get; set; }
        public string VrstaIzpitnegaRoka { get; set; }
        public string Prostor { get; set; }
        public Nullable<int> SteviloMest { get; set; }
        public Nullable<int> IzvedbaPredmeta_idIzvedbaPredmeta { get; set; }
        public Nullable<int> Veljaven { get; set; }
    
        public virtual ICollection<PrijavaNaIzpit> PrijavaNaIzpit { get; set; }
        public virtual IzvedbaPredmeta IzvedbaPredmeta { get; set; }
    }
}
