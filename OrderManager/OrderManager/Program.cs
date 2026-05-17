namespace OrderManager;

internal class Program
{
    private const int DeliveryDays = 3;

    public static void Main()
    {
        bool isOrderProcessing = true;

        while ( isOrderProcessing )
        {
            string product = ReadNonEmptyInput( "Введите товар: " );
            int count = GetProductCount();
            string customerName = ReadNonEmptyInput( "Введите имя пользователя: " );
            string address = ReadNonEmptyInput( "Введите адрес: " );

            bool isConfirmed = ConfirmOrder(
                customerName,
                count,
                product,
                address );

            if ( isConfirmed )
            {
                PrintResult(
                    customerName,
                    product,
                    count,
                    address );
            }
            else
            {
                Console.WriteLine( "Отмена заказа, что то пошло не так" );
            }

            isOrderProcessing = ReadUserChoice( "Хотите оформить еще один заказ?" );
        }

        Console.WriteLine( "Спасибо за заказ! Досвидания." );
    }

    private static string ReadNonEmptyInput( string message )
    {
        string input = "";
        bool isValid = false;

        while ( !isValid )
        {
            Console.Write( message );
            input = ( Console.ReadLine() ?? "" ).Trim();

            isValid = !string.IsNullOrWhiteSpace( input );

            if ( !isValid )
            {
                Console.WriteLine( "Поле не может быть пустым. Попробуйте снова." );
            }
        }

        return input;
    }

    private static int GetProductCount()
    {
        int count = 0;
        bool isValid = false;

        while ( !isValid )
        {
            Console.Write( "Введите количество товара: " );
            string input = Console.ReadLine() ?? "";

            isValid = int.TryParse( input, out count ) && count > 0;

            if ( !isValid )
            {
                Console.WriteLine( "Введите корректное положительное число!" );
            }
        }

        return count;
    }

    private static bool ConfirmOrder(
        string customerName,
        int count,
        string product,
        string address )
    {
        string message = $"Здравствуйте, {customerName}, вы заказали {count} {product} на адрес {address}, все верно?";

        return ReadUserChoice( message );
    }

    private static bool ReadUserChoice( string inputStr )
    {
        string userChoice = "";
        bool isValid = false;

        while ( !isValid )
        {
            Console.Write( $"{inputStr} ( да/нет ): " );
            userChoice = Console.ReadLine()?.Trim().ToLower() ?? "";

            isValid = userChoice == "да" || userChoice == "нет";

            if ( !isValid )
            {
                Console.WriteLine( "Ошибка: Введите да или нет!" );
            }
        }

        return userChoice == "да";
    }

    private static void PrintResult(
        string customerName,
        string product,
        int count,
        string address )
    {
        DateTime dateDelivery = DateTime.Today.AddDays( DeliveryDays );
        string date = dateDelivery.ToString( "dd.MM.yyyy" );

        Console.WriteLine( $"{customerName}! Ваш заказ {product} в количестве {count} оформлен! Ожидайте доставку по адресу {address} к {date}" );
    }
}