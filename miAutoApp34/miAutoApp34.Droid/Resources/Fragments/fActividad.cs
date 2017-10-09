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
using System.Threading;
using System.IO;
using System.Net;
using System.Json;
using Newtonsoft.Json;
using Android.Graphics;

namespace miAutoApp34.Droid {
	public class fActividad : SupportFragment {
		//private List<Linea> mItems;
		private ListView mListView;
		public Android.Graphics.Typeface fntBlack;
		public Android.Graphics.Typeface fntRegular;
		public Android.Graphics.Typeface fntBold;

		/// globales
		List<Linea> mItems;
		string nya;
		string num;
		TextView txtMensaje;
		TextView mCargando;
		View view;
		MyListViewAdapter adapter;
		string preNoticias;
		ISharedPreferences misDatos;
		string dValorLlave;
		TextView tvValorllave;
		string dNumeroWA;
		string dUrlCompartir;
		string dUrlCompartirTitulo;
		string dUrlCompartirTexto;
		string dUrlCompartirImagen;
		string dUrlCompartirMensaje;
		string miVersion;
		string nuevaVersion;
		string dRefeMax;

		public override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);


		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			view = inflater.Inflate(Resource.Layout.fActividad, container, false);
			//FUENTES
			//Android.Graphics.Typeface 
			fntRegular = Android.Graphics.Typeface.CreateFromAsset(inflater.Context.Assets, "fonts/ROBOTO-REGULAR.TTF");
			fntBold = Android.Graphics.Typeface.CreateFromAsset(inflater.Context.Assets, "fonts/ROBOTO-BOLD.TTF");
			Android.Graphics.Typeface fntCondensesBold = Android.Graphics.Typeface.CreateFromAsset(inflater.Context.Assets, "fonts/ROBOTOCONDENSED-BOLD.TTF");
			//Android.Graphics.Typeface 
			fntBlack = Android.Graphics.Typeface.CreateFromAsset(inflater.Context.Assets, "fonts/ROBOTO-BLACK.TTF");

			///CARGAR DATOSSSSS
			misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			num = misDatos.GetString("num", "");
			nya = misDatos.GetString("nya", "");
			string fid = misDatos.GetString("fid", "");
			dValorLlave = misDatos.GetString("valorLlave", "");
			dNumeroWA = misDatos.GetString("numeroWA", "");
			dUrlCompartir = misDatos.GetString("urlCompartir", "");
			dUrlCompartirTitulo = misDatos.GetString("urlCompartirTitulo", "");
			dUrlCompartirTexto = misDatos.GetString("urlCompartirTexto", "");
			dUrlCompartirImagen = misDatos.GetString("urlCompartirImagen", "");
			dUrlCompartirMensaje = misDatos.GetString("urlCompartirMensaje", "");
			miVersion = misDatos.GetString("miVersion", "");
			nuevaVersion = misDatos.GetString("nuevaVersion", miVersion);
			dRefeMax = misDatos.GetString("dRefeMax", "");


			///REFERENCIAS A CONTROLES//////////////////////////////////////////////////
			TextView texto1 = view.FindViewById<TextView>(Resource.Id.textView2);
			texto1.Typeface = fntRegular;
			tvValorllave = view.FindViewById<TextView>(Resource.Id.twValorLlave);
			tvValorllave.Typeface = fntBlack;
			Button btnReferidos = view.FindViewById<Button>(Resource.Id.button1);
			Button btnPromociones = view.FindViewById<Button>(Resource.Id.btnPromociones);
			Button btnConsultar = view.FindViewById<Button>(Resource.Id.btnConsultar);
			mListView = view.FindViewById<ListView>(Resource.Id.myListView);
			LinearLayout lista = view.FindViewById<LinearLayout>(Resource.Id.layoutLista);

			btnReferidos.Typeface = fntCondensesBold;
			btnReferidos.SoundEffectsEnabled = true;
			btnPromociones.Typeface = fntCondensesBold;
			btnPromociones.SoundEffectsEnabled = true;

			mCargando = view.FindViewById<TextView>(Resource.Id.cargando);
			mCargando.Typeface = fntRegular;
			//mCargando.Text = "(Actualizando....)";
			mCargando.Visibility = ViewStates.Invisible;
			mCargando.SetHeight(0);

			//////////TEMP///////////
			txtMensaje = view.FindViewById<TextView>(Resource.Id.temp);
			//txtMensaje.Typeface = fntBlack;
			//txtMensaje.Text = "¡Bienvenido " + nya + "!";
			MostrarTextoMensaje(false);


			////////////////BOTON REFERIDOSSSS///////////////////////////////
			btnReferidos.Click += (object sender, EventArgs args) => {
				//Android.Support.V4.App.FragmentTransaction refeTransaction = FragmentManager.BeginTransaction();
				//FragmentTransaction refeTransaction =  FragmentManager.BeginTransaction();
				//FragmentTransaction refeTransaction = Android.Support.V4.App.FragmentManager.GetObject();

				//fMainReferidos fReferidos = new fMainReferidos();
				//fReferidos.Show(null, "nada");
				//FragmentManager fragss = getFragmentManager();

				//var newFragment = new fMainReferidos();
				//var ft = FragmentManager.BeginTransaction();
				//ft.Add(Resource.Layout.fMainReferidos, newFragment);
				//ft.Attach(newFragment);
				//ft.Add

				//ft.Commit();

				//hace que muestre la ayuda//////////////
				memoriaInterna.mostrarAyudaReferidos();


				var miIntent = new Intent(container.Context, typeof(refeMain));
				miIntent.AddFlags(ActivityFlags.NoAnimation);
				//miIntent.PutExtra("nya", campo1.Text.Trim());
				//miIntent.PutExtra("u", campo1.Text.Trim());
				//miIntent.PutExtra("p", campo3.Text.Trim());
				StartActivity(miIntent);

			};

			////////////////BOTON PROMOCIONES///////////////////////////////
			btnPromociones.Click += (s, o) => {
				var miIntent = new Intent(container.Context, typeof(refeMain));
				miIntent.AddFlags(ActivityFlags.NoAnimation);
				//miIntent.PutExtra("nya", campo1.Text.Trim());
				//miIntent.PutExtra("u", campo1.Text.Trim());
				//miIntent.PutExtra("p", campo3.Text.Trim());
				miIntent.PutExtra("tabSel", "1");
				StartActivity(miIntent);

			};

			//BOTON CONSULTA
			btnConsultar.Click += (o, s) => {
				//------------------------------------------
				Android.App.FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
				//Remove fragment else it will crash as it is already added to backstack
				Android.App.Fragment prev = Activity.FragmentManager.FindFragmentByTag("dialogConsulta");
				if (prev != null) {
					ft.Remove(prev);
				}
				ft.AddToBackStack(null);
				// Create and show the dialog.
				//dialogOKclass newFragment = dialogOKclass.NewInstance(null, "Solicitud registrada", "Un asesor se comunicará con usted en las próximas horas.");
				dialogConsulta newFragmentContactar = dialogConsulta.NewInstance(null, "Consulta", "Mensaje para MiAutoPlan:");
				//Add fragment
				newFragmentContactar.Show(ft, "dialogConsulta");
				//------------------------------------------
			};

			///CLICK EN EL TEXTO DE TITULO
			txtMensaje.Click += (s, o) => {
				if (miVersion.Trim() != nuevaVersion.Trim()) {

					//market://details?id=com.gmail.educontratodos.miautoplan1
					//string appPackageName = "com.gmail.educontratodos.miautoplan1";
                    string appPackageName = "com.gmail.ecabreradev.miautoplan";

                    try {
						StartActivity(new Intent(Intent.ActionView, Android.Net.Uri.Parse("market://details?id=" + appPackageName)));
					}
					catch (Exception ex) {
						StartActivity(new Intent(Intent.ActionView, Android.Net.Uri.Parse("http://play.google.com/store/apps/details?id=" + appPackageName)));
					}

				}
			};

			CargarDatosDesdePreferencesNoticias();
			ActualizarInterfaceNoticias();
			tvValorllave.Text = "$ " + dValorLlave;


			//CLICK EN ELEMENTOS DE LA LISTA
			mListView.ItemClick += (o, e) => {
				//Console.WriteLine("CLICK EN LISTA. Posicion:" + e.Position.ToString());

				//var item = this.adapter.GetItem(e.Position).ToString();

				//Make a toast with the item name just to show it was clicked
				//Toast.MakeText(this, item.Name + " Clicked!", ToastLength.Short).Show();

				//Console.WriteLine("Contenido" + mItems[e.Position].linea1);
				string tmpFiltro = "Mensaje exclusivo para";
				if (mItems[e.Position].linea1.Length >= tmpFiltro.Length) {
					//if (mItems[position].linea1.Substring(0, 4) == "Para") {
					if (mItems[e.Position].linea1.Substring(0, tmpFiltro.Length) == tmpFiltro) {
						//string colorMensajePersonal = "#3E61AD";
						//-------------------------------------
						Android.App.FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
						//Remove fragment else it will crash as it is already added to backstack
						Android.App.Fragment prev = Activity.FragmentManager.FindFragmentByTag("dialogConsulta");
						if (prev != null) {
							ft.Remove(prev);
						}
						ft.AddToBackStack(null);
						// Create and show the dialog.
						//dialogOKclass newFragment = dialogOKclass.NewInstance(null, "Solicitud registrada", "Un asesor se comunicará con usted en las próximas horas.");
						dialogConsulta newFragmentContactar = dialogConsulta.NewInstance(null, "Consulta", "Mensaje para MiAutoPlan:");
						//Add fragment
						newFragmentContactar.Show(ft, "dialogConsulta");
						//------------------------------------------
					}
				}
			};


			//lista.Click += (o,s) =>{
			//	Console.WriteLine("CLICK EN LISTA");
			//mListView.Click();
			//adapter.ItemLayout_Click(o, s);
			//};
			/*
			lista.Touch += (o, s) => {
				string message;
				switch (s.Event.Action & MotionEventActions.Mask) {
					case MotionEventActions.Down:
					case MotionEventActions.Move:
						message = "Touch Begins";
						break;

					case MotionEventActions.Up:
						message = "Touch Ends";
						break;

					default:
						message = string.Empty;
						break;
				}

				Console.WriteLine(message);
			};
			*/
			return view;
			// return base.OnCreateView(inflater, container, savedInstanceState);
		}
		/// ON RESUME
		public override void OnResume() {
			base.OnResume();
			MostrarTextoMensaje(false);
			//Console.WriteLine("------OnResume");
			new Thread(new ThreadStart(delegate {
				bool SinConexion = false;
				//LEO VARIABLE "CAMBIO"
				string webCambios = solicitudesWeb.getVariable("cambios");
				if (webCambios == "SinConexion") {
					SinConexion = true;
				}
				//Guardo los cambios en un array: cambio[0]=noticias, cambio[1]=promo, cambio[2]=autos
				string[] webCambio = webCambios.Split('[');
				//LEO "preCambio" en PREFERENCIAS
				misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
				string preCambios = misDatos.GetString("preCambios", "0[0[0[0[ [ [ [ [ [");
				string[] preCambio = preCambios.Split('[');

				//cargar el valor de la llave


				/*
				Console.WriteLine("webCambios:" + webCambios);
				Console.WriteLine("preCambios:" + preCambios);
				Console.WriteLine("webCambio[0]:" + webCambio[0]);
				Console.WriteLine("preCambio[0]:" + preCambio[0]);
				*/

				//si cambio valorLlave
				//if ((webCambio[1] != preCambio[1])  && !SinConexion) {
				if (!SinConexion) {
					ISharedPreferencesEditor tmpCargarDatos = misDatos.Edit();

					//CONTROL DE VERSION
					string tmpNuevaVersion = webCambio[9].ToString().Trim();
					tmpCargarDatos.PutString("nuevaVersion", tmpNuevaVersion);
					tmpCargarDatos.Apply();
					nuevaVersion = tmpNuevaVersion;
					Console.WriteLine("miVersion:" + miVersion);
					Console.WriteLine("nuevaVersion:" + nuevaVersion);
					if ((webCambio[1] != dValorLlave) && !SinConexion) {
						dValorLlave = webCambio[1];
						//ISharedPreferencesEditor tmpCargarDatos = misDatos.Edit();
						tmpCargarDatos.PutString("valorLlave", dValorLlave);
						tmpCargarDatos.Apply();
						Activity.RunOnUiThread(() => {
							tvValorllave.Text = "$ " + dValorLlave;
						});
					}

					//si cambio numeroWA
					//if ((webCambio[1] != preCambio[1])  && !SinConexion) {
					if ((webCambio[3] != dNumeroWA) && !SinConexion) {
						dNumeroWA = webCambio[3];
						//ISharedPreferencesEditor tmpCargarDatos = misDatos.Edit();
						tmpCargarDatos.PutString("numeroWA", dNumeroWA);
						tmpCargarDatos.Apply();
						//Activity.RunOnUiThread(() => {
						//	tvValorllave.Text = "$ " + dValorLlave;
						//});
					}

					//si cambio urlCompartir
					if ((webCambio[4] != dUrlCompartir) && !SinConexion) {
						dUrlCompartir = webCambio[4].Trim();
						//ISharedPreferencesEditor tmpCargarDatos = misDatos.Edit();
						tmpCargarDatos.PutString("urlCompartir", dUrlCompartir);
						tmpCargarDatos.Apply();
					}
					//si cambio urlCompartirTitulo
					if ((webCambio[5] != dUrlCompartir) && !SinConexion) {
						dUrlCompartirTitulo = webCambio[5].Trim();
						//ISharedPreferencesEditor tmpCargarDatos = misDatos.Edit();
						tmpCargarDatos.PutString("urlCompartirTitulo", dUrlCompartirTitulo);
						tmpCargarDatos.Apply();
					}
					//si cambio urlCompartirTexto
					if ((webCambio[6] != dUrlCompartirTexto) && !SinConexion) {
						dUrlCompartirTexto = webCambio[6].Trim();
						//ISharedPreferencesEditor tmpCargarDatos = misDatos.Edit();
						tmpCargarDatos.PutString("urlCompartirTexto", dUrlCompartirTexto);
						tmpCargarDatos.Apply();
					}
					//si cambio urlCompartirImagen
					if ((webCambio[7] != dUrlCompartirImagen) && !SinConexion) {
						dUrlCompartirImagen = webCambio[7].Trim();
						//ISharedPreferencesEditor tmpCargarDatos = misDatos.Edit();
						tmpCargarDatos.PutString("urlCompartirImagen", dUrlCompartirImagen);
						tmpCargarDatos.Apply();
					}
					//si cambio urlCompartirMensaje
					if ((webCambio[8] != dUrlCompartirMensaje) && !SinConexion) {
						dUrlCompartirMensaje = webCambio[8].Trim();
						//ISharedPreferencesEditor tmpCargarDatos = misDatos.Edit();
						tmpCargarDatos.PutString("urlCompartirMensaje", dUrlCompartirMensaje);
						tmpCargarDatos.Apply();
					}

					//si cambio la cantidad de referidos permitidos por dia
					if ((webCambio[10] != dRefeMax) && !SinConexion) {
						dRefeMax = webCambio[10].Trim();
						//ISharedPreferencesEditor tmpCargarDatos = misDatos.Edit();
						tmpCargarDatos.PutString("dRefeMax", dRefeMax);
						tmpCargarDatos.Apply();
					}



                    //comparo bd web con bd interna:
                    //si el cambio fue en NOTICIAS:
                    if ((webCambio[0] != preCambio[0]) && !SinConexion) {

						Console.WriteLine("Hubo cambios en noticias");

						Activity.RunOnUiThread(() => {
							//mCargando.Text = "(Actualizando....)";
							//mCargando.Visibility = ViewStates.Visible;
							MostrarTextoMensaje(true);
						});
						//Console.WriteLine("Cargando noticias en BD web y actualizando Preferences");
						bool tmpConexionInternet = LeerBDNoticiasYActualizarPreferencias();
						if (tmpConexionInternet) {
							//Console.WriteLine("DAtos leidos y guardados");
							//guardar preCambios
							GuardarCambioPreferencesX(0, webCambio[0]);
							//Actualizar UI
							Activity.RunOnUiThread(() => {
								//Console.WriteLine("Actualizando UI");
								CargarDatosDesdePreferencesNoticias();
								ActualizarInterfaceNoticias();
								//Toast.MakeText(this.Context, webCambios, ToastLength.Long).Show();
								MostrarTextoMensaje(false);
							});

						}
						else {
							Console.WriteLine("No se pudieron leer los datos");
							SinConexion = true;
						}
					}
					else {
						Console.WriteLine("No hubo cambios");
						//Activity.RunOnUiThread(() => {
						//Toast.MakeText(this.Context, "Datos Actualizados", ToastLength.Long).Show();
						//});
					}
				}
				if (SinConexion) {
					Activity.RunOnUiThread(() => MostrarTextoMensaje(true, "(Sin conexión)"));
				}
				//accion UI


			})).Start();
		}
		/// ---------------------------------FUNCIONES-----------------------------------------------------------------------------
		public void MostrarTextoMensaje(bool mostrar, string texto = "(Actualizando...)") {
			if (mostrar) {
				/*
				mCargando.Text = texto;
				//mCargando.SetHeight()
				mCargando.Visibility = ViewStates.Visible;
				mCargando.LayoutParameters.Height= LinearLayout.LayoutParams.WrapContent;
				*/
				txtMensaje.Typeface = fntRegular;
				txtMensaje.Text = texto;
			}
			else {
				/*
				mCargando.SetHeight(0);
				mCargando.Visibility = ViewStates.Invisible;
				*/
				//TextView temp = view.FindViewById<TextView>(Resource.Id.temp);
				txtMensaje.Typeface = fntBlack;
				//temp.Text = "¡Bienvenido " + nya + "! \n fid:" + fid;
				txtMensaje.Text = "¡Bienvenido " + nya + "!";
				//txtMensaje.Text = "VErsion:" + miVersion;
			}
			if (miVersion.Trim() != nuevaVersion.Trim() && nuevaVersion.Trim() != "") {
				//Console.WriteLine("VERSIONES:" + miVersion + "-" + nuevaVersion;)
				txtMensaje.SetTextColor(Color.ParseColor("#ff0000"));
				txtMensaje.Typeface = fntBlack;
				txtMensaje.Text = "Hay una nueva versión de la app.\nClick aquí para actualizar.";
				//txtMensaje.Text = "VErsion:" + miVersion;
				//txtMensaje.Text = "VERSIONES:" + miVersion + "-" + nuevaVersion;
			}
			else {
				txtMensaje.SetTextColor(Color.ParseColor("#4d4d4d"));
			}
		}

		/// PREFERENCES
		public void GuardarCambioPreferencesX(int campo, string valor) {
			ISharedPreferences tmpMisDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			string preCambios = tmpMisDatos.GetString("preCambios", "0[0[0");
			string[] preCambio = preCambios.Split('[');
			preCambio[campo] = valor;
			string finalCambios = "";
			for (int i = 0; i < preCambio.Length; i++) {
				finalCambios = finalCambios + preCambio[i] + "[";
			}
			finalCambios = finalCambios.Substring(0, finalCambios.Length - 1);

			ISharedPreferencesEditor tmpCargarDatos = misDatos.Edit();
			tmpCargarDatos.PutString("preCambios", finalCambios);
			//tmpCargarDatos.PutString("valorLlave", dValorLlave);
			tmpCargarDatos.Apply();

			Console.WriteLine("FINAL CAMBIOS: " + finalCambios);
		}
		public void CargarDatosDesdePreferencesNoticias() {
			//preNoticiasId = misDatos.GetString("preNoticiasId", "");
			preNoticias = misDatos.GetString("preNoticias", "");
			//dValorLlave = misDatos.GetString("valorLlave", "");
			//Console.WriteLine("preNOTICIAS:" + preNoticias);
		}
		/// ACTUALZIAR INTERFACE
		public void ActualizarInterfaceNoticias() {
			//tvValorllave.Text = "$ " + dValorLlave;
			//List<Linea> 
			mItems = new List<Linea>();

			//Console.WriteLine("ACTUALZIAR INTERFACE:");
			//Console.WriteLine("preNoticias:" + preNoticias);
			//Console.WriteLine("miItems.count vacio:" + mItems.Count.ToString());
			if (preNoticias != "") {
				List<Linea> arrayNoticias = JsonConvert.DeserializeObject<List<Linea>>(preNoticias);
				mItems = arrayNoticias;
				//Console.WriteLine("miItems.count con datos:" + mItems.Count.ToString());
			}

			//adapter = new MyListViewAdapter(inflater.Context, mItems, fntBlack, fntRegular);
			//Android.App.FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
			//Android.App.FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
			//adapter = new MyListViewAdapter(Context, mItems, fntBlack, fntRegular);
			adapter = new MyListViewAdapter(this.Activity, mItems, fntBlack, fntRegular);
			mListView.Adapter = adapter;
			//Console.WriteLine("-------------------");

		}
		/// LEER BD NOTICIAS y ACtualizar PREferencias
		public bool LeerBDNoticiasYActualizarPreferencias() {
			bool retorno = false;

			string uriStr = "https://script.google.com/macros/s/AKfycbwPVattCBeKgzkAXXFzBaWpcCasoYzr769K9cUFXrBkNbwi8A-Y/exec?action=getCeldas";
			try {
				var request = HttpWebRequest.Create(@uriStr);
				request.ContentType = "application /json";
				request.Method = "GET";
				HttpWebResponse response = request.GetResponse() as HttpWebResponse;
				using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
					var content = reader.ReadToEnd();
					var arrayRegistros = JsonValue.Parse(content);

					List<Linea> tmpItemsNoticias = new List<Linea>();
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
							//string tmpFechaFormateada = fe2[2] + "/" + fe2[1] + "/" + fe2[0].Substring(2, 2) + " " + fe1[1].Substring(0, 5);
							string tmpFechaFormateada = campo[0];
							string tmpTitulo = campo[1];
							bool agregar = true;
							if (campo[1].Length > 2) {
								//Console.WriteLine(campo[1].Substring(0, 2));
								if (campo[1].Substring(0, 2) == "P:") {
									string[] tmpTitulos = campo[1].Split(new[] { "P:", "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
									//campo[1] = "Para: ";
									Console.WriteLine("0: " + tmpTitulos[0]);
									Console.WriteLine("1: " + tmpTitulos[1]);
									Console.WriteLine("num: " + num);
									//tmpTitulo = "Para " + tmpTitulos[0];
									//tmpTitulo = "Mensaje personal:";
									tmpTitulo = "Mensaje exclusivo para " + tmpTitulos[0];
									if (num.Trim() == tmpTitulos[1].Trim()) {
										Console.WriteLine("IGUALES, MOSTRAR");
									}
									else {
										agregar = false;
									}
								}
							}
							if (agregar) {
								tmpItemsNoticias.Add(new Linea() { linea1 = tmpTitulo, linea2 = tmpFechaFormateada, linea3 = campo[2] });
							}


						}
						else {

						}
						i++;
					}
					tmpItemsNoticias.Reverse();

					///Cargar las noticias a pref.
					ISharedPreferencesEditor tmpCargarDatos = misDatos.Edit();
					tmpCargarDatos.PutString("preNoticias", JsonConvert.SerializeObject(tmpItemsNoticias));
					tmpCargarDatos.Apply();



					//Assert.NotNull(content);
					retorno = true;
				}
			}
			catch (WebException ex) {
				Console.WriteLine("ERROR AL QUERER LEER BD NOTICIAS: " + ex.ToString());
				retorno = false;
			}
			return retorno;
		}
		///--------------------NOOOOOOOOOOOOOOO----------------------------------------------------------
		/*
		public void CargarDatosDesdePreferencesNOOOOOOOOOOOOO() {
			///CARGAR DATOS EN FILAS///////////////////////////////////////////////////////////
			//var dato1 = campo1.Text;
			//String datosRecibidos = "";
			//String[] palabrasTabuladas = { "" };
			//			var progressDialog = ProgressDialog.Show(inflater.Context, "", "Actualizando...", true);

			/// ver por qué se suman los textos (creo q tengo q borrar los datos isharedpreferences)

			/// luego seguir con el algoritmo:
			/// comprobar si hubo cambio en preNoticiasId
			/// si lo hubo, recien leer los datos de la base de datos y reemplazar por los nuevos
			/// si no hubo, saltear.

			new Thread(new ThreadStart(delegate {
				//string uriStr = "https://docs.google.com/spreadsheets/d/1x---EplnO44tEx1EOvZitjtQzkWWAq9bibV3XYAqREU/pub?gid=408535512&single=true&output=tsv";
				string uriStr = "https://script.google.com/macros/s/AKfycbwPVattCBeKgzkAXXFzBaWpcCasoYzr769K9cUFXrBkNbwi8A-Y/exec?action=getCeldas";
				try {
					var request = HttpWebRequest.Create(string.Format(@uriStr));
					request.ContentType = "application /json";
					request.Method = "GET";
					HttpWebResponse response = request.GetResponse() as HttpWebResponse; //) {
																																							 //if (response.StatusCode != HttpStatusCode.OK) {
																																							 //	Console.Out.WriteLine("ErrorRRRRRRRRRRRR fetching data. Server returned status code: {0}", response.StatusCode);
																																							 //}
					using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
						var content = reader.ReadToEnd();
						if (!string.IsNullOrWhiteSpace(content)) {


							var arrayRegistros = JsonValue.Parse(content);
							//Console.WriteLine(" Cantidad de Registros: " + arrayRegistros.Count);
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
									mItems.Add(new Linea() { linea1 = campo[1], linea2 = tmpFechaFormateada, linea3 = campo[2] });

								}
								i++;
							}
							mItems.Reverse();



							///Cargar las noticias a pref.
							var tmpPreNoticias = JsonConvert.SerializeObject(mItems);
							ISharedPreferencesEditor cargarDatos = misDatos.Edit();
							cargarDatos.PutString("preNoticias", tmpPreNoticias);
							cargarDatos.Apply();

							//Console.WriteLine("PRENOTICIAS:"+preNoticias);
							//List<Linea> arrayNoticias = JsonConvert.DeserializeObject <List<Linea>>(preNoticias);
							//mItems = arrayNoticias;
							//List<string> videogames   = JsonConvert.DeserializeObject<List<string>>(json);
							//foreach (var camp in arrayNoticias) {
							//	Console.WriteLine("lin1: " + camp.linea1);
							//}


							//Console.WriteLine("MITEMS:"+ pref);
							//adapter = new MyListViewAdapter(inflater.Context, mItems, fntBlack, fntRegular);
							adapter = new MyListViewAdapter(Context, mItems, fntBlack, fntRegular);
							Activity.RunOnUiThread(() => mListView.Adapter = adapter);

							////ocultar texto mCargando
							Activity.RunOnUiThread(() => mCargando.SetHeight(0));
							Activity.RunOnUiThread(() => mCargando.Visibility = ViewStates.Invisible);
						}
						//Activity.RunOnUiThread(() => Toast.MakeText(this, content, ToastLength.Long).Show());

						//Assert.NotNull(content);
					}
					//Activity.RunOnUiThread(() => progressDialog.Hide());

				}
				catch (WebException ex) {
					Console.WriteLine("*********************************");
					Console.WriteLine(ex.Message);
					//RunOnUiThread(() => Toast.MakeText(this, "sin conexión", ToastLength.Long).Show());
					//					Activity.RunOnUiThread(() => progressDialog.Hide());
					Activity.RunOnUiThread(() => mCargando.Text = "(Sin conexión)");
					Activity.RunOnUiThread(() => mCargando.Visibility = ViewStates.Visible);
				}

			})).Start();
			//progressDialog.Hide();
		}
		*/
		public void clickEnLista() {
			//lista.Click += (o, s) => {
			Console.WriteLine("CLICK EN LISTA");
			//};
		}
	}

}