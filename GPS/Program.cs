using System;
using System.Net;
using System.IO;
using System.Text.Json;

namespace GPS
{
    class Program
    {
        public class WeatherForecast{
            public string userId { get; set; }
            public int id { get; set; }
            public string title { get; set; }
            public string body { get; set; }
        }

        static void Main(string[] args)
        {
            Recebe();
        }

        private static async void Envia(){
            //caso eu precise enviar alguma informação
        }

        private static async void Recebe(){ //metodos assincronos. Incluir o await quando eu tiver as outras funções que serão necessarias rodar
            while(true){
                try{
                    var requisicaoWeb = WebRequest.CreateHttp("http://localhost:5000/rastreio/1");         
                    requisicaoWeb.Method = "GET";

                    using (var resposta = requisicaoWeb.GetResponse())
                    {
                        var streamDados = resposta.GetResponseStream();
                        StreamReader reader = new StreamReader(streamDados);
                        object objResponse = reader.ReadToEnd();

                        WeatherForecast? weatherForecast = 
                        JsonSerializer.Deserialize<WeatherForecast>(objResponse.ToString());

                        Console.WriteLine($"Rua: {weatherForecast?.body}"); //criar um log e armazenar o caminho que a pessoa esta tomando
                        Console.ReadLine();
                        streamDados.Close();
                        resposta.Close();
                    }
                } catch (Exception){
                    continue;
                }
            }
        }
    }
}
