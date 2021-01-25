    using System;

    namespace LuxTravel.Model.Dtos
{
    public class PaymentConfirmDto
    {
        public Guid BookingId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentId { get; set; }
    }
}
