using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static SDG.Provider.SteamGetInventoryResponse;

namespace Supernovea.VehicleRepair
{
    public class ItemRepairCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "ir";

        public string Help => "";

        public string Syntax => "";

        public List<string> Aliases => new List<string>() { };

        public List<string> Permissions => new List<string>() { "ItemRepair" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = caller as UnturnedPlayer;
            PlayerEquipment equipment = player.Player.equipment;
            if (command.Length == 0 && equipment.isEquipped && equipment.asset != null)
            {
                equipment.quality = 100;
                equipment.sendUpdateQuality();
                ItemAsset item = (ItemAsset)Assets.find(EAssetType.ITEM, equipment.asset.id);
                ChatManager.serverSendMessage($"คุณได้ซ่อม <color=#FF3557>{item.itemName}({item.id})</color> เรียบร้อยแล้ว!", Color.white, null, player.SteamPlayer(), EChatMode.SAY, "https://unturned.supernovea.online/services/icon?query=anya", true);
            }
            else
            {
                ChatManager.serverSendMessage("คุณใช้คำสั่งผิด!", Color.red, null, player.SteamPlayer(), EChatMode.SAY, "https://unturned.supernovea.online/services/icon?query=anya", true);
            }
        }
    }
}
