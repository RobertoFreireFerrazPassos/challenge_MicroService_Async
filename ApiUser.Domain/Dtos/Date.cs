namespace UserRegistration.Domain.Dtos
{
    public class Date
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public override string ToString() => Day + "/" + Month + "/" + Year;
    }
}
