using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class Haste : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoSave[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.jumpSpeedBoost = 2f;
			player.moveSpeed += 0.25f;
			player.maxRunSpeed += 0.25f;
		}
	}
}
