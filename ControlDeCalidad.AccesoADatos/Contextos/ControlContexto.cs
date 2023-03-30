using ControlDeCalidad.AccesoADatos.Contratos;
using ControlDeCalidad.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ControlDeCalidad.AccesoADatos.Contextos
{
    public class ControlContexto : DbContext, IUnidadDeTrabajoEF
    {
        public ControlContexto()
        {

        }

        //Al parecer el inyector de dependencia se encarga de esto(Ver la clase)
        public ControlContexto(DbContextOptions<ControlContexto> options)
        : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ControlDeCalidad;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder
            //    .Entity<Entidad>()
            //    .ToTable("Entidades")
            //    .Ignore(p => p.Propiedad)
            //    .HasMany(p => p.Agregaciones)
            //    .WithOne()
            //    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Color>()
                .ToTable("Colores");
            modelBuilder
                .Entity<Modelo>()
                .ToTable("Modelos");
            modelBuilder
                .Entity<Empleado>()
                .ToTable("Empleados");
            modelBuilder
                .Entity<Defecto>()
                .ToTable("Defectos");
            modelBuilder
                .Entity<RegistroDeDefecto>()
                .ToTable("RegistrosDeDefecto");
            modelBuilder
                .Entity<ParDePrimera>()
                .ToTable("ParesDePrimera");
            modelBuilder
                .Entity<Jornada>()
                .ToTable("Jornadas");
            modelBuilder
                .Entity<Semaforo>()
                .ToTable("Semaforos");
            modelBuilder
                .Entity<OrdenDeProduccion>()
                .ToTable("OrdenesDeProduccion")
                .Ignore(op => op.JornadaActiva);
        }

        public void Confirmar()
        {
            SaveChanges();
        }

        public DbSet<T> CrearSet<T>() where T : class
        {
            return Set<T>();
        }

        public void Refrescar<T>(T item) where T : class
        {
            if (Entry(item).State != EntityState.Detached)
            {
                Attach(item);
            }
            base.Update(item);
        }

        public void RevertirCambios()
        {
            throw new NotImplementedException();
        }

        public void SetModificado<T>(T item) where T : class
        {
            Entry(item).State = EntityState.Modified;
        }

        //public void RelacionarEntidad<T>(T item, params string[] entidades) where T : class
        public void RelacionarEntidad<T,E>(T item, Expression<Func<T,E>> entidad)
            where T : class
            where E : class
        {
            Entry<T>(item).Reference(entidad).Load();
        }

        //public void RelacionarColeccion<T, C>(T item, params string[] colecciones)
        public void RelacionarColeccion<T, C>(T item, Expression<Func<T, IEnumerable<C>>> coleccion)
            where T : class
            where C : class
        {
            Entry<T>(item).Collection(coleccion).Load();
        }
    }
}
