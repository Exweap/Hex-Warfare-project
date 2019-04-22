using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;

namespace Hex_Warfare_ver._2
{
    class TerritoryTile
    {
        CircleShape Tile = new CircleShape();
        Vector2f Pos;

        public TerritoryTile()
        {
            Tile.Radius = 50;
            Tile.SetPointCount(6);
            Tile.Rotation = 30;
            Tile.OutlineColor = Color.Black;
            Tile.OutlineThickness = 2;
            Tile.Origin = new Vector2f(50, 50);
            Occupation = 0;
            OccupantIndex = -1;
        }

        public void TileDraw(RenderWindow window)
        {
            window.Draw(Tile);
        }
        public void SetTexture(Texture texture)
        {
            Tile.Texture = new Texture(texture);
        }
        public void SetOrigin(float Xaxis, float Yaxis)
        {
            Tile.Origin = new Vector2f(Xaxis, Yaxis);
        }
        public Vector2f Position
        {
            get
            {
                return Pos;
            }
            set
            {
                Pos = value;
                Tile.Position = new Vector2f(value.X, value.Y);
            }
        }
        public int Occupation
        {
            get; set;
        }
        public int OccupantIndex
        {
            get; set;
        }
        public int Terrain { get; set; }
        public void SetTileColor(Color color)
        {
            if(color == Color.Green)
            {
                Tile.FillColor = Color.Green;
                return;
            }
            if (color == Color.Red)
            {
                Tile.FillColor = Color.Red;
                return;
            }
            if (color == Color.Blue)
            {
                Tile.FillColor = Color.Blue;
                return;
            }
            if (color == Color.Transparent)
            {
                Tile.FillColor = new Color(255,255,255);
                return;
            }
        }
        public bool IsClicked(Vector2f coords)
        {
            int r = 50;
            Vector2f[] points = new Vector2f[6];
            points[0].X = Position.X - 26;
            points[0].Y = Position.Y - (r * (float)Math.Sqrt(3) / 2);
            points[1].X = points[0].X + 50;
            points[1].Y = points[0].Y;
            points[2].X = points[0].X + 75;
            points[2].Y = Position.Y;
            points[3].X = points[0].X + 50;
            points[3].Y = Position.Y + (r * (float)Math.Sqrt(3) / 2);
            points[4].X = points[0].X;
            points[4].Y = points[3].Y;
            points[5].X = points[0].X - 25;
            points[5].Y = Position.Y;

            float[] lengths = new float[6];

            for(int i=0; i<5; i++)
            {
                lengths[i] = (float)(Math.Abs((points[i].Y - points[i + 1].Y) * coords.X + (points[i + 1].X - points[i].X) * coords.Y + (points[i + 1].Y - points[i].Y) * points[i].X - (points[i + 1].X - points[i].X) * points[i].Y) / Math.Sqrt(Math.Pow(points[i].Y - points[i + 1].X, 2.0) + Math.Pow(points[i + 1].X - points[i].X, 2.0)));
            }
            lengths[5] = (float)(Math.Abs((points[5].Y - points[0].Y) * coords.X + (points[0].X - points[5].X) * coords.Y + (points[0].Y - points[5].Y) * points[5].X - (points[0].X - points[5].X) * points[5].Y) / Math.Sqrt(Math.Pow(points[5].Y - points[0].X, 2.0) + Math.Pow(points[0].X - points[5].X, 2.0)));

            for(int i=0; i<3; i++)
            {
                if ((lengths[i] + lengths[i + 3] > (r * Math.Sqrt(3)) + 1) || (lengths[i] + lengths[i + 3] < (r * Math.Sqrt(3)) - 1))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
