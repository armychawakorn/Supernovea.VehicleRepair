using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Supernovea.VehicleRepair
{
    internal class RepairCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "vr";

        public string Help => "";

        public string Syntax => "";

        public List<string> Aliases => new List<string>() { };

        public List<string> Permissions => new List<string>() { "VehicleRepair" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = caller as UnturnedPlayer;
            if (command.Length == 1)
            {
                if (player.IsInVehicle)
                {
                    InteractableVehicle PlayerVehicle = player.CurrentVehicle;
                    switch (command[0])
                    {
                        case "repair":
                            PlayerVehicle.askRepair(100);
                            ChatManager.serverSendMessage("คุณได้ทำการซ่อมรถเรียบร้อยแล้ว", Color.green, null, player.SteamPlayer(), EChatMode.SAY, "https://unturned.supernovea.online/services/icon?query=anya", true);
                            foreach (Wheel ล้อ in PlayerVehicle.tires)
                            {
                                PlayerVehicle.askRepairTire(ล้อ.index);
                            }
                            break;
                        case "refill":
                            player.CurrentVehicle.askFillFuel(100);
                            ChatManager.serverSendMessage("คุณได้เติมน้ำมันให้เต็มถังเรียบร้อยแล้ว", Color.green, null, player.SteamPlayer(), EChatMode.SAY, "https://unturned.supernovea.online/services/icon?query=anya", true);
                            break;
                        default:
                            ChatManager.serverSendMessage("คุณใช้คำสั่งผิดครับ", Color.red, null, player.SteamPlayer(), EChatMode.SAY, "https://unturned.supernovea.online/services/icon?query=anya", true);
                            break;
                    }
                }
                else
                {
                    ChatManager.serverSendMessage("คุณไม่ได้อยู่ในยานพาหนะ", Color.red, null, player.SteamPlayer(), EChatMode.SAY, "https://unturned.supernovea.online/services/icon?query=anya", true);
                }
            }
            else
            {
                ChatManager.serverSendMessage("คุณใช้คำสั่งผิดครับ", Color.red, null, player.SteamPlayer(), EChatMode.SAY, "https://unturned.supernovea.online/services/icon?query=anya", true);
            }
        }
    }
}
