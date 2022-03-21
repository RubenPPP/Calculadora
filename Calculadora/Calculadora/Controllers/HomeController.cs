using Calculadora.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Calculadora.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet] // Por predefinição os pedidos http são get
        public IActionResult Index()
        {
            // Inicializar os dados para a calculadora
            ViewBag.Visor = "0";
            return View();
        }

        [HttpPost]
        public IActionResult Index(string botao, string visor)
        {
            // Vamos decidir a ação a realizar com o valor do botão
            switch (botao)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":
                    // Pressionou um algarismo
                    if (visor == "0")
                        visor = botao;
                    else
                        visor += botao;
                    break;
                case ",":
                    // Pressionou a vírgula
                    if (!visor.Contains(','))
                        visor += botao;
                    break;
                case "+/-":
                    // Pressinou +/-
                    if (visor.StartsWith('-'))
                        visor = visor[1..];
                    else
                        visor = '-' + visor;
                    break;
            }
            // Preparar os dados a serem enviados para a View
            ViewBag.Visor = visor;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}