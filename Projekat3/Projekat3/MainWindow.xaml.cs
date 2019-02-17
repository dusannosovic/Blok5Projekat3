using Projekat2;
using Projekat2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekat3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Substations subs;
        Ellipse[,] ellipses = new Ellipse[101,101];
        ScaleTransform st = new ScaleTransform();
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            subs = Deserializer.DeserializeSubs();
            translateSubs();
            translateNodes();
            translateSwitch();
            double xy = 5.2;
           
            int x = -2;
            int y = -2;

            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            for(int i = 0; i < 101; i++)
            {
                for(int j = 0; j < 101; j++)
                {
                    ellipses[i, j] = new Ellipse();
                    ellipses[i, j].Visibility = Visibility.Collapsed;
                    //ellipses[i, j].Visibility = Visibility.Visible;
                    ellipses[i, j].Fill = Brushes.Red;
                    ellipses[i, j].Stroke = Brushes.Red;
                    ellipses[i, j].Width = 4;
                    ellipses[i, j].Height = 4;
                    Canvas.SetTop(ellipses[i, j], y);
                    Canvas.SetLeft(ellipses[i, j], x);
                    x = x + 6;
                    map.Children.Add(ellipses[i,j]);
                }
                x = -2;
                y = y + 6;
            }
            map.RenderTransform = st;

            addDots();
            /*myEllipse.Visibility = Visibility.Visible;
            myEllipse.Fill = Brushes.Red;
            myEllipse.StrokeThickness = 1;
            myEllipse.Stroke = Brushes.Red;
            myEllipse.Width = 8;
            myEllipse.Height = 8;
            myEllipse.ToolTip = "Ovo je tool tip";
            Canvas.SetTop(myEllipse, -3);
            Canvas.SetLeft(myEllipse, -3);
            Canvas.Children.Add(myEllipse);
            */
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void translateSubs()
        {
            int subblen = subs.SubstationEntities.Length;
            double xD = 0;
            double yD = 0;
            double maxY = 10;
            double minY = 25;
            double maxX = 30;
            double minX = 60;
            for (int i = 0; i < subblen; i++)
            {
                UTMtoDec.ToLatLon(subs.SubstationEntities[i].X, subs.SubstationEntities[i].Y, 34, out xD, out yD);
                subs.SubstationEntities[i].X = xD;
                subs.SubstationEntities[i].Y = yD;
                if (xD > maxX)
                {
                    maxX = xD;
                }
                if (xD < minX)
                {
                    minX = xD;
                }
                if(yD > maxY)
                {
                    maxY = yD;
                }
                if(yD < minY)
                {
                    minY = yD;
                }
            }

        }
        private void translateNodes()
        {
            int nodlen = subs.NodesEntities.Length;
            double xD = 0;
            double yD = 0;
            double maxY = 10;
            double minY = 25;
            double maxX = 30;
            double minX = 60;
            for (int i = 0; i < nodlen; i++)
            {
                UTMtoDec.ToLatLon(subs.NodesEntities[i].X, subs.NodesEntities[i].Y, 34, out xD, out yD);
                subs.NodesEntities[i].X = xD;
                subs.NodesEntities[i].Y = yD;

                if (xD > maxX)
                {
                    maxX = xD;
                }
                if (xD < minX)
                {
                    minX = xD;
                }
                if (yD > maxY)
                {
                    maxY = yD;
                }
                if (yD < minY)
                {
                    minY = yD;
                }
            }
        }
        private void translateSwitch()
        {
            int swtchlen = subs.SwitchEntities.Length;
            double xD = 0;
            double yD = 0;
            double maxY = 10;
            double minY = 25;
            double maxX = 30;
            double minX = 60;
            for (int i = 0; i < swtchlen; i++)
            {
                UTMtoDec.ToLatLon(subs.SwitchEntities[i].X, subs.SwitchEntities[i].Y, 34, out xD, out yD);
                subs.SwitchEntities[i].X = xD;
                subs.SwitchEntities[i].Y = yD;

                if (xD > maxX)
                {
                    maxX = xD;
                }
                if (xD < minX)
                {
                    minX = xD;
                }
                if (yD > maxY)
                {
                    maxY = yD;
                }
                if (yD < minY)
                {
                    minY = yD;
                }
            }
        }
        private void translatePoint()
        {
            int linelen = subs.LineEntities.Length;
            int linepointlen = 0;
            double xD = 0;
            double yD = 0;
            for (int i = 0; i < linelen; i++)
            {
                linepointlen = subs.LineEntities[i].Vertices.Length;
                for (int j = 0; j < linepointlen; j++)
                {
                    UTMtoDec.ToLatLon(subs.LineEntities[i].Vertices[j].X, subs.LineEntities[i].Vertices[j].Y, 34, out xD, out yD);
                    subs.LineEntities[i].Vertices[j].X = xD;
                    subs.LineEntities[i].Vertices[j].Y = yD;
                }
            }
        }

        private void addDots()
        {
            int subslen = subs.SubstationEntities.Length;
            int nodelen = subs.NodesEntities.Length;
            int swtchlen = subs.SwitchEntities.Length;
            int len = subslen;
            if (nodelen > subslen)
                len = nodelen;
            if (swtchlen > len)
                len = swtchlen;

            for(int i = 0; i < len; i++)
            {
                if(i < subslen)
                    printEllipse(subs.SubstationEntities[i].X, subs.SubstationEntities[i].Y, subs.SubstationEntities[i].Name, subs.SubstationEntities[i].Id, Brushes.Red);
                if (i<nodelen)
                    printEllipse(subs.NodesEntities[i].X, subs.NodesEntities[i].Y, subs.NodesEntities[i].Name, subs.NodesEntities[i].Id, Brushes.Green);
                if(i< swtchlen)
                    printEllipse(subs.SwitchEntities[i].X, subs.SwitchEntities[i].Y, subs.SwitchEntities[i].Name, subs.SwitchEntities[i].Id, Brushes.Blue);
            }
        }
        private void printEllipse(double x, double y, string name, string id, System.Windows.Media.Brush brush)
        {
            double minY = 19.72;
            double maxY = 19.96;
            double tempY;
            double Y = maxY - minY;
            Y = Y / 100;
            int dotY;
            double minX = 45.189;
            double maxX = 45.33;
            double tempX;
            double X = maxX - minX;
            X = X / 100;
            int dotX;
            bool check = true;
            int counter = 1;
            check = true;
            counter = 0;
            tempY = y - minY;
            dotY = (int)(tempY / Y);
            tempX = x - minX;
            dotX = (int)(tempX / X);
            if (ellipses[dotX, dotY].IsVisible)
            {
                while (check)
                {
                    if (dotX - counter >= 0)
                    {
                        if (ellipses[dotX - counter, dotY].Visibility ==  Visibility.Collapsed)
                        {
                            ellipses[dotX - counter, dotY].Visibility = Visibility.Visible;
                            ellipses[dotX - counter, dotY].ToolTip = String.Format("ID:" + id + "\n" + "Name:" + name);
                            ellipses[dotX - counter, dotY].Fill = brush;
                            ellipses[dotX - counter, dotY].Stroke = brush;
                            check = false;
                            continue;
                        }
                    }
                    if (dotY - counter >= 0)
                    {
                        if (ellipses[dotX, dotY - counter].Visibility == Visibility.Collapsed)
                        {
                            ellipses[dotX, dotY - counter].Visibility = Visibility.Visible;
                            ellipses[dotX, dotY - counter].ToolTip = String.Format("ID:" + id + "\n" + "Name:" + name);
                            ellipses[dotX, dotY - counter].Fill = brush;
                            ellipses[dotX, dotY - counter].Stroke = brush;
                            check = false;
                            continue;
                        }
                    }
                    if (dotX + counter <= 100)
                    {
                        if (ellipses[dotX + counter, dotY].Visibility == Visibility.Collapsed)
                        {
                            ellipses[dotX + counter, dotY].Visibility = Visibility.Visible;
                            ellipses[dotX + counter, dotY].ToolTip = String.Format("ID:" + id + "\n" + "Name:" + name);
                            ellipses[dotX + counter, dotY].Fill = brush;
                            ellipses[dotX + counter, dotY].Stroke = brush;
                            check = false;
                            continue;
                        }
                    }
                    if (dotY + counter <= 100)
                    {
                        if (ellipses[dotX, dotY + counter].Visibility == Visibility.Collapsed)
                        {
                            ellipses[dotX, dotY + counter].Visibility = Visibility.Visible;
                            ellipses[dotX, dotY + counter].ToolTip = String.Format("ID:" + id + "\n" + "Name:" +name);
                            ellipses[dotX, dotY + counter].Fill = brush;
                            ellipses[dotX, dotY + counter].Stroke = brush;
                            check = false;
                            continue;
                        }
                    }
                    counter++;
                }
            }
            else
            {
                ellipses[dotX, dotY].Visibility = Visibility.Visible;
                ellipses[dotX, dotY].ToolTip = String.Format("ID:" +id + "\n" + "Name:" + name);
                ellipses[dotX, dotY].Fill = brush;
                ellipses[dotX, dotY].Stroke = brush;
            }
        }

        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                st.ScaleX *= 2;
                st.ScaleY *= 2;
            }
            else
            {
                st.ScaleX /= 2;
                st.ScaleY /= 2;
            }
        }
    }
}
