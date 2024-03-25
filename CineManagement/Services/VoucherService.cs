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
        public List<Voucher> GetUnusedVouchersByUserId(int userId)
        {
            using (var context = new CinemaManagementContext())
            {
                List<Voucher> vouchers = context.Vouchers.Where(voucher => voucher.UserId == userId && voucher.IsUsed == false).ToList();

                if (vouchers == null)
                {
                    throw new Exception("No voucher found");
                }
                else
                {
                    return vouchers;
                }
            }
        }

        public bool UpdateVoucherStatus(Voucher voucher)
        {
            using (var _context = new CinemaManagementContext())
            {
                var existingVoucher = _context.Vouchers.FirstOrDefault(x => x.VoucherId == voucher.VoucherId);

                if (existingVoucher != null)
                {
                    existingVoucher.IsUsed = true;

                    try
                    {
                        _context.SaveChanges();
                        return true;
                    }
                    catch (DbUpdateException ex)
                    {
                        throw new Exception("An error occurred while updating the voucher's status.", ex);
                    }
                }
                else
                {
                    throw new Exception("Voucher does not exists.");
                }
            }
        }
    }
}
