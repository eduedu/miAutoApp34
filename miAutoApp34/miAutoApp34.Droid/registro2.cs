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
using Android.Graphics;
using System.Net;
using System.Collections.Specialized;
using System.Threading;

namespace miAutoApp34.Droid {
	[Activity(Label = "registro2")]
	public class registro2 : Activity {
		protected override void OnCreate(Bundle savedInstanceState) {
			RequestWindowFeature(WindowFeatures.NoTitle);
			base.OnCreate(savedInstanceState);

			//DATOS
			ISharedPreferences misDatos;
			misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			string terminar = misDatos.GetString("terminar", "");
			if (terminar == "1") {
				Finish();
			}

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.registro2);

			//FUENTES
			Typeface tf = Typeface.CreateFromAsset(Assets, "fonts/ROBOTO-BOLD.TTF");
			Typeface tf2 = Typeface.CreateFromAsset(Assets, "fonts/ROBOTO-REGULAR.TTF");
			Typeface tf3 = Typeface.CreateFromAsset(Assets, "fonts/ROBOTO-BLACK.TTF");
			TextView texto0 = FindViewById<TextView>(Resource.Id.titulo);
			TextView texto1 = FindViewById<TextView>(Resource.Id.textView1);
			TextView texto2 = FindViewById<TextView>(Resource.Id.textView2);
			texto2.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();

			texto0.Typeface = tf2;
			texto1.Typeface = tf;

			string nya = Intent.GetStringExtra("nya") ?? "";
			string num = Intent.GetStringExtra("num") ?? "";
			string p = Intent.GetStringExtra("p") ?? "";
			string fid = Intent.GetStringExtra("fid") ?? "";

			///borrar +549 y espacios vacios
			num = num.Replace(" ", string.Empty);
			num = num.Replace("-", string.Empty);
			if (num.Length > 4) {
				if (num.Substring(0, 4) == "+549") {
					num = num.Substring(4, num.Length - 4);
				}
			}

			///////////////////CARGAR TERMINOS Y CONDICIONES DESDE VARIABLES
			string tmpTerminos = solicitudesWeb.getVariable("terminosYcondiciones");
			texto2.Text = tmpTerminos;

			///////////////////BOTON VOLVER
			Button btnVolver = FindViewById<Button>(Resource.Id.button2);
			btnVolver.Click += delegate {
				//StartActivity(typeof(registro2));
				var miIntent = new Intent(this, typeof(registro1));
				miIntent.AddFlags(ActivityFlags.NoAnimation);
				miIntent.PutExtra("nya", nya);
				miIntent.PutExtra("num", num);
				miIntent.PutExtra("p", p);
				miIntent.PutExtra("fid", fid);
				StartActivity(miIntent);
			};


			///////////////////BOTON ACEPTO////////////////////////////////////
			Button btnAcepto = FindViewById<Button>(Resource.Id.button1);
			btnAcepto.Click += delegate {
				//CARGAR DATOS EN BASE DE DATOS
				string formkey = "1-2dQTLKg9UQt3Rr5CCUIWAcrLQqHY86rI813sVisxpA";

				string cnumero = "entry.265687753";
				string cpass = "entry.1655807511";
				string cnya = "entry.1101937925";
				string cfid = "entry.1418263251";
				/*
string cllaves = "entry.1218626620";
string creferidos = "entry.1371131431";
string cregistrados = "entry.665792872";
string cface = "entry.431395431";
string cnotas = "entry.161301415";
string cextras = "entry.1455707682";
string cautod = "entry.818295292";
string cvencimiento = "entry.1885090780";
*/

				var keyval = new NameValueCollection();
				keyval.Add(cnumero, num);
				keyval.Add(cpass, p);
				keyval.Add(cnya, nya);
				keyval.Add(cfid, fid);
				/*
keyval.Add(cllaves, "llaves");
keyval.Add(creferidos, "refe");
keyval.Add(cregistrados, "regis");
keyval.Add(cface, "face");
keyval.Add(cnotas, "notas");
keyval.Add(cextras, "extras");
keyval.Add(cautod, "autoid");
keyval.Add(cvencimiento, "vencimiento");
*/
				System.Net.WebClient wc = new WebClient();
				Uri uri = new Uri(@"https://docs.google.com/forms/d/" + formkey + @"/formResponse");
				var progressDialog = ProgressDialog.Show(this, "", "Creando cuenta...", true);

				new Thread(new ThreadStart(delegate {
					try {
						//string resultado = "";
						/*
wc.UploadDataCompleted += (sender, e) => {
	string resultado = e.Result.ToString();
	Console.WriteLine("------------------------------------------:" + resultado);
	RunOnUiThread(() => {
		Android.App.AlertDialog.Builder builder = new AlertDialog.Builder(this);
		AlertDialog ad = builder.Create();
		ad.SetTitle("data");
		ad.SetMessage(resultado);
		//ad.SetButton("OK", (s, e) => { Console.WriteLine("OK Button clicked, alert dismissed"); });
		ad.Show();
	});
};
wc.UploadStringCompleted += (sender, e) => {
	string resultado = e.Result.ToString();
	Console.WriteLine("------------------------------------------:" + resultado);
	RunOnUiThread(() => {
		Android.App.AlertDialog.Builder builder = new AlertDialog.Builder(this);
		AlertDialog ad = builder.Create();
		ad.SetTitle("string");
		ad.SetMessage(resultado);
		//ad.SetButton("OK", (s, e) => { Console.WriteLine("OK Button clicked, alert dismissed"); });
		ad.Show();
	});
};
wc.UploadValuesCompleted += (sender, e) => {
	string resultado = e.Result.ToString();
	Console.WriteLine("------------------------------------------:"+resultado);
	RunOnUiThread(() => {
		Android.App.AlertDialog.Builder builder = new AlertDialog.Builder(this);
		AlertDialog ad = builder.Create();
		ad.SetTitle("debug");
		ad.SetMessage(resultado);
		//ad.SetButton("OK", (s, e) => { Console.WriteLine("OK Button clicked, alert dismissed"); });
		ad.Show();
	});
	RunOnUiThread(() => Toast.MakeText(this, resultado, ToastLength.Long).Show());
};
*/
						//wc.UploadValuesAsync(uri, "POST", keyval, Guid.NewGuid().ToString());
						wc.UploadValues(uri, keyval);

						//ISharedPreferences 
						misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
						ISharedPreferencesEditor cargarDatos = misDatos.Edit();
						cargarDatos.PutString("num", num);
						//cargarDatos.PutString("p", p);
						cargarDatos.PutString("nya", nya);
						cargarDatos.PutString("fid", fid);
						cargarDatos.PutString("terminar", "1");
						cargarDatos.Apply();

						///// ///////////CARGAR SOLICITUD//////////////////
						bool solicitudOK = solicitudesWeb.solicitud("Nuevo Usuario");

						RunOnUiThread(() => progressDialog.Hide());
						//cargarDatos.Commit();
						//StartActivity(typeof(mainFragment));
						//Finish();

						var miIntent = new Intent(this, typeof(mainFragment));
						//miIntent.AddFlags(ActivityFlags.NoAnimation);
						//miIntent.AddFlags(ActivityFlags.ClearTop|ActivityFlags.NewTask);
						//miIntent.PutExtra("nya", e.mProfile.Name);
						//miIntent.PutExtra("fid", e.mProfile.Id);
						StartActivity(miIntent);
						Finish();
						//FinishAndRemoveTask();

					}
					catch (WebException ex) {
						Console.WriteLine(ex.Message);
						RunOnUiThread(() => Toast.MakeText(this, "sin conexión", ToastLength.Long).Show());
						RunOnUiThread(() => progressDialog.Hide());
						return;

					}
				})).Start();
			};
		}
		protected override void OnStart() {
			base.OnStart();
		}
	}
}