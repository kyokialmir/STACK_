using System;

class Program
{
    static void Main(string[] args)
    {
        MovieDatabase movieDatabase = new MovieDatabase();
        movieDatabase.Add_movie("Побег из Шоушенка", "Драма", 2020);
        movieDatabase.Add_movie("Крестный отец", "Криминал", 2024);
        movieDatabase.Add_movie("Темный рыцарь", "Боевик", 2019);

        movieDatabase.Print_all_movies();

        movieDatabase.Remove_movie("Крестный отец");

        movieDatabase.Print_all_movies();

        movieDatabase.Search_movie_by_title("Темный рыцарь");
    }
}
class Movie
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public int ReleaseYear { get; set; }
}

class MyStack<T>
{
    private T[] items;
    private int top;

    public MyStack(int size)
    {
        items = new T[size];
        top = -1;
    }

    public void Push(T item)
    {
        if (top == items.Length - 1)
        {
            Console.WriteLine("Стек переполнен");
            return;
        }

        items[++top] = item;
    }

    public T Pop()
    {
        if (top == -1)
        {
            Console.WriteLine("Стек пуст");
            return default(T);
        }

        T item = items[top];
        items[top--] = default(T);
        return item;
    }

    public T Peek()
    {
        if (top == -1)
        {
            Console.WriteLine("Стек пуст");
            return default(T);
        }

        return items[top];
    }

    public bool IsEmpty()
    {
        return top == -1;
    }
}

class MovieDatabase
{
    private MyStack<Movie> movies = new MyStack<Movie>(100);

    public void Add_movie(string title, string genre, int releaseYear)
    {
        movies.Push(new Movie { Title = title, Genre = genre, ReleaseYear = releaseYear });
        Console.WriteLine("Фильм добавлен.");
    }

    public void Remove_movie(string title)
    {
        MyStack<Movie> tempStack = new MyStack<Movie>(100);

        while (!movies.IsEmpty())
        {
            Movie movie = movies.Pop();
            if (movie.Title != title)
            {
                tempStack.Push(movie);
            }
        }

        while (!tempStack.IsEmpty())
        {
            movies.Push(tempStack.Pop());
        }

        Console.WriteLine("Фильм удален.");
    }

    public void Print_all_movies()
    {
        Console.WriteLine("Все фильмы:");

        MyStack<Movie> tempStack = new MyStack<Movie>(100);

        while (!movies.IsEmpty())
        {
            Movie movie = movies.Pop();
            Console.WriteLine($"Название: {movie.Title}, Жанр: {movie.Genre}, Год выпуска: {movie.ReleaseYear}");
            tempStack.Push(movie);
        }

        while (!tempStack.IsEmpty())
        {
            movies.Push(tempStack.Pop());
        }
    }

    public void Search_movie_by_title(string title)
    {
        bool found = false;

        MyStack<Movie> tempStack = new MyStack<Movie>(100);

        while (!movies.IsEmpty())
        {
            Movie movie = movies.Pop();
            if (movie.Title == title)
            {
                Console.WriteLine($"Фильм : Название: {movie.Title}, Жанр: {movie.Genre}, Год выпуска: {movie.ReleaseYear}");
                found = true;
            }
            tempStack.Push(movie);
        }

        while (!tempStack.IsEmpty())
        {
            movies.Push(tempStack.Pop());
        }

        if (!found)
        {
            Console.WriteLine("Фильм не найден.");
        }
    }
    public void List_movies_by_genre(string genre)
    {
        Console.WriteLine($"Фильмы жанра \"{genre}\":");

        MyStack<Movie> tempStack = new MyStack<Movie>(100);

        while (!movies.IsEmpty())
        {
            Movie movie = movies.Pop();
            if (movie.Genre == genre)
            {
                Console.WriteLine($"Название: {movie.Title}, Год выпуска: {movie.ReleaseYear}");
            }
            tempStack.Push(movie);
        }

        while (!tempStack.IsEmpty())
        {
            movies.Push(tempStack.Pop());
        }
    }

    public void List_movies_by_release_year(int releaseYear)
    {
        Console.WriteLine($"Фильмы выпущены в {releaseYear} году:");

        MyStack<Movie> tempStack = new MyStack<Movie>(100);

        while (!movies.IsEmpty())
        {
            Movie movie = movies.Pop();
            if (movie.ReleaseYear == releaseYear)
            {
                Console.WriteLine($"Название: {movie.Title}, Жанр: {movie.Genre}");
            }
            tempStack.Push(movie);
        }

        while (!tempStack.IsEmpty())
        {
            movies.Push(tempStack.Pop());
        }
    }
}

