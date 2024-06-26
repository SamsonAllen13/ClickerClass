using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Dyes;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace ClickerClass.Items.Consumables
{
	/// <summary>
	/// Handles automatically registering shader, common SetDefaults
	/// </summary>
	public abstract class HairDyeBase : ClickerItem
	{
		/// <summary>
		///  If true, automatically registers the shader using <see cref="LegacyShaderMethod"/>
		/// </summary>
		public virtual bool UsesLegacyShader => true;

		public virtual Color LegacyShaderMethod(Player player, Color newColor, ref bool lighting)
		{
			return newColor;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			if (!Main.dedServ && UsesLegacyShader)
			{
				GameShaders.Hair.BindShader(Type, new LegacyHairShaderData().UseLegacyMethod(LegacyShaderMethod));
			}
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 26;
			Item.maxStack = Item.CommonMaxStack;
			Item.UseSound = SoundID.Item3;
			Item.useStyle = ItemUseStyleID.DrinkLiquid;
			Item.useTurn = true;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.consumable = true;

			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.Green;
		}
	}
}
