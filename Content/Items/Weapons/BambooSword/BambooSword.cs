using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheApocryphaMod.Content.Projectiles;

namespace TheApocryphaMod.Content.Items.Weapons.BambooSword
{
    public class BambooSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 40;
            Item.scale = 1.35f;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4;
            Item.value = Item.buyPrice(silver: 1);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            //shoot projectile (for animation)
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<BambooSwordAni>();
            Item.shootSpeed = 0f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
