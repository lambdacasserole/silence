﻿using Silence.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using Lua = NLua.Lua;

namespace Silence.Macro
{

    /// <summary>
    /// Provides support for playback of recorded macros.
    /// </summary>
    public class MacroPlayer
    {
        /// <summary>
        /// Lua scripting class
        /// </summary>
        private Lua lua;

        /// <summary>
        /// Holds the mouse simulator that underlies this object.
        /// </summary>
        private MouseSimulator underlyingMouseSimulator;

        /// <summary>
        /// Holds the keyboard simulator that underlies this object.
        /// </summary>
        private KeyboardSimulator underlyingKeyboardSimulator;

        /// <summary>
        /// Holds the thread that the macro is currently executing on.
        /// </summary>
        private Thread macroThread;

        /// <summary>
        /// Holds whether or not the macro has been cancelled.
        /// </summary>
        private bool cancelled;

        /// <summary>
        /// Holds the number of times the macro should repeat before ending.
        /// </summary>
        private int repetitions;

        /// <summary>
        /// Gets the macro currently loaded into the player.
        /// </summary>
        public Macro CurrentMacro { get; private set; }

        /// <summary>
        /// Gets or sets the number of times the macro should repeat before ending.
        /// </summary>
        public int Repetitions { 
            get 
            {
                return repetitions;
            }
            set
            {
                repetitions = (value == 0 ? 1 : value);
            }
        }

        /// <summary>
        /// Gets whether or not a macro is currently being played.
        /// </summary>
        public bool IsPlaying { get; private set; }

        /// <summary>
        /// Initialies a new instance of a macro player.
        /// </summary>
        public MacroPlayer()
        {
            lua = new Lua();
            underlyingMouseSimulator = new MouseSimulator(new InputSimulator());
            underlyingKeyboardSimulator = new KeyboardSimulator(new InputSimulator());
            repetitions = 1;
            cancelled = false;
        }

        /// <summary>
        /// Cancels playback of the current macro.
        /// </summary>
        public void CancelPlayback()
        {
            cancelled = IsPlaying;
            macroThread.Abort();
            IsPlaying = false;
        }

        /// <summary>
        /// Loads a macro into the player.
        /// </summary>
        /// <param name="macro">The macro to load into the player.</param>
        public void LoadMacro(Macro macro) 
        {
            CurrentMacro = macro;
        }

        /// <summary>
        /// Converts a point to absolute screen coordinates.
        /// </summary>
        /// <param name="point">The point to convert.</param>
        /// <returns></returns>
        private Point ConvertPointToAbsolute(Point point)
        {
            return new Point((Convert.ToDouble(65535) * point.X) / Convert.ToDouble(Screen.PrimaryScreen.Bounds.Width),
                (Convert.ToDouble(65535) * point.Y) / Convert.ToDouble(Screen.PrimaryScreen.Bounds.Height));
        }

        /// <summary>
        /// Plays the macro on the current thread.
        /// </summary>
        private void PlayMacro()
        {
            if (CurrentMacro == null || CurrentMacro.MacroFile == null)
                return;

            IsPlaying = true;
            int currentRepetition = repetitions;
            while (currentRepetition > 0)
            {
                lua.DoFile(CurrentMacro.MacroFile);
                currentRepetition--;
            }
                
            IsPlaying = false;
        }

        /// <summary>
        /// Plays the macro on a seperate background thread.
        /// </summary>
        public void PlayMacroAsync()
        {
            macroThread = new Thread(PlayMacro);
            macroThread.Start();
        }

    }

}
