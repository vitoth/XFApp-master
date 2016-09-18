using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFApp
{
    class Tile : ContentView
    {
        Label label;
        string text = string.Empty;

        string[] strRow = new string[10] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        string[] strCol = new string[10] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

        public Tile(int row, int col)
        {

            {
                HorizontalOptions = LayoutOptions.Center;
                VerticalOptions = LayoutOptions.CenterAndExpand;
            };

            if (row == 0 && col > 0)
                text = strRow[col - 1];
            else if (col == 0 && row > 0)
                text = strCol[row - 1];
            else text = "";

            if (row == 0 || col == 0)
            {


                label = new Label
                {
                    Text = text,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };

                this.Padding = new Thickness(1);
                this.Content = new Frame
                {
                    Padding = new Thickness(0, 0, 0, 0),
                    Content = new StackLayout
                    {

                        BackgroundColor = Color.Gray,
                        Spacing = 0,
                        Children = {
                        label
                    }
                    }
                };



            }

            else
            {


                label = new Label
                {
                    Text = text,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };

                this.Padding = new Thickness(1);
                this.Content = new Frame
                {
                    Padding = new Thickness(20, 20, 20, 20),
                    Content = new StackLayout
                    {

                        BackgroundColor = Color.Gray,
                        Spacing = 0,
                        Children = {
                        label
                    }
                    }
                };

                this.BackgroundColor = Color.Gray;
                // Don't let touch pass us by.
            }



        }

        // Retain current Row and Col position.
        public int Index { private set; get; }

        public int Row { set; get; }

        public int Col { set; get; }




    }
}
