using DesignPatterns.Factories;
using DesignPatterns.ModelBuilder;
using DesignPatterns.Models;
using DesignPatterns.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DesignPatterns.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleRepository _vehicleRepository;

        // Constructor con inyección de dependencias
        public HomeController(IVehicleRepository vehicleRepository, ILogger<HomeController> logger)
        {
            _vehicleRepository = vehicleRepository;
            _logger = logger;
        }

        // Método para seleccionar la fábrica de autos según el modelo
        // Implementa el patrón Factory Method
        private CarFactory chooseFactory(string vehicle)
        {
            switch (vehicle)
            {
                case "Mustang":
                    return new FordMustangFactory();
                case "Explorer":
                    return new FordExplorerFactory();
                case "Escape":
                    return new FordEscapeFactory();
                default:
                    throw new NotImplementedException();
            }
        }

        // Vista principal que muestra la lista de vehículos
        public IActionResult Index()
        {
            var model = new HomeViewModel();
            model.Vehicles = _vehicleRepository.GetVehicles();

            string error = Request.Query.ContainsKey("error") ? Request.Query["error"].ToString() : null;
            ViewBag.ErrorMessage = error;

            return View(model);
        }

        // Agrega un vehículo modelo Mustang usando la fábrica correspondiente
        [HttpGet]
        public IActionResult AddMustang()
        {
            var carFactory = chooseFactory("Mustang");
            _vehicleRepository.AddVehicle(carFactory.Create());
            return Redirect("/");
        }

        // Agrega un vehículo modelo Explorer usando la fábrica correspondiente
        [HttpGet]
        public IActionResult AddExplorer()
        {
            var carFactory = chooseFactory("Explorer");
            _vehicleRepository.AddVehicle(carFactory.Create());
            return Redirect("/");
        }

        // Agrega un vehículo modelo Escape usando la fábrica correspondiente
        [HttpGet]
        public IActionResult AddEscape()
        {
            var carFactory = chooseFactory("Escape");
            _vehicleRepository.AddVehicle(carFactory.Create());
            return Redirect("/");
        }

        // Inicia el motor del vehículo con el ID especificado
        [HttpGet]
        public IActionResult StartEngine(string id)
        {
            try
            {
                var vehicle = _vehicleRepository.Find(id);
                vehicle.StartEngine();
                return Redirect("/");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return Redirect($"/?error={ex.Message}");
            }
        }

        // Agrega gasolina al vehículo con el ID especificado
        [HttpGet]
        public IActionResult AddGas(string id)
        {
            try
            {
                var vehicle = _vehicleRepository.Find(id);
                vehicle.AddGas();
                return Redirect("/");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return Redirect($"/?error={ex.Message}");
            }
        }

        // Detiene el motor del vehículo con el ID especificado
        [HttpGet]
        public IActionResult StopEngine(string id)
        {
            try
            {
                var vehicle = _vehicleRepository.Find(id);
                vehicle.StopEngine();
                return Redirect("/");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return Redirect($"/?error={ex.Message}");
            }
        }

        // Vista de privacidad (opcional)
        public IActionResult Privacy()
        {
            return View();
        }

        // Manejo de errores
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
