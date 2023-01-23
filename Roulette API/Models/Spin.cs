namespace Roulette_API.Models
{
    public class Spin
    {
    public int SpinID { get; set; }
    public string? Result { get; set; }
    public bool isRed { get; set; }
    public bool isBlack { get; set; }
    public bool isGreen { get; set; }
    public bool isEven { get; set; }
    public bool isOdd { get; set; }
    public bool isNumberRange1 { get; set; }
    public bool isNumberRange2 { get; set; }
  }
}
