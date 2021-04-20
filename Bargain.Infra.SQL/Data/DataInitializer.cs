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

            context.Categories.Add(new Category { Name = "Acessórios para Veículos" });
            context.Categories.Add(new Category { Name = "Agro" });
            context.Categories.Add(new Category { Name = "Alimentos e Bebidas" });
            context.Categories.Add(new Category { Name = "Animais" });
            context.Categories.Add(new Category { Name = "Antiguidades e Coleções" });
            context.Categories.Add(new Category { Name = "Arte, Papelaria e Armarinho" });
            context.Categories.Add(new Category { Name = "Bebês" });
            context.Categories.Add(new Category { Name = "Beleza e Cuidado Pessoal" });
            context.Categories.Add(new Category { Name = "Brinquedos e Hobbies" });
            context.Categories.Add(new Category { Name = "Calçados, Roupas e Bolsas" });
            context.Categories.Add(new Category { Name = "Câmeras e Acessórios" });
            context.Categories.Add(new Category { Name = "Carros, Motos e Outros" });
            context.Categories.Add(new Category { Name = "Casa, Móveis e Decoração" });
            context.Categories.Add(new Category { Name = "Celulares e Telefones" });
            context.Categories.Add(new Category { Name = "Construção" });
            context.Categories.Add(new Category { Name = "Eletrodomésticos" }); 
            context.Categories.Add(new Category { Name = "Eletrônicos, Áudio e Vídeo" });
            context.Categories.Add(new Category { Name = "Esportes e Fitness" });
            context.Categories.Add(new Category { Name = "Ferramentas" });
            context.Categories.Add(new Category { Name = "Festas e Lembrancinhas" });
            context.Categories.Add(new Category { Name = "Games" });
            context.Categories.Add(new Category { Name = "Imóveis" });
            context.Categories.Add(new Category { Name = "Indústria e Comércio" });
            context.Categories.Add(new Category { Name = "Informática" });
            context.Categories.Add(new Category { Name = "Ingressos" });
            context.Categories.Add(new Category { Name = "Instrumentos Musicais" });
            context.Categories.Add(new Category { Name = "Joias e Relógios" });
            context.Categories.Add(new Category { Name = "Livros, Revistas e Comics" });
            context.Categories.Add(new Category { Name = "Música, Filmes e Seriados" });
            context.Categories.Add(new Category { Name = "Saúde" });
            context.Categories.Add(new Category { Name = "Serviços" });
            context.Categories.Add(new Category { Name = "Outros" });          


            context.SaveChanges();

        }
    }
}
