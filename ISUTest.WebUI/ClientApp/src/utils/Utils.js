//import React from 'react';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

//HTTP METHODS
export const POST = "POST";
export const PUT = "PUT";
export const GET = "GET";
export const DELETE = "DELETE";

//END POINTS
export const CREATE_RESERVATION_URL = "AddReservation";
export const UPDATE_RESERVATION_URL = "UpdReservation";

//HTTP CODE STATUS
export const OK_CODE = 200;

//AUX VARS
//var loadingMsg = null;

//AUX CONSTANTS
/*xport function showLoading (loadingMessage) {  
    loadingMsg = message.loading(loadingMessage, 0);
  }*/
  
/*export function hideLoading () {
try {
    if(loadingMsg)
    setTimeout(loadingMsg, 0);
}
catch { }
}*/

export async function request(method, url, body, authorization, onErrorEvent=(code, body) => {}, onOkEvent=(body) => {}, okCode=OK_CODE) {
    const requestData = 
      method === GET || !body
          ? 
          {
            method: method,
            headers: {
              Accept: "application/json",
              "Content-Type": "application/json",
              "Authorization": authorization
            }
          }
          :
          {
            method: method,
            headers: {
              Accept: "application/json",
              "Content-Type": "application/json",
              "Authorization": authorization
            },
            body: body
          };
    return fetch(url, requestData).then( response => {
          const code = response.status;
          const body = code === 401 || code === 403
           ? { success: false, message: "Contenido no autorizado" }
           : response.json();
          return Promise.all([code, body]);
      }).then( response => {
          const code = response[0];
          const body = response[1];
          if(code === okCode)
              return Promise.all([onOkEvent(body)]);
          else {
              if(code === 401)
                //logout();
              return Promise.all([onErrorEvent(code, body)]);
          }
      }).catch( error => {
        toast.error(error.message, {
            position: "top-center",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            });
        //onErrorEvent(null, null);
      });
  }

export function launchToast (type, message) {
    switch(type) {
        case "error": {
            toast.error(message, {
            position: "top-center",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            });
           break;
        }
        case "success": {
            toast.success(message, {
            position: "top-center",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            });
           break;
        }
        case "warning": {
            toast.warn(message, {
            position: "top-center",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            });
           break;
        }
        case "info": {
            toast.info(message, {
            position: "top-center",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            });
           break;
        }
        default: {
            toast(message, {
            position: "top-center",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            });
           break;
        }
     }    
}