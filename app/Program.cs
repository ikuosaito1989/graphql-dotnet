using System;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using GraphQL.NewtonsoftJson;

namespace App
{
    public class Droid
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Hoge
    {
        public string Fuga { get; set; }
        public string Piyo { get; set; }
    }

    public class DroidType : ObjectGraphType<Droid>
    {
        public DroidType()
        {
            Field(x => x.Id).Description("The Id of the Droid.");
            Field(x => x.Name).Description("The name of the Droid.");
        }
    }

    public class HogeType : ObjectGraphType<Hoge>
    {
        public HogeType()
        {
            Field(x => x.Fuga);
            Field(x => x.Piyo);
        }
    }

    public class StarWarsQuery : ObjectGraphType
    {
        public StarWarsQuery()
        {
            Field<DroidType>(
              "hero",
              resolve: context => new Droid { Id = "1", Name = "R2-D2" }
            );

            Field<HogeType>(
              "hoge",
              resolve: context => this.Get()
            );
        }

        public Hoge Get()
        {
            return new Hoge { Fuga = "Fuga", Piyo = "Piyo" };
        }
    }

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var schema = new Schema { Query = new StarWarsQuery() };

            var json = await schema.ExecuteAsync(_ =>
            {
                _.Query = @"{
                    hero { id name }
                    hoge { fuga piyo }
                }";
            });

            Console.WriteLine(json);
        }
    }
}