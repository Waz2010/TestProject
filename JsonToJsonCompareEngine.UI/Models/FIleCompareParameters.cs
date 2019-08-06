using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JsonToJsonCompareEngine.UI.Models
{
    public class ViewModel
    {
        public List<State> States { get; set; }
        public SelectList FilteredCounty { get; set; }
        public string SelectedPreviousFile { get; set; }
        public string SelectedCurrentFile { get; set; }
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
        public List<string> Files { get; set; }
    }
}
