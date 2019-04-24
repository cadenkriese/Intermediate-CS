using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SimplePlatformer_CEK
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        bool gameOver = false;
        int restartCountdown = 300;

        GameCharacter mainCharacter;
        Dictionary<Rect, Color> shapes = new Dictionary<Rect, Color>();

        public MainPage()
        {
            this.InitializeComponent();
            Canvas1.CreateResources += Canvas1_CreateResources;
            Canvas1.Draw += Canvas1_Draw;
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
        }

        private void Canvas1_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            CanvasDrawingSession ds = args.DrawingSession;

            if (gameOver)
            {
                ds.DrawText("Game Over, You Win!!!", (float) Canvas1.ActualWidth/2, (float) Canvas1.ActualHeight/2, Colors.Green);
                ds.DrawText("Restarting in "+restartCountdown/60 +"s.", (float) Canvas1.ActualWidth / 2, (float) (Canvas1.ActualHeight / 2)+50, Colors.Lime);

                if (restartCountdown <= 0)
                {
                    //Starting positions
                    mainCharacter.x = 40;
                    mainCharacter.y = Canvas1.ActualHeight - 115;

                    mainCharacter.box = new Rect(mainCharacter.x, mainCharacter.y, 40, 40);
                    gameOver = false;
                    restartCountdown = 600;
                }
                else
                {
                    restartCountdown--;
                }

                Canvas1.Invalidate();
                return;
            }

            if (mainCharacter == null)
                mainCharacter = new GameCharacter(Canvas1);
            if (mainCharacter.y > Canvas1.ActualHeight)
                mainCharacter.y = 0;
            if (mainCharacter.x > Canvas1.ActualWidth)
                mainCharacter.x = 0;
            else if (mainCharacter.x < 0)
                mainCharacter.x = Canvas1.ActualWidth - mainCharacter.box.Width;
            
            //true when the box intersects with a single box.
            mainCharacter.onGround = !shapes.Keys.ToList().TrueForAll(rect => !checkIntersect(mainCharacter.box, rect));

            //Handle character horizontal acceleration
            if (Window.Current.CoreWindow.GetKeyState(VirtualKey.D).HasFlag(CoreVirtualKeyStates.Down))
            {
                if (mainCharacter.hSpeed < 10)
                    mainCharacter.hSpeed++;
            }
            else if (Window.Current.CoreWindow.GetKeyState(VirtualKey.A).HasFlag(CoreVirtualKeyStates.Down))
            {
                if (mainCharacter.hSpeed > -10)
                    mainCharacter.hSpeed--;
            }
            else
            {
                if (mainCharacter.hSpeed > 0)
                    mainCharacter.hSpeed--;
                else if (mainCharacter.hSpeed < 0)
                    mainCharacter.hSpeed++;
            }

            if (!mainCharacter.onGround)
            {
                mainCharacter.box.Y -= mainCharacter.vSpeed;
                
                foreach (Rect rect in shapes.Keys)
                {
                    if (checkIntersect(mainCharacter.box, rect))
                    {
                        mainCharacter.y = rect.Y - mainCharacter.box.Height;
                        mainCharacter.onGround = true;
                    }
                }

                if (!mainCharacter.onGround)
                {
                    mainCharacter.y -= mainCharacter.vSpeed;
                    mainCharacter.vSpeed--;
                } else
                {
                    mainCharacter.vSpeed = -2;
                }
            }
            else
            {
                mainCharacter.box.Y -= mainCharacter.vSpeed;

                foreach (Rect rect in shapes.Keys)
                {
                    if (checkIntersect(mainCharacter.box, rect) && mainCharacter.y-mainCharacter.box.Height > rect.Y)
                        mainCharacter.y = rect.Y - mainCharacter.box.Height;
                }
            }

            //Character Movement adjustments
            mainCharacter.x += mainCharacter.hSpeed;

            //Draw Rectangles
            foreach (KeyValuePair<Rect, Color> item in shapes)
            {
                ds.FillRectangle(item.Key, item.Value);
            }

            mainCharacter.draw(ds);

            //Check win condition
            if (mainCharacter.y < 300 && mainCharacter.x > 1000 && mainCharacter.x < 1140)
                gameOver = true;

            Canvas1.Invalidate();
        }
        
        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            if (args.VirtualKey == VirtualKey.W && mainCharacter.onGround)
            {
                mainCharacter.vSpeed = 15;
                mainCharacter.y --;
            }
        }

        //Returns true if they intersect
        private Boolean checkIntersect(Rect rectOne, Rect rectTwo)
        {
            Rect tempRect = new Rect(rectOne.X, rectOne.Y, rectOne.Width, rectOne.Height);
            tempRect.Intersect(rectTwo);

            return !tempRect.IsEmpty;
        }

        private void Canvas1_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            if (mainCharacter == null)
                mainCharacter = new GameCharacter(Canvas1);

            //Grass
            shapes.Add(new Rect(0, Canvas1.ActualHeight - 75, Canvas1.ActualWidth, 75), Colors.Green);

            for (int i = 0; i < 5; i++)
            {
                shapes.Add(new Rect(100 + (i*150), Canvas1.ActualHeight - (150 + i*80), 140, 10), Colors.Brown);
            }

            shapes.Add(new Rect(1000, 300, 140, 10), Colors.Green);

            args.TrackAsyncAction(mainCharacter.CreateResources(sender).AsAsyncAction());
        }
    }

    public class GameCharacter
    {
        public double x;
        public double y;

        public double vAccel;
        public double hAccel;

        public double vSpeed;
        public double hSpeed;

        public Rect box;

        public Boolean onGround;

        public CanvasBitmap standardPose;
        public CanvasBitmap jumpingPose;

        public GameCharacter(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl canvas)
        {
            //Starting positions
            x = 40;
            y = canvas.ActualHeight - 115;

            box = new Rect(x, y, 40, 40);
        }

        public void draw(CanvasDrawingSession ds)
        {
            box.X = x;
            box.Y = y;

            ds.DrawImage((onGround ? standardPose : jumpingPose), box);
        }

        public async Task CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender)
        {
            standardPose = await CanvasBitmap.LoadAsync(sender, "Assets/character-standard.png");
            jumpingPose = await CanvasBitmap.LoadAsync(sender, "Assets/character-jump.png");
        }
    }
}
