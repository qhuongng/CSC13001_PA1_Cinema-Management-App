using CineManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineManagement.Services
{
    public class VoucherService
    {
        public List<Voucher> getVouchers()
        {
            using (var context = new CinemaManagementContext())
            {
                List<Voucher> vouchers = context.Vouchers.ToList();
                if (vouchers == null)
                {
                    throw new Exception("No voucher found.");
                }
                else
                {
                    return vouchers;
                }
            }
        }
        public Voucher getVoucherById(int id)
        {
            using (var context = new CinemaManagementContext())
            {
                Voucher vch = context.Vouchers.FirstOrDefault(x => x.VoucherId == id);

                if (vch == null)
                {
                    throw new Exception("Voucher not found.");
                }
                else
                {
                    return vch;
                }
            }
        }
        public bool deleteVoucherById(int id)
        {
            using (var context = new CinemaManagementContext())
            {
                Voucher vch = context.Vouchers.FirstOrDefault(m => m.VoucherId == id);
                if (vch == null)
                {
                    throw new Exception("Movie not found.");
                }
                else
                {
                    context.Vouchers.Remove(vch);
                    context.SaveChanges();
                    return true;
                }
            }
        }
        public bool updateVoucher(Voucher voucher)
        {
            using (var context = new CinemaManagementContext())
            {
                Voucher oldVch = context.Vouchers.FirstOrDefault(m => m.VoucherId.Equals(voucher.VoucherId));
                
                if (oldVch == null)
                {
                    throw new Exception("Voucher not found.");
                }
                else
                {
                    oldVch.UserId = voucher.UserId;
                    oldVch.IsUsed = voucher.IsUsed;
                    oldVch.DiscountPercent = voucher.DiscountPercent;
                    context.SaveChanges();
                    return true;
                }
            }
        }
    }
}
