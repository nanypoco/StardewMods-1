using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using StardewValley;
using StardewValley.Menus;

namespace Leclair.Stardew.Almanac.Pages {
	public interface IAlmanacPage {

		// Type
		PageType Type { get; }
		bool IsMagic { get; }

		// Events
		void Activate();
		void Deactivate();
		void DateChanged(WorldDate oldDate, WorldDate newDate);

		void UpdateComponents();
		List<ClickableComponent> GetComponents();

		bool ReceiveGamePadButton(Buttons b);
		bool ReceiveKeyPress(Keys key);
		bool ReceiveScroll(int x, int y, int direction);
		bool ReceiveLeftClick(int x, int y, bool playSound);
		bool ReceiveRightClick(int x, int y, bool playSound);
		void PerformHover(int x, int y);

		void Draw(SpriteBatch b);
	}

	public enum PageType {
		Blank,
		Calendar,
		Cover
	}
}