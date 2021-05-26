using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace General.Models
{
    public class BusStop
    {
        public BusStop()
        {
        }

        public int IdBusStop { get; set; }

        public string NameOrAddressBusStop { get; set; }


    }
}
