using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Hex_Warfare_ver._2
{
    static class Constants
    {
        public const int MENU_SELECTIONS = 3;
    }

    class Program
    {
        static void TextureInput(Texture[] textureArray)
        {
            textureArray[0] = new Texture(@"C:\Users\Łukasz\Documents\Visual Studio 2017\Projects\Hex Warfare ver. 2\Hex Warfare ver. 2\files\plains.jpg");
            textureArray[1] = new Texture(@"C:\Users\Łukasz\Documents\Visual Studio 2017\Projects\Hex Warfare ver. 2\Hex Warfare ver. 2\files\forest.jpg");
            textureArray[2] = new Texture(@"C:\Users\Łukasz\Documents\Visual Studio 2017\Projects\Hex Warfare ver. 2\Hex Warfare ver. 2\files\highlands.jpg");
            textureArray[3] = new Texture(@"C:\Users\Łukasz\Documents\Visual Studio 2017\Projects\Hex Warfare ver. 2\Hex Warfare ver. 2\files\water.jpg");
            textureArray[4] = new Texture(@"C:\Users\Łukasz\Documents\Visual Studio 2017\Projects\Hex Warfare ver. 2\Hex Warfare ver. 2\files\mountain.jpg");
        }

        static void Zoom(RenderWindow window, MouseWheelEventArgs arg, ref int currentZoomLvl, ref float zoomResX, ref float zoomResY, View zoomView)
        {
            Vector2i mouse = new Vector2i(Mouse.GetPosition(window).X, Mouse.GetPosition(window).Y);
            Vector2f coords = new Vector2f(window.MapPixelToCoords(new Vector2i(mouse.X, mouse.Y)).X, window.MapPixelToCoords(new Vector2i(mouse.X, mouse.Y)).Y);
            if(arg.Delta > 0 && currentZoomLvl > 0)
            {
                zoomResX *= (float)0.8;
                zoomResY *= (float)0.8;
                currentZoomLvl--;
            }
            else if(arg.Delta < 0 && currentZoomLvl <9)
            {
                zoomResX *= (float)1.25;
                zoomResY *= (float)1.25;
                currentZoomLvl++;
            }
            zoomView.Size = new Vector2f(zoomResX, zoomResY);
            zoomView.Center = new Vector2f(coords.X, coords.Y);
            Vector2f mousePos = coords;
            window.SetView(zoomView);
        }

        static void SetDefaultView(RenderWindow window, int RESX, int RESY, ref int currentZoomLvl, View zoomView, View defaultMapView, ref float zoomResX, ref float zoomResY)
        {
	        zoomView = defaultMapView;
	        zoomResX = RESX;
	        zoomResY = RESY;
            zoomView.Size = new Vector2f(zoomResX, zoomResY);
            zoomView.Center = new Vector2f(RESX / 2 - 20, RESY / 2 - 65);
            currentZoomLvl = 7;
	        window.SetView(zoomView);
        }

        static void Gameplay(RenderWindow window, ref MapManager map, View zoomView, View defaultMapView, int RESX, int RESY, int screenMode)
        {
            //Entity[] entities = new Infantry[2];
            //entities[0] = new Infantry();
            //Vector2f position = new Vector2f(500, 100);
            //entities[0].SetPos(position);
            //entities[1] = new Infantry();
            //Vector2f position1 = new Vector2f(100, 100);
            //Console.WriteLine(entities[0].GetPos());
            //entities[1].SetPos(position1);

            Infantry[] inf = new Infantry[2];
            inf[0] = new Infantry();
            inf[1] = new Infantry(); 
            inf[0].SetPosChuj(new Vector2f(500, 100));
            Console.WriteLine(inf[0].GetPos());
            inf[1].SetPosChuj(new Vector2f(100, 100));

            Console.WriteLine(inf[0].GetPos());
            Console.WriteLine(inf[1].GetPos());

            window.SetView(defaultMapView);
            float zoomResX = RESX, zoomResY = RESY;
            int currentZoomLvl = 7;
            bool isRunning = true;

            window.Closed += (sender, arg) =>
            {
                window.Close();
                Environment.Exit(-1);
            };

            window.MouseWheelMoved += (sender, arg) =>
            {
                if (screenMode == 1)
                {
                    Zoom(window, arg, ref currentZoomLvl, ref zoomResX, ref zoomResY, zoomView);
                }
            };

            window.KeyPressed += (sender, arg) =>
            {
                if(screenMode == 1)
                    switch (arg.Code)
                    {
                        case Keyboard.Key.Up:
                            zoomView.Move(new Vector2f(0,-10));
                            Console.WriteLine("pisanko");
                            break;
                        case Keyboard.Key.Down:
                            zoomView.Move(new Vector2f(0, 10));
                            break;
                        case Keyboard.Key.Left:
                            zoomView.Move(new Vector2f(-10, 0));
                            break;
                        case Keyboard.Key.Right:
                            zoomView.Move(new Vector2f(10, 0));
                            break;
                        case Keyboard.Key.Return:
                            SetDefaultView(window, RESX, RESY, ref currentZoomLvl, zoomView, defaultMapView, ref zoomResX, ref zoomResY);
                            break;
                        case Keyboard.Key.Escape:
                            isRunning = false;
                            SetDefaultView(window, RESX, RESY, ref currentZoomLvl, zoomView, defaultMapView, ref zoomResX, ref zoomResY);
                            break;
                    }
            };


            while (isRunning == true)
            {
                window.Clear();
                map.MapDraw(window);
                window.DispatchEvents();

                inf[0].DrawUnit(window);
                inf[1].DrawUnit(window);

                window.SetView(zoomView);
                window.Display();
            }
            screenMode = 0;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            const int RESX = 1920, RESY = 1080;
            int screenMode=0;

            var window = new RenderWindow(new VideoMode(RESX, RESY), "SFML", Styles.Fullscreen);
            var texture = new Texture(@"C:\Users\Łukasz\Documents\Visual Studio 2017\Projects\Hex Warfare ver. 2\Hex Warfare ver. 2\files\MenuBackground.png");
            var background = new Sprite();
            background.Texture = texture;

            Texture[] textureArray = new Texture[5];
            TextureInput(textureArray);

            var defaultMenuView = new View(new Vector2f(RESX / 2, RESY / 2), new Vector2f(RESX, RESY));
            var defaultMapView = new View(new Vector2f(RESX / 2 - 20, RESY / 2 - 65), new Vector2f(RESX, RESY));
            var zoomView = defaultMapView;

            var menu = new Menu(1920, 1080);
            Vector2i menuMouse;

            window.Closed += (sender, arg) => window.Close();

            window.MouseButtonReleased += (sender, arg) =>
            {
                    if (arg.Button == Mouse.Button.Left && screenMode == 0)
                    {
                        menuMouse = Mouse.GetPosition();
                        switch (menu.IsClicked(window, menuMouse))
                        {
                            case 1:
                            {
                                screenMode = 1;
                                var map = new MapManager(textureArray);
                                Gameplay(window, ref map, zoomView, defaultMapView, RESX, RESY, screenMode);
                                window.SetView(defaultMenuView);
                                screenMode = 0;
                                break;
                            }
                            case 2:
                                window.Close();
                                break;
                            case 3:
                                window.Close();
                                break;
                            default:
                                break;
                        }
                    }
            };

            while (window.IsOpen)
            {
                window.Clear();
                window.SetView(defaultMenuView);
                window.Draw(background);
                menu.MenuDraw(window);
                window.DispatchEvents();

                window.Display();
            }
        }
    }
}
