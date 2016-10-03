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

namespace miAutoApp34.Droid {
	public class dialogOKrefefidos : DialogFragment {
		private static string titulo;
		private static string mensaje;
		//public int valorRespuesta;


		//public string mensaje;
		public static dialogOKrefefidos NewInstance(Bundle bundle, String _titulo, String _mensaje) {
			dialogOKrefefidos fragment = new dialogOKrefefidos();
			mensaje = _mensaje;
			titulo = _titulo;
			fragment.Arguments = bundle;
			//string mensaje=_mensaje;
			return fragment;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			// Use this to return your custom view for this Fragment
			View view = inflater.Inflate(Resource.Layout.dialogOKreferidos, container, false);
			//RequestWindowFeature(WindowFeatures.NoTitle);

			///REFERENCIAS A CONTROLES
			Button btnOK = view.FindViewById<Button>(Resource.Id.btnOK);

			TextView texto1 = view.FindViewById<TextView>(Resource.Id.textView1);
			TextView texto2 = view.FindViewById<TextView>(Resource.Id.textView2);
			texto2.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
			texto1.Text = titulo;
			texto2.Text = mensaje;

			//FUENTES
			Typeface tf = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-BOLD.TTF");
			Typeface tf2 = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-REGULAR.TTF");
			Typeface tf3 = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-BLACK.TTF");

			texto1.Typeface = tf;
			texto2.Typeface = tf2;
			btnOK.Typeface = tf2;

			//ajustar tamaño
			var metrics = inflater.Context.Resources.DisplayMetrics;
			int tmpAncho = metrics.WidthPixels;
			int tmpAlto = metrics.HeightPixels;
			if (tmpAncho > 700) {
				//texto2.SetMaxLines(10);
				
				texto2.TextSize = tmpAncho/40;
				//texto2.SetLines(15);
				texto2.LayoutParameters.Width = (int)(tmpAncho * 0.8);
				texto2.LayoutParameters.Height = (int)(tmpAlto * 0.55);
				//texto2.SetTextSize(TypedValue.ComplexToDimension., 35);
			}
			Console.WriteLine("Ancho:" + metrics.WidthPixels.ToString());
			Console.WriteLine("Fuente:" + texto2.TextSize.ToString());

			///FUNCIONES BOTONES
			btnOK.Click += delegate {
				//valorRespuesta = 0;
				//Console.WriteLine("-1---VG: OK");
				Dismiss();
				//Toast.MakeText(Activity, mensaje, ToastLength.Short).Show();
			};

			return view;
		}
		public override void OnActivityCreated(Bundle savedInstanceState) {
			Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
			base.OnActivityCreated(savedInstanceState);
			Dialog.Window.Attributes.WindowAnimations = Resource.Style.animacionesDialog;
		}
	}
}