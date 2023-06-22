using System;
using ExemplosLinq.Entidades;

namespace ExemplosLinq
{
    class Program
    {

        static void Print<T>(string mensagem, IEnumerable<T>collection)
        {
            Console.WriteLine(mensagem);
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            Categoria c1 = new Categoria() { Id = 1, Nome = "Ferramentas", Tier = 2 };
            Categoria c2 = new Categoria() { Id = 2, Nome = "Computadores", Tier = 1 };
            Categoria c3 = new Categoria() { Id = 3, Nome = "Eletronicos", Tier = 1 };

            List <Produto> produtos = new List<Produto>() { 
                new Produto() { Id = 1, Nome = "Computador", Preco = 1200.0, Categoria = c2 },
                new Produto() { Id = 2, Nome = "Martelo", Preco = 90.0, Categoria = c1 },
                new Produto() { Id = 3, Nome = "TV", Preco = 1700.0, Categoria = c3 },
                new Produto() { Id = 4, Nome = "Notebook", Preco = 1300.0, Categoria = c2 },
                new Produto() { Id = 5, Nome = "Serra", Preco = 80.0, Categoria = c1 },
                new Produto() { Id = 6, Nome = "Tablet", Preco = 700.0, Categoria = c3 },
                new Produto() { Id = 7, Nome = "Camera", Preco = 700.0, Categoria = c3 },
                new Produto() { Id = 8, Nome = "Impressora", Preco = 350.0, Categoria = c3 },
                new Produto() { Id = 9, Nome = "Macbook", Preco = 1800.0, Categoria = c2 },
                new Produto() { Id = 10, Nome = "Som", Preco = 700.0, Categoria = c3 },
                new Produto() { Id = 11, Nome = "Level", Preco = 70.0, Categoria = c1 },
            };

            //-----Utilizando Linq-----
            //var itens = produtos.Where(x => x.Categoria.Tier == 1 && x.Preco < 900);
            //--- Utilizado algebra relacional ---
            var r1 = 
                from p in produtos where p.Categoria.Tier == 1 && p.Preco < 900.0 select p;
            Print("Tier 1 e Preço < 900: ", r1);

            //var r2 = produtos.Where(x => x.Categoria.Nome == "Ferramentas").Select(x => x.Nome) ;
            var r2 =
                from p in produtos where p.Categoria.Nome == "Ferramentas" select p;
            Print("Nomes dos produtos de ferramentas", r2);

            //objeto anonimo
            //var r3 = produtos
            //    .Where(p => p.Nome[0] == 'C')
            //    .Select(p => new { p.Nome, p.Preco, NomeCategoria = p.Categoria.Nome }) ;
            var r3 = 
                from p in produtos 
                where p.Nome[0] == 'C' 
                select new {
                    p.Nome, 
                    p.Preco, 
                    NomeCategoria = p.Categoria.Nome 
                };
            Print("Produtos começados por C e objeto anonimo", r3);

            var r40 =
                from p in produtos where p.Categoria.Nome == "Ferramentas" select (p.Nome);
            Print("Apenas o nome das ferramentas", r40);

            //var r4 = produtos.Where(p => p.Categoria.Tier == 1).OrderBy(p => p.Preco).ThenBy(p => p.Nome);
            var r4 = 
                from p in produtos 
                where p.Categoria.Tier == 1 
                orderby p.Nome
                orderby p.Preco
                select p;
            Print("Produtos de tier 1 e ordenados por preço e nome  : ", r4);

            //var r5 = r4.Skip(2).Take(4);
            var r5 = (from p in r4
                      select p).Skip(2).Take(4);
            Print("Tier 1 order by preço Then by Name skip 2 Take 4", r5);



            //Tem uma chave e uma coleção
            //var r16 = produtos.GroupBy(p => p.Categoria);
            var r16 =
                from p in produtos group p by p.Categoria;

            //Categoria = Chave, Produto = coleção
            foreach(IGrouping<Categoria, Produto> group in r16)
            {
                Console.WriteLine("Categoria: " + group.Key.Nome);
                foreach(Produto p in group)
                {
                    Console.WriteLine(p);
                }
                Console.WriteLine();
            }
        }
    }
}