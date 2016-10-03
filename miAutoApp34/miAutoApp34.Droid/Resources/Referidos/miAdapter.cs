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
using Android.Provider;
using Android.Graphics;

namespace miAutoApp34.Droid {
	class miAdapter : BaseAdapter<itemContacto> {
		Activity context;
		Filter filtro;
		List<itemContacto> items;
		List<itemContacto> allitems;
		Typeface fnt1;

		public miAdapter(Activity context, int txtViewResourceId, List<itemContacto> items)
						: base() {
			this.context = context;
			this.items = items;
			
			fnt1 = Android.Graphics.Typeface.CreateFromAsset(context.Assets, "fonts/ROBOTO-BOLD.TTF");
		}

		public override long GetItemId(int position) {
			return position;
		}
		public override itemContacto this[int position] {
			get { return items[position]; }
		}

		public override int Count {
			get { return items.Count; }
		}
		public override View GetView(int position, View convertView, ViewGroup parent) {
			var item = items[position];
			View view = convertView;
			if (view == null)
				view = context.LayoutInflater.Inflate(Resource.Layout.refeContactItem, null);

			TextView nombre = view.FindViewById<TextView>(Resource.Id.Text1);
			nombre.Typeface = fnt1;
			nombre.Text = item.nombre;
			//view.FindViewById<TextView>(Resource.Id.Text2).Text = item.numero;
			view.FindViewById<TextView>(Resource.Id.Text2).Text = item.numero;

			//FOTOS DE LOS CONTACTOS:///////////////////////////////////////////

			//view.FindViewById<ImageView>(Resource.Id.Image).SetImageResource(item.ImageResourceId);
			//view.FindViewById<ImageView>(Resource.Id.Image).SetImageResource(Resource.Drawable.Icon);
			if (item.photoid == null) {
				view.FindViewById<ImageView>(Resource.Id.Image).SetImageResource(Resource.Drawable.iconousuario);
				//view.FindViewById<ImageView>(Resource.Id.Image).SetImageResource(Resource.Drawable.icono);
				//view.FindViewById<ImageView>(Resource.Id.Image).SetImageResource(Resource.Drawable.btnLoginAzul);
				//view.FindViewById<ImageView>(Resource.Id.Image).SetImageBitmap(null);

				//contactImage = view.FindViewById<ImageView>(Resource.Id.ContactImage);
				//contactImage.SetImageResource(Resource.Drawable.ContactImage);
			}
			else {
				//var contactUri = ContentUris.WithAppendedId(ContactsContract.Contacts.ContentUri, contactList[position].Id);
				//var contactUri = ContentUris.WithAppendedId(Android.Provider.ContactsContract.Contacts.ContentUri, Convert.ToInt64(item.id));
				//var contactUri = ContentUris.WithAppendedId(Android.Provider.ContactsContract.Contacts.ContentUri, Int32.Parse(item.id));
				///var contactUri = ContentUris.WithAppendedId(Android.Provider.ContactsContract.Contacts.ContentUri, item.id);

				//var contactPhotoUri = Android.Net.Uri.WithAppendedPath(contactUri, Contacts.Photos.ContentDirectory);
				///Android.Net.Uri contactPhotoUri = Android.Net.Uri.WithAppendedPath(contactUri, Android.Provider.ContactsContract.Contacts.Photo.ContentDirectory);
				//var contactPhotoUri = Android.Net.Uri.WithAppendedPath(contactUri, Contacts.Photos.ContentDirectory);
				//contactImage.SetImageURI(contactPhotoUri);
				//Console.WriteLine("photoId:" + item.photoid);
				//Console.WriteLine("contactUri:" + contactUri.ToString());
				//Console.WriteLine("contactPhotoUri:" + contactPhotoUri.ToString());
				//view.FindViewById<ImageView>(Resource.Id.Image).SetImageURI(contactPhotoUri);
				if (item.photoid != "") {
					view.FindViewById<ImageView>(Resource.Id.Image).SetImageURI(Android.Net.Uri.Parse(item.photoid));
				}
				
				//Bitmap bmp = BitmapFactory.DecodeStream(ContentResolver.OpenInputStream(contactPhotoUri));
				//view.FindViewById<ImageView>(Resource.Id.Image).SetImageBitmap(bmp);
			}


			RelativeLayout itemLayout = view.FindViewById<RelativeLayout>(Resource.Id.itemLayout);
			itemLayout.Tag = position;

			if (item.selec == 0) {
				itemLayout.SetBackgroundResource(Resource.Drawable.refeCustomSelector);
			}
			else {
				itemLayout.SetBackgroundResource(Resource.Drawable.refeSeleccionado);
			}

			//En la linea siguiente elimino el evento porque sino me da como que hace muchuos clicks.
			itemLayout.Click -= new EventHandler(this.ItemLayout_Click);
			itemLayout.Click += new EventHandler(this.ItemLayout_Click);

			/*
			itemLayout.Click += (o, s) => {
					//Toast.MakeText(context, position, ToastLength.Short).Show();
					Console.WriteLine(position);
			};
			*/
			return view;
		}

		private void ItemLayout_Click(object sender, EventArgs e) {
			//throw new NotImplementedException();

			//Toast.MakeText(context, (int)((RelativeLayout)sender).Tag, ToastLength.Short).Show();
			int position = (int)((RelativeLayout)sender).Tag;
			Console.WriteLine(position);
			if (items[position].selec == 0) {
				items[position].selec = 1;
			}
			else {
				items[position].selec = 0;
			}
			//((View)sender).SetBackgroundResource(Resource.Drawable.refeCustomSelector);
			this.NotifyDataSetChanged();


		}

		/*public override Filter Filter {
				get {
						return Filter;
				}
		}
		*/
	}
}