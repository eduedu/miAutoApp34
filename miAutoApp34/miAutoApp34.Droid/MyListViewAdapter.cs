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

namespace miAutoApp34.Droid {
	class MyListViewAdapter : BaseAdapter<Linea> {
		private List<Linea> mItems;
		private Context mContext;
		private Android.Graphics.Typeface mFntBlack;
		private Android.Graphics.Typeface mFntRegular;

		public MyListViewAdapter(Context context, List<Linea> items, Android.Graphics.Typeface fntblack, Android.Graphics.Typeface fntregular) {
			mItems = items;
			mContext = context;
			mFntBlack = fntblack;
			mFntRegular = fntregular;

		}
		public override int Count {
			get {
				return mItems.Count;
			}
		}
		public override long GetItemId(int position) {
			return position;
		}
		public override Linea this[int position] {
			get {
				return mItems[position];
			}
		}
		public override View GetView(int position, View convertView, ViewGroup parent) {
			View row = convertView;

			if (row == null) {
				row = LayoutInflater.From(mContext).Inflate(Resource.Layout.listView_row, null, false);

			}
			TextView linea1 = row.FindViewById<TextView>(Resource.Id.linea1);
			linea1.Text = mItems[position].linea1;
			TextView linea2 = row.FindViewById<TextView>(Resource.Id.linea2);
			//linea2.Text = mItems[position].linea2;
			string tmpFecha = memoriaInterna.convertirFecha( mItems[position].linea2);
			linea2.Text = tmpFecha;
			TextView linea3 = row.FindViewById<TextView>(Resource.Id.linea3);
			linea3.Text = mItems[position].linea3;

			linea1.SetTextColor(Color.ParseColor("#4d4d4d"));
			linea2.SetTextColor(Color.ParseColor("#4d4d4d"));
			linea3.SetTextColor(Color.ParseColor("#4d4d4d"));

			string tmpFiltro = "Mensaje exclusivo para";
			if (mItems[position].linea1.Length >= tmpFiltro.Length) {
				//if (mItems[position].linea1.Substring(0, 4) == "Para") {
				if (mItems[position].linea1.Substring(0, tmpFiltro.Length) == tmpFiltro) {
					//string colorMensajePersonal = "#3E61AD";
					string colorMensajePersonal = "#f05059";
					linea1.SetTextColor(Color.ParseColor(colorMensajePersonal));
					linea2.SetTextColor(Color.ParseColor(colorMensajePersonal));
					linea3.SetTextColor(Color.ParseColor(colorMensajePersonal));
				}
			}
			/*RelativeLayout itemLayout = row.FindViewById<RelativeLayout>(Resource.Id.itemLayout);
			itemLayout.Tag = position;
			itemLayout.Click += new EventHandler(this.ItemLayout_Click);
			*/
			//FUENTES
			/*
public Typeface fntBlack; // = Android.Graphics.Typeface.CreateFromAsset(this, "fonts/ROBOTO-BLACK.TTF");

Android.Graphics.Typeface fntRegular = Android.Graphics.Typeface.CreateFromAsset(mContext.Assets, "fonts/ROBOTO-REGULAR.TTF");
Android.Graphics.Typeface fntBold = Android.Graphics.Typeface.CreateFromAsset(mContext.Assets, "fonts/ROBOTOCONDENSED-BOLD.TTF");
Android.Graphics.Typeface fntBlack = Android.Graphics.Typeface.CreateFromAsset(mContext.Assets, "fonts/ROBOTO-BLACK.TTF");
*/

			linea1.Typeface = mFntBlack;
			linea2.Typeface = mFntRegular;
			linea3.Typeface = mFntRegular;

			return row;
		}

		/*
		private void ItemLayout_Click(object sender, EventArgs e) {
				//throw new NotImplementedException();
				Toast.MakeText(mContext, (int)((Button)sender).Tag,ToastLength.Short).Show();

		}
		*/

		/*
		protected override void OnListItemClick(ListView l, View v, int position, long id) {
				base.OnListItemClick(l, v, position, id);
				Toast.MakeText(this, data[position],
				ToastLength.Short).Show();
		}
		*/

	}


}