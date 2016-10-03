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
	public class fCuentaSinAuto : SupportFragment {
		ISharedPreferences misDatos;
		int mAltoBoton;
		public override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);


			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			View view = inflater.Inflate(Resource.Layout.fCuentaSinAuto, container, false);

			///Datos
			
			misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			/*
			string dMiAuto = misDatos.GetString("miAuto", "");
			string dMisLlaves = misDatos.GetString("misLlaves", "");
			string dMisRefereidos = misDatos.GetString("misReferidos", "");
			*/

			///////////////////////////CONTROLES//////////////////
			TextView text1 = view.FindViewById<TextView>(Resource.Id.textView1);
			TextView text2 = view.FindViewById<TextView>(Resource.Id.textView2);
			TextView text3 = view.FindViewById<TextView>(Resource.Id.textView3);

			Button contactar = view.FindViewById<Button>(Resource.Id.contactar);
			Button elegirAuto = view.FindViewById<Button>(Resource.Id.elegirauto);
			Button consulta=view.FindViewById<Button>(Resource.Id.consulta);
			ImageView miAuto = view.FindViewById<ImageView>(Resource.Id.imAuto);
			//btnllaves.Typeface = fntRegular;

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

			contactar.Typeface = fntCondensedBold;
			elegirAuto.Typeface = fntCondensedBold;
			consulta.Typeface = fntCondensedBold;
			text1.Typeface = tf2;
			text2.Typeface = tf2;
			text3.Typeface = tf2;


			/*ScaleDrawable sd = new ScaleDrawable(Resource.Drawable.llaveiconob, 0, 10f, 10f);
btnllaves.SetCompoundDrawables()
var bounds = btnllaves.Left
left.SetBounds(bounds.Left, bounds.Top, bounds.Right, bounds.Bottom - 70);
*/
			///ajustar tamaño (alto) de imAuto
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
			mAltoBoton = elegirAuto.LayoutParameters.Height;
			int sumaAltoViews = mAltoBoton * 2;
			Console.WriteLine("mAltoboton" + mAltoBoton);
			if (metrics.HeightPixels < 500) {
				double multiplo = 0.8;
				mAltoBoton = (int)(mAltoBoton * multiplo);
				elegirAuto.LayoutParameters.Height = mAltoBoton;
				contactar.LayoutParameters.Height = mAltoBoton;
				consulta.LayoutParameters.Height = mAltoBoton;
				elegirAuto.SetTextSize(ComplexUnitType.Px, (float)(mAltoBoton * 0.5));
				contactar.SetTextSize(ComplexUnitType.Px, (float)(mAltoBoton * 0.5));
				consulta.SetTextSize(ComplexUnitType.Px, (float)(mAltoBoton * 0.5));
				text1.SetTextSize(ComplexUnitType.Px, (float)(text1.TextSize * multiplo));
				text2.SetTextSize(ComplexUnitType.Px, (float)(text2.TextSize * multiplo));
				text3.SetTextSize(ComplexUnitType.Px, (float)(text3.TextSize * multiplo));
				//twRegistrados.SetTextSize(ComplexUnitType.Px, (float)(twRegistrados.TextSize * multiplo));

				//twtitulo.Typeface = tf;


				Console.WriteLine("cambio altoboton");
				Console.WriteLine("mAltoboton" + mAltoBoton);
			}

			///cambiar tamaño de miAuto
			string dLinearTitulo = misDatos.GetString("LinearTitulo", "0");
			int tmpAltoTitulo = Int32.Parse(dLinearTitulo) * 3;

			//int sumaAltoViews = (text2a.LayoutParameters.Height + btnllaves.LayoutParameters.Height +
			//espacioView.LayoutParameters.Height) * 2;
			//mAltoBoton= btnllaves.LayoutParameters.Height;
			sumaAltoViews = sumaAltoViews + (int)(mAltoBoton * 4);
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

			///////////////////BOTON CONTACTAR/////////////////////////////////////////////////////////////////////
			contactar.Click += (o, s) => {
				var progressDialog = ProgressDialog.Show(Context, "", "Registrado Pedido...", true);
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
			elegirAuto.Click += (s, o) => {
				mainFragment.viewPager.SetCurrentItem(2, true);
			};



			return view;
			// return base.OnCreateView(inflater, container, savedInstanceState);
		}
	}
}