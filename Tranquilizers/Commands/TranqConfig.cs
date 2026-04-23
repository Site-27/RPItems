// using System;
// using System.Collections.Generic;
// using System.Diagnostics.CodeAnalysis;
// using System.Linq;
// using CommandSystem;
// using EasyTmp;
// using Exiled.CustomItems.API.Features;
// using JetBrains.Annotations;
//
// namespace RPItems.Commands;
//
// public class ItemConfig
// {
//     [UsedImplicitly]
//     [CommandHandler(typeof(RemoteAdminCommandHandler))]
//     public partial class TranqMod : ICommand
//     {
//         public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
//         {
//             var args = arguments;
//             switch (args.Count)
//             {
//                 case 0:
//                     response = Usage;
//                     return false;
//                 // ReSharper disable StringLiteralTypo
//                 case 1:
//                     if (arguments.At(0).ToLower() is "l" or "list" or "listall" or "ls")
//                     {
//                         var builder = EasyArgs.Build();
//                         foreach (var item in ItemInfo)
//                         {
//                             if (item.Value == null) continue;
//                             builder.Blue(string.Join(" ", item)).NewLine();
//                         }
//
//                         response = builder.Done();
//                         break;
//                     }
//                 // ReSharper restore StringLiteralTypo
//                 case 2:
//                     
//                     break;
//                 case 4:
//                     
//                     break;
//                 default:
//                     response = Usage;
//                     return false;
//             }
//         }
//     }
//     public partial class TranqMod
//     {
//         public string Command { get; set; } = "TranqMod";
//         // ReSharper disable StringLiteralTypo
//         public string[] Aliases { get; set; } = ["ChangeTranquilizers", "ChangeTranqs", "TranqModify", "ModifyTranquilizers", "TranqModifications"];
//         // ReSharper enable StringLiteralTypo
//         public string Description { get; set; } = "";
//         private string Usage { get; } = EasyArgs.Build().CmdArguments("tranqmod").Done();
//         // private static string? ItemLister
//         // {
//         //     get
//         //     {
//         //         var builder = EasyArgs.Build();
//         //         foreach (var customItem in CustomItem.Registered)
//         //         {
//         //             builder.Blue("").Parameter(customItem.Name).NewLine().Parameter(customItem.Description);
//         //         }
//         //         return builder.Done();
//         //     }
//         // }
//         
//         /// <summary>
//         /// The first string in the dictionary is the item's name, the second is the description.
//         /// </summary>
//         private static Dictionary<string, string?> ItemInfo
//         {
//             get
//             {
//                 Dictionary<string, string?> dictionary = new();
//                 foreach (var customItem in CustomItem.Registered.Where(customItem => !dictionary.ContainsKey(customItem.Name)))
//                     dictionary.Add(customItem.Name, customItem.Description);
//
//                 return dictionary;
//             }
//         }
//
//         private static List<object> _configurations = [];
//     }
//
//     public partial class TranqMod
//     {
//         public static void GetDefaultValues()
//         {
//             foreach (var config in Plugin.Instance.Config)
//             {
//                 _configurations.Add(config);
//             }
//         }
//     }
// }