using System;
using ClickerClass.Core;
using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Body)]
	public class MiceSuit : ClickerItem
	{
		public static readonly float DamageIncrease = 0.14f;
		public static readonly float RadiusIncrease = 0.25f;

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageIncrease * 100f, RadiusIncrease * 100f);

		public static Lazy<Asset<Texture2D>> glowmask;

		public override void Unload()
		{
			glowmask = null;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();

			if (!Main.dedServ)
			{
				glowmask = new(() => ModContent.Request<Texture2D>(Texture + "_Glow"));

				BodyGlowmaskPlayer.RegisterData(Item.bodySlot, () => new Color(255, 255, 255, 0) * 0.8f);
			}
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(0, 14, 0, 0);
			Item.rare = ItemRarityID.Red;
			Item.defense = 28;
		}

		public override void UpdateEquip(Player player)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			player.GetDamage<ClickerDamage>() += DamageIncrease;
			clickerPlayer.clickerRadius += 2 * RadiusIncrease;
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(Item.Center, 0.1f, 0.1f, 0.3f);
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Item.BasicInWorldGlowmask(spriteBatch, glowmask.Value.Value, new Color(255, 255, 255, 0) * 0.8f, rotation, scale);
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<MiceFragment>(), 20).AddIngredient(ItemID.LunarBar, 16).AddTile(TileID.LunarCraftingStation).Register();
		}
	}
}
