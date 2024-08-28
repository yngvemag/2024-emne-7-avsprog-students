namespace ExtensionMethods;

public static class IntExtension
{
    // Leg en metode som sjekker om en int er et par-tall
    public static bool IsEven(this int number) => number % 2 == 0;
    
    // lage en metode x.IsGreaterThan(23)
    public static bool IsGreaterThan(this int number, int number2) => number > number2;

}