using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Models;

namespace UniversityAPI.Services
{
    /// <summary>
    /// The base class for all repositories that manipulate over resources.
    /// </summary>
    public class BaseRepository
    {
        /// <summary>
        /// Reference to database context
        /// </summary>
        protected EduDataContext context;

        /// <summary>
        /// Save all changes to database. Must be called in order to save data to database.
        /// </summary>
        /// <returns>TRUE if save succesfully, otherwise return FALSE.</returns>
        public virtual bool Save()
        {
            return (context.SaveChanges() >= 0);
        }


        

    }
}
