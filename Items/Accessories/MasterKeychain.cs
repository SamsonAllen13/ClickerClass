using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ClickerClass.Items.Accessories
{
	public class MasterKeychain : ClickerItem
	{
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = 75000;
			Item.rare = 7;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.accHotKeychain = true;
			clickerPlayer.EnableClickEffect(ClickEffect.ClearKeychain);
			clickerPlayer.EnableClickEffect(ClickEffect.StickyKeychain);
		}
	}
}
