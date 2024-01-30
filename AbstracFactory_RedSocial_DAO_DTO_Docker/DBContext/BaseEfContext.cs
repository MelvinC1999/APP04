using AbstracFactory_RedSocial_DAO_DTO_Docker.Models;
using Microsoft.EntityFrameworkCore;

namespace AbstracFactory_RedSocial_DAO_DTO_Docker
{
    public class BaseEfContext : DbContext
    {
        public BaseEfContext()
        {
        }

        public BaseEfContext(DbContextOptions<BaseEfContext> options) : base(options)
        {
        }

        public virtual DbSet<RedSocial_DTO> Redes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Documentos\\Repositorio-VisualStudio\\CodigoPrimero2\\CodigoPrimero2\\DataCodeFirst2.mdf;Integrated Security=True");
    }
}
