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
using System.Threading;

namespace miAutoApp34.Droid {
	public class dialogPedirAuto : DialogFragment {
		//private static string titulo;
		//private static string mensaje;
		private static string requeremientos;
		private static string AcercaDe;
		//public int valorRespuesta;


		//public string mensaje;
		public static dialogPedirAuto NewInstance(Bundle bundle, string _requeremientos, string _acercaDe = "") {
			dialogPedirAuto fragment = new dialogPedirAuto();
			//mensaje = _mensaje;
			//titulo = _titulo;
			requeremientos = _requeremientos;
			AcercaDe = _acercaDe;
			fragment.Arguments = bundle;
			//string mensaje=_mensaje;
			return fragment;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			// Use this to return your custom view for this Fragment
			View view = inflater.Inflate(Resource.Layout.dialogPedirAuto, container, false);
			//RequestWindowFeature(WindowFeatures.NoTitle);


			///REFERENCIAS A CONTROLES
			Button btnOK = view.FindViewById<Button>(Resource.Id.btnOK);
			Button btnCancelar = view.FindViewById<Button>(Resource.Id.btnCancelar);

			TextView texto1 = view.FindViewById<TextView>(Resource.Id.textView1);
			TextView texto2 = view.FindViewById<TextView>(Resource.Id.textView2);
			texto2.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
			//texto1.Text = titulo;
			if (AcercaDe == "") {
				texto1.Text = "Requerimientos";
			}
			if (AcercaDe == "AcercaDe") {
				texto1.Text = "Sobre MiAuto Plan";
				btnOK.Visibility = ViewStates.Gone;
				btnCancelar.Text = "Ok";
			}
			texto2.Text = requeremientos;
			

			//FUENTES
			Typeface tf = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-BOLD.TTF");
			Typeface tf2 = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-REGULAR.TTF");
			Typeface tf3 = Typeface.CreateFromAsset(Activity.Assets, "fonts/ROBOTO-BLACK.TTF");

			texto1.Typeface = tf;
			texto2.Typeface = tf2;
			btnOK.Typeface = tf2;
			btnCancelar.Typeface = tf2;

			//ajustar tamaño
			var metrics = inflater.Context.Resources.DisplayMetrics;
			int tmpAncho = metrics.WidthPixels;
			int tmpAlto = metrics.HeightPixels;
			if (tmpAncho > 700) {
				//texto2.SetMaxLines(10);

				texto2.TextSize = tmpAncho / 40;
				//texto2.SetLines(15);
				texto2.LayoutParameters.Width = (int)(tmpAncho * 0.8);
				texto2.LayoutParameters.Height = (int)(tmpAlto * 0.55);
				//texto2.SetTextSize(TypedValue.ComplexToDimension., 35);
			}
			Console.WriteLine("Ancho:" + metrics.WidthPixels.ToString());
			Console.WriteLine("Fuente:" + texto2.TextSize.ToString());

			///FUNCIONES BOTONES
			btnCancelar.Click += delegate {
				Dismiss();
			};
			btnOK.Click += delegate {
				var progressDialog = ProgressDialog.Show(inflater.Context, "", "Procesando Solicitud...", true);
				new System.Threading.Thread(new ThreadStart(delegate {
					bool solicitudOK = solicitudesWeb.solicitud("Pedir Auto", false, "miauto_titulo");
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
							dialogOKclass newFragmentContactar = dialogOKclass.NewInstance(null, "Solicitud Registrada", "Un asesor se comunicará con Usted a la brevedad. \n\n¡Muchas Gracias!");
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