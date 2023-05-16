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
                if (equipment.asset.type == EItemType.GUN)
                {
                    equipment.quality = 100;
                    equipment.sendUpdateQuality();
                    ItemGunAsset gun = (ItemGunAsset)Assets.find(EAssetType.ITEM, equipment.asset.id);
                    ChatManager.serverSendMessage($"คุณได้ซ่อมอาวุธ <color=#FF3557>{gun.itemName}({gun.id})</color> เรียบร้อยแล้ว!", Color.white, null, player.SteamPlayer(), EChatMode.SAY, "https://unturned.supernovea.online/services/icon?query=anya", true);
                }else if (equipment.asset.type == EItemType.MELEE)
                {
                    equipment.quality = 100;
                    equipment.sendUpdateQuality();
                    ItemMeleeAsset melee = (ItemMeleeAsset)Assets.find(EAssetType.ITEM, equipment.asset.id);
                    ChatManager.serverSendMessage($"คุณได้ซ่อมอาวุธ <color=#FF3557>{melee.itemName}({melee.id})</color> เรียบร้อยแล้ว!", Color.white, null, player.SteamPlayer(), EChatMode.SAY, "https://unturned.supernovea.online/services/icon?query=anya", true);
                }
                else
                {
                    ChatManager.serverSendMessage("คุณไม่สามารถซ่อมสิ่งนี้ได้ครับ", Color.red, null, player.SteamPlayer(), EChatMode.SAY, "https://unturned.supernovea.online/services/icon?query=anya", true);
                }
            }
            else
            {
                ChatManager.serverSendMessage("คุณใช้คำสั่งผิด!", Color.red, null, player.SteamPlayer(), EChatMode.SAY, "https://unturned.supernovea.online/services/icon?query=anya", true);
            }
        }
    }
}
