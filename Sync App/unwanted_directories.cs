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
using System.CodeDom;
using System.CodeDom.Compiler;

namespace Sync_App
{
    public partial class unwanted_directories : Form
    {
        int yPadding = 0;
        List<TextBox> list_folders_textboxes = new List<TextBox>();  //contains all textboxes that hold excluded folder path   
        List<Button> list_browse_buttons = new List<Button>();      //contains all browse buttons
        List<Button> list_remove_buttons = new List<Button>();      //contains all remove buttons
        Button submit_btn = new Button();
        public unwanted_directories()
        {
            InitializeComponent();
        }

        private void unwanted_directories_Load(object sender, EventArgs e)
        {
            Label directories = new Label();                    //label
            directories.Left = 20;
            directories.Top = 20;
            directories.Text = "Browse for excluded folders";
            directories.AutoSize = true;
            directories.Font = new Font("Arial", 12);
            this.Controls.Add(directories);

            Button add_btn = new Button();      //add button
            add_btn.Left = directories.Left + directories.Width + 10;
            add_btn.Top = directories.Top;
            add_btn.Text = "Add";
            add_btn.AutoSize = true;

            add_btn.Click += new EventHandler(add_event);

            this.Controls.Add(add_btn);

            yPadding = directories.Top + 40;

            TextBox folder1 = new TextBox();    //first textbox that holds a default value
            folder1.Left = directories.Left;
            folder1.Top = yPadding;
            folder1.Width = 200;
            folder1.Text = @"C:\FTP DATA\FTP Temp";

            list_folders_textboxes.Add(folder1);

            this.Controls.Add(folder1);

            Button browse1 = new Button();      //first browse button
            browse1.Left = folder1.Left + folder1.Width + 10;
            browse1.Top = yPadding - 2;
            browse1.Text = "Browse";
            browse1.AutoSize = true;
            browse1.Click += new EventHandler(browse_event);

            list_browse_buttons.Add(browse1);

            this.Controls.Add(browse1);

            Button delete1 = new Button();      //first remove button
            delete1.Left = browse1.Left + browse1.Width + 10;
            delete1.Top = yPadding - 2;
            delete1.Text = "Remove";
            delete1.AutoSize = true;
            delete1.Click += new EventHandler(remove_event);

            list_remove_buttons.Add(delete1);

            this.Controls.Add(delete1);

            yPadding = yPadding + 40;

            TextBox folder2 = new TextBox();        //second textbox that holds a default value
            folder2.Left = directories.Left;
            folder2.Top = yPadding;
            folder2.Width = 200;
            folder2.Text = @"C:\FTP DATA\FTP";

            list_folders_textboxes.Add(folder2);

            this.Controls.Add(folder2);

            Button browse2 = new Button();  //second browse button
            browse2.Left = folder2.Left + folder2.Width + 10;
            browse2.Top = yPadding - 2;
            browse2.Text = "Browse";
            browse2.AutoSize = true;
            browse2.Click += new EventHandler(browse_event);

            list_browse_buttons.Add(browse2);

            this.Controls.Add(browse2);

            Button delete2 = new Button();  //second remove button
            delete2.Left = browse2.Left + browse2.Width + 10;
            delete2.Top = yPadding - 2;
            delete2.Text = "Remove";
            delete2.AutoSize = true;
            delete2.Click += new EventHandler(remove_event);

            list_remove_buttons.Add(delete2);

            this.Controls.Add(delete2);

            yPadding = yPadding + 40;


            submit_btn.Left = 170;      //submit button 
            submit_btn.Top = yPadding;
            submit_btn.Text = "Submit";
            submit_btn.AutoSize = true;

            submit_btn.Click += new EventHandler(submit_event);

            this.Controls.Add(submit_btn);


        }

        private void add_event(object sender, EventArgs e)      //when add button is clicked
        {   
            TextBox folder = new TextBox();     //create new textbox
            folder.Left = 20;
            folder.Top = yPadding;
            folder.Width = 200;

            list_folders_textboxes.Add(folder); //add it to list of folders textboxes

            this.Controls.Add(folder);

            Button browse = new Button();       //create new browse button
            browse.Left = folder.Left + folder.Width + 10;
            browse.Top = yPadding - 2;
            browse.Text = "Browse";
            browse.AutoSize = true;

            browse.Click += new EventHandler(browse_event);

            list_browse_buttons.Add(browse);    //add it to list of browse buttons

            this.Controls.Add(browse);

            Button delete = new Button();       //create new remove button
            delete.Left = browse.Left + browse.Width + 10;
            delete.Top = yPadding - 2;
            delete.Text = "Remove";
            delete.AutoSize = true;
            delete.Click += new EventHandler(remove_event); 

            list_remove_buttons.Add(delete);    //add it to list of remove buttons

            this.Controls.Add(delete);

            yPadding = yPadding + 40;

            submit_btn.Top = yPadding;
        }

        private void remove_event(object sender, EventArgs e)   //when remove button is clicked
        {
            Button clicked_button = sender as Button;   //get which button was clicked
            int index = list_remove_buttons.IndexOf(clicked_button);    //get index of clicked button
            int current_ypadding = clicked_button.Top;
            for (int i = index; i < list_remove_buttons.Count(); i++)   //from this index till the end of the excluded folders list
            {
                list_remove_buttons[i].Top = list_remove_buttons[i].Top - 40;   //bring controls when position upwards
                list_browse_buttons[i].Top = list_browse_buttons[i].Top - 40;
                list_folders_textboxes[i].Top = list_folders_textboxes[i].Top - 40;
            }

            this.Controls.Remove(list_remove_buttons[index]);   //remove controls of removed folder from form
            this.Controls.Remove(list_browse_buttons[index]);
            this.Controls.Remove(list_folders_textboxes[index]);

            list_remove_buttons.RemoveAt(index);    //remove controls of removed folder from their respective list
            list_browse_buttons.RemoveAt(index);
            list_folders_textboxes.RemoveAt(index);

            int count = list_remove_buttons.Count;  
            if (count > 0)  
                yPadding = list_remove_buttons[count - 1].Top + 40;
            submit_btn.Top = yPadding;

            if (count == 0)
                yPadding = 60;
        }
        private void browse_event(object sender, EventArgs e)   //when browse button is clicked
        {
            Button clicked_button = sender as Button;   //get clicked button

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            int index = list_browse_buttons.IndexOf(clicked_button);    //get index of clicked button

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                list_folders_textboxes[index].Text = fbd.SelectedPath;  //put selected folder path in the respective textbox
            }
        }

        private void submit_event(object sender, EventArgs e)       //when submit button is clicked
        {
            List<string> unwanted_directories = new List<string>();     //list contains all unwanted directories
            unwanted_directories.Clear();

            foreach (var textbox in list_folders_textboxes) //fill list from all unempty textboxes
            {
                if (textbox.Text == "")
                {
                    MessageBox.Show("Empty folder path detected");
                    unwanted_directories.Clear();
                    return;
                }
                else
                {
                    unwanted_directories.Add(textbox.Text);
                }
            }
            Form1 frm = (Form1)Application.OpenForms["Form1"];  //set list in main form
            frm.set_unwanted_directories(unwanted_directories);
            this.Close();
        }

    }
}
