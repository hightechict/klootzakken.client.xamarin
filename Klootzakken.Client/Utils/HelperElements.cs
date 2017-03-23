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
using Microsoft.Practices.ServiceLocation;
using Klootzakken.Client.App.Authentication;

namespace Klootzakken.Client.Utils
{
    class HelperElements
    {

        public void addPoup()
        {
            /*
            button.Click += delegate
                {
                    PopupMenu menu = new PopupMenu(this, generatePinButton);
                    menu.Inflate(Resource.Layout.pinCode);
                    menu.Show();
                };
                */
            //where pincode:
            /*
             * <?xml version="1.0" encoding="utf-8"?>

                <menu xmlns:android="http://schemas.android.com/apk/res/android" >
                    <item
                        android:id="@+id/item1"
                        android:title="GeneratedPinCode7788"/>
                </menu>*/
        }

        public void getSharedPreference()
        {
            /*
            //retreive 
            var prefs = Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);
            var somePref = prefs.GetString("bearerToken", null);
            */


        }

        public void createToast()
        {
            //Show a toast
            // RunOnUiThread(() => Toast.MakeText(this, somePref, ToastLength.Long).Show());*/
        }

        public class DialogGeneratePin : DialogFragment
        {
            private string _pinCode;


            /* HOW TO CALL IT FROM AN ACTIVITY
            FragmentTransaction ft = FragmentManager.BeginTransaction();
            //Remove fragment else it will crash as it is already added to backstack
            Fragment prev = FragmentManager.FindFragmentByTag("dialog");
            if (prev != null)
            {
                ft.Remove(prev);
            }

            ft.AddToBackStack(null);

            // Create and show the dialog.
            DialogGeneratePin newFragment = DialogGeneratePin.NewInstance(null);

            //Add fragment
            newFragment.Show(ft, "dialog");*/


            public void AddGeneratePinDialogCLass()
            {
                /*
            public DialogGeneratePin NewInstance(Bundle bundle, string pinCode)
            {
                _pinCode = pinCode;
                DialogGeneratePin fragment = new DialogGeneratePin();
                fragment.Arguments = bundle;
                return fragment;
            }

            public override Dialog OnCreateDialog(Bundle savedInstanceState)
            {
                var authenticationController = ServiceLocator.Current.GetInstance<AuthenticationController>();

                AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
                alert.SetTitle("Generated PinCode");
                alert.SetMessage("ss");
                alert.SetPositiveButton("Login", (senderAlert, args) => {
                    //TODO: here invoke the logic for getting the bearer token
                    Toast.MakeText(Activity, "Auth logic is aangetrapt!", ToastLength.Short).Show();
                });

                alert.SetNegativeButton("Cancel", (senderAlert, args) => {
                    Toast.MakeText(Activity, "Pin code is no longer valid", ToastLength.Short).Show();
                });


                return alert.Create();
            }
            */
            }
        }
    }
}
