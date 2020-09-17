using Terraria.ModLoader;
using ClickerClass.Items;
using ClickerClass.Projectiles;

namespace ClickerClass
{
	class ClickerClass : Mod
	{
		public static ModHotKey AutoClickKey;
		
		public override void Load()
		{
			AutoClickKey = RegisterHotKey("Clicker Accessory", "G");
		}
		
		public ClickerClass()
		{
			
		}
	}
}
