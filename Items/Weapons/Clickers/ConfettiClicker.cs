using ClickerClass.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class ConfettiClicker : ClickerWeapon
	{
		//TODO - I'll need dire to help me out on this one
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.PartyTime = ClickerSystem.RegisterClickEffect(Mod, "PartyTime", null, null, 1, new Color(0, 175, 225), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				SoundEngine.PlaySound(SoundID.Item24, player.Center); 
				//Make confetti here
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.25f);
			SetColor(Item, new Color(25, 175, 225));
			SetDust(Item, 285);
			AddEffect(Item, ClickEffect.PartyTime);

			Item.damage = 10;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 10000;
			Item.rare = 2;
		}
		
		//Make it drop from Pigronatas
	}
}
