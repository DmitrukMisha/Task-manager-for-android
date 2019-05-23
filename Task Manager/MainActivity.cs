using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Task_Manager.Models;

namespace Task_Manager
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
       
       // String[] countries = { "Бразилия", "Аргентина", "Колумбия", "Чили", "Уругвай" };
        public string UserLogin;
        public int Priority;
        Bundle args;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            UserLogin = Intent.GetStringExtra("Login");
            Priority = Intent.GetIntExtra("Priority", 3);
            string Role = Intent.GetStringExtra("Role");
            Android.Support.V4.App.Fragment fragment = null;
            args = new Bundle();
            args.PutString("Login", UserLogin);
            args.PutInt("Priority", Priority);
            fragment = new Home();
            if (fragment != null)
            {
                fragment.Arguments = args;
                SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, fragment).Commit();
            }

            
            SetContentView(Resource.Layout.activity_main);
           
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
            Navigation_Visibility(navigation, Role);


        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
          
            Android.Support.V4.App.Fragment fragment = null;
            
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    fragment = new Home();
                    break;
                case Resource.Id.navigation_AddTask:
                    //fragment = new AddTasks();
                    var intent = new Intent(this, typeof(AddTask));
                    StartActivity(intent);
                    break;
                case Resource.Id.navigation_AddUser:
                    fragment = new AddUsers();
                    break;
            }
            if (fragment != null)
            {
                fragment.Arguments = args;
                SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, fragment).Commit();
            }
            return false;
        }

        public void Navigation_Visibility(BottomNavigationView navigation, string Role)
        {
            if(String.Equals(Role, "Manager"))
            {
                var b = navigation.FindViewById(Resource.Id.navigation_AddUser);
                b.Visibility = ViewStates.Invisible;
            }
            if (String.Equals(Role, "User"))
            {
                var b = navigation.FindViewById(Resource.Id.navigation_AddUser);
                b.Visibility = ViewStates.Invisible;
                var c = navigation.FindViewById(Resource.Id.navigation_AddTask);
                c.Visibility = ViewStates.Invisible;
            }
        }

    }
}

