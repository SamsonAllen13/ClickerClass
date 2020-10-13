using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class OverclockBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Overclock");
			Description.SetDefault("Every click will trigger a clicker effect");
			Main.buffNoSave[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			
		}
	}
}
