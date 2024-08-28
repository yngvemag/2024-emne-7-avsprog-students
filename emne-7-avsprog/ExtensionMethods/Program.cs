// See https://aka.ms/new-console-template for more information

using ExtensionMethods;

int x = 55;
int y = 40;

Console.WriteLine($"{x} er partall: {x.IsEven()}");
Console.WriteLine($"{x} er større enn {y}: {x.IsGreaterThan(y)}");