using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Domain.SharedKernel.ValueObjects
{
    /// <summary>
    /// Selection of phases
    /// </summary>
    public class PhaseSelection
    {
        /// <summary>
        /// Command selected
        /// </summary>
        public bool Command { get; internal set; }

        /// <summary>
        /// Movement selected
        /// </summary>
        public bool Movement { get; internal set; }

        /// <summary>
        /// Psychic selected
        /// </summary>
        public bool Psychic { get; internal set; }

        /// <summary>
        /// Shooting selected
        /// </summary>
        public bool Shooting { get; internal set; }

        /// <summary>
        /// Charge selected
        /// </summary>
        public bool Charge { get; internal set; }

        /// <summary>
        /// Fight selected
        /// </summary>
        public bool Fight { get; internal set; }

        /// <summary>
        /// Moralte selected
        /// </summary>
        public bool Morale { get; internal set; }

        /// <summary>
        /// Protected constructor
        /// </summary>
        protected PhaseSelection() { }

        /// <summary>
        /// Internal constructor
        /// </summary>
        /// <param name="command">Select the command phase</param>
        /// <param name="movement">Select the movement phase</param>
        /// <param name="psychic">Select the psychic phase</param>
        /// <param name="shooting">Select the shooting phase</param>
        /// <param name="charge">Select the charge phase</param>
        /// <param name="fight">Select the fight phase</param>
        /// <param name="morale">Select the morale phase</param>
        internal PhaseSelection(bool command, bool movement, bool psychic, bool shooting, bool charge, bool fight, bool morale)
        {
            Command = command;
            Movement = movement;
            Psychic = psychic;
            Shooting = shooting;
            Charge = charge;
            Fight = fight;
            Morale = morale;
        }

        /// <summary>
        /// Factory method for creating the value object
        /// </summary>
        /// <param name="command">Select the command phase</param>
        /// <param name="movement">Select the movement phase</param>
        /// <param name="psychic">Select the psychic phase</param>
        /// <param name="shooting">Select the shooting phase</param>
        /// <param name="charge">Select the charge phase</param>
        /// <param name="fight">Select the fight phase</param>
        /// <param name="morale">Select the morale phase</param>
        /// <returns>The created value object</returns>
        public static PhaseSelection FromValues(bool command = false, bool movement = false, bool psychic = false, 
            bool shooting = false, bool charge = false, bool fight = false, bool morale = false) 
                => new PhaseSelection(command, movement, psychic, shooting, charge, fight, morale);
    }
}
