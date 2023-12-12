using Front.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace DesafioFront.Controllers
{
    public class ClienteController : Controller
    {
        readonly Uri baseAddress = new Uri("http://localhost:5002/api/Cliente/");
        private readonly HttpClient _cliente;

        public ClienteController()
        {
            _cliente = new HttpClient();
            _cliente.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<ClienteViewModel> clienteList = new List<ClienteViewModel>();
            HttpResponseMessage response =  _cliente.GetAsync(baseAddress + "getclientelist").Result;

            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                clienteList = JsonConvert.DeserializeObject<List<ClienteViewModel>>(data);
            }

            return View(clienteList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ClienteViewModel clienteViewModel)
        {
            try { 
            string data = JsonConvert.SerializeObject(clienteViewModel);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _cliente.PostAsync(baseAddress + "addcliente", content).Result;
            
            if (response.IsSuccessStatusCode)
            {
                    TempData["successMessage"] = "Cliente cadastrado";
                return RedirectToAction("Index");
            }
                }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                ClienteViewModel cliente = new ClienteViewModel();
                HttpResponseMessage response = _cliente.GetAsync(_cliente.BaseAddress + "updatecliente" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    cliente = JsonConvert.DeserializeObject<ClienteViewModel>(data);
                }
                return View(cliente);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View();


        }
        [HttpPost]
        public IActionResult Edit(ClienteViewModel model)
        {
            try { 
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _cliente.PutAsync(_cliente.BaseAddress + "updatecliente", content).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["sucessMessage"] = "Dados do cliente atualizado";
                return RedirectToAction("Index");
            }

            return View(data);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View();
        }
    }
}
