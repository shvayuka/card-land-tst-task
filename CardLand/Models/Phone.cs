namespace CardLand.Models
{
    public class Phone
    {
        public int Id { get; set; }

        public int OfficeId { get; set; }

        public string PhoneNumber { get; set; }

        public string? Additional { get; set; }

        //Не нужно, учитывая Owning и доступ через Office. Закомментировано исключительно для разъяснения
        //public Office Office { get; set; }
    }
}
