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

            if (!File.Exists(ConfigurationFilePath))
                new ConfigurationFile().Save(ConfigurationFilePath);
            _config = ConfigurationFile.FromFile(ConfigurationFilePath);

            _languages = new LanguagePack(@"lang");
            _languages.SelectLanguage(_config.LanguageCode);
        }

        private void recordControlButton_Click(object sender, EventArgs e)
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

        private void stopControlButton_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Status: Idle";
            // Stop recording.
            _recorder.StopRecording();
        }

        private void playControlButton_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Status: Playing";
            // Load and play macro.
            _player.LoadMacro(_recorder.CurrentMacro);
            _player.PlayMacroAsync();
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
    }
}
