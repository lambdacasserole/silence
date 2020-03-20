using Silence.Macro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Silence
{
    public partial class FormSettings : Form
    {
        public MacroRecorder Recorder;
        public ConfigurationFile Config;
        private string ConfigurationFilePath;

        private bool record = false;
        private bool play = false;
        private bool capture = false;
        
        public FormSettings(MacroRecorder recorder, ConfigurationFile config, string configurationFilePath)
        {
            InitializeComponent();
            Recorder = recorder;
            Recorder.ShortcutHandler = KeyDownGlobal;
            Config = config;
            ConfigurationFilePath = configurationFilePath;
        }

        public bool KeyDownGlobal(Hooking.GlobalKeyEventHandlerArgs e)
        {
            if (record)
            {
                txtRecord.Text = "";
                txtRecord.Text = e.VirtualKeyCode.ToString();
                return false;
            } 
            else if (play)
            {
                txtPlay.Text = "";
                txtPlay.Text = e.VirtualKeyCode.ToString();
                return false;
            }
            else if (capture)
            {
                txtCapture.Text = "";
                txtCapture.Text = e.VirtualKeyCode.ToString();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Yeah I know... :)
        /// It will be reworked as soon as it have more settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRecord_DoubleClick(object sender, EventArgs e)
        {
            play = false;
            capture = false;
            record = true;
        }

        private void txtPlay_DoubleClick(object sender, EventArgs e)
        {
            record = false;
            capture = false;
            play = true;
        }

        private void txtCapture_DoubleClick(object sender, EventArgs e)
        {
            record = false;
            capture = true;
            play = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Config.RecordShortcut = int.Parse(txtRecord.Text);
            Config.PlayShortcut = int.Parse(txtPlay.Text);
            Config.CaptureShortcut = int.Parse(txtCapture.Text);
            Config.CaptureWidth = int.Parse(txtCaptureWidth.Text);
            Config.CaptureHeight = int.Parse(txtCaptureHeight.Text);
            Config.AppendScript = chkAppendRecordings.Checked;
            Config.Offset = txtOffset.Text;
            Config.FilePrefix = txtFilePrefix.Text;
            Config.Save(ConfigurationFilePath);
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
