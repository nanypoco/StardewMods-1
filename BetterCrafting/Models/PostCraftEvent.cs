using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Leclair.Stardew.Common.Crafting;

using StardewValley;
using StardewValley.Menus;

namespace Leclair.Stardew.BetterCrafting.Models;

public class PostCraftEvent : IPostCraftEvent {

	public IRecipe Recipe { get; }
	public Farmer Player { get; }
	public Item? Item { get; set; }
	public IClickableMenu Menu { get; }
	public List<Item> ConsumedItems { get; }

	public PostCraftEvent(IRecipe recipe, Farmer player, Item? item, IClickableMenu menu, List<Item> consumedItems) {
		Recipe = recipe;
		Player = player;
		Item = item;
		Menu = menu;
		ConsumedItems = consumedItems;
	}

}
