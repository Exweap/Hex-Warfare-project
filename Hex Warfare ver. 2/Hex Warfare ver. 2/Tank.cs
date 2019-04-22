using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;

namespace Hex_Warfare_ver._2
{
    class Tank : Entity
    {
        public Tank()
        {
            texture = new Texture(@"C:\Users\Łukasz\Documents\Visual Studio 2017\Projects\Hex Warfare ver. 2\Hex Warfare ver. 2\files\tank_texture.png");
            sprite.Texture = texture;
            sprite.Scale = new Vector2f((float)0.19, (float)0.19);
            movementValue = 3;
            MovesLeft = movementValue;
        }
        public new void SetPos(Vector2f position)
        {
            base.SetPos(position);
            sprite.Position = new Vector2f(position.X - 37, position.Y - 23);
            Console.WriteLine("wchodzonko1");
        }
        public float TerrainModifier(int terrain)
        {
            switch (terrain)
            {
                case 0:
                    return 3;
                case 1:
                    return (float)2.5;
                case 2:
                    return 2;
                case 3:
                    return (float)1.5;
                case 4:
                    return 1;
                default:
                    break;
            }
            return 0;
        }
        public new float GetCombatValue(MapManager map)
        {
            int terrain = GetTerrain(map);
            return combatValue * TerrainModifier(terrain) * HP / (float)100.0;
        }
    }
}
