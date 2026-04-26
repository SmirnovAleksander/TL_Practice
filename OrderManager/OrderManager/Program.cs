using System;

class Program
{
    static void Main()
    {
        while ( true )
        {
            string product = ReadNonEmptyInput( "Введите товар: " );
            int count = GetProductCount();
            string name = ReadNonEmptyInput( "Введите имя пользователя: " );
            string adress = ReadNonEmptyInput( "Введите адрес: " );

            bool isConfirmed = ConfirmOrder( name, count, product, adress );

            if ( isConfirmed )
            {
                PrintResult( name, product, count, adress );
            }
            else
            {
                Console.WriteLine( "Отмена заказа, что то пошло не так" );
            }

            if ( !AskToContinue() )
            {
                break;
            }
        }
        Console.WriteLine( "Спасибо за заказ! Досвидания." );
    }

    static bool AskToContinue()
    {
        while ( true )
        {
            Console.Write( "Хотите оформить еще один заказ? да/нет: " );
            string userChoice = Console.ReadLine()?.Trim().ToLower() ?? "";

            if ( userChoice == "да" )
                return true;

            if ( userChoice == "нет" )
                return false;

            Console.WriteLine( "Введите да или нет!" );
        }
    }

    static string ReadNonEmptyInput( string message )
    {
        while ( true )
        {
            Console.Write( message );
            string input = Console.ReadLine() ?? "";

            if ( !string.IsNullOrWhiteSpace( input ) )
                return input.Trim();

            Console.WriteLine( "Поле не может быть пустым. Попробуйте снова." );
        }
    }

    static int GetProductCount()
    {
        Console.Write( "Введите количество товара: " );
        string input = Console.ReadLine() ?? "";

        int count;
        while ( !int.TryParse( input, out count ) || count <= 0 )
        {
            Console.Write( "Введите корректное количество товара: " );
            input = Console.ReadLine() ?? "";
        }
        return count;
    }

    static bool ConfirmOrder( string name, int count, string product, string adress )
    {
        while ( true )
        {
            Console.WriteLine( $"Здравствуйте, {name}, вы заказали {count} {product} на адрес {adress}, все верно?" );
            Console.Write( "Введите да или нет: " );

            string userChoice = Console.ReadLine()?.Trim().ToLower() ?? "";

            if ( userChoice == "да" )
                return true;

            if ( userChoice == "нет" )
                return false;

            Console.WriteLine( "Введите да или нет!" );
        }
    }

    static void PrintResult( string name, string product, int count, string adress )
    {
        DateTime dateDelivery = DateTime.Today.AddDays( 3 );
        string date = dateDelivery.ToString( "dd.MM.yyyy" );

        Console.WriteLine( $"{name}! Ваш заказ {product} в количестве {count} оформлен! Ожидайте доставку по адресу {adress} к {date}" );
    }
}