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

namespace miAutoApp34.Droid
{
    [Activity(Label = "Activity: registrarse1")]
    public class registrarse1 : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.registrarse1);
            // Create your application here
        }
    }
}