/*
 * Создано в SharpDevelop.
 * Пользователь: Голубь
 * Дата: 06.01.2020
 * Время: 20:24
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

/*
 Шаблон состояния используется в случаях, когда объект должен менять поведение в зависимости от состояния.
 Состоит из трех частей. Интерфейс, который реализует необходимые состояния. Класс, в котором меняются объекты зависимые от состояния. Классы необходимых состояний.	
 */

namespace state
{
	interface IState		//реализован основной интерфейс
{
    void On();
    void Off();
    void Pass();
    void AddCoin(int count);
}
	
	class PowerOffState : IState	//состояние отключенного аппарата
{
    private readonly Machine _machine;
 
    public PowerOffState(Machine machine)
    {
        _machine = machine;
    }
    public void On()
    {
        Console.WriteLine("Аппарат включен");
        _machine.SetState(_machine.WaitingState);
    }
 
    public void Off()
    {
        Console.WriteLine("Аппарат и так выключен");
    }
 
    public void Pass()
    {
        Console.WriteLine("Аппарат отключен, выдача невозможна");
    }
 
    public void AddCoin(int count)
    {
        _machine.AddCoin(count);
        Console.WriteLine("Монета добавлена");
    }
}
	
	class WaitingState : IState		//состояние аппарата в ожидании
{
    private readonly Machine _machine;
 
    public WaitingState(Machine machine)
    {
        _machine = machine;
    }
 
    public void On()
    {
        Console.WriteLine("Аппарат уже и так включен");
    }
 
    public void Off()
    {
        Console.WriteLine("Аппарат выключен");
    }
 
    public void Pass()
    {
        if (_machine.CountCoin > 0)
        {
            Console.WriteLine("Сейчас всё разольем");
            _machine.AddCoin(-1);
        }
        else
        {
            _machine.SetState(_machine.CoinOffState);
            _machine.PassCoffee();
        }
    }
 
    public void AddCoin(int count)
    {
        _machine.AddCoin(count);
        Console.WriteLine("Монета добавлена");
    }
}
	
	class CoinOffState : IState		//состояние отсутствия монет в аппарате
{
    private readonly Machine _machine;
 
    public CoinOffState(Machine machine)
    {
        _machine = machine;
    }
 
    public void On()
    {
        Console.WriteLine("Аппарат уже и так включен");
    }
 
    public void Off()
    {
        Console.WriteLine("Аппарат выключен");
        _machine.SetState(_machine.PowerOffState);
    }
 
    public void Pass()
    {
        if (_machine.CountCoin > 0)
        {
            _machine.SetState(_machine.PassState);
            _machine.PassCoffee();
        }
        else
        {
            Console.WriteLine("Монет нет, выдавать не буду");
        }
 
    }
 
    public void AddCoin(int count)
    {
        Console.WriteLine("Добавляем монеты");
        _machine.AddCoin(count);
        if (_machine.CountCoin > 0)
            _machine.SetState(_machine.WaitingState);
    }
}
	
	class PassState : IState		//состояние выдачи
{
    private readonly Machine _machine;
 
    public PassState(Machine machine)
    {
        _machine = machine;
    }
    public void On()
    {
        Console.WriteLine("Аппарат уже и так включен");
    }
 
    public void Off()
    {
        Console.WriteLine("Аппарат выключен");
    }
 
    public void Pass()
    {
        if (_machine.CountCoin > 0)
        {
            Console.WriteLine("Идёт выдача...");
            _machine.AddCoin(-1);
            _machine.SetState(_machine.WaitingState);
        }
 
        else
        {
            _machine.SetState(_machine.CoinOffState);
            _machine.PassCoffee();
        }
 
    }
 
    public void AddCoin(int count)
    {
        _machine.AddCoin(count);
        Console.WriteLine("Монета добавлена");
    }
}
	
	class Machine		//реализация класса работы аппарата
{
    private IState _state;
    private int _countCoin;
 
    public CoinOffState CoinOffState { get; private set; }
    public PowerOffState PowerOffState { get; private set; }
    public PassState PassState { get; private set; }
    public WaitingState WaitingState { get; private set; }
    public int CountCoin {
        get { return _countCoin; }
    }
 
    public Machine()
    {
        PowerOffState = new PowerOffState(this);
        CoinOffState = new CoinOffState(this);
        PassState = new PassState(this);
        WaitingState = new WaitingState(this);
        _state = WaitingState;
    }
 
    public void SetState(IState state)
    {
        _state = state;
    }
 
    public void PassCoffee()
    {
        _state.Pass();
    }
 
    public void PowerOff()
    {
        _state.Off();
    }
    public void PowerOn()
    {
        _state.On();
    }
 
    public void AddCoin(int count)
    {
        _countCoin += count;
    }
}
	
	class Program
	{
		public static void Main(string[] args)
		{
var machine = new Machine();
machine.PowerOn();
machine.PassCoffee();
machine.AddCoin(3);
machine.PassCoffee();
machine.PassCoffee();
machine.PassCoffee();
machine.PassCoffee();
machine.PowerOff();
machine.PassCoffee();
Console.ReadLine();
		}
	}
}