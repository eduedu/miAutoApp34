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
using Android.Graphics.Drawables;
using System.Threading;
using System.Net;
using System.Collections.Specialized;
using miAutoApp34.Droid.Fragments;
using Android.Graphics;

namespace miAutoApp34.Droid {
	public class fCuenta : SupportFragment {
		ISharedPreferences misDatos;
		TextView twValorLlave;
		TextView twRegistrados;
		TextView twLlaves;
		TextView twVencimiento;
		ImageView imageCanvas;
		ImageView imageCanvas2;

		TextView text2a;
		TextView text2c;
		TextView twLabelLlaves;
		TextView twAmigosRegistrados;
		Space espacioView;
		LinearLayout contactar;
		LinearLayout btnllaves;
		LinearLayout consulta;
		ImageView miAuto;
		TextView twtitulo;
		TextView tvCconseguiLlaves;
		TextView tvSolicitar;
		TextView tvConsulta;
		string miNumero;
		string dMisLlaves;
		string dMisRegistrados;
		string dValorLlave;
		string dVencimiento;
		string dmiauto_titulo;

		string dmiauto_id; 
		string dmiauto_precio;
		string dmiauto_url1;
		string dmiauto_foto;
		LayoutInflater inflater;

		int mAltoBoton;
		public override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);



			// Create your fragment here
		}
		
		public override View OnCreateView(LayoutInflater _inflater, ViewGroup container, Bundle savedInstanceState) {
			inflater = _inflater;
			View view = inflater.Inflate(Resource.Layout.fCuenta, container, false);

			///Datos
			misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			dmiauto_titulo = misDatos.GetString("miauto_titulo", "");

			dmiauto_id = misDatos.GetString("miauto_id", "");
			dmiauto_precio = misDatos.GetString("miauto_precio", "");
			dmiauto_url1 = misDatos.GetString("miauto_url", "");
			miNumero = misDatos.GetString("num", "");

			//dMisLlaves = misDatos.GetString("misLlaves", "");
			//dMisRefereidos = misDatos.GetString("misReferidos", "");
			//dValorLlave = misDatos.GetString("valorLlave", "");



			///////////////////////////CONTROLES//////////////////
			twValorLlave = view.FindViewById<TextView>(Resource.Id.twValorLlave);
			twValorLlave.Text = "";
			//TextView text1b = view.FindViewById<TextView>(Resource.Id.textView1b);
			text2a = view.FindViewById<TextView>(Resource.Id.textView2a);
			//TextView text2b = view.FindViewById<TextView>(Resource.Id.textView2b);
			text2c = view.FindViewById<TextView>(Resource.Id.textView2c);
			twRegistrados = view.FindViewById<TextView>(Resource.Id.twRegistrados);
			twLlaves = view.FindViewById<TextView>(Resource.Id.twLlaves);
			twVencimiento = view.FindViewById<TextView>(Resource.Id.twVencimiento);
			twLabelLlaves = view.FindViewById<TextView>(Resource.Id.labelllaves);
			twAmigosRegistrados = view.FindViewById<TextView>(Resource.Id.twAmigosRegistrados);
			espacioView = view.FindViewById<Space>(Resource.Id.space8);

			//Button contactar = view.FindViewById<Button>(Resource.Id.contactar);
			contactar = view.FindViewById<LinearLayout>(Resource.Id.contactar);
			//Button btnllaves = view.FindViewById<Button>(Resource.Id.btnllaves);
			btnllaves = view.FindViewById<LinearLayout>(Resource.Id.btnllaves);
			//Button consulta = view.FindViewById<Button>(Resource.Id.consulta);
			consulta = view.FindViewById<LinearLayout>(Resource.Id.consulta);
			miAuto = view.FindViewById<ImageView>(Resource.Id.imAuto);
			twtitulo = view.FindViewById<TextView>(Resource.Id.twtitulo);
			//LinearLayout linear1 = view.FindViewById<LinearLayout>(Resource.Id.linearLayout2);
			imageCanvas = view.FindViewById<ImageView>(Resource.Id.imageCanvas);
			//btnllaves.Typeface = fntRegular;
			tvCconseguiLlaves = view.FindViewById<TextView>(Resource.Id.tvConseguiLlaves);
			tvSolicitar = view.FindViewById<TextView>(Resource.Id.tvSolicitar);
			tvConsulta = view.FindViewById<TextView>(Resource.Id.tvConsulta);

			/*
Drawable drawable = Context.GetDrawable(Resource.Drawable.llaveiconob);
var metrics = Resources.DisplayMetrics;
//var widthInDp = ConvertPixelsToDp(metrics.WidthPixels);
//var heightInDp = ConvertPixelsToDp(metrics.HeightPixels);
int altoBoton = (int)(metrics.HeightPixels * 0.05);
//Console.WriteLine("77777777777777777777777: " + altoBoton);
//drawable.SetBounds(0, 0, (int)(btnllaves.Height * 0.5),(int)(btnllaves.Height * 0.5));
drawable.SetBounds(0, 0, altoBoton, altoBoton);
ScaleDrawable sd = new ScaleDrawable(drawable, 0, 10f, 10f);
//btnllaves.setCompoundDrawables(drawable, null, null, null);
btnllaves.SetCompoundDrawables(drawable, null, null, null);
			//btnllaves.Text = "hola";
			*/

			//FUENTES
			Typeface tf = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-BOLD.TTF");
			Typeface tf2 = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-REGULAR.TTF");
			Typeface tf3 = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-BLACK.TTF");
			Typeface fntCondensedRegular = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTOCONDENSED-REGULAR.TTF");
			Typeface fntCondensedBold = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTOCONDENSED-BOLD.TTF");

			tvCconseguiLlaves.Typeface = fntCondensedBold;
			tvSolicitar.Typeface = fntCondensedBold;
			tvConsulta.Typeface = fntCondensedBold;
			twValorLlave.Typeface = fntCondensedBold;
			//text1b.Typeface = fntCondensedBold;
			twVencimiento.Typeface = fntCondensedBold;

			text2a.Typeface = fntCondensedRegular;
			//text2b.Typeface = fntCondensedRegular;
			text2c.Typeface = fntCondensedRegular;
			twtitulo.Typeface = tf;
			twAmigosRegistrados.Typeface = fntCondensedBold;
			twLabelLlaves.Typeface = fntCondensedBold;
			twRegistrados.Typeface = tf3;
			/*ScaleDrawable sd = new ScaleDrawable(Resource.Drawable.llaveiconob, 0, 10f, 10f);
btnllaves.SetCompoundDrawables()
var bounds = btnllaves.Left
left.SetBounds(bounds.Left, bounds.Top, bounds.Right, bounds.Bottom - 70);
*/

			//cargar datos en UI
			



			/////////////DIBUJAR CIRCULOS ////////////////////////////////////////////////////////////////////////////
			//imageCanvas = view.FindViewById<ImageView>(Resource.Id.imageCanvas);
			//imageCanvas2 = view.FindViewById<ImageView>(Resource.Id.imageCanvas2);

			mostrarImagenAuto();
			ActualizarDatos();
			ActualizarInterface();

			///////////////////BOTON CONTACTAR/////////////////////////////////////////////////////////////////////
			contactar.Click += (o, s) => {
				var progressDialog = ProgressDialog.Show(Context, "", "Registrando Pedido...", true);
				new System.Threading.Thread(new ThreadStart(delegate {
					bool solicitudOK = solicitudesWeb.solicitud("Asesor");
					//string tmpNumeroWA = solicitudesWeb.getVariable("numeroWA");

					Activity.RunOnUiThread(() => {
						progressDialog.Hide();
						Console.WriteLine("Solicitud: " + solicitudOK.ToString());
						if (solicitudOK) {
							Android.App.FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
							//Remove fragment else it will crash as it is already added to backstack
							Android.App.Fragment prev = Activity.FragmentManager.FindFragmentByTag("dialogContactar1");
							if (prev != null) {
								ft.Remove(prev);
							}
							ft.AddToBackStack(null);
							// Create and show the dialog.
							//dialogOKclass newFragment = dialogOKclass.NewInstance(null, "Solicitud registrada", "Un asesor se comunicar� con usted en las pr�ximas horas.");
							dialogOKcontactar newFragmentContactar = dialogOKcontactar.NewInstance(null);
							//Add fragment
							newFragmentContactar.Show(ft, "dialogContactar1");
						}
						else {
							Activity.RunOnUiThread(() => Toast.MakeText(inflater.Context, "sin conexi�n", ToastLength.Long).Show());
						}

					});

				})).Start();

			};

			//BTN LLAVES
			btnllaves.Click += (o, s) => {

				var miIntent = new Intent(container.Context, typeof(refeMain));
				miIntent.AddFlags(ActivityFlags.NoAnimation);
				//miIntent.PutExtra("nya", campo1.Text.Trim());
				//miIntent.PutExtra("u", campo1.Text.Trim());
				//miIntent.PutExtra("p", campo3.Text.Trim());
				StartActivity(miIntent);


				/*
				ISharedPreferencesEditor cargarDatos = misDatos.Edit();
				cargarDatos.PutString("datoConAuto", "1");
				cargarDatos.Apply();
				Activity.StartActivity(typeof(mainFragment));
				Activity.Finish();
				*/


				//Console.WriteLine("ADAPTERRRRR:" +mainFragment.viewPager.Adapter.Count.ToString());

				//mainFragment.set
				/*
				Android.Support.V4.App.FragmentTransaction trans = Activity.SupportFragmentManager.BeginTransaction();
				SupportFragment frag = new refePromo();
				trans.Replace(this.Id, frag);
				//if (AddToBackstack) {
					trans.AddToBackStack(null);
				//}
				trans.Commit();
				*/
				//mainFragment.cambiarViewPager();

				//mainFragment.adapter.cambiar();
				//mainFragment.viewPager.u
				//mainFragment.viewPager.Adapter = null;
			};

			///Resetear valores del auto
			miAuto.LongClick += (s, o) => {

				Toast.MakeText(inflater.Context, "resetear Valores", ToastLength.Long).Show();

				ISharedPreferencesEditor cargarDatos = misDatos.Edit();
				cargarDatos.PutString("datoConAuto", "");
				cargarDatos.PutString("tempTab2", "1");
				cargarDatos.PutString("miauto_id", "");
				cargarDatos.PutString("cacheAutos", "");
				
				cargarDatos.Apply();
				memoriaInterna.GuardarCambioPreferencesX(2, "0");

				Activity.StartActivity(typeof(mainFragment));
				Activity.Finish();

				//miAuto.LayoutParameters.Height = 100;

			};

			//BOTON CONSULTA
			consulta.Click += (o, s) => {
				Android.App.FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
				//Remove fragment else it will crash as it is already added to backstack
				Android.App.Fragment prev = Activity.FragmentManager.FindFragmentByTag("dialogConsulta");
				if (prev != null) {
					ft.Remove(prev);
				}
				ft.AddToBackStack(null);
				// Create and show the dialog.
				//dialogOKclass newFragment = dialogOKclass.NewInstance(null, "Solicitud registrada", "Un asesor se comunicar� con usted en las pr�ximas horas.");
				dialogConsulta newFragmentContactar = dialogConsulta.NewInstance(null, "Consulta", "Mensaje para MiAutoPlan:");
				//Add fragment
				newFragmentContactar.Show(ft, "dialogConsulta");
			};

			return view;
			// return base.OnCreateView(inflater, container, savedInstanceState);
		}
		public override void OnResume() {

			base.OnResume();
			new Thread(new ThreadStart(delegate {
				bool todoOK = LeerDatosDesdeWeb();
				if (todoOK) {
					bool todoOKAutos = LeerAutosDesdeWeb();
					if (todoOKAutos) {
						ActualizarDatos();
						ActualizarInterface();
						Activity.RunOnUiThread(() => {
							mostrarImagenAuto();
						});

					}
				}
			})).Start();

		}
		public void mostrarImagenAuto() {
			///ajustar tama�o (alto) de imAuto
			var metrics = inflater.Context.Resources.DisplayMetrics;
			//var widthInDp = ConvertPixelsToDp(metrics.WidthPixels);
			//var heightInDp = ConvertPixelsToDp(metrics.HeightPixels);
			Console.WriteLine("Resolucion:" + metrics.WidthPixels + "x" + metrics.HeightPixels);
			//Console.WriteLine("AnchoDP:" + widthInDp);
			//Console.WriteLine("Ancho Pantalla en PX:" + metrics.WidthPixels);
			//int anchoEnPx = (metrics.WidthPixels * porcentajeAnchoPantalla) / 100;
			//miAuto.SetMaxHeight( (metrics.HeightPixels/4));
			//Console.WriteLine("btnllaves.LayoutParameters.Height:" + btnllaves.LayoutParameters.Height);
			//btnllaves.LayoutParameters.Height = (13 * metrics.HeightPixels / 1280);
			//Console.WriteLine("btnllaves.LayoutParameters.Height:" + btnllaves.LayoutParameters.Height);
			mAltoBoton = btnllaves.LayoutParameters.Height;
			int sumaAltoViews = mAltoBoton * 2;
			//Console.WriteLine("mAltoboton" + mAltoBoton);
			if (metrics.HeightPixels < 500) {
				double multiplo = 0.8;
				mAltoBoton = (int)(mAltoBoton * multiplo);
				btnllaves.LayoutParameters.Height = mAltoBoton;
				contactar.LayoutParameters.Height = mAltoBoton;
				consulta.LayoutParameters.Height = mAltoBoton;
				//btnllaves.SetTextSize(ComplexUnitType.Px, (float)(mAltoBoton * 0.5));
				//contactar.SetTextSize(ComplexUnitType.Px, (float)(mAltoBoton * 0.5));
				tvSolicitar.SetTextSize(ComplexUnitType.Px, (float)(mAltoBoton * 0.55));
				tvCconseguiLlaves.SetTextSize(ComplexUnitType.Px, (float)(mAltoBoton * 0.55));
				tvConsulta.SetTextSize(ComplexUnitType.Px, (float)(mAltoBoton * 0.55));
				twValorLlave.SetTextSize(ComplexUnitType.Px, (float)(twValorLlave.TextSize * multiplo));
				twVencimiento.SetTextSize(ComplexUnitType.Px, (float)(twVencimiento.TextSize * multiplo));
				text2a.SetTextSize(ComplexUnitType.Px, (float)(text2a.TextSize * multiplo));
				text2c.SetTextSize(ComplexUnitType.Px, (float)(text2c.TextSize * multiplo));
				twLabelLlaves.SetTextSize(ComplexUnitType.Px, (float)(twLabelLlaves.TextSize * multiplo));
				twAmigosRegistrados.SetTextSize(ComplexUnitType.Px, (float)(twAmigosRegistrados.TextSize * multiplo));
				//twRegistrados.SetTextSize(ComplexUnitType.Px, (float)(twRegistrados.TextSize * multiplo));

				//twtitulo.Typeface = tf;


				//Console.WriteLine("cambio altoboton");
				//Console.WriteLine("mAltoboton" + mAltoBoton);
			}

			//btnllaves.SetTextSize( ComplexUnitType.Px,(float) (mAltoBoton * 0.45));
			//int tmpAlto = metrics.HeightPixels / 6;
			string dLinearTitulo = misDatos.GetString("LinearTitulo", "0");
			int tmpAltoTitulo = Int32.Parse(dLinearTitulo) * 3;

			//int sumaAltoViews = (text2a.LayoutParameters.Height + btnllaves.LayoutParameters.Height +
			//espacioView.LayoutParameters.Height) * 2;
			//mAltoBoton= btnllaves.LayoutParameters.Height;
			sumaAltoViews = sumaAltoViews + (int)(mAltoBoton * 3.5);
			int tmpAlto = (metrics.HeightPixels - (tmpAltoTitulo + sumaAltoViews));

			//Console.WriteLine("ALTO:" + metrics.HeightPixels);
			//Console.WriteLine("linear lo parameters:" + linear1.LayoutParameters.Height);
			//Console.WriteLine("text2a.LayoutParameters.Height:" + text2a.LayoutParameters.Height);
			//Console.WriteLine("btnllaves.LayoutParameters.Height:" + btnllaves.LayoutParameters.Height);
			//Console.WriteLine("espacioView.LayoutParameters.Height:" + espacioView.LayoutParameters.Height);
			Console.WriteLine("alto titulo:" + tmpAltoTitulo);
			Console.WriteLine("sumaAltoViews" + sumaAltoViews);
			Console.WriteLine("HEIGHT" + metrics.HeightPixels);
			miAuto.LayoutParameters.Height = tmpAlto;
			Console.WriteLine("tmpAlto:" + tmpAlto);
			//Console.WriteLine("canvas height:" + imageCanvas.LayoutParameters.Height);
			//Console.WriteLine("ALTO-linearLO:" + (metrics.HeightPixels-linear1.Height).ToString());

			///Cargar imagen miAuto
			//Bitmap imageBitmap = memoriaInterna.cargarImagenDesdeCache(dfotoMiAuto);
			dmiauto_foto = misDatos.GetString("miauto_foto", "");

			if (dmiauto_foto != "") {
				Console.WriteLine("FOTO MI AUTO" + dmiauto_foto);
				//var imageBytes = Convert.FromBase64String(dmiauto_foto);
				//Bitmap imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
				Bitmap imageBitmap = memoriaInterna.LeerImagen(dmiauto_foto, 75);
				//Bitmap.Config config = imageBitmap.GetConfig();
				//imageBitmap.Reconfigure(100, 100, config);

				//imageBitmap = memoriaInterna.ResizeBitmap(imageBitmap, tmpAlto, tmpAlto);
				miAuto.SetImageBitmap(imageBitmap);

				miAuto.SetScaleType(ImageView.ScaleType.FitCenter);
				//imageBitmap.Recycle();

				//imageBitmap = null;
				//dmiauto_foto = "";
			} else {
				miAuto.SetImageBitmap(null);

			
			}
		}
		public Bitmap circulo(string valor, int medidas, string colorArco = "amarillo") {
			//Bitmap retorno=null;

			Bitmap bitmap = Bitmap.CreateBitmap(medidas, medidas, Bitmap.Config.Argb8888);
			//Bitmap bitmap2 = Bitmap.CreateBitmap(120, 120, Bitmap.Config.Argb8888);
			Canvas canvas = new Canvas(bitmap);
			//Canvas canvas2 = new Canvas(bitmap2);

			//canvas.DrawColor(Color.BlueViolet);
			Paint p = new Paint {
				AntiAlias = true,
				//Color = Color.Red,
				//StrokeWidth = 10,
				StrokeWidth = (medidas / 12),
			};
			p.SetStyle(Paint.Style.Stroke);
			// smooths
			/*
			p.setAntiAlias(true);
			p.setColor(Color.RED);
			p.setStyle(Paint.Style.STROKE);
			p.setStrokeWidth(5);
			*/
			// opacity
			//p.setAlpha(0x80); //
			//RectF rectF = new RectF(10, 10, 110, 100);
			//RectF rectF = new RectF(5, 5, medidas-5, medidas - 5);
			RectF rectF = new RectF((int)(medidas / 24), (int)(medidas / 24), medidas - ((int)(medidas / 24)), medidas - ((int)(medidas / 24)));


			//p.SetARGB(255, 152, 152, 152);
			p.SetARGB(255, 222, 221, 221);

			canvas.DrawOval(rectF, p);
			//canvas2.DrawOval(rectF, p);

			if (colorArco == "amarillo") {
				p.SetARGB(255, 250, 164, 27);  //amarillo
			}
			else {
				p.SetARGB(255, 62, 97, 163);  //azul
			}

			//p.SetColor(Color.BLACK);
			int tmpCantDeLlaves;
			//string inputString = "abc";
			bool parsed = Int32.TryParse(valor, out tmpCantDeLlaves);

			if (!parsed) {
				tmpCantDeLlaves = 0;
			}
			int tmpTotalLlaves = 72;
			int tmpTotalArco = 360;
			int tmpArco = tmpCantDeLlaves * tmpTotalArco / tmpTotalLlaves;
			canvas.DrawArc(rectF, 180, tmpArco, false, p);
			//imageCanvas.SetImageBitmap(bitmap);

			//p.SetARGB(255, 62, 97, 163);  //azul
			//canvas2.DrawArc(rectF, 180, tmpArco, false, p);
			//imageCanvas2.SetImageBitmap(bitmap2);

			return bitmap;
		}
		public void ActualizarDatos() {
			misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			dMisLlaves = misDatos.GetString("misLlaves", "");
			dMisRegistrados = misDatos.GetString("misRegistrados", "");
			dValorLlave = misDatos.GetString("valorLlave", "");
			dVencimiento = misDatos.GetString("vencimiento", "");

			//datos auto
			dmiauto_titulo = misDatos.GetString("miauto_titulo", "");
			//dmiauto_id = misDatos.GetString("miauto_id", "");
			dmiauto_precio = misDatos.GetString("miauto_precio", "");
			dmiauto_url1 = misDatos.GetString("miauto_url", "");
		}
		public void ActualizarInterface() {
			Bitmap arcoAmarillo = null;
			arcoAmarillo = circulo(dMisLlaves, mAltoBoton * 4);

			try {
				Activity.RunOnUiThread(() => {
					imageCanvas.SetImageBitmap(arcoAmarillo);
					imageCanvas.LayoutParameters.Height = mAltoBoton * 3;
					twValorLlave.Text = "$ " + dValorLlave;

					string tmpMisRegistrados = "00" + dMisRegistrados;
					twRegistrados.Text = tmpMisRegistrados.Substring(tmpMisRegistrados.Length - 2, 2);
					string tmpDmisLlaves = "00" + dMisLlaves.Trim();
					twLlaves.Text = tmpDmisLlaves.Substring(tmpDmisLlaves.Length - 2, 2);
					twVencimiento.Text = dVencimiento;
					//Console.WriteLine("Alto:"+)

					//Auto
					twtitulo.Text = dmiauto_titulo;
				});

			}
			catch (Exception ex) {

			}
		}
		public bool LeerDatosDesdeWeb() {
			bool retorno = false;
			//Console.WriteLine("------OnResume");
			//string tmpNumero = miNumero;
			//new Thread(new ThreadStart(delegate {
			//bool SinConexion = false;
			//LEO VARIABLE "CAMBIO"
			//string tmpValorLlave = solicitudesWeb.getVariable("valorLlave");

			//if (Activity.IsFinishing != true) {
			string tmpDatosUsuario = solicitudesWeb.getDatosUsuario(miNumero);
			Console.Out.WriteLine("-----DATOS USUARIO: " + tmpDatosUsuario.ToString());
			string[] datos = tmpDatosUsuario.Split('[');
			if (datos.Count() > 1) {
				//try {
				/*
				Console.WriteLine("Datos.count:" + datos.Count());
				Console.WriteLine("Datos[0]:" + datos[0]);
				Console.WriteLine("Datos[1]:" + datos[1]);
				Console.WriteLine("Datos[2]:" + datos[2]);
				Console.WriteLine("Datos[3]:" + datos[3]);
				Console.WriteLine("Datos[4]:" + datos[4]);
				*/
				ISharedPreferencesEditor cargarDatos = misDatos.Edit();
				cargarDatos.PutString("valorLlave", datos[0]);
				cargarDatos.PutString("misLlaves", datos[1]);
				cargarDatos.PutString("misRegistrados", datos[3]);
				cargarDatos.PutString("vencimiento", datos[4]);
				//ACA : datos[5] es el ID del auto. Buscar ese ID en la BD autos y actualizar preferences
				cargarDatos.PutString("miauto_id", datos[5]);
				cargarDatos.Apply();
				retorno = true;
			}
			else {
				//catch (Exception ex) {
				//Console.WriteLine("ERROR EDU:" + ex.Message);
				Console.WriteLine("ERROR GUARDANDO DATOS LEIDOS");
				retorno = false;
			}
			//}
			//if (tmpValorLlave == "SinConexion") {
			//	SinConexion = true;
			//}


			//})).Start();
			return retorno;
		}
		public bool LeerAutosDesdeWeb() {
			bool retorno = false;
			string tmpDatosAuto= solicitudesWeb.getDatosAuto(dmiauto_id);
			Console.Out.WriteLine("-----DATOS AUTO: " + tmpDatosAuto.ToString());
			string[] datos = tmpDatosAuto.Split('[');
			if (datos.Count() > 1) {
				//try {
				/*
				Console.WriteLine("Datos.count:" + datos.Count());
				Console.WriteLine("Datos[0]:" + datos[0]);
				Console.WriteLine("Datos[1]:" + datos[1]);
				Console.WriteLine("Datos[2]:" + datos[2]);
				Console.WriteLine("Datos[3]:" + datos[3]);
				//Console.WriteLine("Datos[4]:" + datos[4]);
				*/
				

				////
				string tmpMiAutoUrl1 = datos[3];

				string urlAlfa = memoriaInterna.convertirEnAlfaNumerico(tmpMiAutoUrl1);

				Context context = Application.Context;
				string[] archivos = context.FileList();
				if (archivos != null) {
					bool existeArchivo = false;
					foreach (string archivo in archivos) {
						if (archivo == urlAlfa) {
							existeArchivo = true;
							break;
						}
					}
					if (!existeArchivo) {
						var imageBitmap = solicitudesWeb.GetImageBitmapFromUrl(tmpMiAutoUrl1);
						if (imageBitmap != null) {
							//var tmpPromoPicString = Convert.ToBase64String(arrayImagenWeb);
							memoriaInterna.GuardarImagen(imageBitmap, urlAlfa);
							imageBitmap.Recycle();
							//}
							//else {
							//DEVUELVE FALSE SI NO HAY CONEXION A INTERNET
							//retorno = false;
						}
					}
				}
				////
				//GUARDAR DATOS DE AUTO
				ISharedPreferencesEditor cargarDatos = misDatos.Edit();
				cargarDatos.PutString("miauto_titulo", datos[0]);
				cargarDatos.PutString("miauto_precio", datos[1]);
				cargarDatos.PutString("miauto_descripcion", datos[2]);
				cargarDatos.PutString("miauto_url", datos[3]);
				cargarDatos.PutString("miauto_foto", urlAlfa);
				cargarDatos.Apply();

				retorno = true;
			}
			else {
				//catch (Exception ex) {
				//Console.WriteLine("ERROR EDU:" + ex.Message);
				Console.WriteLine("ERROR GUARDANDO DATOS LEIDOS");
				retorno = false;
			}
			//}
			//if (tmpValorLlave == "SinConexion") {
			//	SinConexion = true;
			//}


			//})).Start();
			return retorno;
		}
	}

}