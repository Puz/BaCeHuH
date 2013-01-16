using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace VisualDraw_Orlov
{
    public partial class MainScreen : Form
    {
        Shape tempShape;
        string curFile = "test.txt";
        List<Shape> Shapes = new List<Shape>();
        bool IsShapeStart = true;
        Point ShapeStart;

        Pen pMain = new Pen(Color.Black);
        Pen pTemp = new Pen(Color.Red);
        Pen pSelection = new Pen(Color.Red, 2);

        public MainScreen()
        {
            InitializeComponent();
        }

        private void MainScreen_MouseMove(object sender, MouseEventArgs e)
        {
            if (rb_Cross.Checked)
            {
                tempShape = (new Cross(e.X, e.Y));
            }
            if (rb_Circle.Checked)
            {
                if (!IsShapeStart)
                {
                    tempShape = new Circle(ShapeStart, e.Location);
                }
            }
            else
            {
                if (!IsShapeStart)
                {
                    tempShape = new Line(ShapeStart, e.Location);
                }
            }
            this.Refresh();
        }

        private void AddShape(Shape shape)
        {
            Shapes.Add(shape);
            ShapesList.Items.Add(shape.DescriptionString);
        }

        private void MainScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (rb_Cross.Checked)
            {
                AddShape(new Cross(e.X, e.Y));
            }

            else if (rb_Circle.Checked)
            {
                  if (IsShapeStart == true)
                {
                    ShapeStart = e.Location; IsShapeStart = false;
                }

                else
                {
                    IsShapeStart = true; AddShape(new Circle(ShapeStart, e.Location));
                }
            }

            else if (IsShapeStart == true)
            {
                ShapeStart = e.Location; IsShapeStart = false;
            }

            else
            {
                IsShapeStart = true; AddShape(new Line(ShapeStart, e.Location));
            }

            this.Refresh();
        }

        private void MainScreen_Paint(object sender, PaintEventArgs e)
        {

            if (tempShape != null)
            {
                tempShape.DrawWith(e.Graphics, pTemp);
            }
            foreach (Shape p in this.Shapes)
            {
                p.DrawWith(e.Graphics, pMain);
            }

            foreach (int i in ShapesList.SelectedIndices)
            {
                Shapes[i].DrawWith(e.Graphics, pSelection);
            }

        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            IsShapeStart = true;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void сохранитькакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
            StreamWriter sw = new StreamWriter(curFile);
            foreach(Shape p in this.Shapes)
                    {
                         p.SaveTo(sw);
                    }
            sw.Close();
            Refresh();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader (curFile);

            Shapes.Clear();

            while(!sr.EndOfStream){
            string type = sr.ReadLine();
                switch(type){
                    case "Cross":
                        AddShape(new Cross(sr));
                    break;
                    case "Line":
                    AddShape(new Line(sr));
                    break;
                    case "Circle":
                    AddShape(new Circle(sr));
                    break;
                }
            }
            sr.Close();
            Refresh();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
         //   this.Refresh();
        }

        private void MainScreen_MouseLeave(object sender, EventArgs e)
        {
            tempShape = null;
            this.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            while (ShapesList.SelectedIndices.Count > 0)
            {
                Shapes.RemoveAt(ShapesList.SelectedIndices[0]);
                ShapesList.Items.RemoveAt(ShapesList.SelectedIndices[0]);
            }
        }
    }

}


 