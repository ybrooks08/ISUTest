using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISUTest.Data.Models;
using ISUTest.Interfaces;
using ISUTest.WebUI.Controllers.Base;
using ISUTest.WebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISUTest.WebUI.Controllers
{
    public class ReservationController : BaseEntityController
    {
        private readonly IReservationManager _rManager;

        public ReservationController(
            IReservationManager rManager )

        {
            _rManager = rManager;
        }

        // GET: ReservationController1
        public ActionResult Index()
        {
            return View();
        }

        // GET: ReservationController1/Details/5
        public ActionResult Details( int id )
        {
            return View();
        }

        // GET: ReservationController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReservationController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( IFormCollection collection )
        {
            try
            {
                return RedirectToAction( nameof( Index ) );
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservationController1/Edit/5
        public ActionResult Edit( int id )
        {
            return View();
        }

        // POST: ReservationController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( int id, IFormCollection collection )
        {
            try
            {
                return RedirectToAction( nameof( Index ) );
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservationController1/Delete/5
        public ActionResult Delete( int id )
        {
            return View();
        }

        // POST: ReservationController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete( int id, IFormCollection collection )
        {
            try
            {
                return RedirectToAction( nameof( Index ) );
            }
            catch
            {
                return View();
            }
        }

        [HttpGet( "AllReservations" )]
        public async Task<List<ReservationViewModel>> GetReservations()
        {
            try
            {
                List<ReservationViewModel> result = _rManager.GetReservations();
                return result;
            }
            catch ( Exception ex )
            {
                throw ex;
            }

        }

        [HttpPost]
        [Route( "AddReservation" )]
        public async Task<IActionResult> AddReservation( [FromBody] ReservationViewModel model )
        {
            //Making validation in server side
            if ( ModelState.IsValid )
            {
                try
                {
                    var reservationId = await _rManager.AddReservation( model );
                    if ( reservationId > 0 )
                    {
                        return Ok( reservationId );
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch ( Exception )
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }

        [HttpPost]
        [Route( "UpdReservation" )]
        public async Task<IActionResult> UpdateReservation( [FromBody] ReservationViewModel model )
        {
            //Making validation in server side
            if ( ModelState.IsValid )
            {
                try
                {
                    var done = await _rManager.UpdateReservation( model );
                    if ( done )
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch ( Exception )
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }
    }
}
