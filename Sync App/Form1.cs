using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text.RegularExpressions;
using System.Diagnostics;
namespace Sync_App
{
    public partial class Form1 : Form
    {
        string parent_main_directory = "";  //path of folder containing entire FTP folders  (TEMP)
        string final_main_directory = "";   //path of folder containing entire FTP folders  (FINAL)
        List<string> existing_files;
        NotifyIcon notifyIcon;
        FileSystemWatcher fsw = new FileSystemWatcher();
        List<string> list_unwanted_directories = new List<string>();    //will contain all unwanted paths
        public Form1()
        {
            InitializeComponent();
        }
        private void browse_parent_btn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                parent_textbox.Text = fbd.SelectedPath;
            }
        }
        private void browse_final_btn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                final_textbox.Text = fbd.SelectedPath;
            }
        }
        private void browse_logs_btn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                logs_textbox.Text = fbd.SelectedPath;
            }
        }
        private void start_BTN_Click(object sender, EventArgs e)
        {
            timer1 = new System.Windows.Forms.Timer();      //will be used to retry copying already existing files every x seconds
            timer1.Tick += new EventHandler(timer1_Tick);  
            timer1.Interval = 300000; // in miliseconds (5 minutes)
            timer1.Start();

            if (parent_textbox.Text == "")
            {
                MessageBox.Show("Enter valid Parent Directory");
                return;
            }
            if (final_textbox.Text == "")
            {
                MessageBox.Show("Enter valid Final Directory");
                return;
            }
            if (logs_textbox.Text == "")
            {
                MessageBox.Show("Enter valid directory for logs");
                return;
            }
            parent_main_directory = parent_textbox.Text;    //initialize parent main directory (TEMP)
            final_main_directory = final_textbox.Text;      //initialize parent main directory (FINALS)
            existing_files = dirSearch(parent_main_directory); //get all previously existing files in entire directory 

            foreach (string ThisFile in existing_files) //for every found file
            {
                string FilePath = Path.GetDirectoryName(ThisFile);  //file path
                string NameOnly = Path.GetFileName(ThisFile);   //file name
                FileSystemEventArgs FSEAe = new FileSystemEventArgs(WatcherChangeTypes.Created, FilePath, NameOnly);    //create filesystemeventargs for that file
                file_created(this, FSEAe);  //fire event handler
            }

            parent_textbox.Enabled = false; //design
            final_textbox.Enabled = false;
            logs_textbox.Enabled = false;
            start_BTN.Enabled = false;
            stop_BTN.Enabled = true;
            exclude_btn.Enabled = false;
            browse_logs_btn.Enabled = false;
            browse_final_btn.Enabled = false;
            browse_parent_btn.Enabled = false;

            fsw.Path = parent_main_directory;   //file system watcher is to watch over the entire directory
            fsw.IncludeSubdirectories = true;   
            fsw.Created += new FileSystemEventHandler(file_created);    //event handler
            fsw.EnableRaisingEvents = true; //START WATCHING
        }
        private void file_created(object sender, FileSystemEventArgs e)
        {
            try
            {
                new Thread(() =>    //each detected file shall run on a separate thread
                {
                    string temp_directory = Path.GetDirectoryName(e.FullPath);  //folder in the temp directory where the file was added
                    foreach (var unwanted_directory in list_unwanted_directories)   //look in list of unwanted folders
                    {
                        string last_folder_in_unwanted_directory = unwanted_directory.Split(Path.DirectorySeparatorChar).Last();   //get last folder in the unwanted directory 
                        string bringer_of_light = temp_directory.Remove(temp_directory.IndexOf(parent_main_directory), parent_main_directory.Length) +@"\";
                        if (bringer_of_light.Contains(last_folder_in_unwanted_directory + @"\")) //if directory where new file was created contains an unwanted folder, return
                        {
                            return;
                        }
                    }
                    Thread.CurrentThread.IsBackground = true;
                    string fileNameOnly = Path.GetFileNameWithoutExtension(e.FullPath);
                    string final_name_only = "";    //will represent file name after adding date and time to it
                    string extension = Path.GetExtension(e.FullPath);   //.mp4-.avi....
                    //if (temp_directory == unwanted)
                        //return;
                    string logs_directory = logs_textbox.Text;  //where the logs will be saved
                    string current_log_file_success = logs_directory + @"\" + "Succesful Copy Logs " + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";   //full directory of txt successful copy log file to write to
                    string current_log_file_failure = logs_directory + @"\" + "Error Logs " + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";   //full directory of txt error log file to write to

                    if (!File.Exists(current_log_file_success)) //if log file does not exist (first file of the day)
                    {
                        try
                        {
                            File.Create(current_log_file_success).Close();  //create log file
                            File.Create(current_log_file_failure).Close();  //create log file
                        }
                        catch
                        {
                            Thread.Sleep(1);
                        }
                    }
                    if (extension == "")    //if an addition of a folder is detected
                        return; //return, we don't want to handle addition of folders

                    StringBuilder builder = new StringBuilder(temp_directory);  //string builder to create string representing the final directory
                    builder.Replace(parent_main_directory, final_main_directory);   //key statement: replace the parent main directory with the final main directory
                    string final_directory = builder.ToString();    //now we have the path of the folder where the file should be added in the final directory

                    FileInfo file = new FileInfo(e.FullPath);   //detected file

                    while (IsFileLocked(e.FullPath))    //if file is still copying
                        continue;   //wait

                    Thread.Sleep(30000); //wait more to ensure copying is finished and in case of network instability on sender side

                    var source = Path.Combine(temp_directory, fileNameOnly + extension);        //copy from here
                    if (IsEnglish(fileNameOnly))    //if file name is in English, add date and time at the end of the file name
                        final_name_only = fileNameOnly + DateTime.Now.ToString(" (dd-MM-yyyy)") + DateTime.Now.ToString(" (h'h'.mm'm'.ss's')"); //add date and time
                    else    //if not in English, add date and time at the beginning of the file name
                    {
                        final_name_only = DateTime.Now.ToString("(dd-MM-yyyy) ") + DateTime.Now.ToString("(h'h'.mm'm'.ss's') ") + fileNameOnly; //add date and time
                    }
                    var destination = Path.Combine(final_directory, final_name_only + extension);  //copy to here
                    if (file.Exists == true)    //if the file exists
                    {
                        try
                        {
                            File.Move(source, destination); //copy it
                            bool test1 = false; //will be used to avoid multithreading problems
                            while (test1 == false)
                            {
                                try     //try writing to the log file, if log file is being accessed by another thread, test1 will remain false and the loop will continue
                                {
                                    TextWriter tw = new StreamWriter(current_log_file_success, true);
                                    tw.WriteLine("Copied - File Source: " + temp_directory + @"\" + fileNameOnly + extension + "       " + "TIME: " + DateTime.Now.ToString("h:mm:ss")); //write record to log file
                                    tw.WriteLine("");   //write blank line to log file
                                    tw.Close(); //close writer
                                    test1 = true;   //exit loop
                                }
                                catch   //if log file is being accessed by another thread, sleep for 500ms
                                {
                                    Thread.Sleep(500);
                                }
                            }
                        }
                        catch
                        {
                            string error_Message = "";  //generated error message
                            if (!Directory.Exists(destination)) //if directory does not exist in the final destination
                                error_Message = "Destination does not exist: " + final_directory;   //update error message                         

                            bool test2 = false; //will be used to avoid multithreading problems
                            while (test2 == false)
                            {
                                try     //try writing to the log file, if log file is being accessed by another thread, test1 will remain false and the loop will continue
                                {
                                    TextWriter tw = new StreamWriter(current_log_file_failure, true);
                                    tw.WriteLine("ERROR COPYING FILE. " + "File Source: " + temp_directory + fileNameOnly + extension + "         " + error_Message + "  TIME: " + DateTime.Now.ToString("h:mm:ss"));   //write record to log file
                                    tw.WriteLine("");   //write blank line to log file
                                    tw.Close(); //close writer
                                    test2 = true;   //exit loop
                                }
                                catch   //if log file is being accessed by another thread, sleep for 500ms
                                {
                                    Thread.Sleep(500);
                                }
                            }
                            return; //if an error occured while copying, return
                        }
                    }
                }).Start();
            }
            catch
            {
                Thread.Sleep(1);
            }
        }

        private void stop_BTN_Click(object sender, EventArgs e)
        {
            fsw.EnableRaisingEvents = false;    //disable filesystemwatcher

            parent_textbox.Enabled = true;  //design
            final_textbox.Enabled = true;
            logs_textbox.Enabled = true;
            start_BTN.Enabled = true;
            stop_BTN.Enabled = false;
            exclude_btn.Enabled = true;
            browse_logs_btn.Enabled = true;
            browse_parent_btn.Enabled = true;
            browse_final_btn.Enabled = true;
        }


        public bool IsFileLocked(string filePath)   //function to detect if file is still locked (still being copied)
        {
            try
            {
                using (File.Open(filePath, FileMode.Open)) { }
            }
            catch (IOException e)
            {
                var errorCode = Marshal.GetHRForException(e) & ((1 << 16) - 1);

                return errorCode == 32 || errorCode == 33;
            }

            return false;
        }
        private List<String> dirSearch(string sDir) //function that returns all files in all subdirectoris of a given directory
        {
            List<String> files = new List<String>();
            try
            {
                foreach (string f in Directory.GetFiles(sDir))
                {
                    files.Add(f);
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    files.AddRange(dirSearch(d));
                }
            }
            catch (System.Exception excpt)
            {
                MessageBox.Show(excpt.Message);
            }

            return files;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var exists = System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1; //variable true when application is already running

            if (exists) //if application is already running
            {
                MessageBox.Show("Application already running in system tray");
                System.Environment.Exit(1); //exit
            }
            pictureBox1.Image = Sync_App.Properties.Resources.logo; //logo

            ContextMenu contextMenu1;  //system tray icon right click
            MenuItem menuItem1; //object shows when system tray icon is right clicked

            contextMenu1 = new System.Windows.Forms.ContextMenu();
            menuItem1 = new System.Windows.Forms.MenuItem();

            contextMenu1.MenuItems.AddRange(
                        new System.Windows.Forms.MenuItem[] { menuItem1 }); // Initialize contextMenu1

            menuItem1.Index = 0;    // Initialize menuItem1
            menuItem1.Text = "E&xit";

            menuItem1.Click += new System.EventHandler(menuItem1_Click);    //event handler

            notifyIcon = new NotifyIcon();  //notify icon in system tray
            notifyIcon.Icon = Sync_App.Properties.Resources.ftp_icon;
            notifyIcon.MouseDoubleClick += new MouseEventHandler(double_click); //event on double click
            notifyIcon.Visible = true;  //make icon always visible in system tray
            notifyIcon.ContextMenu = contextMenu1;

            list_unwanted_directories.Add(@"C:\FTP DATA\FTP Temp");     //default excluded folders
            list_unwanted_directories.Add(@"C:\FTP DATA\FTP");
        }
        private void double_click(object sender, MouseEventArgs e)  //on double click on system tray icon, show form
        {
            this.WindowState = FormWindowState.Normal;  
            this.ShowInTaskbar = true;
            this.Show();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)   
        {
            e.Cancel = true;
            this.Hide();
        }
        public bool IsEnglish(string inputstring)   //function returns true if inputstring is ENGLISH characters false otherwise
        {
            Regex regex = new Regex(@"[A-Za-z0-9 .,-=+(){}\[\]\\]");
            MatchCollection matches = regex.Matches(inputstring);

            if (matches.Count.Equals(inputstring.Length))
                return true;
            else
                return false;
        }
        private void menuItem1_Click(object Sender, EventArgs e)    //when system tray icon is right clicked
        {
            System.Environment.Exit(1); // Close the form, which closes the application.
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            check_existing_files();
        }
        void check_existing_files()
        {
            existing_files = dirSearch(parent_main_directory); //get all previously existing files in entire directory 

            foreach (string ThisFile in existing_files) //for every found file
            {
                string FilePath = Path.GetDirectoryName(ThisFile);  //file path
                string NameOnly = Path.GetFileName(ThisFile);   //file name
                FileSystemEventArgs FSEAe = new FileSystemEventArgs(WatcherChangeTypes.Created, FilePath, NameOnly);    //create filesystemeventargs for that file
                if(IsFileLocked(ThisFile) == false)
                    file_created(this, FSEAe);  //fire event handler
            }
        }

        private void exclude_btn_Click(object sender, EventArgs e)
        {
            unwanted_directories unwanted = new unwanted_directories();
            unwanted.Show();
        }

        public void set_unwanted_directories(List<string> sent_unwanted_directories)    //function to set list of excluded folders
        {
            list_unwanted_directories.Clear();
            list_unwanted_directories = sent_unwanted_directories;
        }
    }
}
