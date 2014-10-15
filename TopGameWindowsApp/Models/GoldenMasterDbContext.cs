
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TopGameWindowsApp.Models;

namespace TopGameWindowsApp.Models
{
    public class GoldenMasterDbContext : DbContext
    {
        public GoldenMasterDbContext()
            : base("name=GoldenMasterDBContext2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VitalStatistics>()
                .HasRequired(v => v.innerPath)
                .WithMany()
                .HasForeignKey(v => v.innerPathId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VitalStatistics>()
                .HasRequired(v => v.outerPath)
                .WithMany()
                .HasForeignKey(v => v.outerPathId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VitalStatistics>()
                .HasRequired(v => v.startArmDivisionStarts)
                .WithMany()
                .HasForeignKey(v => v.startArmDivisionStartsId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VitalStatistics>()
                .HasRequired(v => v.startArmDivisionEnds)
                .WithMany()
                .HasForeignKey(v => v.startArmDivisionEndsId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VitalStatistics>()
                .HasRequired(v => v.endArmDivisionStarts)
                .WithMany()
                .HasForeignKey(v => v.endArmDivisionStartsId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VitalStatistics>()
                .HasRequired(v => v.endArmDivisionEnds)
                .WithMany()
                .HasForeignKey(v => v.endArmDivisionEndsId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VitalStatistics>()
                .HasRequired(v => v.arcSpokes)
                .WithMany()
                .HasForeignKey(v => v.arcSpokesId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VitalStatistics>()
                .HasRequired(v => v.innerArcSquare)
                .WithMany()
                .HasForeignKey(v => v.innerArcSquareId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VitalStatistics>()
                .HasRequired(v => v.outerArcSquare)
                .WithMany()
                .HasForeignKey(v => v.outerArcSquareId)
                .WillCascadeOnDelete(false);
        }

        // Each one of these records represents the results of a single call to PrepareActualData
        public DbSet<GoldenMasterSinglePass> CallsToPrepareActualData { get; set; }
        public DbSet<TopGamePoint> TopGamePoints { get; set; }
    }
}
