using System.IO;
using Silence.Localization;
using Silence.Macro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Silence
{
    public partial class FormMain : Form
    {
        private const string ConfigurationFilePath = "config.json";

        private readonly MacroRecorder _recorder;
        private readonly MacroPlayer _player = new MacroPlayer();

        private readonly ConfigurationFile _config;
        private readonly LanguagePack _languages;

        public FormMain()
        {
            InitializeComponent();

            _recorder = new MacroRecorder(lblEvents);
            _recorder.ShortcutHandler = KeyDownGlobal;

            if (!File.Exists(ConfigurationFilePath))
                new ConfigurationFile().Save(ConfigurationFilePath);
            _config = ConfigurationFile.FromFile(ConfigurationFilePath);

            _languages = new LanguagePack(@"lang");
            _languages.SelectLanguage(_config.LanguageCode);
        }

        public bool KeyDownGlobal(Hooking.GlobalKeyEventHandlerArgs e)
        {
            if (e.VirtualKeyCode == _config.RecordShortcut)
            {
                if (_recorder.IsRunning)
                    stopMacro();
                else
                {
                    lblStatus.Text = "Status: Recording";
                    _recorder.Clear();
                    _recorder.StartRecording();
                }
                return false;
            }
            if (e.VirtualKeyCode == _config.PlayShortcut)
            {
               if (_player.IsPlaying)
                    _player.CancelPlayback();
                else
                    playMacro();
                return false;
            }
            if (e.VirtualKeyCode == _config.CaptureShortcut && _recorder.IsRunning)
            {
                string basePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" +_config.FilePrefix;

                if (!System.IO.Directory.Exists(basePath))
                    Directory.CreateDirectory(basePath);

                Guid guid = Guid.NewGuid();
                string tempPath = basePath + "\\" + _config.FilePrefix + guid + ".bmp";

                int offsetPointx = int.Parse(_config.Offset.Split(',')[0]) * _config.CaptureWidth / 100;
                int offsetPointy = int.Parse(_config.Offset.Split(',')[1]) * _config.CaptureHeight / 100;

                //System.Drawing.Point destinationPoint = new System.Drawing.Point((int)_recorder.CurrentXY.X + _config.CaptureWidth, (int)_recorder.CurrentXY.Y + _config.CaptureHeight);
                System.Drawing.Point destinationPoint = new System.Drawing.Point((int)_recorder.CurrentXY.X + offsetPointx, (int)_recorder.CurrentXY.Y + offsetPointy);
                //System.Drawing.Point sourcePoint = new Point((int)_recorder.CurrentXY.X, (int)_recorder.CurrentXY.Y);
                System.Drawing.Point sourcePoint = new Point((int)_recorder.CurrentXY.X - offsetPointx, (int)_recorder.CurrentXY.Y - offsetPointy);
                ImageProcessing.CaptureImage(sourcePoint, destinationPoint, tempPath, "bmp");

                _recorder.CurrentMacro.AddEvent(new MacroWaitImageEvent(tempPath.Replace("\\", "\\\\")));
                return false;
            }

            return true;
        }

        private void recordMacro()
        {
            // Confirm action.
            if (_recorder.CurrentMacro != null && _recorder.CurrentMacro.Events.Length > 0)
            {
                var result = MessageBox.Show(_languages.GetLocalizedString("confirm_append_message"),
                    _languages.GetLocalizedString("confirm_append_title"), MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                    _recorder.Clear();
                else if (result == DialogResult.Cancel)
                    return;
            }

            lblStatus.Text = "Status: Recording";
            // Begin recording.
            _recorder.StartRecording();
        }

        private void playMacro()
        {
            lblStatus.Text = "Status: Playing";
            // Load and play macro.
            _player.LoadMacro(_recorder.CurrentMacro);
            _player.PlayMacroAsync();
        }

        private void stopMacro()
        {
            lblStatus.Text = "Status: Idle";
            // Stop recording.
            _recorder.StopRecording();
        }

        private void recordControlButton_Click(object sender, EventArgs e)
        {
            recordMacro();
        }

        private void stopControlButton_Click(object sender, EventArgs e)
        {
            stopMacro();
        }

        private void playControlButton_Click(object sender, EventArgs e)
        {
            playMacro();
        }

        private void clearControlButton_Click(object sender, EventArgs e)
        {
            // Confirm action.
            if (_recorder.CurrentMacro != null && _recorder.CurrentMacro.Events.Length > 0)
            {
                var result = MessageBox.Show(_languages.GetLocalizedString("confirm_clear_message"),
                    _languages.GetLocalizedString("confirm_clear_title"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                    _recorder.Clear();
            }
        }

        private void openControlButton_Click(object sender, EventArgs e)
        {
            // Confirm action.
            if (_recorder.CurrentMacro != null && _recorder.CurrentMacro.Events.Length > 0)
            {
                var result = MessageBox.Show(_languages.GetLocalizedString("confirm_open_message"),
                    _languages.GetLocalizedString("confirm_clear_title"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                    return;
            }

            // Browse for file
            var dialog = new OpenFileDialog
            {
                Title = _languages.GetLocalizedString("dialog_open_macro_title"),
                Filter = _languages.GetLocalizedString("dialog_open_macro_filter")
            };

            // Load macro into recorder.
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var loadedMacro = new Macro.Macro();
                loadedMacro.Load(dialog.FileName);
                _recorder.LoadMacro(loadedMacro);
            }
        }

        private void saveControlButton_Click(object sender, EventArgs e)
        {
            // Check there is a macro to save.
            if (_recorder.CurrentMacro == null || _recorder.CurrentMacro.Events.Length == 0)
            {
                MessageBox.Show(_languages.GetLocalizedString("error_save_nomacro_message"),
                    _languages.GetLocalizedString("error_save_nomacro_title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Choose file to save to.
            var dialog = new SaveFileDialog
            {
                Title = _languages.GetLocalizedString("dialog_save_macro_title"),
                Filter = _languages.GetLocalizedString("dialog_save_macro_filter")
            };

            // Save file.
            if (dialog.ShowDialog() == DialogResult.OK)
                _recorder.CurrentMacro.Save(dialog.FileName);
        }

        private void loopControlButton_Click(object sender, EventArgs e)
        {
            // Set number of repetitions on player.
            var dialog = new RepetitionsDialog { Repetitions = _player.Repetitions };
            if (dialog.ShowDialog() == DialogResult.OK)
                _player.Repetitions = dialog.Repetitions;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            // Set number of repetitions on player.
            var dialog = new FormSettings(_recorder, _config, ConfigurationFilePath);

            dialog.txtCapture.Text = _config.CaptureShortcut.ToString();
            dialog.txtPlay.Text = _config.PlayShortcut.ToString();
            dialog.txtRecord.Text = _config.RecordShortcut.ToString();
            dialog.txtCaptureWidth.Text = _config.CaptureWidth.ToString();
            dialog.txtCaptureHeight.Text = _config.CaptureHeight.ToString();
            dialog.txtFilePrefix.Text = _config.FilePrefix;
            dialog.chkAppendRecordings.Checked = _config.AppendScript;

            dialog.FormClosing += Dialog_FormClosing;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                
            }

           
        }

        private void Dialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            _recorder.ShortcutHandler = KeyDownGlobal;
        }
    }
}
