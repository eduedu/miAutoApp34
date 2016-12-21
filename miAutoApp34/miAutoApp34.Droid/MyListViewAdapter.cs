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
using SupportFragment = Android.Support.V4.App.Fragment;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;

namespace miAutoApp34.Droid {
	class MyListViewAdapter : BaseAdapter<Linea> {
		private List<Linea> mItems;
		private Context mContext;
		private Android.Graphics.Typeface mFntBlack;
		private Android.Graphics.Typeface mFntRegular;
		//public FragmentTransaction mft;
		public string select = "0";
		//dialogConsulta newFragmentContactar = dialogConsulta.NewInstance(null, "Consulta", "Mensaje para MiAutoPlan:");

		public MyListViewAdapter(Context context, List<Linea> items, Android.Graphics.Typeface fntblack, Android.Graphics.Typeface fntregular) {
			mItems = items;
			mContext = context;
			mFntBlack = fntblack;
			mFntRegular = fntregular;

			//mft = ft;

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
			string tmpFecha = memoriaInterna.convertirFecha(mItems[position].linea2);
			linea2.Text = tmpFecha;
			TextView linea3 = row.FindViewById<TextView>(Resource.Id.linea3);
			linea3.Text = mItems[position].linea3;

			linea1.SetTextColor(Color.ParseColor("#4d4d4d"));
			linea2.SetTextColor(Color.ParseColor("#4d4d4d"));
			linea3.SetTextColor(Color.ParseColor("#4d4d4d"));

			int tmpMensajePersonal = 0;
			string tmpFiltro = "Mensaje exclusivo para";
			if (mItems[position].linea1.Length >= tmpFiltro.Length) {
				//if (mItems[position].linea1.Substring(0, 4) == "Para") {
				if (mItems[position].linea1.Substring(0, tmpFiltro.Length) == tmpFiltro) {
					//string colorMensajePersonal = "#3E61AD";
					tmpMensajePersonal = 1;
					string colorMensajePersonal = "#f05059";
					linea1.SetTextColor(Color.ParseColor(colorMensajePersonal));
					linea2.SetTextColor(Color.ParseColor(colorMensajePersonal));
					linea3.SetTextColor(Color.ParseColor(colorMensajePersonal));
				}
			}
			//row.Tag = position;
			//row.Click += new EventHandler(this.ItemLayout_Click);
			/*RelativeLayout itemLayout = row.FindViewById<RelativeLayout>(Resource.Id.itemLayout);
			itemLayout.Tag = position;
			itemLayout.Click += new EventHandler(this.ItemLayout_Click);
			*/
			LinearLayout itemLayout = row.FindViewById<LinearLayout>(Resource.Id.itemLayout);
			itemLayout.Tag = tmpMensajePersonal;
			/*
			itemLayout.Click -= new EventHandler(this.ItemLayout_Click);
			itemLayout.Click += new EventHandler(this.ItemLayout_Click);
			*/

			//itemLayout.Click += (o, s) => {
			//ItemLayout_Click(o,s);
			//};
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


		public void ItemLayout_Click(object sender, EventArgs e) {

			//throw new NotImplementedException();
			//Console.WriteLine("clik en:" + sender.GetType().ToString());
			//Console.WriteLine("clik en:" + sender.GetType().ToString());
			//Toast.MakeText(mContext, (int)((Button)sender).Tag, ToastLength.Short).Show();
			string tmpTag = ((LinearLayout)sender).Tag.ToString().Trim();
			//string tmpTag = "edu";
			Console.WriteLine("TAG:" + tmpTag);
			if (tmpTag == "1") {
				Console.WriteLine("MENSAJE PERSONAL");
				select = "1";
			}
			else {
				select = "0";
			}
			//set alert for executing the task
			/*
			AlertDialog.Builder alert = new AlertDialog.Builder(this.mContext);
			alert.SetTitle("Confirm delete");
			alert.SetMessage("Lorem ipsum dolor sit amet, consectetuer adipiscing elit.");
			alert.SetPositiveButton("Delete", (senderAlert, args) => {
				//Toast.MakeText(Application.Context, "Deleted!", ToastLength.Short).Show();
			});

			alert.SetNegativeButton("Cancel", (senderAlert, args) => {
				//Toast.MakeText(Application.Context, "Cancelled!", ToastLength.Short).Show();
			});

			Dialog dialog = alert.Create();
			dialog.Show();
			*/

			//dialogConsulta.NewInstance(null, "Solicitud registrada", "Un asesor se comunicará con usted en las próximas horas.");

			//dialogConsulta newFragmentContactar = dialogConsulta.NewInstance(null, "Consulta", "Mensaje para MiAutoPlan:");
			//Android.Support.V4.App.FragmentManager fragmentManager = ((Activity context).getFragmentManager();
			//FragmentManager fm = Android.App.Activity.FragmentManager;
			//Android.App.FragmentTransaction ft = this.mContext.ApplicationContext.FragmentManager.BeginTransaction();
			//Android.App.FragmentTransaction ft = FragmentManager.BeginTransaction();
			//Remove fragment else it will crash as it is already added to backstack
			
			/*
			Fragment prev = Activity.FragmentManager.FindFragmentByTag("dialogConsulta");
			if (prev != null) {
				ft.Remove(prev);
			}
			ft.AddToBackStack(null);
			
			//var activity = Resolve<IMvxAndroidCurrentTopActivity>();


			
			// Create and show the dialog.
			//Android.Support.V4.App.FragmentTransaction transaction = fragmentManager.BeginTransaction();
			//dialogOKclass newFragment = dialogOKclass.NewInstance(null, "Solicitud registrada", "Un asesor se comunicará con usted en las próximas horas.");
			dialogConsulta newFragmentContactar = dialogConsulta.NewInstance(null, "Consulta", "Mensaje para MiAutoPlan:");
			//Add fragment
			//newFragmentContactar.Show(ft, "dialogConsulta");
			//FragmentTransaction ft = ;

			newFragmentContactar.Show(mft, "dialogConsulta");
			
			*/
		}


		/*
		protected override void OnListItemClick(ListView l, View v, int position, long id) {
				base.OnListItemClick(l, v, position, id);
				Toast.MakeText(this, data[position],
				ToastLength.Short).Show();
		}
		*/
		/*
		public void openFragment(Fragment fragment) {
			FragmentManager fragmentManager=;
			mContext.
			//FragmentTransaction transaction =  Android.Support.V4.App.Fragment
			Android.Support.V4.App.FragmentTransaction transaction = Android.Support.V4.App.FragmentTransaction. FragmentManager.BeginTransaction();
			transaction.replace(R.id.container, fragment);
			transaction.addToBackStack(null);
			transaction.commit();

		}
		*/
	}


}