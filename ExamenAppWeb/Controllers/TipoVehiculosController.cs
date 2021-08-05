using Datos.Data;
using Datos.Model;
using Datos.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenAppWeb.Controllers
{
    [Authorize]
    public class TipoVehiculosController : Controller
    {
        private readonly EjercicioEvaluacionContext _context;
        public TipoVehiculosController(EjercicioEvaluacionContext context)
        {
            _context = context;
        }
        public void Combox()
        {
             ViewData["CodigoVehiculo"] = new SelectList(_context.Vehiculos.Select(y => new ViewModelVehiculoTipo
             {
              Codigo = y.Codigo,
                 Nombre = $"{y.Nombre}  ",
                 Estado = y.Estado


             }).Where(y => y.Estado == 1).ToList(), "Codigo", "Nombre");
        }
        [Authorize(Roles = "Jefe,Empleado")]
        public ActionResult Index()
        {

            //  List<TipoVehiculo> ltstipoVehiculo = _context.TipoVehiculos.ToList();
            List<ViewModelVehiculoTipo> ltstipoVehiculo = _context.TipoVehiculos.Select(y => new ViewModelVehiculoTipo
            {
                Codigo = y.Codigo,
                Nombre = $"{y.CodigoVehiculoNavigation.Nombre}",
                Estado = y.Estado


            }).ToList();

            return View(ltstipoVehiculo);
        }

        [Authorize(Roles = "Jefe,Empleado")]
        public ActionResult Details(int id)
        {
           TipoVehiculo tipoVehiculo = _context.TipoVehiculos.Where(x => x.CodigoVehiculo == id).FirstOrDefault();
            return View(tipoVehiculo);
        }

        [Authorize(Roles = "Jefe")]
        public ActionResult Create()
        {
            Combox();
            return View();
        }
        [Authorize(Roles = "Jefe")]

        [HttpPost]
        public ActionResult Create(TipoVehiculo tipoVehiculo )
        {
            try
            {
                tipoVehiculo.Estado = 1;
                _context.Add(tipoVehiculo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(tipoVehiculo);
            }
        }

        [Authorize(Roles = "Jefe")]
        public ActionResult Edit(int id)
        {
            Combox();
            TipoVehiculo tipoVehiculo = _context.TipoVehiculos.Where(x => x.CodigoVehiculo == id).FirstOrDefault();
            return View(tipoVehiculo);
        }

        [Authorize(Roles = "Jefe")]
        [HttpPost]
        public ActionResult Edit(int id, TipoVehiculo tipoVehiculo)
        {
            if (id != tipoVehiculo.CodigoVehiculo)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _context.Update(tipoVehiculo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                Combox();
                return View(tipoVehiculo);
            }
        }

        [Authorize(Roles = "Jefe")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        [Authorize(Roles = "Jefe")]
     
     
        public ActionResult Activar(int id)
        {
            TipoVehiculo tipoVehiculo = _context.TipoVehiculos.Where(x => x.CodigoVehiculo == id).FirstOrDefault();
            tipoVehiculo.Estado = 1;
            _context.Update(tipoVehiculo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Jefe")]
        public ActionResult Desactivar(int id)
        {
            TipoVehiculo tipoVehiculo = _context.TipoVehiculos.Where(x => x.CodigoVehiculo == id).FirstOrDefault();
            tipoVehiculo.Estado = 0;
            _context.Update(tipoVehiculo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}