using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerInfoApp.Model;
using CustomerInfoApp.Repository;

namespace CustomerInfoApp.BLL
{
    public class DistrictManager
    {
        CustomerRepository _customerRepository = new CustomerRepository();
        DistrictRepository _districtRepository = new DistrictRepository();

        public List<District> LoadDistrict()
        {
            return _districtRepository.LoadDistrict();
        }
    }
}
