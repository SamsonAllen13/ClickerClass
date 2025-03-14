using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Items.Consumables
{
	public class ClickSpeedHairDye : HairDyeBase
	{
		public readonly static Color blue = new Color(171, 242, 244);
		public readonly static Color red = new Color(255, 39, 15);

		public override Color LegacyShaderMethod(Player player, Color newColor, ref bool lighting)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			float cpsRatio = clickerPlayer.clickerPerSecond / 15f;
			cpsRatio = MathHelper.Clamp(0f, cpsRatio, 1f);
			return Color.Lerp(blue, red, cpsRatio);
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
				new Color(132, 202, 232),
				new Color(201, 124, 182),
				new Color(252, 43, 34)
			};
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.Green;
		}
	}
}
