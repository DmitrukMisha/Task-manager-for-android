using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Task_Manager.Models;

namespace Task_Manager
{
    [Activity(Label = "AddUserPage")]
    public class AddUserPage : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddUserPage);
            EditText LoginText = FindViewById<EditText>(Resource.Id.editText1);
            EditText PasswordText = FindViewById<EditText>(Resource.Id.editText2);
            EditText NameText = FindViewById<EditText>(Resource.Id.editText3);
            CheckBox AdminCheckBox = FindViewById<CheckBox>(Resource.Id.checkBox1);
            CheckBox ManagerCheckBox = FindViewById<CheckBox>(Resource.Id.checkBox2);
            CheckBox UserCheckBox = FindViewById<CheckBox>(Resource.Id.checkBox3);
            CheckBox HighCheckBox = FindViewById<CheckBox>(Resource.Id.checkBox4);
            CheckBox MediumCheckBox = FindViewById<CheckBox>(Resource.Id.checkBox5);
            CheckBox LowCheckBox = FindViewById<CheckBox>(Resource.Id.checkBox6);
            Button AddButton = FindViewById<Button>(Resource.Id.AddButton);
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite");
            var db = new ApplicationContext(dbPath);
            string RoleText = "";
            
            AdminCheckBox.Click += (sender, e) =>
             {
                 AdminCheckBox.Checked = true;
                 ManagerCheckBox.Checked = false;
                 UserCheckBox.Checked = false;
                 RoleText = "Admin";
             };
            ManagerCheckBox.Click += (sender, e) =>
            {
                AdminCheckBox.Checked = false;
                ManagerCheckBox.Checked = true;
                UserCheckBox.Checked = false;
                RoleText = "Manager";
            };
            UserCheckBox.Click += (sender, e) =>
            {
                AdminCheckBox.Checked = false;
                ManagerCheckBox.Checked = false;
                UserCheckBox.Checked = true;
                RoleText = "User";
            };

            int Priority = 0;
            HighCheckBox.Click += (sender, e) =>
            {
                HighCheckBox.Checked = true;
                MediumCheckBox.Checked = false;
                LowCheckBox.Checked = false;
                Priority=1;
            };
            MediumCheckBox.Click += (sender, e) =>
            {
                HighCheckBox.Checked = false;
                MediumCheckBox.Checked = true;
                LowCheckBox.Checked = false;
                Priority = 2;
            };
            LowCheckBox.Click += (sender, e) =>
            {
                HighCheckBox.Checked = false;
                MediumCheckBox.Checked = false;
                LowCheckBox.Checked = true;
                Priority = 3;
            };

            AddButton.Click += (sender, e) =>
            {
                if(LoginText.Text.Replace(" ", "") =="" | PasswordText.Text.Replace(" ", "") == "" | NameText.Text.Replace(" ", "") == ""  | RoleText=="" & Priority==0) {
                    Toast toast = Toast.MakeText(this, "Need to fill in all fields", ToastLength.Short);
                    toast.Show();
                }
                else
                {
                    if (IsSet(LoginText.Text))
                    {
                        Toast toast = Toast.MakeText(this, "This login is already taken", ToastLength.Short);
                        toast.Show();
                    }
                    else
                    {
                        db.Users.Add(new User { Login = LoginText.Text, Name = NameText.Text, Password = PasswordText.Text, Role = RoleText, Priority = Priority });
                        db.SaveChanges();
                        this.Finish();
                    }
                }

            };

        }

        public bool IsSet(string Text)
        {
            bool a;
            List<User> Users;
            string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite");
            using (ApplicationContext db = new ApplicationContext(dbPath))
            {
                Users = db.Users.ToList();
            }
            a = Users.Any(u => String.Equals(u.Login, Text));

            return a;
        }
    }
}