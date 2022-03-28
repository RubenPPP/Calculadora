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

        /// <summary>
        /// Preparar a view com a calculadora, durante a primeira iteração
        /// </summary>
        /// <returns></returns>
        [HttpGet] // Por predefinição os pedidos http são get
        public IActionResult Index()
        {
            // Inicializar os dados para a calculadora
            ViewBag.Visor = "0";
            ViewBag.LimpaEcra = "False";
            return View();
        }

        /// <summary>
        /// Processa a interação com a calculadora
        /// </summary>
        /// <param name="botao">Valor do botão selecionado pelo cliente</param>
        /// <param name="visor">Valor existente no Visor da calculadora</param>
        /// <param name="primeiroOperando">Valor guardado do operando posterior</param>
        /// <param name="operador">Operador guardado do perador posterior</param>
        /// <param name="limpaEcra">Valor flag que indica quando o visor é limpo</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(string botao, string visor, string primeiroOperando, string operador, string limpaEcra)
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
                    if (visor == "0" || limpaEcra == "True")
                    {
                        visor = botao;
                        limpaEcra = "False";
                    }
                    else
                        visor += botao;
                    break;
                case ",":
                    // Pressionou a vírgula
                    if (!visor.Contains(','))
                        visor += botao;
                    break;
                case "+/-":
                    // Pressionou +/-
                    if (visor.StartsWith('-'))
                        visor = visor[1..];
                    else
                        visor = '-' + visor;
                    break;
                case "+":
                case "-":
                case "x":
                case ":":
                case "=":
                    // Pressionou um operador
                    if (!string.IsNullOrEmpty(operador))
                    {
                        double operadorUm = Convert.ToDouble(primeiroOperando);
                        double operadorDois = Convert.ToDouble(visor.Replace(',', '.'));
                        switch (operador)
                        {
                            case "+": visor = operadorUm + operadorDois + ""; break;
                            case "-": visor = operadorUm - operadorDois + ""; break;
                            case "x": visor = operadorUm * operadorDois + ""; break;
                            case ":": visor = operadorUm / operadorDois + ""; break;
                        }
                    }
                    primeiroOperando = visor;
                    if (operador != "=")
                        operador = botao;
                    else
                        operador = "";
                    limpaEcra = "True";
                    break;
                case "C":
                    limpaEcra = "False";
                    visor = "";
                    operador = "";
                    primeiroOperando = "";
                    break;
            }
            // Preparar os dados a serem enviados para a View
            ViewBag.Visor = visor;
            ViewBag.PrimeiroOperando = primeiroOperando;
            ViewBag.Operador = operador;
            ViewBag.LimpaEcra = limpaEcra;
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