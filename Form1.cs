using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Numerics;
using Ionic.Utils;
using System.Security.AccessControl;

namespace CopyToDestination
{
    public partial class Form1 : Form
    {
        long totalBytes = 0;    //'totalBytes' be the size of bytes transmitting after reading from the file and calling inside 'ControlUpdtion' function


        int i = 2;              //i to check whether file/folder is going to transfer or unable to transfer
        int valueDeleated = 0;  //valueDeleted is to find that directory is already deleted or not
                
        int no = 1;             //For displaying number (with increment) in the list box before the coming error
        int final;              //Displaying percentage before progressbar
        long totalCount = 0;    //'totalCount' is the total size of the directory/file selecting as source and calling inside 'ControlUpdation' function
        string getName = "";    //It is the part of the string displayed inside messagebox when the file/folder already existed at destination point
        string destName = "";   //displaying the path of the destination file/folder inside statusbar
        string souName = "";    //displaying the path of the source file/folder inside statusbar
        long Total = 0;

        static long countDi = 0;

        static int cancelOperation = 0;

        public Form1()
        {
            InitializeComponent();
            Source.Focus();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        //Ask for closing the application
        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           DialogResult confirmAns= MessageBox.Show("Are you really want to close this application?","Confirmation",MessageBoxButtons.YesNo);
           if (confirmAns == DialogResult.Yes)
           {
               e.Cancel = false;
           }
           else
           {
               e.Cancel = true;
           }
        }

        //Finding the total free space inside the drive
        private long GetTotalFreeSpace(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName)
                {
                    return drive.TotalFreeSpace;
                }
            }
            return -1;
        }

        //Finding the total drive size inside the drive
        private long GetTotalDriveSize(string driveName)
        {
            long size = 0;
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo driveInfoName in drives)
            {
                if (driveInfoName.IsReady && driveInfoName.Name == driveName)
                    size = driveInfoName.TotalSize;
            }
            return size;
        }

        //Finding the directory size including all files in it (files containing inside subdirectories too) 
        //and return it's value
        private long GetDirectorySize(string p)
        {
            if (cancelOperation != 1)
            {
                string waitText = "Please Wait";
                try
                {
                    fileTransfer.Text = waitText + ".";
                    Application.DoEvents();

                    List<FileInfo> files = new List<FileInfo>();
                    List<DirectoryInfo> folders = new List<DirectoryInfo>();
                    DirectoryInfo di = new DirectoryInfo(p);
                    try
                    {
                        foreach (FileInfo f in di.GetFiles("*.*"))
                        {
                            files.Add(f);
                            countDi += f.Length;
                        }
                    }
                    catch
                    {
                    }

                    foreach (DirectoryInfo d in di.GetDirectories())
                    {
                        folders.Add(d);
                        GetDirectorySize(d.FullName);
                    }
                    fileTransfer.Text += ".";
                    this.Refresh();
                }
                catch
                {
                }
            }

            return countDi;
        }

        //For spliting the text present in the status bar according to the lengthiness of the location
        private void textSplit()
        {
            if (Destination.Text.Split('\\').Count() > 2)   //if count is more than 2 then show first and last(acc. to size) otherwise both
            {
                destName = Destination.Text.Split('\\').First() + "\\...";
                if (Destination.Text.Split('\\').Last().Length < 26)
                    destName += "\\" + Destination.Text.Split('\\').Last();
                else
                    destName += Destination.Text.Substring(Destination.Text.Length - 15, 15);
            }
            else
            {
                destName = Destination.Text;
            }

            if (Source.Text.Split('\\').Count() > 2)    //Similar cond. as done in Destination part 
            {
                souName = Source.Text.Split('\\').First() + "\\...";
                if (Source.Text.Split('\\').Last().Length < 26)
                    souName += "\\" + Source.Text.Split('\\').Last();
                else
                    souName += Source.Text.Substring(Source.Text.Length - 15, 15);
            }
            else
            {
                souName = Source.Text;
            }
        }

        //Updating ProgressBar and Percentage according to the total bytes of that whole directory by finding the
        //size of all the files and sending it to the destination part by splitting it acc. to the buffer size
        private void ControlUpdation(string destinationPath, string sourcePath)
        {
            if (cancelOperation != 1)
            {
                byte[] buffer = new byte[15 * 1024 * 1024];     //20MB size of the buffer

                using (FileStream sourceValue = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
                {
                    long fileLength = sourceValue.Length;
                    using (FileStream dest = new FileStream(destinationPath, FileMode.CreateNew, FileAccess.Write))
                    {
                        int currentBlockSize = 0;

                        while ((currentBlockSize = sourceValue.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            if (cancelOperation != 1)
                            {
                                //Application.DoEvents();
                                totalBytes += currentBlockSize;     //'totalBytes' be the size of bytes transmitting after reading from the file
                                int percentage = (int)((double)(totalBytes * 100.0 / totalCount));  //'totalCount' is the total size of the directory/file selecting as source

                                dest.Write(buffer, 0, currentBlockSize);    //writing on the new destination area's file acc. to the currentBlockSize
                                ProgressBar.Value = percentage;
                                //this.Refresh();
                                showPercent.Text = percentage + "%";
                                Application.DoEvents();
                                this.Refresh();     //Refreshing the things so that update of 'showPercent.Text' will be displayed
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void FilesCountingFromDestination(DirectoryInfo destination)
        {
            ProgressBar.Value = 0;
            showPercent.Text = "0%"; ;
            this.Refresh();
        }

        //If single file is transmitting from source to destination then this function will be called (not 'CopyDirectory' function)
        private void CopySingleFile(FileInfo source, DirectoryInfo destination)
        {
            string cutDestination = destination.FullName.Split('.').Last();
            //destination = new DirectoryInfo(destination.FullName.Replace("."+cutDestination,""));
            if (cancelOperation != 1)
            {
                i = 2;
                totalCount = source.Length;

                FileInfo[] particularFileGetting = destination.Parent.GetFiles(destination.Name);
                if (particularFileGetting.Count() != 0)
                {
                    if (destination.Name.Length > 38)        //Splitting the name according to the length and display on the status bar after the 'File Name'
                    {
                        getName = source.Name.Substring(0, 20) + "..." + source.Name.Substring(destination.Name.Length - 15, 15);
                    }
                    else
                    {
                        getName = source.Name;
                    }

                    try
                    {
                       // FileInfo nameOfTheFileToDelete = new FileInfo(destination.Name);
                        string fExit = MessageBoxForExisting.Show("File '" + getName + "' is already Existed. Do You Want To Delete!!! ", "Deleting Confirmation", "Yes", "No", "Keep Both");
                        if (fExit.ToString() == "Yes")
                        {
                            File.Delete(destination.FullName);
                        }

                        //Condition for keeping both files
                        else if (fExit.ToString() == "Keep Both")
                        {
                            int noPlaced = 2;
                            bool set = true;
                            destination = new DirectoryInfo(destination.FullName.Replace("." + cutDestination, ""));
                            do
                            {
                                //Folder name is already present and change its name to the new name
                                for (int beforeNoPlaced = 1; beforeNoPlaced <= noPlaced; beforeNoPlaced++)
                                {
                                    destination = new DirectoryInfo(destination.FullName.Replace(" (" + beforeNoPlaced + ")" + "." + cutDestination, "") + " (" + noPlaced + ")" + "." + cutDestination);
                                }

                                //Checking the file name is already present or not
                                FileInfo[] checkTheFileAlreadyExisted = destination.Parent.GetFiles(destination.Name);
                                
                                //if file name not exists then count value be 0
                                if (checkTheFileAlreadyExisted.Count() == 0)
                                {
                                    set = false;
                                }

                                //else replace the file name with the original name
                                else
                                {
                                    for (int beforeNoPlaced = 1; beforeNoPlaced <= noPlaced; beforeNoPlaced++)
                                    {
                                        destination = new DirectoryInfo(destination.FullName.Replace(" (" + beforeNoPlaced + ")" + "." + cutDestination, ""));
                                    }
                                    noPlaced++;
                                }
                            }
                            while (set);
                        }
                        else
                        {
                            i = 1;
                        }
                        //DialogResult fExist = MessageBox.Show("File '" + getName + "' is already Existed. Do You Want To Delete!!! ", "Deleting Confirmation", MessageBoxButtons.YesNo);
                        //if (fExist == DialogResult.Yes)
                        //{
                        //    File.Delete(destination.FullName);
                        //}
                        //else
                        //{
                        //    i = 1;
                        //}
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("" + e);
                        listBox.Items.Add(no + ") File '" + getName + "' is not able to Delete");
                        this.listBox.SelectedIndex = this.listBox.Items.Count - 1;
                        this.Refresh();
                        no++;
                    }
                }
                if (i != 1)
                {
                    if (source.Name.Length > 38)        //Splitting the name according to the length and display on the status bar after the 'File Name'
                    {
                        getName = source.Name.Substring(0, 20) + "..." + source.Name.Substring(source.Name.Length - 15, 15);
                    }
                    else
                    {
                        getName = source.Name;
                    }

                    try
                    {
                        textSplit();


                        fileTransfer.Text = "Copying From '" + souName + "' To '" + destName + "' File Name: " + getName;
                        this.Refresh();

                        DirectoryInfo pathOfDirectory = destination.Parent;
                        DateTime startTime = DateTime.Now;
                        //source.CopyTo(destination.FullName);
                        string PathFile1 = destination.FullName;//Path.Combine(destination.FullName, source.Name);
                        string PathFile2 = source.FullName;
                        ControlUpdation(PathFile1, PathFile2);
                        //DateTime stopTime = DateTime.Now;
                        //MessageBox.Show("" + stopTime);
                        //MessageBox.Show("" + startTime);
                        //TimeSpan difference = stopTime - startTime;
                        //MessageBox.Show("" + difference);
                    }
                    catch (Exception)
                    {
                        listBox.Items.Add(no + ") File '" + getName + "' is not able to Copy");
                        this.listBox.SelectedIndex = this.listBox.Items.Count - 1;
                        this.Refresh();
                        no++;
                    }
                }
            }
        }        

        //If Directory is transmitting from source to destination then this function will be called (not 'CopySingleFile' function)
        private void CopyDirectory(DirectoryInfo source, DirectoryInfo destination)
        {
            string cutDestination = destination.FullName.Split('.').Last();
            if (cancelOperation != 1)
            {
                i = 2;
                try
                {
                    //size = Directory.GetFiles(Source.Text, "*.*", SearchOption.AllDirectories).Length;
                    //ProgressBar.Maximum = size;
                    if (Total == 0)
                        totalCount = GetDirectorySize(Source.Text);
                    Total = 1;
                    Random rnd = new Random();

                    #region Ask for deleting the file/directory of same as source

                    //If folder already existed and it contains files and folders which is already present on destination
                    //place. Then ask for deleting that file
                    string cutDestinationName = "";

                    if (destination.Exists)
                    {
                        if (destination.Name == source.Name)
                        {
                            if (destination.Name.Length > 38)
                            {
                                cutDestinationName = destination.Name.Substring(0, 20) + "..." + destination.Name.Substring(destination.Name.Length - 15, 15);
                            }
                            else
                            {
                                cutDestinationName = destination.Name;
                            }

                            try
                            {
                                //string nameForDi = destination.FullName;

                                DirectoryInfo destDriveName = destination.Root;
                                string dExist1;
                                //DialogResult dExist1;

                                if (destDriveName.ToString() == Destination.Text)
                                {
                                    dExist1 = MessageBoxForExisting.Show("Drive '" + cutDestinationName + "' is already Existed. Do You Want To Delete!!!", "Deleting Confirmation", "Yes", "No", "Keep Both");
                                }
                                else
                                {
                                    dExist1 = MessageBoxForExisting.Show("Folder '" + cutDestinationName + "' is already Existed. Do You Want To Delete!!! ", "Deleting Confirmation", "Yes", "No", "Keep Both");
                                }

                                if (dExist1.ToString() == "Yes")    //Deleting the whole directory if it is already existed
                                {
                                    destination.Delete(true);
                                    valueDeleated = 1;
                                }

                                //Condition for keeping both folders
                                else if (dExist1.ToString() == "Keep Both")
                                {
                                    int noPlaced = 2;
                                    bool set = true;
                                    destination = new DirectoryInfo(destination.FullName);
                                    do
                                    {
                                        //Change the folder name with the new folder name
                                        for (int beforeNoPlaced = 1; beforeNoPlaced <= noPlaced; beforeNoPlaced++)
                                        {
                                            destination = new DirectoryInfo(destination.FullName.Replace(" (" + beforeNoPlaced + ")", "") + " (" + noPlaced + ")");
                                        }

                                        DirectoryInfo[] checkTheFolderAlreadyExisted = destination.Parent.GetDirectories(destination.Name);
                                        
                                        //Checking whether folder name already exists. If yes then count be not equal to 0 
                                        if (checkTheFolderAlreadyExisted.Count() == 0)
                                        {
                                            set = false;
                                        }
                                        else
                                        {
                                            //Convert the folder name to the original name
                                            for (int beforeNoPlaced = 1; beforeNoPlaced <= noPlaced; beforeNoPlaced++)
                                            {
                                                destination = new DirectoryInfo(destination.FullName.Replace(" (" + beforeNoPlaced + ")", ""));
                                            }
                                            noPlaced++;
                                        }
                                    }
                                    while (set);    //Loop will stop when condition is false
                                }
                                else
                                {
                                    i = 1;
                                }

                                //if (destDriveName.ToString() == Destination.Text)
                                //{
                                //    dExist1 = MessageBox.Show("Drive '" + cutDestinationName + "' is already Existed. Do You Want To Delete!!! ", "Deleting Confirmation", MessageBoxButtons.YesNo);
                                //}
                                //else
                                //{
                                //    dExist1 = MessageBox.Show("Folder '" + cutDestinationName + "' is already Existed. Do You Want To Delete!!! ", "Deleting Confirmation", MessageBoxButtons.YesNo);
                                //}

                                //if (dExist1 == DialogResult.Yes)    //Deleting the whole directory if it is already existed
                                //{
                                //    destination.Delete(true);
                                //    valueDeleated = 1;
                                //}
                                //else
                                //{
                                //    i = 1;
                                //}
                            }
                            catch   //if not able to delete (due to inaccessible file/folder) then this will be run
                            {
                                listBox.Items.Add(no + ") Directory '" + cutDestinationName + "' is not able to delete.");
                                this.listBox.SelectedIndex = this.listBox.Items.Count - 1;
                                this.Refresh();
                                no++;
                            }
                        }
                    }
                    //End of the logic1
                    #endregion

                    if (!destination.Exists || valueDeleated == 1 && i != 1)  //If destination name not existed or directory is already deleted then create the directory
                    {
                        destination.Create();
                    }

                    #region Read each and every file/folder from the source and copy to destination area

                    //Read each and every file from the source and copy to destination area by calling 'ControlUpdation' function otherwise exception thrown if reading of file is failed
                    if (cancelOperation != 1)
                    {
                        if (i != 1)
                        {
                            FileInfo[] files = source.GetFiles();
                            foreach (FileInfo file in files)
                            {
                                if (file.Name.Length > 38)
                                {
                                    getName = file.Name.Substring(0, 20) + "..." + file.Name.Substring(file.Name.Length - 15, 15);
                                }
                                else
                                {
                                    getName = file.Name;
                                }

                                try
                                {
                                    textSplit();


                                    fileTransfer.Text = "Copying From '" + souName + "' To '" + destName + "' File Name: " + getName;
                                    this.Refresh();

                                    string PathFile1 = Path.Combine(destination.FullName, file.Name);
                                    string PathFile2 = Path.Combine(source.FullName, file.Name);
                                    ControlUpdation(PathFile1, PathFile2);
                                }
                                catch (Exception)
                                {
                                    listBox.Items.Add(no + ") File '" + getName + "' is not able to Copy");
                                    try
                                    {
                                        totalCount -= file.Length;
                                    }
                                    catch
                                    {
                                    }

                                    this.listBox.SelectedIndex = this.listBox.Items.Count - 1;
                                    this.Refresh();
                                    no++;
                                }
                            }

                            //Read each and every directory from the source then call the same function again to check for files.
                            //Directory's Name is also displayed from this code
                            DirectoryInfo[] dirs = source.GetDirectories();
                            foreach (DirectoryInfo dir in dirs)
                            {
                                try
                                {
                                    string destinationDir = Path.Combine(destination.FullName, dir.Name);
                                    DirectoryInfo destinfo = new DirectoryInfo(destinationDir);
                                    this.Refresh();
                                    if (!destinfo.Exists)
                                        CopyDirectory(dir, new DirectoryInfo(destinationDir));
                                }
                                catch (Exception)
                                {
                                    string sourceFileNameRead = "";

                                    if (dir.Name.Length > 38)
                                    {
                                        sourceFileNameRead = dir.Name.Substring(0, 20) + "..." + dir.Name.Substring(dir.Name.Length - 15, 15);
                                    }
                                    else
                                    {
                                        sourceFileNameRead = dir.Name;
                                    }

                                    listBox.Items.Add(no + ") Folder '" + sourceFileNameRead + "' is not able to Copy");

                                    try
                                    {
                                        foreach (FileInfo f in dir.GetFiles("*.*"))
                                        {
                                            try
                                            {
                                                totalCount -= f.Length;
                                            }
                                            catch
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                    catch
                                    {
                                    }

                                    this.listBox.SelectedIndex = this.listBox.Items.Count - 1;
                                    this.Refresh();
                                    no++;
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    string sourceFolderNameRead = "";

                    if (source.Name.Length > 38)
                    {
                        sourceFolderNameRead = source.Name.Substring(0, 20) + "..." + source.Name.Substring(source.Name.Length - 15, 15);
                    }
                    else
                    {
                        sourceFolderNameRead = source.Name;
                    }

                    listBox.Items.Add(no + ") Unable to Copy the Folder '" + sourceFolderNameRead + "'");

                    try
                    {
                        foreach (FileInfo f in source.GetFiles("*.*"))
                        {
                            try
                            {
                                totalCount -= f.Length;
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                    catch
                    {
                    }

                    this.listBox.SelectedIndex = this.listBox.Items.Count - 1;
                    this.Refresh();
                    no++;
                }
            }
            //End of Logic2
                #endregion
        }

        //On clicking the 'Enter' button this function will be called 
        private void GetTheResult_Click(object sender, EventArgs e)
        {
            try
            {
                cancelOperation = 0;
                string str1 = Source.Text;
                Total = 0;
                //a and b string is to get files and directories and check whether only file is selected or directory is selected
                string[] a;     //getting files
                string[] b;     //getting Directories
                try
                {
                    a = Directory.GetFiles(str1, "*.*", SearchOption.TopDirectoryOnly);
                }
                catch
                {
                    a = new string[0];
                }
                try
                {
                    b = Directory.GetDirectories(str1);
                }
                catch
                {
                    b = null;
                }

                fileTransfer.Text = "Please Wait...";

                try
                {
                    FileInfo f_Source = new FileInfo(Source.Text);
                    string driveSource = Path.GetPathRoot(f_Source.FullName);

                    try
                    {
                        FileInfo f_Dest = new FileInfo(Destination.Text);
                        string driveDest = Path.GetPathRoot(f_Dest.FullName);
                        long totalDriveSize = GetTotalDriveSize(driveDest);

                        string str2 = "";
                        if (str1.Split('\\').Last() == "")
                            str2 = Destination.Text + "\\" + str1.Replace(":\\", " Drive");
                        else
                            str2 = Destination.Text + "\\" + str1.Split('\\').Last();

                        DirectoryInfo src = new DirectoryInfo(@str1);
                        FileInfo src1 = new FileInfo(str1);
                        DirectoryInfo dest = new DirectoryInfo(@str2);
                        listBox.Items.Clear();
                        final = 0;
                        totalBytes = 0;
                        totalCount = 0;
                        valueDeleated = 0;
                        showPercent.Text = final + "%";
                        no = 1;
                        Font font = new System.Drawing.Font("Times New Roman", 12.0f);
                        listBox.Font = font;

                        ProgressBar.Value = 0;

                        //Checking for calling 'CopySingleFile' function or 'CopyDirectory' function
                        if (a.Length == 0 && b == null)
                        {
                            i = 3;
                            long countTheSourceSize = src1.Length;
                            if (totalDriveSize - countTheSourceSize > 100 * 1024 * 1024)
                            {
                                if (Source.Text != "" && Destination.Text != "")
                                {
                                    browseSource.Enabled = false;
                                    browseDest.Enabled = false;
                                    Source.Enabled = false;
                                    Destination.Enabled = false;
                                    GetTheResult.Enabled = false;
                                    cancelTheOperation.Enabled = true;
                                    CopySingleFile(src1, dest);
                                }
                                else
                                {
                                    MessageBox.Show("Destination Text is Empty...Please Check", "Please Review");
                                    fileTransfer.Text = "Please Select a File/Folder To Copy To Destination...";
                                }
                            }
                            else
                            {
                                MessageBox.Show("Sorry Not Enough Space");
                                fileTransfer.Text = "Please Select a File/Folder To Copy To Destination...";
                            }
                        }
                        else
                        {
                            i = 3;

                            long countTheSourceSize;

                            if (Source.Text == driveSource)
                            {
                                countTheSourceSize = GetTotalDriveSize(str1.Split('\\').First() + "\\");
                            }
                            else //if (src.Exists)
                            {
                                countTheSourceSize = GetDirectorySize(src.FullName);
                            }

                            if (totalDriveSize - countTheSourceSize > 100 * 1024 * 1024)
                            {
                                if (Source.Text != "" && Destination.Text != "")
                                {
                                    countDi = 0;
                                    browseSource.Enabled = false;
                                    browseDest.Enabled = false;
                                    Source.Enabled = false;
                                    Destination.Enabled = false;
                                    GetTheResult.Enabled = false;
                                    cancelTheOperation.Enabled = true;
                                    CopyDirectory(src, dest);
                                }
                                else
                                {
                                    MessageBox.Show("Destination Text is Empty...Please Check", "Please Review");
                                    fileTransfer.Text = "Please Select a File/Folder To Copy To Destination...";
                                }
                            }
                            else
                            {
                                MessageBox.Show("Sorry Not Enough Space");
                                fileTransfer.Text = "Please Select a File/Folder To Copy To Destination...";
                            }
                        }
                        if (i == 2 & cancelOperation != 1)
                        {
                            ProgressBar.Value = ProgressBar.Maximum;
                            fileTransfer.Text = "Copied......... ";
                            showPercent.Text = "100%";
                            MessageBox.Show("Copied Successfully...", "Status");
                            showPercent.Text = final + "%";
                            ProgressBar.Value = 0;
                            browseSource.Enabled = true;
                            browseDest.Enabled = true;
                            Source.Enabled = true;
                            Destination.Enabled = true;
                            GetTheResult.Enabled = true;
                            cancelTheOperation.Enabled = false;
                        }

                        else if (cancelOperation == 1)
                        {
                            FilesCountingFromDestination(dest);
                            browseSource.Enabled = true;
                            browseDest.Enabled = true;
                            Source.Enabled = true;
                            Destination.Enabled = true;
                            GetTheResult.Enabled = true;
                            cancelTheOperation.Enabled = false;
                            fileTransfer.Text = "Please Select a File/Folder To Copy To Destination...";
                        }

                        else
                        {
                            i = 2;
                            fileTransfer.Text = "Please Select a File/Folder to Copy To Destination...";
                            browseSource.Enabled = true;
                            browseDest.Enabled = true;
                            Source.Enabled = true;
                            Destination.Enabled = true;
                            GetTheResult.Enabled = true;
                            cancelTheOperation.Enabled = false;
                        }
                        Source.Clear();
                        Destination.Clear();
                    }
                    catch
                    {
                        MessageBox.Show("Destination Text is Empty...Please Check", "Please Review");
                        fileTransfer.Text = "Please Select a File/Folder To Copy To Destination...";
                    }
                }
                catch
                {
                    MessageBox.Show("Source Text is Empty...Please Check", "Please Review");
                    fileTransfer.Text = "Please Select a File/Folder To Copy To Destination...";
                }
            }
            catch
            {
                MessageBox.Show("Check the entry Please");
            }
        }

        //To display the browse dialog window (for selecting file/folder)
        private void browseSource_Click(object sender, EventArgs e)
        {
            var dlg1 = new FolderBrowserDialogEx();     //dlg1 is the object of 'FolderBrowserDialogEx.cs' file
            dlg1.Description = "Select a File or Folder";
            dlg1.ShowNewFolderButton = true;
            dlg1.ShowEditBox = false;
            dlg1.ShowBothFilesAndFolders = true;
            dlg1.ShowFullPathInEditBox = true;
            dlg1.RootFolder = System.Environment.SpecialFolder.MyComputer;

            // Show the FolderBrowserDialog.
            DialogResult result = dlg1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Source.Text = dlg1.SelectedPath;
            }
        }

        //To display the browse dialog window (for selecting folder)
        private void browseDest_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Destination.Text = fbd.SelectedPath;
            }
        }

        private void cancelTheOperation_Click(object sender, EventArgs e)
        {
            DialogResult fExist = MessageBox.Show("Do You Really Want To Cancel The Operation", "Cancelling Confirmation", MessageBoxButtons.YesNo);
            if (fExist == DialogResult.Yes)
            {
                cancelOperation = 1;
                listBox.Items.Clear();
            }
            else
            {
                cancelOperation = 0;
            }

        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            //string HelpText = System.IO.File.ReadAllText("../../Support/Help.txt");
            //HelpText = HelpText.Replace("\r\n", "\n");
            //var attr = File.GetAttributes("../../Support/Help.txt");
            //attr = attr | FileAttributes.ReadOnly;

            string path="Help.txt";
            File.SetAttributes(path, FileAttributes.ReadOnly);
            System.Diagnostics.Process.Start("notepad", path);

            //File.Open(HelpText,FileMode.Open,FileAccess.Read);
            // Display the file contents to the console. Variable text is a string.
            //string[] array = HelpText.Split('\n');
            //System.Console.WriteLine("Contents of WriteText.txt = {0}", HelpText);
        }

    }
}
