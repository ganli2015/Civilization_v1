using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading;

namespace GameEntity
{
    public class GameDisplayImp2:GameDisplay
    {
        Grid _grid;
        Dispatcher _dis;

        public GameDisplayImp2(ref Grid grid)
        {
            _grid = grid;
            _dis = grid.Dispatcher;
        }

        public void DrawCircle(System.Drawing.Color color, int x, int y, float diameter)
        {
            if (!CommonFunctions.IsPointValid(new System.Drawing.Point(x, y))) return;
            Ellipse circle = new Ellipse();

            RadialGradientBrush rgb = new RadialGradientBrush();
            rgb.Center = new Point(0.35, 0.15);
            rgb.RadiusX = 0.5;
            rgb.RadiusY = 0.5;
            rgb.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("White"), 0));
            rgb.GradientStops.Add(new GradientStop((Color.FromArgb(color.A,color.R,color.G,color.B)), 1));
            circle.Fill = rgb;

            circle.Margin = new Thickness(0);
            circle.SetValue(Grid.ColumnProperty, x);
            circle.SetValue(Grid.RowProperty, y);
            _grid.Children.Add(circle);
        }

        public void DrawSquare(System.Drawing.Color color, int x, int y, float sideLength)
        {
            if (!CommonFunctions.IsPointValid(new System.Drawing.Point(x, y))) return;

            Rectangle rec = new Rectangle();
            rec.Fill = new SolidColorBrush((Color.FromArgb(color.A,color.R,color.G,color.B)));

            rec.Margin = new Thickness(0);
            rec.SetValue(Grid.ColumnProperty, x);
            rec.SetValue(Grid.RowProperty, y);
            _grid.Children.Add(rec);
        }

        public void ResetGrid(int x, int y)
        {
            if (!CommonFunctions.IsPointValid(new System.Drawing.Point(x, y))) return;

            System.Drawing.Color pictureboxColor=GlobalParameter.PictureboxColor;

            Rectangle rec = new Rectangle();
            rec.Fill =  new SolidColorBrush((Color.FromRgb(pictureboxColor.R,pictureboxColor.G,pictureboxColor.B)));
            rec.SetValue(Grid.ColumnProperty, x);
            rec.SetValue(Grid.RowProperty, y);
            _grid.Children.Add(rec);
        }
    }
}
