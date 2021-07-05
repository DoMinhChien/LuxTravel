using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models;

namespace Service.Interfaces
{
    public interface IOfferService
    {
        bool ProcessOffer(BookingModel offer);
        List<OfferModel> GetOffer(OfferFilterModel filterModel);
    }

}
