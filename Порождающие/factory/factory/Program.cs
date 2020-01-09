/*
 * Создано в SharpDevelop.
 * Пользователь: Голубь
 * Дата: 07.01.2020
 * Время: 22:04
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

/*
 Абстрактная фабрика создает семейство связанных объектов без привязки в классам.
 Состоит из абстрактных классов объектов определяющих интерфейс, конкретных классов для реализации абстрактных, 
 абстрактной фабрики для создания, конкретную фабрику для определения объектов и клиента используемого абстрактные фабрики и объекты
 */

namespace factory
{
	
abstract class Weapon		//абстрактный класс оружие
{
    public abstract void Hit();
}

abstract class Movement		//абстрактный класс движение
{
    public abstract void Move();
}
 

class Arbalet : Weapon		//класс арбалет
{
    public override void Hit()
    {
        Console.WriteLine("Стреляем из арбалета");
    }
}

class Sword : Weapon		//класс меч
{
    public override void Hit()
    {
        Console.WriteLine("Бьем мечом");
    }
}

class FlyMovement : Movement		//движение полет
{
    public override void Move()
    {
        Console.WriteLine("Летим");
    }
}

class RunMovement : Movement		//движение бег
{
    public override void Move()
    {
        Console.WriteLine("Бежим");
    }
}

abstract class HeroFactory		//класс абстрактной фабрики
{
    public abstract Movement CreateMovement();
    public abstract Weapon CreateWeapon();
}

class ElfFactory : HeroFactory		//фабрика создания летящего героя с арбалетом
{
    public override Movement CreateMovement()
    {
        return new FlyMovement();
    }
 
    public override Weapon CreateWeapon()
    {
            return new Arbalet();
    }
}

class VoinFactory : HeroFactory		//фабрика создания бегущего героя с мечом
{
    public override Movement CreateMovement()
    {
        return new RunMovement();
    }
 
    public override Weapon CreateWeapon()
    {
        return new Sword();
    }
}

class Hero		//клиент
{
    private Weapon weapon;
    private Movement movement;
    public Hero(HeroFactory factory)
    {
        weapon = factory.CreateWeapon();
        movement = factory.CreateMovement();
    }
    public void Run()
    {
        movement.Move();
    }
    public void Hit()
    {
        weapon.Hit();
    }
}
	
	class Program
	{
		public static void Main(string[] args)
		{
        Hero elf = new Hero(new ElfFactory());
        elf.Hit();
        elf.Run();
 
        Hero voin = new Hero(new VoinFactory());
        voin.Hit();
        voin.Run();
 
        Console.ReadLine();
		}
	}
}