using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TheApocryphaMod;
namespace TheApocryphaMod.Common.Players
{
    public class PlayerSpeed : ModPlayer
    {
        private static float baseMovespeed = 1.2F;
        private static float bootMoveSpeedBuff = 1.2F;
        private static float fastBootMoveSpeedBuff = 1.35F;
        
        public void IncreaseBaseSpeed(float baseMoveSpeed) {
            Player.moveSpeed *= baseMoveSpeed;
        }

        public void IncreaseRunSpeed(float runSpeedBuff)
        {
            Player.accRunSpeed += runSpeedBuff;
        }
            
            // A flat bonus applied early on in the movement process
            public override void PostUpdateMiscEffects()
            {
                IncreaseBaseSpeed(baseMovespeed);
            }
            
            
            // A flat bonus applied late in the movement process
            public override void PostUpdateRunSpeeds()
            {
                // The faster boots have a runspeed of 6.75F, this If/Else checks which kind of boot you are wearing and applies accordingly
                if (Player.accRunSpeed<=6.1F && Player.accRunSpeed>5.9F)
                {
                    IncreaseRunSpeed(fastBootMoveSpeedBuff);
                }
                else
                {
                    if (Player.accRunSpeed<=6F && Player.accRunSpeed>5.9F)
                    {
                        IncreaseRunSpeed(bootMoveSpeedBuff);
                    }
                }
            }

        
        
    }
}