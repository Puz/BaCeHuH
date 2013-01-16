using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;


namespace VisualDraw_Orlov
{
    public abstract class Shape
    {
        public abstract void DrawWith(Graphics g, Pen p);
        public abstract void SaveTo(StreamWriter sw);
        public abstract string DescriptionString { get; }
    }

    public class Cross : Shape
    {
           Point C;
           public Cross(int _X, int _Y)
        {
            C = new Point(_X, _Y);
        }
        public Cross(StreamReader sr)
        {
            string line = sr.ReadLine();
            string[] foo = line.Split(' ');
            C.X = Convert.ToInt32(foo[0]);
            C.Y = Convert.ToInt32(foo[1]);
        }

        public override void DrawWith(Graphics g, Pen p)
        {
            g.DrawLine(p, C.X - 4, C.Y - 4, C.X + 4, C.Y + 4);
            g.DrawLine(p, C.X + 4, C.Y - 4, C.X - 4, C.Y + 4);
        }
        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("Cross");
            sw.WriteLine(Convert.ToString(C.X) + " " + Convert.ToString(C.Y));
        }
        public override string DescriptionString
        {
            get { return ("Cross(" + Convert.ToString(C.X) + ";" + Convert.ToString(C.Y) + ")"); }
        }

    }
    public class Circle : Shape
    {
        Point C, A;
        Pen p = new Pen(Color.Blue);
        public Circle(Point _C, Point _A)
        {
            C = _C;
            A = _A;
        }

        public Circle(StreamReader sr)
        {
            string line = sr.ReadLine();
            string[] foo = line.Split(' ');
            C.X = Convert.ToInt32(foo[0]);
            C.Y = Convert.ToInt32(foo[1]);
        }

        public override void DrawWith(Graphics g, Pen p)
        {
            float r = (float)Math.Sqrt(Math.Pow(C.X - A.X, 2) + Math.Pow(C.Y - A.Y, 2));
            g.DrawEllipse(p, C.X - r, C.Y - r, 2 * r, 2 * r);
        }

        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("Circle");
            sw.WriteLine(Convert.ToString(C.X) + " " + Convert.ToString(C.Y));
        }
        public override string DescriptionString
        {
            get { return ("Circle(" + Convert.ToString(C.X) + ";" + Convert.ToString(C.Y) + ")"); }
        }
    }

    //
    public class Line : Shape
    {
        Point S, F;
        public Line(Point _S, Point _F)
        {
            S = _S; F = _F;
        }

        public Line(StreamReader sr)
        {
            string line = sr.ReadLine();
            string[] foo = line.Split(' ');
            S.X = Convert.ToInt32(foo[0]);
            S.Y = Convert.ToInt32(foo[1]);
            line = sr.ReadLine();
            foo = line.Split(' ');
            F.X = Convert.ToInt32(foo[0]);
            F.Y = Convert.ToInt32(foo[1]);
        }

        public override void DrawWith(Graphics g, Pen p)
        {
            g.DrawLine(p, S, F);
        }
         public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("Line");
            sw.WriteLine(Convert.ToString(S.X) + " " + Convert.ToString(S.Y));
            sw.WriteLine(Convert.ToString(F.X) + " " + Convert.ToString(F.Y));
        }
         public override string DescriptionString
         {
             get { return ("Line(" + Convert.ToString(S.X) + ";" + Convert.ToString(S.Y) + ")-(" + Convert.ToString(F.X) + ";" + Convert.ToString(F.Y)) + ")"; }
         }
    }
}
