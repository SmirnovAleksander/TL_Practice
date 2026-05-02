namespace OrderManager;

internal class Program
{
    private const int DeliveryDays = 3;
    static void Main( string[] args )
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

            isOrderProcessing = ReadYesNo( "Хотите оформить еще один заказ?" );
        }
        Console.WriteLine( "Спасибо за заказ! Досвидания." );
    }

    static string ReadNonEmptyInput( string message )
    {
        string input = "";
        bool isValid = false;

        while ( !isValid )
        {
            Console.Write( message );
            input = ( Console.ReadLine() ?? "" ).Trim();

            if ( !string.IsNullOrWhiteSpace( input ) )
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine( "Поле не может быть пустым. Попробуйте снова." );
            }
        }
        return input;
    }

    static int GetProductCount()
    {
        int count = 0;
        bool isValid = false;

        while ( !isValid )
        {
            Console.Write( "Введите количество товара: " );
            string input = Console.ReadLine() ?? "";

            if ( int.TryParse( input, out count ) && count > 0 )
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine( "Введите корректное положительное число!" );
            }
        }
        return count;
    }

    static bool ConfirmOrder(
        string customerName,
        int count,
        string product,
        string address )
    {
        string message = $"Здравствуйте, {customerName}, вы заказали {count} {product} на адрес {address}, все верно?";
        return ReadYesNo( message );
    }

    static bool ReadYesNo( string inputStr )
    {
        string userChoice = "";
        bool isValid = false;

        while ( !isValid )
        {
            Console.Write( $"{inputStr} ( да/нет ): " );
            userChoice = Console.ReadLine()?.Trim().ToLower() ?? "";

            if ( userChoice == "да" || userChoice == "нет" )
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine( "Ошибка: Введите да или нет!" );
            }
        }
        return userChoice == "да";
    }

    static void PrintResult(
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