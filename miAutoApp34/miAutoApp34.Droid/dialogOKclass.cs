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
	public class dialogOKclass : DialogFragment {
		private static string titulo;
		private static string mensaje;
		//public int valorRespuesta;
		

		//public string mensaje;
		public static dialogOKclass NewInstance(Bundle bundle, String _titulo, String _mensaje) {
			dialogOKclass fragment = new dialogOKclass();
			mensaje = _mensaje;
			titulo = _titulo;
			fragment.Arguments = bundle;
			//string mensaje=_mensaje;
			return fragment;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			// Use this to return your custom view for this Fragment
			View view = inflater.Inflate(Resource.Layout.dialogOK, container, false);
			//RequestWindowFeature(WindowFeatures.NoTitle);

			///REFERENCIAS A CONTROLES
			Button btnOK = view.FindViewById<Button>(Resource.Id.btnOK);
			TextView texto1 = view.FindViewById<TextView>(Resource.Id.textView1);
			TextView texto2 = view.FindViewById<TextView>(Resource.Id.textView2);
			texto1.Text = titulo;
			texto2.Text = mensaje;

			//FUENTES
			Typeface tf = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-BOLD.TTF");
			Typeface tf2 = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-REGULAR.TTF");
			Typeface tf3 = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-BLACK.TTF");

			texto1.Typeface = tf;
			texto2.Typeface = tf2;
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