using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using System.ComponentModel;
using Exiled.API.Enums;


namespace RPItems.Items;

[CustomItem(ItemType.GunCOM18)]
public class LightTranq : CustomWeapon
{
    public override uint Id { get; set; } = 50;
    public override string Name { get; set; } = "Light Tranquilizer";
    public override string Description { get; set; } = "Shoots a tranquilizing dart to calm a rowdy target. Ineffective against SCPs.";
    public override byte ClipSize { get; set; } = 2;
    public override float Weight { get; set; }
    public override SpawnProperties? SpawnProperties { get; set; }

    /// <summary>
    /// Configurable via config.
    /// Time, in seconds, that humans are affected by the light tranquilizer.
    /// </summary>
    private static float Duration => Plugin.Instance.Config.LightTranqDuration;

    protected override void OnHurting(HurtingEventArgs ev)
    {
        var plr = ev.Player;
        Log.Debug($"[LightTranq] OnHurting running on player \"{plr.Nickname}\".");
        if (ev.Player.IsDead)
        {
            Log.Debug($"[LightTranq] Player \"{plr.Nickname}\" is dead. Returning.");
            return;
        }
        if (!ev.Player.IsConnected)
        {
            Log.Debug($"[LightTranq] Player \"{plr.Nickname}\" is disconnected. Returning.");
            return;
        }

        Log.Debug($"[LightTranq] OnHurting has begun for \"{plr.Nickname}\"");

        ev.Amount = Plugin.Instance.Config.LightTranqDamage;
        
        if (plr.IsScp)
        {
            Log.Debug($"[LightTranq] Target \"{plr.Nickname}\"was SCP. Returning.");
            return;
        }

        plr.EnableEffect(EffectType.Concussed, Duration);
        plr.EnableEffect(EffectType.Slowness, 25, Duration);
        plr.EnableEffect(EffectType.Blinded, 30, Duration);
        plr.ShowHint("<color=#FF0000>You were hit by a Light Tranquilizer! You feel sleepy...</color>");
        Log.Debug($"Player {plr.Nickname} was hit by a Light Tranquilizer, applying effects for {Duration} seconds.");
    }
}