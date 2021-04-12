using BargainNet.Core.Entities;
using BargainNet.Infra.SQL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Infra.SQL.Data
{
    public static class DataInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            if (context.Categories.Any()) return;

            context.Categories.Add(new Category { Name = "Computadores" });
            context.Categories.Add(new Category { Name = "Moveis" });
            context.Categories.Add(new Category { Name = "Veículos" });
            context.Categories.Add(new Category { Name = "Celulares" });
            context.Categories.Add(new Category { Name = "Eletrodomésticos" });
            context.Categories.Add(new Category { Name = "Modas" });
            context.Categories.Add(new Category { Name = "Saúde" });
            context.Categories.Add(new Category { Name = "Construções" });
            context.Categories.Add(new Category { Name = "Ferramentas" });

            context.SaveChanges();

        }
    }
}
