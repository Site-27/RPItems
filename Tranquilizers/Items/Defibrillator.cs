/*

using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.API.Enums;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using System.Collections.Generic;
using UnityEngine;

namespace RPItems.Items
{
    [CustomItem(ItemType.Medkit)]
    public class Defibrillator : CustomItem
    {
        public override uint Id { get; set; } = 54;
        public override string Name { get; set; } = "Defibrillator";
        public override string Description { get; set; } = "Use while pointing at a corpse to revive them, if possible!";
        public override SpawnProperties SpawnProperties { get; set; }
        public override float Weight { get; set; }

        private Dictionary<Ragdoll, (Vector3 position, RoleTypeId role, string name, string info)> _deadPlayers = new Dictionary<Ragdoll, (Vector3 pos, RoleTypeId role, string name, string info)>();

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsingItem += OnItemUsing;
            Exiled.Events.Handlers.Player.Died += OnDead;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsingItem -= OnItemUsing;
            Exiled.Events.Handlers.Player.Died -= OnDead;
            base.UnsubscribeEvents();
        }

        private void OnDead(DiedEventArgs ev)
        {
            Timing.CallDelayed(0.1f, () =>
            {
                Ragdoll ragdoll = ev.Ragdoll;

                _deadPlayers[ragdoll] = (
                    ev.Player.Position,
                    ev.Player.Role,
                    ev.Player.DisplayNickname,
                    ev.Player.CustomInfo
                );
            });
        }

        private void OnItemUsing(UsingItemEventArgs ev)
        {

            Ragdoll ragdoll = null;

            if (!Check(ev.Player.CurrentItem))
                return;

            ev.IsAllowed = false;

            // check if the ray hits a ragdoll??
            if (!Physics.Raycast(new Ray(ev.Player.CameraTransform.position, ev.Player.CameraTransform.forward), out var hit, 5f))
            {
                Log.Debug("Raycast didn't hit anything.");
                return;
            }

            if (!hit.collider.attachedRigidbody == ragdoll.SpecialRigidbodies)

            if (ragdoll == null)
            {
                ev.Player.ShowHint("Can only be used when looking at a body.");
                Log.Debug("Raycast hit something, but it doesn't have a Ragdoll component.");
                return;
            }

            if (!_deadPlayers.TryGetValue(ragdoll, out var data))
            {
                ev.Player.ShowHint("This body can't be revived.");
                Log.Debug("Couldn't find the ragdoll in the dictionary of dead players.");
                return;
            }

            if (ragdoll.Owner == ev.Player)
                return;

            if (!ragdoll.Owner.IsDead)
            {
                ev.Player.ShowHint("It seems like a lost cause...");
                return;
            }

            if (ragdoll.ExistenceTime > 300f)
            {
                ev.Player.ShowHint("You're too late... they can't be saved.");
                return;
            }

            ev.Player.RemoveHeldItem();

            ragdoll.Owner.Role.Set(data.role);
            ragdoll.Owner.Position = data.position;
            ragdoll.Owner.DisplayNickname = data.name;
            ragdoll.Owner.CustomInfo = data.info;
        }
    }
}*/
