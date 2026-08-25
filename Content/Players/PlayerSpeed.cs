using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace TheApocryphaMod.Content.Players
{
    public class PlayerSpeed : ModPlayer
    {
        private static float baseMovespeed = 0.2F;

        public void IncreaseBaseSpeed(float baseMoveSpeed)
        {
            Player.moveSpeed += baseMoveSpeed;
        }
    }
}