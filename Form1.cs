using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectEcho
{
    public partial class MainForm : Form
    {
        private Panel currentPanel = new Panel();
        public Panel[] contextPanels = new Panel[4];
        public String path;
        public MainForm()
        {
            InitializeComponent();
            contextPanels[0] = (mainMenuPanel);
            contextPanels[1] = (taskOnePanel);
            //contextPanels[2] = (taskTwoPanel);
            //contextPanels[3] = (taskThreePanel);

            currentPanel = contextPanels[0];
            setPanelActive(0);
            string[] taskOneArray = { "Context for learning information", "Plans for Learning segment", "Instructional Materials", "Assessments", "Planning Commentary" };
            taskOneList.Items.AddRange(taskOneArray);
            string[] taskTwoArray = { "Video Clips", "Commentary" };
            taskTwoList.Items.AddRange(taskTwoArray);
            string[] taskThreeArray = { "Video Conference", "Notes", "Feedback", "Commentary" };
            taskThreeList.Items.AddRange(taskThreeArray);
            string[] reviewArray = { "Task 1", "Task 2", "Task 3" };
            reviewList.Items.AddRange(reviewArray);

            //create user uploads folder
            string userUploadsPath = Environment.CurrentDirectory + "\\UserUploads";
            try
            {
                //if the directory doesn't exist, create it
                if (!Directory.Exists(userUploadsPath))
                {
                    Directory.CreateDirectory(userUploadsPath);
                }
            }
            catch (Exception)
            {
                //fail silently
            }
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void titlePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void taskTwoList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
           
        }

        private void checkedListBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            SettingsForm sf = new SettingsForm();

            if(sf.ShowDialog() == DialogResult.OK)
            {
                Console.Write("Settings opened");
            }
        }

        private void task1Button_Click(object sender, EventArgs e)
        {
            setPanelActive(1);
        }

        private void taskTwoButton_Click(object sender, EventArgs e)
        {
            setPanelActive(2);
        }

        private void taskThreeButton_Click(object sender, EventArgs e)
        {
            setPanelActive(3);
        }

        private void returnToMenuButton_Click(object sender, EventArgs e)
        {
            setPanelActive(0);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            int i = Array.IndexOf(contextPanels, currentPanel);
            setPanelActive(i - 1);
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {
            int i = Array.IndexOf(contextPanels, currentPanel);
            setPanelActive(i + 1);
        }

        private void setPanelActive(int i)
        {
            currentPanel.Enabled = false;
            currentPanel.Visible = false;
            currentPanel = contextPanels[i];
            currentPanel.Enabled = true;
            currentPanel.Visible = true;

            if(i.Equals(0))
            {
                titleLabel.Text = "MAIN MENU";
                returnToMenuButton.Visible = false;
                returnToMenuButton.Enabled = false;
                backButton.Visible = false;
                backButton.Enabled = false;
                helpButton.Visible = true;
                helpButton.Enabled = true;
                instructionsButton.Visible = false;
                instructionsButton.Enabled = false;
            } else
            {
                returnToMenuButton.Visible = true;
                returnToMenuButton.Enabled = true;
                backButton.Visible = true;
                backButton.Enabled = true;
                helpButton.Visible = false;
                helpButton.Enabled = false;
                instructionsButton.Visible = true;
                instructionsButton.Enabled = true;
                if (i.Equals(1))
                {
                    titleLabel.Text = "TASK ONE";
                } else if(i.Equals(2))
                {
                    forwardButton.Visible = true;
                    forwardButton.Enabled = true;
                    titleLabel.Text = "TASK TWO";
                } else if(i.Equals(3))
                {
                    titleLabel.Text = "TASK THREE";
                    forwardButton.Visible = false;
                    forwardButton.Enabled = false;
                }
            }

            //DISPLAY UPLOADED FILES

            //get data stored in user upload data file (not needed yet)
            /*string userUploadsDataPath = Environment.CurrentDirectory + "\\UserUploads" + "\\uploadsData.txt";
            List<string> lies = File.ReadAllLines(userUploadsDataPath).ToList();*/

            //gets task part name (i.e. task 1 part "A")
            string currentTab = "x";
            currentTab = this.tabControl1.SelectedTab.Text;
            char currentTabLetter = currentTab[currentTab.Length - 1];
            
            string uploadedFile = "taskUpload" + i + currentTabLetter; //generates folder name based on currently selected task/part (i.e. taskUpload1A)
            string taskUploadsPath = Environment.CurrentDirectory + "\\UserUploads\\" + uploadedFile; //finds correct folder path for current section
            //Console.WriteLine(taskUploadsPath); //test
            try
            {
                //if the directory exists, read all files from it
                if (Directory.Exists(taskUploadsPath))
                {                    
                    DirectoryInfo d = new DirectoryInfo(taskUploadsPath); //set directory
                    FileInfo[] Files = d.GetFiles(); //get all files from the folder
                    string str = "";

                    foreach (FileInfo file in Files)
                    {
                        str = str + "\n" + file.Name; //adds each file name to the string
                    }

                    uploadInfo.Text = "Uploaded: " + str; //set upload info text to display all files from the folder
                }
            }
            catch (Exception)
            {
                //do nothing
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.AddExtension = true;
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Document Files(*.doc; *.docx)|*.doc; *.docx";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            String path = "";
            
            if(openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach(string fileName in openFileDialog.FileNames)
                {
                    path = Path.GetFullPath(fileName);
                    uploadButton.Text = Path.GetFileName(fileName);
                    //path = openFileDialog.File.FullName;
                    //string sourcePath = @"C:\Users\Public\TestFolder";
                    //string targetPath = @"C:\Users\Public\TestFolder\SubDir";
                    //System.IO.File.Copy(fileName, destFile, true);

                    string separatedFileName = Path.GetFileName(fileName); //gets only the file name + extension
                    string extension = Path.GetExtension(fileName); //gets only the file extension
                    uploadInfo.Text += "\n" + separatedFileName; //concats new file name

                    //create useruploads data text file (not needed right now)
                    /*string userUploadsDataPath = Environment.CurrentDirectory + "\\UserUploads" + "\\uploadsData.txt";
                    try
                    {
                        //if the file doesn't exist, create it
                        if (!File.Exists(userUploadsDataPath))
                        {
                            File.Create(userUploadsDataPath);
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("File \"{0}\" already exists", separatedFileName);
                        return;
                    }*/

                    //gets task part name (i.e. task 1 part "A")
                    string currentTab = "x";
                    currentTab = this.tabControl1.SelectedTab.Text;
                    char currentTabLetter = currentTab[currentTab.Length - 1];

                    //creates name of file based on current task and part (i.e. taskUpload1A) so that when they reupload a file with a different name, it still overwrites the stored file
                    int i = Array.IndexOf(contextPanels, currentPanel);
                    string uploadedFile = "taskUpload" + i + currentTabLetter;

                    //create user task folder
                    string taskUploadsPath = Environment.CurrentDirectory + "\\UserUploads\\" + uploadedFile;
                    try
                    {
                        //If the directory doesn't exist, create it
                        if (!Directory.Exists(taskUploadsPath))
                        {
                            Directory.CreateDirectory(taskUploadsPath);
                        }
                    }
                    catch (Exception)
                    {
                        //fail silently
                    }

                    string targetPath = Path.Combine(Environment.CurrentDirectory, @"UserUploads\", uploadedFile, separatedFileName); //path to upload the user's file
                    //MessageBox.Show("\nUPLOADED: " + separatedFileName + "\nFROM: " + fileName + "\nTO: " + targetPath +"\n"+ uploadedFile); //shows paths for testing
                    File.Copy(fileName, targetPath, true); //saves a copy of the user's file; the 'true' means that it will overwrite existing files of the same name

                    //writes data enty to file (given file name, original file name, given file path, original file path (not needed right now)
                    /*string userDataEntry = uploadedFile + ',' + separatedFileName + ',' + targetPath + ',' + fileName;
                    File.AppendAllText(userUploadsDataPath, userDataEntry + Environment.NewLine);*/
                }
            }

            
            
            if (path.EndsWith(".docx") || path.EndsWith(".doc"))
            {
                uploadInfo.Text = uploadInfo.Text + path;
                FormatChecker fc = new FormatChecker();
                Boolean[] b = fc.runFormatCheck(path, 90);
                            //label9.Text = "correct alignment  " + b[0] + "   " + "correct font  "  +b[1] + "   " + "correct size  " + b[2] + "   " + "correct length" + b[3];
            if(b[0].Equals(true))
            {
                t1paCL.SetItemChecked(0, true); //Aligned
            }

            if(b[1].Equals(true))
            {
                t1paCL.SetItemChecked(1, true); //Font
            }

            if(b[2].Equals(true))
            {
                t1paCL.SetItemChecked(2, true); //Font Size
            }

            if(b[3].Equals(true))
            {
                t1paCL.SetItemChecked(3, true); //Length
            }
                
            }


        }

        //Executes when the help button is clicked
        private void helpButton_Click(object sender, EventArgs e)
        {
            //Creates the form that displays
            HelpForm hf = new HelpForm();

            if (hf.ShowDialog() == DialogResult.OK)
            {
                Console.Write("Help opened");
            }
        }

        //Executed when the instructions is clicked
        private void instructionsButton_Click(object sender, EventArgs e)
        {
            //We are still figuring out how to approach this
        }
    }
}
