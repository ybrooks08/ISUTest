using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISUTest.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop.Interfaces.Resources;

namespace ISUTest.WebUI.Controllers.Base
{
    public class BaseEntityController : Controller
    {
        public ResponseViewModel<T> HandleFail<T>( Exception ex )
        {
            var result = JsonConvert.SerializeObject( new
            {
                error = ex.Message
            } );
            ResponseViewModel<T> model = new ResponseViewModel<T>()
            {
                success = false,
                message = ex.Message,
                data = default( T )
            };
            return model;
        }
        
        public ResponseViewModel<T> HandleSuccess<T>()
        {
            ResponseViewModel<T> model = new ResponseViewModel<T>()
            {
                success = true,
                message = TransString.OperacionCompletada,
                data = default( T )
            };
            return model;
        }

        public ResponseViewModel<T> HandleSuccess<T>( T TEntity )
        {
            ResponseViewModel<T> model = new ResponseViewModel<T>()
            {
                success = true,
                message = TransString.OperacionCompletada,
                data = TEntity
            };
            return model;
        }

        public string GetInnerException( Exception ex )
        {
            if ( ex.InnerException != null )
            {
                return string.Format( "{0} > {1} ", ex.InnerException.Message, GetInnerException( ex.InnerException ) );
            }
            return string.Empty;
        }
    }
}
