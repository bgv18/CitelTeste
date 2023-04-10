using CitelTeste.WebAppCitelTeste.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace WebAppCitelTeste.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly string baseUrl = "https://localhost:44371/api/categoria";
        public async Task<IActionResult> Index()
        {
            List<Categoria> lstCategorias = new List<Categoria>();
            using (var httpContext = new HttpClient())
            {
                using (var response = await httpContext.GetAsync(baseUrl))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    lstCategorias = JsonConvert.DeserializeObject<List<Categoria>>(apiResponse);
                }
            }
            return View(lstCategorias);
        }

        [HttpGet]
        public async Task<IActionResult> ConsultarCategorias(int id)
        {
            Categoria categoria = new Categoria();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(baseUrl + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    categoria = JsonConvert.DeserializeObject<Categoria>(apiResponse);
                }
            }
            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarCategoria(Categoria categoria)
        {
            Categoria categoriaRecebida = new Categoria();

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(categoria),
                                                  Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(baseUrl, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    categoriaRecebida = JsonConvert.DeserializeObject<Categoria>(apiResponse);
                }
            }
            return View(categoriaRecebida);
        }

        [HttpGet]
        public async Task<IActionResult> EditarCategoria(int id)
        {
            Categoria categoria = new Categoria();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(baseUrl + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    categoria = JsonConvert.DeserializeObject<Categoria>(apiResponse);
                }
            }
            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> EditarCategoria(Categoria categoria)
        {
            Categoria categoriaRecebida = new Categoria();

            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(categoria.Id.ToString()), "Id");
                content.Add(new StringContent(categoria.DataCadastro.ToString()), "DataCadastro");
                content.Add(new StringContent(categoria.Descricao), "Descricao");

                using (var response = await httpClient.PutAsync(baseUrl + "/" + categoria.Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Sucesso";
                    categoriaRecebida = JsonConvert.DeserializeObject<Categoria>(apiResponse);
                }
            }
            return View(categoriaRecebida);
        }

        public async Task<IActionResult> ExcluirCategoria(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(baseUrl + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            ViewBag.DeleteCategoria = $"Categoria {id} deletada.";

            return View();
        }
    }
}
