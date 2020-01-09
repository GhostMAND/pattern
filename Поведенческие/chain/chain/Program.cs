/*
 * Создано в SharpDevelop.
 * Пользователь: Голубь
 * Дата: 07.01.2020
 * Время: 6:17
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

/*
 Цепочка обязанностей позволяет перемещать запрос по цепочке для обработки с помощью нескольких объектов без привязки.
 Состоит из интерфейса для обработки запроса, нескольких обработчиков запроса и отправителя запроса.
 */
namespace chain
{
	class Receiver		//реализация отправителя
{

    public bool BankTransfer { get; set; }
    public bool MoneyTransfer { get; set; }
    public bool PayPalTransfer { get; set; }
    public Receiver(bool bt, bool mt, bool ppt)
    {
        BankTransfer = bt;
        MoneyTransfer = mt;
        PayPalTransfer = ppt;
    }
}
abstract class PaymentHandler		//реализация интерфейса обработки
{
    public PaymentHandler Successor { get; set; }
    public abstract void Handle(Receiver receiver);
}
 
class BankPaymentHandler : PaymentHandler		//первый обработчик
{
    public override void Handle(Receiver receiver)
    {
        if (receiver.BankTransfer == true)
            Console.WriteLine("Выполняем банковский перевод");
        else if (Successor != null)
            Successor.Handle(receiver);
    }
}
 
class PayPalPaymentHandler : PaymentHandler		//второй обработчик
{
    public override void Handle(Receiver receiver)
    {
        if (receiver.PayPalTransfer == true)
            Console.WriteLine("Выполняем перевод через PayPal");
        else if (Successor != null)
            Successor.Handle(receiver);
    }
}

class MoneyPaymentHandler : PaymentHandler		//третий обработчик
{
    public override void Handle(Receiver receiver)
    {
        if (receiver.MoneyTransfer == true)
            Console.WriteLine("Выполняем перевод через системы денежных переводов");
        else if (Successor != null)
            Successor.Handle(receiver);
    }
}
	
	
	class Program
	{
		public static void Main(string[] args)
		{
        Receiver receiver = new Receiver(false, true, true);
         
        PaymentHandler bankPaymentHandler = new BankPaymentHandler();
        PaymentHandler moneyPaymentHnadler = new MoneyPaymentHandler();
        PaymentHandler paypalPaymentHandler = new PayPalPaymentHandler();
        
        bankPaymentHandler.Successor = paypalPaymentHandler;
 
        bankPaymentHandler.Handle(receiver);
 
        Console.Read();
		}
	}
}