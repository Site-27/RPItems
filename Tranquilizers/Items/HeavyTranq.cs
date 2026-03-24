using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using System.ComponentModel;
using System;

namespace RPItems.Items
{
    [CustomItem(ItemType.GunA7)]
    public class HeavyTranq : CustomWeapon
    {
        public override uint Id { get; set; } = 51;
        public override string Name { get; set; } = "Heavy Tranquilizer";
        public override string Description { get; set; } = "Shoots a tranquilizing dart to calm an SCP. May be dangerous to use on humans.";
        public override byte ClipSize { get; set; } = 1;
        public override float Weight { get; set; }
        public override SpawnProperties SpawnProperties { get; set; }

        [Description("Time in seconds SCPs are affected by the heavy tranquilizer.")]
        public float SCPDuration { get; set; } = 90f;

        [Description("Time in seconds humans are affected by the heavy tranquilizer.")]
        public float HumanDuration { get; set; } = 150f;

        Random rnd = new Random();
        protected override void OnHurting(HurtingEventArgs ev)
        {
            Player target = ev.Player;

            ev.Amount = Plugin.Instance.Config.TranqDamage;

            if (target.IsScp)
            { 
                target.EnableEffect(Exiled.API.Enums.EffectType.Concussed, SCPDuration);
                target.EnableEffect(Exiled.API.Enums.EffectType.Slowness, 25, SCPDuration);
                target.EnableEffect(Exiled.API.Enums.EffectType.Blinded, 30, SCPDuration);
                target.ShowHint("<color=#FF0000>You were hit by a Heavy Tranquilizer! You are tranquilized for a few minutes...</color>");
                Log.Debug($"Player {target.Nickname} was hit by a Heavy Tranquilizer, applying effects for {SCPDuration}");
            }

            if (!target.IsScp)
            {
                target.EnableEffect(Exiled.API.Enums.EffectType.Concussed, HumanDuration);
                target.EnableEffect(Exiled.API.Enums.EffectType.Slowness, 45, HumanDuration);
                target.EnableEffect(Exiled.API.Enums.EffectType.Exhausted, HumanDuration);
                target.EnableEffect(Exiled.API.Enums.EffectType.Blinded, 45, HumanDuration);

                int ArrestChance = rnd.Next(1, 5);
                if (ArrestChance == 1)
                {
                    target.EnableEffect(Exiled.API.Enums.EffectType.CardiacArrest);
                    target.ShowHint("<color=#FF0000>You were hit by a Heavy Tranquilizer! Your heart spasms...</color>");
                    Log.Debug($"Player {target.Nickname} was hit by a Heavy Tranquilizer, rolled {ArrestChance}, applying cardiac arrest and effects for {HumanDuration}");
                }
                else
                {
                    target.ShowHint("<color=#FF0000>You were hit by a Heavy Tranquilizer! It incapacitates you. You don't feel very good...</color>");
                    Log.Debug($"Player {target.Nickname} was hit by a Heavy Tranquilizer, rolled {ArrestChance}, applying effects for {HumanDuration}");
                }
            }
        }
    }
}
