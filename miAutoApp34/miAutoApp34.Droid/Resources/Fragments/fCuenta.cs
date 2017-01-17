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
		//ImageView imageCanvas2;

		TextView text2a;
		TextView text2c;
		TextView twLabelLlaves;
		TextView twAmigosRegistrados;
		Space espacioView;
		LinearLayout contactar;
		LinearLayout btnllaves;
		LinearLayout pedirMiAuto;
		ImageView miAuto;
		ImageView lupa;
		TextView twtitulo;
		TextView tvCconseguiLlaves;
		TextView tvSolicitar;
		TextView tvPedirMiAuto;
		TextView twTotalLlavesAuto;
		TextView twTotalReferidos;
		LinearLayout linearRegistrados;
		string miNumero;
		string dMisLlaves;
		string dIntransferibles;
		string dTotalLlaves;
		string dBloqueado;
		string dMisRegistrados;
		string dValorLlave;
		string dVencimiento;
		string dTotalReferidos;
		string dmiauto_titulo;
		string dmiauto_id;
		string dmiauto_precio;
		string dmiauto_url1;
		string dmiauto_foto;
		string dmiauto_llaves;
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

			miNumero = misDatos.GetString("num", "");
			dmiauto_id = misDatos.GetString("miauto_id", "");
			ActualizarDatos();
			/*
			dmiauto_titulo = misDatos.GetString("miauto_titulo", "");
			dmiauto_precio = misDatos.GetString("miauto_precio", "");
			dmiauto_url1 = misDatos.GetString("miauto_url", "");
			*/
			//dMisLlaves = misDatos.GetString("misLlaves", "");
			//dMisRefereidos = misDatos.GetString("misReferidos", "");
			//dValorLlave = misDatos.GetString("valorLlave", "");



			///CONTROLES//////////////////
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
			pedirMiAuto = view.FindViewById<LinearLayout>(Resource.Id.pedirMiAuto);
			miAuto = view.FindViewById<ImageView>(Resource.Id.imAuto);
			lupa = view.FindViewById<ImageView>(Resource.Id.imlupa);
			twtitulo = view.FindViewById<TextView>(Resource.Id.twtitulo);
			//LinearLayout linear1 = view.FindViewById<LinearLayout>(Resource.Id.linearLayout2);
			imageCanvas = view.FindViewById<ImageView>(Resource.Id.imageCanvas);
			//imageCanvas2 = view.FindViewById<ImageView>(Resource.Id.imageCanvas2);
			//btnllaves.Typeface = fntRegular;
			tvCconseguiLlaves = view.FindViewById<TextView>(Resource.Id.tvConseguiLlaves);
			tvSolicitar = view.FindViewById<TextView>(Resource.Id.tvSolicitar);
			tvPedirMiAuto = view.FindViewById<TextView>(Resource.Id.tvPedirMiAuto);
			twTotalLlavesAuto = view.FindViewById<TextView>(Resource.Id.twTotalLlaves);
			linearRegistrados = view.FindViewById<LinearLayout>(Resource.Id.linearRegistrados);
			twTotalReferidos = view.FindViewById<TextView>(Resource.Id.twTotalReferidos);
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
			tvPedirMiAuto.Typeface = fntCondensedBold;
			twValorLlave.Typeface = fntCondensedBold;
			//text1b.Typeface = fntCondensedBold;
			twVencimiento.Typeface = fntCondensedBold;

			text2a.Typeface = fntCondensedRegular;
			//text2b.Typeface = fntCondensedRegular;
			text2c.Typeface = fntCondensedRegular;
			twtitulo.Typeface = tf;
			twAmigosRegistrados.Typeface = fntCondensedBold;
			twTotalReferidos.Typeface = fntCondensedRegular;
			twLabelLlaves.Typeface = fntCondensedBold;
			twRegistrados.Typeface = tf3;
			twTotalLlavesAuto.Typeface = fntCondensedRegular;
			twLlaves.Typeface = fntCondensedBold;
			/*ScaleDrawable sd = new ScaleDrawable(Resource.Drawable.llaveiconob, 0, 10f, 10f);
btnllaves.SetCompoundDrawables()
var bounds = btnllaves.Left
left.SetBounds(bounds.Left, bounds.Top, bounds.Right, bounds.Bottom - 70);
*/

			//cargar datos en UI




			/////////////DIBUJAR CIRCULOS ////////////////////////////////////////////////////////////////////////////
			//imageCanvas = view.FindViewById<ImageView>(Resource.Id.imageCanvas);
			//imageCanvas2 = view.FindViewById<ImageView>(Resource.Id.imageCanvas2);

			bool HayImagenes = mostrarImagenAuto();
			ActualizarDatos();
			ActualizarInterface();
			if (!HayImagenes) {
				LeerAutosDesdeWeb();
				bool recargar = mostrarImagenAuto();
			}

			///BOTON CONTACTAR/////////////////////////////////////////////////////////////////////
			contactar.Click += (o, s) => {
				var progressDialog = ProgressDialog.Show(Context, "", "Registrando Pedido...", true);
				new System.Threading.Thread(new ThreadStart(delegate {
					bool solicitudOK = solicitudesWeb.solicitud("Asesor");
					//string tmpNumeroWA = solicitudesWeb.getVariable("numeroWA");

					Activity.RunOnUiThread(() => {
						progressDialog.Hide();
						//Console.WriteLine("Solicitud: " + solicitudOK.ToString());
						if (solicitudOK) {
							Android.App.FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
							//Remove fragment else it will crash as it is already added to backstack
							Android.App.Fragment prev = Activity.FragmentManager.FindFragmentByTag("dialogContactar1");
							if (prev != null) {
								ft.Remove(prev);
							}
							ft.AddToBackStack(null);
							// Create and show the dialog.
							//dialogOKclass newFragment = dialogOKclass.NewInstance(null, "Solicitud registrada", "Un asesor se comunicará con usted en las próximas horas.");
							dialogOKcontactar newFragmentContactar = dialogOKcontactar.NewInstance(null);
							//Add fragment
							newFragmentContactar.Show(ft, "dialogContactar1");
						}
						else {
							Activity.RunOnUiThread(() => Toast.MakeText(inflater.Context, "sin conexión", ToastLength.Long).Show());
						}

					});

				})).Start();

			};
			///CLICK EN REGISTRADOSSSSS
			linearRegistrados.Click += (o, s) => {
				var progressDialog = ProgressDialog.Show(Context, "", "Consultando base de datos...", true);
				new System.Threading.Thread(new ThreadStart(delegate {
					//bool solicitudOK = solicitudesWeb.solicitud("Asesor");
					//string tmpNumeroWA = solicitudesWeb.getVariable("numeroWA");
					string tmpGetReferidos = solicitudesWeb.getReferidos(miNumero);
					string[] referidosArray = tmpGetReferidos.Split('[');
					string referidosFinal = "";
					int j = 1;
					foreach (string tmpRefeItem in referidosArray) {
						if (tmpRefeItem != "") {
							referidosFinal = referidosFinal + j.ToString() + ") " + tmpRefeItem + "\n";
							j++;
						}
					}

					Activity.RunOnUiThread(() => {
						progressDialog.Hide();
						Console.WriteLine("Referidos: " + tmpGetReferidos);
						if (tmpGetReferidos != "-1") {
							FragmentTransaction ft2 = Activity.FragmentManager.BeginTransaction();
							//Remove fragment else it will crash as it is already added to backstack
							Fragment prev = Activity.FragmentManager.FindFragmentByTag("dialogConsultar");
							if (prev != null) {
								ft2.Remove(prev);
							}
							ft2.AddToBackStack(null);
							// Create and show the dialog.
							dialogOKrefefidos newFragment = dialogOKrefefidos.NewInstance(null, "Mis Referidos", referidosFinal);
							//Add fragment
							newFragment.Show(ft2, "dialogConsultar");
						}
						else {
							Activity.RunOnUiThread(() => Toast.MakeText(inflater.Context, "sin conexión", ToastLength.Long).Show());
						}

					});
				})).Start();

			};

			///BTN LLAVES
			btnllaves.Click += (o, s) => {
				memoriaInterna.mostrarAyudaReferidos();
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

			///VER DETALLES DEL AUTO
			//miAuto.Click += (s, o) => {
			lupa.Click += (s, o) => {
				/*
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
				*/
				var miIntent = new Intent(Context, typeof(fDetallesAuto));
				miIntent.AddFlags(ActivityFlags.NoAnimation);
				misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
				//miNumero = misDatos.GetString("num", "");

				string tmpId = misDatos.GetString("miauto_id", "");
				string tmpTitulo = misDatos.GetString("miauto_titulo", "");
				string tmpPrecio = misDatos.GetString("miauto_precio", "");
				string tmpDescripcion = misDatos.GetString("miauto_descripcion", "");
				string tmpUrl = misDatos.GetString("miauto_url", "");
				string tmpUrl2 = misDatos.GetString("miauto_url2", "");
				string tmpUrl3 = misDatos.GetString("miauto_url3", "");
				string tmpUrl4 = misDatos.GetString("miauto_url4", "");
				string tmpllavesAuto = misDatos.GetString("miauto_llaves", "");

				miIntent.PutExtra("modo", "desdefCuenta");
				miIntent.PutExtra("detalle.url1", tmpUrl);
				miIntent.PutExtra("detalle.url2", tmpUrl2);
				miIntent.PutExtra("detalle.url3", tmpUrl3);
				miIntent.PutExtra("detalle.url4", tmpUrl4);
				miIntent.PutExtra("detalle.titulo", tmpTitulo);
				miIntent.PutExtra("detalle.descripcion", tmpDescripcion);
				miIntent.PutExtra("detalle.precio", tmpPrecio);
				miIntent.PutExtra("detalle.id", tmpId);
				miIntent.PutExtra("detalle.llaves", tmpllavesAuto);
				//Console.WriteLine("auto:" + mAutos[args.Position].nombre);
				//Console.WriteLine("id:" + mAutos[args.Position].id);

				StartActivity(miIntent);
			};

			///BOTON PEDIR MI AUTO
			pedirMiAuto.Click += (o, s) => {
				int tmpLlaves = 0;
				try {
					tmpLlaves = Int32.Parse(dTotalLlaves);
				}
				catch (Exception ex) {
					Console.WriteLine("Error con el formato de dTotalLlaves:" + ex.Message);
				}

				if (tmpLlaves < 1) {
					///si no tiene llaves, no puede pedir el auto
					Android.App.FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
					//Remove fragment else it will crash as it is already added to backstack
					Android.App.Fragment prev = Activity.FragmentManager.FindFragmentByTag("sinLlaves");
					if (prev != null) {
						ft.Remove(prev);
					}
					ft.AddToBackStack(null);
					// Create and show the dialog.
					//string tmpMensaje="Llaves Transferibles: "+dMisLlaves+"\nLlaves Intransferibles: "+dIntransferibles+"\n\nTotal: "+dTotalLlaves;
					dialogOKclass newFragmentMisLlaves = dialogOKclass.NewInstance(null, "Sin llaves", "Su cuenta no registra ninguna Llave (cuota) hasta el momento. \nNecesita tener al menos 1 (una) Llave para poder realizar esta operación.");
					//dialogConsulta newFragmentContactar = dialogConsulta.NewInstance(null, "Consulta", "Mensaje para MiAutoPlan:");
					//Add fragment
					newFragmentMisLlaves.Show(ft, "sinLlaves");
				}
				else {
					var progressDialog = ProgressDialog.Show(inflater.Context, "", "Cargando...", true);
					new System.Threading.Thread(new ThreadStart(delegate {
						string requeremientos = solicitudesWeb.getVariable("Requerimientos");
						this.Activity.RunOnUiThread(() => {
							progressDialog.Hide();
							if (requeremientos != "SinConexion") {
								//Dismiss();
								Android.App.FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
								Android.App.Fragment prev = Activity.FragmentManager.FindFragmentByTag("dialogConsulta");
								if (prev != null) {
									ft.Remove(prev);
								}
								ft.AddToBackStack(null);
								dialogPedirAuto newFragmentContactar = dialogPedirAuto.NewInstance(null, requeremientos);
								newFragmentContactar.Show(ft, "dialogConsulta");
							}
							else {
								Activity.RunOnUiThread(() => {
									//Dismiss();
									Toast.MakeText(inflater.Context, "sin conexión", ToastLength.Long).Show();
								});
							}
						});
					})).Start();
				}
			};

			///CLICK EN RUEDA "LLAVES"
			imageCanvas.Click += (o, s) => {
				Android.App.FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
				//Remove fragment else it will crash as it is already added to backstack
				Android.App.Fragment prev = Activity.FragmentManager.FindFragmentByTag("dialogMisLlaves");
				if (prev != null) {
					ft.Remove(prev);
				}
				ft.AddToBackStack(null);
				// Create and show the dialog.
				//string tmpMensaje="Llaves Transferibles: "+dMisLlaves+"\nLlaves Intransferibles: "+dIntransferibles+"\n\nTotal: "+dTotalLlaves;
				dialogOKLlaves newFragmentMisLlaves = dialogOKLlaves.NewInstance(null, dMisLlaves, dIntransferibles, dTotalLlaves);
				//dialogConsulta newFragmentContactar = dialogConsulta.NewInstance(null, "Consulta", "Mensaje para MiAutoPlan:");
				//Add fragment
				newFragmentMisLlaves.Show(ft, "dialogMisLlaves");
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
						/*
						try {
							Activity.RunOnUiThread(() => {
								mostrarImagenAuto();
							});
						}
						catch (Exception ex) {
							Console.WriteLine("Error al intentar actualizar interface?:" + ex.Data);
						}
						*/
					}
				}
			})).Start();

		}
		/*
		public override void OnPause() {
			base.OnPause();
			miAuto.SetImageBitmap(null);

		}
		*/
		public bool mostrarImagenAuto() {
			bool retorno = true;
			///ajustar tamaño (alto) de imAuto
			var metrics = inflater.Context.Resources.DisplayMetrics;
			//var widthInDp = ConvertPixelsToDp(metrics.WidthPixels);
			//var heightInDp = ConvertPixelsToDp(metrics.HeightPixels);
			//Console.WriteLine("Resolucion:" + metrics.WidthPixels + "x" + metrics.HeightPixels);
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
				pedirMiAuto.LayoutParameters.Height = mAltoBoton;
				//btnllaves.SetTextSize(ComplexUnitType.Px, (float)(mAltoBoton * 0.5));
				//contactar.SetTextSize(ComplexUnitType.Px, (float)(mAltoBoton * 0.5));
				tvSolicitar.SetTextSize(ComplexUnitType.Px, (float)(mAltoBoton * 0.55));
				tvCconseguiLlaves.SetTextSize(ComplexUnitType.Px, (float)(mAltoBoton * 0.55));
				tvPedirMiAuto.SetTextSize(ComplexUnitType.Px, (float)(mAltoBoton * 0.55));
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
			/*
			Console.WriteLine("alto titulo:" + tmpAltoTitulo);
			Console.WriteLine("sumaAltoViews" + sumaAltoViews);
			Console.WriteLine("HEIGHT" + metrics.HeightPixels);
			*/
			miAuto.LayoutParameters.Height = tmpAlto;
			//Console.WriteLine("tmpAlto:" + tmpAlto);
			//Console.WriteLine("canvas height:" + imageCanvas.LayoutParameters.Height);
			//Console.WriteLine("ALTO-linearLO:" + (metrics.HeightPixels-linear1.Height).ToString());

			///Cargar imagen miAuto
			//Bitmap imageBitmap = memoriaInterna.cargarImagenDesdeCache(dfotoMiAuto);
			dmiauto_foto = misDatos.GetString("miauto_foto", "");
			//memoriaInterna.convertirEnAlfaNumerico(datos[3])
			if (dmiauto_foto != "") {
				//Console.WriteLine("FOTO MI AUTO" + dmiauto_foto);
				//var imageBytes = Convert.FromBase64String(dmiauto_foto);
				//Bitmap imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
				Bitmap imageBitmap = memoriaInterna.LeerImagen(dmiauto_foto, 60);
				//Bitmap.Config config = imageBitmap.GetConfig();
				//imageBitmap.Reconfigure(100, 100, config);

				//imageBitmap = memoriaInterna.ResizeBitmap(imageBitmap, tmpAlto, tmpAlto);
				miAuto.SetScaleType(ImageView.ScaleType.FitCenter);
				miAuto.SetImageBitmap(imageBitmap);


				//imageBitmap.Recycle();

				//imageBitmap = null;
				//dmiauto_foto = "";
			}
			else {
				miAuto.SetImageBitmap(null);
				retorno = false;


			}
			return retorno;
		}
		public Bitmap circulo(string valor, string totalLlaves, int medidas, string colorArco = "amarillo") {
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
			Console.WriteLine("tmpCantDeLlaves:" + tmpCantDeLlaves);
			if (!parsed) {

				tmpCantDeLlaves = 0;
			}
			int tmpTotalLlaves;
			Console.WriteLine("totalLlaves:" + totalLlaves);
			bool parsed2 = Int32.TryParse(totalLlaves, out tmpTotalLlaves);
			if (!parsed2) {
				tmpTotalLlaves = 0;
			}
			//Console.WriteLine("tmpCantDeLlaves:" + tmpCantDeLlaves);
			Console.WriteLine("tmpTotalLlaves:" + tmpTotalLlaves);
			//int tmpTotalLlaves = 72;
			//int tmpTotalLlaves = totalLlaves;
			int tmpTotalArco = 360;
			if (tmpTotalLlaves != 0) {
				int tmpArco = tmpCantDeLlaves * tmpTotalArco / tmpTotalLlaves;
				canvas.DrawArc(rectF, 180, tmpArco, false, p);
			}
			//imageCanvas.SetImageBitmap(bitmap);

			//p.SetARGB(255, 62, 97, 163);  //azul
			//canvas2.DrawArc(rectF, 180, tmpArco, false, p);
			//imageCanvas2.SetImageBitmap(bitmap2);

			return bitmap;
		}
		public void ActualizarDatos() {
			misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			dMisLlaves = misDatos.GetString("misLlaves", "0");
			dIntransferibles = misDatos.GetString("intransferibles", "0");
			dBloqueado = misDatos.GetString("bloqueado", "");
			dMisRegistrados = misDatos.GetString("misRegistrados", "");
			dTotalReferidos = misDatos.GetString("totalReferidos", "");
			if (dTotalReferidos.Trim() == "") {
				dTotalReferidos = "00";
			}
			dValorLlave = misDatos.GetString("valorLlave", "");
			dVencimiento = misDatos.GetString("vencimiento", "");
			try {
				dTotalLlaves = (Int32.Parse(dMisLlaves) + Int32.Parse(dIntransferibles)).ToString().Trim();
			}
			catch (Exception ex) {
				Console.WriteLine("ERRRRRRRRRRRRRRRRRRRRRRRRRRRROORRRRRRRRRRRRRRRRRRRRRRRRR:" + ex.Data);
			}

			//datos auto
			dmiauto_titulo = misDatos.GetString("miauto_titulo", "");
			//dmiauto_id = misDatos.GetString("miauto_id", "");
			dmiauto_precio = misDatos.GetString("miauto_precio", "");
			dmiauto_url1 = misDatos.GetString("miauto_url", "");
			dmiauto_llaves = misDatos.GetString("miauto_llaves", "");
			if (dmiauto_llaves == "") {
				dmiauto_llaves = "0";
			}
		}
		public void ActualizarInterface() {
			Bitmap arcoAmarillo = null;
			arcoAmarillo = circulo(dTotalLlaves, dmiauto_llaves, mAltoBoton * 4);

			try {
				Activity.RunOnUiThread(() => {
					int tmpMisLlaves = 0;
					if (Int32.TryParse(dMisLlaves, out tmpMisLlaves)) {
						if (tmpMisLlaves < 1) {
							tvPedirMiAuto.Text = "COMPRAR";
						}
						else {
							tvPedirMiAuto.Text = "Pedir MiAuto";
						}
					}

					imageCanvas.SetImageBitmap(arcoAmarillo);
					imageCanvas.LayoutParameters.Height = mAltoBoton * 3;
					//imageCanvas2.SetImageBitmap(arcoAmarillo);
					//imageCanvas2.LayoutParameters.Height = mAltoBoton * 3;

					twValorLlave.Text = "$ " + dValorLlave;

					//string tmpMisRegistrados = "00" + dMisRegistrados;
					string tmpMisRegistrados = "00" + dIntransferibles;
					twRegistrados.Text = tmpMisRegistrados.Substring(tmpMisRegistrados.Length - 2, 2);
					//string tmpDmisLlaves = "00" + dMisLlaves.Trim();
					string tmpDmisLlaves = "00" + dTotalLlaves.Trim();
					twLlaves.Text = tmpDmisLlaves.Substring(tmpDmisLlaves.Length - 2, 2);
					twTotalLlavesAuto.Text = "/" + dmiauto_llaves;
					//string tmptotalReferidos = "00" + dTotalReferidos;
					//twTotalReferidos.Text = "/" + tmptotalReferidos.Substring(tmptotalReferidos.Length - 2, 2);
					twTotalReferidos.Text = "/" + dTotalReferidos;
					twVencimiento.Text = dVencimiento;
					//Console.WriteLine("Alto:"+)

					//Auto
					twtitulo.Text = dmiauto_titulo;

				});

			}
			catch (Exception ex) {
				Console.WriteLine("ERRRRRRRRRRRRRRRRRRRRRRRRRRRROORRRRRRRRRRRRRRRRRRRRRRRRR:" + ex.Data);
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
			//Console.Out.WriteLine("-----DATOS USUARIO: " + tmpDatosUsuario.ToString());
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
				if (datos[1] == "")
					datos[1] = "0";
				if (datos[6] == "")
					datos[6] = "0";
				ISharedPreferencesEditor cargarDatos = misDatos.Edit();
				cargarDatos.PutString("valorLlave", datos[0]);
				cargarDatos.PutString("misLlaves", datos[1]);
				cargarDatos.PutString("totalReferidos", datos[2]);
				cargarDatos.PutString("misRegistrados", datos[3]);
				cargarDatos.PutString("vencimiento", datos[4]);
				//ACA : datos[5] es el ID del auto. Buscar ese ID en la BD autos y actualizar preferences
				cargarDatos.PutString("miauto_id", datos[5]);
				cargarDatos.PutString("intransferibles", datos[6]);
				cargarDatos.PutString("bloqueado", datos[7]);
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
			string tmpDatosAuto = solicitudesWeb.getDatosAuto(dmiauto_id);
			//Console.Out.WriteLine("-----DATOS AUTO: " + tmpDatosAuto.ToString());
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
				for (int i = 0; i < 4; i++) {
					string tmpMiAutoUrl1 = datos[3 + i].ToString().Trim();
					if (tmpMiAutoUrl1 != "") {
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
					}
				}
				////
				//GUARDAR DATOS DE AUTO
				ISharedPreferencesEditor cargarDatos = misDatos.Edit();
				cargarDatos.PutString("miauto_titulo", datos[0]);
				cargarDatos.PutString("miauto_precio", datos[1]);
				cargarDatos.PutString("miauto_descripcion", datos[2]);
				cargarDatos.PutString("miauto_url", datos[3]);
				cargarDatos.PutString("miauto_foto", memoriaInterna.convertirEnAlfaNumerico(datos[3]));
				cargarDatos.PutString("miauto_url2", datos[4]);
				cargarDatos.PutString("miauto_url3", datos[5]);
				cargarDatos.PutString("miauto_url4", datos[6]);
				cargarDatos.PutString("miauto_llaves", datos[7]);
				//Console.WriteLine("miauto_titulo" + datos[0]);
				//Console.WriteLine("miauto_llaves" + datos[7]);
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
