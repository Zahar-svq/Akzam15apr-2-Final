using System;

namespace SuperBasketball.Models;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Mass { get; set; }
    public double Height { get; set; }
    public DateTimeOffset Birthday { get; set; }
    public DateTimeOffset DateStart { get; set; }
    
    public string Position { get; set; }
    public string Team { get; set; }
    
    public int TeamId { get; set; }
    public int PositionId { get; set; }
}