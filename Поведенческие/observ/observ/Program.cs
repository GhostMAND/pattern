/*
 * Создано в SharpDevelop.
 * Пользователь: Голубь
 * Дата: 07.01.2020
 * Время: 2:17
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;

/*
 Наблюдатель необходим для оповещения наблюдателей о изменении наблюдаемого объекта по аналогии с подпиской на определенный ресурс 
для оповещения конкретных пользователей, а не всех имеющихся.
Состоит из наблюдаемого объекта, интерфейса (добавление, удаление и уведомление) объекта для коллекции, наблюдателя и его интерфейса
 */

namespace observ
{
	interface IObserver		//реализация наблюдателя
{
    void Update(Object ob); //вызывается объектом для уведомления
}
 
interface IObservable		//реализация объекта
{
    void RegisterObserver(IObserver o);
    void RemoveObserver(IObserver o);
    void NotifyObservers();
}
 
class Stock : IObservable		//реализация интерфейса объекта
{
    StockInfo sInfo; 
 
    List<IObserver> observers;
    public Stock()
    {
        observers = new List<IObserver>();
        sInfo= new StockInfo();
    }
    public void RegisterObserver(IObserver o)
    {
        observers.Add(o);
    }
 
    public void RemoveObserver(IObserver o)
    {
        observers.Remove(o);
    }
 
    public void NotifyObservers()
    {
        foreach(IObserver o in observers)
        {
            o.Update(sInfo);
        }
    }
 
    public void Market()
    {
        Random rnd = new Random();
        sInfo.USD = rnd.Next(20, 40);
        sInfo.Euro = rnd.Next(30, 50);
        NotifyObservers();
    }
}
 
class StockInfo
{
    public int USD { get; set; }
    public int Euro { get; set; }
}
 
class Broker : IObserver		//реализация интерфейса наблюдателя 1
{
    public string Name { get; set; }
    IObservable stock;
    public Broker(string name, IObservable obs)
    {
        this.Name = name;
        stock = obs;
        stock.RegisterObserver(this);
    }
    public void Update(object ob)
    {
        StockInfo sInfo = (StockInfo)ob;
 
        if(sInfo.USD>30)
            Console.WriteLine("Брокер {0} продает доллары;  Курс доллара: {1}", this.Name, sInfo.USD);
        else
            Console.WriteLine("Брокер {0} покупает доллары;  Курс доллара: {1}", this.Name, sInfo.USD);
    }
    public void StopTrade()
    {
        stock.RemoveObserver(this);
        stock=null;
    }
}
 
class Bank : IObserver		//реализация интерфейса наблюдателя 2
{
    public string Name { get; set; }
    IObservable stock;
    public Bank(string name, IObservable obs)
    {
        this.Name = name;
        stock = obs;
        stock.RegisterObserver(this);
    }
    public void Update(object ob)
    {
        StockInfo sInfo = (StockInfo)ob;
 
        if (sInfo.Euro > 40)
            Console.WriteLine("Банк {0} продает евро;  Курс евро: {1}", this.Name, sInfo.Euro);
        else
            Console.WriteLine("Банк {0} покупает евро;  Курс евро: {1}", this.Name, sInfo.Euro);
    }
}
	
	
	
	class Program
	{
		public static void Main(string[] args)
		{
        Stock stock = new Stock();
        Bank bank = new Bank("ЮнитБанк", stock);
        Broker broker = new Broker("Иван Иваныч", stock);
        // имитация торгов
        stock.Market();
        // брокер прекращает наблюдать за торгами
        broker.StopTrade();
        // имитация торгов
        stock.Market();
            
            Console.ReadLine();
		}
	}
}