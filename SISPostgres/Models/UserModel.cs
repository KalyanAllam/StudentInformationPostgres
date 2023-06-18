namespace SISPostgres.Models
{
    public class UserModel
    {
        public TeaType SelectTeaType { get; set; }
    }

    public enum TeaType
    {
       Final,  HalfYearly
    }
}
