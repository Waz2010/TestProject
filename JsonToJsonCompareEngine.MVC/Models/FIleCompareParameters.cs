using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace JsonToJsonCompareEngine.MVC.Models
{
    public class ViewModel
    {
        public ViewModel()
        {
            //States = new List<SelectListItem>();
            //Counties = new List<SelectListItem>();
            States = new List<State>();
        }
        //public List<SelectListItem> States { get; set; }
        //public List<SelectListItem> Counties { get; set; }
        public List<State> States { get; set; }
        public SelectList FilteredCounty { get; set; }
        [Display(Name ="State")]
        public string StateId { get; set; }
        [Display(Name = "County")]
        public string CountyId { get; set; }
    }
    public class State
    {
        public int Id { get; set; }
        public string StateName { get; set; }
    }

    public class County
    {
        public int Id { get; set; }
        public string CountyName { get; set; }
        public int StateId { get; set; }
        public string PreviousFileName { get; set; }
        public string CurrentFileName { get; set; }
    }
}
