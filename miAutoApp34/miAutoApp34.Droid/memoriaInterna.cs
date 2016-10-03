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
using System.IO;
using Javax;
using Android.Util;

namespace miAutoApp34.Droid {
	public static class memoriaInterna {
		public static void GuardarImagen(Bitmap b, string nombreArchivo) {
			Context context = Application.Context;

			Stream outp = null;
			try {
				//Console.WriteLine("cache dirpath:" + context.CacheDir);
				//Console.WriteLine("package resource path:" + context.PackageResourcePath);
				outp = context.OpenFileOutput(nombreArchivo, FileCreationMode.WorldReadable);
				b.Compress(Bitmap.CompressFormat.Webp, 100, outp);
				//Console.WriteLine("cache dirpath:" + context.);
			}
			catch (Exception e) {
				Console.WriteLine(e.ToString());
			}
			outp.Flush();
			outp.Close();
		}
		public static void BorrarImagen(string nombreArchivo) {
			if (nombreArchivo.Substring(0, 1) != ".") {
				string path = Application.Context.FilesDir.Path;
				var filePath = System.IO.Path.Combine(path, nombreArchivo);
				File.Delete(filePath);
			}
		}

		public static string convertirEnAlfaNumerico(string url) {
			if (url != "") {
				url = url.Replace("/", string.Empty);
				url = url.Replace(":", string.Empty);
				url = url.Substring(url.Length - 11);
			}
			return url;
		}
		public static Bitmap ResizeBitmap(this Bitmap input, int destWidth, int destHeight) {
			int srcWidth = input.Width,
					srcHeight = input.Height;
			bool needsResize = false;
			float p;
			if (srcWidth > destWidth || srcHeight > destHeight) {
				needsResize = true;
				if (srcWidth > srcHeight && srcWidth > destWidth) {
					p = (float)destWidth / (float)srcWidth;
					destHeight = (int)(srcHeight * p);
				}
				else {
					p = (float)destHeight / (float)srcHeight;
					destWidth = (int)(srcWidth * p);
				}
			}
			else {
				destWidth = srcWidth;
				destHeight = srcHeight;
				/*
				needsResize = true;
				if (srcWidth < srcHeight && srcWidth < destWidth) {
					p = (float)srcWidth / (float)destWidth  ;
					destHeight = (int)(srcHeight * p);
				}
				else {
					p = (float)srcHeight / (float)destHeight  ;
					destWidth = (int)(srcWidth * p);
				}
				*/
			}
			if (needsResize) {
				return Bitmap.CreateScaledBitmap(input, destWidth, destHeight, true);
			}
			return input;
		}
		public static Bitmap cargarImagenDesdeCacheNO(string nombreImagenCache) {
			Bitmap imageBitmap = null;
			ISharedPreferences misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			//Console.WriteLine("NOMBRE Imagen CACHE:" + nombreImagenCache);
			string tmpImagenBase64 = misDatos.GetString(nombreImagenCache, "");
			//Console.WriteLine("promoPic:" + tmpImagenBase64);

			if (tmpImagenBase64 != "") {
				var imageBytes = Convert.FromBase64String(tmpImagenBase64);
				//var imageBytes= Base64.Decode(tmpImagenBase64, Base64Flags.Default);
				imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);

				//imageBitmap = Bitmap.CreateScaledBitmap(BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length),100,100,false);

				//BitmapFactory.Options opciones= new BitmapFactory.Options();
				//opciones.InSampleSize=4;
				//imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length,opciones);



				//Bitmap bitBorrar = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
				//imagenPromo.SetImageBitmap(bitBorrar);
				//progress.Visibility = ViewStates.Invisible;
				//txtPromo.Visibility = ViewStates.Invisible;
			}
			else {
				/*
				imagenPromo.SetImageBitmap(null);
				txtPromo.Text = "Por el momento, no se encuentran promociones disponibles.";
				txtPromo.Visibility = ViewStates.Visible;
				progress.Visibility = ViewStates.Invisible;
				*/
			}
			return imageBitmap;
		}
		public static Bitmap LeerImagenVIEJO(string nombreArchivo) {
			Bitmap b = null;
			Context context = Application.Context;
			try {

				//metodo con stream
				/*
				Stream fis = context.OpenFileInput(nombreArchivo);
				b = BitmapFactory.DecodeStream(fis);
				fis.Flush();
				fis.Close();
				fis.Flush();
				*/

				//metodo lectura directa
				string path = Application.Context.FilesDir.Path;
				var filePath = System.IO.Path.Combine(path, nombreArchivo);
				Console.WriteLine("FILE PATH:" + path);
				b = BitmapFactory.DecodeFile(filePath);

				//return b;
				//b = Bitmap.CreateScaledBitmap(b, 100, 100, false);
			}
			catch (Exception e) {
				Console.WriteLine("Error con imagen: " + nombreArchivo);
				Console.WriteLine("Error messageeeeee: " + e.Message);
				Console.WriteLine("Error al leer Imagen desde memoria interna:" + e.ToString());

			}

			return b;
		}
		private static int ConvertPixelsToDp(float pixelValue) {
			var dp = (int)((pixelValue) / Application.Context.Resources.DisplayMetrics.Density);
			return dp;
		}
		public static Bitmap LeerImagen(string nombreArchivo, int porcentajeAnchoPantalla=100) {
			Bitmap b = null;
			Context context = Application.Context;

			//calcular el ancho y la relacion de aspecto en base a las dimensiones de la pantalla
			var metrics = context.Resources.DisplayMetrics;
			//var widthInDp = ConvertPixelsToDp(metrics.WidthPixels);
			//var heightInDp = ConvertPixelsToDp(metrics.HeightPixels);
			//Console.WriteLine("AnchoDP:" + widthInDp);
			//Console.WriteLine("Ancho Pantalla en PX:" + metrics.WidthPixels);
			int anchoEnPx = (metrics.WidthPixels * porcentajeAnchoPantalla) / 100;
			//Console.WriteLine("Ancho IMAGEN en PX:" +anchoEnPx);

			string path = Application.Context.FilesDir.Path;
			var filePath = System.IO.Path.Combine(path, nombreArchivo);
			var filePathUri = Android.Net.Uri.FromFile(new Java.IO.File(filePath));

			//var filePathUri = Android.Net.Uri.Parse(filePath);
			//Console.WriteLine("---filePath:" + filePath);
			//Console.WriteLine("filePathUri:" + filePathUri);

			try {
				///aca carga la imagen completa o solo la redimensionada
				//b = BitmapFactory.DecodeFile(filePath);
				//b = DecodeBitmapFromStream(filePathUri, 50, 50);
				b = DecodeBitmapFromStream(filePathUri, anchoEnPx, anchoEnPx);
			}
			catch (Exception e) {
				Console.WriteLine("Error con imagen: " + nombreArchivo);
				Console.WriteLine("Error messageeeeee: " + e.Message);
				Console.WriteLine("Error al leer Imagen desde memoria interna:" + e.ToString());

			}

			return b;
		}


		//Nuevas funciones para optimizar el cargado de imágenes
		private static Bitmap DecodeBitmapFromStream(Android.Net.Uri data, int requestedWidth, int requestedHeight) {
			//Android.Net.Uri data;
			//data = Android.Net.Uri.Parse(datos);
			Context context = Application.Context;
			//Stream stream=ContentResolver.openInputStream
			Stream stream = context.ContentResolver.OpenInputStream(data);

			BitmapFactory.Options options = new BitmapFactory.Options();
			options.InJustDecodeBounds = true;
			BitmapFactory.DecodeStream(stream, null, options);

			//calcular el tamaño:
			
			Console.WriteLine("-----------------------");
			Console.WriteLine("Archivo:" + data.ToString());
			Console.WriteLine("Alto:" + options.OutHeight);
			Console.WriteLine("Ancho:" + options.OutWidth);
			//Console.WriteLine("InSampleSize:" + options.InSampleSize);
			
			options.InSampleSize = CalculateInSampleSize(options, requestedWidth, requestedHeight);
			Console.WriteLine("InSampleSize2:" + options.InSampleSize);
			Console.WriteLine("Ancho Pretendido:" + requestedWidth);

			//volver a leer y guardar el bitmap con el tamaño deseado
			stream = context.ContentResolver.OpenInputStream(data);
			options.InJustDecodeBounds = false;
			Bitmap bitmap = BitmapFactory.DecodeStream(stream, null, options);
			return bitmap;
		}
		private static int CalculateInSampleSize(BitmapFactory.Options options, int requestedWidth, int requestedHeight) {
			int heigth = options.OutHeight;
			int width = options.OutWidth;
			int inSampleSize = 1;
			if (heigth > requestedHeight || width > requestedWidth) {
				int halfHeight = heigth / 2;
				int halfWidth = width / 2;

				while ((halfHeight / inSampleSize) > requestedHeight && (halfWidth / inSampleSize) > requestedWidth) {
					inSampleSize *= 2;
				}
			}
			return inSampleSize;
		}
		public static string convertirFecha(string fecha, int formato = 0) {
			string retorno = "";
			Console.WriteLine("string fuente:"+fecha);
			try {

			
			DateTime fechaOriginal = DateTime.Parse(fecha);
			//DateTime fechaActualizada = fechaOriginal.Subtract(new TimeSpan(3, 0, 0));
			DateTime fechaActualizada = fechaOriginal;
			string dia = fechaActualizada.ToString("dd/MM/yy");
			string horas = fechaActualizada.ToString("HH:mm");
			Console.WriteLine("fecha:" + fecha);
			//Console.WriteLine("fecha2:" + fecha2.ToString());
			//Console.WriteLine("fecha3:" + fecha3);
			retorno = dia + " " + horas;
			Console.WriteLine("fecha final:" + retorno);
			} catch (Exception ex) {
				retorno = fecha;
				Console.WriteLine("Error al querer convertir fecha:" + ex.Message);
			}
			return retorno;
		}
		public static void GuardarCambioPreferencesX(int campo, string valor) {
			ISharedPreferences tmpMisDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			string preCambios = tmpMisDatos.GetString("preCambios", "0[0[0");
			string[] preCambio = preCambios.Split('[');
			preCambio[campo] = valor;
			string finalCambios = "";
			for (int i = 0; i < preCambio.Length; i++) {
				finalCambios = finalCambios + preCambio[i] + "[";
			}
			finalCambios = finalCambios.Substring(0, finalCambios.Length - 1);
			//ISharedPreferences misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			ISharedPreferencesEditor tmpCargarDatos = tmpMisDatos.Edit();
			tmpCargarDatos.PutString("preCambios", finalCambios);
			tmpCargarDatos.Apply();

			Console.WriteLine("FINAL CAMBIOS: " + finalCambios);
		}
	}
}
