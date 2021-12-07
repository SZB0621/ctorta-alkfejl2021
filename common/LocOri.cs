using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    /// <summary>
    /// Struct describing a point for easier position description
    /// </summary>
    public struct Point
    {
        /// <summary>
        /// X coordinate
        /// </summary>
        public double X;
        /// <summary>
        /// Ycoordinate
        /// </summary>
        public double Y;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(double x=0.0, double y=0.0) : this()
        {
            X = x;
            Y = y;
        }
        /// <summary>
        /// + operator
        /// </summary>
        /// <param name="a">Point A</param>
        /// <param name="b">Point B</param>
        /// <returns></returns>
        public static Point operator +(Point a, Point b)
        {
            return new Point() { X = a.X + b.X, Y = a.Y + b.Y };
        }
        
        /// <summary>
        /// Calculates eucledian distance between two points
        /// </summary>
        /// <param name="a">Point A</param>
        /// <param name="b">Point B</param>
        /// <returns>Distance</returns>
        public double CalculateDistance(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
    }

    /// <summary>
    /// Struct describing a robot's position as Point and orientation as Double
    /// </summary>
    public struct LocOri
    {
        /// <summary>
        /// Position values (X,Y)
        /// </summary>
        public Point Location;
        /// <summary>
        /// Orientation value (rad)
        /// </summary>
        public double Orientation;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="orientation"></param>
        public LocOri(double x, double y, double orientation) : this()
        {
            Location.X = x;
            Location.Y = y;
            Orientation = (orientation);
        }
    }
}
