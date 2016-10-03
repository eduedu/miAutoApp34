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
using System.Timers;
using System.Threading;
//using System.Threading;

namespace miAutoApp34.Droid {
	[Activity(MainLauncher = true, NoHistory = true, Theme = "@style/Theme.Splash", Icon = "@drawable/icon")]
	public class splash : Activity {
		protected override void OnCreate(Bundle savedInstanceState) {
			RequestWindowFeature(WindowFeatures.NoTitle);
			base.OnCreate(savedInstanceState);


			///VERIFICAR SI YA ESTA LOGUEADO
			ISharedPreferences misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			string num = misDatos.GetString("num", "");
			if (num == "") {
				StartActivity(typeof(login1));
			}
			else {
				///borrar esto:
				/*
				ISharedPreferencesEditor cargarDatos = misDatos.Edit();
				cargarDatos.PutString("datoConAuto", "");
				cargarDatos.PutString("tempTab2", "");
				cargarDatos.Apply();
				*/
				///
				StartActivity(typeof(mainFragment));
			}

			//SetContentView(Resource.Layout.splash);


			//Timer timer = new Timer();
			//timer.Interval = 1; // 3 sec.
			//timer.AutoReset = false; // Do not reset the timer after it's elapsed

			//			var progressDialog = ProgressDialog.Show(this, "Espere", "Cargando...", true);
			//			new Thread(new ThreadStart(delegate {

			//timer.Elapsed += (object sender, ElapsedEventArgs e) =>

			//            {
			//                StartActivity(typeof(login1));
			//RunOnUiThread(() => progressDialog.Hide());
			//			};
			//timer.Start();
			//			})).Start();
			//progressDialog.Hide();
		}
	}
}