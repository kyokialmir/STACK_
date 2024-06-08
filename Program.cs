using System;
using System.Collections.Generic;
class Program
{
    static void Main(string[] args)
    {
        MovieDatabase movieDatabase = new MovieDatabase(100);

        movieDatabase.AddMovie("Побег из Шоушенка", "Драма", 1994);
        movieDatabase.AddMovie("Крестный отец", "Криминал", 1972);
        movieDatabase.AddMovie("Темный рыцарь", "Боевик", 2008);

        movieDatabase.PrintAllMovies();

        movieDatabase.RemoveMovie("Крестный отец");

        movieDatabase.PrintAllMovies();

        movieDatabase.SearchMovieByTitle("Темный рыцарь");

        movieDatabase.ListMoviesByGenre("Драма");

        movieDatabase.ListMoviesByReleaseYear(2008);
    }
}
class StackWithArray
{
    private int[] _items;
    private int _size;
    private readonly int _initialSize = 4;
    private int _top;

    public StackWithArray()
    {
        _size = _initialSize;
        _items = new int[_size];
        _top = -1;
    }

    public StackWithArray(int size)
    {
        _size = size;
        _items = new int[_size];
        _top = -1;
    }

    public void Push(int value)
    {
        IncreaseCapacity();
        _top++;
        _items[_top] = value;
    }

    public int Pop()
    {
        if (IsEmpty())
        {
            throw new Exception("Стек пуст");
        }

        int temp = Peek();
        _top--;
        DecreaseCapacity();
        return temp;
    }

    public int Peek()
    {
        if (IsEmpty())
        {
            throw new Exception("Стек пуст");
        }

        return _items[_top];
    }

    public bool IsEmpty()
    {
        return _top == -1;
    }

    private void IncreaseCapacity()
    {
        if (_top + 1 >= _size)
        {
            int[] temp = new int[_size * 2];
            Array.Copy(_items, temp, _size);
            _items = temp;
            _size *= 2;
        }
    }

    private void DecreaseCapacity()
    {
        if (_size > _initialSize && _top + 1 <= _size / 4)
        {
            int[] temp = new int[_size / 2];
            Array.Copy(_items, temp, _top + 1);
            _items = temp;
            _size /= 2;
        }
    }
}

class Movie
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public int ReleaseYear { get; set; }
}

class MovieDatabase
{
    private Stack<Movie> moviesStack;

    public MovieDatabase(int initialCapacity)
    {
        moviesStack = new Stack<Movie>(initialCapacity);
    }

    public void AddMovie(string title, string genre, int releaseYear)
    {
        Movie newMovie = new Movie { Title = title, Genre = genre, ReleaseYear = releaseYear };
        moviesStack.Push(newMovie);
        Console.WriteLine($"Фильм \"{title}\" успешно добавлен.");
    }

    public void RemoveMovie(string title)
    {
        Movie removedMovie = null;
        Stack<Movie> tempStack = new Stack<Movie>();

        while (moviesStack.Count > 0)
        {
            Movie currentMovie = moviesStack.Pop();
            if (currentMovie.Title != title)
            {
                tempStack.Push(currentMovie);
            }
            else
            {
                removedMovie = currentMovie;
                break;
            }
        }

        while (tempStack.Count > 0)
        {
            moviesStack.Push(tempStack.Pop());
        }

        if (removedMovie != null)
        {
            Console.WriteLine($"Фильм \"{removedMovie.Title}\" успешно удален.");
        }
        else
        {
            Console.WriteLine("Фильм не найден.");
        }
    }

    public void PrintAllMovies()
    {
        Console.WriteLine("Все фильмы:");
        foreach (var movie in moviesStack)
        {
            Console.WriteLine($"Название: {movie.Title}, Жанр: {movie.Genre}, Год выпуска: {movie.ReleaseYear}");
        }
    }

    public void SearchMovieByTitle(string title)
    {
        bool found = false;

        foreach (var movie in moviesStack)
        {
            if (movie.Title == title)
            {
                Console.WriteLine($"Фильм найден: Название: {movie.Title}, Жанр: {movie.Genre}, Год выпуска: {movie.ReleaseYear}");
                found = true;
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine("Фильм не найден.");
        }
    }

    public void ListMoviesByGenre(string genre)
    {
        bool found = false;

        Console.WriteLine($"Фильмы жанра \"{genre}\":");
        foreach (var movie in moviesStack)
        {
            if (movie.Genre == genre)
            {
                Console.WriteLine($"Название: {movie.Title}, Год выпуска: {movie.ReleaseYear}");
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine($"Фильмы жанра \"{genre}\" не найдены.");
        }
    }

    public void ListMoviesByReleaseYear(int releaseYear)
    {
        bool found = false;

        Console.WriteLine($"Фильмы выпущены в {releaseYear} году:");
        foreach (var movie in moviesStack)
        {
            if (movie.ReleaseYear == releaseYear)
            {
                Console.WriteLine($"Название: {movie.Title}, Жанр: {movie.Genre}");
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine($"Фильмы выпущены в {releaseYear} году не найдены.");
        }
    }
}
