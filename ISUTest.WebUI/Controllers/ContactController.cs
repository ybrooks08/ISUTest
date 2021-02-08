using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISUTest.Data.Models;
using ISUTest.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ISUTest.WebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactManager _cManager;

        public ContactController(
            IContactManager cManager )

        {
            _cManager = cManager;
        }

        [HttpGet( "ContactByName" )]
        public async Task<ContactViewModel> GetContactByName( string contactName )
        {
            try
            {
                ContactViewModel result = _cManager.GetContactByName( contactName );
                return result;
            }
            catch ( Exception ex )
            {
                throw ex;
            }

        }

        [HttpGet( "AllContacts" )]
        public List<ContactViewModel> GetContacts()
        {
            try
            {
                List<ContactViewModel> result = _cManager.GetContacts();
                return result;
            }
            catch ( Exception ex )
            {
                throw ex;
            }

        }

        [HttpPost]
        [Route( "DeleteCont" )]
        public async Task<IActionResult> DeleteContact( int? contactId )
        {
            int result = 0;
            //Validation on server
            if ( contactId == null )
            {
                return BadRequest();
            }

            try
            {
                result = await _cManager.DeleteContact( contactId );
                if ( result == 0 )
                {
                    return NotFound();
                }
                return Ok();
            }
            catch ( Exception )
            {

                return BadRequest();
            }
        }

        [HttpPost]
        [Route( "UpdateCont" )]
        public async Task<IActionResult> UpdateContact( [FromBody] ContactViewModel model )
        {
            if ( ModelState.IsValid )
            {
                try
                {
                    await _cManager.UpdateContact( model );

                    return Ok();
                }
                catch ( Exception ex )
                {
                    if ( ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException" )
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }
    }
}
