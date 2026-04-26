class Program
{
    static double balance = 0;
    static bool isGameFinished = false;


    static void Main()
    {
        PrintHeader();
        while ( !isGameFinished )
        {
            PrintMenu();
            string options = Console.ReadLine() ?? "";
            OptionHandleResult result = HandleOption( options );

            if ( result != OptionHandleResult.Success )
            {
                Console.WriteLine( GetErrorMessage( result ) );
            }
        }
    }

    static string GetErrorMessage( OptionHandleResult result )
    {
        switch ( result )
        {
            case OptionHandleResult.InvalidOption:
                return "Ошибка: Неверный выбор меню.";
            case OptionHandleResult.InvalidDeposit:
                return "Ошибка: Неверная сумма депозита.";
            case OptionHandleResult.InvalidBet:
                return "Ошибка: Неверная ставка или недостаточно средств.";
            default:
                return "";
        }
    }

    static OptionHandleResult HandleOption( string option )
    {
        switch ( option )
        {
            case "1":
                return MakeDeposit();
            case "2":
                ShowBalance();
                break;
            case "3":
                return Play();
            case "4":
                return Exit();
            default:
                return OptionHandleResult.InvalidOption;
        }
        return OptionHandleResult.Success;
    }

    static OptionHandleResult MakeDeposit()
    {
        Console.Write( "Введите депозит для пополнения баланса: " );
        string depositStr = Console.ReadLine() ?? "";
        if ( !double.TryParse( depositStr, out double deposit ) || deposit <= 0 )
        {
            return OptionHandleResult.InvalidDeposit;
        }

        if ( double.MaxValue - deposit < balance )
        {
            return OptionHandleResult.InvalidDeposit;
        }

        balance += deposit;
        Console.WriteLine( $"Текущий баланс: {balance}" );
        return OptionHandleResult.Success;
    }

    static OptionHandleResult Exit()
    {
        isGameFinished = true;
        return OptionHandleResult.Success;
    }

    static OptionHandleResult Play()
    {
        Console.WriteLine( "Введите ставку: " );
        string betStr = Console.ReadLine() ?? "";

        if ( !int.TryParse( betStr, out int bet ) || bet <= 0 )
        {
            return OptionHandleResult.InvalidBet;
        }

        if ( bet >= balance )
        {
            return OptionHandleResult.InvalidBet;
        }

        var seed = Random.Shared.Next( 1, 21 );
        if ( seed >= 18 && seed <= 20 )
        {
            double winAmount = CalculateWinAmount( bet, seed );
            balance += winAmount;
            Console.WriteLine( $"Вы выиграли. Выпало {seed}. Выигрыш: {winAmount}. Новый баланс: {balance}" );
        }
        else
        {
            balance -= bet;
            Console.WriteLine( $"Вы проиграли. Выпало {seed}. Проигрыш: {bet}. Новый баланс: {balance}" );
        }
        return OptionHandleResult.Success;
    }

    static double CalculateWinAmount( int bet, int seed )
    {
        const int multiplicator = 20;
        int winPercent = ( multiplicator * seed ) % 17;
        double result = bet * ( 1 + winPercent );
        return result;
    }

    static OptionHandleResult ShowBalance()
    {
        Console.WriteLine( $"Ваш текущий баланс: {balance}" );
        return OptionHandleResult.Success;
    }

    static void PrintHeader()
    {
        Console.WriteLine( "My Casino game" );
        Console.WriteLine();
    }

    static void PrintMenu()
    {
        List<string> menu = new List<string>
        {
            "1. Пополнить баланс",
            "2. Показать баланс",
            "3. Сыграть",
            "4. Выйти"
        };

        foreach ( var item in menu )
        {
            Console.WriteLine( item );
        }
    }

    enum OptionHandleResult
    {
        Success = 0,
        InvalidOption = 1,
        InvalidDeposit = 2,
        InvalidBet = 3

    }
}