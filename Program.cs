using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsumoApi
{
    class Program
    {
        static async void Main(string[] args)
        {

                Console.WriteLine("Escolha uma categoria de piada");
                Console.WriteLine("Digite [1] para receber uma piada sobre: Programação");
                Console.WriteLine("Digite [2] para receber uma piada sobre: De caracter duvidoso");
                Console.WriteLine("Digite [3] para receber uma piada sobre: Trocadilhos");
                Console.WriteLine("Digite [4] para receber uma piada aleatoria");

                string categoria = Console.ReadLine();

                string url = getUrl(categoria);

                if(url.Equals("erro"))
                {
                    Console.WriteLine("Escolha uma opção valida");
                }
                else
                {
                   await getPiadas(url);
                }

        }

        private static string getUrl(string categoria)
        {
            return categoria switch
            {
                "1" => $"https://sv443.net/jokeapi/v2/joke/Programming?type=single",
                "2" => $"https://sv443.net/jokeapi/v2/joke/Miscellaneous?type=single",
                "3" => $"https://sv443.net/jokeapi/v2/joke/Dark?type=single",
                "4" => $"https://sv443.net/jokeapi/v2/joke/Any?type=single",
                _ => "erro",

            };
        }

        private static async Task getPiadas(string url)
        {
            HttpClient httpClient = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

            HttpResponseMessage response = await httpClient.SendAsync(request);

            string piada = await response.Content.ReadAsStringAsync();

            Piadas _piadas = JsonConvert.DeserializeObject<Piadas>(piada);

            PrintPiada(_piadas);

        }

        private static void PrintPiada(Piadas piadas)
        {
            Console.WriteLine("\n\n");
            Console.WriteLine($"Categoria: {piadas.category}");
            Console.WriteLine($"Piada: {piadas.joke}");
            Console.WriteLine("\n\n");
        }
    }
}
