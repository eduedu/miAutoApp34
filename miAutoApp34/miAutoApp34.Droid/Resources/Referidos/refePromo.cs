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
using Android.Provider;
using System.Diagnostics;
using Android.Database;
using Android.Graphics;
//using Java.IO;
//using Java.IO;
//using Java.IO;

namespace miAutoApp34.Droid {
	public class refePromo : SupportFragment {
		public Android.Graphics.Typeface fntBlack;
		public Android.Graphics.Typeface fntRegular;
		//ProgressBar mProgress;
		///GLOBAL
		ImageView imagenPromo;
		ProgressBar progress;
		TextView txtPromo;
		ISharedPreferences misDatos;
		string mUrlPromoPic;

		public override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);


		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			View view = inflater.Inflate(Resource.Layout.refePromo, container, false);

			//CArgar datos:
			misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);

			//FUENTES
			//Android.Graphics.Typeface 
			fntRegular = Android.Graphics.Typeface.CreateFromAsset(inflater.Context.Assets, "fonts/ROBOTO-REGULAR.TTF");
			Android.Graphics.Typeface fntBold = Android.Graphics.Typeface.CreateFromAsset(inflater.Context.Assets, "fonts/ROBOTOCONDENSED-BOLD.TTF");
			fntBlack = Android.Graphics.Typeface.CreateFromAsset(inflater.Context.Assets, "fonts/ROBOTO-BLACK.TTF");


			//REFERENCIAS A CONTROLES
			imagenPromo = view.FindViewById<ImageView>(Resource.Id.imagenPromo);
			progress = view.FindViewById<ProgressBar>(Resource.Id.progressBar11);
			txtPromo = view.FindViewById<TextView>(Resource.Id.txtPromo);

			txtPromo.Typeface = fntRegular;
			txtPromo.Visibility = ViewStates.Invisible;
			progress.Visibility = ViewStates.Invisible;



			//cargarImagenPromoDesdePreferences();
			cargarImagenPromoDesdeMemoriaInterna();


			return view;
			// return base.OnCreateView(inflater, container, savedInstanceState);
		}
		public override void OnResume() {
			base.OnResume();

			new Thread(new ThreadStart(delegate {
				cargarUrlPromoPic();
				string urlPromoPicWeb = solicitudesWeb.getVariable("urlPromoPic");
				Console.WriteLine("mUrlPromoPic:" + mUrlPromoPic);
				Console.WriteLine("urlPromoPicWeb:" + urlPromoPicWeb);

				if (mUrlPromoPic != urlPromoPicWeb) {
					if (urlPromoPicWeb.ToString().Trim() == "") {
						ISharedPreferencesEditor tmpCargarDatos = misDatos.Edit();
						tmpCargarDatos.PutString("promoPic", "");
						tmpCargarDatos.PutString("urlPromoPic", "");
						tmpCargarDatos.Apply();
						mUrlPromoPic = "";
						memoriaInterna.GuardarImagen(null, "promo.png");
					}
					else {
						try {
							Activity.RunOnUiThread(() => {
								progress.Visibility = ViewStates.Visible;
								txtPromo.Text = "Cargando promo...";
								txtPromo.Visibility = ViewStates.Visible;
							});
						}
						catch (Exception ex) {
							Console.WriteLine("error: " + ex.Message);
						}
						//GUARDA/COMPRIME IMAGEN obtenida desde URL Y LA GUARDA EN CACHE/PREFERENCES
						/*
						var arrayImagenWeb = GetImageByteArrayFromUrl(urlPromoPicWeb);
						if (arrayImagenWeb != null) {
							var tmpPromoPicString = Convert.ToBase64String(arrayImagenWeb);
							ISharedPreferencesEditor tmpCargarDatos = misDatos.Edit();
							tmpCargarDatos.PutString("promoPic", tmpPromoPicString.ToString());
							tmpCargarDatos.PutString("urlPromoPic", urlPromoPicWeb);
							tmpCargarDatos.Apply();
							Activity.RunOnUiThread(() => {
								progress.Visibility = ViewStates.Invisible;
								txtPromo.Visibility = ViewStates.Invisible;
							});
						}
						*/
						//GUARDA bitmap obtenido desde URL en un archivo dentro de la carpeta data

						var imageBitmap = GetImageBitmapFromUrl(urlPromoPicWeb);
						if (imageBitmap != null) {
							//var tmpPromoPicString = Convert.ToBase64String(arrayImagenWeb);
							memoriaInterna.GuardarImagen(imageBitmap, "promo.png");
							ISharedPreferencesEditor tmpCargarDatos = misDatos.Edit();
							//tmpCargarDatos.PutString("promoPic", tmpPromoPicString.ToString());
							tmpCargarDatos.PutString("urlPromoPic", urlPromoPicWeb);
							tmpCargarDatos.Apply();
							try {
								Activity.RunOnUiThread(() => {
									progress.Visibility = ViewStates.Invisible;
									txtPromo.Visibility = ViewStates.Invisible;
								});
							}
							catch (Exception ex) {
								Console.WriteLine("error: " + ex.Message);
							}
						}

						else {
							Activity.RunOnUiThread(() => {
								txtPromo.Text = "Error: sin conexión.";
								txtPromo.Visibility = ViewStates.Visible;
								progress.Visibility = ViewStates.Invisible;
							});
						}
					}
					try { 
					Activity.RunOnUiThread(() => {
						//cargarImagenPromoDesdePreferences();
						cargarImagenPromoDesdeMemoriaInterna();
					});
					} catch(Exception ex) {
						Console.WriteLine("Error al cargar la imagenPromo porque se cerro el thread:" + ex.Message);
					}

				}
			})).Start();

		}
		/// FUNCIONES-------------------------------------------------------------------
		public void cargarUrlPromoPic() {
			misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			mUrlPromoPic = misDatos.GetString("urlPromoPic", "");
		}
		public void guardarImagenPromoEnPreferences() {

			//ISharedPreferencesEditor tmpCargarDatos = misDatos.Edit();
			//tmpCargarDatos.PutString("promoUrl", BitmapToBase64(mBitmap));
			//tmpCargarDatos.Apply();
			imagenPromo.BuildDrawingCache(true);
			Bitmap bmap = imagenPromo.GetDrawingCache(true);
			imagenPromo.SetImageBitmap(bmap);
			Bitmap tmpBitmap = Bitmap.CreateBitmap(imagenPromo.GetDrawingCache(true));

			var str = "";
			using (var stream = new MemoryStream()) {
				tmpBitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);

				var bytes = stream.ToArray();
				str = Convert.ToBase64String(bytes);

				//Console.WriteLine(str);
			}
			ISharedPreferencesEditor tmpCargarDatos = misDatos.Edit();
			tmpCargarDatos.PutString("promoPic", str.ToString());
			tmpCargarDatos.Apply();


		}
		public void cargarImagenPromoDesdePreferences() {  //NO USO ESTA FUNCION???
			misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			string tmpImagenBase64 = misDatos.GetString("promoPic", "");
			//string tmpImagenBase64 = misDatos.GetString("http://i.imgur.com/oe6slVZ.jpg", "");

			//Console.WriteLine("promoPic:" + tmpImagenBase64);

			if (tmpImagenBase64 != "") {
				var imageBytes = Convert.FromBase64String(tmpImagenBase64);
				Bitmap bitBorrar = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
				imagenPromo.SetImageBitmap(bitBorrar);
				progress.Visibility = ViewStates.Invisible;
				txtPromo.Visibility = ViewStates.Invisible;
			}
			else {
				imagenPromo.SetImageBitmap(null);
				txtPromo.Text = "Por el momento, no se encuentran promociones disponibles.";
				txtPromo.Visibility = ViewStates.Visible;
				progress.Visibility = ViewStates.Invisible;
			}

		}
		/*
		public void cargarImagenWebNOOOOOOOOOOOOOOOOOOOOOO() {
			//IMAGEN REMOTA
			progress.Visibility = ViewStates.Visible;
			txtPromo.Text = "Cargando promo...";
			txtPromo.Visibility = ViewStates.Visible;
			new Thread(new ThreadStart(delegate {
				//string uriStr = "https://script.google.com/macros/s/AKfycbxB98IS32T9mCUJbSccWmBg17LMRGmcvB7Kqa9lFcM_8eiM6rE/exec?action=getUrlCount&url={0}";
				string uriStr = "https://script.google.com/macros/s/AKfycbzqI79YB05eq7fCIwWIlWeo2Z3cS3ELD84LWvOgDP9H9XnvwUx6/exec?action=getVariable&url=urlPromoPic";
				try {
					var request = HttpWebRequest.Create(@uriStr);
					request.ContentType = "application /json";
					request.Method = "GET";
					HttpWebResponse response = request.GetResponse() as HttpWebResponse;
					using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
						var respuesta = reader.ReadToEnd();
						//Console.Out.WriteLine("URL IMAGEN: "+ respuesta.ToString());
						try {
							if (respuesta.ToString().Trim() == "" || respuesta == null || string.IsNullOrWhiteSpace(respuesta)) {
								Console.WriteLine("Errorrrrr: respuesta vacia");
								Activity.RunOnUiThread(() => {
									txtPromo.Text = "Por el momento, no se encuentran promociones disponibles.";
									txtPromo.Visibility = ViewStates.Visible;
									progress.Visibility = ViewStates.Invisible;
								});
							}
							else {
								//Console.WriteLine("Todo Ok?");
								Activity.RunOnUiThread(() => {
									var imageBitmap = GetImageBitmapFromUrl(respuesta.ToString());
									imagenPromo.SetImageBitmap(imageBitmap);
									ISharedPreferencesEditor tmpCargarDatos = misDatos.Edit();
									tmpCargarDatos.PutString("urlPromoPic", respuesta.ToString().Trim());
									tmpCargarDatos.Apply();
									progress.Visibility = ViewStates.Invisible;
									txtPromo.Visibility = ViewStates.Invisible;

									guardarImagenPromoEnPreferences();
								});
							}
						}
						catch (Exception ex) {
							Console.WriteLine("Errorrrrr (algo q ver con el thread?:" + ex.Message);
						}

					}
					//RunOnUiThread(() => progressDialog.Hide());
				}
				catch (WebException ex) {
					Console.WriteLine("ERROR DE CONEXION: " + ex.Message);
					Activity.RunOnUiThread(() => {
						txtPromo.Text = "Por el momento, no se encuentran promociones disponibles.";
						txtPromo.Visibility = ViewStates.Visible;
						progress.Visibility = ViewStates.Invisible;
					});
				}
			})).Start();
		}
		*/
		private Bitmap GetImageBitmapFromUrl(string url) {
			Bitmap imageBitmap = null;

			using (var webClient = new WebClient()) {
				try {
					var imageBytes = webClient.DownloadData(url);
					if (imageBytes != null && imageBytes.Length > 0) {
						imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
					}
				}
				catch (Exception ex) {
					Console.WriteLine("Error edu: " + ex.Message);
				}

			}

			return imageBitmap;
		}
		private byte[] GetImageByteArrayFromUrl(string url) {
			byte[] imageArray = null;
			using (var webClient = new WebClient()) {
				try {
					var imageBytes = webClient.DownloadData(url);
					if (imageBytes != null && imageBytes.Length > 0) {
						//imageArray = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
						imageArray = imageBytes;
					}
				}
				catch (Exception ex) {
					Console.WriteLine("Error edu: " + ex.Message);
				}

			}

			return imageArray;
		}
		/*
		private String saveToInternalStorage(Bitmap bitmapImage) {
			ContextWrapper cw = new ContextWrapper(Application.Context);
			// path to /data/data/yourapp/app_data/imageDir
			Java.IO.File directory = cw.GetDir("imageDir", FileCreationMode.Private);
			// Create imageDir
			File mypath = new Java.IO.File(directory, "profile.jpg");

			FileStream fos = null;
			try {
				fos = new FileStream()
				// Use the compress method on the BitMap object to write image to the OutputStream
				//bitmapImage.Compress(Bitmap.CompressFormat.Png, 100, fos);
				bitmapImage.Compress(Bitmap.CompressFormat.Png, 100, fos);
			}
			catch (Exception e) {
				Console.WriteLine(e.StackTrace.ToString());
			}
			finally {
				fos.Close();
			}
			return directory.AbsolutePath;
		}
		*/
		public void cargarImagenPromoDesdeMemoriaInterna() {

			var tmpBitmapPromo = memoriaInterna.LeerImagen("promo.png", 80);
			if (tmpBitmapPromo != null) {
				//var imageBytes = Convert.FromBase64String(tmpImagenBase64);
				//Bitmap bitBorrar = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
				imagenPromo.SetImageBitmap(tmpBitmapPromo);
				//imagenPromo.SetScaleType(ImageView.ScaleType.FitCenter);

				progress.Visibility = ViewStates.Invisible;
				txtPromo.Visibility = ViewStates.Invisible;
			}
			else {
				imagenPromo.SetImageBitmap(null);
				txtPromo.Text = "Por el momento, no se encuentran promociones disponibles.";
				txtPromo.Visibility = ViewStates.Visible;
				progress.Visibility = ViewStates.Invisible;
			}

		}



	}

}