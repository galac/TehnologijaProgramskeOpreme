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
    
    public partial class Vpis
    {
        public Vpis()
        {
            this.Zeton = new HashSet<Zeton>();
            this.VpisaniPredmet = new HashSet<VpisaniPredmet>();
        }
    
        public int idVpis { get; set; }
        public int VrstaVpisa_idVrstaVpisa { get; set; }
        public int OblikaStudija_idOblikaStudija { get; set; }
        public int Letnik_idLetnik { get; set; }
        public int StudijskiProgram_idStudijskiProgram { get; set; }
        public int NacinStudija_idNacinStudija { get; set; }
        public int Student_idStudent1 { get; set; }
        public int Potrjen { get; set; }
        public string StudijskoLeto { get; set; }
    
        public virtual Letnik Letnik { get; set; }
        public virtual NacinStudija NacinStudija { get; set; }
        public virtual OblikaStudija OblikaStudija { get; set; }
        public virtual Student Student { get; set; }
        public virtual StudijskiProgram StudijskiProgram { get; set; }
        public virtual ICollection<Zeton> Zeton { get; set; }
        public virtual VrstaVpisa VrstaVpisa { get; set; }
        public virtual ICollection<VpisaniPredmet> VpisaniPredmet { get; set; }
    }
}
