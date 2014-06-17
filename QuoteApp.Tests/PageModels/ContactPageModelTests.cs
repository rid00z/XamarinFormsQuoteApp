using System;
using NUnit.Framework;
using QuoteApp.PageModels;
using QuoteApp.Services;
using QuoteApp.Tests.Dependancies;
using QuoteApp.Models;
using FakeItEasy;
using QuoteApp.Pages;

namespace QuoteApp.Tests.PageModels
{
    [TestFixture]
    public class ContactPageModelTests
    {
        [Test]
        public static void CreateNewContact()
        {
            var container = A.Fake<IRootNavigation> ();
            TinyIoC.TinyIoCContainer.Current.Register<IRootNavigation> (container);

            var db = new DatabaseService (new SQLiteFactory());
            var vm = new ContactPageModel (db);
            vm.Init (null);

            //save to database
            vm.Contact.Name = "Peter";
            vm.Contact.Phone = "9087";
            vm.Done.Execute (null);

            Assert.IsTrue (vm.Contact.ContactId > 0);

            //get from database
            var savedContact 
                    = db.Conn.Table<Contact> ().Where ((c) => c.ContactId == vm.Contact.ContactId).FirstOrDefault ();

            Assert.AreEqual ("Peter", savedContact.Name);
            Assert.AreEqual ("9087", savedContact.Phone);
            A.CallTo (() => container.PopPage ()).MustHaveHappened ();
        }
    }
}

