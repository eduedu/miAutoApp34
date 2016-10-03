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
using Android.Support.V4.Widget;
using SupportFragment = Android.Support.V4.App.Fragment;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using SupportActionBar = Android.Support.V7.App.ActionBar;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Java.Lang;
using Android.Graphics;
//using miAutoApp34.Droid.Fragments;
//Audio:
using Android.Media;
//using System.Threading.Tasks;
using System.Threading;

namespace miAutoApp34.Droid {
	[Activity(Label = "referidos", Theme = "@style/Theme.DesignDemo2", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]



	public class refeMain : AppCompatActivity {
		//AUDIO:
		/*MediaPlayer _lock;
		MediaPlayer _unlock;
		protected override void OnResume() {
				base.OnResume();

				new System.Threading.Thread(new ThreadStart(delegate {
						_unlock.Start();
				})).Start();


		}
		protected override void OnStop() {
				base.OnStop();

				new System.Threading.Thread(new ThreadStart(delegate {
						_lock.Start();

				})).Start();
		}
		*/
		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.refeMain);
			//TOOL BUTTON
			/*SupportToolbar toolBar = FindViewById<SupportToolbar>(Resource.Id.toolBar);
			SetSupportActionBar(toolBar);

			SupportActionBar supportActionBar = SupportActionBar;
			supportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
			supportActionBar.SetDisplayHomeAsUpEnabled(true);
*/

			/////// AUDIOO
			/*
			_lock = MediaPlayer.Create(this, Resource.Raw.carlock);
			_unlock = MediaPlayer.Create(this, Resource.Raw.carunlock);
			*/

			//FUENTES
			Typeface tf = Typeface.CreateFromAsset(Assets, "fonts/ROBOTO-REGULAR.TTF");
			Typeface tf2 = Typeface.CreateFromAsset(Assets, "fonts/ROBOTOCONDENSED-BOLD.TTF");
			Typeface tf3 = Typeface.CreateFromAsset(Assets, "fonts/ROBOTO-BLACK.TTF");

			TextView titulo = FindViewById<TextView>(Resource.Id.titulo);
			titulo.Typeface = tf;

			TabLayout tabs = FindViewById<TabLayout>(Resource.Id.tabs);
			ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
			SetUpViewPager(viewPager);
			tabs.SetupWithViewPager(viewPager);

			string tabSel = Intent.GetStringExtra("tabSel") ?? "";
			if (tabSel == "1") {
				//TabLayout.Tab tab = tabLayout.getTabAt(someIndex);
				TabLayout.Tab tabSelect = tabs.GetTabAt(1);
				tabSelect.Select();
			}
			//ANIMACIONES
			//Window.Attributes.WindowAnimations = Resource.Style.animacionesRefe;


			/////TEMP
			//string u = Intent.GetStringExtra("u");

			/*
			FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);

			fab.Click += (o, e) => {
				View anchor = o as View;

				Snackbar.Make(anchor, "Yay Snackbar!!", Snackbar.LengthLong)
						.SetAction("Action", v => {
							//Do something here
							//   Intent intent = new Intent(fab.Context, typeof(BottomSheetActivity));
							//  StartActivity(intent);
						})
						.Show();
			};
			*/

		}
		/*public override void OnActivityCreated(Bundle savedInstanceState) {
			Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
			base.OnActivityCreated(savedInstanceState);
			Dialog.Window.Attributes.WindowAnimations = Resource.Style.animacionesDialog;
		}
		*/
		private void SetUpViewPager(ViewPager viewPager) {
			TabAdapter adapter = new TabAdapter(SupportFragmentManager);
			adapter.AddFragment(new refeContactos(), "REFERIDOS");
			adapter.AddFragment(new refePromo(), "PROMO ACTUAL");
			adapter.AddFragment(new refeCompartir(), "COMPARTIR");

			//adapter.AddFragment(new fAutos(), "REFERIDOS");
			viewPager.Adapter = adapter;
		}

		public override bool OnOptionsItemSelected(IMenuItem item) {


			return base.OnOptionsItemSelected(item);

		}



		public class TabAdapter : FragmentPagerAdapter {
			public List<SupportFragment> Fragments { get; set; }
			public List<string> FragmentNames { get; set; }

			public TabAdapter(Android.Support.V4.App.FragmentManager sfm) : base(sfm) {
				Fragments = new List<SupportFragment>();
				FragmentNames = new List<string>();
			}



			public void AddFragment(SupportFragment fragment, string name) {
				Fragments.Add(fragment);
				FragmentNames.Add(name);
			}

			public override int Count {
				get {
					return Fragments.Count;
				}
			}

			public override SupportFragment GetItem(int position) {
				return Fragments[position];
			}

			public override ICharSequence GetPageTitleFormatted(int position) {
				return new Java.Lang.String(FragmentNames[position]);
			}


		}

	}
}