using Exiled.API.Features.Attributes;
using Exiled.CustomItems.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.Events.EventArgs.Player;
using MEC;

namespace RPItems.Items
{
    [CustomItem(ItemType.SCP207)]
    public class CokeyCola : CustomItem
    {
        public override uint Id { get; set; } = 55;
        public override string Name { get; set; } = "Cokey Cola";
        public override string Description { get; set; } = "A bottle of normal, non-anomalous Coke!";
        public override float Weight { get; set; }
        public override SpawnProperties? SpawnProperties { get; set; }

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsedItem += OnUsedItem;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsedItem -= OnUsedItem;
            base.UnsubscribeEvents();
        }

        private void OnUsedItem(UsedItemEventArgs ev)
        {
            if (!Check(ev.Usable))
                return;

            Timing.CallDelayed(Timing.WaitForOneFrame, delegate
                {
                    ev.Player.DisableEffect(Exiled.API.Enums.EffectType.Scp207);
                    ev.Player.ShowHint("Tastes just like a normal Coke. Refreshing!", 10f);
                });
        }
    }
}
