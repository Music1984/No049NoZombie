namespace No049NoZombie
{
    using System.ComponentModel;
    using Exiled.API.Interfaces;

    /// <inheritdoc />
    public class Config : IConfig
    {
        /// <inheritdoc/>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether debug messages should be shown in the console.
        /// </summary>
        [Description("Whether debug logs should be shown in the console.")]
        public bool Debug { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether or not all scps should die for zombies to die.
        /// </summary>
        [Description("Whether or not all other scps should die in order for zombies to slowly die. false by default")]
        public bool AllScpsDead { get; set; } = true;

        /// <summary>
        /// Gets or sets the value of the damage dealt each tick to a zombie.
        /// </summary>
        public float DamageIntensity { get; set; } = 25f;

        /// <summary>
        /// Gets or sets the poison effect intensity
        /// </summary>
        public byte PoisonIntensity { get; set; }
    }
}