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

using Android.Support.V4.View;
using Android.Support.V4.App;

namespace miAutoApp34.Droid {

	public class GenericFragmentPagerAdaptor : FragmentPagerAdapter {
		private List<Android.Support.V4.App.Fragment> _fragmentList = new List<Android.Support.V4.App.Fragment>();
		public GenericFragmentPagerAdaptor(Android.Support.V4.App.FragmentManager fm)
				: base(fm) { }

		public override int Count {
			get { return _fragmentList.Count; }
		}

		public override Android.Support.V4.App.Fragment GetItem(int position) {
			return _fragmentList[position];
		}

		public void AddFragment(GenericViewPagerFragment fragment) {
			_fragmentList.Add(fragment);
		}

		public void AddFragmentView(Func<LayoutInflater, ViewGroup, Bundle, View> view) {
			_fragmentList.Add(new GenericViewPagerFragment(view));
		}


	}
	public class cambioDeImagenListener : ViewPager.SimpleOnPageChangeListener {
		private ImageView circ1;
		private ImageView circ2;
		private ImageView circ3;
		private ImageView circ4;
		public cambioDeImagenListener(ImageView _circ1, ImageView _circ2, ImageView _circ3, ImageView _circ4) {
			circ1 = _circ1;
			circ2 = _circ2;
			circ3 = _circ3;
			circ4 = _circ4;
		}
		public override void OnPageSelected(int position) {
			Console.WriteLine("POSICIONNNNNNNNNNNNNN: " + position.ToString());
			switch (position) {
				case 0:
					//circ1.SetImageDrawable(Resource.Drawable.circuloimagenselec);
					circ1.SetImageResource(Resource.Drawable.circuloimagenselec);
					circ2.SetImageResource(Resource.Drawable.circuloimagennoselec);
					circ3.SetImageResource(Resource.Drawable.circuloimagennoselec);
					circ4.SetImageResource(Resource.Drawable.circuloimagennoselec);
					break;
				case 1:
					circ1.SetImageResource(Resource.Drawable.circuloimagennoselec);
					circ2.SetImageResource(Resource.Drawable.circuloimagenselec);
					circ3.SetImageResource(Resource.Drawable.circuloimagennoselec);
					circ4.SetImageResource(Resource.Drawable.circuloimagennoselec);
					break;
				case 2:
					circ1.SetImageResource(Resource.Drawable.circuloimagennoselec);
					circ2.SetImageResource(Resource.Drawable.circuloimagennoselec);
					circ3.SetImageResource(Resource.Drawable.circuloimagenselec);
					circ4.SetImageResource(Resource.Drawable.circuloimagennoselec);
					break;
				case 3:
					circ1.SetImageResource(Resource.Drawable.circuloimagennoselec);
					circ2.SetImageResource(Resource.Drawable.circuloimagennoselec);
					circ3.SetImageResource(Resource.Drawable.circuloimagennoselec);
					circ4.SetImageResource(Resource.Drawable.circuloimagenselec);
					break;
				default:
					Console.WriteLine("Default case");
					break;
			}
		}
	}

	/*
	public class ViewPageListenerForActionBar : ViewPager.SimpleOnPageChangeListener {
		
		private ActionBar _bar;
		public ViewPageListenerForActionBar(ActionBar bar) {
			_bar = bar;
		}
		
		public override void OnPageSelected(int position) {
			_bar.SetSelectedNavigationItem(position);
		}
		

	}
*/
	/*
	public static class ViewPagerExtensions {
		public static ActionBar.Tab GetViewPageTab(this ViewPager viewPager, ActionBar actionBar, string name) {
			var tab = actionBar.NewTab();
			tab.SetText(name);
			tab.TabSelected += (o, e) =>
			{
				viewPager.SetCurrentItem(actionBar.SelectedNavigationIndex, false);
			};
			return tab;
		}
	}
	*/

}