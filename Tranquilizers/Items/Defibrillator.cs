using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using PlayerRoles.Ragdolls;
using System.Collections.Generic;
using UnityEngine;
using static PlayerList;

namespace RPItems.Items;

[CustomItem(ItemType.Medkit)]
public class Defibrillator : CustomItem
{
     public override uint Id { get; set; } = 54;
     public override string Name { get; set; } = "Defibrillator";
     public override string Description { get; set; } = "Use while pointing at a corpse to revive them, if possible!";
     public override SpawnProperties? SpawnProperties { get; set; }
     public override float Weight { get; set; }

     private Dictionary<Player, (Vector3 position, RoleTypeId role, string name, string info)> _deadPlayers =
         new Dictionary<Player, (Vector3 pos, RoleTypeId role, string name, string info)>();

     protected override void SubscribeEvents()
     {
        Exiled.Events.Handlers.Player.UsingItem.Subscribe(OnItemUsing);
        Exiled.Events.Handlers.Player.Dying.Subscribe(OnDying);
        base.SubscribeEvents();
     }

     protected override void UnsubscribeEvents()
     {
        Exiled.Events.Handlers.Player.UsingItem.Unsubscribe(OnItemUsing);
        Exiled.Events.Handlers.Player.Dying.Unsubscribe(OnDying);
        base.UnsubscribeEvents();
     }

     private void OnDying(DyingEventArgs ev)
     {
         Player player = ev.Player;

         _deadPlayers[player] = (
             ev.Player.Position,
             ev.Player.Role,
             ev.Player.CustomName,
             ev.Player.CustomInfo
         );
     }

     private Ragdoll? GetClosestRagdoll(Player player)
     {
         float dist = float.MaxValue;
         Ragdoll? ragdoll = null;
         foreach (Ragdoll obj in Ragdoll.List)
         {
             float tdist = Vector3.Distance(player.Transform.position, obj.Transform.position);
             if (tdist < dist)
             {
                 dist = tdist;
                 ragdoll = obj;
             }
         }

         if (dist > 3f)
         {
             Log.Debug("All ragdolls are too far away, returning null.");
             return null;
         }

         Log.Debug("Found a ragdoll within range, returning it.");
         return ragdoll;
     }

     private void OnItemUsing(UsingItemEventArgs ev)
     {
         if (!Check(ev.Player.CurrentItem))
             return;

         ev.IsAllowed = false;

         var ragdoll = GetClosestRagdoll(ev.Player);

         if (ragdoll == null)
         {
             ev.Player.ShowHint("You aren't close enough to a corpse!");
             Log.Debug("ragdoll == null");
             return;
         }

         if (!_deadPlayers.TryGetValue(ragdoll.Owner, out var data))
         {
             ev.Player.ShowHint($"{data.name} can't be revived.");
             Log.Debug("Couldn't find the ragdoll in the dictionary of dead players.");
             return;
         }

         if (ragdoll.Owner == ev.Player)
         {
             ev.Player.ShowHint("You can't revive yourself.");
             Log.Debug("Player is trying to revive themselves.");
             return;
         }

         if (!ragdoll.Owner.IsDead)
         {
             ev.Player.ShowHint("It seems like a lost cause...");
             Log.Debug("ragdoll owner isn't dead");
             return;
         }

         if (ragdoll.ExistenceTime > 300f)
         {
             ev.Player.ShowHint("You're too late... they can't be saved.");
             Log.Debug("it has been too long since the player died");
             return;
         }

         ev.Player.RemoveHeldItem();

         ragdoll.Owner.Role.Set(data.role);
         Log.Debug("Set ragdoll's role to " + data.role);
         ragdoll.Owner.Position = data.position;
         Log.Debug("Set ragdoll's position to " + data.position);
         ragdoll.Owner.DisplayNickname = data.name;
         Log.Debug("Set ragdoll's nickname to " + data.name);
         ragdoll.Owner.CustomInfo = data.info;
         Log.Debug("Set ragdoll's custom info to " + data.info);
         ragdoll.Owner.ShowHint("You were revived via defibrillator!");
         ragdoll.Destroy();
         Log.Debug("Revived ragdoll successfully.");
     }
 }