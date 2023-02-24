using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class ConfettiClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ClickEffect.PartyTime = ClickerSystem.RegisterClickEffect(Mod, "PartyTime", 1, new Color(25, 175, 225), delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				var dustRect = Utils.CenteredRectangle(position, new Vector2(10));
				for (int i = 0; i < 10; i++)
				{
					int dustType = Main.rand.Next(139, 143); //Confetti dusts
					var dust = Dust.NewDustDirect(dustRect.TopLeft(), dustRect.Width, dustRect.Height, dustType);

					dust.velocity.X += Main.rand.NextFloat(-0.05f, 0.05f);
					dust.velocity.Y += Main.rand.NextFloat(-0.05f, 0.05f);

					dust.scale *= 1f + Main.rand.NextFloat(-0.03f, 0.03f);
				}
			},
			preHardMode: true);
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
			Item.value = Item.sellPrice(0, 0, 20, 0);
			Item.rare = ItemRarityID.Blue;
		}
	}

	public class ConfettiClickerPigronataTile : GlobalTile
	{
		public override void Drop(int i, int j, int type)
		{
			if (type == TileID.Pigronata /*&& Main.rand.NextBool(50)*/)
			{
				Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<ConfettiClicker>());
			}
		}
	}
}
