{
    if (args.Length == 1)
    {
        if (args[0].Length < 15)
        {
            System.Console.WriteLine("Invalid input length. Please provide 4 or 5 color bands.");
            return;
        }
        for (int i = 3; i < args[0].Length; i += 4)
        {
            if (args[0][i] != '-')
            {
                System.Console.WriteLine("Invalid input format. Please use a hyphen (-) to separate color codes");
                return;
            }
        }
        double value = 0;
        double tolerance = 0;
        string color5 = "";
        string color1 = "";
        string color2 = "";
        string color3 = "";
        string color4 = "";
        color1 = args[0].Substring(0, 3);
        color2 = args[0].Substring(4, 3);
        color3 = args[0].Substring(8, 3);
        color4 = args[0].Substring(12, 3);
        if (args[0].Length > 15)
        {
            color5 = args[0].Substring(16, 3);
        }
        if (args[0].Length < 19)
        {
            if (TryDecode4ColorBands(color1, color2, color3, color4, out value, out tolerance))
            {
                System.Console.WriteLine($"Resistance: {value} Ohm");
                System.Console.WriteLine($"Tolerance: ±{tolerance}%");
            }
            else
            {
                System.Console.WriteLine("Invalid color code. Please use valid color codes.");
            }
        }
        else
        {
            if (TryDecode5ColorBands(color1, color2, color3, color4, color5, out value, out tolerance))
            {
                System.Console.WriteLine($"Resistance: {value} Ohm");
                System.Console.WriteLine($"Tolerance: ±{tolerance}%");
            }
            else
            {
                System.Console.WriteLine("Invalid color code. Please use valid color codes.");
            }
        }
    }
    else
    {
        System.Console.WriteLine("not enough arguments");
        return;
    }
}

bool TryConvertColorToDigit(string color, out double digit)
{
    switch (color)
    {
        case "Bla": digit = 0; break;
        case "Bro": digit = 1; break;
        case "Red": digit = 2; break;
        case "Ora": digit = 3; break;
        case "Yel": digit = 4; break;
        case "Gre": digit = 5; break;
        case "Blu": digit = 6; break;
        case "Vio": digit = 7; break;
        case "Gra": digit = 8; break;
        case "Whi": digit = 9; break;
        default: digit = -1; return false;
    }
    return true;
}
bool TryGetMultiplierFromColor(string color, out double multiplier)
{
    multiplier = 0;
    switch (color)
    {
        case "Sil": multiplier = 0.01d; break;
        case "Gol": multiplier = 0.1d; break;
        default:
            if (!TryConvertColorToDigit(color, out multiplier))
            {
                return false;
            }
            else if (TryConvertColorToDigit(color, out multiplier))
            { multiplier = Math.Pow(10, multiplier);} break;
    }
    return true;
}
bool TryGetToleranceFromColor(string color, out double tolerance)
{
    switch (color)
    {
        case "Bro": tolerance = 1; break;
        case "Red": tolerance = 2; break;
        case "Gre": tolerance = 0.5; break;
        case "Blu": tolerance = 0.25; break;
        case "Vio": tolerance = 0.1; break;
        case "Gra": tolerance = 0.05; break;
        case "Gol": tolerance = 5; break;
        case "Sil": tolerance = 10; break;
        default: tolerance = -1; return false;
    }
    return true;
}
bool TryDecode4ColorBands(string color1, string color2, string color3, string color4, out double value, out double tolerance)
{
    double digit1 = 0;
    double digit2 = 0;
    double multiplier = 0;
    value = 0;
    if (!TryGetToleranceFromColor(color4, out tolerance))
    {
        return false;
    }
    else if (!TryConvertColorToDigit(color1, out digit1))
    {
        return false;
    }
    else if (!TryConvertColorToDigit(color2, out digit2))
    {
        return false;
    }
    else if (!TryGetMultiplierFromColor(color3, out multiplier))
    {
        return false;
    }

    value = Math.Round((digit1 * 10 + digit2) * multiplier, 2);
    return true;
}
bool TryDecode5ColorBands(string color1, string color2, string color3, string color4, string color5, out double value, out double tolerance)
{
    double digit1 = 0;
    double digit2 = 0;
    double digit3 = 0;
    double multiplier = 0;
    value = 0;
    if (!TryGetToleranceFromColor(color5, out tolerance))
    {
        return false;
    }
    else if (!TryConvertColorToDigit(color1, out digit1))
    {
        return false;
    }
    else if (!TryConvertColorToDigit(color2, out digit2))
    {
        return false;
    }
    else if (!TryConvertColorToDigit(color3, out digit3))
    {
        return false;
    }
    else if (!TryGetMultiplierFromColor(color4, out multiplier))
    {
        return false;
    }

    value = Math.Round(((digit1 * 10 + digit2) * 10 + digit3) * multiplier, 2);
    return true;
}
