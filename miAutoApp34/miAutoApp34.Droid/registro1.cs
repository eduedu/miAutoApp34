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
using System.Threading;
using System.Net;
using System.IO;
using Android.Telephony;

namespace miAutoApp34.Droid {
	[Activity(Label = "registro1")]
	public class registro1 : Activity {
		protected override void OnCreate(Bundle savedInstanceState) {
			RequestWindowFeature(WindowFeatures.NoTitle);
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.registro1);

			////DATOSSSSSSS
			ISharedPreferences misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			//string us = misDatos.GetString("u", "");
			string nya = Intent.GetStringExtra("nya") ?? "";
			string num = Intent.GetStringExtra("num") ?? "";
			string p = Intent.GetStringExtra("p") ?? "";
			string fid = Intent.GetStringExtra("fid") ?? "";

			string terminar = misDatos.GetString("terminar", "");
			if (terminar == "1") {
				Finish();
			}

			//CONTROLES 
			Button btnContinuar = FindViewById<Button>(Resource.Id.button1);
			Button btnCancelar = FindViewById<Button>(Resource.Id.button2);
			TextView texto0 = FindViewById<TextView>(Resource.Id.titulo);
			TextView texto1 = FindViewById<TextView>(Resource.Id.textView1);
			TextView texto2 = FindViewById<TextView>(Resource.Id.textView2);
			TextView texto3 = FindViewById<TextView>(Resource.Id.textView3);
			TextView texto4 = FindViewById<TextView>(Resource.Id.textView4);
			TextView texto5 = FindViewById<TextView>(Resource.Id.textView5);
			//CAMPOS
			EditText campo1 = FindViewById<EditText>(Resource.Id.editText1);
			EditText campo2 = FindViewById<EditText>(Resource.Id.editText2);
			EditText campo3 = FindViewById<EditText>(Resource.Id.editText3);
			EditText campo4 = FindViewById<EditText>(Resource.Id.editText4);

			//FUENTES
			Typeface tf = Typeface.CreateFromAsset(Assets, "fonts/ROBOTO-BOLD.TTF");
			Typeface tf2 = Typeface.CreateFromAsset(Assets, "fonts/ROBOTO-REGULAR.TTF");
			Typeface tf3 = Typeface.CreateFromAsset(Assets, "fonts/ROBOTO-BLACK.TTF");

			texto0.Typeface = tf2;
			texto1.Typeface = tf;
			texto2.Typeface = tf2;
			texto3.Typeface = tf2;
			texto4.Typeface = tf2;
			texto5.Typeface = tf2;

			campo1.Typeface = tf3;
			campo2.Typeface = tf3;
			campo3.Typeface = tf3;
			campo4.Typeface = tf3;

			/// ////// CONTROL DE FOCOS//////////////////
			/*
			campo1.FocusChange += (s, o) => {

				if (o.HasFocus) {
					texto1.SetTextColor(Android.Graphics.Color.Blue);
				} else {
					texto1.SetTextColor(Android.Graphics.Color.Gray);
				}
			};
			*/

			////SI SE LOGEO CON FACE, NO IMPORTAN LOS CAMPOS DE PASSWORD
			Boolean loginConFace = false;
			if (fid != "") {
				loginConFace = true;
				//campoVacio = false;
				campo3.Visibility = ViewStates.Invisible;
				campo4.Visibility = ViewStates.Invisible;
				texto4.Visibility = ViewStates.Invisible;
				texto5.Visibility = ViewStates.Invisible;
			}

			//LEER el NRO DEL EQUIPO
			TelephonyManager telManager;
			telManager = (TelephonyManager)GetSystemService(TelephonyService);
			var miNumero = telManager.Line1Number;
			//Console.WriteLine("NNNNNNNNNNNNumero:"+ miNumero);
			//campo1.Text = miNumero;

			campo1.Text = nya;
			campo2.Text = miNumero;

			///////////////////////////////////BOTON CANCELAR////////////////////
			btnCancelar.Click += delegate {
				//base.OnBackPressed();
				var miIntent = new Intent(this, typeof(login1));
				/*
				miIntent.AddFlags(ActivityFlags.NoAnimation);
				miIntent.PutExtra("nya", campo1.Text.Trim());
				miIntent.PutExtra("num", campo2.Text.Trim());
				miIntent.PutExtra("p", campo3.Text.Trim());
				miIntent.PutExtra("fid", fid);
				*/
				StartActivity(miIntent);
			};


			///////////////////////////////////BOTON CONTINUAR////////////////////
			btnContinuar.Click += delegate {
				var campoVacio = false;
				if (campo1.Text == "" || campo1.Text == null) {
					Toast.MakeText(this, "Ingrese su NOMBRE", ToastLength.Long).Show();
					campoVacio = true;
				}

				if (campo2.Text == "" || campo2.Text == null) {
					Toast.MakeText(this, "Ingrese su NÚMERO", ToastLength.Long).Show();
					campoVacio = true;
				}
				if (!loginConFace) {
					if (campo3.Text == "" || campo3.Text == null) {
						Toast.MakeText(this, "Ingrese la CONTRASEÑA", ToastLength.Long).Show();
						campoVacio = true;
					}
					if (campo4.Text != campo3.Text) {
						Toast.MakeText(this, "La confirmación de CONTRASEÑA NO COINCIDE", ToastLength.Long).Show();
						campoVacio = true;
					}
				}

				if (!campoVacio) {
					var nuevoNumero = campo2.Text.Trim();
					var progressDialog = ProgressDialog.Show(this, "", "Verificando datos...", true);
					new Thread(new ThreadStart(delegate {
						string uriStr = "https://script.google.com/macros/s/AKfycbxB98IS32T9mCUJbSccWmBg17LMRGmcvB7Kqa9lFcM_8eiM6rE/exec?action=getUrlCount&url={0}";
						/////"https://script.google.com/macros/s/AKfycbzqI79YB05eq7fCIwWIlWeo2Z3cS3ELD84LWvOgDP9H9XnvwUx6/exec?action=getVariable&url=urlPromo"
						try {
							var request = HttpWebRequest.Create(string.Format(@uriStr, nuevoNumero));
							request.ContentType = "application /json";
							request.Method = "GET";
							HttpWebResponse response = request.GetResponse() as HttpWebResponse;
							if (response.StatusCode != HttpStatusCode.OK) {
								//ERROR DE CONEXION
								Console.Out.WriteLine("ErrorRRRRRRRRRRRR fetching data. Server returned status code: {0}", response.StatusCode);
							}
							using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
								var respuesta = reader.ReadToEnd();
								if (string.IsNullOrWhiteSpace(respuesta)) {
									//RESPUESTA VACIA
									//Console.Out.WriteLine("XXXXXXXXXXXXXXXXXXResponse contained empty body...");
								}
								else {
									//Console.Out.WriteLine("xxxxxxxxxxxxxResponse Body: \r\n {0}", content);
								}
								//bool cuentaInexistente = false;
								if (respuesta != "-1") {
									//YA EXISTE LA CUENTA
									//cuentaInexistente = true;
									RunOnUiThread(() => {
										FragmentTransaction ft = FragmentManager.BeginTransaction();
										//Remove fragment else it will crash as it is already added to backstack
										Fragment prev = FragmentManager.FindFragmentByTag("dialog");
										if (prev != null) {
											ft.Remove(prev);
										}
										ft.AddToBackStack(null);
										// Create and show the dialog.
										dialogOKclass newFragment = dialogOKclass.NewInstance(null, "Número Registrado", "El número ingresado ya se encuentra en nuestra Base de Datos. Por favor, compruebe que los datos ingresados sean correctos o comuníquese con un representante.");
										//Add fragment
										newFragment.Show(ft, "dialog");

									});
								}
								else {
									//NO EXISTE LA CUENTA -> CREARLA
									RunOnUiThread(() => {
										//CARGAR DATOS EN REGISTRO
										/*
ISharedPreferencesEditor cargarDatos = misDatos.Edit();
cargarDatos.PutString("nya", campo1.Text.Trim());
cargarDatos.PutString("u", campo2.Text.Trim());
cargarDatos.PutString("p", campo3.Text.Trim());
cargarDatos.Apply();
//cargarDatos.Commit();
*/

										//////PASAR DATOS Y CARGAR PROXIMA ACTIVITY
										//StartActivity(typeof(registro2));
										var miIntent = new Intent(this, typeof(registro2));
										miIntent.AddFlags(ActivityFlags.NoAnimation);
										miIntent.PutExtra("nya", campo1.Text.Trim());
										miIntent.PutExtra("num", campo2.Text.Trim());
										miIntent.PutExtra("p", campo3.Text.Trim());
										miIntent.PutExtra("fid", fid);
										StartActivity(miIntent);
									});
								}
							}
							RunOnUiThread(() => progressDialog.Hide());
						}
						catch (WebException ex) {
							Console.WriteLine("*********************************");
							Console.WriteLine(ex.Message);
							RunOnUiThread(() => Toast.MakeText(this, "sin conexión", ToastLength.Long).Show());
							RunOnUiThread(() => progressDialog.Hide());
						}
					})).Start();
				}
			};
		}
		protected override void OnStart() {
			base.OnStart();
			string nya = Intent.GetStringExtra("nya") ?? "";
			string num = Intent.GetStringExtra("num") ?? "";
			string p = Intent.GetStringExtra("p") ?? "";
			string fid = Intent.GetStringExtra("fid") ?? "";
			EditText campo1 = FindViewById<EditText>(Resource.Id.editText1);
			EditText campo2 = FindViewById<EditText>(Resource.Id.editText2);
			campo1.Text = nya;
			campo2.Text = num;

			ISharedPreferences misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			string terminar = misDatos.GetString("terminar", "");
			if (terminar == "1") {
				FinishAffinity();
				//return;
				//Finish();
			}
		}
	}

}