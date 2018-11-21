using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp
{
    [Serializable]
    public class Shape
    {
        public Shape()
        {
            this.Id = 0;
        }
        public Shape(int id)
        {
            this.Id = id;
        }

        //C# properties
        public int Id { get; }

        public /*UInt32*/ string Color { get; set; }   //RGBA
        public ShapeType Type { get; set; }
        public double CenterX { get; set; }
        public double CenterY { get; set; }
        public double SurfaceArea { get; set; }
        public string Label { get; set; }

        public enum ShapeType
        {
            TRIANGLE,
            SQUARE,
            CIRCLE
        }
    }
}
