using System;
using RectPrisms;

namespace SurfaceArea_CEK
{
    class SurfaceArea
    {
        static void Main(string[] args)
        {
            RectangularPrism prism = new RectangularPrism();
            prism.Input();
            prism.CalculateVolume();
            prism.OutputVolume();
            prism.CalculateSurfaceArea();
            prism.OutputSurfaceArea();
        }
    }

    
    class RectangularPrism : RectPrism
    {
        private int SurfaceArea;

        public void CalculateSurfaceArea()
        {
            SurfaceArea = 2 * (width * length + height * length + height * width);
        }
        
        public void OutputSurfaceArea()
        {
            Console.Out.WriteLine("The surface area of the rectangular prism "+SurfaceArea+" square units.");
        }
    }
}