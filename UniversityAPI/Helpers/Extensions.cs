using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using UniversityAPI.Models;
using UniversityAPI.Services;

namespace UniversityAPI.Helpers
{
    public static class StringExtensions
    {
        /// <summary>
        /// Check if string contains given statement with given comapre attributes 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="toCheck"></param>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }

        /// <summary>
        /// Update object with information about : who made modification and when
        /// </summary>
        /// <param name="context"></param>
        /// <param name="source"></param>
        /// <param name="claimsInfo"></param>
        public static void UpdateModificationInformation(this EduDataContext context, object source, ClaimsPrincipal claimsInfo)
        {
            var userId = claimsInfo.Claims.Where(c => c.Type == UserAuthenticationService.CLAIM_USER_ID).FirstOrDefault()?.Value;
            UpdateField(source, "ModificationDate", DateTime.UtcNow);

            if(int.TryParse(userId, out int Id))
            {
                UpdateField(source, "ModifiedBy", Id);
            }
            
        }

        /// <summary>
        /// Create object information about who and when create this object.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="source"></param>
        /// <param name="claimsInfo"></param>
        public static void CreateModificationInformation(this EduDataContext context, object source, ClaimsPrincipal claimsInfo)
        {
            var userId = claimsInfo.Claims.Where(c => c.Type == UserAuthenticationService.CLAIM_USER_ID).FirstOrDefault()?.Value;
            UpdateField(source, "CreationDate", DateTime.UtcNow);

            if (int.TryParse(userId, out int Id))
            {
                UpdateField(source, "ModifiedBy", Id);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        private static void UpdateField(object obj, string fieldName, object fieldValue)
        {
            PropertyInfo prop = obj.GetType().GetProperty(fieldName);

            if(prop != null && prop.CanWrite)
            {
                prop.SetValue(obj, fieldValue);
            }
        }

    }
}
