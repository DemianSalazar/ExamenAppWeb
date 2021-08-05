using Datos.Data;
using Datos.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenAppWeb.Controllers
{
    [Authorize]
    public class VehiculosController : Controller
    {
        private readonly EjercicioEvaluacionContext _context;
        public VehiculosController(EjercicioEvaluacionContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Jefe,Empleado")]
        public ActionResult Index()
        {
            List<Vehiculo> ltsVehiculo = _context.Vehiculos.ToList();
            return View(ltsVehiculo);
        }

        [Authorize(Roles = "Jefe,Empleado")]
        public ActionResult Details(int id)
        {

            Vehiculo vehiculo = _context.Vehiculos.Where(x => x.Codigo == id).FirstOrDefault();
            return View(vehiculo);
        }
        [Authorize(Roles = "Jefe")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Jefe")]
        [HttpPost]
        
        public ActionResult Create(Vehiculo vehiculo )
        {
            try
            {
                vehiculo.Estado = 1;
                _context.Add(vehiculo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(vehiculo);
            }
        }

        [Authorize(Roles = "Jefe")]
        public ActionResult Edit(int id)
        {
            Vehiculo vehiculo = _context.Vehiculos.Where(x => x.Codigo == id).FirstOrDefault();
            return View(vehiculo);
        }

        [Authorize(Roles = "Jefe")]
        [HttpPost]
   
        public ActionResult Edit(int id, Vehiculo vehiculo)
        {
            if (id != vehiculo.Codigo)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _context.Update(vehiculo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(vehiculo);
            }
        }

        [Authorize(Roles = "Jefe")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        [Authorize(Roles = "Jefe")]
        [HttpPost]
   
        public ActionResult Activar(int id)
        {
            Vehiculo vehiculo = _context.Vehiculos.Where(x => x.Codigo == id).FirstOrDefault();
            vehiculo.Estado = 1;
            _context.Update(vehiculo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Jefe")]
        public ActionResult Desactivar(int id)
        {
            Vehiculo vehiculo = _context.Vehiculos.Where(x => x.Codigo == id).FirstOrDefault();
            vehiculo.Estado = 0;
            _context.Update(vehiculo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
