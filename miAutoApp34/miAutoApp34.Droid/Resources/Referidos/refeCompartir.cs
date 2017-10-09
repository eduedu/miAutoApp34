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
using Xamarin.Facebook.Share.Model;
using Xamarin.Facebook.Share.Widget;
using Xamarin.Facebook.Share;
using Xamarin.Facebook;
using Java.Lang;
using Org.Json;
using Android.Telephony;

namespace miAutoApp34.Droid {
	public class refeCompartir : SupportFragment, IFacebookCallback, GraphRequest.IGraphJSONObjectCallback {
		public Android.Graphics.Typeface fntBlack;
		public Android.Graphics.Typeface fntRegular;
		ISharedPreferences misDatos;
		string dUrlCompartir;
		string dUrlCompartirTitulo;
		string dUrlCompartirTexto;
		string dUrlCompartirImagen;
		string dUrlCompartirMensaje;

		///FACEBOOK
		private ICallbackManager mCallBackManager;
		//private MyProfileTracker mProfileTracker;
		//SHAREEE
		ShareDialog shareDialog;
		MessageDialog messageDialog;
		ShareLinkContent linkContent;
		public override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			///DATOS
			misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			dUrlCompartir = misDatos.GetString("urlCompartir", "");
			dUrlCompartirTitulo = misDatos.GetString("urlCompartirTitulo", "");
			dUrlCompartirTexto = misDatos.GetString("urlCompartirTexto", "");
			dUrlCompartirImagen = misDatos.GetString("urlCompartirImagen", "");
			dUrlCompartirMensaje = misDatos.GetString("urlCompartirMensaje", "");


            ///////FACEBOOOK
            if (FacebookSdk.IsInitialized == false) {
				FacebookSdk.SdkInitialize(Activity.ApplicationContext);
			}
			mCallBackManager = CallbackManagerFactory.Create();
			shareDialog = new ShareDialog(Activity);
			messageDialog = new MessageDialog(Activity);

			shareDialog.RegisterCallback(mCallBackManager, this);
			messageDialog.RegisterCallback(mCallBackManager, this);
			linkContent = new ShareLinkContent.Builder()
					//.SetContentTitle("TITULO")
					//.SetContentDescription("Descargá la App Oficial de MiAutoPlan")
					//.SetContentUrl(Android.Net.Uri.Parse("http://www.autocero.com/"))
					//.SetImageUrl(Android.Net.Uri.Parse("https://scontent-gru2-1.xx.fbcdn.net/t39.2081-0/p128x128/13655700_524964187704825_685694938_n.png"))
					//.SetImageUrl(Android.Net.Uri.Parse("http://www.muypymes.com/wp-content/uploads/2016/05/google.jpg"))
					.SetContentTitle(dUrlCompartirTitulo)
					.SetContentDescription(dUrlCompartirTexto)

					.SetContentUrl(Android.Net.Uri.Parse(dUrlCompartir))
					.JavaCast<ShareLinkContent.Builder>()
					.SetImageUrl(Android.Net.Uri.Parse(dUrlCompartirImagen))
					.JavaCast<ShareLinkContent.Builder>()

					.Build();

		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			View view = inflater.Inflate(Resource.Layout.refeCompartir, container, false);

			//////////////DATOS
			//string textoPromocional = "Hola, descargá la App Oficial de MiAutoPlan en http://www.autocero.com";
			string textoPromocional = dUrlCompartirMensaje;

			//FUENTES
			/*
			//Android.Graphics.Typeface 
			fntRegular = Android.Graphics.Typeface.CreateFromAsset(inflater.Context.Assets, "fonts/ROBOTO-REGULAR.TTF");
			Android.Graphics.Typeface fntBold = Android.Graphics.Typeface.CreateFromAsset(inflater.Context.Assets, "fonts/ROBOTOCONDENSED-BOLD.TTF");
			//Android.Graphics.Typeface 
			fntBlack = Android.Graphics.Typeface.CreateFromAsset(inflater.Context.Assets, "fonts/ROBOTO-BLACK.TTF");
			*/


			//REFERENCIAS A CONTROLES
			LinearLayout btnFBCompartir = view.FindViewById<LinearLayout>(Resource.Id.btnFBCompartir);
			LinearLayout btnFBMensaje = view.FindViewById<LinearLayout>(Resource.Id.btnFBMensaje);
			LinearLayout btnWhatsApp = view.FindViewById<LinearLayout>(Resource.Id.btnWhatsApp);
			LinearLayout btnSMS = view.FindViewById<LinearLayout>(Resource.Id.btnSMS);
			LinearLayout btnMail = view.FindViewById<LinearLayout>(Resource.Id.btnMail);

			/////////////////////BTNFACEBOOK COMPARTIR////////////////////////////////////////////////////////
			btnFBCompartir.Click += (o, e) => {
                //Toast.MakeText(this.Context, "COMPARTIR", ToastLength.Long).Show();
                //messageDialog.Show(linkContent);
                //Console.WriteLine("TITULO:" + dUrlCompartirTitulo);
                //Console.WriteLine("TEXTO:" + dUrlCompartirTexto);

                shareDialog.Show(linkContent);
			};

			/////////////////////BTNFACEBOOK MENSAJE/////////////////
			btnFBMensaje.Click += (o, e) => {
				messageDialog.Show(linkContent);
			};

			/////////////////////WHATSAPP         /////////////////////////////////////////////////////////////////////////
			btnWhatsApp.Click += (o, e) => {

				/* 
				//Intento de mandar a varios destinatarios 
				//string number =  "5493794341567";
				//string number2 = "5491155240909";
				List<Android.Net.Uri> uris = new List<Android.Net.Uri>();
				Android.Net.Uri u1 = Android.Net.Uri.Parse("smsto:" + number);
				Android.Net.Uri u2 = Android.Net.Uri.Parse("smsto:" + number2);
				uris.Add(u1);
				uris.Add(u2);
				//Intent i = new Intent(Intent.ActionSendMultiple, uri);
				*/

				/*
				//abre la ventana de chat de un destinatario
				string number =  "5493794341567";
				Android.Net.Uri uri = Android.Net.Uri.Parse("smsto:" + number);
				Intent i = new Intent(Intent.ActionSendto, uri);
				i.SetPackage("com.whatsapp");
				StartActivity(Intent.CreateChooser(i, "Mensaje con WhatsApp"));
				*/
				Intent i = new Intent();
				i.SetType("text/plain");
				i.SetAction(Intent.ActionSend);
				i.SetPackage("com.whatsapp");
				i.PutExtra(Intent.ExtraText, textoPromocional);
				StartActivity(i);

			};
			/////////////////////btn SMS/////////////////
			btnSMS.Click += (o, e) => {
				//SmsManager.Default.SendTextMessage("", null, "Hola, bajate la App Oficial de MiAutoPlan en http://www.autocero.com", null, null);
				//var smsUri = Android.Net.Uri.Parse("smsto:1234567890");
				//var smsIntent = new Intent(Intent.ActionSendto, smsUri);
				var smsIntent = new Intent(Intent.ActionSend);
				smsIntent.PutExtra(Intent.ExtraText, textoPromocional);
				//smsIntent.PutExtra("sms_body", textoPromocional);
				smsIntent.SetType("text/plain");
				//StartActivity(smsIntent);
				StartActivity(Intent.CreateChooser(smsIntent, "Mandar SMS"));
			};

			/////////////////////////////////////MAIL                //////////////////////
			btnMail.Click += (o, e) => {
				var email = new Intent(Android.Content.Intent.ActionSend);
				email.PutExtra(Android.Content.Intent.ExtraSubject, "MiAutoPlan");
				email.PutExtra(Android.Content.Intent.ExtraText, textoPromocional);
				email.SetType("message/rfc822");
				StartActivity(Intent.CreateChooser(email, "Mandar Correo"));
			};

			return view;
			// return base.OnCreateView(inflater, container, savedInstanceState);
		}
		public void OnCancel() {
			//throw new NotImplementedException();
		}
		public void OnError(FacebookException p0) {
			//throw new NotImplementedException();
		}

		public void OnSuccess(Java.Lang.Object p0) {
			//throw new NotImplementedException();
		}

		void GraphRequest.IGraphJSONObjectCallback.OnCompleted(JSONObject p0, GraphResponse p1) {
			throw new NotImplementedException();
		}
	}
}