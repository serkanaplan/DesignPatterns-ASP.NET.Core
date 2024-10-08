﻿using ChainOfResponsibiltyDesignPattern.DAL;
using ChainOfResponsibiltyDesignPattern.Models;

namespace ChainOfResponsibiltyDesignPattern.ChainOfResponsibility;

public class Manager : Employee
{
    public override void ProcessRequest(CustomerProcessViewModel req)
    {
        Context context = new();
        if (req.Amount <= 250000)
        {
            CustomerProcess customerProcess = new()
            {
                Amount = req.Amount.ToString(),
                Name = req.Name,
                EmployeeName = "Şube Müdürü - Hatice Sarı",
                Description = "Para Çekme İşlemi Onaylandı, Müşteriye Talep Ettiği Tutar Ödendi"
            };
            context.CustomerProcesses.Add(customerProcess);
            context.SaveChanges();
        }
        else if (NextApprover != null)
        {
            CustomerProcess customerProcess = new()
            {
                Amount = req.Amount.ToString(),
                Name = req.Name,
                EmployeeName = "Şube Müdürü - Hatice Sarı",
                Description = "Para Çekme Tutarı Şube Müdürünün Günlük Ödeyebileceği Limiti Aştığı İçin İşlem Bölge Müdürüne Yönlendirildi"
            };
            context.CustomerProcesses.Add(customerProcess);
            context.SaveChanges();
            NextApprover.ProcessRequest(req);
        }
    }
}
