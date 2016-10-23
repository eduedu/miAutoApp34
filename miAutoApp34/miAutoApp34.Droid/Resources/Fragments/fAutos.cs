using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using SupportFragment = Android.Support.V4.App.Fragment;
using Android.Graphics;
using System.Threading;
using System.Net;
using System.IO;
using System.Json;
using Newtonsoft.Json;

namespace miAutoApp34.Droid {
	public class fAutos : SupportFragment {
		/// GLOBALES
		GridView gridview;
		public List<autoClass> mAutos;
		ISharedPreferences misDatos;
		Typeface tf3;
		LayoutInflater inflater;
		int anchoCol;
		string mCacheAutos;
		TextView titulo;
		ProgressBar progressBar1;
		List<string> ListaArchivosDeImagenesEnUso;
		public override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);
			//SetContentView(Resource.Layout.autos);
		}

		public override View OnCreateView(LayoutInflater _inflater, ViewGroup container, Bundle savedInstanceState) {
			inflater = _inflater;
			View view = inflater.Inflate(Resource.Layout.fAutos, container, false);

			misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			string terminar = misDatos.GetString("datoConAuto", "");
			if (terminar != "") {
				Activity.FinishAffinity();
				//return;
				//Finish();
			}

			//CONTROLES
			titulo = view.FindViewById<TextView>(Resource.Id.textView2);
			gridview = view.FindViewById<GridView>(Resource.Id.gridview);
			progressBar1 = view.FindViewById<ProgressBar>(Resource.Id.progressBar1);


			//FUENTES
			tf3 = Typeface.CreateFromAsset(inflater.Context.Assets, "fonts/ROBOTO-BLACK.TTF");
			titulo.Typeface = tf3;

			/////////////
			var metrics = Resources.DisplayMetrics;
			var widthInDp = metrics.WidthPixels; //ConvertPixelsToDp(metrics.WidthPixels);
			var heightInDp = ConvertPixelsToDp(metrics.HeightPixels);
			anchoCol = widthInDp / 2;
			//gridview.ColumnWidth = widthInDp / 2;

			/////CARGAR DATOS
			//LeerAutosJSon();
			//gridview.Adapter = new ImageAdapter(inflater.Context, anchoCol, tf3, mAutos);


			///CLICK EN IMAGENES
			gridview.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args) {
				/*
				Toast.MakeText(inflater.Context, args.Position.ToString(), ToastLength.Short).Show();
				FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
				//Remove fragment else it will crash as it is already added to backstack
				Fragment prev = Activity.FragmentManager.FindFragmentByTag("dialogAutos");
				if (prev != null) {
					ft.Remove(prev);
				}
				ft.AddToBackStack(null);
				// Create and show the dialog.
				fDetallesAuto newFragment = fDetallesAuto.NewInstance(null,
												mAutos[args.Position].nombre, mAutos[args.Position].descripcion,
												mAutos[args.Position].url1,
												mAutos[args.Position].url2,
												mAutos[args.Position].url3,
												mAutos[args.Position].url4
												);
				//Add fragment
				newFragment.Show(ft, "dialogAutos");
				*/
				var miIntent = new Intent(Context, typeof(fDetallesAuto));
				miIntent.AddFlags(ActivityFlags.NoAnimation);

				miIntent.PutExtra("detalle.url1", mAutos[args.Position].url1);
				miIntent.PutExtra("detalle.url2", mAutos[args.Position].url2);
				miIntent.PutExtra("detalle.url3", mAutos[args.Position].url3);
				miIntent.PutExtra("detalle.url4", mAutos[args.Position].url4);
				miIntent.PutExtra("detalle.titulo", mAutos[args.Position].nombre);
				miIntent.PutExtra("detalle.descripcion", mAutos[args.Position].descripcion);
				miIntent.PutExtra("detalle.precio", mAutos[args.Position].precio);
				miIntent.PutExtra("detalle.id", mAutos[args.Position].id.ToString());
				//Console.WriteLine("auto:" + mAutos[args.Position].nombre);
				//Console.WriteLine("id:" + mAutos[args.Position].id);

				StartActivity(miIntent);
				//this.Activity.OverridePendingTransition(Resource.Style.animacionesRefe, Resource.Style.animacionesRefe);
				//this.Activity.OverridePendingTransition(Resource.Animation.anima1, Resource.Animation.anima2);

			};

			MostrarEstado(false);
			CargarDatosDesdeCache();
			//ActualizarImagenesEnCache();
			ActualizarInterface();

			return view;
			//  return base.OnCreateView(inflater, container, savedInstanceState);
		}
		/// ON RESUME
		public override void OnResume() {
			base.OnResume();
			misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			string terminar = misDatos.GetString("datoConAuto", "");
			if (terminar != "") {
				Activity.FinishAffinity();
				//return;
				//Finish();
			}
			/*
			new Thread(new ThreadStart(delegate {
				bool SinConexion = false;
				//LEO VARIABLE "CAMBIO"
				string webCambios = solicitudesWeb.getVariable("cambios");
				if (webCambios == "SinConexion") {
					SinConexion = true;
				}
				if (!SinConexion) {
					//Guardo los cambios en un array: cambio[0]=noticias, cambio[1]=promo, cambio[2]=autos
					string[] webCambio = webCambios.Split('-');
					//LEO "preCambio" en PREFERENCIAS
					misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
					string preCambios = misDatos.GetString("preCambios", "0-0-0");
					string[] preCambio = preCambios.Split('-');

					Console.WriteLine("webCambios:" + webCambios);
					Console.WriteLine("preCambios:" + preCambios);
					Console.WriteLine("webCambio[2]:" + webCambio[2]);
					Console.WriteLine("preCambio[2]:" + preCambio[2]);
					//comparo bd web con bd interna:
					//si el cambio fue en NOTICIAS:
					if (webCambio[2] != preCambio[2]) {
						Console.WriteLine("Hubo cambios en autos");

						Activity.RunOnUiThread(() => {
							//Acutalizando...
							MostrarEstado(true);
						});
						//Console.WriteLine("Cargando noticias en BD web y actualizando Preferences");
						bool tmpConexionInternet = LeerBDAutosYActualizarPreferencias();
						if (tmpConexionInternet) {
							Console.WriteLine("DAtos leidos y guardados");
							//guardar preCambios
							GuardarCambioPreferencesX(2, webCambio[2]);
							//Actualizar UI
							CargarDatosDesdeCache();

							//bool okActualizarAutos = ActualizarImagenesEnMemoriaInterna();
							ActualizarImagenesEnCache();
							Activity.RunOnUiThread(() => {
								
								//if (okActualizarAutos) {
									ActualizarInterface();
									//Toast.MakeText(this.Context, webCambios, ToastLength.Long).Show();
									MostrarEstado(false);
								//}
								//else {
								//	SinConexion = true;
								//}

							});

						}
						else {
							Console.WriteLine("No se pudieron leer los datos");
							SinConexion = true;
						}
					}
					else {
						Console.WriteLine("No hubo cambios en AUTOS");

					}
				}
				if (SinConexion) {
					Activity.RunOnUiThread(() => MostrarEstado(true, "(Sin conexión)"));
				}

			})).Start();
			*/

			new Thread(new ThreadStart(delegate {
				bool SinConexion = false;
				//LEO VARIABLE "CAMBIO"
				string webCambios = solicitudesWeb.getVariable("cambios");
				if (webCambios == "SinConexion") {
					SinConexion = true;
				}
				if (!SinConexion) {
					//Guardo los cambios en un array: cambio[0]=noticias, cambio[1]=promo, cambio[2]=autos
					string[] webCambio = webCambios.Split('[');
					//LEO "preCambio" en PREFERENCIAS
					misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
					string preCambios = misDatos.GetString("preCambios", "0[0[0[");
					//preCambios = "0[0[0[";
					string[] preCambio = preCambios.Split('[');


					Console.WriteLine("webCambios:" + webCambios);
					Console.WriteLine("preCambios:" + preCambios);
					Console.WriteLine("webCambio[2]:" + webCambio[2]);
					Console.WriteLine("preCambio[2]:" + preCambio[2]);
					//comparo bd web con bd interna:
					//si el cambio fue en NOTICIAS:
					//Console.WriteLine("Cantidad de mAutos" + mAutos.Count.ToString());
					if (webCambio[2] != preCambio[2]) {
						Console.WriteLine("Hubo cambios en autos");

						Activity.RunOnUiThread(() => {
							//Acutalizando...
							MostrarEstado(true);
						});
						//Console.WriteLine("Cargando noticias en BD web y actualizando Preferences");
						bool tmpConexionInternet = LeerBDAutosYActualizarPreferencias();
						if (tmpConexionInternet) {
							//Console.WriteLine("DAtos leidos y guardados");
							//guardar preCambios
							
							//Actualizar UI
							CargarDatosDesdeCache();
							//ActualizarImagenesEnCache();
							bool okActualizarAutos = ActualizarImagenesEnMemoriaInterna();
							Activity.RunOnUiThread(() => {
								//Console.WriteLine("Actualizando UI");
								if (okActualizarAutos) {
									ActualizarInterface();
									memoriaInterna.GuardarCambioPreferencesX(2, webCambio[2]);
									//Toast.MakeText(this.Context, webCambios, ToastLength.Long).Show();
									MostrarEstado(false);
								}
								else {
									SinConexion = true;
								}

							});
						}
						else {
							Console.WriteLine("No se pudieron leer los datos");
							SinConexion = true;
						}
					}
					else {
						//Console.WriteLine("No hubo cambios en AUTOS");
					}
				}
				if (SinConexion) {
					Activity.RunOnUiThread(() => MostrarEstado(true, "(Sin conexión)"));
				}
			})).Start();


		}
		public override void OnDestroy() {
			base.OnDestroy();
		}
		public override void OnPause() {
			base.OnPause();
			//Console.WriteLine("ADAPTER COUNT:" +gridview.Adapter.Count.ToString());
		}
		/// FUNCIONES------------------------------------------------------------
		public void MostrarEstado(bool mostrar, string texto = "(Actualizando...)") {
			if (mostrar) {
				/*
mCargando.Text = texto;
//mCargando.SetHeight()
mCargando.Visibility = ViewStates.Visible;
mCargando.LayoutParameters.Height= LinearLayout.LayoutParams.WrapContent;
*/
				//titulo.Typeface = fntRegular;
				titulo.Text = texto;
				progressBar1.Visibility = ViewStates.Visible;
			}
			else {
				/*
mCargando.SetHeight(0);
mCargando.Visibility = ViewStates.Invisible;
*/
				//TextView temp = view.FindViewById<TextView>(Resource.Id.temp);
				//titulo.Typeface = fntBlack;
				//temp.Text = "¡Bienvenido " + nya + "! \n fid:" + fid;
				titulo.Text = "¡ESTÁS A UN TOQUE DE TU PRÓXIMO AUTO!";
				progressBar1.Visibility = ViewStates.Gone;
			}
		}
		public void CargarDatosDesdeCache() {
			misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			//preNoticiasId = misDatos.GetString("preNoticiasId", "");
			mCacheAutos = misDatos.GetString("cacheAutos", "");
			if (mCacheAutos != "") {
				//paso de string a lista de objetos
				List<autoClass> arrayAutos = JsonConvert.DeserializeObject<List<autoClass>>(mCacheAutos);
				//borro la lista de objetos global mAutos
				//List<autoClass> 
				mAutos = new List<autoClass>();
				//asigno el nuevo valor recien leido del cache a la lista de objetos global mAutos
				mAutos = arrayAutos;
				
			}
			//Console.WriteLine("cacheAutos:" + mCacheAutos);
		}
		public bool ActualizarImagenesEnMemoriaInterna() {
			bool retorno = true;
			if (mCacheAutos != "") {
				if (mAutos.Count != 0) {
					ListaArchivosDeImagenesEnUso = new List<string>();
					foreach (autoClass auto in mAutos) {
						//Console.WriteLine(auto.nombre);
						Activity.RunOnUiThread(() => MostrarEstado(true, "Actualizando datos ("+auto.nombre+" 1/4)..."));
						
						bool a1 = ChequearSiExisteImagenEnMemoriaInterna(auto.url1);
						Activity.RunOnUiThread(() => MostrarEstado(true, "Actualizando datos (" + auto.nombre + " 2/4)..."));
						bool a2 = ChequearSiExisteImagenEnMemoriaInterna(auto.url2);
						Activity.RunOnUiThread(() => MostrarEstado(true, "Actualizando datos (" + auto.nombre + " 3/4)..."));
						bool a3 = ChequearSiExisteImagenEnMemoriaInterna(auto.url3);
						Activity.RunOnUiThread(() => MostrarEstado(true, "Actualizando datos (" + auto.nombre + " 4/4)..."));
						bool a4 = ChequearSiExisteImagenEnMemoriaInterna(auto.url4);
						Activity.RunOnUiThread(() => ActualizarInterface());
						if (a1 && a2 && a3 && a4) {
							
							
						}
						else {
							//problema de conexion a internet
							retorno = false;
						}
					}
					BorrarArchivosQueNoSeUsan();
				}
			}
			return retorno;
		}
		public bool ChequearSiExisteImagenEnMemoriaInterna(string url) {
			//DEVUELVE FALSE SI NO HAY CONEXION A INTERNET
			bool retorno = true;
			if (url != "") {
				//Console.WriteLine("-------------URL:" + url);
				string urlAlfa = memoriaInterna.convertirEnAlfaNumerico(url);
				//Console.WriteLine("URL Alfanumerico:" + urlAlfa);

				//agregar a la lista de archivos que se usan
				ListaArchivosDeImagenesEnUso.Add(urlAlfa);

				Context context = Application.Context;
				string[] archivos = context.FileList();
				//Console.WriteLine("*****************************");
				if (archivos != null) {
					//Console.WriteLine("ARCHIVOS.COUNT:" + archivos.Count());
					//chequear si existe, sino bajar la imagen
					bool existeArchivo = false;
					foreach (string archivo in archivos) {
						//Console.WriteLine(urlAlfa + "=" + archivo + "?");
						if (archivo == urlAlfa) {
							//Console.WriteLine("Iguales");
							existeArchivo = true;
							break;
						}
					}
					if (!existeArchivo) {
						var imageBitmap = solicitudesWeb.GetImageBitmapFromUrl(url);
						if (imageBitmap != null) {
							//var tmpPromoPicString = Convert.ToBase64String(arrayImagenWeb);
							memoriaInterna.GuardarImagen(imageBitmap, urlAlfa);
							imageBitmap.Recycle();
						}
						else {
							//DEVUELVE FALSE SI NO HAY CONEXION A INTERNET
							retorno = false;
						}
					}
				}
				else {
					//Console.WriteLine("NO existen Archivos");
				}

				//Console.WriteLine("*****************************");
			}
			return retorno;
		}
		public void BorrarArchivosQueNoSeUsan() {
			ListaArchivosDeImagenesEnUso.Add("promo.png");
			int i = 0;
			foreach(var item in ListaArchivosDeImagenesEnUso) {
				i++;
				Console.WriteLine(i.ToString() + "-" + item);
			}
			Console.WriteLine("TOTAL ARCHIVOS EN USO:" + i.ToString());

			i = 0;
			Context context = Application.Context;
			string[] archivos = context.FileList();
			foreach(var archivo in archivos) {
				bool EnUso = false;
				foreach (var item in ListaArchivosDeImagenesEnUso) {
					if (item == archivo) {
						EnUso = true;
					}
				}
				if (!EnUso) {
					Console.Write("borrar siguiente:");
					memoriaInterna.BorrarImagen(archivo);
				}
				i++;
				Console.WriteLine(i.ToString() + "-" + archivo);
				
			}
			Console.WriteLine("TOTAL ARCHIVOS:" + i.ToString());
			Console.WriteLine("--------------------");
		}
		public void ActualizarImagenesEnCache() {  ///no se usa esta funcion
			if (mCacheAutos != "") {
				if (mAutos.Count != 0) {
					foreach (autoClass auto in mAutos) {
						//Console.WriteLine(auto.nombre);

						ChequearSiExisteEnCacheEstaImagen(auto.url1);
						ChequearSiExisteEnCacheEstaImagen(auto.url2);
						ChequearSiExisteEnCacheEstaImagen(auto.url3);
						ChequearSiExisteEnCacheEstaImagen(auto.url4);
					}
				}
			}
		}
		public void ActualizarInterface() {
			if (mCacheAutos != "") {
				//Console.WriteLine("anchoCOl: " + anchoCol.ToString());
				//Console.WriteLine("mAutos:" + mAutos.Count);
				gridview.Adapter = null;
				gridview.Adapter = new ImageAdapter(inflater.Context, anchoCol, tf3, mAutos);
				
			}

		}

		public void ChequearSiExisteEnCacheEstaImagen(string url) { ///NO se usa esta funcion
			//proceso SOLO si no está vacio el campo
			if (url != "") {
				//Console.WriteLine("URL:" + url);
				misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
				ISharedPreferencesEditor tmpCargarDatos = misDatos.Edit();
				string tmpNombre = misDatos.GetString(url, "");
				//si no existe un valor de cache con ese url, crearlo y descargar imagen
				if (tmpNombre == "") {
					tmpCargarDatos.PutString(url, url);
					var imageBitmap =solicitudesWeb.GetImageBitmapFromUrl(url);
					var str = "";
					using (var stream = new MemoryStream()) {
						imageBitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
						
						var bytes = stream.ToArray();
						str = Convert.ToBase64String(bytes);
						//Console.WriteLine(str);
					}
					imageBitmap.Recycle();
					tmpCargarDatos.PutString(url, str.ToString());
					tmpCargarDatos.Apply();

				}
			}
		}


		private int ConvertPixelsToDp(float pixelValue) {
			var dp = (int)((pixelValue) / Resources.DisplayMetrics.Density);
			return dp;
		}

		public bool LeerBDAutosYActualizarPreferencias() {
			bool retorno = false;
			string uriStr = "https://script.google.com/macros/s/AKfycbwAlPVE6SLC6MTEiGWrzqUZ2iYsoiTyl_yKet__P5HPU3TyU04/exec?action=getCeldas";
			try {
				var request = HttpWebRequest.Create(@uriStr);
				request.ContentType = "application /json";
				request.Method = "GET";
				HttpWebResponse response = request.GetResponse() as HttpWebResponse;
				using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
					var content = reader.ReadToEnd();
					var arrayRegistros = JsonValue.Parse(content);

					List<autoClass> tmpAutos = new List<autoClass>();
					int i = 1;
					foreach (var registro in arrayRegistros) {
						//SALTEA EL PRIMER ELEMENTO
						if (i != 1) {
							//Console.WriteLine(i.ToString()+"-"+registro.ToString());
							string tmpStringCampos = registro.ToString();
							//Console.WriteLine(tmpStringCampos);
							tmpStringCampos = tmpStringCampos.Substring(2, tmpStringCampos.Length - 2);
							//Console.WriteLine(tmpStringCampos);
							tmpStringCampos = tmpStringCampos.Substring(0, tmpStringCampos.Length - 1);
							//Console.WriteLine(tmpStringCampos);
							string[] campo = tmpStringCampos.Split(new[] { "\", \"", "\", " }, StringSplitOptions.None);
							string[] fe1 = campo[0].Split('T');
							string[] fe2 = fe1[0].Split('-');
							string tmpFechaFormateada = fe2[2] + "/" + fe2[1] + "/" + fe2[0].Substring(2, 2) + " " + fe1[1].Substring(0, 5);
							if (campo[4].Trim() != "") {
								tmpAutos.Add(new autoClass() {
									id = Int32.Parse(campo[8]),
									fecha = campo[0],
									nombre = campo[1],
									precio = campo[2],
									descripcion = campo[3],
									url1 = campo[4],
									url2 = campo[5],
									url3 = campo[6],
									url4 = campo[7],
								});
							}
							//Console.WriteLine("id:" + campo[8]);
							//Console.WriteLine("nombre:" + campo[1]);
						}
						i++;
					}
					tmpAutos.Reverse();

					///Cargar autos a pref.
					ISharedPreferencesEditor tmpCargarDatos = misDatos.Edit();
					tmpCargarDatos.PutString("cacheAutos", JsonConvert.SerializeObject(tmpAutos));
					tmpCargarDatos.Apply();



					//Assert.NotNull(content);
					retorno = true;
				}
			}
			catch (WebException ex) {
				Console.WriteLine("ERROR AL QUERER LEER BD AUTOS: " + ex.ToString());
				retorno = false;
			}
			return retorno;
		}
		
		private byte[] GetImageByteArrayFromUrl(string url) {
			byte[] imageArray = null;
			using (var webClient = new WebClient()) {
				try {
					var imageBytes = webClient.DownloadData(url);
					if (imageBytes != null && imageBytes.Length > 0) {
						//imageArray = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
						imageArray = imageBytes;
					}
				}
				catch (Exception ex) {
					Console.WriteLine("Error edu: " + ex.Message);
				}

			}

			return imageArray;
		}

		/*	public void LeerAutosJSon(Context context, int ancho, Typeface typeface, List<autoClass> _listaDeAutos) {
				new Thread(new ThreadStart(delegate {
					//string uriStr = "https://script.google.com/macros/s/AKfycbx1jMj101jA8O0DcVEY2nqMsMwYmOZN5Krvh6ZASNrWwvNKfu_j/exec?action=getCeldas";
					string uriStr = "https://script.google.com/macros/s/AKfycbwAlPVE6SLC6MTEiGWrzqUZ2iYsoiTyl_yKet__P5HPU3TyU04/exec?action=getCeldas";
					try {
						var request = HttpWebRequest.Create(@uriStr);
						request.ContentType = "application /json";
						request.Method = "GET";
						HttpWebResponse response = request.GetResponse() as HttpWebResponse;
						using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
							var respuesta = reader.ReadToEnd();
							//SI DEVUELVE DATOS:
							if (!string.IsNullOrWhiteSpace(respuesta)) {
								//var resp = JsonConvert.DeserializeObject(respuesta);

								//List<autoClass> tempAutos = new List<autoClass>();
								mAutos = new List<autoClass>();
								//JArray arrayRegistros = JArray.Parse(respuesta);
								var arrayRegistros = JsonValue.Parse(respuesta);
								//Console.WriteLine(" Cantidad de Registros: " + arrayRegistros.Count);
								int i = 1;
								foreach (var registro in arrayRegistros) {
									//SALTEA EL PRIMER ELEMENTO
									if (i != 1) {
										string tmpStringCampos = registro.ToString();
										//Console.WriteLine(tmpStringCampos);
										tmpStringCampos = tmpStringCampos.Substring(2, tmpStringCampos.Length - 2);
										//Console.WriteLine(tmpStringCampos);
										tmpStringCampos = tmpStringCampos.Substring(0, tmpStringCampos.Length - 1);
										Console.WriteLine(tmpStringCampos);
										string[] campo = tmpStringCampos.Split(new[] { "\", \"", "\", " }, StringSplitOptions.None);

										//Console.WriteLine("LEEEEENGTH: " + campo.Length.ToString());
										mAutos.Add(new autoClass() {
											id = Int32.Parse(campo[8]),
											fecha = campo[0],
											nombre = campo[1],
											precio = campo[2],
											descripcion = campo[3],
											url1 = campo[4],
											url2 = campo[5],
											url3 = campo[6],
											url4 = campo[7],
										});
									}
									i++;
								}
								Activity.RunOnUiThread(() => {
									gridview.Adapter = new ImageAdapter(context, ancho, typeface, mAutos);
								});
								/*
	mAutos = tempAutos;
	Application.Current.Dispatcher.BeginInvoke((Action)(() => {
	mCambioSeleccionGridAutoDesdeClick = false;
	var gridActual = gridAutos;
	var selec = gridActual.SelectedIndex;
	Boolean tieneFoco = gridActual.IsFocused;
	gridActual.ItemsSource = mAutos;
	gridActual.SelectedIndex = selec;
	//Console.WriteLine("Selec: " + selec);
	//Console.WriteLine("tieneFoco: " + tieneFoco);
	if (tieneFoco) {
	gridActual.Focus();
	Keyboard.Focus(gridActual);
	}
	}), DispatcherPriority.Normal, null);
	*/
		/*

								}
								else {
									Console.WriteLine(" ERROR: Respuesta sin datos desde el Servidor ");
								}
								if (respuesta != "-1") {
									//Application.Current.Dispatcher.BeginInvoke((Action)(() => {
									//}), DispatcherPriority.Normal, null);
								}
								else {
								}
							}
							//RunOnUiThread(() => progressDialog.Hide());
						}
						catch (WebException ex) {
							Console.WriteLine("ERROR DE CONEXION al Leer Base de Datos: " + ex.Message);

						}
					})).Start();
				}

		*/
	}
	/// /////////////////////////////////////////////////////////////////////////////
	public class ImageAdapter : BaseAdapter {
		Context context;
		int ancho;
		Typeface tf;
		//int[] thumbIds;
		List<autoClass> listaDeAutos = new List<autoClass>();

		public ImageAdapter(Context c, int _ancho, Typeface _tf, List<autoClass> _listaDeAutos) {
			context = c;
			ancho = _ancho;
			tf = _tf;
			//thumbIds = _thumbIds;
			listaDeAutos = _listaDeAutos;
		}

		public override int Count {
			//get { return thumbIds.Length; }
			get { return listaDeAutos.Count; }
			//get { return 3; }
		}

		public override Java.Lang.Object GetItem(int position) {
			return null;
		}

		public override long GetItemId(int position) {
			return 0;
		}
		/*
// create a new ImageView for each item referenced by the Adapter
public override View GetView(int position, View convertView, ViewGroup parent)
{
ImageView imageView;


if (convertView == null)
{  // if it's not recycled, initialize some attributes
		imageView = new ImageView(context);
		imageView.LayoutParameters = new GridView.LayoutParams(ancho, ancho);
		imageView.SetScaleType(ImageView.ScaleType.CenterInside);
		imageView.SetPadding(8, 8, 8, 8);
}
else
{
		imageView = (ImageView)convertView;
}

imageView.SetImageResource(thumbIds[position]);
return imageView;
}
*/
		public  override  View GetView(int position, View convertView, ViewGroup parent) {
			View view;

			if (convertView == null) {
				view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.itemAuto, parent, false);
			}
			else {
				view = convertView;
			}
			//referencia a CONTROLES
			var imagen = view.FindViewById<ImageView>(Resource.Id.my_image_view);
			var texto = view.FindViewById<TextView>(Resource.Id.my_text_view);
			//imageView.SetPadding(8, 8, 8, 8);

			//imageView.SetImageResource(thumbIds[position]);
			///Imagen de auto
			//var imageBitmap = GetImageBitmapFromUrl(listaDeAutos[position].url1);

			//var imageBitmap = memoriaInterna.cargarImagenDesdeCache(listaDeAutos[position].url1);

			//var imageBitmap =  utilitarios.GetImageForDisplay(listaDeAutos[position].url1, 150, 150).Result;
			string tmpUrlAlfa = memoriaInterna.convertirEnAlfaNumerico(listaDeAutos[position].url1);
			Bitmap imageBitmap = memoriaInterna.LeerImagen(tmpUrlAlfa,25);
			if (imageBitmap != null) {
			//if (tmpUrlAlfa != "") { 

				//imageView.LayoutParameters = new GridView.LayoutParams(ancho, ancho);
				//imagen.SetScaleType(ImageView.ScaleType.CenterInside);
				imagen.SetScaleType(ImageView.ScaleType.FitCenter);
				imagen.SetMaxHeight(ancho);
				imagen.SetImageBitmap(imageBitmap);
				//imagen.SetImageBitmap(memoriaInterna.LeerImagen(tmpUrlAlfa));
				//imageBitmap.Recycle();

				//textView.Text = "Texto " + position.ToString();
				texto.Text = listaDeAutos[position].nombre;
				//TextView miTexto1 = FindViewById<TextView>(Resource.Id.my_text_view);
				texto.Typeface = tf;
			}
			else {
				imagen.Visibility = ViewStates.Gone;
				texto.Visibility = ViewStates.Gone;
			}
			imageBitmap = null;
			//imageBitmap.Recycle();

			


			return view;
		}



	}

}