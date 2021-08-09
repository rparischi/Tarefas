using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Tarefas.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Tarefas.Controllers
{
    public class TarefasController : Controller
    {
        private readonly string apiUrl = "http://localhost:25030/api/tarefa";
        public async Task<IActionResult> Index()
        {
            List<Tarefa> listaTarefas = new List<Tarefa>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiUrl))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    listaTarefas = JsonConvert.DeserializeObject<List<Tarefa>>(apiResponse);
                }
            }
            return View(listaTarefas);
        }

        public ViewResult GetTarefa() => View();
        [HttpPost]
        public async Task<IActionResult> GetTarefa(int id)
        {
            Tarefa tarefa = new Tarefa();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiUrl + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    tarefa = JsonConvert.DeserializeObject<Tarefa>(apiResponse);
                }
            }
            return View(tarefa);
        }

        public ViewResult AddTarefa() => View();
        [HttpPost]
        public async Task<IActionResult> AddTarefa(Tarefa tarefa)
        {
            Tarefa tarefaRecebida = new Tarefa();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(tarefa),
                                                  Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(apiUrl, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    tarefaRecebida = JsonConvert.DeserializeObject<Tarefa>(apiResponse);
                }
            }
            return View(tarefaRecebida);
        }
    }
}
