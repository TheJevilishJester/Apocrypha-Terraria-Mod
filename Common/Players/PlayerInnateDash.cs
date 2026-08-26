using System.Numerics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TheApocryphaMod;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace TheApocryphaMod.Common.Players;

public class PlayerInnateDash : ModPlayer
{
    // Do not change dashRight/Left, these determine the direction of the dash
    private const int dashRight = 2;
    private const int dashLeft = 3;
    // Interval in frames between two dashes can be used
    private const int dashCooldown = 50;
    // Duration of the dash itself
    private const int dashDuration = 7;
    // Starting velocity of the dash
    private const float dashVelocity = 5F;

    private int dashDelay;

    private int dashTimer;

    private int dashDir = -1;
    // Very early in the frame, checks if a dash is to be inputted and sets the direction of the dash
    public override void ResetEffects()
    {
        if (Player.controlRight && Player.releaseRight && Player.doubleTapCardinalTimer[dashRight] < 15)
        {
            dashDir = dashRight;
        } else if (Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[dashLeft] < 15)
        {
            dashDir = dashLeft;
        }
        else
        {
            dashDir = -1;
        }
        
    }

    public override void PreUpdateMovement()
    {
        if (canUseDash() && dashDir != -1 && dashDelay == 0)
        {
            Vector2 newVelocity = Player.velocity;

            switch (dashDir)
            {
                // Only apply dash when current speed is less that the dashVelocity in the chosen direction.
                case dashLeft when Player.velocity.X > -dashVelocity:
                case dashRight when Player.velocity.X < dashVelocity:
                {


                    float dashDirection = dashDir == dashRight ? 1 : -1;
                    newVelocity.X = dashDirection * dashVelocity;
                    break;
                }
                default:
                    return; // Not moving fast enough so don't start the dash
            }

            dashDelay = dashCooldown;
            dashTimer = dashDuration;
            Player.velocity = newVelocity;
        }

        if (dashDelay > 0)
        {
            dashDelay--;
        }

        bool canUseDash()
        {
            return Player.dashType == DashID.None // Player doesn't have a dash accessory equipped already
                   && !Player.setSolar // Player doesn't have the Solar Armour equipped (Solar Armour gives a dash as its set bonus)
                   && !Player.mount.Active; // Dashing on mounts looks strange
        }
            
        
    }

}