using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;

namespace Hex_Warfare_ver._2
{
    abstract class Entity
    {
        protected static RectangleShape healthBar = new RectangleShape();
        protected static CircleShape counter = new CircleShape();
        protected static Texture texture;
        protected static Sprite sprite = new Sprite();
        protected static bool isClicked;
        protected static Vector2f pos;
        protected static int team;
        protected static int movementValue;
        protected int combatValue;

        public Entity()
        {
            HP = 100;
            counter.Radius = 37;
            counter.SetPointCount(300);
            counter.Rotation = 45;
            counter.Origin = new Vector2f(37,37);
            healthBar.Size = new Vector2f(74, 10);
            healthBar.Origin = new Vector2f(37, 5);
            healthBar.FillColor = Color.Green;
            isClicked = false;
        }
        public void DrawUnit(RenderWindow window)
        {
            window.Draw(counter);
            window.Draw(sprite);
            if (HP < 100)
            {
                healthBar.Size = new Vector2f((74 * HP) / (float)100.0, 10);
                window.Draw(healthBar);
            }
        }
        public void SetPos(Vector2f position)
        {
            counter.Position = new Vector2f(position.X, position.Y);
            healthBar.Position = new Vector2f(position.X, position.Y - 37);
            pos = new Vector2f(position.X, position.Y);

        }
        public Vector2f GetPos()
        {
            return pos;
        }
        public static Vector2i MapPos {get; set;}
        public static Vector2f Origin { get; }
        public static int MovesLeft { get; set; }
        public static bool IsClicked()
        {
            return isClicked;
        }
        public static void Clicked()
        {
            isClicked = true;
            counter.OutlineColor = Color.Cyan;
            counter.OutlineThickness = 2;
        }
        public static void Unclicked()
        {
            isClicked = false;
            counter.OutlineColor = Color.Transparent;
        }
        public static int GetRadius()
        {
            return (int)counter.Radius;
        }
        public static int GetTerrain(MapManager map)
        {
            return map.GetTerrain(MapPos.X, MapPos.Y);
        }

        public static int Team
        {
            get
            {
                return team;
            }
            set
            {
                team = value;
                if (value == 0)
                    counter.FillColor = Color.Red;
                if (value == 1)
                    counter.FillColor = Color.Green;
            }
        }
        public static int HP { get; set; }
        virtual public float GetCombatValue(MapManager map)
        {
            return combatValue;
        }
        public static void TakingDamage(int damage)
        {
            HP -= damage;
            if (HP <= 50)
                healthBar.FillColor = Color.Yellow;
            if (HP <= 20)
                healthBar.FillColor = Color.Red;
        }
    }
}
