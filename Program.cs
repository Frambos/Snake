using System;
using System.Threading;

namespace SnakeApp
{
    public class Snake
    {
        int height = 25; //Wysokość planszy
        int width = 35; //Szerokość planszy

        int[] X = new int[50];
        int[] Y = new int[50];

        int foodX;
        int foodY;

        int parts = 3; //długość węża
        int points = 0; //punkty gracza

        ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
        char key = 's';

        Random rnd = new Random();

        Snake() //konstruktor
        {
            X[0] = 10;
            Y[0] = 10;
            Console.CursorVisible = false;
            foodX = rnd.Next(2, (width - 2));   //Nowe losowe współrzędne dla owocu
            foodY = rnd.Next(2, (height - 2));
        }
        public void DrawGrid()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 1; i <= (width + 2); i++)  //górna ściana
            {
                Console.SetCursorPosition(i, 1);
                Console.WriteLine("-");
            }
            for (int i = 1; i <= (width + 2); i++)  //dolna ściana
            {
                Console.SetCursorPosition(i, (height + 2));
                Console.WriteLine("-");
            }
            for (int i = 1; i <= (height + 1); i++) //lewa ściana
            {
                Console.SetCursorPosition(1, i);
                Console.WriteLine("|");
            }
            for (int i = 1; i <= (height + 1); i++) //prawa ściana
            {
                Console.SetCursorPosition((width + 2), i);
                Console.WriteLine("|");
            }
        }
        public void Input()
        {
            if (Console.KeyAvailable) //Sprawdzanie czy wciśnięty jest klawisz
            {
                keyInfo = Console.ReadKey(true); //pobiera klawisz 
                key = keyInfo.KeyChar; //Zamiana klawisza na znak char i przypisanie do zmiennej
            }
        }
        public void WritePoint(int x, int y) //Rysowanie pojedynczego punktu 
        {
            Console.SetCursorPosition(x, y); //ustawia kursor
            Console.Write("#");
        }
        public void Game()
        {
            if (X[0] == foodX)
            {
                if(Y[0]==foodY)
                {
                    parts++; //Zwiększenie długości węża
                    foodX = rnd.Next(2, (width - 2));   //Nowe losowe współrzędne dla owocu
                    foodY = rnd.Next(2, (height - 2));
                    points++; //Inkrementacja punktów gracza
                }
            }
            for (int i = parts; i > 1; i--) //kolejne człony węża
            {
                X[i - 1] = X[i - 2];
                Y[i - 1] = Y[i - 2];
            }
            switch(key)
            {
                case 'w':
                    Y[0]--;
                    break;

                case 's':
                    Y[0]++;
                    break;
                case 'a':
                    X[0]--;
                    break;
                case 'd':
                    X[0]++;
                    break;
            }
            for (int i = 0; i <= (parts-1); i++) //Rysowanie węża
            {
                Console.ForegroundColor = ConsoleColor.Green;
                WritePoint(X[i], Y[i]);
                Console.ForegroundColor = ConsoleColor.Yellow;
                WritePoint(foodX, foodY); //owoc
            }
            Thread.Sleep(100); //Wstrzymuje bieżący wątek dla określonej liczby milisekund
        }
        public bool InBoard() //Zwraca false jeśli wąż dotknie ściany
        {
            if (X[0] > width) return false;
            else if (Y[0] > height) return false;
            else if (X[0] < 2) return false;
            else if (Y[0] < 2) return false;
            else return true;
        }
        public void Reset() //Resetuje punkty, pozycję i długość węża
        {
            Console.Clear();
            Console.WriteLine("Koniec gry");
            Console.WriteLine("Zdobyłes " + points + " punktów");
            Console.WriteLine("Nacisnij dowolny klawisz aby zagrać ponownie");
            Console.ReadKey();
            points = 0;
            parts = 3;
            X[0] = 10; // pozycja węża
            Y[0] = 10;
            foodX = rnd.Next(2, (width - 2));   //Nowe losowe współrzędne dla owocu
            foodY = rnd.Next(2, (height - 2));
        }
        static void Main(string[] args)
        {
            Snake snake = new Snake();
            do
            {
                snake.DrawGrid();
                snake.Input();
                snake.Game();
                if (snake.InBoard() == false)
                {
                    snake.Reset();

                }
            } while (true);
            
        }
    }
}

