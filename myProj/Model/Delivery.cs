using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myProj.Model
{
    public class Delivery
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public double OriginLatitude { get; set; }

        public double OriginLongitude { get; set; }

        public double DestinationLatitude { get; set; }

        public double DestinationLongitude { get; set; }

        /// <summary>
        /// 0 = waiting delivery person
        /// 1 = being delivered
        /// 2 = delivered
        /// </summary>


        public int status { get; set; }

        public string DeliveryPersonId { get; set; }


        public static async Task<bool> MarkAsPickerUp(Delivery delivery,string deliveryPErsonId)
        {
            try
            {
                delivery.status = 1;
                delivery.DeliveryPersonId = deliveryPErsonId;
                await AzureHelper.MobileService.GetTable<Delivery>().UpdateAsync(delivery);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public static async Task<bool> MarkAsDelivered(Delivery delivery)
        {
            try
            {
                delivery.status = 2;
                await AzureHelper.MobileService.GetTable<Delivery>().UpdateAsync(delivery);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public static async Task<bool> MarkAsPickerUp(string deliveryId,string deliveryPErsonId)
        {
            try
            {
                var delivery = (await AzureHelper.MobileService.GetTable<Delivery>().Where(d => d.Id == deliveryId).ToListAsync()).FirstOrDefault();
                delivery.status = 1;
                delivery.DeliveryPersonId = deliveryPErsonId;
                await AzureHelper.MobileService.GetTable<Delivery>().UpdateAsync(delivery);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public static async Task<bool> MarkAsDelivered(string deliveryId)
        {
            try
            {
                var delivery = (await AzureHelper.MobileService.GetTable<Delivery>().Where(d => d.Id == deliveryId).ToListAsync()).FirstOrDefault();
                delivery.status = 2;
                await AzureHelper.MobileService.GetTable<Delivery>().UpdateAsync(delivery);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        //delivered right now packages by special person
        public static async Task<List<Delivery>> GetBeingDelivered(string id)
        {
            List<Delivery> delivered = new List<Delivery>();

            delivered = await AzureHelper.MobileService.GetTable<Delivery>().Where(d => d.status == 1 && d.DeliveryPersonId == id).ToListAsync();

            return delivered;
        }


        //Get all waiting for delivery packages
        public static async Task<List<Delivery>> GetWaiting()
        {
            List<Delivery> delivered = new List<Delivery>();

            delivered = await AzureHelper.MobileService.GetTable<Delivery>().Where(d => d.status == 0 ).ToListAsync();

            return delivered;
        }

        //All packages on the way right now and all packages waiting for delivery
        public static async Task<List<Delivery>> GetDeliveries()
        {
            List<Delivery> deliveries = new List<Delivery>();

            deliveries = await AzureHelper.MobileService.GetTable<Delivery>().Where(d=>d.status !=2).ToListAsync();

            return deliveries;
        }

        //All delivered completed packages
        public static async Task<List<Delivery>> GetDelivered()
        {
            List<Delivery> delivered = new List<Delivery>();

            delivered = await AzureHelper.MobileService.GetTable<Delivery>().Where(d => d.status == 2).ToListAsync();

            return delivered;
        }

        public static async Task<List<Delivery>> GetDelivered(string userId)
        {
            List<Delivery> delivered = new List<Delivery>();

            delivered = await AzureHelper.MobileService.GetTable<Delivery>().Where(d => d.status == 2 && d.DeliveryPersonId == userId).ToListAsync();

            return delivered;
        }

        public static async Task<bool> InsertDelivery(Delivery delivery)
        {
            return await AzureHelper.Insert<Delivery>(delivery);
        }

        public override string ToString()
        {
            return $"{Name} - {status}";
        }
    }
}
