using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using System.IO;

namespace WindowPhoneFileHandling
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void ReadFileButton_Click(object sender, RoutedEventArgs e)
        {                                                                                                                                                                      
            IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
            string[] files = file.GetFileNames();
            string[] folders = file.GetDirectoryNames();
            foreach(string s in files)
                FilesTextBlock.Text += s + "\n";
            FilesTextBlock.Text = "\nFolders\n";
            foreach (string s in folders)
                FilesTextBlock.Text += s + "\n";
            if (file.FileExists("Test/a.txt"))
            {
                FileStream stream = file.OpenFile("Test/a.txt", FileMode.Open);
                StreamReader reader = new StreamReader(stream);
                string str = reader.ReadToEnd();
                stream.Close();
                MessageBox.Show(str);
            }
            
            //to check where a key exists in storage or not we can use contains
            bool status = IsolatedStorageSettings.ApplicationSettings.Contains("username");
            if (status)
            {
                MessageBox.Show("Username: " + IsolatedStorageSettings.ApplicationSettings["username"].ToString() + "\nPassword: " +
                                IsolatedStorageSettings.ApplicationSettings["password"].ToString());
            }
            else
            {
                IsolatedStorageSettings.ApplicationSettings.Add("username", "mohsin");
                IsolatedStorageSettings.ApplicationSettings.Add("password", "123");
            }
        }

        private void CreateFolderButton_Click(object sender, RoutedEventArgs e)
        {
            //every app os create a storage so by this we are accessing that location
            IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
            if (!file.DirectoryExists("Test"))
            {
                file.CreateDirectory("Test");
                MessageBox.Show("Folder Created!");
            }
            else
            {
                MessageBox.Show("Folder Exists already!");
            }
        }

        private void CreateFileButton_Click(object sender, RoutedEventArgs e)
        {
            IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
            //Filestream is not used for writing file
            if (file.FileExists("Test/a.txt"))
            {
                FileStream stream = file.OpenFile("Test/a.txt", FileMode.Create);
                //for writing file we have to use stream writer
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine("Hello, I am a windows phone developer!");
                writer.Flush();
                stream.Close();
                MessageBox.Show("Created!");
            }
            else
            {
                MessageBox.Show("Already exists!");
            }
        }
    }
}