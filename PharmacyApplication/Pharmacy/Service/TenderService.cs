﻿using Pharmacy.Model;
using Pharmacy.Repository;
using Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text; 

namespace Pharmacy.Service
{
   public class TenderService
    {
        ITendersRepository tenderRepository;
        IMedicineRepository medicineRepository;
        public TenderService(ITendersRepository tendersRepository, IMedicineRepository medicineRepository)
        {
            this.tenderRepository = tendersRepository;
            this.medicineRepository = medicineRepository;
        }

        public Tender GetByTenderHash(string id)
        {
            return tenderRepository.GetById(id);
        }

        public int GetQuantityForOfferItem(int itemQuantity, int medicineQuantity)
        { 
            if (medicineQuantity >= itemQuantity)
            {
                return itemQuantity;
            }
            else
            {
                return medicineQuantity;
            }
        }

        public OfferItem CheckMedicine(TenderItem item)
        {

            foreach (Medicine medicine in medicineRepository.GetAll())
            {
                if (medicine.MedicineName.Trim().Equals(item.MedicineName.Trim()) && medicine.Weigth == item.Dosage && medicine.Quantity != 0)
                {
                    int quantity = GetQuantityForOfferItem(item.Quantity, medicine.Quantity);
                    return new OfferItem(medicine.MedicineName, medicine.Weigth, quantity, medicine.Price);
                }
            }

            return null;
        }

        public List<OfferItem> FillOfferItems(List<TenderItem> tenderItems)
        {
            List<OfferItem> items = new List<OfferItem>();

            foreach (TenderItem item in tenderItems)
            { 
                OfferItem offer = CheckMedicine(item);
                if(offer != null){
                    items.Add(offer); 
                }
            }

            return items;
        }

        public double GetTotalPrice(List<OfferItem> items)
        {
            double totalPrice = 0;
            
            foreach(OfferItem item in items)
            {
                totalPrice += (item.Price * item.Quantity);
            }
            return totalPrice;
        }

        public TenderOffer CreateTenderOffer(Tender tender)
        {

            List<OfferItem> items = FillOfferItems(tender.TenderItems.ToList());
            TenderOffer offer = new TenderOffer(GenerateTenderOfferHash(),GetTotalPrice(items),items);
            tender.TenderOffers.Add(offer);
            tenderRepository.Update(tender);
            return offer;
        }

        public static string GenerateTenderOfferHash()
        {
            PharmacyDbContext context = new PharmacyDbContext();
            TenderOffer foundedOffer = null;
            string tenderHash = "";
            do
            {
                var key = new byte[32];
                using (var generator = RandomNumberGenerator.Create())
                    generator.GetBytes(key);
                tenderHash = Convert.ToBase64String(key);
                foundedOffer = context.TenderOffers.SingleOrDefault(tender => tender.TenderOfferHash == tenderHash);

            } while (foundedOffer != null);

            return tenderHash;
        }

        public void CloseTender(Tender tender)
        {
            tenderRepository.CloseTender(tender);
        }

        public void Update(Tender activeTender)
        {
            tenderRepository.Update(activeTender);
        }

        public Tender GetById(int id)
        {
            foreach (Tender t in tenderRepository.GetAll())
            {
                if (t.TenderId == id)
                {
                    return t;
                }
            }

            return null;
        }
    }
}
