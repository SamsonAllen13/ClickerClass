using ClickerClass.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class ImpishClicker : ClickerWeapon
	{
		//May be used later
		public const int DashDamage = 11;
		public const float DashKB = 0f;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			MoRSupportHelper.RegisterElement(Item, MoRSupportHelper.Fire, projsInheritItemElements: true);

			ClickEffect.HotWings = ClickerSystem.RegisterClickEffect(Mod, "HotWings", 15, new Color(240, 35, 0), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				SoundEngine.PlaySound(SoundID.Item24, player.Center);
				player.AddBuff(ModContent.BuffType<HotWingsBuff>(), 300, false);
				for (int i = 0; i < 15; i++)
				{
					int index = Dust.NewDust(player.position, player.width, player.height, 174, 0f, 0f, 150, default(Color), 1.25f);
					Dust dust = Main.dust[index];
					dust.noGravity = true;
					dust.velocity *= 0.75f;
					int x = Main.rand.Next(-50, 51);
					int y = Main.rand.Next(-50, 51);
					dust.position.X += x;
					dust.position.Y += y;
					dust.velocity.X = -x * 0.075f;
					dust.velocity.Y = -y * 0.075f;
				}
			},
			preHardMode: true);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.75f);
			SetColor(Item, new Color(250, 40, 0));
			SetDust(Item, 174);
			AddEffect(Item, ClickEffect.HotWings);

			Item.damage = 11;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 0, 60, 0);
			Item.rare = ItemRarityID.Orange;
		}
	}
}
