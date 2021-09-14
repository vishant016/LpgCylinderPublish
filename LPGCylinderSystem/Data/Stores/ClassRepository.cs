using LPGCylinderSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LPGCylinderSystem.Data.Stores
{
    public class ClassRepository<TUser> : IDisposable,Repository<TUser> where TUser : ApplicationUser
    {
        private readonly IMongoCollection<TUser> _users;
        private readonly IMongoCollection<Netbanking> _Netbankings;
        private readonly IMongoCollection<Dcard> _Cards;
        private readonly IMongoCollection<Complaint> _Complaints;
        private readonly IMongoCollection<Cylinder> _Cylinders;
        private readonly string _apikey;

        public ClassRepository(MongoTablesFactory mongoProxy, IConfiguration configuration)
        {
            _users = mongoProxy.GetCollection<TUser>(MongoTablesFactory.TABLE_USERS);
            _Netbankings= mongoProxy.GetCollection<Netbanking>(MongoTablesFactory.TABLE_NETBANKING);
            _Cards = mongoProxy.GetCollection<Dcard>(MongoTablesFactory.TABLE_CARD);
            _Complaints = mongoProxy.GetCollection<Complaint>(MongoTablesFactory.TABLE_COMPLAINT);
           _Cylinders = mongoProxy.GetCollection<Cylinder>(MongoTablesFactory.TABLE_CYLINDER);
            _apikey = configuration["APIKEY"];
        }



        public void Dispose()
        {

        }
        public List<TUser> GetuserssAsync()
        {

            var query = _users.Find(new BsonDocument()).ToListAsync();
            return query.Result;
        }
        public virtual async Task<IdentityResult> CreateCylinderAsync(Cylinder cylinder, CancellationToken token)
        {
            await _Cylinders.InsertOneAsync(cylinder, cancellationToken: token);
            return IdentityResult.Success;
        }

        //
        public virtual async Task<IdentityResult> CreateComplaintAsync(Complaint complaint, CancellationToken token)
        {
            await _Complaints.InsertOneAsync(complaint, cancellationToken: token);
            return IdentityResult.Success;
        }

        public virtual async Task<IdentityResult> CreateNetbankingAsync(Netbanking netbanking, CancellationToken token)
        {
            await _Netbankings.InsertOneAsync(netbanking, cancellationToken: token);
            return IdentityResult.Success;
        }

        public virtual async Task<IdentityResult> CreateCardAsync(Dcard card, CancellationToken token)
        {
            await _Cards.InsertOneAsync(card, cancellationToken: token);
            return IdentityResult.Success;
        }
        public void updateCard(Dcard card)
        {
            _Cards.ReplaceOne(item => item._id == card._id, card);
        }


        public void updateComplaint(Complaint complaint)
        {
            
        }

        public void updateNetbankingBalance(Netbanking banking)
        {
            _Netbankings.ReplaceOne(item => item._id == banking._id, banking);
        }
       
        public Task<Dcard> FindByCardAsync(string cardno)
                => _Cards.Find(u => u.CardNumber == cardno).FirstOrDefaultAsync();

        public async Task<Cylinder> SetQuantityofCylinderAsync(string cylindertype)
        {
            Cylinder cylinder = await _Cylinders.Find(c => c.CylinderType == cylindertype).FirstOrDefaultAsync();
            cylinder.Quantity = cylinder.Quantity -1;
            _Cylinders.ReplaceOne(item => item.CylinderType == cylindertype, cylinder);
            return cylinder;
        }
        public async Task<int> getprice(string cylindertype)
        {
            Cylinder cylinder = await _Cylinders.Find(c => c.CylinderType == cylindertype).FirstOrDefaultAsync();
            return cylinder.Price;
        } 
        public List<Cylinder> FindCylindersAsync()
        {
            var query = _Cylinders.Find(u=>u.Quantity>=1).ToListAsync();
            return query.Result;
        }
        public Task<Netbanking> FindNetbankingAsync(string username)
                => _Netbankings.Find(u => u.Username == username).FirstOrDefaultAsync();

        public Task<TUser> FindByConnectionIdAsync(string ConnectionId)
                 => _users.Find(u => u.Connection_Id == ConnectionId).FirstOrDefaultAsync();


        public Task<TUser> FindByIdAsync(string uId)
        {
            ObjectId o = new ObjectId(uId);
               return  _users.Find(u => u.Id ==o).FirstOrDefaultAsync();
        }

        //public void SendMailForPaper(string newMail,string subject,string body)
        //{
        //    using (MailMessage emailMessage = new MailMessage())
        //    {
        //        emailMessage.From = new MailAddress("lpgcylinderbookingsystem@gmail.com", "Lpg Cylinder System");
        //        emailMessage.To.Add(new MailAddress(newMail, newMail));
        //        emailMessage.Subject = subject;
        //        emailMessage.Body =body ;
        //        emailMessage.Priority = MailPriority.Normal;
        //        using (SmtpClient MailClient = new SmtpClient("smtp.gmail.com", 587))
        //        {
        //            MailClient.EnableSsl = true;
        //             MailClient.Credentials = new System.Net.NetworkCredential("lpgcylinderbookingsystem@gmail.com", "Aero@7878787878");
        //           // MailClient.Credentials = new System.Net.NetworkCredential("apikey", "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");
        //            MailClient.Send(emailMessage);
        //        }
        //    }

        //}

        public async void SendMailForPaper(string newMail, string subject, string body)
        {
            var apiKey = _apikey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("lpgcylinderbookingsystem@gmail.com", "LpgCylinderBookingSystem");
            //var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress(newMail, newMail);
           // var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>"+body+" </strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, body, htmlContent);
            var response = await client.SendEmailAsync(msg);
            
        }

    }
}
