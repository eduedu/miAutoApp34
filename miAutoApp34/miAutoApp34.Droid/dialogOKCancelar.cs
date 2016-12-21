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
	public class dialogOKCancelarclass : DialogFragment {
		private static int mModoBotones;
		///modoBOTONES=0 (ok y cancelar), modoBOTONES=1 (ok y corregir)
		private static string mDatoExtra;
		private static string titulo;
		private static string mensaje;
		//public int valorRespuesta;



		//public string mensaje;
		public static dialogOKCancelarclass NewInstance(Bundle bundle, String _titulo, String _mensaje, int _modoBotones = 0, string _datoExtra = "") {
			dialogOKCancelarclass fragment = new dialogOKCancelarclass();
			mensaje = _mensaje;
			titulo = _titulo;
			fragment.Arguments = bundle;
			//string mensaje=_mensaje;
			mModoBotones = _modoBotones;
			mDatoExtra = _datoExtra;
			return fragment;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			// Use this to return your custom view for this Fragment
			View view = inflater.Inflate(Resource.Layout.dialogOKCancelar, container, false);
			//RequestWindowFeature(WindowFeatures.NoTitle);

			///REFERENCIAS A CONTROLES
			Button btnOK = view.FindViewById<Button>(Resource.Id.btnOK);
			Button btnCancelar = view.FindViewById<Button>(Resource.Id.btnCancelar);
			TextView texto1 = view.FindViewById<TextView>(Resource.Id.textView1);
			TextView texto2 = view.FindViewById<TextView>(Resource.Id.textView2);
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

			//MODO OK/CORREGIR
			if (mModoBotones == 1) {
				btnCancelar.Text = "Corregir";
			}
			if (mModoBotones == 2) {
				btnCancelar.Text = "No";
			}

			///FUNCIONES BOTONES
			btnOK.Click += delegate {
				//MODO OK/CORREGIR
				if (mModoBotones == 1) {
					var progressDialog = ProgressDialog.Show(inflater.Context, "", "Procesando Solicitud...", true);
					new System.Threading.Thread(new ThreadStart(delegate {
						bool solicitudOK = solicitudesWeb.solicitud("Contraseña", true);
						//string tmpNumeroWA = solicitudesWeb.getVariable("numeroWA");
						Console.WriteLine("Solicituddddddd: " + solicitudOK.ToString());
						Console.WriteLine("0");

						this.Activity.RunOnUiThread(() => {
							Console.WriteLine("1");
							progressDialog.Hide();
							Console.WriteLine("2");
							Console.WriteLine("Solicitud: " + solicitudOK.ToString());
							if (solicitudOK) {
								Dismiss();
								Android.App.FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
								//Remove fragment else it will crash as it is already added to backstack
								Android.App.Fragment prev = Activity.FragmentManager.FindFragmentByTag("dialogContactar1");
								if (prev != null) {
									ft.Remove(prev);
								}
								ft.AddToBackStack(null);
								// Create and show the dialog.
								//dialogOKclass newFragment = dialogOKclass.NewInstance(null, "Solicitud registrada", "Un asesor se comunicará con usted en las próximas horas.");
								dialogOKcontactar newFragmentContactar = dialogOKcontactar.NewInstance(null);
								//Add fragment
								newFragmentContactar.Show(ft, "dialogContactar1");

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
				///Graba los datos del auto en cache
				if (mModoBotones == 2) {
					var progressDialog = ProgressDialog.Show(inflater.Context, "", "Procesando Solicitud...", true);
					new System.Threading.Thread(new ThreadStart(delegate {
						ISharedPreferences misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
						string num = misDatos.GetString("num", "");
						string nya = misDatos.GetString("nya", "");

						//datos extra
						string[] datosExtra = mDatoExtra.Split('[');
						Console.WriteLine("datoExtra0" + datosExtra[0]);
						Console.WriteLine("datoExtra1" + datosExtra[1]);
						Console.WriteLine("datoExtra2" + datosExtra[2]);
						Console.WriteLine("-----------------------datoExtra3" + datosExtra[3]);
						Console.WriteLine("-----------------------datoExtra4" + datosExtra[4]);

						bool solicitudOK = solicitudesWeb.solicitud("Eligió Auto");
						//bool cargarAuto = solicitudesWeb.grabarDatoUsuario(num, "autoid", titulo);
						bool cargarAuto = solicitudesWeb.grabarDatoUsuario(num, "autoid", datosExtra[0]);

						//string tmpNumeroWA = solicitudesWeb.getVariable("numeroWA");
						//Console.WriteLine("Solicituddddddd: " + solicitudOK.ToString());
						//Console.WriteLine("0");

						this.Activity.RunOnUiThread(() => {
							//Console.WriteLine("1");
							progressDialog.Hide();
							//Console.WriteLine("2");
							//Console.WriteLine("Solicitud: " + solicitudOK.ToString());
							if (solicitudOK && cargarAuto) {
								Dismiss();
								/*
								Android.App.FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
								//Remove fragment else it will crash as it is already added to backstack
								Android.App.Fragment prev = Activity.FragmentManager.FindFragmentByTag("dialogContactar1");
								if (prev != null) {
									ft.Remove(prev);
								}
								ft.AddToBackStack(null);
								// Create and show the dialog.
								//dialogOKclass newFragment = dialogOKclass.NewInstance(null, "Solicitud registrada", "Un asesor se comunicará con usted en las próximas horas.");
								dialogOKcontactar newFragmentContactar = dialogOKcontactar.NewInstance(null, tmpNumeroWA);
								//Add fragment
								newFragmentContactar.Show(ft, "dialogContactar1");
								*/
								ISharedPreferencesEditor cargarDatos = misDatos.Edit();
								cargarDatos.PutString("datoConAuto", "1");
								cargarDatos.PutString("tempTab2", "1");




								//ACA ME QUEDE!!
								//string nombreImagenCache = ;
								//string tmpImagenBase64 = misDatos.GetString(datosExtra[3], "");
								//Console.WriteLine("promoPic:" + tmpImagenBase64);


								cargarDatos.PutString("miauto_id", datosExtra[0]);
								cargarDatos.PutString("miauto_titulo", datosExtra[1]);
								cargarDatos.PutString("miauto_precio", datosExtra[2]);
								cargarDatos.PutString("miauto_url1", datosExtra[3]);
								//cargarDatos.PutString("miauto_foto", tmpImagenBase64);
								cargarDatos.PutString("miauto_foto", datosExtra[4]);
								cargarDatos.PutString("miauto_llaves", datosExtra[5]);


								/*
								var imageBitmap = GetImageBitmapFromUrl(url);
								var str = "";
								using (var stream = new MemoryStream()) {
									imageBitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);

									var bytes = stream.ToArray();
									str = Convert.ToBase64String(bytes);
									//Console.WriteLine(str);
								}
								imageBitmap.Recycle();
								cargarDatos.PutString("fotoMiAuto", str.ToString());
								*/

								cargarDatos.Apply();
								//tmpImagenBase64 = "";
								//Activity.RunOnUiThread(() => {
								//Intent miIntent = new Intent(container.Context, typeof(mainFragment));
								//Intent miIntent = new Intent(this.Context, typeof(mainFragment));

								////miIntent.AddFlags(ActivityFlags.ClearTop);
								//miIntent.AddFlags(ActivityFlags.NoAnimation);
								//miIntent.PutExtra("tabSel2", "1");
								//Activity.StartActivity(miIntent);

								///BORRAR EL RESTO DE LAS IMAGENES DE AUTOS
								/*
								//public void BorrarArchivosQueNoSeUsan() {
								List<string> ListaArchivosDeImagenesEnUso = new List<string>();
								ListaArchivosDeImagenesEnUso.Add("promo.png");
								string tmpNombreArchivoImagenDesdeUrl=memoriaInterna.convertirEnAlfaNumerico(datosExtra[4]);
								ListaArchivosDeImagenesEnUso.Add(tmpNombreArchivoImagenDesdeUrl);
								int i = 0;
								//foreach (var item in ListaArchivosDeImagenesEnUso) {
								//	i++;
								//	Console.WriteLine(i.ToString() + "-" + item);
								//}
								//Console.WriteLine("TOTAL ARCHIVOS EN USO:" + i.ToString());

								i = 0;
								Context context = Application.Context;
								string[] archivos = context.FileList();
								foreach (var archivo in archivos) {
									bool EnUso = false;
									foreach (var item in ListaArchivosDeImagenesEnUso) {
										if (item == archivo) {
											EnUso = true;
										}
									}
									if (!EnUso) {
										Console.Write("borrar siguiente:");
										memoriaInterna.BorrarImagen(archivo);
									}
									i++;
									Console.WriteLine(i.ToString() + "-" + archivo);

								}
								Console.WriteLine("TOTAL ARCHIVOS:" + i.ToString());
								Console.WriteLine("--------------------");
								//}
								*/
								memoriaInterna.GuardarCambioPreferencesX(2, "");
								Activity.StartActivity(typeof(mainFragment));


								//Activity.FinishAndRemoveTask();
								Activity.Finish();

								/*var miIntent = new Intent(null, typeof(mainFragment));
								//miIntent.AddFlags(ActivityFlags.NoAnimation);
								miIntent.AddFlags(ActivityFlags.ClearTop|ActivityFlags.NewTask|ActivityFlags.NoAnimation);
								//miIntent.PutExtra("nya", e.mProfile.Name);
								//miIntent.PutExtra("fid", e.mProfile.Id);
								StartActivity(miIntent);
								Activity.Finish();
								*/
								//});
								//Activity.Parent.FinishAndRemoveTask();
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
				//Dismiss();
				///CERRAR SESION
				if (mModoBotones==3) {
					//Toast.MakeText(this.Context, "Cerrando sesión", ToastLength.Long).Show();
					ISharedPreferences misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
					ISharedPreferencesEditor cargarDatos = misDatos.Edit();
					cargarDatos.Clear();
					cargarDatos.Commit();
					/*
					try {
						memoriaInterna.trimCache(this);
						// Toast.makeText(this,"onDestroy " ,Toast.LENGTH_LONG).show();
					}
					catch (Java.Lang.Exception ex) {
						// TODO Auto-generated catch block
						Console.WriteLine(ex.Message.ToString());
					}
					*/
					Activity.StartActivity(typeof(splash));
					Activity.Finish();
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