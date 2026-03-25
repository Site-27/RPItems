using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Firearms.Attachments;
using PlayerStatsSystem;

namespace RPItems.Items
{
    [CustomItem(ItemType.GunE11SR)]
    public class SniperRifle : CustomWeapon
    {
        public override uint Id { get; set; } = 52;
        public override string Name { get; set; } = "Sniper Rifle";
        public override string Description { get; set; } = "A modified E-11 with a scope that does more damage, but only holds one bullet at a time.";
        public override float Weight { get; set; }
        public override byte ClipSize { get; set; } = 1;
        public override SpawnProperties SpawnProperties { get; set; }
        public override AttachmentName[] Attachments { get; set; } = new[]
        {
            AttachmentName.ScopeSight,
            AttachmentName.RifleBody,
            AttachmentName.LowcapMagAP,
        };
        public float DamageMultiplier { get; set; } = 2.1f;

        protected override void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Attacker != ev.Player && ev.DamageHandler.Base is FirearmDamageHandler firearmDamageHandler && firearmDamageHandler.WeaponType == ev.Attacker.CurrentItem.Type)
                ev.Amount *= DamageMultiplier;
        }
    }
}
