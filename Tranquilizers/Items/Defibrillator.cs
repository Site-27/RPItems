// using Exiled.API.Features;
// using Exiled.API.Features.Attributes;
// using Exiled.API.Features.Spawn;
// using Exiled.CustomItems.API.Features;
// using Exiled.Events.EventArgs.Player;
// using MEC;
// using PlayerRoles;
// using PlayerRoles.Ragdolls;
// using System.Collections.Generic;
// using UnityEngine;
// using static PlayerList;
//
// namespace RPItems.Items;
//
// public static class PlayerExtensions
// {
//     public static bool TryGetObjectOnSight<T>(this Player player, float distance, out T target) where T : MonoBehaviour
//     {
//         target = null;
//
//         if (!Physics.Raycast(
//                 new Ray(player.ReferenceHub.PlayerCameraReference.position + player.GameObject.transform.forward * 0.3f, player.ReferenceHub.PlayerCameraReference.forward),
//                 out RaycastHit raycastHit, distance))
//             return false;
//
//         target = raycastHit.collider.GetComponentInParent<T>();
//
//         return target != null;
//     }
// }
//
// [CustomItem(ItemType.Medkit)]
// public class Defibrillator : CustomItem
// {
//     public override uint Id { get; set; } = 54;
//     public override string Name { get; set; } = "Defibrillator";
//     public override string Description { get; set; } = "Use while pointing at a corpse to revive them, if possible!";
//     public override SpawnProperties SpawnProperties { get; set; }
//     public override float Weight { get; set; }
//
//     private Dictionary<Ragdoll, (Vector3 position, RoleTypeId role, string name, string info)> _deadPlayers =
//         new Dictionary<Ragdoll, (Vector3 pos, RoleTypeId role, string name, string info)>();
//
//     protected override void SubscribeEvents()
//     {
//         Exiled.Events.Handlers.Player.UsingItem += OnItemUsing;
//         Exiled.Events.Handlers.Player.Died += OnDead;
//         base.SubscribeEvents();
//     }
//
//     protected override void UnsubscribeEvents()
//     {
//         Exiled.Events.Handlers.Player.UsingItem -= OnItemUsing;
//         Exiled.Events.Handlers.Player.Died -= OnDead;
//         base.UnsubscribeEvents();
//     }
//
//     private void OnDead(DiedEventArgs ev)
//     {
//         Timing.CallDelayed(0.1f, () =>
//         {
//             Ragdoll ragdoll = ev.Ragdoll;
//
//             _deadPlayers[ragdoll] = (
//                 ev.Player.Position,
//                 ev.Player.Role,
//                 ev.Player.DisplayNickname,
//                 ev.Player.CustomInfo
//             );
//         });
//     }
//
//     private Ragdoll GetClosestRagdoll(Player player)
//     {
//         float dist = float.MaxValue;
//         Ragdoll ragdoll = null;
//         foreach (Ragdoll obj in Ragdoll.List)
//         {
//             float tdist = Vector3.Distance(player.Transform.position, obj.Transform.position);
//             if (tdist < dist)
//             {
//                 dist = tdist;
//                 ragdoll = obj;
//             }
//         }
//
//         if (dist > 3f)
//         {
//             Log.Debug("All ragdolls are too far away, returning null.");
//             return null;
//         }
//
//         Log.Debug("Found a ragdoll within range, returning it.");
//         return ragdoll;
//     }
//
//     private void OnItemUsing(UsingItemEventArgs ev)
//     {
//         if (!Check(ev.Player.CurrentItem))
//             return;
//
//         ev.IsAllowed = false;
//
//         var ragdoll = GetClosestRagdoll(ev.Player);
//
//         if (ragdoll == null)
//         {
//             ev.Player.ShowHint("You aren't close enough to a corpse!");
//             Log.Debug("ragdoll == null");
//             return;
//         }
//
//         if (!_deadPlayers.TryGetValue(ragdoll, out var data))
//         {
//             ev.Player.ShowHint($"{data.name} can't be revived.");
//             Log.Debug("Couldn't find the ragdoll in the dictionary of dead players.");
//             return;
//         }
//
//         if (ragdoll.Owner == ev.Player)
//         {
//             ev.Player.ShowHint("You can't revive yourself.");
//             Log.Debug("Player is trying to revive themselves.");
//             return;
//         }
//
//         if (!ragdoll.Owner.IsDead)
//         {
//             ev.Player.ShowHint("It seems like a lost cause...");
//             Log.Debug("ragdoll owner isn't dead");
//             return;
//         }
//
//         if (ragdoll.ExistenceTime > 300f)
//         {
//             ev.Player.ShowHint("You're too late... they can't be saved.");
//             Log.Debug("it has been too long since the player died");
//             return;
//         }
//
//         ev.Player.RemoveHeldItem();
//
//         ragdoll.Owner.Role.Set(data.role);
//         ragdoll.Owner.Position = data.position;
//         ragdoll.Owner.DisplayNickname = data.name;
//         ragdoll.Owner.CustomInfo = data.info;
//         Log.Debug("Revived ragdoll successfully.");
//     }
// }