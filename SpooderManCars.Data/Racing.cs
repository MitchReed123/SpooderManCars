using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooderManCars.Data
{
    public class Racing
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Manufacturer))]
        public int ManufacturerID { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        [Required]
        public string TeamName { get; set; }
        [Required]
        public string BasedOutOF { get; set; }
        [Required]
        public string Drivers { get; set; }
        [Required]
        public RaceEvent RaceEvent { get; set; }
        
    }

    public enum RaceEvent
    {
        F1,
        Nascar,
        IndyCar,
        DragRacing,
        SportsCarChampionship,
    }

}
