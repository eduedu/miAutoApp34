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
using System.Threading;
using Android.Graphics;
using System.IO;

namespace miAutoApp34.Droid {
	public class dialogCambiarContrasena : DialogFragment {
		private static int mModoBotones;
		///modoBOTONES=0 (ok y cancelar), modoBOTONES=1 (ok y corregir)
		private static string mDatoExtra;
		//private static string titulo;
		//private static string mensaje;
		//public int valorRespuesta;
		ISharedPreferences misDatos;


		//public string mensaje;
		public static dialogCambiarContrasena NewInstance(Bundle bundle, string _datoExtra = "") {
			dialogCambiarContrasena fragment = new dialogCambiarContrasena();
			//mensaje = _mensaje;
			//titulo = _titulo;
			fragment.Arguments = bundle;
			//string mensaje=_mensaje;
			//mModoBotones = _modoBotones;
			mDatoExtra = _datoExtra;
			return fragment;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			// Use this to return your custom view for this Fragment
			View view = inflater.Inflate(Resource.Layout.dialogCambiarContrasena, container, false);
			//RequestWindowFeature(WindowFeatures.NoTitle);

			///REFERENCIAS A CONTROLES
			Button btnContinuar = view.FindViewById<Button>(Resource.Id.btnContinuar);
			Button btnCancelar = view.FindViewById<Button>(Resource.Id.btnCancelar);
			TextView txtAnterior = view.FindViewById<TextView>(Resource.Id.txtAnterior);
			TextView txtNueva = view.FindViewById<TextView>(Resource.Id.txtNueva);
			TextView txtRepetir = view.FindViewById<TextView>(Resource.Id.txtRepetir);
			TextView textView1 = view.FindViewById<TextView>(Resource.Id.textView1);
			TextView textView2 = view.FindViewById<TextView>(Resource.Id.textView2);
			TextView textView3 = view.FindViewById<TextView>(Resource.Id.textView3);
			TextView textView4 = view.FindViewById<TextView>(Resource.Id.textView4);
			LinearLayout LinearMain = view.FindViewById<LinearLayout>(Resource.Id.LinearMain);



			//FUENTES
			Typeface tf = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-BOLD.TTF");
			Typeface tf2 = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-REGULAR.TTF");
			Typeface tf3 = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-BLACK.TTF");

			textView1.Typeface = tf;
			textView2.Typeface = tf2;
			textView3.Typeface = tf2;
			textView4.Typeface = tf2;
			btnContinuar.Typeface = tf2;
			btnCancelar.Typeface = tf2;

			btnCancelar.Text = "Cancelar";
			//btnOK.Text = "Enviar";

			//ancho dialog
			var metrics = Application.Context.Resources.DisplayMetrics;
			//textoConsulta.LayoutParameters.Width = metrics.WidthPixels - 10;
			//Console.WriteLine("ANCHO:" + metrics.WidthPixels);
			//Console.WriteLine("txtAnterior width:" + txtAnterior.LayoutParameters.Width);
			txtAnterior.LayoutParameters.Width = metrics.WidthPixels - 10;
			//LinearMain.LayoutParameters.Width = metrics.WidthPixels - 10;

			///CARGAR DATOSSSSS
			misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			string num = misDatos.GetString("num", "").ToString().Trim();
			string fid = misDatos.GetString("fid", "").ToString().Trim();

			//ENTRO CON FACEBOOK
			/*
			if (fid != "") {
				Android.App.FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
				//Remove fragment else it will crash as it is already added to backstack
				Android.App.Fragment prev = Activity.FragmentManager.FindFragmentByTag("dialogOK100");
				if (prev != null) {
					ft.Remove(prev);
				}
				ft.AddToBackStack(null);
				// Create and show the dialog.
				//dialogOKclass newFragment = dialogOKclass.NewInstance(null, "Solicitud registrada", "Un asesor se comunicará con usted en las próximas horas.");
				dialogOKclass newFragmentContactar = dialogOKclass.NewInstance(null, "Contraseña", "Su cuenta de MiAutoPlan está asociada a su cuenta de Facebook, por lo tanto no requiere contraseña para ingresar.");
				//Add fragment
				newFragmentContactar.Show(ft, "dialogOK100");
				//Dismiss();
			}
			*/
			///FUNCIONES BOTONES
			btnContinuar.Click += delegate {
				string anterior = txtAnterior.Text.ToString().Trim();
				string nueva = txtNueva.Text.ToString().Trim();
				string repetir = txtRepetir.Text.ToString().Trim();

				//controlar que los datos sean validos
				bool datosValidos = true;
				if (anterior == "" || nueva == "") {
					Toast.MakeText(inflater.Context, "Campo vacío", ToastLength.Long).Show();
					datosValidos = false;
				}
				if (nueva != repetir) {
					Toast.MakeText(inflater.Context, "No coincide la confirmación", ToastLength.Long).Show();
					datosValidos = false;
				}


				//MODO OK/CORREGIR
				if (datosValidos) {
					var progressDialog = ProgressDialog.Show(inflater.Context, "", "Procesando Solicitud...", true);
					new System.Threading.Thread(new ThreadStart(delegate {
						string solicitudOK = solicitudesWeb.setContrasena(num, anterior, nueva);
						//string tmpNumeroWA = solicitudesWeb.getVariable("numeroWA");
						//Console.WriteLine("Solicituddddddd: " + solicitudOK.ToString());
						//Console.WriteLine("0");

						this.Activity.RunOnUiThread(() => {
							//Console.WriteLine("1");
							progressDialog.Hide();
							//Console.WriteLine("2");
							//Console.WriteLine("Solicitud: " + solicitudOK.ToString());
							if (solicitudOK == "2") {
								Android.App.FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
								//Remove fragment else it will crash as it is already added to backstack
								Android.App.Fragment prev = Activity.FragmentManager.FindFragmentByTag("dialogOK100");
								if (prev != null) {
									ft.Remove(prev);
								}
								ft.AddToBackStack(null);
								// Create and show the dialog.
								//dialogOKclass newFragment = dialogOKclass.NewInstance(null, "Solicitud registrada", "Un asesor se comunicará con usted en las próximas horas.");
								dialogOKclass newFragmentContactar = dialogOKclass.NewInstance(null, "Error", "La contraseña ingresada es incorrecta.");
								//Add fragment
								newFragmentContactar.Show(ft, "dialogOK100");

							}

							if (solicitudOK == "1") {
								Dismiss();
								Android.App.FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
								//Remove fragment else it will crash as it is already added to backstack
								Android.App.Fragment prev = Activity.FragmentManager.FindFragmentByTag("dialogOK100");
								if (prev != null) {
									ft.Remove(prev);
								}
								ft.AddToBackStack(null);
								// Create and show the dialog.
								//dialogOKclass newFragment = dialogOKclass.NewInstance(null, "Solicitud registrada", "Un asesor se comunicará con usted en las próximas horas.");
								dialogOKclass newFragmentContactar = dialogOKclass.NewInstance(null, "Contraseña", "La nueva contraseña ha sido almacenada con éxito.");
								//Add fragment
								newFragmentContactar.Show(ft, "dialogOK100");

							}
							if (solicitudOK == "0") {
								Activity.RunOnUiThread(() => {
									//Dismiss();
									Toast.MakeText(inflater.Context, "sin conexión", ToastLength.Long).Show();

								});
							}
						});
					})).Start();
				}


			};

			btnCancelar.Click += delegate {
				Dismiss();
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