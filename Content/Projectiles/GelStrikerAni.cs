using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ApocryphaAnimatorBranch.Content.Projectiles
{
    public class GelStrikerAni : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;

            // projectile lasts for 3/4 a second
            Projectile.timeLeft = 45;

            // hit multiple enemies, but only once each
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = int.MaxValue;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Projectile.Center = player.Center;

            float progress = 1f - (Projectile.timeLeft / 45f);
            float eased = MathHelper.SmoothStep(0f, 1f, progress);

            float burstStart = 0.30f;
            float boosted = eased;

            if (progress >= burstStart)
            {
                float burstProgress = (progress - burstStart) / (1f - burstStart);
                float multiplier = 1.30f;
                boosted = MathHelper.Lerp(eased, eased * multiplier, burstProgress);
                boosted = MathHelper.Clamp(boosted, 0f, 1f);
            }

            // you can edit -130 and 110, they are the arcs of the sword
            float start = MathHelper.ToRadians(-130f);
            float end = MathHelper.ToRadians(110f);
            Projectile.rotation = MathHelper.Lerp(start, end, boosted);

            if (player.direction == -1)
                Projectile.rotation += MathHelper.Pi;

            player.itemRotation = Projectile.rotation;
            player.itemTime = 2;
            player.itemAnimation = 2;

            float shrinkStart = 0.75f;
            if (progress > shrinkStart)
            {
                float shrinkProgress = (progress - shrinkStart) / (1f - shrinkStart);
                Projectile.scale = MathHelper.Lerp(1.25f, 0f, shrinkProgress);

                Projectile.position += new Vector2(2f, 2f) * shrinkProgress;
            }
            else
            {
                // overall scale of projectile (for sword)
                Projectile.scale = 1.25f;
            }
        }

        public override void PostDraw(Color lightColor)
        {
            Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;

            Vector2 origin = new Vector2(0, texture.Height);

            Main.spriteBatch.Draw(
                texture,
                Projectile.Center - Main.screenPosition,
                null,
                Color.White,
                Projectile.rotation,
                origin,
                Projectile.scale,
                Projectile.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                0f
            );
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
    }
}
