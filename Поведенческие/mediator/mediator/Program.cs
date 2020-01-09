/*
 * Создано в SharpDevelop.
 * Пользователь: Голубь
 * Дата: 08.01.2020
 * Время: 21:39
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

/*
 Посредник позволяет множеству объектов взаимодействовать друг с другом без необходимости ссылаться.
 Состоит из интерфейсов коллег и посредника, а также их конкретных классов
 */

namespace mediator
{
	
	abstract class Mediator		//реализация интерфейса посредника
{
    public abstract void Send(string msg, Colleague colleague);
}
abstract class Colleague		//реализация интерфейса коллеги
{
    protected Mediator mediator;
 
    public Colleague(Mediator mediator)
    {
        this.mediator = mediator;
    }
 
    public virtual void Send(string message)
    {
        mediator.Send(message, this);
    }
    public abstract void Notify(string message);
}

class CustomerColleague : Colleague		// класс коллеги 1
{
    public CustomerColleague(Mediator mediator)
        : base(mediator)
    { }
 
    public override void Notify(string message)
    {
        Console.WriteLine("Сообщение заказчику: " + message);
    }
}

class ProgrammerColleague : Colleague		// класс коллеги 2
{
    public ProgrammerColleague(Mediator mediator)
        : base(mediator)
    { }
 
    public override void Notify(string message)
    {
        Console.WriteLine("Сообщение программисту: " + message);
    }
}

class TesterColleague : Colleague		// класс коллеги 3
{
    public TesterColleague(Mediator mediator)
        : base(mediator)
    { }
 
    public override void Notify(string message)
    {
        Console.WriteLine("Сообщение тестеру: " + message);
    }
}
 
class ManagerMediator : Mediator		//класс посредника
{
    public Colleague Customer { get; set; }
    public Colleague Programmer { get; set; }
    public Colleague Tester { get; set; }
    public override void Send(string msg, Colleague colleague)
    {
        // если отправитель - заказчик, значит есть новый заказ
        // отправляем сообщение программисту - выполнить заказ
        if (Customer == colleague)
            Programmer.Notify(msg);
        // если отправитель - программист, то можно приступать к тестированию
        // отправляем сообщение тестеру
        else if (Programmer == colleague)
            Tester.Notify(msg);
        // если отправитель - тест, значит продукт готов
        // отправляем сообщение заказчику
        else if (Tester == colleague)
            Customer.Notify(msg);
    }
}
	
	class Program
	{
		public static void Main(string[] args)
		{
        ManagerMediator mediator = new ManagerMediator();
        Colleague customer = new CustomerColleague(mediator);
        Colleague programmer = new ProgrammerColleague(mediator);
        Colleague tester = new TesterColleague(mediator);
        mediator.Customer = customer;
        mediator.Programmer = programmer;
        mediator.Tester = tester;
        customer.Send("Есть заказ, надо сделать программу");
        programmer.Send("Программа готова, надо протестировать");
        tester.Send("Программа протестирована и готова к продаже");
 
        Console.Read();
		}
	}
}