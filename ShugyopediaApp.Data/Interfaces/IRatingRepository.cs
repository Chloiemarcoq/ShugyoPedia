﻿using ShugyopediaApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Data.Interfaces
{
    public interface IRatingRepository
    {
        IQueryable<Rating> GetRatings();
        void DeleteRating(Rating rating);
        void AddRating(Rating rating);
    }
}
