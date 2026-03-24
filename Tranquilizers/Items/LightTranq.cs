using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using System.ComponentModel;


namespace RPItems.Items
{
    [CustomItem(ItemType.GunCOM18)]
    public class LightTranq : CustomWeapon
    {
        public override uint Id { get; set; } = 50;
        public override string Name { get; set; } = "Light Tranquilizer";
        public override string Description { get; set; } = "Shoots a tranquilizing dart to calm a rowdy target. Ineffective against SCPs.";
        public override byte ClipSize { get; set; } = 2;
        public override float Weight { get; set; }
        public override SpawnProperties SpawnProperties { get; set; }

        [Description("Time in seconds humans are affected by the light tranquilizer.")]
        public float Duration { get; set; } = 30f;
        protected override void OnHurting(HurtingEventArgs ev)
        {
            {
                Player target = ev.Player;

                ev.Amount = Plugin.Instance.Config.TranqDamage;

                if (target.IsScp)
                    return;

                target.EnableEffect(Exiled.API.Enums.EffectType.Concussed, Duration);
                target.EnableEffect(Exiled.API.Enums.EffectType.Slowness, 25, Duration);
                target.EnableEffect(Exiled.API.Enums.EffectType.Blinded, 30, Duration);
                target.ShowHint("<color=#FF0000>You were hit by a Light Tranquilizer! You feel sleepy...</color>");
                Log.Debug($"Player {target.Nickname} was hit by a Light Tranquilizer, applying effects for {Duration} seconds.");
            }
        }
    }
}