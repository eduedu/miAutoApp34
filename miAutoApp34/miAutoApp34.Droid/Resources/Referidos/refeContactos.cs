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


namespace miAutoApp34.Droid {
	public class refeContactos : SupportFragment {
		private List<itemContacto> mContactos;
		private miAdapter adapterContactos;
		//private List<Linea> mItems;
		EditText filtroText;
		ListView mListView;
		public Android.Graphics.Typeface fntBlack;
		public Android.Graphics.Typeface fntRegular;
		///DATOS
		ISharedPreferences misDatos;
		string mNum;
		string mNya;
		string mAyudaReferidos;

		public override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			/// Datos
			misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			mNum = misDatos.GetString("num", "");
			mNya = misDatos.GetString("nya", "");

			// Create your fragment here
			var phoneuri = ContactsContract.CommonDataKinds.Phone.ContentUri;

			string[] phoneprojection =
			{
								ContactsContract.Contacts.InterfaceConsts.Id,
								ContactsContract.Contacts.InterfaceConsts.DisplayName,
								ContactsContract.CommonDataKinds.Phone.Number,
								//ContactsContract.Contacts.InterfaceConsts.PhotoId
								ContactsContract.Contacts.InterfaceConsts.PhotoThumbnailUri
						};
			var phoneloader = new CursorLoader(Application.Context, phoneuri, phoneprojection, null, null, null);
			var phonecursor = (ICursor)phoneloader.LoadInBackground();
			mContactos = new List<itemContacto>();
			if (phonecursor.MoveToFirst()) {
				do {
					string tempNumero = phonecursor.GetString(phonecursor.GetColumnIndex(phoneprojection[2]));
					tempNumero = tempNumero.Replace(" ", string.Empty);
					tempNumero = tempNumero.Replace("-", string.Empty);
					if (tempNumero.Length > 4) {
						if (tempNumero.Substring(0, 4) == "+549") {
							tempNumero = tempNumero.Substring(4, tempNumero.Length - 4);
						}
					}
					mContactos.Add(new itemContacto() {
						nombre = phonecursor.GetString(phonecursor.GetColumnIndex(phoneprojection[1])),
						numero = tempNumero,
						//id = phonecursor.GetString(phonecursor.GetColumnIndex(phoneprojection[0])),
						id = phonecursor.GetLong(phonecursor.GetColumnIndex(phoneprojection[0])),
						photoid = phonecursor.GetString(phonecursor.GetColumnIndex(phoneprojection[3])),
						selec = 0
					});
				} while (phonecursor.MoveToNext());
			}
			///ORDENAR CONTACTOS
			List<itemContacto> ordenados;
			ordenados = (from item in mContactos
									 orderby item.nombre
									 select item).ToList<itemContacto>();
			mContactos = ordenados;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
			View view = inflater.Inflate(Resource.Layout.refeContactos, container, false);
			///FUENTES
			//Android.Graphics.Typeface 
			fntRegular = Android.Graphics.Typeface.CreateFromAsset(inflater.Context.Assets, "fonts/ROBOTO-REGULAR.TTF");
			Android.Graphics.Typeface fntBold = Android.Graphics.Typeface.CreateFromAsset(inflater.Context.Assets, "fonts/ROBOTOCONDENSED-BOLD.TTF");
			fntBlack = Android.Graphics.Typeface.CreateFromAsset(inflater.Context.Assets, "fonts/ROBOTO-BLACK.TTF");


			///REFERENCIAS A CONTROLES

			TextView texto1 = view.FindViewById<TextView>(Resource.Id.textView1);
			texto1.Typeface = fntBold;
			TextView texto2 = view.FindViewById<TextView>(Resource.Id.textView2);
			texto2.Typeface = fntBold;
			Button btnAgregar = view.FindViewById<Button>(Resource.Id.btnAgregar);
			Button btnConsultar = view.FindViewById<Button>(Resource.Id.btnConsultar);
			//texto1.Text = "¡GANÁ UNA LLAVE GRATIS por cada uno de tus contactos que se registre en miAutoPlan!\n(click para ver condiciones)";
			//texto1.Text = "¡GANÁ UNA LLAVE GRATIS por cada contacto que se registre en miAutoPlan!\n(click para ver condiciones)";



			texto1.Click += (object sender, EventArgs args) => {
				FragmentTransaction ft = Activity.FragmentManager.BeginTransaction();
				//Remove fragment else it will crash as it is already added to backstack
				Fragment prev = Activity.FragmentManager.FindFragmentByTag("dialog");
				if (prev != null) {
					ft.Remove(prev);
				}
				ft.AddToBackStack(null);
				// Create and show the dialog.
				dialogOKclass newFragment = dialogOKclass.NewInstance(null, "Condiciones", "Los números de los contactos que selecciones se almacenarán en nuestra base de datos.\nSe te adjudicará UNA LLAVE GRATIS a tu plan, sólo si se cumplen las siguientes condiciones:\n1) El nuevo usuario se REGISTRA y ABONA la primer cuota.\n2) El número de teléfono del Nuevo Usuario coincide con el Número Referido. \n3) Fuiste el PRIMERO en referir a ese Usuario, según el registro en nuestra base de datos.\nSi no se cumplen todas las condiciones o se detecta algún tipo de uso deshonesto a criterio de la Empresa, quedará sin efecto la promoción.");
				//Add fragment
				newFragment.Show(ft, "dialog");
			};

			///BOTON CONSULTAR
			btnConsultar.Click += (o, s) => {
				var progressDialog = ProgressDialog.Show(Context, "", "Consultando base de datos...", true);
				new System.Threading.Thread(new ThreadStart(delegate {
					//bool solicitudOK = solicitudesWeb.solicitud("Asesor");
					//string tmpNumeroWA = solicitudesWeb.getVariable("numeroWA");
					string tmpGetReferidos = solicitudesWeb.getReferidos(mNum);
					string[] referidosArray = tmpGetReferidos.Split('[');
					string referidosFinal = "";
					int j = 1;
					foreach (string tmpRefeItem in referidosArray) {
						if (tmpRefeItem != "") {
							referidosFinal = referidosFinal + j.ToString() + ") " + tmpRefeItem + "\n";
							j++;
						}
					}

					Activity.RunOnUiThread(() => {
						progressDialog.Hide();
						Console.WriteLine("Referidos: " + tmpGetReferidos);
						if (tmpGetReferidos != "-1") {
							FragmentTransaction ft2 = Activity.FragmentManager.BeginTransaction();
							//Remove fragment else it will crash as it is already added to backstack
							Fragment prev = Activity.FragmentManager.FindFragmentByTag("dialogConsultar");
							if (prev != null) {
								ft2.Remove(prev);
							}
							ft2.AddToBackStack(null);
							// Create and show the dialog.
							dialogOKrefefidos newFragment = dialogOKrefefidos.NewInstance(null, "Mis Referidos", referidosFinal);
							//Add fragment
							newFragment.Show(ft2, "dialogConsultar");
						}
						else {
							Activity.RunOnUiThread(() => Toast.MakeText(inflater.Context, "sin conexión", ToastLength.Long).Show());
						}

					});
				})).Start();


			};

			/// BOTON AGREGAR
			btnAgregar.Click += (o, s) => {

				string textotemp = "";
				string contactosUrlString = "";

				foreach (itemContacto tmpContacto in mContactos) {
					if (tmpContacto.selec == 1) {
						textotemp = textotemp + tmpContacto.nombre + " \t" + tmpContacto.numero + "\n";
						contactosUrlString = contactosUrlString + tmpContacto.nombre + "[" + tmpContacto.numero + "]";
					}
				}
				//Console.WriteLine(textotemp);
				Console.WriteLine("URLSSS: -" + contactosUrlString + "-");
				if (contactosUrlString != "") {
					var progressDialog2 = ProgressDialog.Show(Context, "", "Procesando...", true);
					new System.Threading.Thread(new ThreadStart(delegate {
						contactosUrlString = contactosUrlString.Substring(0, contactosUrlString.Length - 1);
						contactosUrlString = mNya + "[" + mNum + "]" + contactosUrlString;
						string seAgregaronOk = solicitudesWeb.setReferidos(contactosUrlString);
						Activity.RunOnUiThread(() => {
							if (seAgregaronOk == "1") {
								progressDialog2.Hide();
								FragmentTransaction ft2 = Activity.FragmentManager.BeginTransaction();
								//Remove fragment else it will crash as it is already added to backstack
								Fragment prev = Activity.FragmentManager.FindFragmentByTag("dialog");
								if (prev != null) {
									ft2.Remove(prev);
								}
								ft2.AddToBackStack(null);
								// Create and show the dialog.
								dialogOKrefefidos newFragment = dialogOKrefefidos.NewInstance(null, "Referidos", "Se agregaron los siguientes Contactos a tu lista de Referidos:\n" + textotemp);
								//Add fragment
								newFragment.Show(ft2, "dialog");

							}
						});
					})).Start();
				}
				else {
					MostrarAyuda();
				}


			};

			/*
			TextView valorllave = view.FindViewById<TextView>(Resource.Id.textView1);
			valorllave.Typeface = fntBlack;
			Button btnReferidos = view.FindViewById<Button>(Resource.Id.button1);
			btnReferidos.Typeface = fntRegular;
			mListView = view.FindViewById<ListView>(Resource.Id.lis);
			*/
			mListView = view.FindViewById<ListView>(Resource.Id.listView1);



			adapterContactos = new miAdapter(Activity, Resource.Layout.refeContactItem, mContactos);
			mListView.Adapter = adapterContactos;

			///BOTON FILTRO
			filtroText = view.FindViewById<EditText>(Resource.Id.editText1);
			filtroText.TextChanged += FiltroText_TextChanged;

			///DIALOG AYUDA
			/// Datos
			misDatos = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
			mAyudaReferidos = misDatos.GetString("ayudaReferidos", "");
			if (mAyudaReferidos == "1") {
				MostrarAyuda();
				ISharedPreferencesEditor tmpCargarDatos = misDatos.Edit();
				tmpCargarDatos.PutString("ayudaReferidos", "0");
				tmpCargarDatos.Apply();
			}

			return view;
			// return base.OnCreateView(inflater, container, savedInstanceState);
		}

		private void FiltroText_TextChanged(object sender, Android.Text.TextChangedEventArgs e) {

			List<itemContacto> filtrados = (from itemContacto in mContactos
																			where itemContacto.nombre.Contains(filtroText.Text, StringComparison.OrdinalIgnoreCase)
																			select itemContacto).ToList<itemContacto>();
			adapterContactos = new miAdapter(Activity, Resource.Layout.refeContactItem, filtrados);
			mListView.Adapter = adapterContactos;
			//throw new NotImplementedException();


		}
		private void MostrarAyuda() {
			FragmentTransaction ft2 = Activity.FragmentManager.BeginTransaction();
			//Remove fragment else it will crash as it is already added to backstack
			Fragment prev = Activity.FragmentManager.FindFragmentByTag("dialogAyuda1");
			if (prev != null) {
				ft2.Remove(prev);
			}
			ft2.AddToBackStack(null);
			// Create and show the dialog.
			//dialogAyudaReferidos newFragment = dialogAyudaReferidos.NewInstance(null, "Referidos", "Seleccione los Contactos que desee agregar a su Lista de Referidos y presione el botón Agregar (+)");
			dialogAyudaReferidos newFragment = dialogAyudaReferidos.NewInstance(null);
			//Add fragment
			newFragment.Show(ft2, "dialogAyuda1");
		}

	}
}