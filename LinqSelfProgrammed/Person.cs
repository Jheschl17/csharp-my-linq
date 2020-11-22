namespace LinqSelfProgrammed
{
  public class Person
  {
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public int Age { get; set; }
    public int Sallary { get; set; }

    public override string ToString() =>  $"{Firstname} {Lastname} - {Age} / {Sallary}";
  }
}
