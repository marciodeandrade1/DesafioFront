using Front.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace DesafioFront.Controllers
{
    public class LogradouroController : Controller
    {
        readonly Uri baseAddress = new Uri("http://localhost:5002/api/Logradouro/");
        private readonly HttpClient _logradouro;

        public LogradouroController()
        {
            _logradouro = new HttpClient();
            _logradouro.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<LogradouroViewModel> logradouroList = new List<LogradouroViewModel>();
            HttpResponseMessage response = _logradouro.GetAsync(baseAddress + "getlogradourolist").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                logradouroList = JsonConvert.DeserializeObject<List<LogradouroViewModel>>(data);
            }

            return View(logradouroList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(LogradouroViewModel logradouroViewModel)
        {
            try
            {
                string data = JsonConvert.SerializeObject(logradouroViewModel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _logradouro.PostAsync(baseAddress + "addlogradouro", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["errorMessage"] = "Logradouro cadastrado";
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
                LogradouroViewModel logradouro = new LogradouroViewModel();
                HttpResponseMessage response = _logradouro.GetAsync(_logradouro.BaseAddress + "updatelogradouro" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    logradouro = JsonConvert.DeserializeObject<LogradouroViewModel>(data);
                }
                return View(logradouro);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View();


        }
        [HttpPost]
        public IActionResult Edit(LogradouroViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _logradouro.PutAsync(_logradouro.BaseAddress + "updatelogradouro", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["sucessMessage"] = "Dados do logradouro atualizado";
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
