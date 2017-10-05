using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Threading;
using System.Net.Http;
using System.Collections.Generic;
using Android.Telephony;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Java.Lang;
using Android.Content.PM;
using Java.Security;
using System.Threading.Tasks;

namespace miAutoApp34.Droid {
	[Activity(Icon = "@drawable/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
	public class login1 : Activity, IFacebookCallback {
		//VIEWS Globales
		Button btnOlvidoContrasena;
		EditText campo1;
		EditText campo2;

		//DATOS
		ISharedPreferences misDatos;

		//Para FACEBOOK
		private ICallbackManager mCallbackManager;
		MyProfileTracker mProfileTracker;

		protected override void OnCreate(Bundle bundle) {
			RequestWindowFeature(WindowFeatures.NoTitle);
			base.OnCreate(bundle);

			///Para FACEBOOK
			FacebookSdk.SdkInitialize(this.ApplicationContext);
			mProfileTracker = new MyProfileTracker();
			mProfileTracker.mOnProfileChanged += mProfileTracker_mOnProfileChanged;
			mProfileTracker.StartTracking();
			mCallbackManager = CallbackManagerFactory.Create();
			LoginManager.Instance.RegisterCallback(mCallbackManager, this);
			if (AccessToken.CurrentAccessToken != null) {
				Console.WriteLine("ACCESStokennnnnnnnnnnnnnnn: " + AccessToken.CurrentAccessToken.UserId);
			}
			else {
				Console.WriteLine("SINNNNNNNNNNNNNNNNNNNNNN   ACCESStoken ");
			}



			////KEYAHASH//////////////////
			/*
            PackageInfo info = this.PackageManager.GetPackageInfo("com.gmail.educontratodos.miautoapptest34", PackageInfoFlags.Signatures);
            foreach (Android.Content.PM.Signature signature in info.Signatures) {
                MessageDigest md = MessageDigest.GetInstance("SHA");
                md.Update(signature.ToByteArray());

                string keyhash = Convert.ToBase64String(md.Digest());
                //NO SIRVE:Console.WriteLine("KeyHash:", keyhash);
                Console.WriteLine("KeyHash:" + keyhash);
            }
            */
			//////////////////////////////

			///DATOSSSSSSSSSSSSS
			misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			string num = misDatos.GetString("num", "");

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.login1);

			//VIEWS
			TextView miTexto1 = FindViewById<TextView>(Resource.Id.textView1);
			campo1 = FindViewById<EditText>(Resource.Id.editText1);
			campo2 = FindViewById<EditText>(Resource.Id.editText2);
            //ImageButton btnFacebook = FindViewById<ImageButton>(Resource.Id.btnFacebook);
            LinearLayout btnFacebook = FindViewById<LinearLayout>(Resource.Id.btnFacebook);
            Button btnRegistrarse = FindViewById<Button>(Resource.Id.btnRegistrarse);
			Button btnIngresar = FindViewById<Button>(Resource.Id.btnIngresar);
			btnOlvidoContrasena = FindViewById<Button>(Resource.Id.btnOlvidoContrasena);



			//FUENTES
			Typeface tf = Typeface.CreateFromAsset(Assets, "fonts/ROBOTO-BOLD.TTF");
			Typeface tf2 = Typeface.CreateFromAsset(Assets, "fonts/ROBOTOCONDENSED-BOLD.TTF");
			Typeface tf3 = Typeface.CreateFromAsset(Assets, "fonts/ROBOTO-BLACK.TTF");

			miTexto1.Typeface = tf;
			campo1.Typeface = tf3;
			campo2.Typeface = tf3;
			btnRegistrarse.Typeface = tf;
			btnIngresar.Typeface = tf;
			btnOlvidoContrasena.Typeface = tf3;

			///
			//campo1.Text = us;

			//LEER el NRO DEL EQUIPO
			var miNumero = "";
			//ERROR ANDROID 6 MARSHMALLOW:

			TelephonyManager telManager;
			telManager = (TelephonyManager)GetSystemService(TelephonyService);
			miNumero = telManager.Line1Number;

			//Console.WriteLine("NNNNNNNNNNNNumero:"+ miNumero);
			campo1.Text = miNumero;
			////////////////////////////////////////////////////////////////
			if (AccessToken.CurrentAccessToken != null) {
				//The user is logged in through Facebook
				LoginManager.Instance.LogOut();
				//faceBookButton.Text = "My Facebook login button";
			}
			////////////////////////////////////////////////BOTON FACEBOOK///////////////////////////////////////////////////
			btnFacebook.Click += (o, e) => {
				List<string> permisos = new List<string>(new string[] { "public_profile" });
				LoginManager.Instance.LogInWithReadPermissions(this, permisos);
				//Console.WriteLine("ACCESSSSS: "+AccessToken.CurrentAccessToken.UserId);
				///ir al evento mProfileTracker_mOnProfileChanged

			};

			////////////////////////////////////////////////BOTON OLVIDO CONTRASEÑA///////////////////////////////////////////////////
			btnOlvidoContrasena.Click += delegate {
				//LoginManager.Instance.LogOut();
				//Console.WriteLine("CHAUUUUUUUU " + Profile.CurrentProfile.FirstName);
				//List<string> permisos = new List<string>(new string[] { "public_profile", "user_friends" });
				//LoginManager.Instance.LogInWithReadPermissions(this, permisos);

				//controlar que el campo numero no este vacio, si esta, avisar
				if (campo1.Text == "" || campo1.Text == null) {
					Android.App.FragmentTransaction ft = this.FragmentManager.BeginTransaction();
					//Remove fragment else it will crash as it is already added to backstack
					Android.App.Fragment prev = this.FragmentManager.FindFragmentByTag("dialog");
					if (prev != null) {
						ft.Remove(prev);
					}
					ft.AddToBackStack(null);
					// Create and show the dialog.
					dialogOKclass newFragment = dialogOKclass.NewInstance(null, "Ingrese su \nNúmero de Teléfono", "Complete el campo: \nNUMERO DE TELÉFONO \ny un asesor se comunicará con Usted a la brevedad.");
					//Add fragment
					newFragment.Show(ft, "dialog");
				}
				else {
					variablesGlobales.numeroTemp = campo1.Text.Trim();
					//preguntar si el numero es correcto
					Android.App.FragmentTransaction ft = this.FragmentManager.BeginTransaction();
					//Remove fragment else it will crash as it is already added to backstack
					Android.App.Fragment prev = this.FragmentManager.FindFragmentByTag("dialogOkCorregir");
					if (prev != null) {
						ft.Remove(prev);
					}
					ft.AddToBackStack(null);
					// Create and show the dialog.
					dialogOKCancelarclass newFragmentCorregir = dialogOKCancelarclass.NewInstance(bundle, "¿Número Correcto?\n" + variablesGlobales.numeroTemp, "Presione OK para confirmar, o CORREGIR para cambiar el número ingresado.", 1);
					newFragmentCorregir.Show(ft, "dialogOkCorregir");
					//Add fragment


					////dar la opcion de mandar un wassap



				}
			};

			////////////////////////////////////////////////BOTON REGISTRARSE///////////////////////////////////////////////////
			btnRegistrarse.Click += delegate {
				var miIntent = new Intent(this, typeof(registro1));
				miIntent.AddFlags(ActivityFlags.NoAnimation);
				miIntent.PutExtra("num", campo1.Text.Trim());
				miIntent.PutExtra("fid", "");
				StartActivity(miIntent);
				//StartActivity(typeof(registro1));
			};

			////////////////////////////////////////////////BOTON INGRESAR///////////////////////////////////////////////////
			btnIngresar.Click += delegate {
				//VERIFICAR QUE EL CAMPO CONTRASENA NO ESTE VACIO
				var dato1 = campo1.Text;
				var dato2 = campo2.Text;
				if ((dato1 == "") || (dato1 == null)) {
					Toast.MakeText(this, "Ingresar Número de Teléfono", ToastLength.Long).Show();
				}
				else if ((dato2 == "") || (dato2 == null)) {
					Toast.MakeText(this, "Ingresar contraseña", ToastLength.Long).Show();
				}
				else {
					//var dato1 = campo1.Text;
					var progressDialog = ProgressDialog.Show(this, "", "Verificando datos...", true);
					new System.Threading.Thread(new ThreadStart(delegate {
						string uriStr = "https://script.google.com/macros/s/AKfycbxB98IS32T9mCUJbSccWmBg17LMRGmcvB7Kqa9lFcM_8eiM6rE/exec?action=getUrlCount&url={0}";
						try {
							var request = HttpWebRequest.Create(string.Format(@uriStr, dato1));
							request.ContentType = "application /json";
							request.Method = "GET";
							HttpWebResponse response = request.GetResponse() as HttpWebResponse; //) {
							if (response.StatusCode != HttpStatusCode.OK) {
								Console.Out.WriteLine("ErrorRRRRRRRRRRRR fetching data. Server returned status code: {0}", response.StatusCode);
							}
							using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
								var content = reader.ReadToEnd();
								if (string.IsNullOrWhiteSpace(content)) {
									//Console.Out.WriteLine("XXXXXXXXXXXXXXXXXXResponse contained empty body...");
								}
								else {
									//Console.Out.WriteLine("xxxxxxxxxxxxxResponse Body: \r\n {0}", content);
								}
								//RunOnUiThread(() => Toast.MakeText(this, content, ToastLength.Long).Show());
								bool cuentaInexistente = false;
								if (content == "-1") {
									//RunOnUiThread(() => Toast.MakeText(this, content, ToastLength.Long).Show());
									Console.WriteLine("Cuenta Inexistente");
									cuentaInexistente = true;
									RunOnUiThread(() => {
										FragmentTransaction ft = FragmentManager.BeginTransaction();
										//Remove fragment else it will crash as it is already added to backstack
										Fragment prev = FragmentManager.FindFragmentByTag("dialog");
										if (prev != null) {
											ft.Remove(prev);
										}
										ft.AddToBackStack(null);
										// Create and show the dialog.
										dialogOKclass newFragment = dialogOKclass.NewInstance(null, "Cuenta Inexistente", "Verifique el número ingresado");
										//Add fragment
										newFragment.Show(ft, "dialog");
									});
								}
								else {
									Char separador = '[';
									string[] contents = content.Split(separador);
									if ((contents[0] != dato2) & (!cuentaInexistente)) {
										Console.WriteLine("Contraseña Incorrecta");
										FragmentTransaction ft = FragmentManager.BeginTransaction();
										//Remove fragment else it will crash as it is already added to backstack
										Fragment prev = FragmentManager.FindFragmentByTag("dialog");
										if (prev != null) {
											ft.Remove(prev);
										}
										ft.AddToBackStack(null);
										// Create and show the dialog.
										dialogOKclass newFragment = dialogOKclass.NewInstance(null, "Contraseña incorrecta", "Por favor, vuelva a intentarlo");
										//Add fragment
										newFragment.Show(ft, "dialog");
									}
									///////////DATOS CORRECTOS
									if (contents[0] == dato2) {
										Console.WriteLine("CORRECTO");
										ISharedPreferencesEditor cargarDatos = misDatos.Edit();
										cargarDatos.PutString("num", campo1.Text.Trim());
										cargarDatos.PutString("p", campo2.Text.Trim());
										cargarDatos.PutString("nya", contents[1]);
										cargarDatos.PutString("miauto_id", contents[2]);
										if (contents[2] != "") {
											cargarDatos.PutString("datoConAuto", contents[2]);
										}
										cargarDatos.Apply();
										StartActivity(typeof(mainFragment));
										Finish();
									}
								}
								//Assert.NotNull(content);
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

			// ///////////////////////// TIENE Q ESTAR ACA LO DE CLICKABLE??
			////btnRegistrarse.Clickable = false;
			//btnIngresar.Clickable = false;
			//EDICION CAMPOS///////////////////////////////////////////////////////////////
			campo1.KeyPress += (object sender, View.KeyEventArgs e) => {
				e.Handled = false;
				if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter) {
					e.Handled = true;
				}
				/*
				if (campo1.Text == "" ) {
					//btnRegistrarse.SetBackgroundResource(Resource.Drawable.btnLoginGris);
					//btnRegistrarse.Clickable = false;
					btnIngresar.SetBackgroundResource(Resource.Drawable.btnLoginGris);
					btnIngresar.Clickable = false;
				}
				else {
					//btnRegistrarse.SetBackgroundResource(Resource.Drawable.btnLoginAzul);
					//btnRegistrarse.Clickable = true;
					btnIngresar.SetBackgroundResource(Resource.Drawable.btnLoginAzul);
					btnIngresar.Clickable = true;
				}
				*/
			};
			campo2.KeyPress += (object sender2, View.KeyEventArgs e2) => {
				e2.Handled = false;
			};
		}
		//////// ////////////////////////////// PROFILE CHANGED FACEBOOK//////////////////////////////////
		void mProfileTracker_mOnProfileChanged(object sender, OnProfileChangedEventArgs e) {
			Console.WriteLine("mOnProfileChangedddddddddddddddddddddddddddddddddddddddddddddddddddd1");
			if (e.mProfile != null) {
				try {
					//btnOlvidoContrasena.Text = e.mProfile.FirstName;
					Console.WriteLine("----------------------------");
					Console.WriteLine(e.mProfile.FirstName);
					Console.WriteLine(e.mProfile.Id);
					Console.WriteLine(e.mProfile.LastName);
					Console.WriteLine(e.mProfile.LinkUri);
					Console.WriteLine(e.mProfile.Name);
					Console.WriteLine(AccessToken.CurrentAccessToken.UserId);
					Console.WriteLine("----------------------------");

					////////ENTRAR CON FACEBOOK:
					//verificar si ya esta asociado
					var faceid = e.mProfile.Id;
					var progressDialog = ProgressDialog.Show(this, "", "Verificando cuenta...", true);
					new System.Threading.Thread(new ThreadStart(delegate {
						string uriStr = "https://script.google.com/macros/s/AKfycbxB98IS32T9mCUJbSccWmBg17LMRGmcvB7Kqa9lFcM_8eiM6rE/exec?action=getFBid&url={0}";
						try {
							var request = HttpWebRequest.Create(string.Format(@uriStr, faceid));
							request.ContentType = "application /json";
							request.Method = "GET";
							HttpWebResponse response = request.GetResponse() as HttpWebResponse; //) {
							if (response.StatusCode != HttpStatusCode.OK) {
								//Console.Out.WriteLine("ErrorRRRRRRRRRRRR fetching data. Server returned status code: {0}", response.StatusCode);
							}
							using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
								var content = reader.ReadToEnd();
								if (string.IsNullOrWhiteSpace(content)) {
									//Console.Out.WriteLine("XXXXXXXXXXXXXXXXXXResponse contained empty body...");
								}
								else {
									Console.Out.WriteLine("---------------------------------------------------------------------------------");
									Console.Out.WriteLine("FAAAACEEEEResponse Body: \r\n {0}", content);
								}
								//RunOnUiThread(() => Toast.MakeText(this, content, ToastLength.Long).Show());
								bool cuentaInexistente = false;
								/////////////SI NO ESTA ASOCIADA LA CUENTA, ir a REGISTRO1 con los datos de FACEBOOK
								if (content == "-1") {
									Console.Out.WriteLine("CUENTA FACEBOOK INEXISTENTE");
									RunOnUiThread(() => Toast.MakeText(this, "Cuenta de Facebook NO asociada", ToastLength.Long).Show());
									cuentaInexistente = true;

									var miIntent = new Intent(this, typeof(registro1));
									miIntent.AddFlags(ActivityFlags.NoAnimation);
									//miIntent.AddFlags(ActivityFlags.ClearTask);
									miIntent.PutExtra("nya", e.mProfile.Name);
									miIntent.PutExtra("fid", e.mProfile.Id);
									StartActivity(miIntent);
								}
								else {
									Char separador = '[';
									string[] contents = content.Split(separador);
									///////SI YA EXISTE LA CUENTA DE FACEBOOK, ir a MAINFRAGMENT
									//Console.WriteLine("CORRECTO");
									Console.Out.WriteLine("CUENTA FACEBOOK EXISTENTE");
									ISharedPreferencesEditor cargarDatos = misDatos.Edit();
									cargarDatos.PutString("num", contents[0]);
									cargarDatos.PutString("p", "");
									cargarDatos.PutString("nya", contents[1]);
									cargarDatos.PutString("fid", e.mProfile.Id);
									cargarDatos.PutString("miauto_id", contents[2]);
									if (contents[2] != "") {
										cargarDatos.PutString("datoConAuto", contents[2]);
									}
									cargarDatos.Apply();
									StartActivity(typeof(mainFragment));
									Finish();
								}
								//Assert.NotNull(content);
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

				catch (System.Exception ex) {
					//Handle error
				}
			}
		}

		public void OnCancel() {
			//throw new NotImplementedException();
		}

		public void OnError(FacebookException p0) {
			//throw new NotImplementedException();
		}

		public void OnSuccess(Java.Lang.Object result) {
			//throw new NotImplementedException();
			LoginResult loginResult = result as LoginResult;
			Console.WriteLine("LOGGGGGGINNNNNNNNNNN RESULTTTTTTTTTTTTTTTTTTTTT");
			Console.WriteLine("ACCESStokenLOGIN: " + loginResult.AccessToken.UserId);
		}

		protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data) {
			//Console.WriteLine("ONACTIVITY RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRESULT");
			base.OnActivityResult(requestCode, resultCode, data);
			mCallbackManager.OnActivityResult(requestCode, (int)resultCode, data);
		}
		protected override void OnDestroy() {
			//mProfileTracker.StopTracking();
			base.OnDestroy();
		}
	}

	public class MyProfileTracker : ProfileTracker {
		public event EventHandler<OnProfileChangedEventArgs> mOnProfileChanged;

		protected override void OnCurrentProfileChanged(Profile oldProfile, Profile newProfile) {
			if (mOnProfileChanged != null) {
				mOnProfileChanged.Invoke(this, new OnProfileChangedEventArgs(newProfile));
			}
		}
	}

	public class OnProfileChangedEventArgs : EventArgs {
		public Profile mProfile;

		public OnProfileChangedEventArgs(Profile profile) { mProfile = profile; }
	}

}


