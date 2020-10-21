using ClickerClass.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass
{
	public class ClickerClass : Mod
	{
		public static ModHotKey AutoClickKey;
		internal static ClickerClass mod;

		public override void Load()
		{
			mod = this;
			AutoClickKey = RegisterHotKey("Clicker Accessory", "G");
			ShaderManager.Load();
		}

		public override void Unload()
		{
			ShaderManager.Unload();
			AutoClickKey = null;
			mod = null;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(this);
			recipe.AddIngredient(null, "ClickerEmblem", 1);
			recipe.AddIngredient(ItemID.SoulofMight, 5);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(ItemID.AvengerEmblem, 1);
			recipe.AddRecipe();
		}
	}
}
