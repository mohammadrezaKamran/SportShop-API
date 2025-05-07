using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.UserAgg.Enums;
using Shop.Domain.UserAgg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.UserAgg
{
    public class User : AggregateRoot
    {
        private User()
        {

        }
        public User(string name, string family, string phoneNumber, string email,
            string password, Gender gender, IUserDomainService userDomainService)
        {
            Guard(phoneNumber, email, userDomainService);

            Name = name;
            Family = family;
            PhoneNumber = phoneNumber;
            Email = email;
            Password = password;
            Gender = gender;
            AvatarName = "avatar.png";
            IsPhoneNumberVerified = false;
            Roles = new();
            Wallets = new();
            Tokens = new();
        }

        public string Name { get; private set; }
        public string Family { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string AvatarName { get; set; }
        public bool IsPhoneNumberVerified { get; set; }

        public Gender Gender { get; private set; }
        public List<UserRole> Roles { get; }
        public List<Wallet> Wallets { get; }
        public UserAddress Address { get; private set; }
        public List<UserToken> Tokens { get; }
        public List<WishList> WishLists { get; }

        public void Edit(string name, string family, string phoneNumber, string email,
            Gender gender, IUserDomainService userDomainService)
        {
            Guard(phoneNumber, email, userDomainService);
            Name = name;
            Family = family;
            PhoneNumber = phoneNumber;
            Email = email;
            Gender = gender;
        }

        public void ChangePassword(string newPassword)
        {
            NullOrEmptyDomainDataException.CheckString(newPassword, nameof(newPassword));

            Password = newPassword;
        }
        public static User RegisterUser(string phoneNumber, string password, IUserDomainService userDomainService)
        {
            return new User("", "", phoneNumber, null, password, Gender.None, userDomainService);
        }

        public void SetAvatar(string imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName))
                imageName = "avatar.png";

            AvatarName = imageName;
        }
        public void AddAddress(UserAddress address)
        {
            if (Address != null)
                throw new InvalidOperationException("Address is already set.");

            address.UserId = Id;
            Address = address;
        }

        public void DeleteAddress(long addressId)
        {
            if (Address == null || Address.Id != addressId)
                throw new NullOrEmptyDomainDataException("Address not found.");

            Address = null;
        }

        public void EditAddress(UserAddress address, long addressId)
        {
            if (Address == null || Address.Id != addressId)
                throw new NullOrEmptyDomainDataException("Address not found.");


            Address.Edit(address.Shire, address.City, address.PostalCode, address.PostalAddress, address.PhoneNumber,
                address.Name, address.Family, address.NationalCode);
        }

    

        public void ChargeWallet(Wallet wallet)
        {
            wallet.UserId = Id;
            Wallets.Add(wallet);
        }

        public void SetRoles(List<UserRole> roles)
        {
            roles.ForEach(f => f.UserId = Id);
            Roles.Clear();
            Roles.AddRange(roles);
        }

        public void AddToken(string hashJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device , string ipAddress)
        {
            // حذف توکن‌های منقضی‌شده
            Tokens.RemoveAll(t => t.RefreshTokenExpireDate <= DateTime.Now);

            var activeTokenCount = Tokens.Count(c => c.RefreshTokenExpireDate > DateTime.Now);
            if (activeTokenCount >= 3)
                throw new InvalidDomainDataException("امکان استفاده از 4 دستگاه همزمان وجود ندارد");

            var token = new UserToken(hashJwtToken, hashRefreshToken, tokenExpireDate, refreshTokenExpireDate, device, ipAddress);
            token.UserId = Id;
            Tokens.Add(token);
        }
        public string RemoveToken(long tokenId)
        {
            var token = Tokens.FirstOrDefault(f => f.Id == tokenId);
            if (token == null)
                throw new InvalidDomainDataException("invalid TokenId");

            Tokens.Remove(token);
            return token.HashJwtToken;
        }
        public void RemoveFromWishlist(long productId)
        {
            var item = WishLists.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
                WishLists.Remove(item);
        }

        public void AddToWishlist(long productId)
        {
            if (WishLists.Any(i => i.ProductId == productId))
                throw new Exception("قبلا به اضافه شده است");

            WishLists.Add(new WishList(productId));
        }

        public void VerifyPhoneNumber()
        {
            if (IsPhoneNumberVerified)
                throw new InvalidOperationException("شماره تلفن قبلا ثبت شده است");
            IsPhoneNumberVerified = true;
        }

        public void Guard(string phoneNumber, string email, IUserDomainService userDomainService)
        {
            NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));

            if (phoneNumber.Length != 11)
                throw new InvalidDomainDataException("شماره موبایل نامعتبر است");

            if (!string.IsNullOrWhiteSpace(email))
                if (email.IsValidEmail() == false)
                    throw new InvalidDomainDataException(" ایمیل  نامعتبر است");

            if (phoneNumber != PhoneNumber)
                if (userDomainService.PhoneNumberIsExist(phoneNumber))
                    throw new InvalidDomainDataException("شماره موبایل تکراری است");

            if (email != Email)
                if (userDomainService.IsEmailExist(email))
                    throw new InvalidDomainDataException("ایمیل تکراری است");
        }
    }
}