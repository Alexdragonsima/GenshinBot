using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GenshinBot.Models.DataBase;

namespace GenshinBot.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Element> Elements { get; set; }
        public DbSet<WeaponType> WeaponTypes { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Rarity> Rarities { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<CharacterTeam> CharacterTeams { get; set; }
        public DbSet<TeamRotation> TeamRotations { get; set; }
        public DbSet<Artifact> Artifacts { get; set; }
        public DbSet<CharacterArtifact> CharacterArtifacts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка уникальных ограничений
            modelBuilder.Entity<Character>()
                .HasIndex(c => c.CharacterName)
                .IsUnique();

            modelBuilder.Entity<CharacterTeam>()
                .HasIndex(ct => new { ct.CharacterID, ct.TeamID })
                .IsUnique();

            // Настройка отнощений
            modelBuilder.Entity<Character>()
                .HasOne(c => c.Element)
                .WithMany(e => e.Characters)
                .HasForeignKey(c => c.ElementID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Character>()
                .HasOne(c=>c.WeaponType)
                .WithMany(w=>w.Characters)
                .HasForeignKey(c=>c.WeaponTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CharacterTeam>()
                .HasOne(ct=>ct.Character)
                .WithMany(c=>c.CharacterTeams)
                .HasForeignKey(ct=>ct.CharacterID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CharacterTeam>()
                .HasOne(ct=>ct.Team)
                .WithMany(t=>t.CharacterTeams)
                .HasForeignKey(ct=>ct.TeamID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
