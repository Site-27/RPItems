using System.Collections;
using Exiled.API.Interfaces;
using System.ComponentModel;


namespace RPItems;

public class Config : IConfig/*, IEnumerable*/
{
    [Description("Whether the plugin is enabled or not.")]
    public bool IsEnabled { get; set; } = true;

    [Description("Whether to show debug messages in the console.")]
    public bool Debug { get; set; } = false;

    [Description("")]
    public float LightTranqDamage { get; set; } = 1f;
    [Description("")]
    public float HeavyTranqDamage { get; set; } = 1f;

    [Description("")]
    public float KnifeDamage { get; set; } = 15f;

    

    [Description("")]
    public float LightTranqDuration { get; set; } = 30f;

    // public IEnumerator GetEnumerator()
    // {
    //     throw new System.NotImplementedException();
    // }
}