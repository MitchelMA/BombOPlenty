using BombOPlenty.Content.Projectiles.Explosives;

namespace BombOPlenty.Content.Items.Explosives;

public class BombArrow : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.WoodenArrow);

        Item.ammo = AmmoID.Arrow;
        Item.shoot = ModContent.ProjectileType<BombArrowProjectile>();
        Item.maxStack = Item.CommonMaxStack;

        Item.damage = 3;
        Item.width = 20;
        Item.height = 32;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.WoodenArrow)
            .AddIngredient(ItemID.Bomb)
            .Register();
    }
}