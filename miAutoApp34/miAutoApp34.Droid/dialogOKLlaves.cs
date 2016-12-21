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
	public class dialogOKLlaves : DialogFragment {
		//private static string titulo;
		private static string mensaje;
		private static string transferibles;
		private static string intransferibles;
		private static string totalLlaves;
		//public int valorRespuesta;


		//public string mensaje;
		//public static dialogOKLlaves NewInstance(Bundle bundle, String _titulo, String _mensaje) {
		public static dialogOKLlaves NewInstance(Bundle bundle, String _transferibles, String _intransferibles, String _totalLlaves) {
			dialogOKLlaves fragment = new dialogOKLlaves();
			//mensaje = _mensaje;
			mensaje= "(*)Son adquiridas abonando el costo de la llave (cuota). Se pueden vender, transferir o enajenar. "+
				"\n(**)Son adquiridas a través del sistema de capitalización por llaves, NO se pueden vender ni transferir.";
			transferibles = _transferibles;
			intransferibles = _intransferibles;
			totalLlaves = _totalLlaves;
			//titulo = _titulo;
			fragment.Arguments = bundle;
			//string mensaje=_mensaje;
			return fragment;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			// Use this to return your custom view for this Fragment
			View view = inflater.Inflate(Resource.Layout.dialogOKLlaves, container, false);
			//RequestWindowFeature(WindowFeatures.NoTitle);

			///REFERENCIAS A CONTROLES
			Button btnOK = view.FindViewById<Button>(Resource.Id.btnOK);
			TextView texto1 = view.FindViewById<TextView>(Resource.Id.textView1);
			TextView texto2 = view.FindViewById<TextView>(Resource.Id.textView2);
			TextView texto3 = view.FindViewById<TextView>(Resource.Id.textView3);
			TextView texto4 = view.FindViewById<TextView>(Resource.Id.textView4);
			TextView texto5 = view.FindViewById<TextView>(Resource.Id.textView5);
			TextView texto6 = view.FindViewById<TextView>(Resource.Id.textView6);
			TextView texto7 = view.FindViewById<TextView>(Resource.Id.textView7);
			TextView texto8 = view.FindViewById<TextView>(Resource.Id.textView8);
			//texto1.Text = titulo;
			texto8.Text = mensaje;
			texto3.Text = transferibles;
			texto5.Text = intransferibles;
			texto7.Text = totalLlaves;

			//FUENTES
			Typeface tf = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-BOLD.TTF");
			Typeface tf2 = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-REGULAR.TTF");
			Typeface tf3 = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-BLACK.TTF");

			texto1.Typeface = tf;
			texto2.Typeface = tf2;
			texto3.Typeface = tf3;
			texto4.Typeface = tf2;
			texto5.Typeface = tf3;
			texto6.Typeface = tf2;
			texto7.Typeface = tf3;
			texto8.Typeface = tf2;
			btnOK.Typeface = tf2;

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