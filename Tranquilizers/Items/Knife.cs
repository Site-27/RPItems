
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Scp1509;
using Exiled.Events.EventArgs.Player;
using Exiled.API.Features;

namespace RPItems.Items
{
    [CustomItem(ItemType.SCP1509)]
    public class Knife : CustomItem
    {
        public override uint Id { get; set; } = 53;
        public override string Name { get; set; } = "NerfedMachete";
        public override string Description { get; set; } = "Sharp knife. Or maybe claws.";
        public override SpawnProperties SpawnProperties { get; set; }
        public override float Weight { get; set; }
        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Scp1509.Resurrecting += OnResurrecting;
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
            base.SubscribeEvents();
        }
        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Scp1509.Resurrecting -= OnResurrecting;
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
            base.UnsubscribeEvents();
        }

        private void OnResurrecting(ResurrectingEventArgs ev)
        {
            if (!Check(ev.Player.CurrentItem))
                return;

            ev.IsAllowed = false;
        }

        private void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Player == null || ev.Attacker == null || ev.Attacker.CurrentItem == null)
                return;

            if (!Check(ev.Attacker.CurrentItem))
            {
                Log.Debug($"Player: {ev.Player}, Attacker: {ev.Attacker}, Attacker's Item: {ev.Attacker.CurrentItem}");
                return;
            }

            ev.Amount = Plugin.Instance.Config.KnifeDamage;
        }
    }
}
