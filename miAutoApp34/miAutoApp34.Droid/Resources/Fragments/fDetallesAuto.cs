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

using Android.Util;
using Android.Graphics;
using Macaw.UIComponents;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Android.Content.PM;
using System.Threading;

namespace miAutoApp34.Droid {
	[Activity(Label = "Detalles Auto", Theme = "@style/Theme.DesignDemo", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]


	//public class fDetallesAuto : Android.App.DialogFragment {

	public class fDetallesAuto : FragmentActivity {
		ISharedPreferences misDatos;
		//protected override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
		int mAltoBoton;
		protected override void OnCreate(Bundle bundle) {
			base.OnCreate(bundle);
			//Window.Attributes.WindowAnimations = Resource.Animation.abc_popup_enter;
			//Attributes.WindowAnimations = Resource.Style.animacionesDialog;

			misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);

			SetContentView(Resource.Layout.fDetallesAuto);
			///REFERENCIAS A CONTROLES
			Button btnQuieroEsteAuto = FindViewById<Button>(Resource.Id.btnQuieroEsteAuto);
			//ImageView flechaBack = FindViewById<ImageView>(Resource.Id.btnFlecha);
			TextView twTitulo = FindViewById<TextView>(Resource.Id.twtitulo);
			TextView twDescripcion = FindViewById<TextView>(Resource.Id.twdescripcion);
			TextView twpreciolabel = FindViewById<TextView>(Resource.Id.twpreciolabel);
			TextView twprecio = FindViewById<TextView>(Resource.Id.twprecio);
			TextView twllaveslabel = FindViewById<TextView>(Resource.Id.twllaveslabel);
			TextView twllaves = FindViewById<TextView>(Resource.Id.twllaves);
			ImageView circ1 = FindViewById<ImageView>(Resource.Id.circulo1);
			ImageView circ2 = FindViewById<ImageView>(Resource.Id.circulo2);
			ImageView circ3 = FindViewById<ImageView>(Resource.Id.circulo3);
			ImageView circ4 = FindViewById<ImageView>(Resource.Id.circulo4);
			LinearLayout btnVolver = FindViewById<LinearLayout>(Resource.Id.btnVolver);

			//FUENTES
			Typeface tf = Typeface.CreateFromAsset(Assets, "fonts/ROBOTO-BOLD.TTF");
			Typeface tf2 = Typeface.CreateFromAsset(Assets, "fonts/ROBOTO-REGULAR.TTF");
			Typeface tf3 = Typeface.CreateFromAsset(Assets, "fonts/ROBOTO-BLACK.TTF");
			twTitulo.Typeface = tf3;
			twDescripcion.Typeface = tf2;
			twpreciolabel.Typeface = tf2;
			twprecio.Typeface = tf3;
			twllaveslabel.Typeface = tf2;
			twllaves.Typeface = tf3;
			btnQuieroEsteAuto.Typeface = tf3;

			///obtener datos desde el intent
			string modo = Intent.GetStringExtra("modo") ?? "";
			string url1 = Intent.GetStringExtra("detalle.url1") ?? "";
			string url2 = Intent.GetStringExtra("detalle.url2") ?? "";
			string url3 = Intent.GetStringExtra("detalle.url3") ?? "";
			string url4 = Intent.GetStringExtra("detalle.url4") ?? "";
			string titulo = Intent.GetStringExtra("detalle.titulo") ?? "";
			string descripcion = Intent.GetStringExtra("detalle.descripcion") ?? "";
			string precio = Intent.GetStringExtra("detalle.precio") ?? "";
			string detalle_id = Intent.GetStringExtra("detalle.id") ?? "";
			string detalle_llaves = Intent.GetStringExtra("detalle.llaves") ?? "";
			// Use this to return your custom view for this Fragment
			//View view = inflater.Inflate(Resource.Layout.fDetallesAuto, container, false);
			//RequestWindowFeature(WindowFeatures.NoTitle);

			///Mostrar datos
			twTitulo.Text = titulo;
			twDescripcion.Text = descripcion;
			twDescripcion.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
			twprecio.Text = "$ " + precio;
			twllaves.Text = detalle_llaves;

			//Console.WriteLine("titulo detalle:" + titulo);
			//Console.WriteLine("id detalle:"+ detalle_id);

			///ajustar tamaño (alto) de imAuto
			var metrics = Application.Context.Resources.DisplayMetrics;
			mAltoBoton = btnQuieroEsteAuto.LayoutParameters.Height;
			int sumaAltoViews = mAltoBoton * 2;
			//Console.WriteLine("mAltoboton" + mAltoBoton);
			if (metrics.HeightPixels < 500) {
				double multiplo = 0.8;
				mAltoBoton = (int)(mAltoBoton * multiplo);
				btnQuieroEsteAuto.LayoutParameters.Height = mAltoBoton;
				btnQuieroEsteAuto.SetTextSize(ComplexUnitType.Px, (float)(mAltoBoton * 0.5));

				twllaves.SetTextSize(ComplexUnitType.Px, (float)(twprecio.TextSize * multiplo));
				twllaveslabel.SetTextSize(ComplexUnitType.Px, (float)(twpreciolabel.TextSize * multiplo));
				twprecio.SetTextSize(ComplexUnitType.Px, (float)(twprecio.TextSize * multiplo));
				twpreciolabel.SetTextSize(ComplexUnitType.Px, (float)(twpreciolabel.TextSize * multiplo));
				twTitulo.SetTextSize(ComplexUnitType.Px, (float)(twTitulo.TextSize * multiplo));
				twDescripcion.SetTextSize(ComplexUnitType.Px, (float)(twDescripcion.TextSize * multiplo));
				//twRegistrados.SetTextSize(ComplexUnitType.Px, (float)(twRegistrados.TextSize * multiplo));
				//twtitulo.Typeface = tf;
				//Console.WriteLine("cambio altoboton");
				//Console.WriteLine("mAltoboton" + mAltoBoton);
			}


			///cambiar tamaño de miAuto
			string dLinearTitulo = misDatos.GetString("LinearTitulo", "0");
			int tmpAltoTitulo = Int32.Parse(dLinearTitulo) * 3;
			sumaAltoViews = sumaAltoViews + (int)(mAltoBoton * 2);
			int tmpAlto = (metrics.HeightPixels - (tmpAltoTitulo + sumaAltoViews));

			//miAuto.LayoutParameters.Height = tmpAlto;
			RelativeLayout relative1 = FindViewById<RelativeLayout>(Resource.Id.relative1);
			relative1.LayoutParameters.Height = tmpAlto;

			///MODOS
			if (modo == "desdefCuenta") {
				//btnQuieroEsteAuto.Text = "Pedir MiAuto";
				///Desde fCuenta oculta el boton quiero este auto
				RelativeLayout relative2 = FindViewById<RelativeLayout>(Resource.Id.relative2);
				btnQuieroEsteAuto.LayoutParameters.Height = 0;
				btnQuieroEsteAuto.Visibility = ViewStates.Gone;
				int tmpAlturaDescipcion = twDescripcion.LayoutParameters.Height;
				//twDescripcion.LayoutParameters.Height = tmpAlturaDescipcion + mAltoBoton;
				relative2.LayoutParameters.Height = tmpAlturaDescipcion + mAltoBoton + mAltoBoton;
				twDescripcion.LayoutParameters.Height = tmpAlturaDescipcion + mAltoBoton;

				//Console.WriteLine("mAltoBoton:" + mAltoBoton.ToString());
				//Console.WriteLine("tmpAlturaDescipcion:" + tmpAlturaDescipcion.ToString());
				//relative1.LayoutParameters.Height = tmpAlto+mAltoBoton;
			}

			///Mostrar las imagenes - Carrousel.
			var pager = FindViewById<ViewPager>(Resource.Id.pager);
			var adaptor = new GenericFragmentPagerAdaptor(SupportFragmentManager);

			int anchoImagenes = 60;

			adaptor.AddFragmentView((i, v, b) => {
				var tabImagenAuto = i.Inflate(Resource.Layout.tabImagenAuto, v, false);
				//var sampleTextView = tabImagenAuto.FindViewById<TextView>(Resource.Id.detallesTexto1);
				//sampleTextView.Text = "This is content";
				//sampleTextView.Text = "url1:" + url1;
				ImageView imagenAuto = tabImagenAuto.FindViewById<ImageView>(Resource.Id.iwauto);
				//imagenAuto.LayoutParameters.Height = tmpAlto;
				string tmpUrlAlfa = memoriaInterna.convertirEnAlfaNumerico(url1);
				Bitmap imageBitmap = memoriaInterna.LeerImagen(tmpUrlAlfa, anchoImagenes);
				//Bitmap imageBitmap = memoriaInterna.cargarImagenDesdeCache(url1);
				imagenAuto.SetImageBitmap(imageBitmap);
				imagenAuto.SetScaleType(ImageView.ScaleType.FitCenter);
				//imageBitmap.Recycle();
				return tabImagenAuto;
			});
			circ2.Visibility = ViewStates.Gone;
			circ3.Visibility = ViewStates.Gone;
			circ4.Visibility = ViewStates.Gone;
			int tmpCantidadDePreviews = 1;
			if (url2 != "") {
				tmpCantidadDePreviews++;
			}
			if (url3 != "") {
				tmpCantidadDePreviews++;
			}
			if (url4 != "") {
				tmpCantidadDePreviews++;
			}
			switch (tmpCantidadDePreviews) {
				case 2:
					circ2.Visibility = ViewStates.Visible;
					break;
				case 3:
					circ2.Visibility = ViewStates.Visible;
					circ3.Visibility = ViewStates.Visible;
					break;
				case 4:
					circ2.Visibility = ViewStates.Visible;
					circ3.Visibility = ViewStates.Visible;
					circ4.Visibility = ViewStates.Visible;
					break;
				default:
					Console.WriteLine("Default case");
					break;
			}
			/*
			if (url2 == "") {
				circ2.Visibility = ViewStates.Gone;
			}
			else {
				circ2.Visibility = ViewStates.Visible;
			}
			if (url3 == "") {
				circ3.Visibility = ViewStates.Gone;
			}
			else {
				circ3.Visibility = ViewStates.Visible;
			}
			if (url4 == "") {
				circ4.Visibility = ViewStates.Gone;
			}
			else {
				circ4.Visibility = ViewStates.Visible;
			}
			*/

			if (url2 != "") {
				adaptor.AddFragmentView((i, v, b) => {
					var tabImagenAuto = i.Inflate(Resource.Layout.tabImagenAuto, v, false);
					ImageView imagenAuto = tabImagenAuto.FindViewById<ImageView>(Resource.Id.iwauto);
					string tmpUrlAlfa = memoriaInterna.convertirEnAlfaNumerico(url2);
					Bitmap imageBitmap = memoriaInterna.LeerImagen(tmpUrlAlfa, anchoImagenes);
					//Bitmap imageBitmap = memoriaInterna.cargarImagenDesdeCache(url2);
					imagenAuto.SetScaleType(ImageView.ScaleType.FitCenter);
					imagenAuto.SetImageBitmap(imageBitmap);
					//imageBitmap.Recycle();
					return tabImagenAuto;
				});
			}
			if (url3 != "") {
				adaptor.AddFragmentView((i, v, b) => {
					var tabImagenAuto = i.Inflate(Resource.Layout.tabImagenAuto, v, false);
					ImageView imagenAuto = tabImagenAuto.FindViewById<ImageView>(Resource.Id.iwauto);
					string tmpUrlAlfa = memoriaInterna.convertirEnAlfaNumerico(url3);
					Bitmap imageBitmap = memoriaInterna.LeerImagen(tmpUrlAlfa, anchoImagenes);
					//Bitmap imageBitmap = memoriaInterna.cargarImagenDesdeCache(url3);
					imagenAuto.SetScaleType(ImageView.ScaleType.FitCenter);
					imagenAuto.SetImageBitmap(imageBitmap);
					//imageBitmap.Recycle();
					return tabImagenAuto;
				});
			}
			if (url4 != "") {
				adaptor.AddFragmentView((i, v, b) => {
					var tabImagenAuto = i.Inflate(Resource.Layout.tabImagenAuto, v, false);
					ImageView imagenAuto = tabImagenAuto.FindViewById<ImageView>(Resource.Id.iwauto);
					string tmpUrlAlfa = memoriaInterna.convertirEnAlfaNumerico(url4);
					Bitmap imageBitmap = memoriaInterna.LeerImagen(tmpUrlAlfa, anchoImagenes);
					//Bitmap imageBitmap = memoriaInterna.cargarImagenDesdeCache(url4);
					imagenAuto.SetScaleType(ImageView.ScaleType.FitCenter);
					imagenAuto.SetImageBitmap(imageBitmap);

					//imageBitmap.Recycle();
					return tabImagenAuto;
				});
			}


			pager.AddOnPageChangeListener(new cambioDeImagenListener(circ1, circ2, circ3, circ4));
			pager.Adapter = adaptor;

			///FUNCIONES BOTONES
			btnQuieroEsteAuto.Click += delegate {
				if (modo == "desdefCuenta") {
					/*
					//var progressDialog = ProgressDialog.Show(this.BaseContext, "", "Cargando...", true);
					new System.Threading.Thread(new ThreadStart(delegate {
						string requeremientos = solicitudesWeb.getVariable("Requerimientos");
						RunOnUiThread(() => {
							//progressDialog.Hide();
							if (requeremientos != "SinConexion") {
								//Dismiss();
								Android.App.FragmentTransaction ft = this.FragmentManager.BeginTransaction();
								Android.App.Fragment prev = this.FragmentManager.FindFragmentByTag("dialogConsulta");
								if (prev != null) {
									ft.Remove(prev);
								}
								ft.AddToBackStack(null);
								dialogPedirAuto newFragmentContactar = dialogPedirAuto.NewInstance(null, requeremientos);
								newFragmentContactar.Show(ft, "dialogConsulta");
							}
							else {
								RunOnUiThread(() => {
									//Dismiss();
									Toast.MakeText(Application.Context, "sin conexión", ToastLength.Long).Show();
								});
							}
						});
					})).Start();
					*/
				}
				else {
					Android.App.FragmentTransaction ft = this.FragmentManager.BeginTransaction();
					//Remove fragment else it will crash as it is already added to backstack
					Android.App.Fragment prev = this.FragmentManager.FindFragmentByTag("dialogOkElegir");
					if (prev != null) {
						ft.Remove(prev);
					}
					ft.AddToBackStack(null);
					// Create and show the dialog.
					string tmpNombreArchivoUrl1 = memoriaInterna.convertirEnAlfaNumerico(url1);
					string datosExtra = detalle_id + "[" + titulo + "[" + precio + "[" + url1 + "[" + tmpNombreArchivoUrl1+ "[" + detalle_llaves;
					//Console.WriteLine(datosExtra);
					dialogOKCancelarclass newFragmentCorregir = dialogOKCancelarclass.NewInstance(bundle, titulo, "¿Asociar este modelo con tu perfil?", 2, datosExtra);
					newFragmentCorregir.Show(ft, "dialogOkElegir");
				}
			};
			btnVolver.Click += (s, o) => {
				//base.OnBackPressed();
				//Console.WriteLine("PAGER COUNT: "+pager.ChildCount);

				try {
					ImageView im0 = pager.GetChildAt(0).FindViewById<ImageView>(Resource.Id.iwauto);
					im0.SetImageBitmap(null);
					ImageView im1 = pager.GetChildAt(1).FindViewById<ImageView>(Resource.Id.iwauto);
					im1.SetImageBitmap(null);
					ImageView im2 = pager.GetChildAt(2).FindViewById<ImageView>(Resource.Id.iwauto);
					im2.SetImageBitmap(null);
					ImageView im3 = pager.GetChildAt(3).FindViewById<ImageView>(Resource.Id.iwauto);
					im3.SetImageBitmap(null);

				}
				catch (Exception ex) {
					//Console.WriteLine("Error messageeeeee: " + ex.Message);
					Console.WriteLine("Error messageeeeee: " + ex.Message);
				}

				this.Finish();
			};

			//this.FragmentManager
			//return view;



		}
		/*
		public override void OnActivityCreated(Bundle savedInstanceState) {
			Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
			base.OnActivityCreated(savedInstanceState);
			Dialog.Window.Attributes.WindowAnimations = Resource.Style.animacionesDialog;
		}
		*/
		public override void OnBackPressed() {
			base.OnBackPressed();
			this.Finish();
		}
		public override void OnDetachedFromWindow() {
			base.OnDetachedFromWindow();
		}

	}
}
