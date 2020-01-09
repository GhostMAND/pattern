/*
 * Создано в SharpDevelop.
 * Пользователь: Голубь
 * Дата: 08.01.2020
 * Время: 21:36
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

/*
 Мост отделяет абстракцию от реализации для независимого изменения каждой из них.
 Состоит из базового интерфейса с ссылкой, уточненной абстракции, базового интерфейса для реализации, конкретных реализаций и клиента
 */

namespace bridge
{
	interface ILanguage		//интерфейс для реализации
{
    void Build();
    void Execute();
}
 
class CPPLanguage : ILanguage		//конкретная реализация 1
{
    public void Build()
    {
        Console.WriteLine("С помощью компилятора C++ компилируем программу в бинарный код");
    }
 
    public void Execute()
    {
        Console.WriteLine("Запускаем исполняемый файл программы");
    }
}
 
class CSharpLanguage : ILanguage		//конкретная реализация 2
{
    public void Build()
    {
        Console.WriteLine("С помощью компилятора Roslyn компилируем исходный код в файл exe");
    }
 
    public void Execute()
    {
        Console.WriteLine("JIT компилирует программу бинарный код");
        Console.WriteLine("CLR выполняет скомпилированный бинарный код");
    }
}
 
abstract class Programmer		//базовый интерфейс с ссылкой
{
    protected ILanguage language;
    public ILanguage Language
    {
        set { language = value; }
    }
    public Programmer (ILanguage lang)
    {
        language = lang;
    }
    public virtual void DoWork()
    {
        language.Build();
        language.Execute();
    }
    public abstract void EarnMoney();
}
 
class FreelanceProgrammer : Programmer		//уточненная абстракция 1
{
    public FreelanceProgrammer(ILanguage lang) : base(lang)
    {
    }
    public override void EarnMoney()
    {
        Console.WriteLine("Получаем оплату за выполненный заказ");
    }
}
class CorporateProgrammer : Programmer		//уточненная абстракция 2
{
    public CorporateProgrammer(ILanguage lang)
        : base(lang)
    {
    }
    public override void EarnMoney()
    {
        Console.WriteLine("Получаем в конце месяца зарплату");
    }
}
	
	
	class Program
	{
		public static void Main(string[] args)
		{
        // создаем нового программиста, он работает с с++
        Programmer freelancer = new FreelanceProgrammer(new CPPLanguage());
        freelancer.DoWork();
        freelancer.EarnMoney();
        // пришел новый заказ, но теперь нужен c#, изменяем язык у программиста 
        freelancer.Language = new CSharpLanguage();
        freelancer.DoWork();
        freelancer.EarnMoney();
 
        Console.Read();
		}
	}
}