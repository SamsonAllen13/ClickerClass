using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class FaultyClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.Fritz = ClickerSystem.RegisterClickEffect(Mod, "Fritz", null, null, 3, new Color(100, 100, 100), delegate (Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				var clickerPlayer = player.GetModPlayer<ClickerPlayer>();
				
				int radius = (int)(95 * clickerPlayer.clickerRadius);
				if (radius > 350)
				{
					radius = 350;
				}
				
				//Hopefully this doesnt offend dire
				double xOffset = 0;
				double yOffset = 0;
				while (!Collision.CanHit(new Vector2(player.Center.X, player.Center.Y - 12), 1, 1, new Vector2((float)xOffset, (float)yOffset), 1, 1))
				{
					//Circles give me a damn headache...
					double r = radius * Math.Sqrt(Main.rand.NextFloat(0f, 1f));
					double theta = Main.rand.NextFloat(0f, 1f) * MathHelper.TwoPi;
					xOffset = player.Center.X + r * Math.Cos(theta);
					yOffset = player.Center.Y + r * Math.Sin(theta);
				}

				Projectile.NewProjectile(source, (float)xOffset, (float)yOffset, 0f, 0f, ModContent.ProjectileType<FaultyClickerPro>(), (int)(damage * 1.5f), 0f, player.whoAmI);
			});
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1f);
			SetColor(Item, new Color(120, 120, 120));
			SetDust(Item, 54);
			AddEffect(Item, ClickEffect.Fritz);

			Item.damage = 6;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 10000;
			Item.rare = 1;
		}
		
		//Generated in Deadman's chests
	}
}
