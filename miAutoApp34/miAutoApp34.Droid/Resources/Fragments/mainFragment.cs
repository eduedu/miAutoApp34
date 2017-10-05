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
using System.Collections.Specialized;
using System.Net;
using System.Threading;
using Java.Lang.Reflect;
//using miAutoApp34.Droid.Fragments;

namespace miAutoApp34.Droid {
	[Activity(Label = "mainFragment", Theme = "@style/Theme.DesignDemo", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
	//[Activity(Label = "mainFragment", Theme = "@style/AppTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]

	public class mainFragment : AppCompatActivity {
		public static ViewPager viewPager;
		public TabAdapter adapter;
		public static TabAdapter adapter2;


		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.mainFragment);
			//TOOL BUTTON
			/*SupportToolbar toolBar = FindViewById<SupportToolbar>(Resource.Id.toolBar);
            SetSupportActionBar(toolBar);

            SupportActionBar supportActionBar = SupportActionBar;
            supportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            supportActionBar.SetDisplayHomeAsUpEnabled(true);
			*/
			///Datos
			ISharedPreferences misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			string datoconAuto = misDatos.GetString("datoConAuto", "");
			string tempTab2 = misDatos.GetString("tempTab2", "");

			//FUENTES
			Typeface tf = Typeface.CreateFromAsset(Assets, "fonts/ROBOTO-REGULAR.TTF");
			Typeface tf2 = Typeface.CreateFromAsset(Assets, "fonts/ROBOTOCONDENSED-BOLD.TTF");
			Typeface tf3 = Typeface.CreateFromAsset(Assets, "fonts/ROBOTO-BLACK.TTF");


			TextView titulo = FindViewById<TextView>(Resource.Id.titulo);
			titulo.Typeface = tf;

			TabLayout tabs = FindViewById<TabLayout>(Resource.Id.tabs);
			viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);

			ImageView btnMenu = FindViewById<ImageView>(Resource.Id.imageMenu);
			//AVERIGUAR EL ALTO APROX en PX DE la BARRA AZUL y guardar en PREF
			//LinearLayout barraAzul = FindViewById<LinearLayout>(Resource.Id.LinearTitulo);
			ImageView image1 = FindViewById<ImageView>(Resource.Id.logoBarra);
			ISharedPreferencesEditor cargarDatos = misDatos.Edit();
			string LinearTitulo = (image1.LayoutParameters.Height).ToString();
			cargarDatos.PutString("LinearTitulo", LinearTitulo);
			//Console.WriteLine("LinearTitulo:" + LinearTitulo);

			///VERSION
			Context context = this.ApplicationContext;
			//var name = context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName;
			var code = context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionCode;
			//string miVersion = GetString(Resource.String.miVersion);
			string miVersion = code.ToString().Trim();
			cargarDatos.PutString("miVersion",miVersion );
			cargarDatos.Apply();


			//si datoconAuto="" es porque recien se creo la cuenta o porque se borraron los datos del usuario 
			//(o porque se borro el dato para elegir otro)
			//por eso, averiguar si el usuario ya tenia cargado un auto
			string dmiauto_id = misDatos.GetString("miauto_id", "");
			Console.WriteLine("dmiato_id:" + dmiauto_id);
			//si no tenia cargado, seguir.
			//si tenia cargado, leer el id del auto y buscar esos datos en BD de autos.
			//cargar esos datos en preferences (id, nombre, precio, url1 y descargar la imagen fotoauto
			//modulo en onResume de fcuenta que lea los datos d ela web del auto, para controlar q estan actualizados
			if (dmiauto_id.ToString().Trim() != "" || dmiauto_id.ToString().Trim() != "0")
            {
				datoconAuto = dmiauto_id;
			}



			if (datoconAuto == "") {
				SetUpViewPager(viewPager);
			}
			else {
				setupViewPager2(viewPager);
			}


			tabs.SetupWithViewPager(viewPager);

			//string tabSel2 = Intent.GetStringExtra("tabSel2") ?? "";


			if (tempTab2 == "1") {
				//TabLayout.Tab tab = tabLayout.getTabAt(someIndex);
				TabLayout.Tab tabSelect = tabs.GetTabAt(1);
				tabSelect.Select();
				//ISharedPreferencesEditor cargarDatos = misDatos.Edit();
				cargarDatos.PutString("tempTab2", "");
				cargarDatos.Apply();
			}
			/////TEMP
			//string u = Intent.GetStringExtra("u");

			//ANIMACIONES
			//Window.Attributes.WindowAnimations = Resource.Style.animacionesRefe;
			Window.Attributes.WindowAnimations = Resource.Style.animacionesRefe;
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
			///ajustar tamaño (alto) de imAuto
			var metrics = Application.Context.Resources.DisplayMetrics;
			Console.WriteLine("tabs.LayoutParameters.Height" + tabs.LayoutParameters.Height);
			if (metrics.HeightPixels < 500) {
				tabs.LayoutParameters.Height = (tabs.LayoutParameters.Height) / 2;
				Console.WriteLine("CAMBIO ALTURA TABS");
				Console.WriteLine("tabs.LayoutParameters.Height" + tabs.LayoutParameters.Height);
			}




			btnMenu.Click += (s, o) => {
				//------------------------------------------
				Context wrapper = new ContextThemeWrapper(Application.Context, Resource.Style.menuTheme1);
				/*
				//Context wrapper = new ContextThemeWrapper(Application.Context,resou);
				PopupMenu menu = new PopupMenu(wrapper, btnMenu);
				//PopupMenu menu = new PopupMenu(this, btnMenu, GravityFlags.AxisClip, 0,0);

				// with Android 3 need to use MenuInfater to inflate the menu
				//menu.MenuInflater.Inflate (Resource.Layout.menu, menu.Menu);

				// with Android 4 Inflate can be called directly on the menu
				menu.Inflate(Resource.Layout.menu);
				*/

				PopupMenu menu = new PopupMenu(wrapper, btnMenu);

				Field field = menu.Class.GetDeclaredField("mPopup");
				field.Accessible = true;
				Java.Lang.Object menuPopupHelper = field.Get(menu);
				Method setForceIcons = menuPopupHelper.Class.GetDeclaredMethod("setForceShowIcon", Java.Lang.Boolean.Type);
				setForceIcons.Invoke(menuPopupHelper, true);

				//puMenu.Inflate(Resource.Menu.your_actions);
				menu.Inflate(Resource.Layout.menu);
				//puMenu.Show();

				menu.MenuItemClick += (s1, arg1) => {
					string tituloMenuSeleccionado = arg1.Item.TitleFormatted.ToString();
					//Console.WriteLine("{0} selected", arg1.Item.TitleFormatted);
					Console.WriteLine("ITEM:" + tituloMenuSeleccionado);
					string titulo1 = GetString(Resource.String.menuitem1); //CONSULTA
					string titulo2 = GetString(Resource.String.menuitem2); //	"Acerca de esta App";
					string titulo3 = GetString(Resource.String.menuitem3); //CAMBIAR CONTRASEÑA
					string titulo4 = GetString(Resource.String.menuitem4);//"Logout";

					/// /////////////////////////CONSULTA
					if (tituloMenuSeleccionado == titulo1) {
						Android.App.FragmentTransaction ft = this.FragmentManager.BeginTransaction();
						//Remove fragment else it will crash as it is already added to backstack
						Android.App.Fragment prev = this.FragmentManager.FindFragmentByTag("dialogConsulta");
						if (prev != null) {
							ft.Remove(prev);
						}
						ft.AddToBackStack(null);
						// Create and show the dialog.
						//dialogOKclass newFragment = dialogOKclass.NewInstance(null, "Solicitud registrada", "Un asesor se comunicará con usted en las próximas horas.");
						dialogConsulta newFragmentContactar = dialogConsulta.NewInstance(null, "Consulta", "Mensaje para MiAutoPlan:");
						//Add fragment
						newFragmentContactar.Show(ft, "dialogConsulta");
					}
					///LOGOUTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT
					if (tituloMenuSeleccionado == titulo4) {
						Android.App.FragmentTransaction ft = this.FragmentManager.BeginTransaction();
						//Remove fragment else it will crash as it is already added to backstack
						Android.App.Fragment prev = this.FragmentManager.FindFragmentByTag("dialogLogout");
						if (prev != null) {
							ft.Remove(prev);
						}
						ft.AddToBackStack(null);
						dialogOKCancelarclass newFragmentCorregir = dialogOKCancelarclass.NewInstance(null, "Cerrar sesión", "¿Está seguro de cerrar sesión en este dispositivo?", 3);
						newFragmentCorregir.Show(ft, "dialogLogout");

					}
					/// /////////////////////////ACERCA DE...
					if (tituloMenuSeleccionado == titulo2) {
						//Toast.MakeText(this, "Pantalla Acerca de...", ToastLength.Long).Show();
						var progressDialog = ProgressDialog.Show(this, "", "Cargando...", true);
						new System.Threading.Thread(new ThreadStart(delegate {
							string AcercaDe = solicitudesWeb.getVariable("AcercaDe");
							RunOnUiThread(() => {
								progressDialog.Hide();
								if (AcercaDe != "SinConexion") {
									//Dismiss();
									Android.App.FragmentTransaction ft = this.FragmentManager.BeginTransaction();
									Android.App.Fragment prev = this.FragmentManager.FindFragmentByTag("AcercaDe");
									if (prev != null) {
										ft.Remove(prev);
									}
									ft.AddToBackStack(null);
									dialogPedirAuto newFragmentContactar = dialogPedirAuto.NewInstance(null, AcercaDe, "AcercaDe");
									newFragmentContactar.Show(ft, "AcercaDe");
								}
								else {
									RunOnUiThread(() => {
										//Dismiss();
										Toast.MakeText(this, "sin conexión", ToastLength.Long).Show();
									});
								}
							});
						})).Start();
					}
					///Cambiar contraseña
					if (tituloMenuSeleccionado == titulo3) {
						//Toast.MakeText(this, "Pantalla CAMBIAR CONTRASEÑA...", ToastLength.Long).Show();
						///CARGAR DATOSSSSS
						misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
						string num = misDatos.GetString("num", "").ToString().Trim();
						string fid = misDatos.GetString("fid", "").ToString().Trim();

						//ENTRO CON FACEBOOK
						if (fid != "") {
							Android.App.FragmentTransaction ft2 = this.FragmentManager.BeginTransaction();
							//Remove fragment else it will crash as it is already added to backstack
							Android.App.Fragment prev2 = this.FragmentManager.FindFragmentByTag("dialogOK100");
							if (prev2 != null) {
								ft2.Remove(prev2);
							}
							ft2.AddToBackStack(null);
							// Create and show the dialog.
							//dialogOKclass newFragment = dialogOKclass.NewInstance(null, "Solicitud registrada", "Un asesor se comunicará con usted en las próximas horas.");
							dialogOKclass newFragmentContactar = dialogOKclass.NewInstance(null, "Sin Contraseña", "Su cuenta de MiAutoPlan está asociada a su cuenta de Facebook, por lo tanto no requiere contraseña para ingresar.");
							//Add fragment
							newFragmentContactar.Show(ft2, "dialogOK100");
							//Dismiss();
						}
						else {
							Android.App.FragmentTransaction ft = this.FragmentManager.BeginTransaction();
							//Remove fragment else it will crash as it is already added to backstack
							Android.App.Fragment prev = this.FragmentManager.FindFragmentByTag("dialogContrasena");
							if (prev != null) {
								ft.Remove(prev);
							}
							ft.AddToBackStack(null);
							// Create and show the dialog.
							dialogCambiarContrasena newFragment = dialogCambiarContrasena.NewInstance(null);
							//Add fragment
							newFragment.Show(ft, "dialogContrasena");
						}
					}

				};

				// Android 4 now has the DismissEvent
				menu.DismissEvent += (s2, arg2) => {
					Console.WriteLine("menu dismissed");
				};

				menu.Show();
				//------------------------------------------
			};
		}
		public override void OnBackPressed() {
			base.OnBackPressed();
			//Console.WriteLine( this.ComponentName);
			//this.Finish();
		}
		protected override void OnDestroy() {
			base.OnDestroy();
			//Finish();
		}
		protected override void OnStop() {
			base.OnStop();

		}

		private void SetUpViewPager(ViewPager viewPager) {
			adapter = null;
			adapter = new TabAdapter(SupportFragmentManager);
			adapter.AddFragment(new fActividad(), "NOTICIAS");
			adapter.AddFragment(new fCuentaSinAuto(), "MI CUENTA");
			adapter.AddFragment(new fAutos(), "AUTOS");
			//adapter.AddFragment(new refePromo(), "PROMO");
			viewPager.Adapter = adapter;
		}

		public void setupViewPager2(ViewPager viewPager) {
			adapter = new TabAdapter(SupportFragmentManager);
			adapter.AddFragment(new fActividad(), "NOTICIAS");
			adapter.AddFragment(new fCuenta(), "MI CUENTA");
			//adapter.AddFragment(new refePromo(), "PROMOCIONES");
			adapter.AddFragment(new fAutos(), "CATÁLOGO");
			//adapter.AddFragment(new refePromo(), "PROMO");
			viewPager.Adapter = adapter;
		}

		public void cambiarViewPager() {
			//setupViewPager2();
			//adapter.cambiar();
			//viewPager.Adapter = adapter2;


		}




		public override bool OnOptionsItemSelected(IMenuItem item) {


			return base.OnOptionsItemSelected(item);

		}



		public class TabAdapter : FragmentPagerAdapter {
			//public class TabAdapter : FragmentStatePagerAdapter {
			public List<SupportFragment> Fragments { get; set; }
			public List<string> FragmentNames { get; set; }

			public TabAdapter(Android.Support.V4.App.FragmentManager sfm) : base(sfm) {
				Fragments = new List<SupportFragment>();
				FragmentNames = new List<string>();
			}

			public void cambiar() {
				//FragmentNames[1] = "hola";
				//Fragments[1] = new refePromo();

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