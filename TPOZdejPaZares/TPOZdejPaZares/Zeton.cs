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
    
    public partial class Zeton
    {
        public int idZeton { get; set; }
        public Nullable<bool> Izkoriscen { get; set; }
        public Nullable<int> Student_idStudent { get; set; }
        public Nullable<int> Vpis_idVpis { get; set; }
        public Nullable<int> studijskiProgram { get; set; }
        public Nullable<int> letnik { get; set; }
        public Nullable<int> vrstaVpisa { get; set; }
        public Nullable<int> nacinStudija { get; set; }
        public Nullable<int> oblikaStudija { get; set; }
        public Nullable<int> prostaIzbira { get; set; }
    
        public virtual Student Student { get; set; }
        public virtual Vpis Vpis { get; set; }
    }
}
