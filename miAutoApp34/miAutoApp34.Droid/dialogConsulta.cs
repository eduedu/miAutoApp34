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
	public class dialogConsulta : DialogFragment {
		private static int mModoBotones;
		///modoBOTONES=0 (ok y cancelar), modoBOTONES=1 (ok y corregir)
		private static string mDatoExtra;
		private static string titulo;
		private static string mensaje;
		//public int valorRespuesta;



		//public string mensaje;
		public static dialogConsulta NewInstance(Bundle bundle, String _titulo, String _mensaje, string _datoExtra = "") {
			dialogConsulta fragment = new dialogConsulta();
			mensaje = _mensaje;
			titulo = _titulo;
			fragment.Arguments = bundle;
			//string mensaje=_mensaje;
			//mModoBotones = _modoBotones;
			mDatoExtra = _datoExtra;
			return fragment;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			// Use this to return your custom view for this Fragment
			View view = inflater.Inflate(Resource.Layout.dialogConsulta, container, false);
			//RequestWindowFeature(WindowFeatures.NoTitle);

			///REFERENCIAS A CONTROLES
			Button btnOK = view.FindViewById<Button>(Resource.Id.btnOK);
			Button btnCancelar = view.FindViewById<Button>(Resource.Id.btnCancelar);
			TextView texto1 = view.FindViewById<TextView>(Resource.Id.textView1);
			TextView texto2 = view.FindViewById<TextView>(Resource.Id.textView2);
			TextView textoConsulta = view.FindViewById<TextView>(Resource.Id.textConsulta);

			///Datos
			ISharedPreferences misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			string bloqueado = misDatos.GetString("bloqueado", "");
			string numeroWA = misDatos.GetString("numeroWA", "");
			btnCancelar.Text = "Cancelar";
			btnOK.Text = "Enviar";

			if (bloqueado.Trim() == "1") {
				titulo = "MiAuto Plan";
				mensaje = "Existe un problema con su cuenta.\nPara más información comuníquese al siguiente número:\n" + numeroWA + "\nMuchas gracias.";
				btnCancelar.Text = "Ok";
				btnOK.Visibility = ViewStates.Gone;
				textoConsulta.Visibility = ViewStates.Gone;
			}

			texto1.Text = titulo;
			texto2.Text = mensaje;

			//FUENTES
			Typeface tf = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-BOLD.TTF");
			Typeface tf2 = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-REGULAR.TTF");
			Typeface tf3 = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-BLACK.TTF");

			texto1.Typeface = tf;
			texto2.Typeface = tf2;
			btnOK.Typeface = tf2;
			btnCancelar.Typeface = tf2;
			textoConsulta.Typeface = tf2;



			//ancho dialog
			var metrics = Application.Context.Resources.DisplayMetrics;
			textoConsulta.LayoutParameters.Width = metrics.WidthPixels - 10;

			///FUNCIONES BOTONES
			btnOK.Click += delegate {
				//MODO OK/CORREGIR
				if (textoConsulta.Text.Trim() != "") {
					var progressDialog = ProgressDialog.Show(inflater.Context, "", "Procesando Solicitud...", true);
					new System.Threading.Thread(new ThreadStart(delegate {
						bool solicitudOK = solicitudesWeb.solicitud("Consulta", false, textoConsulta.Text.Trim());
						//string tmpNumeroWA = solicitudesWeb.getVariable("numeroWA");
						//Console.WriteLine("Solicituddddddd: " + solicitudOK.ToString());
						//Console.WriteLine("0");

						this.Activity.RunOnUiThread(() => {
							//Console.WriteLine("1");
							progressDialog.Hide();
							//Console.WriteLine("2");
							//Console.WriteLine("Solicitud: " + solicitudOK.ToString());
							if (solicitudOK) {
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
								dialogOKclass newFragmentContactar = dialogOKclass.NewInstance(null, "Consulta Registrada", "Un asesor se comunicará con Usted a la brevedad. \n\n¡Muchas Gracias!");
								//Add fragment
								newFragmentContactar.Show(ft, "dialogOK100");

							}
							else {
								Activity.RunOnUiThread(() => {
									Dismiss();
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