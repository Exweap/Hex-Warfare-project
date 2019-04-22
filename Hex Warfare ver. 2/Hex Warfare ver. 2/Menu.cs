using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;

namespace Hex_Warfare_ver._2
{
    class Menu
    {
        Font font;
        List<Text> menuText = new List<Text>();
        List<RectangleShape> menuButton = new List<RectangleShape>();
        int buttonX, buttonY;

        public Menu(float width, float height)
        {
            font = new Font(@"C:\Users\Łukasz\Documents\Visual Studio 2017\Projects\Hex Warfare ver. 2\Hex Warfare ver. 2\files\OldLondon.ttf");
            buttonX = 180; buttonY = 40;
            for(int i = 0; i < Constants.MENU_SELECTIONS; i++)
            {
                Text tmp1 = new Text();
                RectangleShape tmp2 = new RectangleShape();
                tmp1.Font = font;
                tmp1.Color = Color.Black;
                tmp2.Size = new Vector2f(buttonX, buttonY);
                tmp2.FillColor = Color.White;
                menuText.Add(tmp1);
                menuButton.Add(tmp2);
            }

            float tmpWidth;
            menuText[0].DisplayedString = "PLAY";
            tmpWidth = menuText[0].GetLocalBounds().Width;
            menuText[0].Position = new Vector2f((width / 2) - tmpWidth / 2, height / (Constants.MENU_SELECTIONS + 1) * 1);
            tmpWidth = menuButton[0].GetLocalBounds().Width;
            menuButton[0].Position = new Vector2f((width / 2) - tmpWidth / 2, height / (Constants.MENU_SELECTIONS + 1) * 1);

            menuText[1].DisplayedString = "SETTINGS";
            tmpWidth = menuText[1].GetLocalBounds().Width;
            menuText[1].Position = new Vector2f((width / 2) - tmpWidth / 2, height / (Constants.MENU_SELECTIONS + 1) * 2);
            tmpWidth = menuButton[1].GetLocalBounds().Width;
            menuButton[1].Position = new Vector2f((width / 2) - tmpWidth / 2, height / (Constants.MENU_SELECTIONS + 1) * 2);

            menuText[2].DisplayedString = "EXIT";
            tmpWidth = menuText[2].GetLocalBounds().Width;
            menuText[2].Position = new Vector2f((width / 2) - tmpWidth / 2, height / (Constants.MENU_SELECTIONS + 1) * 3);
            tmpWidth = menuButton[2].GetLocalBounds().Width;
            menuButton[2].Position = new Vector2f((width / 2) - tmpWidth / 2, height / (Constants.MENU_SELECTIONS + 1) * 3);
        }

        public void MenuDraw(RenderWindow window)
        {
            for (int i = 0; i < Constants.MENU_SELECTIONS; i++)
            {
                window.Draw(menuButton[i]);
                window.Draw(menuText[i]);
            }
        }

        public int IsClicked(RenderWindow window, Vector2i menuMouse)
        {
            int mX = menuMouse.X, mY = menuMouse.Y;
            Vector2f b;
            for(int i = 0; i < Constants.MENU_SELECTIONS; i++)
            {
                b = menuButton[i].Position;
                if ((mX >= b.X) && (mX <= b.X + menuButton[i].GetLocalBounds().Width) && (mY >= b.Y) && (mY <= b.Y + menuButton[i].GetLocalBounds().Height))
                {
                    return i + 1;
                }
            }
            return -1;
        }
    }
}
