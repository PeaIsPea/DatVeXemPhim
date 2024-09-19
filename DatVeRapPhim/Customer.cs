using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatVeRapPhim
{
    public class Customer
    {
        public Customer()
        {
            Ghes = new List<int>();
        }
        public string Name { get; set; }
        public string Phone { get; set; }
        public List<int> Ghes { get; set; }

        public int TinhTien
        {
            get
            {
                return Ghes.Count * 100000;
            }
        }

        public DateTime GioDatGhe { get;set; }
        

        public override string ToString()
        {
            return Name +"-" + Phone;
        }

    }
}
