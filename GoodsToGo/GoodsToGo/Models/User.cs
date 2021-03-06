//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GoodsToGo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password didn't match")]
        public string Re_Password { get; set; }
        public Nullable<int> BarangayID { get; set; }
        public string House_No { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Street { get; set; }
        public string Purok_No { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Last_Name { get; set; }
        public Nullable<int> Gender { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Phone_No { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Birthdate { get; set; }
        public string ResetPassCode { get; set; }
    }
}
