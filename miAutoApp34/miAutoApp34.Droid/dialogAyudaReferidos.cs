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
	public class dialogAyudaReferidos : DialogFragment {
		//private static string titulo;
		//private static string mensaje;
		//public int valorRespuesta;


		//public string mensaje;
		public static dialogAyudaReferidos NewInstance(Bundle bundle) {
			dialogAyudaReferidos fragment = new dialogAyudaReferidos();
			//mensaje = _mensaje;
			//titulo = _titulo;
			fragment.Arguments = bundle;
			//string mensaje=_mensaje;
			return fragment;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			// Use this to return your custom view for this Fragment
			View view = inflater.Inflate(Resource.Layout.dialogAyudaReferidos, container, false);
			//RequestWindowFeature(WindowFeatures.NoTitle);

			///REFERENCIAS A CONTROLES
			Button btnOK = view.FindViewById<Button>(Resource.Id.btnOK);
			TextView texto1 = view.FindViewById<TextView>(Resource.Id.textView1);
			TextView texto2 = view.FindViewById<TextView>(Resource.Id.textView2);
			CheckBox checkBox1 = view.FindViewById<CheckBox>(Resource.Id.checkBox1);
			string titulo = "Instrucciones";
			string mensaje = "1) Seleccione los Contactos que desee agregar a su Lista de Referidos \n\n2) Presione el botón Agregar (+)";
			texto1.Text = titulo;
			texto2.Text = mensaje;

			//MOSTRAR U OCULTAR LA AYUDA AL INICIAR
			ISharedPreferences misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			string NoMostrarAyuda = misDatos.GetString("NoMostrarAyuda", "");
			if(NoMostrarAyuda=="1") {
				checkBox1.Checked = true;
			} else {
				checkBox1.Checked = false;
			}

			//FUENTES
			Typeface tf = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-BOLD.TTF");
			Typeface tf2 = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-REGULAR.TTF");
			Typeface tf3 = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-BLACK.TTF");

			texto1.Typeface = tf;
			texto2.Typeface = tf2;
			btnOK.Typeface = tf2;

			///FUNCIONES BOTONES
			btnOK.Click += delegate {
				ISharedPreferencesEditor tmpCargarDatos = misDatos.Edit();
				string tmpValor = "0";
				if (checkBox1.Checked) {
					tmpValor = "1";
				}
				//Toast.MakeText(Activity, tmpValor, ToastLength.Long).Show();
				tmpCargarDatos.PutString("NoMostrarAyuda", tmpValor);
				tmpCargarDatos.Apply();
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