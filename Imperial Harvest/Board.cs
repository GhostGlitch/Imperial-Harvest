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

namespace Imperial_Harvest
{
    class Board
    {
        private Random rng = new Random();

        private int rot;
        public List<Image> Random()
        {
            Queue<string> longs = new Queue<string>();
            Queue<string> sqr = new Queue<string>();
            Queue<string> wtr = new Queue<string>();
            List<Image> tiles = new List<Image>();
            longs = TileQ(TileL("long_", true, 8));
            sqr = TileQ(TileL("", true, 4));
            wtr = TileQ(TileL("water_", false, 4));
            tiles.Add(PrepTile("end_raid", 0, 0, 0, 2, 2));
            tiles.Add(PrepTile("end_imp", 0, 8, 90, 2, 2));
            tiles.Add(PrepTile("start_imp", 8, 0, 270, 2, 2));
            tiles.Add(PrepTile("start_raid", 8, 8, 180, 2, 2));
            tiles.AddRange(PrepTiles(longs, 2, 5, 0, 8, 2, 180, 90, 3, 2));
            tiles.AddRange(PrepTiles(longs, 0, 8, 2, 5, 2, 180, 0, 2, 3));
            tiles.AddRange(PrepTiles(sqr, 3, 5, 3, 5, 4, 90, 0, 2, 2));
            int c = tiles.Count;

            Queue<int> wrotQ = new Queue<int>(new[] { 0, 270, 90, 180 });
            for (int y = 2; y <= 5; y += 3)
            {
                for (int x = 2; x <= 5; x += 3)
                {
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Tile_" + wtr.Dequeue() + ".png"));
                    img.LayoutTransform = new RotateTransform(wrotQ.Dequeue());
                    tiles.Add(img);
                    Grid.SetRow(tiles[c], x);
                    Grid.SetColumn(tiles[c], y);
                    Grid.SetRowSpan(tiles[c], 3);
                    Grid.SetColumnSpan(tiles[c], 3);
                    c++;
                }
            }
               return tiles;
        }
        private List<Image> PrepTiles(Queue<string> Q, int x1, int x2, int y1, int y2, int rotPos, int rotStep, int rotMod, int Cspan, int Rspan)
        {
            List<Image> tiles = new List<Image>();
            int c1 = 0;
            for (int x = x1; x <= x2; x += (x2 - x1))
            {
                for (int y = y1; y <= y2; y += (y2 - y1))
                {
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Tile_" + Q.Dequeue() + ".png"));
                    rot = (rng.Next(0, rotPos) * rotStep) + rotMod;
                    img.LayoutTransform = new RotateTransform(rot);
                    tiles.Add(img);
                    Grid.SetColumn(tiles[c1], x);
                    Grid.SetRow(tiles[c1], y);
                    Grid.SetColumnSpan(tiles[c1], Cspan);
                    Grid.SetRowSpan(tiles[c1], Rspan);
                    c1++;
                }
            }
            return tiles;
        }
        private Image PrepTile(string S, int x, int y, int rot, int Cspan, int Rspan)
        {
            List<Image> tiles = new List<Image>();
            Image img = new Image();
            img.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Tile_" + S + ".png"));
            img.LayoutTransform = new RotateTransform(rot);
            Grid.SetColumn(img, x);
            Grid.SetRow(img, y);
            Grid.SetColumnSpan(img, Cspan);
            Grid.SetRowSpan(img, Rspan);
            return img;
        }
        private Queue<string> TileQ(List<string> tileL)
        {
            Queue<string> tileQ = new Queue<string>();
            while (0 < tileL.Count)
            {
                int rin = rng.Next(0, tileL.Count());
                tileQ.Enqueue(tileL[rin]);
                tileL.RemoveAt(rin);
            }
            return tileQ;
        }
        private  List<string> TileL(string type, bool flip, int count)
        {
            string side;
            List<string> tileL = new List<string>();
            for (int i = 1; i <= count; i++)
            {
                if (flip)
                {
                    if (rng.Next(0, 2) == 0)
                        side = "a";
                    else
                        side = "b";
                    tileL.Add(type + i + side);
                }
                else
                    tileL.Add(type + i);
            }
            return tileL;
        }
    }
}
