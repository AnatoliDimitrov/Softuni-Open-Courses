﻿using NUnit.Framework;
using System;
using _02.VaniPlanning;

public class Test09
{
    [TestCase]
    public void ThrowInvoice_Agency_Should_Not_Contain_Deleted_Invoice()
    {
        //Arrange

        var agency = new Agency();
        var invoice = new Invoice("first", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 12, 28), new DateTime(2001, 10, 28));
        var invoice2 = new Invoice("second", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 11, 28), new DateTime(2001, 10, 28));
        var invoice3 = new Invoice("third", "SoftUni", 1200, Department.Incomes, new DateTime(2000, 10, 28), new DateTime(2001, 10, 28));


        //Act

        agency.Create(invoice);
        agency.Create(invoice2);
        agency.Create(invoice3);
        agency.ThrowInvoice("first");

       

        //Assert

        Assert.IsFalse(agency.Contains("first"));
    }
}
