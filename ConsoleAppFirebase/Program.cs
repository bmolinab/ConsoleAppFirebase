using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppFirebase
{/// <summary>
/// Aplicación ejemplo de como usar firebase con c#
/// 
/// </summary>
    class Program
    {
        public const string USER_AGENT = "firebase-net/1.0";

        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese el id de la bodega");
            int Numero = int.Parse(Console.ReadLine());
            FirebaseInsert(Numero);
            Console.WriteLine("enviada correctamente, pulse enter para continuar");
            Console.ReadLine();

        }
        /// <summary>
        /// Método para crear una nueva bodega en Firebase, mediane el id.
        /// </summary>
        /// <param name="idBodega"></param>
        /// <returns></returns>
        static async Task FirebaseInsert(int idBodega)
        {
            try
            {
                string FirebaseURI = GetUrlFirebase(idBodega);
                var rutajson = new DistribuidorFirebase { idDistribuidor = 0, Lat = 0, Lon = 0, Estado = 1 };
                var json = JsonConvert.SerializeObject(rutajson);
                var client = new HttpClient();
                var msg = new HttpRequestMessage(new HttpMethod("Post"), FirebaseURI);
                msg.Headers.Add("user-agent", USER_AGENT);
                if (json != null)
                {
                    msg.Content = new StringContent(
                        json,
                        UnicodeEncoding.UTF8,
                        "application/json");
                }
                var respuesta = await client.SendAsync(msg);
                var result = await respuesta.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Método para crear un nuevo distribuidor segun el id de la bodega.
        /// </summary>
        /// <param name="idBodega"></param>
        /// <param name="distribuidorFirebase"></param>
        /// <returns></returns>
        static async Task FirebaseInsert(int idBodega, DistribuidorFirebase distribuidorFirebase)
        {
            try
            {
                string FirebaseURI = GetUrlFirebase(idBodega);
                var rutajson = distribuidorFirebase;
                var json = JsonConvert.SerializeObject(rutajson);
                var client = new HttpClient();
                var msg = new HttpRequestMessage(new HttpMethod("Post"), FirebaseURI);
                msg.Headers.Add("user-agent", USER_AGENT);
                if (json != null)
                {
                    msg.Content = new StringContent(
                        json,
                        UnicodeEncoding.UTF8,
                        "application/json");
                }
                var respuesta = await client.SendAsync(msg);
                var result = await respuesta.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Devuelve el url de firebase segun el id de la bodega.
        /// </summary>
        /// <param name="idBodega"></param>
        /// <returns></returns>
        static string GetUrlFirebase(int idBodega)
        {
            return string.Format("https://elgasec.firebaseio.com/Bodega/{0}/Distribuidor.json", idBodega);
        }      
      
        /// <summary>
        /// Objeto Distribuidor
        /// </summary>
        public class DistribuidorFirebase
        {
            public double Lat { get; set; }
            public double Lon { get; set; }
            public int idDistribuidor { get; set; }
            public int Estado{ get; set; }

        }
    
    }
}
