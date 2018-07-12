using OMC.Models;
using System.Collections.Generic;

namespace OMCWebApp.Models
{
    public class DoctorModel
    {
        public DoctorProfile DoctorProfileObject { get; set; }
        public List<TimezoneMaster> Timezones { get; set; }
        /// <summary>
        /// Use this for logged in user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// this is used to create the user address record
        /// </summary>
        public UserAddressModel UserAddressModelObject { get; set; }
        public UserAddressResponse UserAddressResponseObject { get; set; }

        /// <summary>
        /// this is used to create the doctor award record
        /// </summary>
        public DoctorAwardModel DoctorAwardModelObject { get; set; }
        public DoctorAwardsResponse DoctorAwardsResponseObject { get; set; }

        /// <summary>
        /// this is used to create the doctor board record
        /// </summary>
        public DoctorBoardModel DoctorBoardModelObject { get; set; }
        public DoctorBoardResponse DoctorBoardResponseObject { get; set; }

        /// <summary>
        /// this is used to create the doctor Education record
        /// </summary>
        public DoctorEducationModel DoctorEducationModelObject { get; set; }
        public DoctorEducationResponse DoctorEducationResponseObject { get; set; }

        /// <summary>
        /// this is used to create the doctor Fellowship record
        /// </summary>
        public DoctorFellowshipModel DoctorFellowshipModelObject { get; set; }
        public DoctorFellowshipResponse DoctorFellowshipResponseObject { get; set; }

        /// <summary>
        /// this is used to create the doctor image record
        /// </summary>
        public DoctorImageModel DoctorImageModelObject { get; set; }
        public DoctorImagesResponse DoctorImagesResponseObject { get; set; }

        /// <summary>
        /// this is used to create the doctor Residency record
        /// </summary>
        public DoctorResidencyModel DoctorResidencyModelObject { get; set; }
        public DoctorResidencyResponse DoctorResidencyResponseObject { get; set; }

        public DoctorModel()
        {
            DoctorProfileObject = new DoctorProfile();
        }
    }
}