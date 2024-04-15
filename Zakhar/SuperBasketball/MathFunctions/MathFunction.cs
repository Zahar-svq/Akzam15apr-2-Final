namespace MathFunctions;

public class MathFunction
{
    public static double TimeFlyBall(double startSpeed)
    {
        double g = 9.8;
        double time = 2 * startSpeed / g;
        return time;
    }
}