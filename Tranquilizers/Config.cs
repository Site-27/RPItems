using Exiled.API.Interfaces;
using System.ComponentModel;


namespace RPItems
{
    public class Config : IConfig
    {
        [Description("Whether the plugin is enabled or not.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Whether to show debug messages in the console.")]
        public bool Debug { get; set; } = false;

        public float TranqDamage { get; set; } = 1f;

        public float KnifeDamage { get; set; } = 15f;

    }
}