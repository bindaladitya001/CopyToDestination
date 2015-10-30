using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CopyToDestination
{
    public partial class MessageBoxForExisting : Form
    {
        public MessageBoxForExisting()
        {
            InitializeComponent();
        }

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        } 

        static MessageBoxForExisting MsgBox;
        static string result = "No";

        public static string Show(string Text, string Caption, string btnYes, string btnNo, string btnKeepFiles)
        {
            MsgBox = new MessageBoxForExisting();
            
            MsgBox.label1.Text = Text;
            MsgBox.buttonYes.Text = btnYes;
            MsgBox.buttonNo.Text = btnNo;
            MsgBox.buttonKeepFiles.Text = btnKeepFiles;
            MsgBox.Text = Caption;
            result = btnKeepFiles;
            MsgBox.ShowDialog();
            return result;
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            result = "Yes";
            MsgBox.Close();
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            result = "No";
            MsgBox.Close();
        }

        private void buttonKeepFiles_Click(object sender, EventArgs e)
        {
            result = "Keep Both";
            MsgBox.Close();
        }
    }
}
