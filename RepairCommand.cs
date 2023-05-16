using Rocket.API;
using Rocket.Unturned.Chat;
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
    public class RepairCommand : IRocketCommand
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
                    VehicleAsset vehicle = (VehicleAsset)Assets.find(EAssetType.VEHICLE, PlayerVehicle.asset.id);
                    switch (command[0])
                    {
                        case "repair":
                            PlayerVehicle.askRepair(100);
                            ChatManager.serverSendMessage($"คุณได้ซ่อม <color=#FF3557>{vehicle.vehicleName}({vehicle.id})</color> เรียบร้อยแล้ว!", Color.white, null, player.SteamPlayer(), EChatMode.SAY, "https://unturned.supernovea.online/services/icon?query=anya", true);
                            foreach (Wheel ล้อ in PlayerVehicle.tires)
                            {
                                PlayerVehicle.askRepairTire(ล้อ.index);
                            }
                            break;
                        case "refill":
                            player.CurrentVehicle.askFillFuel(100);
                            ChatManager.serverSendMessage($"คุณได้เติมน้ำมันให้เต็มถังให้ <color=#FF3557>{vehicle.vehicleName}({vehicle.id})</color> เรียบร้อยแล้ว!", Color.white, null, player.SteamPlayer(), EChatMode.SAY, "https://unturned.supernovea.online/services/icon?query=anya", true);
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
