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
using Android.Content;

namespace miAutoApp34.Droid {
	public class dialogOKcontactar : DialogFragment  {
		//private static string titulo;
		private static string mensaje;
		//public int valorRespuesta;
		private static string txtNumero;


		//public string mensaje;
		//public static dialogOKcontactar NewInstance(Bundle bundle, String _titulo, String _mensaje) {
		public static dialogOKcontactar NewInstance(Bundle bundle) {
			dialogOKcontactar fragment = new dialogOKcontactar();
			//mensaje = _mensaje;
			//titulo = _titulo;
			//titulo = "Solicitud registrada";
			ISharedPreferences misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			txtNumero= misDatos.GetString("numeroWA", "");
			mensaje = "Un asesor se comunicará con usted a la brevedad.\n\n" +
				"O si lo desea, puede llamar o mandar un mensaje de WhatsApp al siguiente número:";
			//txtNumero = _txtNumero;
			fragment.Arguments = bundle;

			//string mensaje=_mensaje;
			return fragment;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			// Use this to return your custom view for this Fragment
			View view = inflater.Inflate(Resource.Layout.dialogOKContactar, container, false);
			//RequestWindowFeature(WindowFeatures.NoTitle);

			///REFERENCIAS A CONTROLES
			Button btnOK = view.FindViewById<Button>(Resource.Id.btnOK);
			LinearLayout btnWhatsApp = view.FindViewById<LinearLayout>(Resource.Id.linearWhatsapp);
			TextView texto1 = view.FindViewById<TextView>(Resource.Id.textView1);
			TextView texto2 = view.FindViewById<TextView>(Resource.Id.textView2);
			TextView numeroWA = view.FindViewById<TextView>(Resource.Id.textNumeroWA);
			LinearLayout btnCopiarNumero = view.FindViewById<LinearLayout>(Resource.Id.btnCopiarNumero);


			//FUENTES
			Typeface tf = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-BOLD.TTF");
			Typeface tf2 = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-REGULAR.TTF");
			Typeface tf3 = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-BLACK.TTF");

			texto1.Typeface = tf;
			texto2.Typeface = tf2;
			btnOK.Typeface = tf2;
			numeroWA.Typeface = tf2;

			//texto1.Text = titulo;
			texto2.Text = mensaje;
			numeroWA.Text = txtNumero;

			///FUNCIONES BOTONES
			btnOK.Click += delegate {
				//valorRespuesta = 0;
				//Console.WriteLine("-1---VG: OK");
				Dismiss();
				//Toast.MakeText(Activity, mensaje, ToastLength.Short).Show();
			};

			btnWhatsApp.Click += delegate {
				Dismiss();

				//abre la ventana de chat de un destinatario
				//string number = "5493794341567";
				string number = txtNumero;
				Android.Net.Uri uri = Android.Net.Uri.Parse("smsto:" + number);
				Intent i = new Intent(Intent.ActionSendto, uri);
				i.SetPackage("com.whatsapp");
				StartActivity(Intent.CreateChooser(i, "Mensaje con WhatsApp"));

				/*Intent i = new Intent();
				i.SetType("text/plain");
				i.SetAction(Intent.ActionSend);
				i.SetPackage("com.whatsapp");
				//i.PutExtra(Intent.ExtraText, textoPromocional);
				*/
				StartActivity(i);
			};

			btnCopiarNumero.Click += delegate {
				var clipboard = (ClipboardManager) Application.Context.GetSystemService(Context.ClipboardService);
				var clip = ClipData.NewPlainText("numero WhatsApp",txtNumero);

				clipboard.PrimaryClip = clip;
				Toast.MakeText(Activity, "Se copió el número al Portapapeles", ToastLength.Short).Show();

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