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
    [Activity(Label = "Date")]
    public class AddTask : Activity
    {
        TextView _dateDisplay;
        Button _dateSelectButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddTask);

           
            EditText NameText = FindViewById<EditText>(Resource.Id.editText1);
            EditText DescriptionText = FindViewById<EditText>(Resource.Id.editText2);
            CheckBox HighCheckBox = FindViewById<CheckBox>(Resource.Id.checkBox4);
            CheckBox MediumCheckBox = FindViewById<CheckBox>(Resource.Id.checkBox5);
            CheckBox LowCheckBox = FindViewById<CheckBox>(Resource.Id.checkBox6);
            Button AddButton = FindViewById<Button>(Resource.Id.AddButton);
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite");
            var db = new ApplicationContext(dbPath);

            _dateDisplay = FindViewById<TextView>(Resource.Id.date_display);
            _dateDisplay.Text = DateTime.Now.ToLongDateString();
            _dateSelectButton = FindViewById<Button>(Resource.Id.date_select_button);
            _dateSelectButton.Click += DateSelect_OnClick;

            int Priority = 0;
            HighCheckBox.Click += (sender, e) =>
            {
                HighCheckBox.Checked = true;
                MediumCheckBox.Checked = false;
                LowCheckBox.Checked = false;
                Priority = 1;
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
                if (NameText.Text.Replace(" ", "") == "" | DescriptionText.Text.Replace(" ", "") == "" | Priority == 0)
                {
                    Toast toast = Toast.MakeText(this, "Need to fill in all fields", ToastLength.Short);
                    toast.Show();
                }
                else
                {
                    if (IsSet(NameText.Text))
                    {
                        Toast toast = Toast.MakeText(this, "This name is already taken", ToastLength.Short);
                        toast.Show();
                    }
                    else
                    {
                        db.Tasks.Add(new Task { Name = NameText.Text, Description = DescriptionText.Text, Date = _dateDisplay.Text, Priority = Priority });
                        db.SaveChanges();
                        this.Finish();
                    }
                }

            };

        }

        void DateSelect_OnClick(object sender, EventArgs eventArgs)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time) {
                _dateDisplay.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        public bool IsSet(string Text)
        {
            bool a;
            List<Task> Tasks;
            string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite");
            using (ApplicationContext db = new ApplicationContext(dbPath))
            {
                Tasks = db.Tasks.ToList();
            }
            a = Tasks.Any(u => String.Equals(u.Name, Text));

            return a;
        }
    }
}