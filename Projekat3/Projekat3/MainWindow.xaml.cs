using Projekat2;
using Projekat2.Models;
using Projekat3.Model;
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
        Dictionary<string, Tuple<int, int>> idPosition = new Dictionary<string, Tuple<int, int>>();
        int[,] visitedmap;
        List<LineMapEntity> allLines;
        struct direction
        {
            public int x;
            public int y;
        };
        static direction[] directions = {
                                       new direction(){x = 1, y = 0},
                                       new direction(){x = 0, y = 1},
                                       new direction(){x = -1, y = 0},
                                       new direction(){x = 0, y = -1}
        };

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            allLines = new List<LineMapEntity>();
            visitedmap = resetVisitedMap();
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
            map.LayoutTransform = st;

            addDots();
            AddLines();
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
            for (int i = 0; i < subblen; i++)
            {
                UTMtoDec.ToLatLon(subs.SubstationEntities[i].X, subs.SubstationEntities[i].Y, 34, out xD, out yD);
                subs.SubstationEntities[i].X = xD;
                subs.SubstationEntities[i].Y = yD;
                
            }

        }
        private void translateNodes()
        {
            int nodlen = subs.NodesEntities.Length;
            double xD = 0;
            double yD = 0;
            for (int i = 0; i < nodlen; i++)
            {
                UTMtoDec.ToLatLon(subs.NodesEntities[i].X, subs.NodesEntities[i].Y, 34, out xD, out yD);
                subs.NodesEntities[i].X = xD;
                subs.NodesEntities[i].Y = yD;
            }
        }
        private void translateSwitch()
        {
            int swtchlen = subs.SwitchEntities.Length;
            double xD = 0;
            double yD = 0;
            for (int i = 0; i < swtchlen; i++)
            {
                UTMtoDec.ToLatLon(subs.SwitchEntities[i].X, subs.SwitchEntities[i].Y, 34, out xD, out yD);
                subs.SwitchEntities[i].X = xD;
                subs.SwitchEntities[i].Y = yD;
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
            tempX = maxX - x;// - minX;
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
                            ellipses[dotX - counter, dotY].ToolTip = String.Format("ID:_" + id + "_\n" + "Name:" + name);
                            ellipses[dotX - counter, dotY].Fill = brush;
                            ellipses[dotX - counter, dotY].Stroke = brush;
                            idPosition.Add(id, new Tuple<int, int>(dotX - counter, dotY));
                            check = false;
                            continue;
                        }
                    }
                    if (dotY - counter >= 0)
                    {
                        if (ellipses[dotX, dotY - counter].Visibility == Visibility.Collapsed)
                        {
                            ellipses[dotX, dotY - counter].Visibility = Visibility.Visible;
                            ellipses[dotX, dotY - counter].ToolTip = String.Format("ID:_" + id + "_\n" + "Name:" + name);
                            ellipses[dotX, dotY - counter].Fill = brush;
                            ellipses[dotX, dotY - counter].Stroke = brush;
                            idPosition.Add(id, new Tuple<int, int>(dotX, dotY - counter));
                            check = false;
                            continue;
                        }
                    }
                    if (dotX + counter <= 100)
                    {
                        if (ellipses[dotX + counter, dotY].Visibility == Visibility.Collapsed)
                        {
                            ellipses[dotX + counter, dotY].Visibility = Visibility.Visible;
                            ellipses[dotX + counter, dotY].ToolTip = String.Format("ID:_" + id + "_\n" + "Name:" + name);
                            ellipses[dotX + counter, dotY].Fill = brush;
                            ellipses[dotX + counter, dotY].Stroke = brush;
                            idPosition.Add(id, new Tuple<int, int>(dotX + counter, dotY));
                            check = false;
                            continue;
                        }
                    }
                    if (dotY + counter <= 100)
                    {
                        if (ellipses[dotX, dotY + counter].Visibility == Visibility.Collapsed)
                        {
                            ellipses[dotX, dotY + counter].Visibility = Visibility.Visible;
                            ellipses[dotX, dotY + counter].ToolTip = String.Format("ID:_" + id + "_\n" + "Name:" +name);
                            ellipses[dotX, dotY + counter].Fill = brush;
                            ellipses[dotX, dotY + counter].Stroke = brush;
                            idPosition.Add(id, new Tuple<int, int>(dotX, dotY + counter));
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
                ellipses[dotX, dotY].ToolTip = String.Format("ID:_" +id + "_\n" + "Name:" + name);
                ellipses[dotX, dotY].Fill = brush;
                ellipses[dotX, dotY].Stroke = brush;
                idPosition.Add(id, new Tuple<int, int>(dotX, dotY));
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

        private void AddLines()
        {
            foreach(LineEntity line in subs.LineEntities)
            {
                if (!idPosition.ContainsKey(line.FirstEnd) || !idPosition.ContainsKey(line.SecondEnd))
                {
                    continue;
                }

                Position startPosition = new Position();
                startPosition.row = idPosition[line.FirstEnd].Item1;
                startPosition.col = idPosition[line.FirstEnd].Item2;
                startPosition.parent = null;

                string endPosition = line.SecondEnd;
                startPosition = BFS(startPosition, ellipses, endPosition);

                while (startPosition != null)
                {

                    if (startPosition.parent == null)
                    {
                        break;
                    }

                    int y1 = startPosition.row * 6;
                    int x1 = startPosition.col * 6;
                    int y2 = startPosition.parent.row * 6;
                    int x2 = startPosition.parent.col *6;

                    bool lineExists = false;

                    foreach (LineMapEntity myLine in allLines)
                    {
                        if (myLine.X1 == x1 && myLine.Y1 == y1 && myLine.X2 == x2 && myLine.Y2 == y2)
                        {
                            lineExists = true;
                            break;
                        }
                    }
                    foreach(LineMapEntity myLine in allLines)
                    {

                    }

                    if (!lineExists)
                    {
                        Line l = new Line();
                        l.Stroke = Brushes.Orange;
                        l.X1 = x1;
                        l.Y1 = y1;

                        l.X2 = x2;
                        l.Y2 = y2;
                        l.StrokeThickness = 1;

                        allLines.Add(new LineMapEntity(l.X1, l.Y1, l.X2, l.Y2));
                        map.Children.Add(l);

                    }
                    startPosition = startPosition.parent;
                }
            }
        }
        private Position BFS(Position present, Ellipse[,] map, string who)
        {
            Position[] poses = new Position[] { present };
            bool found = false;
            while (!found)
            {
                foreach(Position pos in poses)
                {
                       /* string[] id = ellipses[pos.row, pos.col].ToolTip.ToString().Split('_');
                        if (id[1].Equals(who))
                        {
                            visitedmap = resetVisitedMap();
                            return pos;
                        }*/
                    if(idPosition[who].Item1 == pos.row && idPosition[who].Item2 == pos.col)
                    {
                        visitedmap = resetVisitedMap();
                        return pos;
                    }
                        
                }
                poses = cellsNear(poses);
            }
            return present;
        }

        private int[,] resetVisitedMap()
        {
            int[,] returnArray = new int[101, 101];

            for (int i = 0; i < 101; i++)
            {
                for (int j = 0; j < 101; j++)
                {
                    returnArray[i, j] = 0;
                }
            }

            return returnArray;
        }
        private bool visited(int row, int col)
        {
            return visitedmap[row, col] == 1;
        }
        private Position[] cellsNear(Position pos)
        {
            List<Position> result = new List<Position>(); // lista putanje
            foreach (direction dir in directions)   //kroz sva 4 pavca
            {
                int rowCalculated = pos.row + dir.x;
                int colCalculated = pos.col + dir.y;
                if (rowCalculated >= 0 && colCalculated >= 0 && rowCalculated < ellipses.GetLength(0) && colCalculated < ellipses.GetLength(1) && !visited(rowCalculated, colCalculated))
                {
                    visitedmap[rowCalculated, colCalculated] = 1;
                    Position posPath = new Position();
                    posPath.col = colCalculated;
                    posPath.row = rowCalculated;
                    posPath.parent = pos;
                    result.Add(posPath);
                }
            }
            return result.ToArray();    //gore, levo, dole, desno
        }
        private Position[] cellsNear(Position[] poses)
        {
            List<Position> result = new List<Position>();
            foreach (Position pos in poses)
            {
                result.AddRange(cellsNear(pos));
            }
            return result.ToArray();
        }
    }
}
