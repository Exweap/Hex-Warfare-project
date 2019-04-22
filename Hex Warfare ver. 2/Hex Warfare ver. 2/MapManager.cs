using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;

namespace Hex_Warfare_ver._2
{
    class MapManager
    {
        TerritoryTile[,] hex_grid = new TerritoryTile[26, 13];
        
        public MapManager(Texture[] unitTextures)
        {
            for (int i = 0; i < 26; i++)
                for (int j = 0; j < 13; j++)
                    hex_grid[i, j] = new TerritoryTile();
            int evenX = 0;
            int oddX = evenX + 75;
            float evenY = (float)(100 * Math.Sqrt(3) / 2.0);
            float oddY = (float)(evenY / 2.0);
            var randtmp = new Random();
            int random;

            for (int i = 0; i < 26; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < 13; j++)
                    {
                        hex_grid[i,j].Position = new Vector2f(evenX, evenY * j - 25);
                        //std::cout << "j=" << j << " " << "i=" << i << " " << evenX << " " << evenY * j - 25 << std::endl;
                        random = randtmp.Next(0,5);
                        hex_grid[i, j].SetTexture(unitTextures[random]);
                        hex_grid[i, j].Terrain = random;
                    }
                    evenX += 150;
                }
                else
                {
                    for (int j = 0; j < 13; j++)
                    {
                        hex_grid[i,j].Position = new Vector2f(oddX, (evenY * j) - oddY - 25);
                        //std::cout << "j=" << j << " " << "i=" << i << " " << oddX << " " << (evenY*j) - oddY - 25 << std::endl;
                        random = randtmp.Next(0, 4);
                        hex_grid[i,j].SetTexture(unitTextures[random]);
                        hex_grid[i, j].Terrain = random;
                    }
                    oddX += 150;
                }
            }
        }

        public Vector2f GetPosition(int i, int j)
        {
            return hex_grid[i, j].Position;
        }
        public void SetOccupation(int i, int j, int o)
        {
            hex_grid[i, j].Occupation = o;
        }
        public int GetOccupation(int i, int j)
        {
            return hex_grid[i, j].Occupation;
        }
        public void SetOccupantIndex(int i, int j, int index)
        {
            hex_grid[i, j].OccupantIndex = index;
        }
        public int GetOccupantIndex(int i, int j)
        {
            return hex_grid[i, j].OccupantIndex;
        }
        public int GetTerrain(int i, int j)
        {
            return hex_grid[i, j].Terrain;
        }
        public bool IsCliked(Vector2f coords, int i, int j)
        {
            return hex_grid[i, j].IsClicked(coords);
        }
        public void SetTileColor(int i, int j, Color color)
        {
            hex_grid[i, j].SetTileColor(color);
        }
        public Vector3f ToCubeCoords(int i, int j)
        {
            float x = i, z = (float)(j - (i + i % 2) / 2.0);
            return new Vector3f(x, -x - z, z);
        }
        public Vector2i ToOffsetCoords(int x, int y, int z)
        {
            return new Vector2i(x, z + (x + (x & 1)) / 2);
        }
        public void MapDraw(RenderWindow window)
        {
            for (int i = 0; i < 26; i++)
                for (int j = 0; j < 13; j++)
                {
                    hex_grid[i,j].TileDraw(window);
                }
        }
    }
}
