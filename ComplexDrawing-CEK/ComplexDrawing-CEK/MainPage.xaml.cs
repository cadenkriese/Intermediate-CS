using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Threading;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ComplexDrawing_CEK
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int hexagonsDrawn = 0;

        public MainPage()
        {
            this.InitializeComponent();

            IconListBox.SelectionChanged += IconListBox_SelectionChanged;
            Canvas1.Draw += Canvas1_Draw;
        }

        private void Canvas1_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            CanvasDrawingSession ds = args.DrawingSession;

            if (!ListBoxItem1.IsSelected)
            {
                //Drawing 1

                //Sun
                ds.FillEllipse(0, 0, 150, 100, Colors.Yellow);
                //Grass
                ds.FillRectangle(new Rect(0, 700, 5000, 200), Colors.Green);

                //Tree
                ds.FillRectangle(new Rect(200, 325, 80, 400), Colors.SaddleBrown);
                ds.FillEllipse(240, 325, 100, 100, Colors.Green);
                ds.FillEllipse(200, 350, 100, 100, Colors.Green);
                ds.FillEllipse(260, 350, 100, 100, Colors.Green);

                //House
                ds.FillRectangle(new Rect(800, 475, 350, 300), Colors.DarkRed);
                ds.DrawLine(new Vector2(800, 475), new Vector2(1150, 775), Colors.White, 10);
                ds.DrawLine(new Vector2(800, 775), new Vector2(1150, 475), Colors.White, 10);

                Vector2[] roof = { new Vector2(800, 475), new Vector2(975, 375), new Vector2(1150, 475) };
                CanvasPathBuilder pathBuilder = new CanvasPathBuilder(Canvas1);
                pathBuilder.AddGeometry(CanvasGeometry.CreatePolygon(Canvas1, roof));
                ds.FillGeometry(CanvasGeometry.CreatePath(pathBuilder), Colors.SaddleBrown);
            }
            else
            {
                Color[] rainbow = {Colors.Red, Colors.Orange, Colors.Yellow, Colors.Green, Colors.Blue, Colors.Indigo, Colors.Violet};

                //Drawing 2
                Vector2 startingPoint = new Vector2((float) (ActualWidth / 2), (float) (ActualHeight / 2));

                int angleIncrement = 3;

                int sideLength = 60;
                int angle = 0;

                hexagonsDrawn++;

                if (hexagonsDrawn == 1000)
                {
                    //Draw hexagons
                    for (int i = 0; i < hexagonsDrawn; i++)
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            startingPoint = DrawLine(ds, startingPoint, angle, sideLength, rainbow[i % rainbow.Length]);
                            angle += 60;
                        }

                        angle += angleIncrement;
                        sideLength++;
                    }

                    hexagonsDrawn = 0;
                }
                else
                {
                    //Draw hexagons
                    for (int i = 0; i < hexagonsDrawn; i++)
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            startingPoint = DrawLine(ds, startingPoint, angle, sideLength, rainbow[i%rainbow.Length]);
                            angle += 60;
                        }

                        angle += angleIncrement;
                        sideLength++;
                    }

                    Task.Delay(10).Wait();
                    Canvas1.Invalidate();
                }
            }
        }

        private void IconListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Canvas1.Invalidate();
        }


        private Vector2 DrawLine(CanvasDrawingSession ds, Vector2 startingPoint, double angle, double length)
        {
            return DrawLine(ds, startingPoint, angle, length, Colors.Black, 0.4f);
        }

        private Vector2 DrawLine(CanvasDrawingSession ds, Vector2 startingPoint, double angle, double length, Color color)
        {
            return DrawLine(ds, startingPoint, angle, length, color, 0.4f);
        }

        private Vector2 DrawLine(CanvasDrawingSession ds, Vector2 startingPoint, double angle, double length, Color color, float strokeWidth)
        {
            //Convert to radians
            angle *= (Math.PI / 180);

            Vector2 endPoint = new Vector2((float)(startingPoint.X + length * Math.Cos(angle)), (float)(startingPoint.Y + length * Math.Sin(angle)));

            ds.DrawLine(startingPoint, endPoint, color, strokeWidth);

            return endPoint;
        }
    }
}
