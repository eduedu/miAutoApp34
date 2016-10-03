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
using System.Threading.Tasks;

namespace miAutoApp34.Droid {
	public  class utilitarios {

		static async Task<BitmapFactory.Options> GetBitmapOptionsOfImageAsync(byte[] imageBytes) {
			BitmapFactory.Options options = new BitmapFactory.Options {
				/*Setting the InJustDecodeBounds property to true while decoding avoids memory allocation,
				 * returning null for the bitmap object but setting OutWidth, OutHeight and OutMimeType .
				 * This technique allows you to read the dimensions and type of the image data prior to
				 * construction (and memory allocation) of the bitmap.*/
				InJustDecodeBounds = true
			};

			// The result will be null because InJustDecodeBounds == true.
			Bitmap result = await BitmapFactory.DecodeByteArrayAsync(imageBytes, 0, imageBytes.Length - 1, options);

			int imageHeight = options.OutHeight;
			int imageWidth = options.OutWidth;

			System.Diagnostics.Debug.WriteLine(string.Format("Original Size= {0}x{1}", imageWidth, imageHeight));

			return options;
		}

		static int CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight) {
			// Raw height and width of image
			float height = options.OutHeight;
			float width = options.OutWidth;
			double inSampleSize = 1D;

			if (height > reqHeight || width > reqWidth) {
				int halfHeight = (int)(height / 2);
				int halfWidth = (int)(width / 2);

				// Calculate a inSampleSize that is a power of 2 - the decoder will use a value that is a power of two anyway.
				while ((halfHeight / inSampleSize) > reqHeight && (halfWidth / inSampleSize) > reqWidth) {
					inSampleSize *= 2;
				}
			}

			return (int)inSampleSize;
		}

		static public async Task<Android.Graphics.Bitmap> LoadScaledDownBitmapForDisplayAsync(byte[] imageBytes, BitmapFactory.Options options, int reqWidth, int reqHeight) {
			// Calculate inSampleSize
			options.InSampleSize = CalculateInSampleSize(options, reqWidth, reqHeight);

			// Decode bitmap with inSampleSize set
			options.InJustDecodeBounds = false;

			return await Android.Graphics.BitmapFactory.DecodeByteArrayAsync(imageBytes, 0, imageBytes.Length -1, options);
		}

		static public async Task<Bitmap> GetImageForDisplay(string nombreImagen, int reqWidth, int reqHeight) {
			ISharedPreferences misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			string tmpImagenBase64 = misDatos.GetString(nombreImagen, "");
			byte[] imageBytes = null;
			Console.WriteLine("procesandoooo: " + nombreImagen);
			Bitmap bitmapToDisplay = null;
			if (nombreImagen != "") {
				//using (var webClient = new WebClient()) {
				//	imageBytes = webClient.DownloadData(imageURL);
				//}
				imageBytes = Convert.FromBase64String(tmpImagenBase64);

				BitmapFactory.Options options = await GetBitmapOptionsOfImageAsync(imageBytes);
				bitmapToDisplay = await LoadScaledDownBitmapForDisplayAsync(imageBytes, options, reqWidth, reqHeight);
				imageBytes = null;
			}
			Console.WriteLine("Listo: " + nombreImagen);
			return bitmapToDisplay;
		}
	}

}