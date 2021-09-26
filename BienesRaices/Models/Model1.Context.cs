﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class db_a3cb5b_webbienesraicesEntities : DbContext
    {
        public db_a3cb5b_webbienesraicesEntities()
            : base("name=db_a3cb5b_webbienesraicesEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Categoria_CA> Categoria_CA { get; set; }
        public DbSet<Imagen_I> Imagen_I { get; set; }
        public DbSet<Propiedad_P> Propiedad_P { get; set; }
        public DbSet<Credenciales_CR> Credenciales_CR { get; set; }
        public DbSet<Contacto_CO> Contacto_CO { get; set; }
    
        public virtual int IngresaPedido(string nombreCompleto, string telefono, string correo, Nullable<int> idPropiedad, string estado, string comentario)
        {
            var nombreCompletoParameter = nombreCompleto != null ?
                new ObjectParameter("NombreCompleto", nombreCompleto) :
                new ObjectParameter("NombreCompleto", typeof(string));
    
            var telefonoParameter = telefono != null ?
                new ObjectParameter("Telefono", telefono) :
                new ObjectParameter("Telefono", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            var idPropiedadParameter = idPropiedad.HasValue ?
                new ObjectParameter("IdPropiedad", idPropiedad) :
                new ObjectParameter("IdPropiedad", typeof(int));
    
            var estadoParameter = estado != null ?
                new ObjectParameter("Estado", estado) :
                new ObjectParameter("Estado", typeof(string));
    
            var comentarioParameter = comentario != null ?
                new ObjectParameter("Comentario", comentario) :
                new ObjectParameter("Comentario", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("IngresaPedido", nombreCompletoParameter, telefonoParameter, correoParameter, idPropiedadParameter, estadoParameter, comentarioParameter);
        }
    
        public virtual int EstadoCuenta(Nullable<int> idCredencial)
        {
            var idCredencialParameter = idCredencial.HasValue ?
                new ObjectParameter("idCredencial", idCredencial) :
                new ObjectParameter("idCredencial", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EstadoCuenta", idCredencialParameter);
        }
    
        public virtual int IngresaCuenta(string credencial, string contrasena)
        {
            var credencialParameter = credencial != null ?
                new ObjectParameter("Credencial", credencial) :
                new ObjectParameter("Credencial", typeof(string));
    
            var contrasenaParameter = contrasena != null ?
                new ObjectParameter("Contrasena", contrasena) :
                new ObjectParameter("Contrasena", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("IngresaCuenta", credencialParameter, contrasenaParameter);
        }
    
        public virtual int ModicaCuenta(Nullable<int> idCredencial, string contrasena)
        {
            var idCredencialParameter = idCredencial.HasValue ?
                new ObjectParameter("idCredencial", idCredencial) :
                new ObjectParameter("idCredencial", typeof(int));
    
            var contrasenaParameter = contrasena != null ?
                new ObjectParameter("contrasena", contrasena) :
                new ObjectParameter("contrasena", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ModicaCuenta", idCredencialParameter, contrasenaParameter);
        }
    
        public virtual ObjectResult<MuestraCuenta_Result> MuestraCuenta()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MuestraCuenta_Result>("MuestraCuenta");
        }
    
        public virtual ObjectResult<RetornaCuenta_Result> RetornaCuenta(string usuario, string contrasena)
        {
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            var contrasenaParameter = contrasena != null ?
                new ObjectParameter("contrasena", contrasena) :
                new ObjectParameter("contrasena", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RetornaCuenta_Result>("RetornaCuenta", usuarioParameter, contrasenaParameter);
        }
    
        public virtual ObjectResult<RetornaCuentaID_Result> RetornaCuentaID(Nullable<int> id_Usuario)
        {
            var id_UsuarioParameter = id_Usuario.HasValue ?
                new ObjectParameter("id_Usuario", id_Usuario) :
                new ObjectParameter("id_Usuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RetornaCuentaID_Result>("RetornaCuentaID", id_UsuarioParameter);
        }
    
        public virtual ObjectResult<MostrarCategoria_Result> MostrarCategoria()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MostrarCategoria_Result>("MostrarCategoria");
        }
    
        public virtual int FinalizarVenta(Nullable<int> idContacto)
        {
            var idContactoParameter = idContacto.HasValue ?
                new ObjectParameter("idContacto", idContacto) :
                new ObjectParameter("idContacto", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FinalizarVenta", idContactoParameter);
        }
    
        public virtual int RechazarVenta(Nullable<int> idContacto)
        {
            var idContactoParameter = idContacto.HasValue ?
                new ObjectParameter("idContacto", idContacto) :
                new ObjectParameter("idContacto", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RechazarVenta", idContactoParameter);
        }
    
        public virtual ObjectResult<MuestralosPedidos_Result> MuestralosPedidos()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MuestralosPedidos_Result>("MuestralosPedidos");
        }
    
        public virtual ObjectResult<MostrarReporte_Result> MostrarReporte()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MostrarReporte_Result>("MostrarReporte");
        }
    
        public virtual int IngresaPropiedad(string nombrePro, Nullable<decimal> precioPro, Nullable<int> cantidadCuarto, Nullable<int> cantidadBano, Nullable<int> cantidadGarajes, Nullable<int> categoriaPro, string ruta)
        {
            var nombreProParameter = nombrePro != null ?
                new ObjectParameter("NombrePro", nombrePro) :
                new ObjectParameter("NombrePro", typeof(string));
    
            var precioProParameter = precioPro.HasValue ?
                new ObjectParameter("PrecioPro", precioPro) :
                new ObjectParameter("PrecioPro", typeof(decimal));
    
            var cantidadCuartoParameter = cantidadCuarto.HasValue ?
                new ObjectParameter("CantidadCuarto", cantidadCuarto) :
                new ObjectParameter("CantidadCuarto", typeof(int));
    
            var cantidadBanoParameter = cantidadBano.HasValue ?
                new ObjectParameter("CantidadBano", cantidadBano) :
                new ObjectParameter("CantidadBano", typeof(int));
    
            var cantidadGarajesParameter = cantidadGarajes.HasValue ?
                new ObjectParameter("CantidadGarajes", cantidadGarajes) :
                new ObjectParameter("CantidadGarajes", typeof(int));
    
            var categoriaProParameter = categoriaPro.HasValue ?
                new ObjectParameter("CategoriaPro", categoriaPro) :
                new ObjectParameter("CategoriaPro", typeof(int));
    
            var rutaParameter = ruta != null ?
                new ObjectParameter("Ruta", ruta) :
                new ObjectParameter("Ruta", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("IngresaPropiedad", nombreProParameter, precioProParameter, cantidadCuartoParameter, cantidadBanoParameter, cantidadGarajesParameter, categoriaProParameter, rutaParameter);
        }
    
        public virtual int ModificarPropiedad(Nullable<int> idPropi, string nombrePro, Nullable<decimal> precio, Nullable<int> cantidadba, Nullable<int> cantidadCuar, Nullable<int> cantidadGara, string estado, Nullable<int> categoria, string ruta)
        {
            var idPropiParameter = idPropi.HasValue ?
                new ObjectParameter("idPropi", idPropi) :
                new ObjectParameter("idPropi", typeof(int));
    
            var nombreProParameter = nombrePro != null ?
                new ObjectParameter("NombrePro", nombrePro) :
                new ObjectParameter("NombrePro", typeof(string));
    
            var precioParameter = precio.HasValue ?
                new ObjectParameter("Precio", precio) :
                new ObjectParameter("Precio", typeof(decimal));
    
            var cantidadbaParameter = cantidadba.HasValue ?
                new ObjectParameter("Cantidadba", cantidadba) :
                new ObjectParameter("Cantidadba", typeof(int));
    
            var cantidadCuarParameter = cantidadCuar.HasValue ?
                new ObjectParameter("CantidadCuar", cantidadCuar) :
                new ObjectParameter("CantidadCuar", typeof(int));
    
            var cantidadGaraParameter = cantidadGara.HasValue ?
                new ObjectParameter("CantidadGara", cantidadGara) :
                new ObjectParameter("CantidadGara", typeof(int));
    
            var estadoParameter = estado != null ?
                new ObjectParameter("Estado", estado) :
                new ObjectParameter("Estado", typeof(string));
    
            var categoriaParameter = categoria.HasValue ?
                new ObjectParameter("Categoria", categoria) :
                new ObjectParameter("Categoria", typeof(int));
    
            var rutaParameter = ruta != null ?
                new ObjectParameter("Ruta", ruta) :
                new ObjectParameter("Ruta", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ModificarPropiedad", idPropiParameter, nombreProParameter, precioParameter, cantidadbaParameter, cantidadCuarParameter, cantidadGaraParameter, estadoParameter, categoriaParameter, rutaParameter);
        }
    
        public virtual ObjectResult<MostrarPropiedadID_Result> MostrarPropiedadID(Nullable<int> idPropiedad)
        {
            var idPropiedadParameter = idPropiedad.HasValue ?
                new ObjectParameter("idPropiedad", idPropiedad) :
                new ObjectParameter("idPropiedad", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MostrarPropiedadID_Result>("MostrarPropiedadID", idPropiedadParameter);
        }
    
        public virtual int OcultarPropiedad(Nullable<int> idPropiedad, string estado)
        {
            var idPropiedadParameter = idPropiedad.HasValue ?
                new ObjectParameter("IdPropiedad", idPropiedad) :
                new ObjectParameter("IdPropiedad", typeof(int));
    
            var estadoParameter = estado != null ?
                new ObjectParameter("Estado", estado) :
                new ObjectParameter("Estado", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("OcultarPropiedad", idPropiedadParameter, estadoParameter);
        }
    
        public virtual ObjectResult<MostrarPropiedad_Result> MostrarPropiedad(Nullable<int> precio02, Nullable<int> canCuato, Nullable<int> canBano, Nullable<int> canGara, Nullable<int> cate)
        {
            var precio02Parameter = precio02.HasValue ?
                new ObjectParameter("Precio02", precio02) :
                new ObjectParameter("Precio02", typeof(int));
    
            var canCuatoParameter = canCuato.HasValue ?
                new ObjectParameter("CanCuato", canCuato) :
                new ObjectParameter("CanCuato", typeof(int));
    
            var canBanoParameter = canBano.HasValue ?
                new ObjectParameter("CanBano", canBano) :
                new ObjectParameter("CanBano", typeof(int));
    
            var canGaraParameter = canGara.HasValue ?
                new ObjectParameter("CanGara", canGara) :
                new ObjectParameter("CanGara", typeof(int));
    
            var cateParameter = cate.HasValue ?
                new ObjectParameter("Cate", cate) :
                new ObjectParameter("Cate", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MostrarPropiedad_Result>("MostrarPropiedad", precio02Parameter, canCuatoParameter, canBanoParameter, canGaraParameter, cateParameter);
        }
    
        public virtual ObjectResult<MostrarPropiedadAdmin_Result> MostrarPropiedadAdmin(Nullable<int> precio02, Nullable<int> canCuato, Nullable<int> canBano, Nullable<int> canGara, Nullable<int> cate, string estado)
        {
            var precio02Parameter = precio02.HasValue ?
                new ObjectParameter("Precio02", precio02) :
                new ObjectParameter("Precio02", typeof(int));
    
            var canCuatoParameter = canCuato.HasValue ?
                new ObjectParameter("CanCuato", canCuato) :
                new ObjectParameter("CanCuato", typeof(int));
    
            var canBanoParameter = canBano.HasValue ?
                new ObjectParameter("CanBano", canBano) :
                new ObjectParameter("CanBano", typeof(int));
    
            var canGaraParameter = canGara.HasValue ?
                new ObjectParameter("CanGara", canGara) :
                new ObjectParameter("CanGara", typeof(int));
    
            var cateParameter = cate.HasValue ?
                new ObjectParameter("Cate", cate) :
                new ObjectParameter("Cate", typeof(int));
    
            var estadoParameter = estado != null ?
                new ObjectParameter("Estado", estado) :
                new ObjectParameter("Estado", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MostrarPropiedadAdmin_Result>("MostrarPropiedadAdmin", precio02Parameter, canCuatoParameter, canBanoParameter, canGaraParameter, cateParameter, estadoParameter);
        }
    }
}
