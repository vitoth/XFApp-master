﻿using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFApp
{
    
    class Board : ContentPage
    {

       
        // Number of tiles horizontally and vertically,
        //  but if you change it, some code will break.
        static readonly int NUM = 11;

        // Array of Tile views, and empty row & column.
        Tile[,] tiles = new Tile[NUM, NUM];

        StackLayout stackLayout;
        AbsoluteLayout absoluteLayout;
        Button randomizeButton;
        Label timeLabel;
        double tileSize;
        bool isBusy;
        bool isPlaying;

        public Board()
        {
            // AbsoluteLayout to host the tiles.
            absoluteLayout = new AbsoluteLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            // Create Tile's for all the rows and columns.
            int index = 0;

            for (int row = 0; row < NUM; row++)
            {
                for (int col = 0; col < NUM; col++)
                {


                    // Instantiate Tile.
                    Tile tile = new Tile(row, col)
                    {
                        Row = row,
                        Col = col
                    };

                    // Add tap recognition
                    TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer
                    {
                        Command = new Command(OnTileTapped),
                        CommandParameter = tile
                    };
                    //dodaje prepoznavanje ako nije prvi red i stgupac gdje su indexi
                    if (!((row == 0 && col > 0) || (col == 0 && row > 0) || (col == 0 && row == 0)))
                        tile.GestureRecognizers.Add(tapGestureRecognizer);

                    // Add it to the array and the AbsoluteLayout.
                    tiles[row, col] = tile;
                    absoluteLayout.Children.Add(tile);
                    index++;

                }
            }

            // This is the "Randomize" button.
            randomizeButton = new Button
            {
                Text = "Randomize",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            randomizeButton.Clicked += OnRandomizeButtonClicked;

            // Label to display elapsed time.
            timeLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            // Put everything in a StackLayout.
            stackLayout = new StackLayout
            {
                Children = {
                    new StackLayout {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Children = {
                            randomizeButton,
                            timeLabel
                        }
                    },
                    absoluteLayout
                }
            };
            stackLayout.SizeChanged += OnStackSizeChanged;

            // And set that to the content of the page.
            this.Padding =
                new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);
            this.Content = stackLayout;
        }

        void OnStackSizeChanged(object sender, EventArgs args)
        {
            double width = stackLayout.Width;
            double height = stackLayout.Height;

            if (width <= 0 || height <= 0)
                return;

            // Orient StackLayout based on portrait/landscape mode.
            stackLayout.Orientation = (width < height) ? StackOrientation.Vertical :
                                                         StackOrientation.Horizontal;

            // Calculate tile size and position based on stack size.
            tileSize = Math.Min(width, height) / NUM;
            absoluteLayout.WidthRequest = NUM * tileSize;
            absoluteLayout.HeightRequest = NUM * tileSize;

            foreach (View view in absoluteLayout.Children)
            {
                Tile tile = (Tile)view;

                AbsoluteLayout.SetLayoutBounds(tile,
                    new Rectangle(tile.Col * tileSize,
                        tile.Row * tileSize,
                        tileSize,
                        tileSize));
            }
        }

        async void OnTileTapped(object parameter)
        {
            if (isBusy)
                return;

            isBusy = true;
            Tile tappedTile = (Tile)parameter;
            await ChangeColor(tappedTile.Row, tappedTile.Col);
            isBusy = false;
        }



        async void OnRandomizeButtonClicked(object sender, EventArgs args)
        {
            
            for (int row = 0; row < NUM; row++)
            {
                for (int col = 0; col < NUM; col++)
                {
                    tiles[row, col].Content.BackgroundColor = Color.Gray;

                }


            }

        }

        async Task ChangeColor(int tappedRow, int tappedCol, uint length = 100)
        {
            if (tiles[tappedRow, tappedCol].Content.BackgroundColor == Color.Olive)
                tiles[tappedRow, tappedCol].Content.BackgroundColor = Color.Gray;
            else
                tiles[tappedRow, tappedCol].Content.BackgroundColor = Color.Olive;

        }


    }
}
