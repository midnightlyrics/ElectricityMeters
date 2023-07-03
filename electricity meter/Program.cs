using System;

public interface IElMeter
{
    void Authorize(string address, string password);
    DateTime ReadDateTime();
    void WriteDateTime(DateTime dateTime);
    void WriteTariffProgram(int tariffNumber, TimeSpan[] transitionPoints);
}

public abstract class ElMeter : IElMeter
{
    protected string SerialNumber { get; set; }
    protected string DigitalAddress { get; set; }
    protected string Password { get; set; }

    public abstract void Authorize(string digitaladdress, string password);

    public DateTime ReadDateTime()
    {
        Console.WriteLine("Считанное время с ПК: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
        return DateTime.Now;
    }

    public void WriteDateTime(DateTime dateTime)
    {
        Console.WriteLine("Дата и время для записи: " + dateTime.ToString("yyyy.MM.dd HH:mm:ss"));
    }

    public abstract void WriteTariffProgram(int tariffNumber, TimeSpan[] transitionPoints);
}

public class CE207ElMeter : ElMeter
{
    private const string ExpectedAddress = "16";
    private const string ExpectedPassword = "1234567812345678";
    private const int MaxTariffs = 4;

    public override void Authorize(string digitaladdress, string password)
    {
        // Имитация обмена счетчиком (задержка в несколько секунд)
        System.Threading.Thread.Sleep(2000);

        if (digitaladdress != ExpectedAddress || password != ExpectedPassword)
            throw new Exception("Авторизация не удалась. Неверный адрес или пароль.");

        DigitalAddress = digitaladdress;
        Password = password;
    }

    public override void WriteTariffProgram(int tariffNumber, TimeSpan[] transitionPoints)
    {
        Console.WriteLine("Точки перехода для тарифа " + tariffNumber + ":");

        for (int i = 0; i < transitionPoints.Length; i++)
        {
            Console.WriteLine("Точка " + (i + 1) + ": " + transitionPoints[i].ToString(@"hh\:mm"));
        }
    }
}

public class CE208ElMeter : ElMeter
{
    private const string ExpectedAddress = "16";
    private const string ExpectedPassword = "1234567812345678";
    private const int MaxTariffs = 8;

    public override void Authorize(string digitaladdress, string password)
    {
        // Имитация обмена счетчиком (задержка в несколько секунд)
        System.Threading.Thread.Sleep(2000);

        if (digitaladdress != ExpectedAddress || password != ExpectedPassword)
            throw new Exception("Авторизация не удалась. Неверный адрес или пароль.");

        DigitalAddress = digitaladdress;
        Password = password;
    }

    public override void WriteTariffProgram(int tariffNumber, TimeSpan[] transitionPoints)
    {
        Console.WriteLine("Точки перехода для тарифа " + tariffNumber + ":");

        for (int i = 0; i < transitionPoints.Length; i++)
        {
            Console.WriteLine("Точка " + (i + 1) + ": " + transitionPoints[i].ToString(@"hh\:mm"));
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            ElMeter elmeter1 = new CE207ElMeter();
            ElMeter elmeter2 = new CE208ElMeter();

            elmeter1.Authorize("16", "1234567812345678");
            elmeter1.ReadDateTime();
            elmeter1.WriteDateTime(DateTime.Now);
            elmeter1.WriteTariffProgram(1, new TimeSpan[]
            {
                new TimeSpan(0, 0, 0),    // Точка перехода 1
                new TimeSpan(6, 0, 0),    // Точка перехода 2
                new TimeSpan(12, 0, 0),   // Точка перехода 3
                new TimeSpan(18, 0, 0)    // Точка перехода 4
            });

            Console.WriteLine();

            elmeter2.Authorize("16", "1234567812345678");
            elmeter2.ReadDateTime();
            elmeter2.WriteDateTime(DateTime.Now);
            elmeter2.WriteTariffProgram(2, new TimeSpan[]
            {
                new TimeSpan(0, 0, 0),    // Точка перехода 1
                new TimeSpan(3, 0, 0),    // Точка перехода 2
                new TimeSpan(6, 0, 0),    // Точка перехода 3
                new TimeSpan(9, 0, 0),    // Точка перехода 4
                new TimeSpan(12, 0, 0),   // Точка перехода 5
                new TimeSpan(15, 0, 0),   // Точка перехода 6
                new TimeSpan(18, 0, 0),   // Точка перехода 7
                new TimeSpan(21, 0, 0)    // Точка перехода 8
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }
}