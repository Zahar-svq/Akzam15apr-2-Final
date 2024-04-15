using MathFunctions;

namespace ModulTest;

public class Tests
{
    [Fact]
    public void TimeFlyFrom_9_8_StartSpeed()
    {
        // Arrange
        double startSpeed = 9.8;
        double expected = 2;

        // Act
        double actual = MathFunction.TimeFlyBall(startSpeed);

        // Assert
        Assert.Equal(expected, actual);
    }
    
    
    [Fact]
    public void TimeFlyFrom_4_9_StartSpeed()
    {
        // Arrange
        double startSpeed = 4.9;
        double expected = 1;

        // Act
        double actual = MathFunction.TimeFlyBall(startSpeed);

        // Assert
        Assert.Equal(expected, actual);
    }
}