using System.Collections.Generic;
using System.IO;
using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Task_Manager.Models;

namespace Task_Manager
{


    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class login : Activity
    {
        
        public string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite");
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);

            
            using (var db = new ApplicationContext(dbPath))
            {
                // Создаем бд, если она отсутствует
               // db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                if (db.Users.Count() == 0)
                {
                    db.Users.Add(new User { Login = "Misha", Name = "Misha Dmitruk", Password = "1234qwer", Role = "Admin", Priority = 1 });
                    db.SaveChanges();
                }
            }

           

            EditText LoginText = FindViewById<EditText>(Resource.Id.login);
            EditText PasswordText = FindViewById<EditText>(Resource.Id.password);
            Button LoginButton = FindViewById<Button>(Resource.Id.LoginButton);

           
             
            LoginButton.Click += (sender, e) =>
            {
                bool enter = false;
                List<User> UsersList = GetUsers();
                foreach (User user in UsersList)
                {
                    if (user.Login.ToLower() == LoginText.Text.ToLower() & user.Password==PasswordText.Text)
                    {
                        enter = true;
                        var intent = new Intent(this, typeof(MainActivity));
                        intent.PutExtra("Login", user.Login);
                        intent.PutExtra("Priority", user.Priority);
                        intent.PutExtra("Role", user.Role);
                        StartActivity(intent);
                    }
                }
                if (!enter)
                {
                    Toast toast = Toast.MakeText(this, "Неверный логин или пароль", ToastLength.Short);
                    toast.Show();
                }
            };

        }

        public List<User> GetUsers()
        {
            List<User> Users;
            using (ApplicationContext db = new ApplicationContext(dbPath))
            {
                Users = db.Users.ToList();
            }
            return Users;
        }
    }




}