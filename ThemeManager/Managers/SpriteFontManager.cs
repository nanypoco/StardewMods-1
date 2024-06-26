using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Leclair.Stardew.Common.Events;

using Microsoft.Xna.Framework.Graphics;

using StardewModdingAPI.Events;

using StardewValley;

namespace Leclair.Stardew.ThemeManager.Managers;

public class SpriteFontManager : BaseManager {

	public const string SMALL_FONT_ASSET = "Mods/leclair.thememanager/Game/DefaultFont/Small";
	public const string DIALOGUE_FONT_ASSET = "Mods/leclair.thememanager/Game/DefaultFont/Dialogue";
	public const string TINY_FONT_ASSET = "Mods/leclair.thememanager/Game/DefaultFont/Tiny";

	#region Static Default Font Storage

	internal static SpriteFont DefaultSmallFont = Game1.smallFont;
	internal static SpriteFont DefaultDialogueFont = Game1.dialogueFont;
	internal static SpriteFont DefaultTinyFont = Game1.tinyFont;

	private static bool EverUpdated = false;

	#endregion

	#region Managed Fonts

	private IManagedAsset<SpriteFont>? ManagedSmall;
	private IManagedAsset<SpriteFont>? ManagedDialogue;
	private IManagedAsset<SpriteFont>? ManagedTiny;

	#endregion

	public SpriteFontManager(ModEntry mod) : base(mod) {



	}

	#region Default Font Updates

	public void MaybeUpdateDefaultFonts() {
		if (!EverUpdated)
			UpdateDefaultFonts();
	}

	public void UpdateDefaultFonts() {
		EverUpdated = true;

		if (DefaultSmallFont != Game1.smallFont) {
			DefaultSmallFont = Game1.smallFont;
			Mod.Helper.GameContent.InvalidateCache(SMALL_FONT_ASSET);
		}

		if (DefaultDialogueFont != Game1.dialogueFont) {
			DefaultDialogueFont = Game1.dialogueFont;
			Mod.Helper.GameContent.InvalidateCache(DIALOGUE_FONT_ASSET);
		}

		if (DefaultTinyFont != Game1.tinyFont) {
			DefaultTinyFont = Game1.tinyFont;
			Mod.Helper.GameContent.InvalidateCache(TINY_FONT_ASSET);
		}
	}

	#endregion

	#region Override Default Fonts

	private bool HandleManagedFonts(ref IManagedAsset<SpriteFont>? existing, IManagedAsset<SpriteFont>? updated) {
		if (existing == updated)
			return false;

		if (existing is not null)
			existing.MarkedStale -= OnManagedMarkedStale;

		if (updated is not null)
			updated.MarkedStale += OnManagedMarkedStale;

		existing = updated;
		return true;
	}

	public void AssignFonts(IGameTheme? theme) {
		MaybeUpdateDefaultFonts();

		bool changed = HandleManagedFonts(ref ManagedSmall, theme?.GetManagedFontVariable("Small"));
		changed = HandleManagedFonts(ref ManagedDialogue, theme?.GetManagedFontVariable("Dialogue")) || changed;
		changed = HandleManagedFonts(ref ManagedTiny, theme?.GetManagedFontVariable("Tiny")) || changed;

		if (changed)
			OnManagedMarkedStale();
	}

	#endregion

	#region Events

	private void OnManagedMarkedStale() {
		Game1.smallFont = ManagedSmall?.Value ?? DefaultSmallFont;
		Game1.dialogueFont = ManagedDialogue?.Value ?? DefaultDialogueFont;
		Game1.tinyFont = ManagedTiny?.Value ?? DefaultTinyFont;
	}

	[Subscriber]
	private void OnAssetRequested(object? sender, AssetRequestedEventArgs e) {
		if (e.Name.IsEquivalentTo(SMALL_FONT_ASSET))
			e.LoadFrom(() => DefaultSmallFont, priority: AssetLoadPriority.Low);
		if (e.Name.IsEquivalentTo(DIALOGUE_FONT_ASSET))
			e.LoadFrom(() => DefaultDialogueFont, priority: AssetLoadPriority.Low);
		if (e.Name.IsEquivalentTo(TINY_FONT_ASSET))
			e.LoadFrom(() => DefaultTinyFont, priority: AssetLoadPriority.Low);
	}

	#endregion

}
