/*
 * Создано в SharpDevelop.
 * Пользователь: Голубь
 * Дата: 07.01.2020
 * Время: 16:38
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

/*
 Строитель позволяет разделять создание объекта на этапы когда необъодима независимость от частей объекта и его связей.
 Состоит из создаваемого объекта, интерфейс строителя и его реализацию, а также распорядителя, используемого строителя
 */
namespace builder
{
	
	public abstract class BaseComputer		//описание частей объекта
{
    public abstract void GetProcessor();
    public abstract void GetRom();
    public abstract void GetHdd();
    public abstract void GetVideoCard();
}
	class StandartComputer:BaseComputer		//строитель стандарт
{
    public override void GetProcessor()
    {
        Console.WriteLine("Intel i3");
    }
 
    public override void GetRom()
    {
        Console.WriteLine("4Gb");
    }
 
    public override void GetHdd()
    {
        Console.WriteLine("500Gb");
    }
 
    public override void GetVideoCard()
    {
        Console.WriteLine("GeForce GT 730");
    }
}
	    class PremiumComputer:BaseComputer		//строитель премиум
    {
        public override void GetProcessor()
        {
            Console.WriteLine("Intel i7");
        }
 
        public override void GetRom()
        {
            Console.WriteLine("16Gb");
        }
 
        public override void GetHdd()
        {
            Console.WriteLine("2Tb");
        }
 
        public override void GetVideoCard()
        {
            Console.WriteLine("GeForce GTX 980");
        }
    }
	    
class Director		//распорядитель
{
    public void Construct(BaseComputer computer)
    {
        computer.GetProcessor();
        computer.GetRom();
        computer.GetHdd();
        computer.GetVideoCard();
    }
}
	
	class Program
	{
		public static void Main(string[] args)
		{
var director = new Director();
var standartComputer = new StandartComputer();
var premiumComputer = new PremiumComputer();
 
Console.WriteLine("--Надо собрать обычный компьютер");
director.Construct(standartComputer);
Console.WriteLine("\n--Надо собрать дорогой и мощный компьютер");
director.Construct(premiumComputer);
Console.ReadLine();
		}
	}
}