﻿namespace BombOPlenty.Content.Items;

public class HammerBomb : ModItem
{
    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = SoundID.Item1;
        Item.shootSpeed = 6f;
        Item.shoot = ModContent.ProjectileType<Projectiles.HammerBombProjectile>();
        Item.consumable = true;
        Item.maxStack = Item.CommonMaxStack;
        Item.width = 22;
        Item.height = 30;
        Item.useAnimation = 1 * UnitHelpers.SecondsToTicks;
        Item.useTime = 1 * UnitHelpers.SecondsToTicks;
        Item.autoReuse = false;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe(5);
        recipe.AddIngredient<TinyHammer>();
        recipe.AddIngredient<Fuse>();
        recipe.AddIngredient(ItemID.ExplosivePowder);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}