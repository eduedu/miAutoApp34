using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Collections.Specialized;
using System.Net;
using System.Threading;
using System.IO;
using Android.Graphics;

namespace miAutoApp34.Droid {
	public static class solicitudesWeb {
		public static bool grabarDatoUsuario(string NumeroUsuario, string campo, string valor) {
			bool retorno;

			string ssKey = @"AKfycbxB98IS32T9mCUJbSccWmBg17LMRGmcvB7Kqa9lFcM_8eiM6rE";
			string uriStr = "https://script.google.com/macros/s/" + ssKey + "/exec?action=setCampoUsuario" + "&url=" + NumeroUsuario.ToString().Trim() + "&campo=" + campo.ToString().Trim() + "&valor=" + @valor.ToString().Trim();
			Console.WriteLine(uriStr);
			try {
				var request = HttpWebRequest.Create(@uriStr);
				request.ContentType = "application /json";
				request.Method = "GET";
				HttpWebResponse response = request.GetResponse() as HttpWebResponse;
				using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
					var respuesta = reader.ReadToEnd();
					Console.Out.WriteLine("RESPUESTA AL INTENTAR GRABAR datos USUARIO: " + respuesta.ToString());
					if (respuesta != "-1") {
						//Application.Current.Dispatcher.BeginInvoke((Action)(() => {
						//}), DispatcherPriority.Normal, null);
					}
					else {
					}
				}
				//RunOnUiThread(() => progressDialog.Hide());
				retorno = true;
			}
			catch (WebException ex) {
				Console.WriteLine("ERROR DE CONEXION al grabar DATO USUARIO: " + ex.Message);
				retorno = false;
			}
			//})).Start();
			return retorno;
		}

		public static bool solicitud(string asunto, bool cargarSolamenteElNumero = false, string notaSolicitud="") {
			bool respuesta;
			string num = "";
			string nya = "";
			//SI NO se carga solamente el numero, leer los otros datos de las preferencias
			if (!cargarSolamenteElNumero) {
				ISharedPreferences misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
				num = misDatos.GetString("num", "");
				nya = misDatos.GetString("nya", "");
			}
			else {
				num = variablesGlobales.numeroTemp;
			}


			//CARGAR DATOS EN BASE DE DATOS
			string formkey = "1n5An84TrTn4abT_QXJYc4-1fwhzBpLWEkYGsVt-4fvI";
			string cnya = "entry.483622950";
			string cnumero = "entry.2057588626";
			string casunto = "entry.34134450";
			string cnota = "entry.738188072";

			var keyval = new NameValueCollection();
			keyval.Add(cnumero, num);
			keyval.Add(cnya, nya);
			keyval.Add(casunto, asunto);
			keyval.Add(cnota, notaSolicitud);

			System.Net.WebClient wc = new WebClient();
			Uri uri = new Uri(@"https://docs.google.com/forms/d/" + formkey + @"/formResponse");
			//var progressDialog = ProgressDialog.Show(Context, "", "Registrado Pedido...", true);

			//new System.Threading.Thread(new ThreadStart(delegate {
			try {
				wc.UploadValues(uri, keyval);
				respuesta = true;
				/*
				Activity.RunOnUiThread(() => {
					//progressDialog.Hide();

					Android.App.FragmentTransaction ft = FragmentManager.BeginTransaction();
					//Remove fragment else it will crash as it is already added to backstack
					Android.App.Fragment prev = FragmentManager.FindFragmentByTag("dialog");
					if (prev != null) {
						ft.Remove(prev);
					}
					ft.AddToBackStack(null);
					// Create and show the dialog.
					dialogOKclass newFragment = dialogOKclass.NewInstance(null, "Solicitud registrada", "Un asesor se comunicará con usted en las próximas horas.");
					//Add fragment
					newFragment.Show(ft, "dialog");
				});
				*/


			}
			catch (WebException ex) {
				Console.WriteLine(ex.Message);
				//RunOnUiThread(() => progressDialog.Hide());
				//RunOnUiThread(() => Toast.MakeText(this, "sin conexión", ToastLength.Long).Show());

				//return;
				respuesta = false;

			}
			//})).Start();
			return respuesta;
		}
		public static string getVariable(string nombreVariable) {
			string respuesta = "";

			//string formkey = "1n5An84TrTn4abT_QXJYc4-1fwhzBpLWEkYGsVt-4fvI";
			//Uri uri = new Uri(@"https://docs.google.com/forms/d/" + formkey + @"/formResponse");
			string uriStr = "https://script.google.com/macros/s/AKfycbzqI79YB05eq7fCIwWIlWeo2Z3cS3ELD84LWvOgDP9H9XnvwUx6/exec?action=getVariable&url=" + nombreVariable;
			try {
				var request = HttpWebRequest.Create(@uriStr);
				request.ContentType = "application /json";
				request.Method = "GET";
				HttpWebResponse response = request.GetResponse() as HttpWebResponse;
				using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
					respuesta = reader.ReadToEnd().ToString();
					//Console.Out.WriteLine("-----VALOR DE LA VARIABLE " + respuesta.ToString());

					return respuesta;
				}

			}
			catch (WebException ex) {
				Console.WriteLine("ERROR DE CONEXION: " + ex.Message);
				respuesta = "SinConexion";
				return respuesta;
			}


		}
		public static string getDatosUsuario(string numerotel) {
			string respuesta = "";

			//string formkey = "1n5An84TrTn4abT_QXJYc4-1fwhzBpLWEkYGsVt-4fvI";
			//Uri uri = new Uri(@"https://docs.google.com/forms/d/" + formkey + @"/formResponse");
			
			string uriStr = "https://script.google.com/macros/s/AKfycbxB98IS32T9mCUJbSccWmBg17LMRGmcvB7Kqa9lFcM_8eiM6rE/exec?action=getDatosUsuario&url=" + numerotel.Trim();
			try {
				var request = HttpWebRequest.Create(@uriStr);
				request.ContentType = "application /json";
				request.Method = "GET";
				HttpWebResponse response = request.GetResponse() as HttpWebResponse;
				using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
					respuesta = reader.ReadToEnd().ToString();
					//Console.Out.WriteLine("-----DATOS USUARIO: " + respuesta.ToString());

					return respuesta;
				}

			}
			catch (WebException ex) {
				Console.WriteLine("ERROR DE CONEXION: " + ex.Message);
				respuesta = "SinConexion";
				return respuesta;
			}


		}
		public static string getDatosAuto(string idAuto) {
			string respuesta = "";

			//string formkey = "1n5An84TrTn4abT_QXJYc4-1fwhzBpLWEkYGsVt-4fvI";
			//Uri uri = new Uri(@"https://docs.google.com/forms/d/" + formkey + @"/formResponse");

			string uriStr = "https://script.google.com/macros/s/AKfycbwAlPVE6SLC6MTEiGWrzqUZ2iYsoiTyl_yKet__P5HPU3TyU04/exec?action=getDatosAuto&url=" + idAuto.Trim();
			try {
				var request = HttpWebRequest.Create(@uriStr);
				request.ContentType = "application /json";
				request.Method = "GET";
				HttpWebResponse response = request.GetResponse() as HttpWebResponse;
				using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
					respuesta = reader.ReadToEnd().ToString();
					//Console.Out.WriteLine("-----DATOS AUTO: " + respuesta.ToString());

					return respuesta;
				}

			}
			catch (WebException ex) {
				Console.WriteLine("ERROR DE CONEXION: " + ex.Message);
				respuesta = "SinConexion";
				return respuesta;
			}


		}
		public static string setReferidos(string refesUrl) {
			string retorno = "";
			//string uriStr = "https://script.google.com/macros/s/AKfycbzqI79YB05eq7fCIwWIlWeo2Z3cS3ELD84LWvOgDP9H9XnvwUx6/exec?action=getVariable&url=" + nombreVariable;
			string uriStr = "https://script.google.com/macros/s/AKfycbwNipOppi8Zio2YXlRLJJTqpVBifj3hMf3mK7El800MaaMbPiJN/exec?action=setRefes&url=" + refesUrl.Trim();
			try {
				var request = HttpWebRequest.Create(@uriStr);
				request.ContentType = "application /json";
				request.Method = "GET";
				HttpWebResponse response = request.GetResponse() as HttpWebResponse;
				using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
					retorno = reader.ReadToEnd().ToString();
					Console.Out.WriteLine("-----TODO OK CON EL SCRIPT? " + retorno.ToString());

					return retorno;
				}

			}
			catch (WebException ex) {
				Console.WriteLine("ERROR DE CONEXION: " + ex.Message);
				retorno = "-1";
				return retorno;
			}

			//return retorno;
		}
		public static string getReferidos(string numero) {
			string retorno = "";
			//string uriStr = "https://script.google.com/macros/s/AKfycbzqI79YB05eq7fCIwWIlWeo2Z3cS3ELD84LWvOgDP9H9XnvwUx6/exec?action=getVariable&url=" + nombreVariable;
			string uriStr = "https://script.google.com/macros/s/AKfycbwNipOppi8Zio2YXlRLJJTqpVBifj3hMf3mK7El800MaaMbPiJN/exec?action=getRefes&url=" + numero.Trim();

			try {
				var request = HttpWebRequest.Create(@uriStr);
				request.ContentType = "application /json";
				request.Method = "GET";
				HttpWebResponse response = request.GetResponse() as HttpWebResponse;
				using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
					retorno = reader.ReadToEnd().ToString();
					//Console.Out.WriteLine("-----TODO OK CON EL SCRIPT? " + retorno.ToString());

					return retorno;
				}

			}
			catch (WebException ex) {
				Console.WriteLine("ERROR DE CONEXION: " + ex.Message);
				retorno = "-1";
				return retorno;
			}

			//return retorno;
		}
		public static Bitmap GetImageBitmapFromUrl(string url) {
			Bitmap imageBitmap = null;

			if (url.Trim() != "") {
				using (var webClient = new WebClient()) {
					try {
						var imageBytes = webClient.DownloadData(url);
						if (imageBytes != null && imageBytes.Length > 0) {
							imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
						}
					}
					catch (Exception ex) {
						Console.WriteLine("Error edu: " + ex.Message);
					}

				}
			}


			return imageBitmap;
		}
	
	}

}