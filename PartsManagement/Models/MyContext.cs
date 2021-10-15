using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PartsManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace PartsManagement.Models
{
    public class MyContext : IdentityDbContext<User>
    {
        public MyContext(DbContextOptions options) : base(options) { }
   
        public DbSet<Komenti> Komentet { get; set; }
        public DbSet<Produkti> Produktet { get; set; }
        public DbSet<Porosia> Porosite { get; set; }
        public DbSet<Sektori> Sektoret { get; set; }
        public DbSet<FaturaIN> FaturatIN { get; set; }
        public DbSet<FaturaOUT> FaturatOUT { get; set; }
        public DbSet<Furnitori> Furnitoret { get; set; }
        public DbSet<Contact> Pyetjet { get; set; }
        public DbSet<Modeli> Modelet { get; set; }
        public DbSet<Marka> Markat { get; set; }
        public DbSet<Shteti> Shtetet { get; set; }
        public DbSet<Qyteti> Qytetet { get; set; }


        protected override void OnModelCreating(ModelBuilder model)
        {
          base.OnModelCreating(model);
          
          
          model.Entity<Shteti>().HasData(
            new Shteti() { ShtetiId = 1, Emri = "Kosovë" },
            new Shteti() { ShtetiId = 2, Emri = "Shqipëri" },         
            new Shteti() { ShtetiId = 3, Emri = "Maqedoni" }
            );

            model.Entity<Marka>().HasData(
            new Marka() { MarkaId = 1, Emri = "Ferrari" },
            new Marka() { MarkaId = 2, Emri = "Audi" },
            new Marka() { MarkaId = 3, Emri = "BMW" },
            new Marka() { MarkaId = 4, Emri = "VolksWagen" },
            new Marka() { MarkaId = 5, Emri = "Mercedes" },
            new Marka() { MarkaId = 6, Emri = "Skoda" },
            new Marka() { MarkaId = 7, Emri = "Volvo" },
            new Marka() { MarkaId = 8, Emri = "Toyota" },                 
            new Marka() { MarkaId = 9, Emri = "Mitsubishi" },
            new Marka() { MarkaId = 10, Emri = "Porsche" }

            );

            //model.Entity<Shitja>()
            //.HasOne(a => a.Fatura)
            //.WithOne(a => a.Shitja)
            //.HasForeignKey<FaturaOUT>(c => c.ShitjaId);

            model.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "Puntor",
                    NormalizedName = "PUNTOR"
                }
                );

            model.Entity<Modeli>().HasData(
            new Modeli() { ModelId = 1, Emri = "Ferrari Ri" , MarkaId=1},
            new Modeli() { ModelId = 2, Emri = "Audi A3", MarkaId=2 },
            new Modeli() { ModelId = 3, Emri = "BMW 5qe" , MarkaId=3},
            new Modeli() { ModelId = 4, Emri = "Golf 4shi Bajramit", MarkaId=4 },
            new Modeli() { ModelId = 5, Emri = "Mercedes e class", MarkaId=5 },
            new Modeli() { ModelId = 6, Emri = "Skoda octavia" , MarkaId=6},
            new Modeli() { ModelId = 7, Emri = "Volvo 3.0 tdi", MarkaId=7 },
            new Modeli() { ModelId = 8, Emri = "Toyota off-road", MarkaId=8 },                 
            new Modeli() { ModelId = 9, Emri = "Mitsubishi modeli 2t", MarkaId=9 },
            new Modeli() { ModelId = 10, Emri = "Porsche panamera" , MarkaId=10}

            );

        model.Entity<Qyteti>().HasData(

             new Qyteti() { QytetiId = 1, ShtetiId = 1, Emri = "Artanë" },
             new Qyteti() { QytetiId = 2, ShtetiId = 1, Emri = "Besianë" },
             new Qyteti() { QytetiId = 3, ShtetiId = 1, Emri = "Burim" },
             new Qyteti() { QytetiId = 4, ShtetiId = 1, Emri = "Dardanë" },
             new Qyteti() { QytetiId = 5, ShtetiId = 1, Emri = "Decan" },
             new Qyteti() { QytetiId = 6, ShtetiId = 1, Emri = "Dragash" },
             new Qyteti() { QytetiId = 7, ShtetiId = 1, Emri = "Drenas" },
             new Qyteti() { QytetiId = 8, ShtetiId = 1, Emri = "Ferizaj" },
             new Qyteti() { QytetiId = 9, ShtetiId = 1, Emri = "Fushë Kosovë" },
             new Qyteti() { QytetiId = 10, ShtetiId = 1, Emri = "Gjakovë" },
             new Qyteti() { QytetiId = 11, ShtetiId = 1, Emri = "Gjilan" },
             new Qyteti() { QytetiId = 12, ShtetiId = 1, Emri = "Kastriot" },
             new Qyteti() { QytetiId = 13, ShtetiId = 1, Emri = "Kaqanik" },
             new Qyteti() { QytetiId = 14, ShtetiId = 1, Emri = "Klinë" },
             new Qyteti() { QytetiId = 15, ShtetiId = 1, Emri = "Leposaviq" },
             new Qyteti() { QytetiId = 16, ShtetiId = 1, Emri = "Lipjan" },
             new Qyteti() { QytetiId = 17, ShtetiId = 1, Emri = "Malishevë" },
             new Qyteti() { QytetiId = 18, ShtetiId = 1, Emri = "Mitrovicë" },
             new Qyteti() { QytetiId = 19, ShtetiId = 1, Emri = "Pejë" },
             new Qyteti() { QytetiId = 20, ShtetiId = 1, Emri = "Prishtinë" },
             new Qyteti() { QytetiId = 21, ShtetiId = 1, Emri = "Prizren" },
             new Qyteti() { QytetiId = 22, ShtetiId = 1, Emri = "Rahovec" },
             new Qyteti() { QytetiId = 23, ShtetiId = 1, Emri = "Skënderaj" },
             new Qyteti() { QytetiId = 24, ShtetiId = 1, Emri = "Shtërpcë" },
             new Qyteti() { QytetiId = 25, ShtetiId = 1, Emri = "Shtime" },
             new Qyteti() { QytetiId = 26, ShtetiId = 1, Emri = "Therandë" },
             new Qyteti() { QytetiId = 27, ShtetiId = 1, Emri = "Viti" },
             new Qyteti() { QytetiId = 28, ShtetiId = 1, Emri = "Vushtrri" },
             new Qyteti() { QytetiId = 29, ShtetiId = 1, Emri = "Zubin Potok" },
             new Qyteti() { QytetiId = 30, ShtetiId = 1, Emri = "Zveqan" },


             new Qyteti() { QytetiId = 31, ShtetiId = 2, Emri = "Berat" },
             new Qyteti() { QytetiId = 32, ShtetiId = 2, Emri = "Bulqizë" },
             new Qyteti() { QytetiId = 33, ShtetiId = 2, Emri = "Delvinë" },
             new Qyteti() { QytetiId = 34, ShtetiId = 2, Emri = "Devoll" },
             new Qyteti() { QytetiId = 35, ShtetiId = 2, Emri = "Dibër" },
             new Qyteti() { QytetiId = 36, ShtetiId = 2, Emri = "Durrës" },
             new Qyteti() { QytetiId = 37, ShtetiId = 2, Emri = "Elbasan" },
             new Qyteti() { QytetiId = 38, ShtetiId = 2, Emri = "Fier" },
             new Qyteti() { QytetiId = 39, ShtetiId = 2, Emri = "Gramsh" },
             new Qyteti() { QytetiId = 40, ShtetiId = 2, Emri = "Gjirokastër" },
             new Qyteti() { QytetiId = 41, ShtetiId = 2, Emri = "Has" },
             new Qyteti() { QytetiId = 42, ShtetiId = 2, Emri = "Kavajë" },
             new Qyteti() { QytetiId = 43, ShtetiId = 2, Emri = "Kolonjë" },
             new Qyteti() { QytetiId = 44, ShtetiId = 2, Emri = "Korcë" },
             new Qyteti() { QytetiId = 45, ShtetiId = 2, Emri = "Krujë" },
             new Qyteti() { QytetiId = 46, ShtetiId = 2, Emri = "Kucovë" },
             new Qyteti() { QytetiId = 47, ShtetiId = 2, Emri = "Kukës" },
             new Qyteti() { QytetiId = 48, ShtetiId = 2, Emri = "Kurbin" },
             new Qyteti() { QytetiId = 49, ShtetiId = 2, Emri = "Lezhë" },
             new Qyteti() { QytetiId = 50, ShtetiId = 2, Emri = "Librazhd" },
             new Qyteti() { QytetiId = 51, ShtetiId = 2, Emri = "Lushnjë" },
             new Qyteti() { QytetiId = 52, ShtetiId = 2, Emri = "Malësi e madhe" },
             new Qyteti() { QytetiId = 53, ShtetiId = 2, Emri = "Mallakastër" },
             new Qyteti() { QytetiId = 54, ShtetiId = 2, Emri = "Mat" },
             new Qyteti() { QytetiId = 55, ShtetiId = 2, Emri = "Mirditë" },
             new Qyteti() { QytetiId = 56, ShtetiId = 2, Emri = "Peqin" },
             new Qyteti() { QytetiId = 57, ShtetiId = 2, Emri = "Përmet" },
             new Qyteti() { QytetiId = 58, ShtetiId = 2, Emri = "Pogradec" },
             new Qyteti() { QytetiId = 59, ShtetiId = 2, Emri = "Pukë" },
             new Qyteti() { QytetiId = 60, ShtetiId = 2, Emri = "Sarandë" },
             new Qyteti() { QytetiId = 61, ShtetiId = 2, Emri = "Skrapar" },
             new Qyteti() { QytetiId = 62, ShtetiId = 2, Emri = "Shkodër" },
             new Qyteti() { QytetiId = 63, ShtetiId = 2, Emri = "Tepelenë" },
             new Qyteti() { QytetiId = 64, ShtetiId = 2, Emri = "Tiranë" },
             new Qyteti() { QytetiId = 65, ShtetiId = 2, Emri = "Tropojë" },
             new Qyteti() { QytetiId = 66, ShtetiId = 2, Emri = "Vlorë" }
        );
    }
  }
}



