
//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------


namespace BienesRaices.Models
{

using System;
    using System.Collections.Generic;
    
public partial class Imagen_I
{

    public int Id_Imagen_I { get; set; }

    public string Ruta_I { get; set; }

    public Nullable<int> Id_Propiedad_I { get; set; }



    public virtual Propiedad_P Propiedad_P { get; set; }

}

}
