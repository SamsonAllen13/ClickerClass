using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class AutoClick : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Auto Click");
			Description.SetDefault("Hold down the Left Mouse Button to auto click");
			Main.buffNoSave[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{

		}
	}
}
