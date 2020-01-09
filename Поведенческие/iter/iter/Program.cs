/*
 * Создано в SharpDevelop.
 * Пользователь: Голубь
 * Дата: 07.01.2020
 * Время: 3:57
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

/*
 Метод итератора используется для обхода (переборки) всех объектов без дополнительного описания
 Состоит из таких элементов, как: определение интерфейсов для обхода составных объектов и создания объекта-итератора, 
 реализация итератора и агрегата для хранения, а также клиента для использования их.
 */

namespace iter
{
	
	class Reader 	//описание клиента
{
    public void SeeBooks(Library library)
    {
        IBookIterator iterator = library.CreateNumerator();
        while(iterator.HasNext())
        {
            Book book = iterator.Next();
            Console.WriteLine(book.Name);
        }
    }
}
 
interface IBookIterator		//описание интерфейса обхода
{
    bool HasNext();
    Book Next();
}
interface IBookNumerable		//описание интерфейса объекта
{
    IBookIterator CreateNumerator();
    int Count { get; }
    Book this[int index] { get;}
}
class Book		
{
    public string Name { get; set; }
}
 
class Library : IBookNumerable		//реализация хранения
{
    private Book[] books;
    public Library()
    {
        books = new Book[]
        {
            new Book{Name="Призрак прошлого"},
            new Book {Name="Объятия"},
            new Book {Name="Фрэнк"},
            new Book {Name="Пациент в системе"}
        };
    }
    public int Count
    {
        get { return books.Length; }
    }
 
    public Book this[int index]
    {
        get { return books[index]; }
    }
    public IBookIterator CreateNumerator()
    {
        return new LibraryNumerator(this);
    }
}
class LibraryNumerator : IBookIterator		//реализация итератора
{
    IBookNumerable aggregate;
    int index=0;
    public LibraryNumerator(IBookNumerable a)
    {
        aggregate = a;
    }
    public bool HasNext()
    {
        return index<aggregate.Count;
    }
 
    public Book Next()
    {
        return aggregate[index++];
    }
}
	
	class Program
	{
		public static void Main(string[] args)
		{
        Library library = new Library();
        Reader reader = new Reader();
        reader.SeeBooks(library);
 
        Console.Read();
		}
	}
}