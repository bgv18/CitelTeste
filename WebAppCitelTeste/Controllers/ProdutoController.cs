using CitelTeste.WebAppCitelTeste.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace WebAppCitelTeste.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly string baseUrlProduto = "https://localhost:44371/api/produto";
        private readonly string baseUrlCategoria = "https://localhost:44371/api/categoria";

        public async Task<IActionResult> Index()
        {
            List<Produto> lstProdutos = new List<Produto>();
            List<Categoria> lstCategorias = new List<Categoria>();

            using (var httpContext = new HttpClient())
            {
                using (var response = await httpContext.GetAsync(baseUrlProduto))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    lstProdutos = JsonConvert.DeserializeObject<List<Produto>>(apiResponse);
                }
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(baseUrlCategoria))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    lstCategorias = JsonConvert.DeserializeObject<List<Categoria>>(apiResponse);
                }
            }

            ViewData["CategoriaId"] = new SelectList(lstCategorias, "Id", "Nome");

            return View(lstProdutos);
        }

        public ViewResult ConsultarProdutos() => View();

        [HttpGet]
        public async Task<IActionResult> ConsultarProdutos(int id)
        {
            Produto produto = new Produto();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(baseUrlProduto + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    produto = JsonConvert.DeserializeObject<Produto>(apiResponse);
                }
            }
            return View(produto);
        }

        public async Task<IActionResult> AdicionarProduto()
        {
            List<Categoria> listaCategorias = new List<Categoria>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(baseUrlCategoria))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    listaCategorias = JsonConvert.DeserializeObject<List<Categoria>>(apiResponse);
                }
            }

            ViewData["CategoriaId"] = new SelectList(listaCategorias, "Id", "Nome");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarProduto(Produto produto)
        {
            Produto produtoRecebido = new Produto();

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(produto),
                                                  Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(baseUrlProduto, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    produtoRecebido = JsonConvert.DeserializeObject<Produto>(apiResponse);
                }
            }
            TempData["Mensagem"] = "Produto adicionado com sucesso.";
            return View(produtoRecebido);
        }

        [HttpGet]
        public async Task<IActionResult> EditarProduto(int id)
        {
            Produto produto = new Produto();
            List<Categoria> categorias = new List<Categoria>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(baseUrlProduto + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    produto = JsonConvert.DeserializeObject<Produto>(apiResponse);
                }
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(baseUrlCategoria))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    categorias = JsonConvert.DeserializeObject<List<Categoria>>(apiResponse);
                }
            }

            ViewData["CategoriaId"] = new SelectList(categorias, "Id", "Nome", produto.CategoriaId);
            return View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> EditarProduto(Produto produto)
        {
            Produto produtoRecebido = new Produto();

            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(produto.Id.ToString()), "Id");
                content.Add(new StringContent(produto.Descricao), "Descricao");
                content.Add(new StringContent(produto.CategoriaId.ToString()), "CategoriaId");
                content.Add(new StringContent(produto.Saldo.ToString()), "Saldo");

                using (var response = await httpClient.PutAsync(baseUrlProduto + "/" + produto.Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Sucesso";
                    produtoRecebido = JsonConvert.DeserializeObject<Produto>(apiResponse);
                }
            }
            TempData["Mensagem"] = "Produto alterado com sucesso.";
            return View(produtoRecebido);
        }

        public async Task<IActionResult> ExcluirProduto(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(baseUrlProduto + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            ViewBag.DeleteProduto = "Produto ID " + id + " deletado com sucesso";

            return View();
        }
    }
}
